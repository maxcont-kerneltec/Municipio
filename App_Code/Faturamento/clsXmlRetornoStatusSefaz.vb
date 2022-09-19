Imports System.Xml
Imports Microsoft.VisualBasic

Public Class clsXmlRetornoStatusSefaz

  Private _idLote, _tpAmb, _cOrgao, _cStat, _cStatLote, _cUF, _tMed, _tpEvento, _nSeqEvento, _ano, _mod, _serie, _nNFIni, _nNFFin As Integer
  Private _verAplic, _xMotivo, _xMotivoLote, _dhRecbto, _nRec, _chNFe, _nProt, _digVal, _xEvento, _CNPJDest, _dhRegEvento, _CNPJ As String
  Private _emailDest, _xSolucao As String
  
  Public Sub New()

  End Sub

  Property idLote() As Integer
    Get
      Return _idLote
    End Get
    Set(value As Integer)
      _idLote = value
    End Set
  End Property

  Property tpAmb() As Integer
    Get
      Return _tpAmb
    End Get
    Set(value As Integer)
      _tpAmb = value
    End Set
  End Property

  Property cOrgao() As Integer
    Get
      Return _cOrgao
    End Get
    Set(value As Integer)
      _cOrgao = value
    End Set
  End Property

  Property cStat() As Integer
    Get
      Return _cStat
    End Get
    Set(value As Integer)
      _cStat = value
    End Set
  End Property

  Property cStatLote() As Integer
    Get
      Return _cStatLote
    End Get
    Set(value As Integer)
      _cStatLote = value
    End Set
  End Property

  Property verAplic() As String
    Get
      Return _verAplic
    End Get
    Set(value As String)
      _verAplic = value
    End Set
  End Property

  Property xMotivo() As String
    Get
      Return _xMotivo
    End Get
    Set(value As String)
      _xMotivo = value
    End Set
  End Property

  Property xMotivoLote() As String
    Get
      Return _xMotivoLote
    End Get
    Set(value As String)
      _xMotivoLote = value
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

  Property dhRecbto() As String
    Get
      Return _dhRecbto
    End Get
    Set(value As String)
      _dhRecbto = value
    End Set
  End Property

  Property nRec() As String
    Get
      Return _nRec
    End Get
    Set(value As String)
      _nRec = value
    End Set
  End Property

  Property tMed() As Integer
    Get
      Return _tMed
    End Get
    Set(value As Integer)
      _tMed = value
    End Set
  End Property

  Property chNFe() As String
    Get
      Return _chNFe
    End Get
    Set(value As String)
      _chNFe = value
    End Set
  End Property

  Property nProt() As String
    Get
      Return _nProt
    End Get
    Set(value As String)
      _nProt = value
    End Set
  End Property

  Property digVal() As String
    Get
      Return _digVal
    End Get
    Set(value As String)
      _digVal = value
    End Set
  End Property

  Property xEvento() As String
    Get
      Return _xEvento
    End Get
    Set(value As String)
      _xEvento = value
    End Set
  End Property

  Property CNPJDest() As String
    Get
      Return _CNPJDest
    End Get
    Set(value As String)
      _CNPJDest = value
    End Set
  End Property
  
  Property emailDest() As String
    Get
	  Return _emailDest
	End Get	
	Set(value As String)
	  _emailDest = value
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

  Property dhRegEvento() As String
    Get
      Return _dhRegEvento
    End Get
    Set(value As String)
      _dhRegEvento = value
    End Set
  End Property

  Property tpEvento() As Integer
    Get
      Return _tpEvento
    End Get
    Set(value As Integer)
      _tpEvento = value
    End Set
  End Property

  Property nSeqEvento() As Integer
    Get
      Return _nSeqEvento
    End Get
    Set(value As Integer)
      _nSeqEvento = value
    End Set
  End Property

  Property ano() As Integer
    Get
      Return _ano
    End Get
    Set(value As Integer)
      _ano = value
    End Set
  End Property

  Property CNPJ() As String
    Get
      Return _CNPJ
    End Get
    Set(value As String)
      _CNPJ = value
    End Set
  End Property

  Property modd() As Integer
    Get
      Return _mod
    End Get
    Set(value As Integer)
      _mod = value
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

  Property nNFIni() As Integer
    Get
      Return _nNFIni
    End Get
    Set(value As Integer)
      _nNFIni = value
    End Set
  End Property

  Property nNFFin() As Integer
    Get
      Return _nNFFin
    End Get
    Set(value As Integer)
      _nNFFin = value
    End Set
  End Property


  ''' <summary>
  ''' Lê o arquivo de retorno das notas de evento... e salva as informações no banco, caso cStat = 135...
  ''' Pega as informações: idLote, tpAmb, verAplic, cOrgao, cStat, xMotivo
  ''' </summary>
  ''' <param name="caminho_arquivo"></param>
  Public Function ArquivoRetornoRetEnvEvento(ByVal id_empresa As Integer, ByVal caminho_arquivo As String, ByVal xCorrecao As String, _
                                             ByVal xCondUso As String, ByVal xJust As String) As Integer
    Me.cStat = -1

    Dim m_xmld As XmlDocument
    Dim m_node, m_node_princ As XmlNode
    Dim qtde_linhas As Integer = 0

    m_xmld = New XmlDocument
    m_xmld.Load(caminho_arquivo)

    Try
      For Each m_node_princ In m_xmld.GetElementsByTagName("retEvento")
        For Each m_node In m_node_princ.ChildNodes
          If Not (m_node("tpAmb") Is Nothing) Then
            Me.tpAmb = m_node("tpAmb").InnerText
          End If

          If Not (m_node("verAplic") Is Nothing) Then
            Me.verAplic = m_node("verAplic").InnerText
          End If

          If Not (m_node("cOrgao") Is Nothing) Then
            Me.cOrgao = m_node("cOrgao").InnerText
          End If

          If Not (m_node("cStat") Is Nothing) Then
            Me.cStat = m_node("cStat").InnerText
          End If

          If Not (m_node("xMotivo") Is Nothing) Then
            Me.xMotivo = m_node("xMotivo").InnerText
          End If

          If Not (m_node("chNFe") Is Nothing) Then
            Me.chNFe = m_node("chNFe").InnerText
          End If

          If Not (m_node("tpEvento") Is Nothing) Then
            Me.tpEvento = m_node("tpEvento").InnerText
          End If

          If Not (m_node("xEvento") Is Nothing) Then
            Me.xEvento = m_node("xEvento").InnerText
          End If

          If Not (m_node("nSeqEvento") Is Nothing) Then
            Me.nSeqEvento = m_node("nSeqEvento").InnerText
          End If

          If Not (m_node("CNPJDest") Is Nothing) Then
            Me.CNPJDest = m_node("CNPJDest").InnerText
          End If

          If Not (m_node("emailDest") Is Nothing) Then
            Me.emailDest = m_node("emailDest").InnerText
          End If

          If Not (m_node("dhRegEvento") Is Nothing) Then
            Me.dhRegEvento = m_node("dhRegEvento").InnerText
          End If

          If Not (m_node("nProt") Is Nothing) Then
            Me.nProt = m_node("nProt").InnerText
          End If

          If Me.cStat = 135 Or Me.cStat = 155 Then 'Carta de correção/cancelamento foi enviada com sucesso...
            Dim salva As New clsXmlRetornoStatusSefazSalva

            Try
              salva.SalvaRetornoSefazEvento(id_empresa, Me.tpEvento, Me.nSeqEvento, Me.tpAmb, Me.chNFe, Me.nProt, _
                                            Me.dhRegEvento, xCorrecao, xCondUso, caminho_arquivo, xJust)
              Me.cStat = 0
            Catch ex As Exception
              Me.xMotivo = "ERRO AO SALVAR O RETORNO DO EVENTO NO BANCO DE DADOS: " & ex.Message() & "---------" & ex.StackTrace()
            End Try
          End If

        Next
      Next

    Catch ex As Exception
      Me.xMotivo = "FALHA AO LER O ARQUIVO XML DE RETORNO DO EVENTO: " & ex.Message() & "---------" & ex.StackTrace()
    End Try

    Return Me.cStat

  End Function

  ''' <summary>
  ''' Lê o arquivo de retorno do Sefaz para notas enviada no método síncrono...
  ''' Pega as informações: tpAmb, verAplic, cStat, xMotivo, cUF, dhRecbto, nRec, tMed
  ''' </summary>
  ''' <param name="caminho_arquivo"></param>
  Public Sub ArquivoRetornoRetEnviNFe(ByVal caminho_arquivo As String)
    Dim m_xmld As XmlDocument
    Dim m_node As XmlNode
    m_xmld = New XmlDocument

    m_xmld.Load(caminho_arquivo)

    Try
      For Each m_node In m_xmld.GetElementsByTagName("retEnviNFe")
        Me.tpAmb = m_node.ChildNodes(0).InnerText
        Me.verAplic = m_node.ChildNodes(1).InnerText
        Me.cStat = m_node.ChildNodes(2).InnerText
        Me.xMotivo = m_node.ChildNodes(3).InnerText
        Me.cUF = m_node.ChildNodes(4).InnerText
        Me.dhRecbto = m_node.ChildNodes(5).InnerText
      Next

      For Each m_node In m_xmld.GetElementsByTagName("infRec")
        Me.nRec = m_node.ChildNodes(0).InnerText
        Me.tMed = m_node.ChildNodes(1).InnerText
      Next
    Catch ex As Exception
      Me.xMotivo = "ERRO AO EFETUAR A LEITURA DO XML DE RETORNO DO SEFAZ: " & ex.Message() & "---------" & ex.StackTrace()
    End Try

  End Sub

  ''' <summary>
  ''' Lê o arquivo de retorno da consulta do status do envio da nota ao Sefaz...
  ''' Pega o valor da variável xSolucao... para casos onde o XML é de erro na emissão...
  ''' </summary>
  Public Sub ArquivoRetornoConsReciNFe(ByVal id_empresa As Integer, ByVal id_nf As Integer, ByVal caminho_arquivo As String)
    Dim m_xmld As XmlDocument
    Dim m_node As XmlNode
    Dim qtde_linhas As Integer

    m_xmld = New XmlDocument

    m_xmld.Load(caminho_arquivo)

    Try
      For Each m_node In m_xmld.GetElementsByTagName("infProt")
        If Not (m_node("tpAmb") Is Nothing) Then
          Me.tpAmb = m_node("tpAmb").InnerText
        End If

        If Not (m_node("verAplic") Is Nothing) Then
          Me.verAplic = m_node("verAplic").InnerText
        End If

        If Not (m_node("chNFe") Is Nothing) Then
          Me.chNFe = m_node("chNFe").InnerText
        End If

        If Not (m_node("dhRecbto") Is Nothing) Then
          Me.dhRecbto = m_node("dhRecbto").InnerText
        End If

        If Not (m_node("nProt") Is Nothing) Then
          Me.nProt = m_node("nProt").InnerText
        End If

        If Not (m_node("digVal") Is Nothing) Then
          Me.digVal = m_node("digVal").InnerText
        End If

        If Not (m_node("cStat") Is Nothing) Then
          Me.cStat = m_node("cStat").InnerText
        End If

        If Not (m_node("xMotivo") Is Nothing) Then
          Me.xMotivo = m_node("xMotivo").InnerText
        End If

        If Not (m_node("digVal") Is Nothing) Then
          Me.digVal = m_node("digVal").InnerText
        End If

        Try
          Dim salva As New clsXmlRetornoStatusSefazSalva

          If Me.cStat = 100 Or Me.cStat = 302 Then '100=Autorizado | 302=Denegado.. SALVA AS INFORMAÇÕES NO SISTEMA...
            salva.SalvaRetornoSefazAutorizacao(id_empresa, id_nf, "4.00", Me.tpAmb, Me.verAplic, Me.chNFe, Me.dhRecbto, Me.nProt, Me.digVal, Me.cStat, Me.xMotivo, caminho_arquivo)

            If id_empresa = 328 Or id_empresa = 830 Then 'Usado para gravar o caminho do arquivo da CDA... que está dando erro...
              Dim nfe_ide As New clsNFEide

              nfe_ide.SalvaCaminhoArquivoXml(id_nf, id_empresa, 0, caminho_arquivo)
            End If

            If salva.xMotivo <> "" Then
              _xMotivo = salva.xMotivo
            End If

            InsereIdinfProt(caminho_arquivo) 'Insere o atributo Id na tag infProt...

          Else 'Salva o erro para melhora no atendimento...
            salva.SalvaRetornoSefazErro(id_empresa, id_nf, Me.tpAmb, Me.verAplic, Me.chNFe, Me.dhRecbto, Me.cStat, Me.xMotivo)
            _xSolucao = salva.xSolucao
          End If

        Catch ex As Exception
          _cStat = -1
          _xMotivo = "ERRO AO SALVAR AS INFORMAÇÕES DO XML DE RETORNO DO SEFAZ NO BANCO DE DADOS: " & ex.Message() & "--------" & ex.StackTrace()
        End Try

      Next
    Catch ex As Exception
      _cStat = -1
      _xMotivo = "ERRO AO REALIZAR A LEITURA DO XML DE RETORNO DO SEFAZ: " & ex.Message() & "------" & ex.StackTrace()
    End Try
  End Sub

  Public Function ArquivoRetornoRetInutNFe(ByVal id_empresa As Integer, ByVal xJust As String, ByVal caminho_arquivo As String) As Boolean
    Dim m_xmld As XmlDocument
    Dim m_node As XmlNode
    Dim qtde_linhas As Integer

    m_xmld = New XmlDocument

    m_xmld.Load(caminho_arquivo)

    Try
      For Each m_node In m_xmld.GetElementsByTagName("infInut")
        qtde_linhas = m_node.ChildNodes.Count - 1

        If qtde_linhas = 12 Then 'A nota foi gerada com sucesso... trás 8 linhas de retorno
          Me.tpAmb = m_node.ChildNodes(0).InnerText
          Me.verAplic = m_node.ChildNodes(1).InnerText
          Me.cStat = m_node.ChildNodes(2).InnerText
          Me.xMotivo = m_node.ChildNodes(3).InnerText
          Me.cUF = m_node.ChildNodes(4).InnerText
          Me.ano = m_node.ChildNodes(5).InnerText
          Me.CNPJ = m_node.ChildNodes(6).InnerText
          Me.modd = m_node.ChildNodes(7).InnerText
          Me.serie = m_node.ChildNodes(8).InnerText
          Me.nNFIni = m_node.ChildNodes(9).InnerText
          Me.nNFFin = m_node.ChildNodes(10).InnerText
          Me.dhRecbto = m_node.ChildNodes(11).InnerText
          Me.nProt = m_node.ChildNodes(12).InnerText

          If Me.cStat = 102 Then 'Inutilização enviada com sucesso... salva no banco de dados...
            Dim salva As New clsXmlRetornoStatusSefazSalva()

            salva.SalvaRetornoInutilizacao(id_empresa, Me.modd, Me.serie, Me.nNFIni, Me.nNFFin, xJust, Me.nProt, Me.dhRecbto, caminho_arquivo)
          End If
		  
		  Return true
        End If

      Next
    Catch ex As Exception
      Me.cStat = -1
      Me.xMotivo = "ERRO AO LER O ARQUIVO DE RETORNO DO SEFAZ REFERENTE A INUTILIZAÇÃO: " & ex.Message() & "----------" & ex.StackTrace()
    
	  Return false
	End Try
	
	Return true
  End Function

  Private Sub InsereIdinfProt(ByVal caminho_xml As String)
    Dim m_xmld As New XmlDocument
    Dim m_attri As XmlAttribute
    Dim m_node As XmlNode
    Dim root As XmlNode
    Dim nProt As String

    m_xmld.Load(caminho_xml)

    Try
      For Each m_node In m_xmld.GetElementsByTagName("infProt")
        nProt = "Id" & m_node.ChildNodes(4).InnerText
      Next

      For Each m_node In m_xmld.GetElementsByTagName("protNFe")
        root = m_node.FirstChild
      Next

      'Create a new attribute.
      Dim ns As String = root.GetNamespaceOfPrefix("Id")
      Dim attr As XmlNode = m_xmld.CreateNode(XmlNodeType.Attribute, "Id", ns)
      attr.Value = nProt

      'Add the attribute to the document
      root.Attributes.SetNamedItem(attr)

      Using writer As New XmlTextWriter(caminho_xml, System.Text.Encoding.UTF8)
        writer.Formatting = Formatting.None 'Formata o arquivo XMl para não conter quebra de linhas entre as tags...
        m_xmld.Save(writer)
      End Using

      'm_xmld.Save(caminho_xml)
    Catch ex As Exception
      _xMotivo = "ERRO AO INSERIR O ATRIBUTO ID NA TAG infProt: " & ex.Message() & "-------------" & ex.StackTrace()
    End Try
  End Sub

End Class
