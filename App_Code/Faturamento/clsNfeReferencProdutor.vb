Imports Microsoft.VisualBasic

Public Class clsNfeReferencProdutor
  Private _id_nf, _item, _cUF, _modd, _serie, _nNF, _result As Integer
  Private _AAMM, _cnpj, _UF, _msg_result As String

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

  Property result() As Integer
    Get
      Return _result
    End Get
    Set(value As Integer)
      _result = value
    End Set
  End Property

  Property item() As Integer
    Get
      Return _item
    End Get
    Set(value As Integer)
      _item = value
    End Set
  End Property

  Property cUF() As Integer
    Get
      Return _cUF
    End Get
    Set(value As Integer)
      _cUF = value
    End Set
  End Property

  Property modd() As Integer
    Get
      Return _modd
    End Get
    Set(value As Integer)
      _modd = value
    End Set
  End Property

  Property serie() As Integer
    Get
      Return _serie
    End Get
    Set(value As Integer)
      _serie = value
    End Set
  End Property

  Property nNF() As Integer
    Get
      Return _nNF
    End Get
    Set(value As Integer)
      _nNF = value
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

  Property AAMM() As String
    Get
      Return _AAMM
    End Get
    Set(value As String)
      _AAMM = value
    End Set
  End Property

  Property cnpj() As String
    Get
      Return _cnpj
    End Get
    Set(value As String)
      _cnpj = value
    End Set
  End Property

  Property UF() As String
    Get
      Return _UF
    End Get
    Set(value As String)
      _UF = value
    End Set
  End Property


  Public Sub AtualizaNfeReferenciaProdutor(ByVal id_nf As Integer, ByVal item As Integer, ByVal IE As Integer, _
                                           ByVal cUF As String, ByVal AAMM As String, ByVal cnpj As String, _
                                           ByVal modd As String, ByVal serie As Integer, ByVal nNF As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim acao As String = ""

    If item = 0 Then
      acao = "I"
    End If

    str_builder.Append("EXEC sp9_NFE_Ref_Pro_atualiza '" & id_nf & "','" & item & "','" & acao & "','" & IE & "'")
    str_builder.Append(",'" & cUF & "','" & AAMM & "','" & cnpj & "','" & modd & "','" & serie & "','" & nNF & "'")

    Try
      conexao.ExecutaComando(str_builder.ToString())

      If item = 0 Then
        Me.result = 0
        Me.msg_result = "Chave de acesso referenciada com sucesso!"
      Else
        Me.result = 1
        Me.msg_result = "Chave de acesso atualizada com sucesso!"
      End If

    Catch ex As Exception
      MsgBox("ERRO AO ATUALIZAR A REFERÊNCIA: " & ex.Message() & "----------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
    End Try
  End Sub

  Public Sub ExcluiNfeReferenciaProdutor(ByVal id_nf As Integer, ByVal item As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_NFE_Ref_Pro_atualiza '" & id_nf & "','" & item & "','D'")


    Try
      conexao.ExecutaComando(str_builder.ToString())

      Me.result = 0
      Me.msg_result = "Chave de acesso excluída com sucesso!"
    Catch ex As Exception
      MsgBox("ERRO AO EXCLUÍR A REFERÊNCIA: " & ex.Message() & "----------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
    End Try
  End Sub

End Class
