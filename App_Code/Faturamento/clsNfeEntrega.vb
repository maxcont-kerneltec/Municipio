Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System

Public Class clsNfeEntrega

  Private _id_nf, _tipo_reg, _cMun, _id_end_entrega As Integer
  Private _tipo_pessoa, _cnpj, _xLgr, _nro, _xCpl, _xBairro, _xMun, _UF, _CEP As String

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

  Property tipo_reg() As Integer
    Get
      Return _tipo_reg
    End Get
    Set(value As Integer)
      _tipo_reg = value
    End Set
  End Property

  Property cMun() As Integer
    Get
      Return _cMun
    End Get
    Set(value As Integer)
      _cMun = value
    End Set
  End Property

  Property id_end_entrega() As Integer
    Get
      Return _id_end_entrega
    End Get
    Set(value As Integer)
      _id_end_entrega = value
    End Set
  End Property

  Property tipo_pessoa() As String
    Get
      Return _tipo_pessoa
    End Get
    Set(value As String)
      _tipo_pessoa = value
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

  Property xLgr() As String
    Get
      Return _xLgr
    End Get
    Set(value As String)
      _xLgr = value
    End Set
  End Property

  Property nro() As String
    Get
      Return _nro
    End Get
    Set(value As String)
      _nro = value
    End Set
  End Property

  Property xCpl() As String
    Get
      Return _xCpl
    End Get
    Set(value As String)
      _xCpl = value
    End Set
  End Property

  Property xBairro() As String
    Get
      Return _xBairro
    End Get
    Set(value As String)
      _xBairro = value
    End Set
  End Property

  Property xMun() As String
    Get
      Return _xMun
    End Get
    Set(value As String)
      _xMun = value
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

  Property CEP() As String
    Get
      Return _CEP
    End Get
    Set(value As String)
      _CEP = value
    End Set
  End Property

  Public Sub ListaEndRetUmaNfe(ByVal id_nf As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    str_builder.Append("SELECT tipo_pessoa, cnpj, dbo.fLC(xLgr), nro, dbo.fLC(xCpl), dbo.fLC(xBairro), cMun, dbo.fLC(xMun), UF, ")
    str_builder.Append("dbo.fCNPJ_Tipo_Le(cnpj, tipo_pessoa) as mRET_cnpj_fmt, CEP ")
    str_builder.Append("FROM NFE_entrega ")
    str_builder.Append("WHERE (id_nf = " & id_nf & ") and (tipo_reg = 1)")

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        Me.tipo_pessoa = dr(0)
        'Me.cnpj = dr(1)
        Me.xLgr = dr(2)
        Me.nro = dr(3)
        Me.xCpl = dr(4)
        Me.xBairro = dr(5)
        Me.cMun = dr(6)
        Me.xMun = dr(7)
        Me.UF = dr(8)
        Me.cnpj = dr(9)
        Me.CEP = dr(10)
      Loop

      dr.Close()
    Catch ex As Exception
      MsgBox("ERRO AO LISTAR O ENDEREÇO DE RETIRADA: " & ex.Message() & "----------------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
    End Try

  End Sub

  Public Sub ListaEndEntUmaNfe(ByVal id_nf As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    str_builder.Append("SELECT tipo_pessoa, cnpj, dbo.fLC(xLgr), nro, dbo.fLC(xCpl), dbo.fLC(xBairro), cMun, dbo.fLC(xMun), UF, ")
    str_builder.Append("dbo.fCNPJ_Tipo_Le(cnpj, tipo_pessoa) as mRET_cnpj_fmt, CEP ")
    str_builder.Append("FROM NFE_entrega ")
    str_builder.Append("WHERE (id_nf = " & id_nf & ") and (tipo_reg = 0)")

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        Me.tipo_pessoa = dr(0)
        'Me.cnpj = dr(1)
        Me.xLgr = dr(2)
        Me.nro = dr(3)
        Me.xCpl = dr(4)
        Me.xBairro = dr(5)
        Me.cMun = dr(6)
        Me.xMun = dr(7)
        Me.UF = dr(8)
        Me.cnpj = dr(9)
        Me.CEP = dr(10)
      Loop

      dr.Close()
    Catch ex As Exception
      MsgBox("ERRO AO LISTAR O ENDEREÇO DE ENTREGA DIFERENTE: " & ex.Message() & "----------------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
    End Try
  End Sub

  ''' <summary>
  ''' Verifica se existe um endereço de retirada para a nota fiscal...
  ''' </summary>
  ''' <param name="id_nf"></param>
  ''' <returns></returns>
  Public Function VerifExisteEndRet(ByVal id_nf As Integer) As Boolean
    Dim conexao As New clsConexao
    Dim dr As SqlDataReader
    Dim str_builder As New StringBuilder
    Dim retorno As Boolean = False

    str_builder.Append("SELECT * FROM NFE_entrega WHERE (id_nf = " & id_nf & ") and (tipo_reg = 1)")

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      If dr.HasRows Then
        retorno = True
      End If

      dr.Close()
    Catch ex As Exception
      MsgBox("ERRO AO VERIFICAR O ENDEREÇO DE RETIRADA: " & ex.Message() & "----------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
    End Try

    Return retorno
  End Function

  ''' <summary>
  ''' Verifica se existe um endereço de entrega diferente para a nota fisca....
  ''' </summary>
  ''' <param name="id_nf"></param>
  ''' <returns></returns>
  Public Function VerifExisteEndEnt(ByVal id_nf As Integer) As Boolean
    Dim conexao As New clsConexao
    Dim dr As SqlDataReader
    Dim str_builder As New StringBuilder
    Dim retorno As Boolean = False

    str_builder.Append("SELECT * FROM NFE_entrega WHERE (id_nf = " & id_nf & ") and (tipo_reg = 0)")

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      If dr.HasRows Then
        retorno = True
      End If

      dr.Close()
    Catch ex As Exception
      MsgBox("ERRO AO VERIFICAR O ENDEREÇO DE ENTREGA: " & ex.Message() & "--------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
    End Try

    Return retorno
  End Function

  ''' <summary>
  ''' Salva um endereço de retirada...
  ''' </summary>
  ''' <param name="id_nf"></param>
  ''' <param name="tipo_pessoa"></param>
  ''' <param name="cnpj"></param>
  ''' <param name="xLgr"></param>
  ''' <param name="nro"></param>
  ''' <param name="xCpl"></param>
  ''' <param name="xBairro"></param>
  ''' <param name="cMun"></param>
  Public Sub SalvaUmEndRet(ByVal id_nf As Integer, ByVal tipo_pessoa As String, ByVal cnpj As String, ByVal xLgr As String, _
                           ByVal nro As String, ByVal xCpl As String, ByVal xBairro As String, ByVal cMun As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_NFE_local_Atualiza '" & id_nf & "','1','" & tipo_pessoa & "','" & cnpj & "','" & xLgr & "'")
    str_builder.Append(",'" & nro & "','" & xCpl & "','" & xBairro & "','" & cMun & "',''")

    Try
      conexao.ExecutaComando(str_builder.ToString())

    Catch ex As Exception
      MsgBox("ERRO AO SALVAR O ENDEREÇO DE RETIRADA: " & ex.Message() & "-----------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
    End Try

  End Sub

  ''' <summary>
  ''' Salva um endereço de entrega
  ''' </summary>
  ''' <param name="id_nf"></param>
  ''' <param name="tipo_pessoa"></param>
  ''' <param name="cnpj"></param>
  ''' <param name="xLgr"></param>
  ''' <param name="nro"></param>
  ''' <param name="xCpl"></param>
  ''' <param name="xBairro"></param>
  ''' <param name="cMun"></param>
  ''' <param name="id_end_ent"></param>
  Public Sub SalvaUmEndEnt(ByVal id_nf As Integer, ByVal tipo_pessoa As String, ByVal cnpj As String, ByVal xLgr As String, _
                           ByVal nro As String, ByVal xCpl As String, ByVal xBairro As String, ByVal cMun As Integer, _
                           ByVal id_end_ent As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_NFE_local_Atualiza '" & id_nf & "','0','" & tipo_pessoa & "','" & cnpj & "','" & xLgr & "'")
    str_builder.Append(",'" & nro & "','" & xCpl & "','" & xBairro & "','" & cMun & "','','" & id_end_ent & "'")

    Try
      conexao.ExecutaComando(str_builder.ToString())

    Catch ex As Exception
      MsgBox("ERRO AO SALVAR O ENDEREÇO DE ENTREGA: " & ex.Message() & "-----------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
    End Try
  End Sub

  Public Sub ExcluiUmEndRet(ByVal id_nf As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_NFE_local_Atualiza '" & id_nf & "','1','','',''")
    str_builder.Append(",'','','','0','D','0'")

    Try
      conexao.ExecutaComando(str_builder.ToString())

    Catch ex As Exception
      MsgBox("ERRO AO EXCLUIR O ENDEREÇO DE RETIRADA: " & ex.Message() & "-----------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
    End Try
  End Sub

  Public Sub ExcluiUmEndEnt(ByVal id_nf As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_NFE_local_Atualiza '" & id_nf & "','0','','',''")
    str_builder.Append(",'','','','0','D','0'")

    Try
      conexao.ExecutaComando(str_builder.ToString())

    Catch ex As Exception
      MsgBox("ERRO AO EXCLUIR O ENDEREÇO DE ENTREGA: " & ex.Message() & "-----------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
    End Try
  End Sub
End Class
