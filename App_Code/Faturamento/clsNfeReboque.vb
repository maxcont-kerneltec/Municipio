Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class clsNfeReboque

  Private _id_nf, _seq As Integer
  Private _reboque_placa, _reboque_uf, _reboque_RNTC, _reboque_vagao, _reboque_balsa, _msg_erro As String

  Public Sub New()

  End Sub

  Property id_nf() As Integer
    Get
      Return _id_nf
    End Get
    Set(value As Integer)
      _id_nf = value
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

  Property reboque_placa() As String
    Get
      Return _reboque_placa
    End Get
    Set(value As String)
      _reboque_placa = value
    End Set
  End Property

  Property reboque_uf() As String
    Get
      Return _reboque_uf
    End Get
    Set(value As String)
      _reboque_uf = value
    End Set
  End Property

  Property reboque_RNTC() As String
    Get
      Return _reboque_RNTC
    End Get
    Set(value As String)
      _reboque_RNTC = value
    End Set
  End Property

  Property reboque_vagao() As String
    Get
      Return _reboque_vagao
    End Get
    Set(value As String)
      _reboque_vagao = value
    End Set
  End Property

  Property reboque_balsa() As String
    Get
      Return _reboque_balsa
    End Get
    Set(value As String)
      _reboque_balsa = value
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

  Public Function ListaReboqueNfe(ByVal id_nf As Integer) As DataView
    Dim table As New DataTable
    Dim row As DataRow
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    table.Columns.Add(New DataColumn("seq"))
    table.Columns.Add(New DataColumn("reboque_placa"))
    table.Columns.Add(New DataColumn("reboque_UF"))
    table.Columns.Add(New DataColumn("reboque_RNTC"))
    table.Columns.Add(New DataColumn("reboque_vagao"))
    table.Columns.Add(New DataColumn("reboque_balsa"))

    str_builder.Append("SELECT seq, reboque_placa, reboque_UF, reboque_RNTC, ISNULL((reboque_vagao), '') AS reboque_vagao, ")
    str_builder.Append("ISNULL((reboque_balsa), '') AS reboque_balsa ")
    str_builder.Append("FROM NFE_reboque ")
    str_builder.Append("WHERE (id_nf = " & id_nf & ") ")


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
      _msg_erro = "ERRO AO LISTAR OS REBOQUES DA NFE: " & ex.Message() & "---------------" & ex.StackTrace()
    End Try

    Dim dv As New DataView(table)

    Return dv
  End Function

  Public Sub AddReboqueNfe(ByVal id_nf As Integer, ByVal reboque_placa As String, ByVal reboque_RNTC As String, _
                           ByVal reboque_UF As String)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_NFe_Reboque_atualiza '" & id_nf & "','0','I','" & reboque_placa & "'")
    str_builder.Append(",'" & reboque_RNTC & "','" & reboque_UF & "'")

    Try
      conexao.ExecutaComando(str_builder.ToString())

    Catch ex As Exception
      _msg_erro = "ERRO AO ADICIONAR O REBOQUE A NOTA FISCAL: " & ex.Message() & "---------" & ex.StackTrace()
    End Try

  End Sub

  Public Sub ExcluiUmReboqueNfe(ByVal id_nf As Integer, ByVal seq As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_NFe_Reboque_atualiza '" & id_nf & "','" & seq & "','D'")

    Try
      conexao.ExecutaComando(str_builder.ToString())

    Catch ex As Exception
      _msg_erro = "ERRO AO EXCLUÍR UM REBOQUE: " & ex.Message() & "-------------" & ex.StackTrace()
    End Try
  End Sub
End Class
