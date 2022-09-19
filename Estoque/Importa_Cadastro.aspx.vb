Imports System.Xml 
Imports System
Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration
Imports System.Data.OleDb

Partial Class Municipio_Estoque_Default
  Inherits System.Web.UI.Page

  Dim cnnSQL As String = ""
  Dim asp_id_empresa As String
  Dim asp_id_usuario As String
  Dim asp_id_lote As String
  Dim Session_ID As String = ""
  Dim asp_cnpj_emp As String = ""
  Dim asp_function As String = ""


  'busca string de conexão a partir do WebConfig
  Private Sub GetAllConnectionStrings()
      Dim collCS As ConnectionStringSettingsCollection
      Try
          collCS = ConfigurationManager.ConnectionStrings
      Catch ex As Exception
          collCS = Nothing
      End Try

      If collCS IsNot Nothing Then
          For Each cs As ConnectionStringSettings In collCS
            If cs.Name = "maxcont_cloud" Then
              Session("cnxStr") = (cs.ConnectionString)
            Else
              Session("cnxStr") = ""
            End If
          Next
      Else
        Session("cnxStr") = ""
      End If

      collCS = Nothing
  End Sub

  Private Sub Carrega_Session(txt_Session As String)
    Dim strSQL As String = "EXEC spSession_Compartilha_Pega '" & txt_Session & "'"

    Using adapter As New SqlDataAdapter(strSQL, cnnSQL)
      Dim ds As New DataSet
      adapter.Fill(ds, "mResult")

      Dim conta As Integer = 0
      For Each dr As DataRow In ds.Tables("mResult").Rows

        asp_id_empresa = dr("id_empresa").ToString()
        asp_id_usuario = dr("id_usuario").ToString()
        asp_id_lote = dr("param1").ToString()
        asp_cnpj_emp = dr("cnpj_emp").ToString()

      Next
    End Using
  End Sub

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    GetAllConnectionStrings()

    cnnSQL = Session("cnxStr").ToString

    'Session_ID = Request.QueryString("_Session_2").ToString.Replace(",","")
    'asp_function = Request.QueryString("_func_2").ToString.Replace(",", "")

    'Carrega_Session(Session_ID)

    Carrega_Excel()

  End Sub

  Public Function Carrega_Campo(ByVal Campo As String) As String
    Campo = Campo.Replace("""", "")
    Campo = """" & Campo & """;"
    Return Campo
  End Function


  Public Sub Grava_Dados()

  End Sub

  Public Sub Carrega_Excel()
    Dim result As String = ""
    Dim cod_result As String = ""
    Dim strCnn2 As String = GetConnectionString("maxcont_cloud")
    Dim cnn2 As New SqlConnection(strCnn2)

    Dim asp_caminho_arquivo As String

    asp_caminho_arquivo = Server.MapPath("../../Temp/IMPORTA_CADASTRO.xlsx")

    'Response.write(asp_caminho_arquivo)
    'Response.end()

    Dim strConn_Excel As String = "Provider=Microsoft.ACE.OLEDB.12.0;" _
                          & "data source=""" & asp_caminho_arquivo & """;" _
                          & "Extended Properties=""Excel 12.0;HDR=YES"";"
    'Primeiro DataGrid - vou exibir todos os dados da planilha
    Dim objConn_Excel As New OleDbConnection(strConn_Excel)
    Dim strSql_Excel As String = "Select cod_produto, descr, NCM, unid_estoque,"
    strSql_Excel = strSql_Excel & " ICMS_orig, Unid_Comercial, EAN, preco_venda1, ICMS_ST_entrada,"
    strSql_Excel = strSql_Excel & " Peso_liquido, IPI_aliq , ICMS_aliq_interna, ICMS_Red_Perc, Qtde_Estoque_min, p_Markup,"
    strSql_Excel = strSql_Excel & " CFOP_Venda, PIS_CST, COFINS_CST, ICMS_CST, CSOSN_CST, Vlr_Aprox_Tributos_Perc,"
    strSql_Excel = strSql_Excel & " Fonte_Tribut_Vlr_Tributos, CEST, IPI_CST From [cadastro_modelo$]"
    lblSql1.Text = strSql_Excel

    Dim objCmd_Excel As New OleDbCommand(strSql_Excel, objConn_Excel)


    Try
      'abre a conexão com a fonte de dados e executa a consulta SQL para retornar os dados e vinculando-os ao datagrid1
      objConn_Excel.Open()

      'dtgAgenda1.DataSource = objCmd_Excel.ExecuteReader()
      'dtgAgenda1.DataBind()


      Dim ssqltable As String = "Aux_Estoq_Itens_Excel_Importa"

      'series of commands to bulk copy data from the excel file into our sql table 
      'Dim oledbconn As New OleDbConnection(sexcelconnectionstring)
      'Dim oledbcmd As New OleDbCommand(myexceldataquery, oledbconn)

      'oledbconn.Open()
      'Dim dr As OleDbDataReader = objCmd.ExecuteReader()

      Dim objDR As OleDbDataReader


      Using bulkCopy As SqlBulkCopy = New SqlBulkCopy(cnnSQL)

      bulkCopy.DestinationTableName = "dbo.Aux_Estoq_Itens_Excel_Importa"
        'Try
          objDR = objCmd_Excel.ExecuteReader()
          bulkCopy.WriteToServer(objDR)

          objConn_Excel.Close()
          objDR.Close()
          bulkCopy.Close()

        'Catch ex As Exception
          'MsgBox(ex.ToString)
        'End Try
      End Using

    Catch exc As Exception
      Response.Write(exc.ToString())
    Finally
      objConn_Excel.Dispose()
    End Try

  End Sub

  Private Shared Function GetConnectionString(ByVal name As String) As String

   Dim settings As ConnectionStringSettings = ConfigurationManager.ConnectionStrings(name)
   If Not settings Is Nothing Then
    Return settings.ConnectionString
   Else
    Return Nothing
   End If
  End Function

End Class
