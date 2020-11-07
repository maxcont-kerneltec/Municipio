Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsNFeItemGrupoExportacao
  Private _detExport As Integer
  Private _nDraw, _nRE, _chNFe, _msg_erro As String
  Private _qExport As Decimal
  Private conexao1 As New SqlConnection

  Public Sub New()

  End Sub

  Property detExport() As Integer
    Get
      Return _detExport
    End Get
    Set(value As Integer)
      _detExport = value
    End Set
  End Property

  Property qExport() As Decimal
    Get
      Return _qExport
    End Get
    Set(value As Decimal)
      _qExport = value
    End Set
  End Property

  Property nDraw() As String
    Get
      Return _nDraw
    End Get
    Set(value As String)
      _nDraw = value
    End Set
  End Property

  Property nRE() As String
    Get
      Return _nRE
    End Get
    Set(value As String)
      _nRE = value
    End Set
  End Property

  Property chNFe() As String
    Get
      Return _chNFe
    End Get
    Set(value As String)
      _chNFe = value
    End Set
  End Property

  Property msg_erro() As String
    Get
      Return _msg_erro
    End Get
    Set(value As String)
      _msg_erro = value
    End Set
  End Property

  Public Function ListaUmGrupoExport(ByVal id_nf As Integer, ByVal nItem As Integer) As DataView
    Dim conexao As New clsConexao
    Dim dr As SqlDataReader
    Dim str_builder As New StringBuilder
    Dim table As New DataTable
    Dim row As DataRow

    table.Columns.Add(New DataColumn("detExport"))
    table.Columns.Add(New DataColumn("nDraw"))
    table.Columns.Add(New DataColumn("nRE"))
    table.Columns.Add(New DataColumn("chNFe"))
    table.Columns.Add(New DataColumn("qExport"))

    str_builder.Append("SELECT detExport, ISNULL(nDraw, ''), ISNULL(nRE, ''), ISNULL(chNFe, ''), qExport ")
    str_builder.Append("FROM NFe_Item_Grupo_Exportacao ")
    str_builder.Append("WHERE (id_nf = " & id_nf & ") AND (nItem = " & nItem & ") ")

    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      Do While dr.Read()
        row = table.NewRow()

        row(0) = dr(0)
        row(1) = dr(1)
        row(2) = dr(2)
        row(3) = dr(3)
        row(4) = dr(4)

        table.Rows.Add(row)
      Loop
      conexao.FechaBanco(conexao1)
      dr.Close()
    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      _msg_erro = "ERRO AO LISTAR O GRUPO EXPORTAÇÃO PARA O ITEM: " & ex.Message() & "-------" & ex.StackTrace()
    End Try

    Dim dv As New DataView(table)

    Return dv
  End Function

  ''' <summary>
  ''' Para inserir um novo Grupo, informar o (detExport = 0)...
  ''' </summary>
  Public Sub SalvaUmGrupoExportacao(ByVal id_nf As Integer, ByVal nItem As Integer, ByVal detExport As Integer, ByVal nDraw As String, ByVal nRE As String, ByVal chNFe As String, ByVal qExport As Decimal)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_NFe_Item_Grupo_Exportacao_Altera '" & id_nf & "','" & nItem & "','" & detExport & "'")
    str_builder.Append(",'" & nDraw & "','" & nRE & "','" & chNFe & "','" & qExport & "'")

    Try
      conexao.ExecutaComando(str_builder.ToString())

    Catch ex As Exception
      _msg_erro = "ERRO AO SALVAR AS INFORMAÇÕES DO GRUPO EXPORTAÇÃO: " & ex.Message() & "-------" & ex.StackTrace()
    End Try
  End Sub

  Public Sub ExcluiUmGrupoExportacao(ByVal id_nf As Integer, ByVal nItem As Integer, ByVal detExport As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_NFe_Item_Grupo_Exportacao_Altera '" & id_nf & "','" & nItem & "','" & detExport & "'")
    str_builder.Append(",'','','','','DEL'")

    Try
      conexao.ExecutaComando(str_builder.ToString())

    Catch ex As Exception
      _msg_erro = "ERRO AO EXCLUIR AS INFORMAÇÕES DO GRUPO EXPORTAÇÃO: " & ex.Message() & "-------" & ex.StackTrace()
    End Try
  End Sub
End Class
