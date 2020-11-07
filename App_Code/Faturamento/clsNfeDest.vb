Imports Microsoft.VisualBasic
Imports System.Data.SqlClient

Public Class clsNfeDest

  Private _id_dest, _id_dest2, _cMun, _cPais, _id_cliente, _id_fornec, _indIEDest As Integer
  Private _tipo_pessoa, _cnpj, _xNome, _xLgr, _nro, _xCpl, _xBairro, _xMun, _UF, _CEP, _xPais, _fone, _IE, _IM As String
  Private _ISUF, _email, _IM_Eventual As String

  Private conexao1 As New SqlConnection

  Public Sub New()

  End Sub

  Property id_dest() As Integer
    Get
      Return _id_dest
    End Get
    Set(value As Integer)
      _id_dest = value
    End Set
  End Property

  Property id_dest2() As Integer
    Get
      Return _id_dest2
    End Get
    Set(value As Integer)
      _id_dest2 = value
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

  Property cPais() As Integer
    Get
      Return _cPais
    End Get
    Set(value As Integer)
      _cPais = value
    End Set
  End Property

  Property id_cliente() As Integer
    Get
      Return _id_cliente
    End Get
    Set(value As Integer)
      _id_cliente = value
    End Set
  End Property

  Property id_fornec() As Integer
    Get
      Return _id_fornec
    End Get
    Set(value As Integer)
      _id_fornec = value
    End Set
  End Property

  Property indIEDest() As Integer
    Get
      Return _indIEDest
    End Get
    Set(value As Integer)
      _indIEDest = value
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

  Property xNome() As String
    Get
      Return _xNome
    End Get
    Set(value As String)
      _xNome = value
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

  Property xPais() As String
    Get
      Return _xPais
    End Get
    Set(value As String)
      _xPais = value
    End Set
  End Property

  Property fone() As String
    Get
      Return _fone
    End Get
    Set(value As String)
      _fone = value
    End Set
  End Property

  Property IE() As String
    Get
      Return _IE
    End Get
    Set(value As String)
      _IE = value
    End Set
  End Property

  Property IM() As String
    Get
      Return _IM
    End Get
    Set(value As String)
      _IM = value
    End Set
  End Property

  Property ISUF() As String
    Get
      Return _ISUF
    End Get
    Set(value As String)
      _ISUF = value
    End Set
  End Property

  Property email() As String
    Get
      Return _email
    End Get
    Set(value As String)
      _email = value
    End Set
  End Property

  Property IM_Eventual() As String
    Get
      Return _IM_Eventual
    End Get
    Set(value As String)
      _IM_Eventual = value
    End Set
  End Property


  Public Sub PegaUmDestinatarioNfe(ByVal id_dest As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    str_builder.Append("SELECT cnpj, dbo.fLC(xNome) as xNome, dbo.fLC(xLgr) as xLgr, dbo.fLC(nro) as nro, dbo.fLC(xCpl), dbo.fLC(xBairro), cMun, dbo.fLC(xMun) as xMun,  ")
    str_builder.Append("UF, CEP, cPais, dbo.fLC(xPais) as xPais, isnull(fone, '') as fone, ISNULL((IE), ''), ISNULL((ISUF), ''), ISNULL((email), ''), tipo_pessoa, ")
    str_builder.Append("CASE WHEN tipo_pessoa = 'J' THEN dbo.fCNPJ_Le(RIGHT('00000000000000' + CAST(cnpj as varchar(14)), 14)) ELSE dbo.fCNPJ_Le(cnpj) END AS cnpjFormat, ")
    str_builder.Append("indIEDest, ISNULL((IM), ''), ISNULL((id_cliente),0) AS id_cliente ")
    str_builder.Append("FROM NFE_dest ")
    str_builder.Append("WHERE (id_dest = " & id_dest & ") ")

    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      Do While dr.Read()
        'Me.cnpj = dr(0)
        Me.xNome = dr(1)
        Me.xLgr = dr(2)
        Me.nro = dr(3)
        Me.xCpl = dr(4)
        Me.xBairro = dr(5)
        Me.cMun = dr(6)
        Me.xMun = dr(7)
        Me.UF = dr(8)
        Me.CEP = dr(9)
        Me.cPais = dr(10)
        Me.xPais = dr(11)
        Me.fone = dr(12)
        Me.IE = dr(13)
        Me.ISUF = dr(14)
        Me.email = dr(15)
        Me.tipo_pessoa = dr(16)
        Me.cnpj = dr(17)
        Me.indIEDest = dr(18)
        Me.IM = dr(19)
        Me.id_cliente = dr(20)
      Loop
      conexao.FechaBanco(conexao1)
      dr.Close()
    Catch ex As System.Exception
      conexao.FechaBanco(conexao1)
      MsgBox("ERRO AO BUSCAR O DESTINATÁRIO DA NF: " & ex.Message() & "--------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
    End Try

  End Sub

  Public Sub SalvaUmDestinatarioNfe(ByVal id_nf As Integer, ByVal tipo_pessoa As String, ByVal cnpj As String, ByVal xNome As String, _
                                    ByVal xLgr As String, ByVal nro As String, ByVal xCpl As String, ByVal xBairro As String, _
                                    ByVal cMun As Integer, ByVal CEP As String, ByVal cPais As String, ByVal fone As String, _
                                    ByVal IE As String, ByVal ISUF As String, ByVal email As String, ByVal IM As String, _
                                    ByVal indIEDest As Integer, ByVal id_cliente As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_NFE_dest_Atualiza '" & id_nf & "','" & tipo_pessoa & "','" & cnpj & "'")
    str_builder.Append(",'" & xNome & "','" & xLgr & "','" & nro & "','" & xCpl & "','" & xBairro & "','" & cMun & "'")
    str_builder.Append(",'" & CEP & "','" & cPais & "','" & fone & "','" & IE & "','" & ISUF & "','" & email & "'")
    str_builder.Append(",'" & IM & "','" & indIEDest & "','" & id_cliente & "'")

    Try
      conexao.ExecutaComando(str_builder.ToString())

    Catch ex As Exception
      MsgBox("ERRO AO SALVAR OS DADOS DO DESTINATÁRIO: " & ex.Message() & "-----------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
    End Try

  End Sub

End Class
