Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class clsNfeEmit

  Private _id_emit, _id_emit2, _cMun, _cPais, _CRT, _id_fornec, _id_cliente As Integer
  Private _cnpj, _xNome, _xFant, _xLgr, _nro, _xCpl, _xBairro, _xMun, _UF, _CEP, _xPais, _fone, _IE, _IEST As String
  Private _IM, _CNAE As String

  Private conexao1 As New SqlConnection
  Public Sub New()

  End Sub

  Property id_emit() As Integer
    Get
      Return _id_emit
    End Get
    Set(value As Integer)
      _id_emit = value
    End Set
  End Property

  Property id_emit2() As Integer
    Get
      Return _id_emit2
    End Get
    Set(value As Integer)
      _id_emit2 = value
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

  Property CRT() As Integer
    Get
      Return _CRT
    End Get
    Set(value As Integer)
      _CRT = value
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

  Property id_cliente() As Integer
    Get
      Return _id_cliente
    End Get
    Set(value As Integer)
      _id_cliente = value
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

  Property xFant() As String
    Get
      Return _xFant
    End Get
    Set(value As String)
      _xFant = value
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

  Property IEST() As String
    Get
      Return _IEST
    End Get
    Set(value As String)
      _IEST = value
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

  Property CNAE() As String
    Get
      Return _CNAE
    End Get
    Set(value As String)
      _CNAE = value
    End Set
  End Property


  Public Sub PegaUmEmitenteNfe(ByVal id_empresa As Integer, ByVal id_emit As Integer)
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader
    Dim conexao As New clsConexao

    str_builder.Append("SELECT A.cnpj, dbo.fLC(A.xNome), dbo.fLC(A.xFant), dbo.fLC(A.xLgr), dbo.fLC(A.nro) as nro, dbo.fLC(A.xCpl), dbo.fLC(A.xBairro), A.cMun,  ")
    str_builder.Append("dbo.fLC(A.xMun), A.UF, A.CEP, A.cPais, dbo.fLC(A.xPais), A.fone, A.IE, ISNULL((A.IEST), ''), ISNULL((A.IM), ''), ISNULL((A.CNAE), ''), ISNULL((A.CRT), ''),  ")
    str_builder.Append("dbo.fCNPJ_Le(RIGHT('00000000000000' + CAST(A.cnpj as varchar(20)), 14)) as cnpjFormat, B.fax ")
    str_builder.Append("FROM NFE_emit AS A ")
    str_builder.Append("INNER JOIN tbEmpresas AS B ON (B.id_empresa = " & id_empresa & ") ")
    str_builder.Append("WHERE (id_emit = " & id_emit & ") ")


    Try
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)
      'dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        'Me.cnpj = dr(0)
        Me.xNome = dr(1)
        Me.xFant = dr(2)
        Me.xLgr = dr(3)
        Me.nro = dr(4)
        Me.xCpl = dr(5)
        Me.xBairro = dr(6)
        Me.cMun = dr(7)
        Me.xMun = dr(8)
        Me.UF = dr(9)
        Me.CEP = dr(10)
        Me.cPais = dr(11)
        Me.xPais = dr(12)
        Me.fone = dr(13)
        Me.IE = dr(14)
        Me.IEST = dr(15)
        Me.IM = dr(16)
        Me.CNAE = dr(17)
        Me.CRT = dr(18)
        Me.cnpj = dr(19)
      Loop

      conexao.FechaBanco(conexao1)
      dr.Close()
    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      MsgBox("ERRO AO LISTAR O EMITENTE DA NOTA:" & ex.Message() & "--------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
    End Try


  End Sub

End Class
