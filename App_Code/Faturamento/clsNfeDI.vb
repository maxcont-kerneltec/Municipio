Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System

Public Class clsNfeDI

  Private _nItem, _id_di, _tpViaTransp, _tpIntermedio As Integer
  Private _nDI, _dDI, _xLocalDesemb, _UFDesemb, _dDesemb, _cExportador, _UFTerceiro, _CNPJ, _msg_erro As String
  Private _vAFRMM As Decimal

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

  Property tpViaTransp() As Integer
    Get
      Return _tpViaTransp
    End Get
    Set(value As Integer)
      _tpViaTransp = value
    End Set
  End Property

  Property tpIntermedio() As Integer
    Get
      Return _tpIntermedio
    End Get
    Set(value As Integer)
      _tpIntermedio = value
    End Set
  End Property

  Property nDI() As String
    Get
      Return _nDI
    End Get
    Set(value As String)
      _nDI = value
    End Set
  End Property

  Property dDI() As String
    Get
      Return _dDI
    End Get
    Set(value As String)
      _dDI = value
    End Set
  End Property

  Property xLocalDesemb() As String
    Get
      Return _xLocalDesemb
    End Get
    Set(value As String)
      _xLocalDesemb = value
    End Set
  End Property

  Property UFDesemb() As String
    Get
      Return _UFDesemb
    End Get
    Set(value As String)
      _UFDesemb = value
    End Set
  End Property

  Property dDesemb() As String
    Get
      Return _dDesemb
    End Get
    Set(value As String)
      _dDesemb = value
    End Set
  End Property

  Property cExportador() As String
    Get
      Return _cExportador
    End Get
    Set(value As String)
      _cExportador = value
    End Set
  End Property

  Property UFTerceiro() As String
    Get
      Return _UFTerceiro
    End Get
    Set(value As String)
      _UFTerceiro = value
    End Set
  End Property

  Property CNPJ() As String
    Get
      Return _CNPJ
    End Get
    Set(value As String)
      _CNPJ = value
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

  Property vAFRMM() As Decimal
    Get
      Return _vAFRMM
    End Get
    Set(value As Decimal)
      _vAFRMM = value
    End Set
  End Property

  Public Function ListaDiUmaNfe(ByVal id_nf As Integer, ByVal nItem As Integer) As DataView
    Dim table As New DataTable
    Dim row As DataRow
    Dim conexao As New clsConexao
    Dim dr As SqlDataReader
    Dim str_builder As New StringBuilder
    Dim i As Integer = 0

    table.Columns.Add(New DataColumn("id_nf"))
    table.Columns.Add(New DataColumn("nItem"))
    table.Columns.Add(New DataColumn("id_di"))
    table.Columns.Add(New DataColumn("nDI"))
    table.Columns.Add(New DataColumn("dDI"))
    table.Columns.Add(New DataColumn("xLocDesemb"))
    table.Columns.Add(New DataColumn("UFDesemb"))
    table.Columns.Add(New DataColumn("dDesemb"))
    table.Columns.Add(New DataColumn("cExportador"))
    table.Columns.Add(New DataColumn("tpViaTransp"))
    table.Columns.Add(New DataColumn("vAFRMM"))
    table.Columns.Add(New DataColumn("tpIntermedio"))
    table.Columns.Add(New DataColumn("CNPJ"))
    table.Columns.Add(New DataColumn("UFTerceiro"))

    str_builder.Append("SELECT id_nf, nItem, id_di, nDI, ")
    str_builder.Append("dbo.fFormata_Data(CONVERT(smalldatetime, dDI, 120), 2) as dDI, ")
    str_builder.Append("xLocDesemb, UFDesemb, ")
    str_builder.Append("dbo.fFormata_Data(CONVERT(smalldatetime, dDesemb, 120), 2) as dDesemb, cExportador, ")
    str_builder.Append("isnull(tpViaTransp, 1) as tpViaTransp, isnull(vAFRMM, 0) as vAFRMM, isnull(tpIntermedio, 0) as tpIntermedio, ")
    str_builder.Append("dbo.fCNPJ_Grava(isnull(CNPJ, 0)) as CNPJ, ISNULL(UFTerceiro, '') ")
    str_builder.Append("FROM NFE_DI ")
    str_builder.Append("WHERE (id_nf = " & id_nf & ") AND (nItem = " & nItem & ") ")
    str_builder.Append("ORDER BY id_di ")


    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        row = table.NewRow
        i = 0

        Do While i <= 13
          row(i) = dr(i)

          i = i + 1
        Loop

        table.Rows.Add(row)
      Loop

      dr.Close()
    Catch ex As Exception
      _msg_erro = "ERRO AO LISTAR A DI DO ITEM: " & ex.Message() & "----------" & ex.StackTrace()
    End Try

    Dim dv As New DataView(table)

    Return dv
  End Function

  Public Sub AddDiItem(ByVal id_nf As Integer, ByVal nItem As Integer, ByVal nDI As String, ByVal dDI As String, _
                       ByVal xLocDesemb As String, ByVal UFDesemb As String, ByVal dDesemb As String, ByVal cExportador As String)
    Dim conexao As New clsConexao
    Dim ajuste As New clsAjuste
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_NFE_DI_Grava '" & id_nf & "','" & nItem & "','" & nDI & "','" & ajuste.AMD(dDI) & "'")
    str_builder.Append(",'" & xLocDesemb & "','" & UFDesemb & "','" & ajuste.AMD(dDesemb) & "','" & cExportador & "'")

    Try
      conexao.ExecutaComando(str_builder.ToString())
    Catch ex As Exception
      _msg_erro = "ERRO AO ADICIONAR A DI AO ITEM: " & ex.Message() & "----------" & ex.StackTrace()
    End Try
  End Sub

  Public Sub ExcluiDiItem(ByVal id_nf As Integer, ByVal nItem As Integer, ByVal id_di As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_NFE_DI_Apaga '" & id_nf & "','" & nItem & "','" & id_di & "'")

    Try
      conexao.ExecutaComando(str_builder.ToString())
    Catch ex As Exception
      _msg_erro = "ERRO AO EXCLUÍR A DI DO ITEM: " & ex.Message() & "---------" & ex.StackTrace()
    End Try
  End Sub

End Class
