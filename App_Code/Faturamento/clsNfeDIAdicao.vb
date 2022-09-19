Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class clsNfeDIAdicao

  Private _nItem, _id_di, _seq, _nAdicao, _nSeqAdic, _nDraw As Integer
  Private _cFabricante, _msg_erro As String
  Private _vDescDI As Decimal

  Public Sub New()

  End Sub

  Property nItem() As Integer
    Get
      Return _nItem
    End Get
    Set(value As Integer)
      _nItem = value
    End Set
  End Property

  Property id_di() As Integer
    Get
      Return _id_di
    End Get
    Set(value As Integer)
      _id_di = value
    End Set
  End Property

  Property seq() As Integer
    Get
      Return _seq
    End Get
    Set(value As Integer)
      _seq = value
    End Set
  End Property

  Property nAdicao() As Integer
    Get
      Return _nAdicao
    End Get
    Set(value As Integer)
      _nAdicao = value
    End Set
  End Property

  Property nSeqAdic() As Integer
    Get
      Return _nSeqAdic
    End Get
    Set(value As Integer)
      _nSeqAdic = value
    End Set
  End Property

  Property nDraw() As Integer
    Get
      Return _nDraw
    End Get
    Set(value As Integer)
      _nDraw = value
    End Set
  End Property

  Property cFabricante() As String
    Get
      Return _cFabricante
    End Get
    Set(value As String)
      _cFabricante = value
    End Set
  End Property

  Property vDescDI() As Decimal
    Get
      Return _vDescDI
    End Get
    Set(value As Decimal)
      _vDescDI = value
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

  Public Sub AddAdicaoDI(ByVal id_nf As Integer, ByVal nItem As Integer, ByVal id_di As Integer, ByVal nAdicao As Integer, _
                         ByVal cFabricante As String, ByVal vDescDI As Decimal)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_NFE_DI_Grava_adicao '" & id_nf & "','" & nItem & "','" & id_di & "','" & nAdicao & "'")
    str_builder.Append(",'" & cFabricante & "','" & vDescDI & "'")

    Try
      conexao.ExecutaComando(str_builder.ToString())
    Catch ex As Exception
      _msg_erro = "ERRO AO ADICIONAR A ADIÇÃO DA DI: " & ex.Message() & "-----------" & ex.StackTrace()
    End Try
  End Sub

  Public Sub ExcluiAdicaoDI(ByVal id_nf As Integer, ByVal nItem As Integer, ByVal id_di As Integer, ByVal seq As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_NFE_DI_Apaga_adicao '" & id_nf & "','" & nItem & "','" & id_di & "','" & seq & "'")

    Try
      conexao.ExecutaComando(str_builder.ToString())

    Catch ex As Exception
      _msg_erro = "ERRO AO EXCLUÍR A ADICAÇÃO DA DI: " & ex.Message() & "--------------" & ex.StackTrace()
    End Try
  End Sub

  Public Function ListaAdicaoDI(ByVal id_nf As Integer, ByVal nItem As Integer, ByVal id_di As Integer) As DataView
    Dim table As New DataTable
    Dim row As DataRow
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    table.Columns.Add(New DataColumn("id_nf"))
    table.Columns.Add(New DataColumn("nItem"))
    table.Columns.Add(New DataColumn("id_di"))
    table.Columns.Add(New DataColumn("seq"))
    table.Columns.Add(New DataColumn("nAdicao"))
    table.Columns.Add(New DataColumn("nSeqAdic"))
    table.Columns.Add(New DataColumn("cFabricante"))
    table.Columns.Add(New DataColumn("vDescDI"))
    table.Columns.Add(New DataColumn("nDraw"))

    str_builder.Append("SELECT id_nf, nItem, id_di, seq, nAdicao, nSeqAdic, cFabricante, vDescDI, ISNULL(nDraw, '') ")
    str_builder.Append("FROM NFE_DI_adicao ")
    str_builder.Append("WHERE (id_nf = " & id_nf & ") AND (nitem = " & nItem & ") AND (id_di = " & id_di & ") ")
    str_builder.Append("ORDER BY seq ")

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
        row(6) = dr(6)
        row(7) = dr(7)
        row(8) = dr(8)

        table.Rows.Add(row)
      Loop

      dr.Close()
    Catch ex As Exception
      _msg_erro = "ERRO AO LISTAR AS ADIÇÕES DA DI: " & ex.Message() & "--------" & ex.StackTrace()
    End Try

    Dim dv As New DataView(table)

    Return dv
  End Function

End Class
