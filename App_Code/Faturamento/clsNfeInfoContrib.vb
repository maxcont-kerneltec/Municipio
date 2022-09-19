Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class clsNfeInfoContrib

  Private _seq As Integer
  Private _xCampo, _xTexto, _msg_erro As String

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

  Property xCampo() As String
    Get
      Return _xCampo
    End Get
    Set(value As String)
      _xCampo = value
    End Set
  End Property

  Property xTexto() As String
    Get
      Return _xTexto
    End Get
    Set(value As String)
      _xTexto = value
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

  Public Function ListaInfContribUmaNfe(ByVal id_nf As Integer) As DataView
    Dim table As New DataTable
    Dim row As DataRow
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    table.Columns.Add(New DataColumn("seq"))
    table.Columns.Add(New DataColumn("xCampo"))
    table.Columns.Add(New DataColumn("xTexto"))

    str_builder.Append("SELECT seq, xCampo, xTexto FROM NFE_info_contrib WHERE (id_nf = " & id_nf & ")")

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
      _msg_erro = "ERRO AO LISTAR AS INFORMAÇÕES DO CONTRIBUINTE: " & ex.Message() & "---------" & ex.StackTrace()
    End Try

    Dim dv As New DataView(table)

    Return dv
  End Function

  Public Sub AddInfoContribNfe(ByVal id_nf As Integer, ByVal xCampo As String, ByVal xTexto As String)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_NFe_Info_Contrib_atualiza '" & id_nf & "','0','I','" & xCampo & "','" & xTexto & "'")

    Try
      conexao.ExecutaComando(str_builder.ToString())

    Catch ex As Exception
      _msg_erro = "ERRO AO ADICIONAR AS INFORMAÇÕES DO CONTRIBUINTE: " & ex.Message() & "-----------" & ex.StackTrace()
    End Try
  End Sub

  Public Sub ExcluiUmaInfoContibNfe(ByVal id_nf As Integer, ByVal seq As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_NFe_Info_Contrib_atualiza '" & id_nf & "','" & seq & "','D'")

    Try
      conexao.ExecutaComando(str_builder.ToString())

    Catch ex As Exception
      _msg_erro = "ERRO AO EXCLUÍR A INFORMAÇÃO DO CONTRIBUINTE: " & ex.Message() & "----------" & ex.StackTrace()
    End Try

  End Sub
End Class
