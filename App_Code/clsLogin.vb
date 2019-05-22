Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Text

Public Class clsLogin

  Private _id_empresa, _id_usuario, _id_contador, _id_func, _id_sistema, _msg_retorno, _cnpj_emp, _uf As String
  Private _empresa, _login, _senha, _logado, _sec_login, _descr_empresa, _nome_usuario, _permissao, _conta As String
  Private _sts_bloq, _acesso, _tipo_cliente, _cpf_usuario, _dt_ultimo, _dt_cadastro, _dt_altera, _tipo_comissao As String
  Private _fAlt_Preco, _email, _fone, _tipo_conta, _nome_sacado, _dig_ccorr, _obs, _fCad_cliente, _fCompra_Autoriza As String
  Private _fAcesso_nota, _descr_banco, _fCria_CFOP, _fGera_orcamento, _fAcessa_clientes, _gerente_vendas, _crq As String
  Private _aliq_comissao, _desconto_max, _valor_meta As Decimal
  Private _tabela_precos, _id_revenda, _id_ccusto, _fAcesso, _cod_banco, _agencia, _dig_agencia, _ccorr As Integer
  Private _id_cliente, _id_usuario_holding, _id_dpto, _id_vendedor_maxcont, _result, _cmun As Integer

  Public Sub New()

  End Sub

  Property id_empresa() As String
    Get
      Return _id_empresa
    End Get
    Set(value As String)
      _id_empresa = value
    End Set
  End Property

  Property id_usuario() As String
    Get
      Return _id_usuario
    End Get
    Set(value As String)
      _id_usuario = value
    End Set
  End Property

  Property id_contador() As String
    Get
      Return _id_contador
    End Get
    Set(value As String)
      _id_contador = value
    End Set
  End Property

  Property id_func() As String
    Get
      Return _id_func
    End Get
    Set(value As String)
      _id_func = value
    End Set
  End Property

  Property empresa() As String
    Get
      Return _empresa
    End Get
    Set(value As String)
      _empresa = value
    End Set
  End Property

  Property login() As String
    Get
      Return _login
    End Get
    Set(value As String)
      _login = value
    End Set
  End Property

  Property senha() As String
    Get
      Return _senha
    End Get
    Set(value As String)
      _senha = value
    End Set
  End Property

  Property logado() As String
    Get
      Return _logado
    End Get
    Set(value As String)
      _logado = value
    End Set
  End Property

  Property sec_login() As String
    Get
      Return _sec_login
    End Get
    Set(value As String)
      _sec_login = value
    End Set
  End Property

  Property descr_empresa() As String
    Get
      Return _descr_empresa
    End Get
    Set(value As String)
      _descr_empresa = value
    End Set
  End Property

  Property nome_usuario() As String
    Get
      Return _nome_usuario
    End Get
    Set(value As String)
      _nome_usuario = value
    End Set
  End Property

  Property permissao() As String
    Get
      Return _permissao
    End Get
    Set(value As String)
      _permissao = value
    End Set
  End Property

  Property conta() As String
    Get
      Return _conta
    End Get
    Set(value As String)
      _conta = value
    End Set
  End Property

  Property sts_bloq() As String
    Get
      Return _sts_bloq
    End Get
    Set(value As String)
      _sts_bloq = value
    End Set
  End Property

  Property id_sistema() As Integer
    Get
      Return _id_sistema
    End Get
    Set(value As Integer)
      _id_sistema = value
    End Set
  End Property

  Property acesso() As String
    Get
      Return _acesso
    End Get
    Set(value As String)
      _acesso = value
    End Set
  End Property

  Property tipo_cliente() As String
    Get
      Return _tipo_cliente
    End Get
    Set(value As String)
      _tipo_cliente = value
    End Set
  End Property

  Property cpf_usuario() As String
    Get
      Return _cpf_usuario
    End Get
    Set(value As String)
      _cpf_usuario = value
    End Set
  End Property

  Property dt_ultimo() As String
    Get
      Return _dt_ultimo
    End Get
    Set(value As String)
      _dt_ultimo = value
    End Set
  End Property

  Property dt_cadastro() As String
    Get
      Return _dt_cadastro
    End Get
    Set(value As String)
      _dt_cadastro = value
    End Set
  End Property

  Property dt_altera() As String
    Get
      Return _dt_altera
    End Get
    Set(value As String)
      _dt_altera = value
    End Set
  End Property

  Property aliq_comissao() As Decimal
    Get
      Return _aliq_comissao
    End Get
    Set(value As Decimal)
      _aliq_comissao = value
    End Set
  End Property

  Property tipo_comissao() As String
    Get
      Return _tipo_comissao
    End Get
    Set(value As String)
      _tipo_comissao = value
    End Set
  End Property

  Property tabela_precos() As Integer
    Get
      Return _tabela_precos
    End Get
    Set(value As Integer)
      _tabela_precos = value
    End Set
  End Property

  Property fAlt_Preco() As String
    Get
      Return _fAlt_Preco
    End Get
    Set(value As String)
      _fAlt_Preco = value
    End Set
  End Property

  Property desconto_max() As Decimal
    Get
      Return _desconto_max
    End Get
    Set(value As Decimal)
      _desconto_max = value
    End Set
  End Property

  Property id_revenda() As Integer
    Get
      Return _id_revenda
    End Get
    Set(value As Integer)
      _id_revenda = value
    End Set
  End Property

  Property id_ccusto() As Integer
    Get
      Return _id_ccusto
    End Get
    Set(value As Integer)
      _id_ccusto = value
    End Set
  End Property

  Property fAcesso() As Integer
    Get
      Return _fAcesso
    End Get
    Set(value As Integer)
      _fAcesso = value
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

  Property fone() As String
    Get
      Return _fone
    End Get
    Set(value As String)
      _fone = value
    End Set
  End Property

  Property tipo_conta() As String
    Get
      Return _tipo_conta
    End Get
    Set(value As String)
      _tipo_conta = value
    End Set
  End Property

  Property nome_sacado() As String
    Get
      Return _nome_sacado
    End Get
    Set(value As String)
      _nome_sacado = value
    End Set
  End Property

  Property cod_banco() As Integer
    Get
      Return _cod_banco
    End Get
    Set(value As Integer)
      _cod_banco = value
    End Set
  End Property

  Property agencia() As Integer
    Get
      Return _agencia
    End Get
    Set(value As Integer)
      _agencia = value
    End Set
  End Property

  Property dig_agencia() As Integer
    Get
      Return _dig_agencia
    End Get
    Set(value As Integer)
      _dig_agencia = value
    End Set
  End Property

  Property ccorr() As Integer
    Get
      Return _ccorr
    End Get
    Set(value As Integer)
      _ccorr = value
    End Set
  End Property

  Property dig_ccorr() As String
    Get
      Return _dig_ccorr
    End Get
    Set(value As String)
      _dig_ccorr = value
    End Set
  End Property

  Property obs() As String
    Get
      Return _obs
    End Get
    Set(value As String)
      _obs = value
    End Set
  End Property

  Property fCad_cliente() As String
    Get
      Return _fCad_cliente
    End Get
    Set(value As String)
      _fCad_cliente = value
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

  Property fCompra_Autoriza() As String
    Get
      Return _fCompra_Autoriza
    End Get
    Set(value As String)
      _fCompra_Autoriza = value
    End Set
  End Property

  Property fAcesso_nota() As String
    Get
      Return _fAcesso_nota
    End Get
    Set(value As String)
      _fAcesso_nota = value
    End Set
  End Property

  Property id_usuario_holding() As Integer
    Get
      Return _id_usuario_holding
    End Get
    Set(value As Integer)
      _id_usuario_holding = value
    End Set
  End Property

  Property descr_banco() As String
    Get
      Return _descr_banco
    End Get
    Set(value As String)
      _descr_banco = value
    End Set
  End Property

  Property valor_meta() As Decimal
    Get
      Return _valor_meta
    End Get
    Set(value As Decimal)
      _valor_meta = value
    End Set
  End Property

  Property fCria_CFOP() As String
    Get
      Return _fCria_CFOP
    End Get
    Set(value As String)
      _fCria_CFOP = value
    End Set
  End Property

  Property id_dpto() As Integer
    Get
      Return _id_dpto
    End Get
    Set(value As Integer)
      _id_dpto = value
    End Set
  End Property

  Property fGera_orcamento() As String
    Get
      Return _fGera_orcamento
    End Get
    Set(value As String)
      _fGera_orcamento = value
    End Set
  End Property

  Property fAcessa_clientes() As String
    Get
      Return _fAcessa_clientes
    End Get
    Set(value As String)
      _fAcessa_clientes = value
    End Set
  End Property

  Public Property gerente_vendas() As String
    Get
      Return _gerente_vendas
    End Get
    Set( value As String)
      _gerente_vendas = value
    End Set
  End Property

  Property id_vendedor_maxcont() As Integer
    Get
      Return _id_vendedor_maxcont
    End Get
    Set(value As Integer)
      _id_vendedor_maxcont = value
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

  Property crq() As String
    Get
      Return _crq
    End Get
    Set(value As String)
      _crq = value
    End Set
  End Property

  Property msg_retorno() As String
    Get
      Return _msg_retorno
    End Get
    Set(value As String)
      _msg_retorno = value
    End Set
  End Property
  
  Property cnpj_emp() AS String
	Get
	  Return _cnpj_emp
	End Get
	Set(value As String)
	  _cnpj_emp = value
	End Set
  End Property
  
  Property cmun() As Integer
	Get
	  Return _cmun
	End Get
	Set(value As Integer)
	  _cmun = value
	End Set
  End Property
  
  Property uf() As String
	Get
	  Return _uf
	End Get
	Set(value As String)
	  _uf = value
	End Set
  End Property

  ''' <summary>
  ''' Retorno: -1=Erro critico | 0=Acesso com sucesso | 1=EMPRESA INVÁLIDA | 2=SENHA INVÁLIDA OU USUÁRIO
  ''' </summary>
  ''' <param name="empresa"></param>
  ''' <param name="login"></param>
  ''' <param name="senha"></param>
  Public Function AcessaSistema( empresa As String,  login As String,  senha As String) As Integer
    Dim retorno As Integer = -1
    Dim conexao As New clsConexao
    Dim clsLogin As New clsLogin
    Dim funcao As New StringBuilder
    Dim dr As SqlDataReader
    Dim ip As String = "" 'PegaIP() 'pega o IP da máquina
    Dim sessao_asp_classic As String = "0000000000" 'A SESSÃO DO ASP_CLASSIC É NUMERICO / NO VB.NET É VARCHAR
    Dim instance As New SessionIDManager
    Dim context As HttpContext = Nothing
    Dim sessao As String

    sessao = instance.CreateSessionID(context) 'cria uma única identificação para sessão

    empresa = Replace(Replace(empresa, " ", ""), "'", "")
    login = Replace(Replace(login, " ", ""), "'", "")
    senha = Replace(Replace(senha, " ", ""), "'", "")

    funcao.Append("EXEC spLogin_Dominio '" & empresa & "','" & login & "','" & senha & "','" & sessao_asp_classic & "'")
    funcao.Append(",'" & ip & "','" & sessao & "'")

    Try
      dr = conexao.RetornaDataReader(funcao.ToString)

      If dr.HasRows() Then
        Do While dr.Read
          Me.logado = dr(0)
          Me.sec_login = dr(1)
          _id_empresa = "" & dr(2)
          Me.id_usuario = "" & dr(3)
          Me.id_contador = "" & dr(4)
          Me.id_func = dr(5)
          Me.descr_empresa = dr(6)
          Me.nome_usuario = dr(7)
          Me.permissao = dr(8)
          Me.conta = dr(10)
          Me.sts_bloq = dr(11)
          Me.id_sistema = dr(12)
          Me.acesso = dr(13)
          Me.tipo_cliente = dr(15)
          Me.cnpj_emp = dr(18)
          Me.cmun = dr(19)
          Me.uf = dr(20)
        Loop

        If _id_empresa = "" Then _id_empresa = 0

        If Me.logado = "F" Then 'SENHA INVÁLIDA OU USUÁRIO
          retorno = 2
          Return retorno

        ElseIf Me.logado = "I" Then 'EMPRESA INVÁLIDA
          retorno = 1
          Return retorno

        End If
      End If 'FIM DO HasRows

      retorno = 0
      dr.Close()
      dr = Nothing

    Catch ex As Exception
      Me.msg_retorno = "ERRO AO PEGAR OS DADOS PARA LOGIN: " & ex.Message & " - " & ex.StackTrace
    End Try

    Return retorno

  End Function  

End Class
