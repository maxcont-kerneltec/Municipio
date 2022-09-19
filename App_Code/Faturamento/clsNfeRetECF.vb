Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsNfeRetECF

  Private _id_nf, _id_ret_ecf, _id_mod, _nECF, _nCOO, _result As Integer
  Private _msg_result As String

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

  Property id_ret_ecf() As Integer
    Get
      Return _id_ret_ecf
    End Get
    Set(value As Integer)
      _id_ret_ecf = value
    End Set
  End Property

  Property id_mod() As Integer
    Get
      Return _id_mod
    End Get
    Set(value As Integer)
      _id_mod = value
    End Set
  End Property

  Property nECF() As Integer
    Get
      Return _nECF
    End Get
    Set(value As Integer)
      _nECF = value
    End Set
  End Property

  Property nCOO() As Integer
    Get
      Return _nCOO
    End Get
    Set(value As Integer)
      _nCOO = value
    End Set
  End Property

  Property result() As Integer
    Get
      Return _result
    End Get
    Set(value As Integer)
      _result = value
    End Set
  End Property

  Property msg_result() As String
    Get
      Return _msg_result
    End Get
    Set(value As String)
      _msg_result = value
    End Set
  End Property

  Public Function ListaEcfUmaNota(ByVal id_nf As Integer) As DataView
    Dim table As New DataTable
    Dim row As DataRow
    Dim conexao As New clsConexao
    Dim str_builer As New StringBuilder
    Dim dr As SqlDataReader

    table.Columns.Add(New DataColumn("id_ret_ecf"))
    table.Columns.Add(New DataColumn("id_mod"))
    table.Columns.Add(New DataColumn("nECF"))
    table.Columns.Add(New DataColumn("nCOO"))
    table.Columns.Add(New DataColumn("mod_descr"))

    str_builer.Append("SELECT id_ret_ecf, A.id_mod, nECF, nCOO, B.mod ")
    str_builer.Append("FROM NFE_RetECF AS A ")
    str_builer.Append("INNER JOIN NFE_modECF AS B ON (B.id_mod = A.id_mod) ")
    str_builer.Append("WHERE (id_nf = " & id_nf & ") ")


    Try
      dr = conexao.RetornaDataReader(str_builer.ToString())

      Do While dr.Read()
        row = table.NewRow()

        row(0) = dr(0)
        row(1) = dr(1)
        row(2) = dr(2)
        row(3) = dr(3)
        row(4) = dr(4)

        table.Rows.Add(row)
      Loop

      dr.Close()
    Catch ex As Exception
      MsgBox("ERRO AO LISTAR O ECF: " & ex.Message() & "------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
    End Try

    Dim dv As New DataView(table)

    Return dv
  End Function

  ''' <summary>
  ''' informe o id_ref_ecf = 0, para adicionar um novo lançamento...
  ''' retorno: result(0=Ok) | msg_retorno
  ''' </summary>
  ''' <param name="id_empresa"></param>
  ''' <param name="id_nf"></param>
  ''' <param name="id_ret_ecf"></param>
  ''' <param name="nECF"></param>
  ''' <param name="nCOO"></param>
  ''' <param name="id_mod"></param>
  Public Sub AlteraEcfUmaNota(ByVal id_empresa As Integer, ByVal id_nf As Integer, ByVal id_ret_ecf As Integer, _
                              ByVal nECF As Integer, ByVal nCOO As Integer, ByVal id_mod As Integer)

    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim acao As String

    If id_ret_ecf = 0 Then
      acao = "INCLUIR"
    Else
      acao = "ALTERAR"
    End If

    str_builder.Append("EXEC sp9_Altera_ECF '" & id_empresa & "','" & id_nf & "','" & id_ret_ecf & "'")
    str_builder.Append(",'" & nECF & "','" & nCOO & "','" & id_mod & "','" & acao & "'")


    Try
      conexao.ExecutaComando(str_builder.ToString())

      Me.result = 0

      If id_ret_ecf = 0 Then
        Me.msg_result = "ECF adicionado com sucesso"
      Else
        Me.msg_result = "ECF atualizado com sucesso"
      End If

    Catch ex As Exception
      MsgBox("FALHA AO SALVAR UM ECF: " & ex.Message() & "-----------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
    End Try

  End Sub

  ''' <summary>
  ''' retorno: result(0=Ok) | msg_retorno
  ''' </summary>
  ''' <param name="id_empresa"></param>
  ''' <param name="id_nf"></param>
  ''' <param name="id_ret_ecf"></param>
  Public Sub ExcluiEcfUmaNota(ByVal id_empresa As Integer, ByVal id_nf As Integer, ByVal id_ret_ecf As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_Altera_ECF '" & id_empresa & "','" & id_nf & "','" & id_ret_ecf & "'")
    str_builder.Append(",'0','0','0','EXCLUIR'")


    Try
      conexao.ExecutaComando(str_builder.ToString())

      Me.result = 0
      Me.msg_result = "ECF excluído com sucesso"
    Catch ex As Exception
      MsgBox("FALHA AO EXCLUÍR UM ECF: " & ex.Message() & "-----------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
    End Try
  End Sub

End Class
