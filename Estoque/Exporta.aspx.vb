Imports System.Xml 
Imports System
Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration



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
            If cs.Name = "maxcont_cloud" then
              Session("cnxStr") = (cs.ConnectionString)
            Else
              Session("cnxStr") = ""
            end if
          Next
      Else
        Session("cnxStr") = ""
      End If

      collCS = Nothing
  End Sub

  Private Sub Carrega_Session (txt_Session As string)
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

    Session_ID = Request.QueryString("_Session_2").ToString.Replace(",","")
    asp_function = Request.QueryString("_func_2").ToString.Replace(",", "")

    Carrega_Session(Session_ID)

    If asp_function = "CAD_EMPRESA" Then
      Pega_Cadastro_Empresa(asp_id_empresa)
    Else
      Pega_Itens_Estoque_Arquivo(asp_id_empresa)
    End If

  End Sub

  Public Function Carrega_Campo(ByVal Campo As String) As String
    Campo = Campo.Replace("""", "")
    Campo = """" & Campo & """;"
    Return Campo
  End Function


  Public Sub Pega_Cadastro_Empresa(ByVal id_empresa As String)
    Dim result As String = ""
    Dim cod_result As String = ""
    Dim strCnn2 As String = GetConnectionString("maxcont_cloud")
    Dim cnn2 As New SqlConnection(strCnn2)

    'Dim strSQL As String = "EXEC spWS_Empresa_Pega_Cadastro '" & id_empresa & "','" & asp_cnpj_emp & "','CONSULTAR'"
    Dim command As SqlCommand = New SqlCommand("EXEC spWS_Empresa_Pega_Cadastro '" & id_empresa & "','" & asp_cnpj_emp & "','CONSULTAR'", cnn2)

    cnn2.Open()
    Dim reader As SqlDataReader = command.ExecuteReader()

    Dim path_download As String = ""

    Dim path As String = Server.MapPath("../../Temp/Produtos_WS/" & CStr(id_empresa) & ".TXT")

    Dim txt_cabecalho As String = ""

    txt_cabecalho = "id_empresa|"
    txt_cabecalho = txt_cabecalho & "razao_empresa|"
    txt_cabecalho = txt_cabecalho & "fantasia|"
    txt_cabecalho = txt_cabecalho & "CNPJ|"
    txt_cabecalho = txt_cabecalho & "IE|"
    txt_cabecalho = txt_cabecalho & "IM|"
    txt_cabecalho = txt_cabecalho & "CRT|"
    txt_cabecalho = txt_cabecalho & "descr_CRT|"
    txt_cabecalho = txt_cabecalho & "xLgr|"
    txt_cabecalho = txt_cabecalho & "nro|"
    txt_cabecalho = txt_cabecalho & "xCpl|"
    txt_cabecalho = txt_cabecalho & "bairro|"
    txt_cabecalho = txt_cabecalho & "cMun|"
    txt_cabecalho = txt_cabecalho & "xMun|"
    txt_cabecalho = txt_cabecalho & "UF|"
    txt_cabecalho = txt_cabecalho & "CEP|"
    txt_cabecalho = txt_cabecalho & "fone|"
    txt_cabecalho = txt_cabecalho & "email|"
    txt_cabecalho = txt_cabecalho & "SAT_assinatura_digital|"
    txt_cabecalho = txt_cabecalho & "tipo_PDV|"
    'txt_cabecalho = txt_cabecalho & "{"
    If File.Exists(path) Then
      File.Delete(path)
    End If
    If Not File.Exists(path) Then
      ' Create a file to write to. 
      Using sw As StreamWriter = File.CreateText(path)
        sw.WriteLine(txt_cabecalho)
        If reader.HasRows Then
          Do While reader.Read()
            txt_cabecalho = ""
            txt_cabecalho = txt_cabecalho & reader.GetString(0) & "|" 'id_empresa
            txt_cabecalho = txt_cabecalho & reader.GetString(1) & "|" 'razao_empresa
            If reader.GetString(2) <> "" Then
              txt_cabecalho = txt_cabecalho & reader.GetString(2) & "|" 'fantasia
            Else
              txt_cabecalho = txt_cabecalho & "--|" 'fantasia
            End If
            txt_cabecalho = txt_cabecalho & reader.GetString(3) & "|" 'CNPJ_emp
            txt_cabecalho = txt_cabecalho & reader.GetString(4) & "|" 'IE
            If reader.GetString(5) <> "" Then
              txt_cabecalho = txt_cabecalho & reader.GetString(5) & "|" 'IM
            Else
              txt_cabecalho = txt_cabecalho & "--|" 'IM
            End If

            txt_cabecalho = txt_cabecalho & reader.GetString(6) & "|" 'CRT
            txt_cabecalho = txt_cabecalho & reader.GetString(7) & "|" 'descr_CRT
            txt_cabecalho = txt_cabecalho & reader.GetString(8) & "|" 'xLgr
            txt_cabecalho = txt_cabecalho & reader.GetString(9) & "|" 'nro
            If reader.GetString(10) <> "" Then
              txt_cabecalho = txt_cabecalho & reader.GetString(10) & "|" 'xCpl
            Else
              txt_cabecalho = txt_cabecalho & "--|" 'xCpl
            End If

            txt_cabecalho = txt_cabecalho & reader.GetString(11) & "|" 'bairro
            txt_cabecalho = txt_cabecalho & reader.GetString(12) & "|" 'cMun
            txt_cabecalho = txt_cabecalho & reader.GetString(13) & "|" 'xMun
            txt_cabecalho = txt_cabecalho & reader.GetString(14) & "|" 'UF
            txt_cabecalho = txt_cabecalho & reader.GetString(15) & "|" 'CEP
            If reader.GetString(16) <> "" Then
              txt_cabecalho = txt_cabecalho & reader.GetString(16) & "|" 'fone
            Else
              txt_cabecalho = txt_cabecalho & "--|" 'fone
            End If
            If reader.GetString(17) <> "" Then
              txt_cabecalho = txt_cabecalho & reader.GetString(17) & "|" 'Email
            Else
              txt_cabecalho = txt_cabecalho & "--|" 'Email
            End If
            txt_cabecalho = txt_cabecalho & reader.GetString(18) & "|" 'SAT_Assinatura_Digital

            txt_cabecalho = txt_cabecalho & reader.GetString(19) & "|" 'id_tipo_pdv

            txt_cabecalho = txt_cabecalho & reader.GetString(20) & "|" 'descr_tipo_pdv

            'txt_cabecalho = txt_cabecalho & "{"

            sw.WriteLine(txt_cabecalho)
          Loop
          Else 'nenhum reigstro localizado
            path_download = ""
          End If
        End Using
      End If
    cnn2.Close()
    Response.ContentType = "text/csv"
    Response.Charset = "UTF-8"
    Response.AppendHeader("Content-Disposition", "attachment; filename=" & CStr(id_empresa) & "_CAD_.TXT")
    Response.TransmitFile(path)
    Response.End()

  End Sub

  Public Function Pega_Itens_Estoque_Arquivo(ByVal id_empresa As String) As String
    Dim result As String = ""
    Dim cod_result As String = ""
    Dim strCnn As String = GetConnectionString("maxcont_cloud")
    Dim strSQL As String = "EXEC spWS_Estoque_Pega_Itens '" & id_empresa & "','" & asp_cnpj_emp & "','VALIDAR'"
    Response.Charset = "ISO-8859-1"
    Response.Charset = "UTF-8"

    Dim path_download As String = ""

    Dim path As String = Server.MapPath("../../Temp/Produtos_WS/" & CStr(id_empresa) & ".csv")

   Dim cnn As New SqlConnection(strCnn)
   cnn.Open()
   Dim cmd As New SqlClient.SqlCommand(strSQL, cnn)
   'cmd.Parameters.Add(New SqlParameter("@ProductID", productID))

    Dim resultado As String = ""

   Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    If dr.HasRows Then
      dr.Read()
      result = dr("result")
      cod_result = dr("cod_result")
    End If
    cnn.Close()


    Dim txt_cabecalho As String = ""

    If cod_result = "1" Then

     Dim strCnn2 As String = GetConnectionString("maxcont_cloud")
     Dim cnn2 As New SqlConnection(strCnn2)

     Dim command As SqlCommand = New SqlCommand("EXEC spWS_Estoque_Pega_Itens '" & id_empresa & "','" & asp_cnpj_emp & "','RELATORIO'", cnn2)

     cnn2.Open()
     Dim reader As SqlDataReader = command.ExecuteReader()


      txt_cabecalho = Carrega_Campo("id_empresa")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("id_item")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("cod_produto")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("descr")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("ICMS_orig")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("id_tipo")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("descr_tipo")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("id_grp")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("descr_grp")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("id_armazem")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("descr_armazem")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("EAN")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("und")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("und_com")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("classif_NCM")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("ipi")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("ICMS_ST_entrada")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("peso_liq")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("peso_bruto")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("preco_venda1")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("preco_venda2")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("preco_venda3")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("preco_venda4")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("CFOP")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("ICMS_CST")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("descr_ICMS_CST")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("ICMS_aliq")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("ipi_CST")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("descr_ipi_CST")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("pis_CST")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("descr_pis_CST")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("pPIS")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("COFINS_CST")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("descr_COFINS_CST")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("pCOFINS")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("aliq_vTotTrib")
      txt_cabecalho = txt_cabecalho & Carrega_Campo("aliq_vTotTrib_fonte_tribut")

      If File.Exists(path) Then
        File.Delete(path)
      End If

      If Not File.Exists(path) Then
        ' Create a file to write to. 
        Using sw As StreamWriter = File.CreateText(path)
          sw.WriteLine(txt_cabecalho)
          If reader.HasRows Then
            path_download = "www.maxcont.com.br/Temp/Produtos_WS/" & CStr(id_empresa) & ".txt"
            Do While reader.Read()

              txt_cabecalho = Carrega_Campo(id_empresa) 'id_empresa
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(0)) 'id_item
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(1)) 'cod_produto
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(2)) 'descr
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(3)) 'ICMS_orig
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(4)) 'id_tipo
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(5)) 'descr_tipo
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(6)) 'id_grp
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(7)) 'descr_grp
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(8)) 'id_armazem
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(9)) 'descr_armazem
              If reader.GetString(10) = "" Then
                txt_cabecalho = txt_cabecalho & Carrega_Campo("0") 'EAN
              Else
                txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(10)) 'EAN
              End If

              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(11)) 'und
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(12)) 'und_com
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(13)) 'classif_NCM
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(14)) 'ipi
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(15)) 'ICMS_ST_entrada
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(16)) 'peso_liq
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(17)) 'peso_bruto
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(18)) 'preco_venda1
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(19)) 'preco_venda2
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(20)) 'preco_venda3
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(21)) 'preco_venda4
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(22)) 'CFOP
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(23)) 'ICMS_CST
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(24)) 'descr_ICMS_CST
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(25)) 'ICMS_aliq
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(26)) 'ipi_CST
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(27)) 'descr_ipi_CST
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(28)) 'pis_CST
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(29)) 'descr_pis_CST
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(30)) 'pPIS
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(31)) 'COFINS_CST
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(32)) 'descr_COFINS_CST
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(33)) 'pCOFINS
              txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(34)) 'aliq_vTotTrib

              If reader.GetString(37) = "I" Then 'Item Inativo
                txt_cabecalho = txt_cabecalho & "INATIVO" 'aliq_vTotTrib_fonte_tribut
              ElseIf reader.GetString(37) = "S" Then 'SERVIÇO
                txt_cabecalho = txt_cabecalho & "SERVICO" 'aliq_vTotTrib_fonte_tribut
              Else
                If reader.GetString(35) <> "" Then 'Tem IBPT preenchido
                  txt_cabecalho = txt_cabecalho & Carrega_Campo(reader.GetString(35)) 'aliq_vTotTrib_fonte_tribut
                Else
                  txt_cabecalho = txt_cabecalho & Carrega_Campo("IBPT") 'aliq_vTotTrib_fonte_tribut
                End If
              End If

              sw.WriteLine(txt_cabecalho)

            Loop
          Else 'nenhum reigstro localizado
            path_download = ""
          End If
        End Using
      End If
      cnn.Close()
    End If
    Response.ContentType = "text/csv"
    Response.Charset = "UTF-8"
    Response.AppendHeader("Content-Disposition", "attachment; filename=" & id_empresa & "_ESTOQUE_.csv")
    Response.TransmitFile(path)
    Response.End()
    Return path_download
  End Function

  Private Shared Function GetConnectionString(ByVal name As String) As String

   Dim settings As ConnectionStringSettings = ConfigurationManager.ConnectionStrings(name)
   If Not settings Is Nothing Then
    Return settings.ConnectionString
   Else
    Return Nothing
   End If
  End Function

End Class
