Imports Microsoft.VisualBasic
Imports System.Data.SqlClient

Public Class clsXmlRetornoStatusSefazSalva

  Private conexao1 As New SqlConnection
  Public Sub New()

  End Sub

  Private _xMotivo, _xSolucao As String
  'Private asp_nNF, asp_dEmiAs, asp_nome_emit, asp_email_dest, asp_email_emp String
  'Private asp_sig_chNFe, asp_nome_dest, asp_arq_xml, asp_canc_arq_xml, asp_corrige_XML, asp_tpEvento String

  Property xMotivo() As String
    Get
      Return _xMotivo
    End Get
    Set(value As String)
      _xMotivo = value
    End Set
  End Property

  Property xSolucao() As String
    Get
      Return _xSolucao
    End Get
    Set(value As String)
      _xSolucao = value
    End Set
  End Property

  ''' <summary>
  ''' SALVA AS INFORMAÇÕES DO RETORNO REFERENTE A NOTA FISCAL...
  ''' </summary>  
  Public Sub SalvaRetornoSefazAutorizacao(ByVal id_empresa As Integer, ByVal id_nf As Integer, ByVal versao As String, ByVal tpAmb As Integer, ByVal verAplic As String, _
                                          ByVal chNFe As String, ByVal dhRecbto As String, ByVal nProt As String, ByVal digVal As String, ByVal cStat As Integer, _
                                          ByVal xMotivo As String, ByVal caminho_arquivo As String)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_NFE_infProt '" & id_nf & "','" & versao & "','" & tpAmb & "','" & verAplic & "', ")
    str_builder.Append("'" & chNFe & "','" & Left(dhRecbto, 19) & "','" & nProt & "','" & digVal & "','" & cStat & "', ")
    str_builder.Append("'" & xMotivo & "','" & caminho_arquivo & "'")

    Try
      conexao.ExecutaComando(str_builder.ToString())

      BaixaEstoque(id_empresa, id_nf) 'Realiza a baixa dos itens da NF do estoque...
      GeraFinanceiro(id_nf) 'Gera o financeiro referente a NF...
      GeraContabilidade(id_nf) 'Gera a contabilidade referente a NF...

    Catch ex As Exception
      _xMotivo = "ERRO AO SALVAR AS INFORMAÇÕES DO XML DE RETORNO DO SEFAZ NO BANCO DE DADOS: " & ex.Message() & "-------" & ex.StackTrace()
    End Try
  End Sub

  ''' <summary>
  ''' SALVA O RETORNO DOS EVENTOS(CC E CANCELAMENTO) NO BANCO DE DADOS...
  ''' </summary> 
  Public Sub SalvaRetornoSefazEvento(ByVal id_empresa As Integer, ByVal tpEvento As Integer, ByVal nSeqEvento As Integer, _
                                     ByVal tpAmb As Integer, ByVal chNFe As String, ByVal nProt As String, ByVal dhEvento As String, _
                                     ByVal xCorrecao As String, ByVal xCondUso As String, ByVal arq_xml As String, ByVal xJust As String)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader
	Dim id_nf As Integer

    str_builder.Append("EXEC sp9_NFE_evento '" & id_empresa & "','" & tpEvento & "','" & nSeqEvento & "'")
    str_builder.Append(",'" & tpAmb & "','" & chNFe & "','" & nProt & "','" & Left(dhEvento, 19) & "','" & xCorrecao & "'")
    str_builder.Append(",'" & xCondUso & "','" & arq_xml & "','" & xJust & "'")

    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      Do While dr.Read()
        id_nf = dr(0)
      Loop
      conexao.FechaBanco(conexao1)
      dr.Close()
	  
	  If tpEvento = 110111 Then 'CANCELAMENTO... realiza os cancelamentos abaixo...
		BaixaEstoqueCanc(id_empresa, id_nf)
		MovFinancCanc(id_nf)
		MovContabCanc(id_nf)
	  End If
	  
    Catch ex As Exception
      _xMotivo = "ERRO AO SALVAR AS INFORMAÇÕES DA CARTA DE CORREÇÃO NO BANCO DE DADOS: " & ex.Message() & "-------" & ex.StackTrace()
    End Try
  End Sub

  Public Sub SalvaRetornoInutilizacao(ByVal id_empresa As Integer, ByVal modd As Integer, ByVal serie As Integer, ByVal nNFIni As Integer, ByVal nNFFim As Integer, ByVal xJust As String, ByVal nProt As String, ByVal dhRecbto As String, ByVal caminho_xml As String)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    str_builder.Append("EXEC sp9_NFE_XML_Inut '" & id_empresa & "','" & modd & "','" & serie & "','" & nNFIni & "'")
    str_builder.Append(",'" & nNFFim & "','" & xJust & "','" & nProt & "','" & dhRecbto & "','" & caminho_xml & "'")

    Try
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao.FechaBanco(conexao1)
    Catch ex As Exception
      _xMotivo = "ERRO AO SALVAR AS INFORMAÇÕES DA INUTILIZAÇÃO NO BANCO DE DADOS: " & ex.Message() & "--------" & ex.StackTrace()
    End Try
  End Sub

  Private Sub GeraFinanceiro(ByVal id_nf As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_NFE_Mov_Financ '" & id_nf & "'")

    Try
      conexao.ExecutaComando(str_builder.ToString())
    Catch ex As Exception
      _xMotivo = "FALHA AO GERAR O FINANCEIRO DA NOTA FISCAL: " & ex.Message() & "---------" & ex.StackTrace()
    End Try
  End Sub
  
  Private Sub MovFinancCanc(Byval id_nf As Integer)
	Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_NFE_Mov_Financ_Canc '" & id_nf & "'")

    Try
      conexao.ExecutaComando(str_builder.ToString())
    Catch ex As Exception
      _xMotivo = "FALHA AO CANCELAR O FINANCEIRO DA NOTA FISCAL: " & ex.Message() & "---------" & ex.StackTrace()
    End Try
  End Sub

  Private Sub GeraContabilidade(ByVal id_nf As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    Try
      conexao.ExecutaComando("EXEC sp9_NFE_Mov_Contab '" & id_nf & "'")
	  
    Catch ex As Exception
      _xMotivo = "ERRO AO GERAR A CONTABILIDADE PARA A NFE: " & ex.Message() & "-----------" & ex.StackTrace()
    End Try
  End Sub
  
  Private Sub MovContabCanc(ByVal id_nf As Integer)
	Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    Try
      conexao.ExecutaComando("EXEC sp9_NFE_Mov_Contab_Canc '" & id_nf & "'")
	  
    Catch ex As Exception
      _xMotivo = "ERRO AO CANCELAR A CONTABILIDADE PARA A NFE: " & ex.Message() & "-----------" & ex.StackTrace()
    End Try	
  End Sub

  Private Sub BaixaEstoque(ByVal id_empresa As Integer, ByVal id_nf As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_NFE_Estoque_Baixa '" & id_empresa & "','" & id_nf & "','0'")

    Try
      conexao.ExecutaComando(str_builder.ToString())
    Catch ex As Exception
      Me.xMotivo = "ERRO AO REALIZAR A BAIXA DOS ITENS NO ESTOQUE: " & ex.Message() & "----------" & ex.StackTrace()
    End Try
  End Sub
  
  Private Sub BaixaEstoqueCanc(Byval id_empresa As Integer, Byval id_nf As Integer)
	Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_NFE_Estoque_Baixa_Canc '" & id_empresa & "','" & id_nf & "'")

    Try
      conexao.ExecutaComando(str_builder.ToString())
    Catch ex As Exception
      _xMotivo = "ERRO AO REALIZAR O CANCELAMENTO DOS ITENS NO ESTOQUE: " & ex.Message() & "----------" & ex.StackTrace()
    End Try
  End Sub

  ''' <summary>
  ''' A tabela de erro é zerada todos os dias... é utlizada somente para agilização do atendimento... listando os erro em uma tela...
  ''' Insere valor na variável xSolucao
  ''' </summary>  
  Public Sub SalvaRetornoSefazErro(ByVal id_empresa As Integer, ByVal id_nf As Integer, ByVal tpAmb As Integer, ByVal verAplic As String, _
                                   ByVal chNFe As String, ByVal dhRecbto As String, ByVal cStat As Integer, ByVal xMotivo As String)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    str_builder.Append("EXEC sp9_Salva_Erro_Trans_Temp '" & id_empresa & "','" & id_nf & "','" & Left(dhRecbto, 10) & "'")
    str_builder.Append(",'" & cStat & "','" & xMotivo & "'")

    Try
      'dr = conexao.RetornaDataReader(str_builder.toString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      Do While dr.Read()
        _xSolucao = dr(0)
      Loop
      conexao.FechaBanco(conexao1)
      dr.Close()
    Catch ex As Exception
      _xMotivo = "ERRO AO SALVAR AS INFORMAÇÕES DO ERRO: " & ex.Message() & "-------" & ex.StackTrace()
    End Try
  End Sub

  'Public Sub EmailEnvia(ByVal id_empresa As Integer, ByVal id_nf As Integer)
  '  Dim str_builder As New StringBuilder
  '  Dim dr As SqlDataReader
  '  Dim conexao As New clsConexao

  '  '0 = id_usuario
  '  str_builder.Append("EXEC sp9_Pega_Uma_NFe_Email '" & id_empresa & "','" & id_nf & "','','E','0'")


  '  Try
  '    conexao1 = conexao.AbreBanco()
  '    dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)
  '    'dr = conexao.RetornaDataReader(str_builder.ToString())

  '    Do While dr.Read()
  '      asp_nNF = mPegaEmail(0, 0)
  '      asp_dEmi = mPegaEmail(1, 0)
  '      asp_nome_emit = mPegaEmail(2, 0)
  '      asp_email_dest = mPegaEmail(3, 0)
  '      asp_email_emp = mPegaEmail(4, 0)
  '      asp_sig_chNFe = mPegaEmail(5, 0)
  '      asp_nome_dest = mPegaEmail(6, 0)
  '      asp_arq_xml = "" & mPegaEmail(7, 0)
  '      asp_canc_arq_xml = "" & mPegaEmail(8, 0)
  '      asp_corrige_XML = "" & mPegaEmail(9, 0)
  '      asp_tpEvento = "" & mPegaEmail(10, 0)
  '    Loop

  '    conexao.FechaBanco(conexao1)
  '    dr.Close()
  '  Catch ex As Exception
  '    conexao.FechaBanco(conexao1)
  '    MsgBox("ERRO AO LISTAR O EMITENTE DA NOTA:" & ex.Message() & "--------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
  '  End Try
  'End Sub

End Class
