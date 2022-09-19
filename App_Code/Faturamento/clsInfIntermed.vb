Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsNFeInfIntermed
  Private _CNPJ As Integer
  Private _idCadIntTran, _msg_erro As String

  Private conexao1 As New SqlConnection

  Public Sub New()

  End Sub

  Property CNPJ() As Integer
    Get
      Return _CNPJ
    End Get
    Set(value As Integer)
      _CNPJ = value
    End Set
  End Property

  Property idCadIntTran() As String
    Get
      Return _idCadIntTran
    End Get
    Set(value As String)
      _idCadIntTran = value
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


  Public Function PegaInfIntermed(ByVal id_nf As Integer) As DataView
    Dim table As New DataTable
    Dim row As DataRow
    Dim conexao As New clsConexao
    Dim dr As SqlDataReader
    Dim str_builder As New StringBuilder

    table.Columns.Add(New DataColumn("infIntermed_CNPJ"))
    table.Columns.Add(New DataColumn("infIntermed_idCadIntTran"))
    table.Columns.Add(New DataColumn("indIntermed"))
    str_builder.Append("SELECT dbo.fCNPJ_Le(infIntermed_CNPJ) as infIntermed_CNPJ, infIntermed_idCadIntTran, indIntermed ")
    str_builder.Append("FROM dbo.NFE_dados AS A ")
    str_builder.Append("WHERE (A.id_nf = " & id_nf & ") AND (A.infIntermed_CNPJ > 0)")

    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      Do While dr.Read()
        row = table.NewRow()

        row(0) = dr(0)
        row(1) = dr(1)
        row(2) = dr(2)

        table.Rows.Add(row)
      Loop
      conexao.FechaBanco(conexao1)
      dr.Close()

    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      _msg_erro = "ERRO AO PEGAR AS INFORMAÇÕES DE INTERMEDIÁRIO DA NOTA FISCAL: " & ex.Message() & "---------" & ex.StackTrace()
    End Try

    Dim dv As New DataView(table)

    Return dv
  End Function
End Class
