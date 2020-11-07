Imports System.Data
Imports System.Data.SqlClient

Public Class clsNFeAutXML
  Private _id_nf, _cnpj As Integer
  Private _tipo_pessoa As String

  Private conexao1 As New SqlConnection

  Property id_nf() As Integer
    Get
      Return _id_nf
    End Get
    Set(value As Integer)
      _id_nf = value
    End Set
  End Property

  Property cnpj() As Integer
    Get
      Return _cnpj
    End Get
    Set(value As Integer)
      _cnpj = value
    End Set
  End Property

  Property tipo_pessoa() As Integer
    Get
      Return _tipo_pessoa
    End Get
    Set(value As Integer)
      _tipo_pessoa = value
    End Set
  End Property


  Public Function Lista_autXML(ByVal id_nf As Integer) As DataView
    Dim table As New DataTable
    Dim row As DataRow
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    table.Columns.Add(New DataColumn("tipo_pessoa"))
    table.Columns.Add(New DataColumn("cnpj"))

    str_builder.Append("SELECT tipo_pessoa, cnpj ")
    str_builder.Append("FROM NFe_autXML ")
    str_builder.Append("WHERE (id_nf = " & id_nf & ")")

    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      Do While dr.Read()
        row = table.NewRow()

        row(0) = dr(0)
        row(1) = dr(1)

        table.Rows.Add(row)
      Loop
      conexao.FechaBanco(conexao1)
      dr.Close()
    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      MsgBox("ERRO AO BUSCAR OS CNPJs autorizados: " & ex.Message() & "----------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
    End Try

    Dim dv As New DataView(table)

    Return dv
  End Function
End Class
