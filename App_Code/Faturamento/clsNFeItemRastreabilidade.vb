Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsNFeItemRastreabilidade
  Private _seq As Integer
  Private _nLote, _qLote, _dFab, _dVal, _cAgreg, _msg_erro As String

  Public Sub New()

  End Sub

  Property seq() As Integer
    Get
      Return _seq
    End Get
    Set(value As Integer)
      _seq = value
    End Set
  End Property

  Property nLote() As String
    Get
      Return _nLote
    End Get
    Set(value As String)
      _nLote = value
    End Set
  End Property

  Property qLote() As String
    Get
      Return _qLote
    End Get
    Set(value As String)
      _qLote = value
    End Set
  End Property

  Property dFab() As String
    Get
      Return _dFab
    End Get
    Set(value As String)
      _dFab = value
    End Set
  End Property

  Property dVal() As String
    Get
      Return _dVal
    End Get
    Set(value As String)
      _dVal = value
    End Set
  End Property

  Property cAgreg() As String
    Get
      Return _cAgreg
    End Get
    Set(value As String)
      _cAgreg = value
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

  Public Function ListaRastreabilidade(ByVal id_nf As Integer, ByVal nItem As Integer) As DataView
    Dim table As New DataTable
    Dim row As DataRow
    Dim conexao As New clsConexao
    Dim dr As SqlDataReader
    Dim str_builder As New StringBuilder

    table.Columns.Add(New DataColumn("seq"))
    table.Columns.Add(New DataColumn("nLote"))
    table.Columns.Add(New DataColumn("qLote"))
    table.Columns.Add(New DataColumn("dFab"))
    table.Columns.Add(New DataColumn("dVal"))
    table.Columns.Add(New DataColumn("cAgreg"))

    str_builder.Append("SELECT seq, nLote, qLote, dFab, dVal, cAgreg ")
    str_builder.Append("FROM NFe_Item_Rastreabilidade ")
    str_builder.Append("WHERE (id_nf = " & id_nf & ") AND (nItem = " & nItem & ")  AND (isnull(nLote, '') <> '')")

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        row = table.NewRow()

        row(0) = dr(0)
        row(1) = dr(1)
        row(2) = dr(2)
        row(3) = dr(3)
        row(4) = dr(4)
        row(5) = dr(5)

        table.Rows.Add(row)
      Loop

      dr.Close()
    Catch ex As Exception
      _msg_erro = "ERRO AO LISTAR A RASTREABILIDADE DO ITEM: " & ex.Message() & "--------" & ex.StackTrace()
    End Try

    Dim dv As New DataView(table)

    Return dv
  End Function

  ''' <summary>
  ''' Informar (seq = 0), para casos de inclusão...
  ''' </summary>
  Public Sub SalvaUmaRastreabilidade(ByVal id_nf As Integer, ByVal nItem As Integer, ByVal seq As Integer, ByVal nLote As String, _
                                     ByVal qLote As String, ByVal dFab As String, ByVal dVal As String, ByVal cAgreg As String)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim ajuste As New clsAjuste

    str_builder.Append("EXEC sp9_Salva_Item_Rastreabilidade '" & id_nf & "','" & nItem & "','" & seq & "','" & nLote & "'")
    str_builder.Append(",'" & qLote & "','" & ajuste.AMD(dFab) & "','" & ajuste.AMD(dVal) & "','" & cAgreg & "'")

    Try
      conexao.ExecutaComando(str_builder.ToString())

    Catch ex As Exception
      _msg_erro = "ERRO AO SALVAR AS INFORMAÇÕES DA RASTREABILIDADE DO ITEM: " & ex.Message() & "-----" & ex.StackTrace()
    End Try
  End Sub

  Public Sub ExcluiUmaRastreabilidade(ByVal id_nf As Integer, ByVal nItem As Integer, ByVal seq As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_Salva_Item_Rastreabilidade '" & id_nf & "','" & nItem & "','" & seq & "',''")
    str_builder.Append(",'','','','','DEL'")

    Try
      conexao.ExecutaComando(str_builder.ToString())

    Catch ex As Exception
      _msg_erro = "ERRO AO EXCLUIR A RASTREABILIDADE DO ITEM: " & ex.Message() & "-----" & ex.StackTrace()
    End Try

  End Sub


End Class
