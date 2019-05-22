Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class clsNfeInfoProc

  Private _seq, _indProc As Integer
  Private _nProc, _msg_erro As String

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

  Property indProc() As Integer
    Get
      Return _indProc
    End Get
    Set(value As Integer)
      _indProc = value
    End Set
  End Property

  Property nProc() As String
    Get
      Return _nProc
    End Get
    Set(value As String)
      _nProc = value
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

  Public Function ListaInfoProcNfe(ByVal id_nf As Integer) As DataView
    Dim table As New DataTable
    Dim row As DataRow
    Dim conexao As New clsConexao
    Dim dr As SqlDataReader
    Dim str_builder As New StringBuilder

    table.Columns.Add(New DataColumn("seq"))
    table.Columns.Add(New DataColumn("nProc"))
    table.Columns.Add(New DataColumn("indProc"))

    str_builder.Append("SELECT seq, nProc, indProc FROM NFE_info_proc WHERE (id_nf = " & id_nf & ")")

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        row = table.NewRow()

        row(0) = dr(0)
        row(1) = dr(1)
        row(2) = dr(2)

        table.Rows.Add(row)
      Loop

      dr.Close()
    Catch ex As Exception
      _msg_erro = "ERRO AO LISTAR OS PROCESSOS REFERENCIADOS: " & ex.Message() & "----------" & ex.StackTrace()
    End Try

    Dim dv As New DataView(table)

    Return dv
  End Function

  Public Sub AddInfoProcNfe(ByVal id_nf As Integer, ByVal nProc As String, ByVal indProc As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_NFE_Info_Proc_atualiza '" & id_nf & "','0','I','" & nProc & "','" & indProc & "'")

    Try
      conexao.ExecutaComando(str_builder.ToString())

    Catch ex As Exception
      _msg_erro = "ERRO AO ADICIONAR UM PROCESSO REFERENCIADO A NFE: " & ex.Message() & "--------------" & ex.StackTrace()
    End Try
  End Sub

  Public Sub ExcluiUmInfProcNfe(ByVal id_nf As Integer, ByVal seq As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_NFE_Info_Proc_atualiza '" & id_nf & "','" & seq & "','D'")

    Try
      conexao.ExecutaComando(str_builder.ToString())

    Catch ex As Exception
      _msg_erro = "ERRO AO EXCLUÍR O PROCESSO REFERENCIADO: " & ex.Message() & "----------" & ex.StackTrace()
    End Try
  End Sub

End Class
