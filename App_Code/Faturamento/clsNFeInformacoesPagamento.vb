Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsNFeInformacoesPagamento
  Private _id_detPag, _tPag As Integer
  Private _vPag, _vTroco As Decimal
  Private _msg_erro As String

  Public Sub New()

  End Sub

  Property id_detPag() As Integer
    Get
      Return _id_detPag
    End Get
    Set(value As Integer)
      _id_detPag = value
    End Set
  End Property

  Property tPag() As Integer
    Get
      Return _tPag
    End Get
    Set(value As Integer)
      _tPag = value
    End Set
  End Property

  Property vPag() As Decimal
    Get
      Return _vPag
    End Get
    Set(value As Decimal)
      _vPag = value
    End Set
  End Property

  Property vTroco() As Decimal
    Get
      Return _vTroco
    End Get
    Set(value As Decimal)
      _vTroco = value
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

  Public Function PegaInfPagamentoUmaNFe(ByVal id_nf As Integer) As DataView
    Dim table As New DataTable
    Dim row As DataRow
    Dim conexao As New clsConexao
    Dim dr As SqlDataReader
    Dim str_builder As New StringBuilder

    table.Columns.Add(New DataColumn("id_detPag"))
    table.Columns.Add(New DataColumn("tPag"))
    table.Columns.Add(New DataColumn("vPag"))
    table.Columns.Add(New DataColumn("vTroco"))
    table.Columns.Add(New DataColumn("descr_tPag"))
    table.Columns.Add(New DataColumn("id_detPag_grp_cartao"))
    table.Columns.Add(New DataColumn("tpIntegra"))
    table.Columns.Add(New DataColumn("cnpj"))
    table.Columns.Add(New DataColumn("tBand"))
    table.Columns.Add(New DataColumn("cAut"))

    str_builder.Append("SELECT A.id_detPag, A.tPag, vPag, vTroco, B.descr_tPag, ISNULL(C.id_detPag, 0) AS id_detPag_grp_cartao, ")
    str_builder.Append("C.tpIntegra, dbo.fFormata_Grava(C.cnpj) AS cnpj, C.tBand, C.cAut ")
    str_builder.Append("FROM NFE_Informacoes_Pagamento AS A ")
    str_builder.Append("INNER JOIN NFe_Forma_Pagamento AS B ON (B.tPag = A.tPag) ")
    str_builder.Append("LEFT OUTER JOIN NFe_Grupo_Cartoes AS C ON (C.id_nf = A.id_nf) AND (C.id_detPag = A.id_detPag) ")
    str_builder.Append("WHERE (A.id_nf = " & id_nf & ")")

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
        row(9) = dr(9)

        table.Rows.Add(row)
      Loop

      dr.Close()

    Catch ex As Exception
      _msg_erro = "ERRO AO PEGAR AS INFORMAÇÕES DE PAGAMENTO DA NOTA FISCAL: " & ex.Message() & "---------" & ex.StackTrace()
    End Try

    Dim dv As New DataView(table)

    Return dv
  End Function
End Class
