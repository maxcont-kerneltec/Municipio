Imports Microsoft.VisualBasic
Imports System.Xml
Imports System.Data
Imports System

Public Class clsCriaXmlNFeVersao4
  Private _xMotivo, _ambiente, _local_xml, _caminho_pasta As String
  Private _id_retorno, _id_nf, _id_empresa, _id_emit, _id_dest, _cDV, _finNFe, _result As Integer
  'http://www.vbmania.com.br/index.php?modulo=forum&metodo=abrir&id=337965&pagina=174
  'https://www.sefaz.rs.gov.br/NFE/NFE-EXE.aspx

  Public Sub New()

  End Sub

  Property id_retorno() As Integer
    Get
      Return _id_retorno
    End Get
    Set(value As Integer)
      _id_retorno = value
    End Set
  End Property

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

  Property id_empresa() As Integer
    Get
      Return _id_empresa
    End Get
    Set(value As Integer)
      _id_empresa = value
    End Set
  End Property

  Property id_emit() As Integer
    Get
      Return _id_emit
    End Get
    Set(value As Integer)
      _id_emit = value
    End Set
  End Property

  Property id_dest() As Integer
    Get
      Return _id_dest
    End Get
    Set(value As Integer)
      _id_dest = value
    End Set
  End Property

  Property cDV As Integer
    Get
      Return _cDV
    End Get
    Set(value As Integer)
      _cDV = value
    End Set
  End Property

  Property finNFe() As Integer
    Get
      Return _finNFe
    End Get
    Set(value As Integer)
      _finNFe = value
    End Set
  End Property

  Property xMotivo As String
    Get
      Return _xMotivo
    End Get
    Set(value As String)
      _xMotivo = value
    End Set
  End Property

  Property ambiente() As String
    Get
      Return _ambiente
    End Get
    Set(value As String)
      _ambiente = value
    End Set
  End Property

  Property local_xml() As String
    Get
      Return _local_xml
    End Get
    Set(value As String)
      _local_xml = value
    End Set
  End Property

  Property caminho_pasta() As String
    Get
      Return _caminho_pasta
    End Get
    Set(value As String)
      _caminho_pasta = value
    End Set
  End Property

  ''' <summary>
  ''' ambiente: P=Produção | H=Homologação
  ''' Gera o XML para validação... e gera o local completo cujo qual o XML foi salvo... variável(caminho_pasta)...
  ''' ERROS: (1=Falha na criação da pasta de armazenamento do XML | 2=Falha na montagem do XML)
  ''' </summary>
  ''' <param name="id_nf"></param>
  ''' <param name="id_empresa"></param>
  ''' <param name="ambiente"></param>
  ''' <returns></returns>
  Public Function CriaXmlNFe(ByVal id_nf As Integer, ByVal id_empresa As Integer, ByVal nNF As Integer, ByVal caminho_salva_xml As String) As Boolean
    Me.id_nf = id_nf
    Me.id_empresa = id_empresa
    Dim nNF_str As String = nNF

    'Gera a chave de acesso da NFE...
    Dim nfe_ide As New clsNFEide
    Dim chave_acesso As String = nfe_ide.PegaChaveAcessoNFE(Me.id_nf)
    Me.cDV = Right(chave_acesso, 1)

    'Cria o arquivo XML na pasta....
    Me.caminho_pasta = caminho_salva_xml & "\" & nNF_str & ".xml"

    'Montagem do XML
    Try
      Using writer As New XmlTextWriter(caminho_pasta, System.Text.Encoding.UTF8)
        writer.Formatting = Formatting.None

        MontagemXML(writer, chave_acesso)
      End Using

      'Salva o caminho onde está o arquivo XML...
      nfe_ide.SalvaCaminhoArquivoXml(Me.id_nf, Me.id_empresa, 0, Me.caminho_pasta)
    Catch ex As Exception
      xMotivo = "FALHA NA MONTAGEM DO XML: " & ex.Message() & "------------" & ex.StackTrace()
      Return False
    End Try

    Return True
  End Function

  Private Sub MontagemXML(ByVal writer As XmlTextWriter, ByVal chave_acesso As String)
    writer.WriteStartDocument(True) 'INICIO DOCUMENTO


    writer.WriteStartElement("enviNFe", "http://www.portalfiscal.inf.br/nfe")
    writer.WriteAttributeString("versao", "4.00")

    writer.WriteStartElement("idLote")
    writer.WriteString("000000000000003")
    writer.WriteEndElement()

    writer.WriteStartElement("indSinc")
    writer.WriteString("0")
    writer.WriteEndElement()

    writer.WriteStartElement("NFe")
    'writer.WriteAttributeString("xmlns", "http://www.portalfiscal.inf.br/nfe")

    writer.WriteStartElement("infNFe") 'INICIO infNFe... A. Dados da Nota Fiscal eletrônica
    writer.WriteAttributeString("Id", "NFe" & chave_acesso)
    writer.WriteAttributeString("versao", "4.00")
	

    IdentificacaoNFe(writer) 'B. Identificação da Nota Fiscal eletrônica
    IdentificacaoEmitente(writer) 'C. Identificação do Emitente da Nota Fiscal eletrônica
    'IdentificacaoFisco(writer) D. Identificação do Fisco Emitente da NF-e
    IdentificacaoDestinatario(writer) 'E. Identificação do Destinatário da Nota Fiscal eletrônica
    IdentificacaoLocalRetirada(writer) 'F. Identificação do Local de Retirada
    IdentificacaoLocalEntrega(writer) 'G. Identificação do Local de Entrega
    'Autorizacao(writer) GA. Autorização para obter XML
    DetalhamentoProdutos(writer) 'H. Detalhamento de Produtos e Serviços da NF-e
    TotalNfe(writer) 'W. Total da NF-e
    Transportadora(writer) 'X. Informações do Transporte da NF-e
    Cobranca(writer) 'Y. Dados da Cobrança
    InformacoesAdicionais(writer) 'Z. Informações Adicionais da NF-e


    writer.WriteEndElement() 'FIM infNFe
    writer.WriteEndElement() 'FIM NFe
    writer.WriteEndElement() 'FIM enviNFe

    writer.WriteEndDocument() 'FIM DOCUMENTO


    writer.Flush()
    writer.Close()
  End Sub

  Private Sub IdentificacaoNFe(ByVal writer As XmlTextWriter)
    Dim nfe As New clsNFEide
    Dim ajuste As New clsAjuste
    Dim time_zone As String = ajuste.PegaTimeZone()

    Try
      nfe.PegaIdeNfe(Me.id_empresa, Me.id_nf)

      Me.id_emit = nfe.id_emit
      Me.id_dest = nfe.id_dest

      writer.WriteStartElement("ide") 'INICIO DA ABA IDE


      writer.WriteStartElement("cUF")
      writer.WriteString(nfe.cUF)
      writer.WriteEndElement()

      writer.WriteStartElement("cNF")
      writer.WriteString(Right("00000000" & nfe.cNF, 8))
      writer.WriteEndElement()

      writer.WriteStartElement("natOp")
      writer.WriteString(nfe.natOp)
      writer.WriteEndElement()

      writer.WriteStartElement("mod")
      writer.WriteString(nfe.mod_descr)
      writer.WriteEndElement()

      writer.WriteStartElement("serie")
      writer.WriteString(nfe.serie)
      writer.WriteEndElement()

      writer.WriteStartElement("nNF")
      writer.WriteString(nfe.nNF)
      writer.WriteEndElement()

      writer.WriteStartElement("dhEmi")
      writer.WriteString(ajuste.AMD(nfe.dEmi) & "T" & Right("00" & Hour(Date.Now()), 2) & ":" & Right("00" & Minute(Date.Now()), 2) & ":" & Right("00" & Second(Date.Now()), 2) & time_zone) '"-02:00"
      writer.WriteEndElement()

      writer.WriteStartElement("dhSaiEnt")
      writer.WriteString(ajuste.AMD(nfe.dSaiEnt) & "T" & Right("00" & Hour(Date.Now()), 2) & ":" & Right("00" & Minute(Date.Now()), 2) & ":" & Right("00" & Second(Date.Now()), 2) & time_zone) '"-02:00"
      writer.WriteEndElement()

      writer.WriteStartElement("tpNF")
      writer.WriteString(nfe.tpNF)
      writer.WriteEndElement()

      writer.WriteStartElement("idDest")
      writer.WriteString(nfe.idDest)
      writer.WriteEndElement()

      writer.WriteStartElement("cMunFG")
      writer.WriteString(nfe.cMunFG)
      writer.WriteEndElement()

      writer.WriteStartElement("tpImp")
      writer.WriteString(nfe.tpImp)
      writer.WriteEndElement()

      writer.WriteStartElement("tpEmis")
      writer.WriteString(nfe.tpEmis)
      writer.WriteEndElement()

      writer.WriteStartElement("cDV")
      writer.WriteString(Me.cDV)
      writer.WriteEndElement()

      If Me.id_empresa = "830" Then 'Empresa de teste FTEX2-TESTE... sempre vai emitir em homologação...
        writer.WriteStartElement("tpAmb")
        writer.WriteString(2)
        writer.WriteEndElement()
      Else
        writer.WriteStartElement("tpAmb")
        writer.WriteString(nfe.tpAmb)
        writer.WriteEndElement()
      End If

      writer.WriteStartElement("finNFe")
      writer.WriteString(nfe.finNFe)
      writer.WriteEndElement()
      Me.finNFe = nfe.finNFe

      writer.WriteStartElement("indFinal")
      writer.WriteString(nfe.indFinal)
      writer.WriteEndElement()

      writer.WriteStartElement("indPres")
      writer.WriteString(nfe.indPres)
      writer.WriteEndElement()

      writer.WriteStartElement("procEmi")
      writer.WriteString(nfe.procEmi)
      writer.WriteEndElement()

      writer.WriteStartElement("verProc")
      writer.WriteString(nfe.verProc)
      writer.WriteEndElement()


      NotasFiscaisReferenciadas(writer) 'BA. Documento Fiscal Referenciado

      writer.WriteEndElement() 'FIM DA ABA IDE
	  

    Catch ex As Exception
      'MsgBox("ERRO NA TRANSMISSÃO DA NFE: " & ex.Message() & "-----------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
      writer.WriteStartElement("ERRO")
      writer.WriteString("ERRO NA TRANSMISSÃO DA NFE: " & ex.Message() & "-----------" & ex.StackTrace())
      writer.WriteEndElement()
    End Try
  End Sub

  Private Sub NotasFiscaisReferenciadas(ByVal writer As XmlTextWriter)
    Dim nfe_referenc As New clsNfeReferenc
    Dim dv As New DataView
    Dim ajuste As New clsAjuste

    dv = nfe_referenc.ListaNotaFiscalEletronicaReferenciada(Me.id_nf) 'NOTAS FISCAIS ELETRÔNICAS REFERENCIADAS E CT-e...

    If dv.Count > 0 Then
      For Each drv As DataRowView In dv
        writer.WriteStartElement("NFref") 'INICIO NFref

        writer.WriteStartElement("refNFe")
        writer.WriteString(drv("refNFe"))
        writer.WriteEndElement()

        writer.WriteEndElement() 'FIM NFref
		
      Next
    End If


    nfe_referenc = New clsNfeReferenc
    dv = New DataView
    Dim AAMM As String = ""

    dv = nfe_referenc.ListaNotaFiscalReferenciada(Me.id_nf) 'NOTA FISCAL REFERENCIADA...

    If dv.Count > 0 Then
      For Each drv As DataRowView In dv
        writer.WriteStartElement("NFref")

        writer.WriteStartElement("refNF")

        writer.WriteStartElement("cUF")
        writer.WriteString(drv("cUF"))
        writer.WriteEndElement()

        AAMM = drv("AAMM") 'No banco de dados é salvo MM/AA
        AAMM = AAMM.Replace("/", "")
        AAMM = Right(AAMM, 2) & Left(AAMM, 2)

        writer.WriteStartElement("AAMM")
        writer.WriteString(AAMM)
        writer.WriteEndElement()

        writer.WriteStartElement("CNPJ")
        writer.WriteString(Right("00000000000000" & ajuste.FormataCNPJ(drv("cnpj"), FormataCnpj.semCaracter), 14))
        writer.WriteEndElement()

        writer.WriteStartElement("mod")
        writer.WriteString(Right("00" & drv("mod"), 2))
        writer.WriteEndElement()

        writer.WriteStartElement("serie")
        writer.WriteString(drv("serie"))
        writer.WriteEndElement()

        writer.WriteStartElement("nNF")
        writer.WriteString(drv("nNF"))
        writer.WriteEndElement()

        writer.WriteEndElement() 'Fim refNF

        writer.WriteEndElement() 'FIM NFref		
		
      Next
    End If


    nfe_referenc = New clsNfeReferenc
    dv = New DataView

    dv = nfe_referenc.ListaNotaFiscalProdutorRural(Me.id_nf) 'PRODUTOR RURAL

    If dv.Count > 0 Then
      For Each drv As DataRowView In dv
		Dim cnpj As String = drv("cnpj") 
		
        writer.WriteStartElement("NFref")

        writer.WriteStartElement("refNFP")

        If drv("cUF") > 0 Then
          writer.WriteStartElement("cUF")
          writer.WriteString(drv("cUF"))
          writer.WriteEndElement()
        End If

        If drv("AAMM") <> "" Then
          AAMM = drv("AAMM") 'No banco de dados é salvo MM/AA
          AAMM = AAMM.Replace("/", "")
          AAMM = Right(AAMM, 2) & Left(AAMM, 2)

          writer.WriteStartElement("AAMM")
          writer.WriteString(AAMM)
          writer.WriteEndElement()
        End If

        If cnpj.Length > 11 Then 'CNPJ
          writer.WriteStartElement("CNPJ")
          writer.WriteString(Right("00000000000000" & ajuste.FormataCPF(drv("cnpj"), FormataCpf.semCaracter), 14))
          writer.WriteEndElement()
        Else 'If drv("cnpj") > 0 Then
          writer.WriteStartElement("CPF")
          writer.WriteString(Right("00000000000" & ajuste.FormataCPF(drv("cnpj"), FormataCpf.semCaracter), 11))
          writer.WriteEndElement()
        End If

        If drv("IE") <> "" Then
          writer.WriteStartElement("IE")
          writer.WriteString(drv("IE"))
          writer.WriteEndElement()
        End If

        If Trim(drv("mod")) <> "" Then
          writer.WriteStartElement("mod")
          writer.WriteString(Right("00" & Trim(drv("mod")), 2))
          writer.WriteEndElement()
        End If

        writer.WriteStartElement("serie")
        writer.WriteString(drv("serie"))
        writer.WriteEndElement()

        writer.WriteStartElement("nNF")
        writer.WriteString(drv("nNF"))
        writer.WriteEndElement()

        writer.WriteEndElement() 'FIM refNFP

        writer.WriteEndElement() 'FIM NFref		
		
      Next
    End If


    Dim cupom As New clsNfeRetECF
    dv = New DataView

    dv = cupom.ListaEcfUmaNota(Me.id_nf) 'CUPOM FISCAL REFERENCIADO

    If dv.Count > 0 Then
      For Each drv As DataRowView In dv
        writer.WriteStartElement("NFref")

        writer.WriteStartElement("refECF")

        writer.WriteStartElement("mod")
        writer.WriteString(drv("mod_descr"))
        writer.WriteEndElement()

        writer.WriteStartElement("nECF")
        writer.WriteString(drv("nECF"))
        writer.WriteEndElement()

        writer.WriteStartElement("nCOO")
        writer.WriteString(drv("nCOO"))
        writer.WriteEndElement()

        writer.WriteEndElement() 'FIM refECF

        writer.WriteEndElement() 'FIm NFref		
		
      Next
    End If
  End Sub

  Private Sub IdentificacaoEmitente(ByVal writer As XmlTextWriter)
    writer.WriteStartElement("emit")

    Dim emitente As New clsNfeEmit
    Dim ajuste As New clsAjuste

    emitente.PegaUmEmitenteNfe(Me.id_empresa, Me.id_emit)

    writer.WriteStartElement("CNPJ")
    writer.WriteString(ajuste.FormataCNPJ(emitente.cnpj, FormataCnpj.semCaracter))
    writer.WriteEndElement()

    writer.WriteStartElement("xNome")
    writer.WriteString(RTrim(emitente.xNome))
    writer.WriteEndElement()

    If emitente.xFant <> "" Then
      writer.WriteStartElement("xFant")
      writer.WriteString(emitente.xFant)
      writer.WriteEndElement()
    End If

    writer.WriteStartElement("enderEmit")

    writer.WriteStartElement("xLgr")
    writer.WriteString(RTrim(emitente.xLgr))
    writer.WriteEndElement()

    writer.WriteStartElement("nro")
    writer.WriteString(RTrim(emitente.nro))
    writer.WriteEndElement()

    If emitente.xCpl <> "" Then
      writer.WriteStartElement("xCpl")
      writer.WriteString(RTrim(emitente.xCpl))
      writer.WriteEndElement()
    End If

    writer.WriteStartElement("xBairro")
    writer.WriteString(RTrim(emitente.xBairro))
    writer.WriteEndElement()

    writer.WriteStartElement("cMun")
    writer.WriteString(emitente.cMun)
    writer.WriteEndElement()

    writer.WriteStartElement("xMun")
    writer.WriteString(emitente.xMun)
    writer.WriteEndElement()

    writer.WriteStartElement("UF")
    writer.WriteString(emitente.UF)
    writer.WriteEndElement()

    writer.WriteStartElement("CEP")
    writer.WriteString(emitente.CEP)
    writer.WriteEndElement()

    writer.WriteStartElement("cPais")
    writer.WriteString(emitente.cPais)
    writer.WriteEndElement()

    writer.WriteStartElement("xPais")
    writer.WriteString(emitente.xPais)
    writer.WriteEndElement()

    If emitente.fone <> "" Then
      writer.WriteStartElement("fone")
      writer.WriteString(emitente.fone)
      writer.WriteEndElement()
    End If


    writer.WriteEndElement() 'FIM enderEmit
	


    If emitente.IE <> "" Then
      writer.WriteStartElement("IE")
      writer.WriteString(emitente.IE)
      writer.WriteEndElement()
    End If

    If emitente.IEST <> "" Then
      writer.WriteStartElement("IEST")
      writer.WriteString(emitente.IEST)
      writer.WriteEndElement()
    End If

    If emitente.IM <> "" Then
      writer.WriteStartElement("IM")
      writer.WriteString(emitente.IM)
      writer.WriteEndElement()
    
	  if Trim(emitente.CNAE) <> "" Then
        writer.WriteStartElement("CNAE")
        writer.WriteString(emitente.CNAE)
        writer.WriteEndElement()
	  End If
    End If

    writer.WriteStartElement("CRT")
    writer.WriteString(emitente.CRT)
    writer.WriteEndElement()

    writer.WriteEndElement() 'Fim emit	
	
  End Sub

  Private Sub IdentificacaoDestinatario(ByVal writer As XmlTextWriter)
    writer.WriteStartElement("dest") 'dest

    Dim destinatario As New clsNfeDest
    Dim ajuste As New clsAjuste

    destinatario.PegaUmDestinatarioNfe(Me.id_dest)
	
	If destinatario.UF = "EX" Then
	  writer.WriteStartElement("idEstrangeiro")
      writer.WriteString("")
      writer.WriteEndElement()
    ElseIf destinatario.tipo_pessoa = "J" Then
      writer.WriteStartElement("CNPJ")
      writer.WriteString(ajuste.FormataCNPJ(destinatario.cnpj, FormataCnpj.semCaracter))
      writer.WriteEndElement()
    Else
      writer.WriteStartElement("CPF")
      writer.WriteString(ajuste.FormataCPF(destinatario.cnpj, FormataCpf.semCaracter))
      writer.WriteEndElement()
    End If

    writer.WriteStartElement("xNome")
    writer.WriteString(RTrim(destinatario.xNome))
    writer.WriteEndElement()

    writer.WriteStartElement("enderDest")

    writer.WriteStartElement("xLgr")
    writer.WriteString(RTrim(destinatario.xLgr))
    writer.WriteEndElement()

    writer.WriteStartElement("nro")
    writer.WriteString(RTrim(destinatario.nro))
    writer.WriteEndElement()

    If destinatario.xCpl <> "" Then
      writer.WriteStartElement("xCpl")
      writer.WriteString(RTrim(destinatario.xCpl))
      writer.WriteEndElement()
    End If

    writer.WriteStartElement("xBairro")
    writer.WriteString(RTrim(destinatario.xBairro))
    writer.WriteEndElement()

    writer.WriteStartElement("cMun")
    writer.WriteString(destinatario.cMun)
    writer.WriteEndElement()

    writer.WriteStartElement("xMun")
    writer.WriteString(destinatario.xMun)
    writer.WriteEndElement()

    writer.WriteStartElement("UF")
    writer.WriteString(destinatario.UF)
    writer.WriteEndElement()
	
	If destinatario.UF <> "EX" Then
      writer.WriteStartElement("CEP")
      writer.WriteString(destinatario.CEP)
      writer.WriteEndElement()
	End If
	
    writer.WriteStartElement("cPais")
    writer.WriteString(destinatario.cPais)
    writer.WriteEndElement()

    writer.WriteStartElement("xPais")
    writer.WriteString(destinatario.xPais)
    writer.WriteEndElement()

    If destinatario.fone <> "" Then
      writer.WriteStartElement("fone")
      writer.WriteString(destinatario.fone)
      writer.WriteEndElement()
    End If


    writer.WriteEndElement() 'FIM enderDest
	

    writer.WriteStartElement("indIEDest")
    writer.WriteString(destinatario.indIEDest)
    writer.WriteEndElement()
	
    If destinatario.IE <> "" Then
      writer.WriteStartElement("IE")
      writer.WriteString(destinatario.IE)
      writer.WriteEndElement()
    End If

    If destinatario.ISUF <> "" Then
      writer.WriteStartElement("ISUF")
      writer.WriteString(destinatario.ISUF)
      writer.WriteEndElement()
    End If

    If destinatario.IM <> "" Then
      writer.WriteStartElement("IM")
      writer.WriteString(destinatario.IM)
      writer.WriteEndElement()
    End If

    If destinatario.email <> "" Then
      writer.WriteStartElement("email")
      writer.WriteString(RTrim(destinatario.email))
      writer.WriteEndElement()
    End If


    writer.WriteEndElement() 'FIM dest		
  End Sub

  Private Sub IdentificacaoLocalRetirada(ByVal writer As XmlTextWriter)
    Dim retirada As New clsNfeEntrega
    Dim ajuste As New clsAjuste

    If retirada.VerifExisteEndRet(Me.id_nf) Then 'ENDEREÇO DE RETIRADA É DIFERENTE DO ENDEREÇO DO EMITENTE
      retirada.ListaEndRetUmaNfe(Me.id_nf)

      writer.WriteStartElement("retirada")

      If retirada.tipo_pessoa = "J" Then
        writer.WriteStartElement("CNPJ")
        writer.WriteString(ajuste.FormataCNPJ(retirada.cnpj, FormataCnpj.semCaracter))
        writer.WriteEndElement()
      Else
        writer.WriteStartElement("CPF")
        writer.WriteString(ajuste.FormataCPF(retirada.cnpj, FormataCpf.semCaracter))
        writer.WriteEndElement()
      End If

      writer.WriteStartElement("xLgr")
      writer.WriteString(RTrim(retirada.xLgr))
      writer.WriteEndElement()

      writer.WriteStartElement("nro")
      writer.WriteString(RTrim(retirada.nro))
      writer.WriteEndElement()

      If retirada.xCpl <> "" Then
        writer.WriteStartElement("xCpl")
        writer.WriteString(retirada.xCpl)
        writer.WriteEndElement()
      End If

      writer.WriteStartElement("xBairro")
      writer.WriteString(retirada.xBairro)
      writer.WriteEndElement()

      writer.WriteStartElement("cMun")
      writer.WriteString(retirada.cMun)
      writer.WriteEndElement()

      writer.WriteStartElement("xMun")
      writer.WriteString(retirada.xMun)
      writer.WriteEndElement()

      writer.WriteStartElement("UF")
      writer.WriteString(retirada.UF)
      writer.WriteEndElement()

      writer.WriteEndElement() 'FIM retirada
    End If
  End Sub

  Private Sub IdentificacaoLocalEntrega(ByVal writer As XmlTextWriter)
    Dim entrega As New clsNfeEntrega
    Dim ajuste As New clsAjuste

    If entrega.VerifExisteEndEnt(Me.id_nf) Then 'ENDEREÇO DE ENTREGA É DIFERENTE DO ENDEREÇO DO CLIENTE
      entrega.ListaEndEntUmaNfe(Me.id_nf)

      writer.WriteStartElement("entrega")

      If entrega.tipo_pessoa = "J" Then
        writer.WriteStartElement("CNPJ")
        writer.WriteString(ajuste.FormataCNPJ(entrega.cnpj, FormataCnpj.semCaracter))
        writer.WriteEndElement()
      Else
        writer.WriteStartElement("CPF")
        writer.WriteString(ajuste.FormataCPF(entrega.cnpj, FormataCpf.semCaracter))
        writer.WriteEndElement()
      End If

      writer.WriteStartElement("xLgr")
      writer.WriteString(RTrim(entrega.xLgr))
      writer.WriteEndElement()

      writer.WriteStartElement("nro")
      writer.WriteString(RTrim(entrega.nro))
      writer.WriteEndElement()
	  
	  If entrega.xCpl <> "" Then
	    writer.WriteStartElement("xCpl")
        writer.WriteString(entrega.xCpl)
        writer.WriteEndElement()
	  End If      

      writer.WriteStartElement("xBairro")
      writer.WriteString(entrega.xBairro)
      writer.WriteEndElement()

      writer.WriteStartElement("cMun")
      writer.WriteString(entrega.cMun)
      writer.WriteEndElement()

      writer.WriteStartElement("xMun")
      writer.WriteString(entrega.xMun)
      writer.WriteEndElement()

      writer.WriteStartElement("UF")
      writer.WriteString(entrega.UF)
      writer.WriteEndElement()

      writer.WriteEndElement() 'FIM entrega
    End If
  End Sub

  ''' <summary>
  ''' H. Detalhamento de Produtos e Serviços da NF-e
  ''' </summary>
  ''' <param name="writer"></param>
  Private Sub DetalhamentoProdutos(ByVal writer As XmlTextWriter)
    Dim item As New clsNfeItem
    Dim cEAN, cEANTrib As String
    Dim vProd, vOutro, vFrete, vDesc, vSeg As Decimal
    Dim ajuste As New clsAjuste
	
    Try
      'dv = item.ListaItemAbaProdutoNfe(Me.id_nf)

      Using dv As New DataView(item.ListaItemAbaProdutoNfe(Me.id_nf))
        For Each drv As DataRowView In dv
          writer.WriteStartElement("det") 'INICIO det
          writer.WriteAttributeString("nItem", drv("nItem"))

          writer.WriteStartElement("prod") 'INICIO prod

          writer.WriteStartElement("cProd")
          writer.WriteString(RTrim(drv("cProd")))
          writer.WriteEndElement()

          cEAN = drv("cEAN")
          'if cEAN = "" Then cEAN = "SEM GTIN"
          'https://www.oobj.com.br/bc/article/rejei%C3%A7%C3%A3o-883-gtin-cean-sem-informa%C3%A7%C3%A3o-nitem999-como-resolver-771.html
          writer.WriteStartElement("cEAN")
          writer.WriteString(cEAN)
          writer.WriteEndElement()

          writer.WriteStartElement("xProd")
          writer.WriteString(RTrim(drv("xProd")))
          writer.WriteEndElement()

          writer.WriteStartElement("NCM")
          writer.WriteString(drv("NCM"))
          writer.WriteEndElement()

          If drv("cest") <> "" Then
            writer.WriteStartElement("CEST")
            writer.WriteString(drv("cest"))
            writer.WriteEndElement()
          End If

          If drv("indEscala") <> "" Then
            writer.WriteStartElement("indEscala")
            writer.WriteString(drv("indEscala"))
            writer.WriteEndElement()
          End If

          If drv("CNPJFab") <> "" Then
            writer.WriteStartElement("CNPJFab")
            writer.WriteString(ajuste.FormataCNPJ(drv("CNPJFab"), FormataCnpj.semCaracter))
            writer.WriteEndElement()
          End If

          If drv("cBenef") <> "" Then
            writer.WriteStartElement("cBenef")
            writer.WriteString(drv("cBenef"))
            writer.WriteEndElement()
          End If

          If drv("EXTIPI") <> "" Then
            writer.WriteStartElement("EXTIPI")
            writer.WriteString(drv("EXTIPI"))
            writer.WriteEndElement()
          End If

          writer.WriteStartElement("CFOP")
          writer.WriteString(drv("CFOP"))
          writer.WriteEndElement()

          writer.WriteStartElement("uCom")
          writer.WriteString(drv("uCom"))
          writer.WriteEndElement()

          writer.WriteStartElement("qCom")
          writer.WriteString(drv("qCom").ToString().Replace(",", "."))
          writer.WriteEndElement()

          writer.WriteStartElement("vUnCom")
          writer.WriteString(drv("vUnCom").ToString().Replace(",", "."))
          writer.WriteEndElement()

          vProd = FormatNumber(drv("vProd"), 2)

          writer.WriteStartElement("vProd")
          writer.WriteString(vProd.ToString().Replace(",", "."))
          writer.WriteEndElement()

          cEANTrib = drv("cEANTrib")
          'if cEANTrib = "" Then cEANTrib = "SEM GTIN"
		  
          writer.WriteStartElement("cEANTrib")
          writer.WriteString(cEANTrib)
          writer.WriteEndElement()

          writer.WriteStartElement("uTrib")
          writer.WriteString(drv("uTrib"))
          writer.WriteEndElement()

          writer.WriteStartElement("qTrib")
          writer.WriteString(drv("qTrib").ToString().Replace(",", "."))
          writer.WriteEndElement()

          writer.WriteStartElement("vUnTrib")
          writer.WriteString(drv("vUnTrib").ToString().Replace(",", "."))
          writer.WriteEndElement()

          If FormatNumber(drv("vFrete"), 2) > 0 Then
            vFrete = FormatNumber(drv("vFrete"), 2)
			
            writer.WriteStartElement("vFrete")
            writer.WriteString(vFrete.ToString("0.00").Replace(",", "."))
            writer.WriteEndElement()
          End If

          If FormatNumber(drv("vSeg"), 2) > 0 Then
			vSeg = FormatNumber(drv("vSeg"), 2)
		  
            writer.WriteStartElement("vSeg")
            writer.WriteString(vSeg.ToString().Replace(",", "."))
            writer.WriteEndElement()
          End If

          If FormatNumber(drv("vDesc"), 2) > 0 Then
            vDesc = FormatNumber(drv("vDesc"), 2)
			
            writer.WriteStartElement("vDesc")
            writer.WriteString(vDesc.ToString("0.00").Replace(",", "."))
            writer.WriteEndElement()
          End If

          If FormatNumber(drv("vOutro"), 2) > 0 Then
            vOutro = FormatNumber(drv("vOutro"))
			
            writer.WriteStartElement("vOutro")
            writer.WriteString(vOutro.ToString("0.00").Replace(",", "."))
            writer.WriteEndElement()
          End If 'combustivel.qTemp.ToString("0.00").Replace(",", ".")

          writer.WriteStartElement("indTot")
          writer.WriteString(drv("indTot"))
          writer.WriteEndElement()
          
          'I01. Produtos e Serviços / Declaração de Importação
          DeclaracaoDeImportacao(writer, drv("nItem"))

          'I03. Produtos e Serviços / Grupo de Exportação
          DeclaracaoDeExportacao(writer, drv("nItem"))

          If drv("xPed") <> "" Then
            writer.WriteStartElement("xPed")
            writer.WriteString(drv("xPed"))
            writer.WriteEndElement()
          End If

          If drv("nItemPed") <> "" Then
            writer.WriteStartElement("nItemPed")
            writer.WriteString(drv("nItemPed"))
            writer.WriteEndElement()
          End If

          If drv("nFCI") <> "" Then
            writer.WriteStartElement("nFCI")
            writer.WriteString(drv("nFCI"))
            writer.WriteEndElement()
          End If
		  
          'I80. Rastreabilidade de produto
          ListaRastreabilidade(writer, drv("nItem"))

          'J. Produto Específico
          'JA. Detalhamento Específico de Veículos novos
          'K. Detalhamento Específico de Medicamento e de matérias-primas farmacêuticas
          'L. Detalhamento Específico de Armamentos

          ListaCombustivel(writer, drv("nItem")) 'LA. Detalhamento Específico de Combustíveis

          writer.WriteEndElement() 'FIM prod

          ListaImposto(writer, drv("nItem"), drv("vTotTrib")) 'M. Tributos incidentes no Produto ou Serviço

          infAdProd(writer, drv("nItem")) 'V. Informações adicionais (para o item da NF-e)

          writer.WriteEndElement() 'FIM det				
        Next
      End Using
    Catch ex As Exception
      writer.WriteStartElement("ERRO")
      writer.WriteString("ERRO AO PEGAR AS INFORMAÇÕES DO ITEM PARA ENVIO DA NFE: " & ex.Message() & "---------" & ex.StackTrace())
      writer.WriteEndElement()
      'MsgBox("ERRO AO PEGAR AS INFORMAÇÕES DO ITEM PARA ENVIO DA NFE: " & ex.Message() & "---------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
    End Try
  End Sub

  Private Sub TotalNfe(ByVal writer As XmlTextWriter)
    writer.WriteStartElement("total")

    ICMSTot(writer) 'Grupo Totais referentes ao ICMS

    writer.WriteEndElement() 'FIM total
  End Sub

  ''' <summary>
  ''' Grupo Informações do Transporte
  ''' </summary>
  ''' <param name="writer"></param>
  Private Sub Transportadora(ByVal writer As XmlTextWriter)
    Dim transp As New clsNFEide

    writer.WriteStartElement("transp")

    Try
      transp.PegaTransporteUmaNfe(Me.id_nf)

      writer.WriteStartElement("modFrete")
      writer.WriteString(transp.transp_modFrete)
      writer.WriteEndElement()

      Transporta(writer, transp.transp_CNPJ, transp.transp_tipo_pessoa, transp.transp_xNome, transp.transp_IE, transp.transp_xEnder, transp.transp_xMun, transp.transp_UF)

      RetTransp(writer, transp.retTransp_vServ, transp.retTransp_vBCRet, transp.retTransp_pICMSRet, transp.retTransp_vICMSRet, transp.retTransp_CFOP, transp.retTransp_cMunFG)

      VeicTransp(writer, transp.veicTransp_placa, transp.veicTransp_UF, transp.veicTransp_RNTC)

      Reboque(writer)

      Volume(writer)

      'Lacres(writer)
    Catch ex As Exception
      'MsgBox("ERRO AO PEGAR AS INFORMAÇÕES DO TRANSPORTE PARA ENVIO DA NFE: " & ex.Message() & "--------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
      writer.WriteStartElement("ERRO")
      writer.WriteString("ERRO AO PEGAR AS INFORMAÇÕES DO TRANSPORTE PARA ENVIO DA NFE: " & ex.Message() & "--------" & ex.StackTrace())
      writer.WriteEndElement()
    End Try

    writer.WriteEndElement() 'FIM transp
  End Sub

  Private Sub FormaPagamento(ByVal writer As XmlTextWriter, ByVal vLiq As Decimal)    	
    writer.WriteStartElement("pag")

    Dim inf_pgto As New clsNFeInformacoesPagamento
    Dim dv As DataView
    Dim vPag, vTroco As Decimal
    Dim ajuste As New clsAjuste

    Try
      dv = inf_pgto.PegaInfPagamentoUmaNFe(Me.id_nf)

      If inf_pgto.msg_erro <> "" Then
        writer.WriteStartElement("ERRO")
        writer.WriteString(inf_pgto.msg_erro)
        writer.WriteEndElement()
      End If

      If dv.Count > 0 Then
        For Each drv As DataRowView In dv
          vPag = drv(2)
          vTroco = drv(3)

          writer.WriteStartElement("detPag")

          writer.WriteStartElement("tPag")
          writer.WriteString(Right("00" & drv(1), 2))
          writer.WriteEndElement()

          writer.WriteStartElement("vPag")
          writer.WriteString(vPag.ToString("0.00").Replace(",", "."))
          writer.WriteEndElement()

          If drv(5) > 0 Then 'Tem grupo cartões preenchido...
            writer.WriteStartElement("card")

            writer.WriteStartElement("tpIntegra")
            writer.WriteString(drv(6))
            writer.WriteEndElement()

            Dim cnpj As String = Right("00000000000000" & drv(7), 14)

            writer.WriteStartElement("CNPJ")
            writer.WriteString(ajuste.FormataCNPJ(cnpj, FormataCnpj.semCaracter))
            writer.WriteEndElement()

            writer.WriteStartElement("tBand")
            writer.WriteString(Right("00" & drv(8), 2))
            writer.WriteEndElement()

            writer.WriteStartElement("cAut")
            writer.WriteString(drv(9))
            writer.WriteEndElement()

            writer.WriteEndElement()  'FIM card
          End If

          If vTroco > 0 Then
            writer.WriteStartElement("vTroco")
            writer.WriteString(vTroco.ToString("0.00").Replace(",", "."))
            writer.WriteEndElement()
          End If

          writer.WriteEndElement()  'FIM detPag
        Next

      Else 'Nota sem informação de pagamento, insere manual no XML
        writer.WriteStartElement("detPag")

        If Me.finNFe = 4 Or Me.finNFe = 3 Then 'FINALIDADE DE EMISSÃO 4=DEVOLUÇÃO DE MERCADORIA | 3=NOTA DE AJUSTE
          writer.WriteStartElement("tPag")
          writer.WriteString("90") 'Sem Pagamento
          writer.WriteEndElement()
        ElseIf vLiq > 0 Then
          writer.WriteStartElement("tPag")
          writer.WriteString("14") 'Mercantil
          writer.WriteEndElement()
        Else
          writer.WriteStartElement("tPag")
          writer.WriteString("99") 'Outros
          writer.WriteEndElement()
        End If

        writer.WriteStartElement("vPag")
        writer.WriteString(vLiq.ToString("0.00").Replace(",", "."))
        writer.WriteEndElement()

        writer.WriteEndElement()  'FIM detPag
      End If

    Catch ex As Exception
      writer.WriteStartElement("ERRO")
      writer.WriteString("ERRO AO PEGAR AS INFORMAÇÕES DO PAGAMENTO PARA ENVIO DA NFE: " & ex.Message() & "---------" & ex.StackTrace())
      writer.WriteEndElement()
    End Try

    writer.WriteEndElement() 'FIM pag
  End Sub

  ''' <summary>
  ''' YA. Formas de Pagamento
  ''' </summary>
  ''' <param name="writer"></param>
  Private Sub Cobranca(ByVal writer As XmlTextWriter)
    Dim cobranca As New clsNFEide

    Try
      writer.WriteStartElement("cobr")

      cobranca.PegaTotalUmaNfe(Me.id_empresa, Me.id_nf)

      Fatura(writer, cobranca.cob_nFat, cobranca.cob_vOrig, cobranca.cob_vDesc, cobranca.cob_vLiq)

      Duplicata(writer)

      writer.WriteEndElement() 'FIM cobr
	
      FormaPagamento(writer, cobranca.cob_vLiq) 'YA. Formas de Pagamento

    Catch ex As Exception
      'MsgBox("ERRO AO PEGAR AS INFORMAÇÕES DA COBRANÇA PARA ENVIO DA NFE: " & ex.Message() & "---------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
      writer.WriteStartElement("ERRO")
      writer.WriteString("ERRO AO PEGAR AS INFORMAÇÕES DA COBRANÇA PARA ENVIO DA NFE: " & ex.Message() & "---------" & ex.StackTrace())
      writer.WriteEndElement()
    End Try
  End Sub

  Private Sub InformacoesAdicionais(ByVal writer As XmlTextWriter)
    Dim inf_adic As New clsNFEide

    Try
      inf_adic.PegaInfAdicUmaNfe(Me.id_nf)

      writer.WriteStartElement("infAdic")

      If inf_adic.infAdFisco <> "" Then
        writer.WriteStartElement("infAdFisco")
        writer.WriteString(inf_adic.infAdFisco)
        writer.WriteEndElement()
      End If

      If inf_adic.infCpl <> "" Then
        writer.WriteStartElement("infCpl")
        writer.WriteString(RTrim(inf_adic.infCpl))
        writer.WriteEndElement()
      End If


      ObsCont(writer)
      'ObsFisco(writer)
      ProcRef(writer)

      writer.WriteEndElement() 'FIM infAdic
	  

      InformacoesComercioExterior(writer) 'ZA. Informações de Comércio Exterior

    Catch ex As Exception
      'MsgBox("ERRO AO PEGAR AS INFORMAÇÕES ADICIONAIS PARA ENVIO DA NFE: " & ex.Message() & "-----------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
      writer.WriteStartElement("ERRO")
      writer.WriteString("ERRO AO PEGAR AS INFORMAÇÕES ADICIONAIS PARA ENVIO DA NFE: " & ex.Message() & "-----------" & ex.StackTrace())
      writer.WriteEndElement()
    End Try
  End Sub

  ''' <summary>
  ''' I01. Produtos e Serviços / Declaração de Importação
  ''' </summary>
  ''' <param name="writer"></param>
  Private Sub DeclaracaoDeImportacao(ByVal writer As XmlTextWriter, ByVal nItem As Integer)
    Dim item_di As New clsNfeDI
    Dim dv As New DataView
    Dim ajuste As New clsAjuste
    Dim id_di As Integer

    Try
      dv = item_di.ListaDiUmaNfe(Me.id_nf, nItem)

      If item_di.msg_erro <> "" Then
        writer.WriteStartElement("ERRO")
        writer.WriteString(item_di.msg_erro)
        writer.WriteEndElement()
      End If

      If dv.Count > 0 Then
        For Each drv As DataRowView In dv
          Dim vAFRMM As Decimal
          vAFRMM = FormatNumber(drv("vAFRMM"))
		  
          id_di = drv("id_di")

          writer.WriteStartElement("DI")

          writer.WriteStartElement("nDI")
          writer.WriteString(drv("nDI"))
          writer.WriteEndElement()

          writer.WriteStartElement("dDI")
          writer.WriteString(ajuste.AMD(drv("dDI")))
          writer.WriteEndElement()

          writer.WriteStartElement("xLocDesemb")
          writer.WriteString(drv("xLocDesemb"))
          writer.WriteEndElement()

          writer.WriteStartElement("UFDesemb")
          writer.WriteString(drv("UFDesemb"))
          writer.WriteEndElement()

          writer.WriteStartElement("dDesemb")
          writer.WriteString(ajuste.AMD(drv("dDesemb")))
          writer.WriteEndElement()

          writer.WriteStartElement("tpViaTransp")
          writer.WriteString(drv("tpViaTransp"))
          writer.WriteEndElement()

          If vAFRMM > 0 Then
            writer.WriteStartElement("vAFRMM")
            writer.WriteString(vAFRMM.ToString().Replace(",", "."))
            writer.WriteEndElement()
          End If

          writer.WriteStartElement("tpIntermedio")
          writer.WriteString(drv("tpIntermedio"))
          writer.WriteEndElement()

          If drv("CNPJ") <> "" And drv("CNPJ") <> "-1" Then
            Dim cnpj As String = Right("00000000000000" & drv("CNPJ"), 14)
			
            writer.WriteStartElement("CNPJ")
            writer.WriteString(ajuste.FormataCNPJ(cnpj, FormataCnpj.semCaracter))
            writer.WriteEndElement()
          End If

          If Trim(drv("UFTerceiro")) <> "" Then
            writer.WriteStartElement("UFTerceiro")
            writer.WriteString(drv("UFTerceiro"))
            writer.WriteEndElement()
          End If

          writer.WriteStartElement("cExportador")
          writer.WriteString(drv("cExportador"))
          writer.WriteEndElement()


          ListaAdicaoDi(writer, nItem, id_di)


          writer.WriteEndElement() 'FIM DI
		  
        Next
      End If

    Catch ex As Exception
      writer.WriteStartElement("ERRO")
      writer.WriteString("ERRO AO PEGAR A DI DO ITEM PARA TRANSMISSÃO DA NOTA: " & ex.Message() & "---------" & ex.StackTrace())
      writer.WriteEndElement()
    End Try
  End Sub

  Private Sub DeclaracaoDeExportacao(ByVal writer As XmlTextWriter, ByVal nItem As Integer)
    Dim dv As New DataView
    Dim exportacao As New clsNFeItemGrupoExportacao

    Try
      dv = exportacao.ListaUmGrupoExport(Me.id_nf, nItem)

      If exportacao.msg_erro <> "" Then
        writer.WriteStartElement("ERRO")
        writer.WriteString(exportacao.msg_erro)
        writer.WriteEndElement()
      End If

      If dv.Count > 0 Then
        Dim qExport As Decimal

        For Each drv As DataRowView In dv
          qExport = drv("qExport")

          writer.WriteStartElement("detExport")

          If Trim(drv("nDraw")) <> "" Then
            writer.WriteStartElement("nDraw")
            writer.WriteString(drv("nDraw"))
            writer.WriteEndElement()
          End If

          If (Trim(drv("nRE")) <> "") Or (Trim(drv("chNFe")) <> "") Then
            writer.WriteStartElement("exportInd")

            writer.WriteStartElement("nRE")
            writer.WriteString(drv("nRE"))
            writer.WriteEndElement()

            writer.WriteStartElement("chNFe")
            writer.WriteString(drv("chNFe"))
            writer.WriteEndElement()

            writer.WriteStartElement("qExport")
            'writer.WriteString(qExport)
            writer.WriteString(drv("qExport").ToString().Replace(",", "."))
            writer.WriteEndElement()

            writer.WriteEndElement() 'FIM exportInd
          End If

          writer.WriteEndElement() 'FIM detExport
        Next
      End If

    Catch ex As Exception
      writer.WriteStartElement("ERRO")
      writer.WriteString("ERRO AO LISTAR O GRUPO EXPORTAÇÃO PARA O ITEM:  " & ex.Message() & "---------" & ex.StackTrace())
      writer.WriteEndElement()
    End Try
  End Sub

  Private Sub ListaAdicaoDi(ByVal writer As XmlTextWriter, ByVal nItem As Integer, ByVal id_di As Integer)
    Dim adicao As New clsNfeDIAdicao
    Dim dv As New DataView
	Dim vDescDI As Decimal 
	
    Try
      dv = adicao.ListaAdicaoDI(Me.id_nf, nItem, id_di)

      If adicao.msg_erro <> "" Then
        writer.WriteStartElement("ERRO")
        writer.WriteString(adicao.msg_erro)
        writer.WriteEndElement()
      End If

      If dv.Count > 0 Then
        For Each drv As DataRowView In dv
		  vDescDI = FormatNumber(drv("vDescDI"))
		
          writer.WriteStartElement("adi")

          writer.WriteStartElement("nAdicao")
          writer.WriteString(drv("nAdicao"))
          writer.WriteEndElement()

          writer.WriteStartElement("nSeqAdic")
          writer.WriteString(drv("nSeqAdic"))
          writer.WriteEndElement()

          writer.WriteStartElement("cFabricante")
          writer.WriteString(drv("cFabricante"))
          writer.WriteEndElement()

		  If vDescDI > 0 Then
			writer.WriteStartElement("vDescDI")
			writer.WriteString(vDescDI.ToString("0.00").Replace(",", "."))
			writer.WriteEndElement()
		  End If
		  
		  If drv("nDraw") > 0 Then
		    writer.WriteStartElement("nDraw")
            writer.WriteString(drv("nDraw"))
            writer.WriteEndElement()
		  End If          

          writer.WriteEndElement() 'FIM adi		  
        Next
      End If

    Catch ex As Exception
      'MsgBox("ERRO AO LISTAR AS ADIÇÕES DA DI PARA ENVIO DA NOTA FISCAL: " & ex.Message() & "----------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
      writer.WriteStartElement("ERRO")
      writer.WriteString("ERRO AO LISTAR AS ADIÇÕES DA DI PARA ENVIO DA NOTA FISCAL: " & ex.Message() & "----------" & ex.StackTrace())
      writer.WriteEndElement()
    End Try
  End Sub

  ''' <summary>
  ''' Grupo I80. Rastreabilidade de produto
  ''' </summary>
  Private Sub ListaRastreabilidade(ByVal writer As XmlTextWriter, ByVal nItem As Integer)
    Dim rastreab As New clsNFeItemRastreabilidade
    Dim dv As New DataView
    Dim ajuste As New clsAjuste
    Dim qLote As Decimal

    Try
      dv = rastreab.ListaRastreabilidade(Me.id_nf, nItem)

      If dv.Count > 0 Then
        writer.WriteStartElement("rastro")

        For Each drv As DataRowView In dv
          writer.WriteStartElement("nLote")
          writer.WriteString(drv(1))
          writer.WriteEndElement()

          qLote = FormatNumber(drv(2), 2)
          writer.WriteStartElement("qLote")
          writer.WriteString(qLote.ToString("0.000").Replace(",", "."))
          writer.WriteEndElement()

          writer.WriteStartElement("dFab")
          writer.WriteString(ajuste.AMD(drv(3)))
          writer.WriteEndElement()

          writer.WriteStartElement("dVal")
          writer.WriteString(ajuste.AMD(drv(4)))
          writer.WriteEndElement()

          If drv(5) <> "" Then
            writer.WriteStartElement("cAgreg")
            writer.WriteString(drv(5))
            writer.WriteEndElement()
          End If
        Next

        writer.WriteEndElement() 'FIM rastro	
      End If

    Catch ex As Exception
      writer.WriteStartElement("ERRO")
      writer.WriteString("ERRO AO PEGAR AS INFORMAÇÕES DA RASTREABILIDADE PARA ENVIO DA NFE: " & ex.Message() & "-----------" & ex.StackTrace())
      writer.WriteEndElement()
    End Try
  End Sub

  ''' <summary>
  ''' LA. Detalhamento Específico de Combustíveis
  ''' </summary>
  ''' <param name="writer"></param>
  ''' <param name="nItem"></param>
  Private Sub ListaCombustivel(ByVal writer As XmlTextWriter, ByVal nItem As Integer)
    Dim combustivel As New clsNfeItemCombustivel	
		
    If combustivel.PegaCombustivelUmItemNfe(Me.id_nf, nItem) Then
      Dim pGLP, pGNn, pGNi, qTemp, vAliqProd, vCIDE As Decimal

      pGLP = combustivel.pGLP
      pGNn = combustivel.pGNn
      pGNi = combustivel.pGNi
      qTemp = combustivel.qTemp
      vAliqProd = combustivel.vAliqProd
      vCIDE = combustivel.vCIDE
	  
      writer.WriteStartElement("comb")

      writer.WriteStartElement("cProdANP")
      writer.WriteString(combustivel.cProdANP)
      writer.WriteEndElement()

      'writer.WriteStartElement("pMixGN") NÃO TEM MAIS NA VERSÃO 4.0
      'writer.WriteString(combustivel.pMixGN.ToString("0.00").Replace(",", "."))
      'writer.WriteEndElement()

      writer.WriteStartElement("descANP")
      writer.WriteString(combustivel.descANP)
      writer.WriteEndElement()
	  
      If pGLP > 0 Then
        writer.WriteStartElement("pGLP")
        writer.WriteString(pGLP.ToString("0.00").Replace(",", "."))
        writer.WriteEndElement()
      End If

      If pGNn > 0 Then
        writer.WriteStartElement("pGNn")
        writer.WriteString(pGNn.ToString("0.00").Replace(",", "."))
        writer.WriteEndElement()
      End If

      If pGNi > 0 Then
        writer.WriteStartElement("pGNi")
        writer.WriteString(pGNi.ToString("0.00").Replace(",", "."))
        writer.WriteEndElement()
      End If

      writer.WriteStartElement("vPart")
      writer.WriteString(combustivel.vPart.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("CODIF")
      writer.WriteString(combustivel.CODIF)
      writer.WriteEndElement()

      If qTemp > 0 Then
        writer.WriteStartElement("qTemp")
        writer.WriteString(qTemp.ToString("0.00").Replace(",", "."))
        writer.WriteEndElement()
      End If

      writer.WriteStartElement("UFCons")
      writer.WriteString(combustivel.UFCons)
      writer.WriteEndElement()

	 
      If vAliqProd > 0 Or vCIDE > 0 Then
        writer.WriteStartElement("CIDE")

        writer.WriteStartElement("qBCProd")
        writer.WriteString(combustivel.qBcProd.ToString("0.00").Replace(",", "."))
        writer.WriteEndElement()

        writer.WriteStartElement("vAliqProd")
        writer.WriteString(vAliqProd.ToString().Replace(",", "."))
        writer.WriteEndElement()

        writer.WriteStartElement("vCIDE")
        writer.WriteString(vCIDE.ToString("0.00").Replace(",", "."))
        writer.WriteEndElement()

        writer.WriteEndElement() 'FIM CIDE	  
      End If

      writer.WriteEndElement() 'FIM comb
	  
    End If
  End Sub

  ''' <summary>
  ''' M. Tributos incidentes no Produto ou Serviço
  ''' </summary>
  ''' <param name="writer"></param>
  ''' <param name="nItem"></param>
  Private Sub ListaImposto(ByVal writer As XmlTextWriter, ByVal nItem As Integer, ByVal vTotTrib as Decimal)
    writer.WriteStartElement("imposto")
	
	If vTotTrib > 0 Then
	  writer.WriteStartElement("vTotTrib")
      writer.WriteString(vTotTrib.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()
	End If
		
    ListaICMS(writer, nItem) 'Lista o ICMS
    ListaIPI(writer, nItem) 'IPI - Imposto sobre Produtos Industrializados
    ListaII(writer, nItem) 'P. Imposto de Importação
    ListaPIS(writer, nItem) 'Q. PIS
    ListaCofins(writer, nItem) 'S. COFINS
    ListaISSQN(writer, nItem) 'U. ISSQN
    ListaICMSUFDest(writer, nItem) 'NA. ICMS para a UF de destino    

    writer.WriteEndElement() 'FIM imposto	

    ImpostoDevol(writer, nItem) ' UB. Tributos Devolvidos (para o item da NF-e)
  End Sub

  ''' <summary>
  ''' Norma referenciada, informações complementares, etc.
  ''' </summary>
  ''' <param name="writer"></param>
  ''' <param name="nItem"></param>
  Private Sub infAdProd(ByVal writer As XmlTextWriter, ByVal nItem As Integer)
    Dim inf_adc As New clsNfeItem

    Try
      inf_adc.ListaItemAbaDadosNfeItem(Me.id_nf, nItem)

      If inf_adc.infAdProd <> "" Then
        writer.WriteStartElement("infAdProd")
        writer.WriteString(inf_adc.infAdProd)
        writer.WriteEndElement()
		
      End If
    Catch ex As Exception
      'MsgBox("ERRO AO PEGAR AS INFORMAÇÕES ADICIONAIS DO ITEM PARA ENVIO DA NF: " & ex.Message() & "---------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
      writer.WriteStartElement("ERRO")
      writer.WriteString("ERRO AO PEGAR AS INFORMAÇÕES ADICIONAIS DO ITEM PARA ENVIO DA NF: " & ex.Message() & "---------" & ex.StackTrace())
      writer.WriteEndElement()
    End Try
  End Sub

  ''' <summary>
  ''' Lista o ICMS dos itens...
  ''' </summary>
  ''' <param name="writer"></param>
  ''' <param name="nItem"></param>
  Private Sub ListaICMS(ByVal writer As XmlTextWriter, ByVal nItem As Integer)
    Dim item As New clsNfeItem
    Dim icms_interest As New clsNfeItemIcmsDest

    Try
      item.ListaAbaTributosNfeItemICMS(Me.id_nf, nItem)
      icms_interest.ListaAbaTributosNfeItemIcmsInterest(Me.id_nf, nItem)

      writer.WriteStartElement("ICMS")

      Select Case item.ICMS_CST
        Case 0
          ICMS00(writer, item.ICMS_orig, item.ICMS_modBC, item.ICMS_vBC, item.ICMS_pICMS, item.ICMS_vICMS, icms_interest.pFCPUFDest, icms_interest.vFCPUFDest)
        Case 10
          ICMS10(writer, item.ICMS_orig, item.ICMS_modBC, item.ICMS_vBC, item.ICMS_pICMS, item.ICMS_vICMS, item.ICMS_modBCST, item.ICMS_pMVAST, item.ICMS_pRedBCST, item.ICMS_vBCST, item.ICMS_pICMSST, item.ICMS_vICMSST, icms_interest.vBCFCPST, icms_interest.pFCPST, icms_interest.vFCPST)
        Case 20
          ICMS20(writer, item.ICMS_orig, item.ICMS_modBC, item.ICMS_pRedBC, item.ICMS_vBC, item.ICMS_pICMS, item.ICMS_vICMS, item.ICMS_vICMSDeson, item.ICMS_motDesICMS, icms_interest.vBCUFDest, icms_interest.pFCPUFDest, icms_interest.vFCPUFDest)
        Case 30
          ICMS30(writer, item.ICMS_orig, item.ICMS_modBC, item.ICMS_pMVAST, item.ICMS_pRedBCST, item.ICMS_vBCST, item.ICMS_pICMSST, item.ICMS_vICMSST, item.ICMS_vICMSDeson, item.ICMS_motDesICMS, icms_interest.vBCFCPST, icms_interest.pFCPST, icms_interest.vFCPST)
        Case 40, 41, 50
          ICMS40(writer, item.ICMS_orig, item.ICMS_CST, item.ICMS_vICMSDeson, item.ICMS_motDesICMS)
        Case 51
          ICMS51(writer, item.ICMS_orig, item.ICMS_modBC, item.ICMS_pRedBC, item.ICMS_vBC, item.ICMS_pICMS, item.ICMS_vICMS, item.ICMS_vICMSOp, item.ICMS_pDif, item.ICMS_vICMSDif, icms_interest.vBCUFDest, icms_interest.pFCPUFDest, icms_interest.vFCPUFDest)
        Case 60
          Dim combustivel As New clsNfeItemCombustivel

          If Not combustivel.PegaCombustivelUmItemNfe(Me.id_nf, nItem) Then 'Não tem informações de combustível
            ICMS60(writer, item.ICMS_orig, item.ICMS_vBCSTRet, item.ICMS_vICMSSTRet, item.ICMS_pICMSST)
          Else 'N10b - Grupo de Repasse do ICMS ST
            ICMSST(writer, item.ICMS_orig, item.ICMS_CST, item.ICMS_vBCSTRet, item.ICMS_vICMSSTRet, item.ICMS_vBCSTDest, item.ICMS_vICMSSTDest)
          End If
        Case 70
          ICMS70(writer, item.ICMS_orig, item.ICMS_modBC, item.ICMS_pRedBC, item.ICMS_vBC, item.ICMS_pICMS, item.ICMS_vICMS, item.ICMS_modBCST, item.ICMS_pMVAST, item.ICMS_pRedBCST, item.ICMS_vBCST, item.ICMS_pICMSST, item.ICMS_vICMSST, item.ICMS_vICMSDeson, item.ICMS_motDesICMS, icms_interest.vBCFCPST, icms_interest.pFCPST, icms_interest.vFCPST)
        Case 90
          ICMS90(writer, item.ICMS_orig, item.ICMS_modBC, item.ICMS_vBC, item.ICMS_pRedBC, item.ICMS_pICMS, item.ICMS_vICMS, item.ICMS_modBCST, item.ICMS_pMVAST, item.ICMS_pRedBCST, item.ICMS_vBCST, item.ICMS_pICMSST, item.ICMS_vICMSST, item.ICMS_vICMSDeson, item.ICMS_motDesICMS, icms_interest.vBCUFDest, icms_interest.pFCPUFDest, icms_interest.vFCPUFDest, icms_interest.vBCFCPST, icms_interest.pFCPST, icms_interest.vFCPST)
      End Select

      'N10a ICMSPart

      Select Case item.ICMS_CST
        Case 101
          ICMSSN101(writer, item.ICMS_orig, item.ICMS_pCredSN, item.ICMS_vCredICMSSN)
        Case 102, 103, 300, 400
          ICMSSN102(writer, item.ICMS_orig, item.ICMS_CST)
        Case 201
          ICMSSN201(writer, item.ICMS_orig, item.ICMS_modBCST, item.ICMS_pMVAST, item.ICMS_pRedBCST, item.ICMS_vBCST, item.ICMS_pICMSST, item.ICMS_vICMSST, item.ICMS_pCredSN, item.ICMS_vCredICMSSN, icms_interest.vBCFCPST, icms_interest.pFCPST, icms_interest.vFCPST)
        Case 202, 203
          ICMSSN202(writer, item.ICMS_orig, item.ICMS_CST, item.ICMS_modBCST, item.ICMS_pMVAST, item.ICMS_pRedBCST, item.ICMS_vBCST, item.ICMS_pICMSST, item.ICMS_vICMSST, icms_interest.vBCFCPST, icms_interest.pFCPST, icms_interest.vFCPST)
        Case 500
          ICMSSN500(writer, item.ICMS_orig, item.ICMS_vBCSTRet, item.ICMS_vICMSSTRet, icms_interest.vBCFCPST, icms_interest.pFCPST, icms_interest.vFCPST, item.ICMS_pICMSST)
        Case 900
          ICMSSN900(writer, item.ICMS_orig, item.ICMS_modBC, item.ICMS_vBC, item.ICMS_pRedBC, item.ICMS_pICMS, item.ICMS_vICMS, item.ICMS_modBCST, item.ICMS_pMVAST, item.ICMS_pRedBCST, item.ICMS_vBCST, item.ICMS_pICMSST, item.ICMS_vICMSST, item.ICMS_pCredSN, item.ICMS_vCredICMSSN, icms_interest.vBCFCPST, icms_interest.pFCPST, icms_interest.vFCPST)
      End Select

      writer.WriteEndElement() 'FIM ICMS
    Catch ex As Exception
      'MsgBox("ERRO AO PEGAR AS INFORMAÇÕES DOS IMPOSTOS DO ITEM AO TRANSMITIR A NF: " & ex.Message() & "----------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
      writer.WriteStartElement("ERRO")
      writer.WriteString("ERRO AO PEGAR AS INFORMAÇÕES DOS IMPOSTOS DO ITEM AO TRANSMITIR A NF: " & ex.Message() & "----------" & ex.StackTrace())
      writer.WriteEndElement()
    End Try
  End Sub

  Private Sub ListaIPI(ByVal writer As XmlTextWriter, ByVal nItem As Integer)
    Dim ipi As New clsNfeItem

    Try
      ipi.ListaAbaTributosNfeItemIPI(Me.id_nf, nItem)

      If ipi.IPI_CST >= 0 Then 'Existe o CST -1... onde não é informado no XML...
        writer.WriteStartElement("IPI")

        If ipi.IPI_clEnq <> "" Then
          writer.WriteStartElement("clEnq")
          writer.WriteString(ipi.IPI_clEnq)
          writer.WriteEndElement()
        End If

        If ipi.IPI_CNPJProd <> "0" And ipi.IPI_CNPJProd <> "" Then
          writer.WriteStartElement("CNPJProd")
          writer.WriteString(ipi.IPI_CNPJProd)
          writer.WriteEndElement()
        End If

        If ipi.IPI_cSelo <> "" Then
          writer.WriteStartElement("cSelo")
          writer.WriteString(ipi.IPI_cSelo)
          writer.WriteEndElement()
        End If

        If ipi.IPI_qSelo > 0 Then
          writer.WriteStartElement("qSelo")
          writer.WriteString(ipi.IPI_qSelo)
          writer.WriteEndElement()
        End If

        writer.WriteStartElement("cEnq")
        writer.WriteString(ipi.IPI_cEnq)
        writer.WriteEndElement()

        Select Case ipi.IPI_CST
          Case 0, 49, 50, 99
            IPITrib(writer, ipi.IPI_CST, ipi.IPI_vBC, ipi.IPI_pIPI, ipi.IPI_qUnid, ipi.IPI_vUnid, ipi.IPI_vIPI)
          Case 1, 2, 3, 4, 51, 52, 53, 54, 55
            IPINT(writer, ipi.IPI_CST)
        End Select

        writer.WriteEndElement() 'FIM IPI
		
      End If
    Catch ex As Exception
      'MsgBox("ERRO AO PEGAR AS INFORMAÇÕES DO IPI PARA TRANSMISSÃO DA NF: " & ex.Message() & "-------------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
      writer.WriteStartElement("ERRO")
      writer.WriteString("ERRO AO PEGAR AS INFORMAÇÕES DO IPI PARA TRANSMISSÃO DA NF: " & ex.Message() & "-------------" & ex.StackTrace())
      writer.WriteEndElement()
    End Try
  End Sub

  Private Sub ListaII(ByVal writer As XmlTextWriter, ByVal nItem As Integer)
    Dim II As New clsNfeItem

    Try
      II.ListaAbaTributosNfeItemII(Me.id_nf, nItem)

      If II.II_vBC > 0 Or II.II_vDespAdu > 0 Or II.II_vII > 0 Or II.II_vIOF > 0 Then
        writer.WriteStartElement("II")

        writer.WriteStartElement("vBC")
        writer.WriteString(II.II_vBC.ToString("0.00").Replace(",", "."))
        writer.WriteEndElement()

        writer.WriteStartElement("vDespAdu")
        writer.WriteString(II.II_vDespAdu.ToString("0.00").Replace(",", "."))
        writer.WriteEndElement()

        writer.WriteStartElement("vII")
        writer.WriteString(II.II_vII.ToString("0.00").Replace(",", "."))
        writer.WriteEndElement()

        writer.WriteStartElement("vIOF")
        writer.WriteString(II.II_vIOF.ToString("0.00").Replace(",", "."))
        writer.WriteEndElement()

        writer.WriteEndElement() 'FIM II
		
      End If
    Catch ex As Exception
      'MsgBox("ERRO AO PEGAR AS INFORMAÇÕES DO II PARA ENVIO DA NF: " & ex.Message() & "-------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
      writer.WriteStartElement("ERRO")
      writer.WriteString("ERRO AO PEGAR AS INFORMAÇÕES DO II PARA ENVIO DA NF: " & ex.Message() & "-------" & ex.StackTrace())
      writer.WriteEndElement()
    End Try
  End Sub

  Private Sub ListaPIS(ByVal writer As XmlTextWriter, ByVal nItem As Integer)
    Dim pis As New clsNfeItem

    Try
      pis.ListaAbaTributosNfeItemPIS(Me.id_nf, nItem)

      writer.WriteStartElement("PIS")

      Select Case pis.PIS_CST
        Case 1, 2
          PISAliq(writer, pis.PIS_CST, pis.PIS_vBC, pis.PIS_pPIS, pis.PIS_vPIS)
        Case 3
          PISQtde(writer, pis.PIS_qBCProd, pis.PIS_vAliqProd, pis.PIS_vPIS)
        Case 4, 5, 6, 7, 8, 9
          PISNT(writer, pis.PIS_CST)
        Case 49, 50, 51, 52, 53, 54, 55, 56, 60, 61, 62, 63, 64, 65, 66, 67, 70, 71, 72, 73, 74, 75, 98, 99
          PISOutr(writer, pis.PIS_CST, pis.PIS_vBC, pis.PIS_pPIS, pis.PIS_qBCProd, pis.PIS_vAliqProd, pis.PIS_vPIS)
      End Select

      writer.WriteEndElement() 'FIM PIS
	  

      If pis.PISST_vPIS > 0 Then 'R. PIS ST
        PISST(writer, pis.PISST_vBC, pis.PISST_pPIS, pis.PISST_qBCProd, pis.PISST_vAliqProd, pis.PISST_vPIS)
      End If

    Catch ex As Exception
      'MsgBox("ERRO AO PEGAR AS INFORMAÇÕES DO PIS PARA ENVIO DA NF: " & ex.Message() & "--------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
      writer.WriteStartElement("ERRO")
      writer.WriteString("ERRO AO PEGAR AS INFORMAÇÕES DO PIS PARA ENVIO DA NF: " & ex.Message() & "--------" & ex.StackTrace())
      writer.WriteEndElement()
    End Try
  End Sub

  Private Sub ListaCofins(ByVal writer As XmlTextWriter, ByVal nItem As Integer)
    Dim cofins As New clsNfeItem

    Try
      cofins.ListaAbaTributosNfeItemCOFINS(Me.id_nf, nItem)

      writer.WriteStartElement("COFINS")

      Select Case cofins.COFINS_CST
        Case 1, 2
          COFINSAliq(writer, cofins.COFINS_CST, cofins.COFINS_vBC, cofins.COFINS_pCOFINS, cofins.COFINS_vCOFINS)
        Case 3
          COFINSQtde(writer, cofins.COFINS_qBCProd, cofins.COFINS_vAliqProd, cofins.COFINS_vCOFINS)
        Case 4, 5, 6, 7, 8, 9
          COFINSNT(writer, cofins.COFINS_CST)
        Case 49, 50, 51, 52, 53, 54, 55, 56, 60, 61, 62, 63, 64, 65, 66, 67, 70, 71, 72, 73, 74, 75, 98, 99
          COFINSOutr(writer, cofins.COFINS_CST, cofins.COFINS_vBC, cofins.COFINS_pCOFINS, cofins.COFINS_qBCProd, cofins.COFINS_vAliqProd, cofins.COFINS_vCOFINS)
      End Select

      writer.WriteEndElement() 'FIM COFINS
	  

      If cofins.COFINSST_vCOFINS > 0 Then 'T. COFINS ST
        COFINSST(writer, cofins.COFINSST_vBC, cofins.COFINSST_pCOFINS, cofins.COFINSST_qBCProd, cofins.COFINSST_vAliqProd, cofins.COFINSST_vCOFINS)
      End If
    Catch ex As Exception
      'MsgBox("ERRO AO PEGAR AS INFORMAÇÕES DO COFINS PARA ENVIO DA NF: " & ex.Message() & "-----------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
      writer.WriteStartElement("ERRO")
      writer.WriteString("ERRO AO PEGAR AS INFORMAÇÕES DO COFINS PARA ENVIO DA NF: " & ex.Message() & "-----------" & ex.StackTrace())
      writer.WriteEndElement()
    End Try
  End Sub

  ''' <summary>
  ''' Campos para cálculo do ISSQN na NF-e conjugada, onde há a prestação de serviços sujeitos ao ISSQN e
  ''' fornecimento de peças sujeitas ao ICMS. Grupo ISSQN é mutuamente exclusivo com os grupos
  ''' Grupo ISSQN é mutuamente exclusivo com os grupos ICMS, IPI e II, isto é se ISSQN for informado os grupos
  ''' ICMS, IPI e II não serão informados e vice-versa (v2.0). 
  ''' </summary>
  ''' <param name="writer"></param>
  ''' <param name="nItem"></param>
  Private Sub ListaISSQN(ByVal writer As XmlTextWriter, ByVal nItem As Integer)
    Dim issqn As New clsNfeItem

    Try
      issqn.ListaAbaTributosNfeItemISSQN(Me.id_nf, nItem)

      If issqn.ISSQN_vISSQN > 0 Then
        writer.WriteStartElement("ISSQN")

        writer.WriteStartElement("vBC")
        writer.WriteString(issqn.ISSQN_vBC.ToString("0.00").Replace(",", "."))
        writer.WriteEndElement()

        writer.WriteStartElement("vAliq")
        writer.WriteString(issqn.ISSQN_vAliq.ToString("0.00").Replace(",", "."))
        writer.WriteEndElement()

        writer.WriteStartElement("vISSQN")
        writer.WriteString(issqn.ISSQN_vISSQN.ToString("0.00").Replace(",", "."))
        writer.WriteEndElement()

        writer.WriteStartElement("cMunFG")
        writer.WriteString(issqn.ISSQN_cMunFG)
        writer.WriteEndElement()

        writer.WriteStartElement("cListServ")
        writer.WriteString(issqn.ISSQN_cListServ)
        writer.WriteEndElement()

        writer.WriteEndElement()
		
      End If
    Catch ex As Exception
      'MsgBox("ERRO AO PEGAR AS INFORMAÇÕES DO ISSQN PARA ENVIO DA NF: " & ex.Message() & "-----------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
      writer.WriteStartElement("ERRO")
      writer.WriteString("ERRO AO PEGAR AS INFORMAÇÕES DO ISSQN PARA ENVIO DA NF: " & ex.Message() & "-----------" & ex.StackTrace())
      writer.WriteEndElement()
    End Try
  End Sub

  Private Sub ListaICMSUFDest(ByVal writer As XmlTextWriter, ByVal nItem As Integer)
    Dim icms_inter As New clsNfeItemIcmsDest

    Try
      icms_inter.ListaAbaTributosNfeItemIcmsInterest(Me.id_nf, nItem)

      If icms_inter.vBCUFDest > 0 Then
        writer.WriteStartElement("ICMSUFDest")

        writer.WriteStartElement("vBCUFDest")
        writer.WriteString(icms_inter.vBCUFDest.ToString("0.00").Replace(",", "."))
        writer.WriteEndElement()

        writer.WriteStartElement("vBCFCPUFDest")
        writer.WriteString(icms_inter.vBCUFDest.ToString("0.00").Replace(",", "."))
        writer.WriteEndElement()

        writer.WriteStartElement("pFCPUFDest")
        writer.WriteString(icms_inter.pFCPUFDest.ToString("0.00").Replace(",", "."))
        writer.WriteEndElement()

        writer.WriteStartElement("pICMSUFDest")
        writer.WriteString(icms_inter.pICMSUFDest.ToString("0.00").Replace(",", "."))
        writer.WriteEndElement()

        writer.WriteStartElement("pICMSInter")
        writer.WriteString(icms_inter.pICMSInter.ToString("0.00").Replace(",", "."))
        writer.WriteEndElement()

        writer.WriteStartElement("pICMSInterPart")
        writer.WriteString(icms_inter.pICMSInterPart.ToString("0.00").Replace(",", "."))
        writer.WriteEndElement()

        writer.WriteStartElement("vFCPUFDest")
        writer.WriteString(icms_inter.vFCPUFDest.ToString("0.00").Replace(",", "."))
        writer.WriteEndElement()

        writer.WriteStartElement("vICMSUFDest")
        writer.WriteString(icms_inter.vICMSUFDest.ToString("0.00").Replace(",", "."))
        writer.WriteEndElement()

        writer.WriteStartElement("vICMSUFRemet")
        writer.WriteString(icms_inter.vICMSUFRemet.ToString("0.00").Replace(",", "."))
        writer.WriteEndElement()

        writer.WriteEndElement() 'FIM ICMSUFDest
		
      End If

    Catch ex As Exception
      'MsgBox("ERRO AO PEGAR AS INFORMAÇÕES DO ICMS INTERESTADUAL PARA ENVIO DA NFE: " & ex.Message() & "----------" & ex.Message(), MsgBoxStyle.Critical, "Maxcont")
      writer.WriteStartElement("ERRO")
      writer.WriteString("ERRO AO PEGAR AS INFORMAÇÕES DO ICMS INTERESTADUAL PARA ENVIO DA NFE: " & ex.Message() & "----------" & ex.Message())
      writer.WriteEndElement()
    End Try
  End Sub

  Private Sub ImpostoDevol(ByVal writer As XmlTextWriter, ByVal nItem As Integer)
    If Me.finNFe = 4 Then 'FINALIDADE DE EMISSÃO 4=DEVOLUÇÃO DE MERCADORIA
      Try
        Dim ipi_devolvido As New clsNfeItem

        ipi_devolvido.ListaAbaTributosNFeItemIPIDevolvido(Me.id_nf, nItem)

        If ipi_devolvido.IPI_pDevol > 0 Or ipi_devolvido.IPI_vIPIDevol > 0 Then
          writer.WriteStartElement("impostoDevol")

          writer.WriteStartElement("pDevol")
          writer.WriteString(ipi_devolvido.IPI_pDevol.ToString("0.00").Replace(",", "."))
          writer.WriteEndElement()


          writer.WriteStartElement("IPI")

          writer.WriteStartElement("vIPIDevol")
          writer.WriteString(ipi_devolvido.IPI_vIPIDevol.ToString("0.00").Replace(",", "."))
          writer.WriteEndElement()

          writer.WriteEndElement() 'FIM IPI


          writer.WriteEndElement() 'FIM impostoDevol
        End If
      Catch ex As Exception
        writer.WriteStartElement("ERRO")
        writer.WriteString("ERRO AO PEGAR O IMPOSTO DEVOLVIDO" & ex.Message() & "--------" & ex.StackTrace())
        writer.WriteEndElement()
      End Try
    End If
  End Sub

  Private Sub ICMS00(ByVal writer As XmlTextWriter, ByVal ICMS_orig As Integer, ByVal ICMS_modBC As Integer, ByVal ICMS_vBC As Decimal, ByVal ICMS_pICMS As Decimal, ByVal ICMS_vICMS As Decimal, _
                       ByVal pFCPUFDest As Decimal, ByVal vFCPUFDest As Decimal)
    writer.WriteStartElement("ICMS00")

    writer.WriteStartElement("orig")
    writer.WriteString(ICMS_orig)
    writer.WriteEndElement()

    writer.WriteStartElement("CST")
    writer.WriteString("00")
    writer.WriteEndElement()

    writer.WriteStartElement("modBC")
    writer.WriteString(ICMS_modBC)
    writer.WriteEndElement()

    writer.WriteStartElement("vBC")
    writer.WriteString(ICMS_vBC.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("pICMS")
    writer.WriteString(ICMS_pICMS.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("vICMS")
    writer.WriteString(ICMS_vICMS.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()
	
    'If pFCPUFDest > 0 Then
      'writer.WriteStartElement("pFCP")
      'writer.WriteString(pFCPUFDest.ToString("0.00").Replace(",", "."))
      'writer.WriteEndElement()

      'writer.WriteStartElement("vFCP")
      'writer.WriteString(vFCPUFDest.ToString("0.00").Replace(",", "."))
      'writer.WriteEndElement()
    'End If
    

    writer.WriteEndElement() 'FIM ICMS00	
  End Sub

  Private Sub ICMS10(ByVal writer As XmlTextWriter, ByVal ICMS_orig As Integer, ByVal ICMS_modBC As Integer, ByVal ICMS_vBC As Decimal, _
                       ByVal ICMS_pICMS As Decimal, ByVal ICMS_vICMS As Decimal, ByVal ICMS_modBCST As Integer, ByVal ICMS_pMVAST As Decimal, ByVal ICMS_pRedBCST As Decimal, _
                       ByVal ICMS_vBCST As Decimal, ByVal ICMS_pICMSST As Decimal, ByVal ICMS_vICMSST As Decimal, ByVal vBCUFDestST As Decimal, ByVal pFCPUFDestST As Decimal, _
                       ByVal vFCPUFDestST As Decimal)

    writer.WriteStartElement("ICMS10")

    writer.WriteStartElement("orig")
    writer.WriteString(ICMS_orig)
    writer.WriteEndElement()

    writer.WriteStartElement("CST")
    writer.WriteString("10")
    writer.WriteEndElement()

    writer.WriteStartElement("modBC")
    writer.WriteString(ICMS_modBC)
    writer.WriteEndElement()

    writer.WriteStartElement("vBC")
    writer.WriteString(ICMS_vBC.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("pICMS")
    writer.WriteString(ICMS_pICMS.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("vICMS")
    writer.WriteString(ICMS_vICMS.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("modBCST")
    writer.WriteString(ICMS_modBCST)
    writer.WriteEndElement()

    writer.WriteStartElement("pMVAST")
    writer.WriteString(ICMS_pMVAST.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("pRedBCST")
    writer.WriteString(ICMS_pRedBCST.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("vBCST")
    writer.WriteString(ICMS_vBCST.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("pICMSST")
    writer.WriteString(ICMS_pICMSST.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("vICMSST")
    writer.WriteString(ICMS_vICMSST.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    If vFCPUFDestST > 0 Then
      writer.WriteStartElement("vBCFCPST")
      writer.WriteString(vBCUFDestST.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("pFCPST")
      writer.WriteString(pFCPUFDestST.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vFCPST")
      writer.WriteString(vFCPUFDestST.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()
    End If

    writer.WriteEndElement() 'FIM ICMS10
  End Sub

  Private Sub ICMS20(ByVal writer As XmlTextWriter, ByVal ICMS_orig As Integer, ByVal ICMS_modBC As Integer, ByVal ICMS_pRedBC As Decimal, _
                       ByVal ICMS_vBC As Decimal, ByVal ICMS_pICMS As Decimal, ByVal ICMS_vICMS As Decimal, ByVal ICMS_vICMSDeson As Decimal, _
                       ByVal ICMS_motDesICMS As Integer, ByVal vBCUFDest As Decimal, ByVal pFCPUFDest As Decimal, ByVal vFCPUFDest As Decimal)
    writer.WriteStartElement("ICMS20")

    writer.WriteStartElement("orig")
    writer.WriteString(ICMS_orig)
    writer.WriteEndElement()

    writer.WriteStartElement("CST")
    writer.WriteString("20")
    writer.WriteEndElement()

    writer.WriteStartElement("modBC")
    writer.WriteString(ICMS_modBC)
    writer.WriteEndElement()

    writer.WriteStartElement("pRedBC")
    writer.WriteString(ICMS_pRedBC.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("vBC")
    writer.WriteString(ICMS_vBC.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("pICMS")
    writer.WriteString(ICMS_pICMS.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("vICMS")
    writer.WriteString(ICMS_vICMS.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

	If vFCPUFDest > 0 Then
	  writer.WriteStartElement("vBCFCP")
      writer.WriteString(vBCUFDest.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("pFCP")
      writer.WriteString(pFCPUFDest.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vFCP")
      writer.WriteString(vFCPUFDest.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()
	End If    

    If ICMS_vICMSDeson > 0 Then
      writer.WriteStartElement("vICMSDeson")
      writer.WriteString(ICMS_vICMSDeson.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("motDesICMS")
      writer.WriteString(ICMS_motDesICMS)
      writer.WriteEndElement()
    End If

    writer.WriteEndElement() 'FIM ICMS20	
  End Sub

  Private Sub ICMS30(ByVal writer As XmlTextWriter, ByVal ICMS_orig As Integer, ByVal ICMS_modBC As Integer, ByVal ICMS_pMVAST As Decimal, _
                       ByVal ICMS_pRedBCST As Decimal, ByVal ICMS_vBCST As Decimal, ByVal ICMS_pICMSST As Decimal, ByVal ICMS_vICMSST As Decimal, _
                       ByVal ICMS_vICMSDeson As Decimal, ByVal ICMS_motDesICMS As Integer, ByVal vBCUFDestST As Decimal, ByVal pFCPUFDestST As Decimal, _
                       ByVal vFCPUFDestST As Decimal)
    writer.WriteStartElement("ICMS30")

    writer.WriteStartElement("orig")
    writer.WriteString(ICMS_orig)
    writer.WriteEndElement()

    writer.WriteStartElement("CST")
    writer.WriteString("30")
    writer.WriteEndElement()

    writer.WriteStartElement("modBCST")
    writer.WriteString(ICMS_modBC)
    writer.WriteEndElement()

    writer.WriteStartElement("pMVAST")
    writer.WriteString(ICMS_pMVAST.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("pRedBCST")
    writer.WriteString(ICMS_pRedBCST.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("vBCST")
    writer.WriteString(ICMS_vBCST.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("pICMSST")
    writer.WriteString(ICMS_pICMSST.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("vICMSST")
    writer.WriteString(ICMS_vICMSST.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    If vFCPUFDestST > 0 Then
      writer.WriteStartElement("vBCFCPST")
      writer.WriteString(vBCUFDestST.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("pFCPST")
      writer.WriteString(pFCPUFDestST.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vFCPST")
      writer.WriteString(vFCPUFDestST.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()
    End If

    If ICMS_vICMSDeson > 0 Then
      writer.WriteStartElement("vICMSDeson")
      writer.WriteString(ICMS_vICMSDeson.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("motDesICMS")
      writer.WriteString(ICMS_motDesICMS)
      writer.WriteEndElement()	  
    End If

    writer.WriteEndElement() 'FIM ICMS30	
  End Sub

  Private Sub ICMS40(ByVal writer As XmlTextWriter, ByVal ICMS_orig As Integer, ByVal ICMS_CST As Integer, ByVal ICMS_vICMSDeson As Decimal, ByVal ICMS_motDesICMS As Integer)
    writer.WriteStartElement("ICMS40")

    writer.WriteStartElement("orig")
    writer.WriteString(ICMS_orig)
    writer.WriteEndElement()

    writer.WriteStartElement("CST")
    writer.WriteString(ICMS_CST)
    writer.WriteEndElement()

    If ICMS_vICMSDeson > 0 Then
      writer.WriteStartElement("vICMSDeson")
      writer.WriteString(ICMS_vICMSDeson.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("motDesICMS")
      writer.WriteString(ICMS_motDesICMS)
      writer.WriteEndElement()
    End If

    writer.WriteEndElement() 'FIM ICMS40	
  End Sub

  Private Sub ICMS51(ByVal writer As XmlTextWriter, ByVal ICMS_orig As Integer, ByVal ICMS_modBC As Integer, ByVal ICMS_pRedBC As Decimal, _
                       ByVal ICMS_vBC As Decimal, ByVal ICMS_pICMS As Decimal, ByVal ICMS_vICMS As Decimal, ByVal ICMS_vICMSOp As Decimal, _
                       ByVal ICMS_pDif As Decimal, ByVal ICMS_vICMSDif As Decimal, ByVal vBCUFDest As Decimal, ByVal pFCPUFDest As Decimal, _
                       ByVal vFCPUFDest As Decimal)
    writer.WriteStartElement("ICMS51")

    writer.WriteStartElement("orig")
    writer.WriteString(ICMS_orig)
    writer.WriteEndElement()

    writer.WriteStartElement("CST")
    writer.WriteString("51")
    writer.WriteEndElement()

    writer.WriteStartElement("modBC")
    writer.WriteString(ICMS_modBC)
    writer.WriteEndElement()

    writer.WriteStartElement("pRedBC")
    writer.WriteString(ICMS_pRedBC.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("vBC")
    writer.WriteString(ICMS_vBC.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("pICMS")
    writer.WriteString(ICMS_pICMS.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    If ICMS_vICMSOp > 0 Or ICMS_pDif > 0 Then
      writer.WriteStartElement("vICMSOp")
      writer.WriteString(ICMS_vICMSOp.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("pDif")
      writer.WriteString(ICMS_pDif.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vICMSDif")
      writer.WriteString(ICMS_vICMSDif.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()
    End If

    writer.WriteStartElement("vICMS")
    writer.WriteString(ICMS_vICMS.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()
	
	If vFCPUFDest > 0 Or pFCPUFDest > 0 Then
	  writer.WriteStartElement("vBCFCP")
      writer.WriteString(vBCUFDest.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("pFCP")
      writer.WriteString(pFCPUFDest.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vFCP")
      writer.WriteString(vFCPUFDest.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()
	End If    

    writer.WriteEndElement() 'FIM ICMS51
	
  End Sub

  Private Sub ICMS60(ByVal writer As XmlTextWriter, ByVal ICMS_orig As Integer, ByVal ICMS_vBCSTRet As Decimal, ByVal ICMS_vICMSSTRet As Decimal, _
                     ByVal pST As Decimal)
    writer.WriteStartElement("ICMS60")

    writer.WriteStartElement("orig")
    writer.WriteString(ICMS_orig)
    writer.WriteEndElement()

    writer.WriteStartElement("CST")
    writer.WriteString("60")
    writer.WriteEndElement()

    'If ICMS_vBCSTRet > 0 Then
    writer.WriteStartElement("vBCSTRet")
    writer.WriteString(ICMS_vBCSTRet.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("pST")
    writer.WriteString(pST.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("vICMSSubstituto")
    writer.WriteString("0.00")
    writer.WriteEndElement()

    writer.WriteStartElement("vICMSSTRet")
    writer.WriteString(ICMS_vICMSSTRet.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()
    'End If

    'writer.WriteStartElement("vBCFCPST")
    'writer.WriteString(vBCUFDestST.ToString("0.00").Replace(",", "."))
    'writer.WriteEndElement()

    'writer.WriteStartElement("pFCPSTRet")
    'writer.WriteString(pFCPUFDestST.ToString("0.00").Replace(",", "."))
    'writer.WriteEndElement()

    'writer.WriteStartElement("vFCPSTRet")
    'writer.WriteString(vFCPUFDestST.ToString("0.00").Replace(",", "."))
    'writer.WriteEndElement()

    writer.WriteEndElement() 'FIM ICMS60	
  End Sub

  Private Sub ICMS70(ByVal writer As XmlTextWriter, ByVal ICMS_orig As Integer, ByVal ICMS_modBC As Integer, ByVal ICMS_pRedBC As Decimal, _
                       ByVal ICMS_vBC As Decimal, ByVal ICMS_pICMS As Decimal, ByVal ICMS_vICMS As Decimal, ByVal ICMS_modBCST As Integer, _
                       ByVal ICMS_pMVAST As Decimal, ByVal ICMS_pRedBCST As Decimal, ByVal ICMS_vBCST As Decimal, ByVal ICMS_pICMSST As Decimal, _
                       ByVal ICMS_vICMSST As Decimal, ByVal ICMS_vICMSDeson As Decimal, ByVal ICMS_motDesICMS As Integer, _
                       ByVal vBCUFDestST As Decimal, ByVal pFCPUFDestST As Decimal, ByVal vFCPUFDestST As Decimal)
    writer.WriteStartElement("ICMS70")

    writer.WriteStartElement("orig")
    writer.WriteString(ICMS_orig)
    writer.WriteEndElement()

    writer.WriteStartElement("CST")
    writer.WriteString("70")
    writer.WriteEndElement()

    writer.WriteStartElement("modBC")
    writer.WriteString(ICMS_modBC)
    writer.WriteEndElement()

    writer.WriteStartElement("pRedBC")
    writer.WriteString(ICMS_pRedBC.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("vBC")
    writer.WriteString(ICMS_vBC.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("pICMS")
    writer.WriteString(ICMS_pICMS.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("vICMS")
    writer.WriteString(ICMS_vICMS.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("modBCST")
    writer.WriteString(ICMS_modBCST)
    writer.WriteEndElement()

    writer.WriteStartElement("pMVAST")
    writer.WriteString(ICMS_pMVAST.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("pRedBCST")
    writer.WriteString(ICMS_pRedBCST.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("vBCST")
    writer.WriteString(ICMS_vBCST.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("pICMSST")
    writer.WriteString(ICMS_pICMSST.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("vICMSST")
    writer.WriteString(ICMS_vICMSST.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    If ICMS_vICMSDeson > 0 Then
      writer.WriteStartElement("vICMSDeson")
      writer.WriteString(ICMS_vICMSDeson.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("motDesICMS")
      writer.WriteString(ICMS_motDesICMS)
      writer.WriteEndElement()
    End If

    If vFCPUFDestST > 0 Then
      writer.WriteStartElement("vBCFCPST")
      writer.WriteString(vBCUFDestST.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("pFCPST")
      writer.WriteString(pFCPUFDestST.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vFCPST")
      writer.WriteString(vFCPUFDestST.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()
    End If

    writer.WriteEndElement() 'FIM ICMS70	
  End Sub

  Private Sub ICMS90(ByVal writer As XmlTextWriter, ByVal ICMS_orig As Integer, ByVal ICMS_modBC As Integer, ByVal ICMS_vBC As Decimal, ByVal ICMS_pRedBC As Decimal, _
                       ByVal ICMS_pICMS As Decimal, ByVal ICMS_vICMS As Decimal, ByVal ICMS_modBCST As Integer, ByVal ICMS_pMVAST As Decimal, ByVal ICMS_pRedBCST As Decimal, _
                       ByVal ICMS_vBCST As Decimal, ByVal ICMS_pICMSST As Decimal, ByVal ICMS_vICMSST As Decimal, ByVal ICMS_vICMSDeson As Decimal, ByVal ICMS_motDesICMS As Integer, _
                       ByVal vBCUFDest As Decimal, ByVal pFCPUFDest As Decimal, ByVal vFCPUFDest As Decimal, ByVal vBCUFDestST As Decimal, ByVal pFCPUFDestST As Decimal, ByVal vFCPUFDestST As Decimal)
    writer.WriteStartElement("ICMS90")

    writer.WriteStartElement("orig")
    writer.WriteString(ICMS_orig)
    writer.WriteEndElement()

    writer.WriteStartElement("CST")
    writer.WriteString("90")
    writer.WriteEndElement()

    If ICMS_vICMS > 0 Then
      writer.WriteStartElement("modBC")
      writer.WriteString(ICMS_modBC)
      writer.WriteEndElement()

      writer.WriteStartElement("vBC")
      writer.WriteString(ICMS_vBC.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("pRedBC")
      writer.WriteString(ICMS_pRedBC.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("pICMS")
      writer.WriteString(ICMS_pICMS.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vICMS")
      writer.WriteString(ICMS_vICMS.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()
    End If

    'If vBCUFDest > 0 Or vFCPUFDest > 0 Then
    'writer.WriteStartElement("vBCFCP")
    'writer.WriteString(vBCUFDest.ToString("0.00").Replace(",", "."))
    'writer.WriteEndElement()

    'writer.WriteStartElement("pFCP")
    'writer.WriteString(pFCPUFDest.ToString("0.00").Replace(",", "."))
    'writer.WriteEndElement()

    'writer.WriteStartElement("vFCP")
    'writer.WriteString(vFCPUFDest.ToString("0.00").Replace(",", "."))
    'writer.WriteEndElement()
    'End If	

    If ICMS_vICMSST > 0 Then
      writer.WriteStartElement("modBCST")
      writer.WriteString(ICMS_modBCST)
      writer.WriteEndElement()

      writer.WriteStartElement("pMVAST")
      writer.WriteString(ICMS_pMVAST.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("pRedBCST")
      writer.WriteString(ICMS_pRedBCST.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vBCST")
      writer.WriteString(ICMS_vBCST.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("pICMSST")
      writer.WriteString(ICMS_pICMSST.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vICMSST")
      writer.WriteString(ICMS_vICMSST.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      If vBCUFDestST > 0 Or vFCPUFDestST > 0 Then
        writer.WriteStartElement("vBCFCPST")
        writer.WriteString(vBCUFDestST.ToString("0.00").Replace(",", "."))
        writer.WriteEndElement()

        writer.WriteStartElement("pFCPST")
        writer.WriteString(pFCPUFDestST.ToString("0.00").Replace(",", "."))
        writer.WriteEndElement()

        writer.WriteStartElement("vFCPST")
        writer.WriteString(vFCPUFDestST.ToString("0.00").Replace(",", "."))
        writer.WriteEndElement()
      End If
    End If 'FIM ICMS_vICMSST

    If ICMS_vICMSDeson > 0 Then
      writer.WriteStartElement("vICMSDeson")
      writer.WriteString(ICMS_vICMSDeson.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("motDesICMS")
      writer.WriteString(ICMS_motDesICMS)
      writer.WriteEndElement()
    End If

    writer.WriteEndElement() 'FIM ICMS90	
  End Sub

  Private Sub ICMSSN101(ByVal writer As XmlTextWriter, ByVal ICMS_orig As Integer, ByVal ICMS_pCredSN As Decimal, ByVal ICMS_vCredICMSSN As Decimal)
    writer.WriteStartElement("ICMSSN101")

    writer.WriteStartElement("orig")
    writer.WriteString(ICMS_orig)
    writer.WriteEndElement()

    writer.WriteStartElement("CSOSN")
    writer.WriteString("101")
    writer.WriteEndElement()

    writer.WriteStartElement("pCredSN")
    writer.WriteString(ICMS_pCredSN.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("vCredICMSSN")
    writer.WriteString(ICMS_vCredICMSSN.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteEndElement() 'FIM ICMSSN101

  End Sub

  Private Sub ICMSSN102(ByVal writer As XmlTextWriter, ByVal ICMS_orig As Integer, ByVal ICMS_CSOSN As Integer)
    writer.WriteStartElement("ICMSSN102")

    writer.WriteStartElement("orig")
    writer.WriteString(ICMS_orig)
    writer.WriteEndElement()

    writer.WriteStartElement("CSOSN")
    writer.WriteString(ICMS_CSOSN)
    writer.WriteEndElement()

    writer.WriteEndElement() 'FIM ICMSSN102	
  End Sub

  Private Sub ICMSSN201(ByVal writer As XmlTextWriter, ByVal ICMS_orig As Integer, ByVal ICMS_modBCST As Integer, ByVal ICMS_pMVAST As Decimal, _
                          ByVal ICMS_pRedBCST As Decimal, ByVal ICMS_vBCST As Decimal, ByVal ICMS_pICMSST As Decimal, ByVal ICMS_vICMSST As Decimal, _
                          ByVal ICMS_pCredSN As Decimal, ByVal ICMS_vCredICMSSN As Decimal, ByVal vBCUFDestST As Decimal, ByVal pFCPUFDestST As Decimal, _
                          ByVal vFCPUFDestST As Decimal)
    writer.WriteStartElement("ICMSSN201")

    writer.WriteStartElement("orig")
    writer.WriteString(ICMS_orig)
    writer.WriteEndElement()

    writer.WriteStartElement("CSOSN")
    writer.WriteString("201")
    writer.WriteEndElement()

    writer.WriteStartElement("modBCST")
    writer.WriteString(ICMS_modBCST)
    writer.WriteEndElement()

    writer.WriteStartElement("pMVAST")
    writer.WriteString(ICMS_pMVAST.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("pRedBCST")
    writer.WriteString(ICMS_pRedBCST.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("vBCST")
    writer.WriteString(ICMS_vBCST.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("pICMSST")
    writer.WriteString(ICMS_pICMSST.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("vICMSST")
    writer.WriteString(ICMS_vICMSST.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    If vFCPUFDestST > 0 Then
      writer.WriteStartElement("vBCFCPST")
      writer.WriteString(vBCUFDestST.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("pFCPST")
      writer.WriteString(pFCPUFDestST.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vFCPST")
      writer.WriteString(vFCPUFDestST.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()
    End If

    writer.WriteStartElement("pCredSN")
    writer.WriteString(ICMS_pCredSN.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("vCredICMSSN")
    writer.WriteString(ICMS_vCredICMSSN.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteEndElement() 'FIM ICMSSN201	
  End Sub

  Private Sub ICMSSN202(ByVal writer As XmlTextWriter, ByVal ICMS_orig As Integer, ByVal ICMS_CSOSN As Integer, ByVal ICMS_modBCST As Integer, _
                          ByVal ICMS_pMVAST As Decimal, ByVal ICMS_pRedBCST As Decimal, ByVal ICMS_vBCST As Decimal, ByVal ICMS_pICMSST As Decimal, _
                          ByVal ICMS_vICMSST As Decimal, ByVal vBCUFDestST As Decimal, ByVal pFCPUFDestST As Decimal, ByVal vFCPUFDestST As Decimal)
    writer.WriteStartElement("ICMSSN202")

    writer.WriteStartElement("orig")
    writer.WriteString(ICMS_orig)
    writer.WriteEndElement()

    writer.WriteStartElement("CSOSN")
    writer.WriteString(ICMS_CSOSN)
    writer.WriteEndElement()

    writer.WriteStartElement("modBCST")
    writer.WriteString(ICMS_modBCST)
    writer.WriteEndElement()

    writer.WriteStartElement("pMVAST")
    writer.WriteString(ICMS_pMVAST.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("pRedBCST")
    writer.WriteString(ICMS_pRedBCST.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("vBCST")
    writer.WriteString(ICMS_vBCST.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("pICMSST")
    writer.WriteString(ICMS_pICMSST.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("vICMSST")
    writer.WriteString(ICMS_vICMSST.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    If vFCPUFDestST > 0 Then
      writer.WriteStartElement("vBCFCPST")
      writer.WriteString(vBCUFDestST.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("pFCPST")
      writer.WriteString(pFCPUFDestST.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vFCPST")
      writer.WriteString(vFCPUFDestST.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()
    End If

    writer.WriteEndElement() 'FIM ICMSSN202	
  End Sub

  Private Sub ICMSSN500(ByVal writer As XmlTextWriter, ByVal ICMS_orig As Integer, ByVal ICMS_vBCSTRet As Decimal, ByVal ICMS_vICMSSTRet As Decimal, _
                          ByVal vBCUFDestSTRet As Decimal, ByVal pFCPUFDestSTRet As Decimal, ByVal vFCPUFDestSTRet As Decimal, ByVal pST As Decimal)
    writer.WriteStartElement("ICMSSN500")

    writer.WriteStartElement("orig")
    writer.WriteString(ICMS_orig)
    writer.WriteEndElement()

    writer.WriteStartElement("CSOSN")
    writer.WriteString("500")
    writer.WriteEndElement()

    'If ICMS_vBCSTRet > 0 Then
    writer.WriteStartElement("vBCSTRet")
    writer.WriteString(ICMS_vBCSTRet.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("pST")
    writer.WriteString(pST.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("vICMSSubstituto")
    writer.WriteString("0.00")
    writer.WriteEndElement()

    writer.WriteStartElement("vICMSSTRet")
    writer.WriteString(ICMS_vICMSSTRet.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()
    'End If

    If pFCPUFDestSTRet > 0 Or vFCPUFDestSTRet > 0 Then
      writer.WriteStartElement("vBCFCPSTRet")
      writer.WriteString(vBCUFDestSTRet.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("pFCPSTRet")
      writer.WriteString(pFCPUFDestSTRet.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vFCPSTRet")
      writer.WriteString(vFCPUFDestSTRet.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()
    End If

    writer.WriteEndElement() 'FIM ICMSSN500	
  End Sub

  Private Sub ICMSSN900(ByVal writer As XmlTextWriter, ByVal ICMS_orig As Integer, ByVal ICMS_modBC As Integer, ByVal ICMS_vBC As Decimal, _
                          ByVal ICMS_pRedBC As Decimal, ByVal ICMS_pICMS As Decimal, ByVal ICMS_vICMS As Decimal, ByVal ICMS_modBCST As Integer, _
                          ByVal ICMS_pMVAST As Decimal, ByVal ICMS_pRedBCST As Decimal, ByVal ICMS_vBCST As Decimal, ByVal ICMS_pICMSST As Decimal, _
                          ByVal ICMS_vICMSST As Decimal, ByVal ICMS_pCredSN As Decimal, ByVal ICMS_vCredICMSSN As Decimal, _
                          ByVal vBCUFDestST As Decimal, ByVal pFCPUFDestST As Decimal, ByVal vFCPUFDestST As Decimal)
    writer.WriteStartElement("ICMSSN900")

    writer.WriteStartElement("orig")
    writer.WriteString(ICMS_orig)
    writer.WriteEndElement()

    writer.WriteStartElement("CSOSN")
    writer.WriteString("900")
    writer.WriteEndElement()

    If ICMS_vICMS > 0 Then
      writer.WriteStartElement("modBC")
      writer.WriteString(ICMS_modBC)
      writer.WriteEndElement()

      writer.WriteStartElement("vBC")
      writer.WriteString(ICMS_vBC.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("pRedBC")
      writer.WriteString(ICMS_pRedBC.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("pICMS")
      writer.WriteString(ICMS_pICMS.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vICMS")
      writer.WriteString(ICMS_vICMS.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()
    End If

    If ICMS_vICMSST > 0 Then
      writer.WriteStartElement("modBCST")
      writer.WriteString(ICMS_modBCST)
      writer.WriteEndElement()

      writer.WriteStartElement("pMVAST")
      writer.WriteString(ICMS_pMVAST.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("pRedBCST")
      writer.WriteString(ICMS_pRedBCST.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vBCST")
      writer.WriteString(ICMS_vBCST.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("pICMSST")
      writer.WriteString(ICMS_pICMSST.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vICMSST")
      writer.WriteString(ICMS_vICMSST.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()
    End If

    If vFCPUFDestST > 0 Or pFCPUFDestST > 0 Then
      writer.WriteStartElement("vBCFCPST")
      writer.WriteString(vBCUFDestST.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("pFCPST")
      writer.WriteString(pFCPUFDestST.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vFCPST")
      writer.WriteString(vFCPUFDestST.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()
    End If

    If ICMS_vCredICMSSN > 0 Then
      writer.WriteStartElement("pCredSN")
      writer.WriteString(ICMS_pCredSN.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vCredICMSSN")
      writer.WriteString(ICMS_vCredICMSSN.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()
    End If

    writer.WriteEndElement() 'FIM ICMSSN900	
  End Sub

  'Grupo de Repasse do ICMS ST
  Private Sub ICMSST(ByVal writer As XmlTextWriter, ByVal orig As Integer, ByVal CST As Integer, ByVal vBCSTRet As Decimal, _
                     ByVal vICMSSTRet As Decimal, ByVal vBCSTDest As Decimal, ByVal vICMSSTDest As Decimal)
    writer.WriteStartElement("ICMSST")

    writer.WriteStartElement("orig")
    writer.WriteString(orig)
    writer.WriteEndElement()

    writer.WriteStartElement("CST")
    writer.WriteString(CST)
    writer.WriteEndElement()

    writer.WriteStartElement("vBCSTRet")
    writer.WriteString(vBCSTRet.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("vICMSSTRet")
    writer.WriteString(vICMSSTRet.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("vBCSTDest")
    writer.WriteString(vBCSTDest.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("vICMSSTDest")
    writer.WriteString(vICMSSTDest.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteEndElement() 'FIM ICMSST	
  End Sub

  Private Sub IPITrib(ByVal writer As XmlTextWriter, ByVal IPI_CST As Integer, ByVal IPI_vBC As Decimal, ByVal IPI_pIPI As Decimal, _
                        ByVal IPI_qUnid As Decimal, ByVal IPI_vUnid As Decimal, ByVal IPI_vIPI As Decimal)
    writer.WriteStartElement("IPITrib")

    writer.WriteStartElement("CST")
    writer.WriteString(Right("00" & IPI_CST, 2))
    writer.WriteEndElement()

    If IPI_vUnid > 0 Then 'se o cálculo do IPI for de VALOR POR UNIDADE
      writer.WriteStartElement("qUnid")
      writer.WriteString(IPI_qUnid.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vUnid")
      writer.WriteString(IPI_vUnid.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()
    Else 'IPI_pIPI > 0 Then 'se o cálculo do IPI for por ALIQUOTA
      writer.WriteStartElement("vBC")
      writer.WriteString(IPI_vBC.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("pIPI")
      writer.WriteString(IPI_pIPI.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()
    End If

    writer.WriteStartElement("vIPI")
    writer.WriteString(IPI_vIPI.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteEndElement() 'FIM IPITrib	
  End Sub

  Private Sub IPINT(ByVal writer As XmlTextWriter, ByVal IPI_CST As Integer)
    writer.WriteStartElement("IPINT")

    writer.WriteStartElement("CST")
    writer.WriteString(Right("00" & IPI_CST, 2))
    writer.WriteEndElement()

    writer.WriteEndElement() 'FIM IPINT	
  End Sub

  ''' <summary>
  ''' Grupo PIS tributado pela alíquota
  ''' </summary>
  ''' <param name="writer"></param>
  ''' <param name="PIS_CST"></param>
  ''' <param name="PIS_vBC"></param>
  ''' <param name="PIS_pPIS"></param>
  ''' <param name="PIS_vPIS"></param>
  Private Sub PISAliq(ByVal writer As XmlTextWriter, ByVal PIS_CST As Integer, ByVal PIS_vBC As Decimal, ByVal PIS_pPIS As Decimal, ByVal PIS_vPIS As Decimal)
    writer.WriteStartElement("PISAliq")

    writer.WriteStartElement("CST")
    writer.WriteString(Right("00" & PIS_CST, 2))
    writer.WriteEndElement()

    writer.WriteStartElement("vBC")
    writer.WriteString(PIS_vBC.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("pPIS")
    writer.WriteString(PIS_pPIS.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("vPIS")
    writer.WriteString(PIS_vPIS.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteEndElement() 'FIM PISAliq	
  End Sub

  ''' <summary>
  ''' 'Grupo PIS tributado por Qtde
  ''' </summary>
  ''' <param name="writer"></param>
  ''' <param name="PIS_qBCProd"></param>
  ''' <param name="PIS_vAliqProd"></param>
  ''' <param name="PIS_vPIS"></param>
  Private Sub PISQtde(ByVal writer As XmlTextWriter, ByVal PIS_qBCProd As Decimal, ByVal PIS_vAliqProd As Decimal, ByVal PIS_vPIS As Decimal)
    writer.WriteStartElement("PISQtde")

    writer.WriteStartElement("CST")
    writer.WriteString("03")
    writer.WriteEndElement()

    writer.WriteStartElement("qBCProd")
    writer.WriteString(PIS_qBCProd.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("vAliqProd")
    writer.WriteString(PIS_vAliqProd.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("vPIS")
    writer.WriteString(PIS_vPIS.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteEndElement() 'FIM PISQtde	
  End Sub

  ''' <summary>
  ''' Grupo PIS não tributado
  ''' </summary>
  ''' <param name="writer"></param>
  ''' <param name="PIS_CST"></param>
  Private Sub PISNT(ByVal writer As XmlTextWriter, ByVal PIS_CST As Integer)
    writer.WriteStartElement("PISNT")

    writer.WriteStartElement("CST")
    writer.WriteString(Right("00" & PIS_CST, 2))
    writer.WriteEndElement()

    writer.WriteEndElement() 'FIM PISNT	
  End Sub

  Private Sub PISOutr(ByVal writer As XmlTextWriter, ByVal PIS_CST As Integer, ByVal PIS_vBC As Decimal, ByVal PIS_pPIS As Decimal, _
                        ByVal PIS_qBCProd As Decimal, ByVal PIS_vAliqProd As Decimal, ByVal PIS_vPIS As Decimal)
    writer.WriteStartElement("PISOutr")

    writer.WriteStartElement("CST")
    writer.WriteString(Right("00" & PIS_CST, 2))
    writer.WriteEndElement()

    If PIS_qBCProd > 0 Or PIS_vAliqProd > 0 Then 'cálculo do PIS em valor
      writer.WriteStartElement("qBCProd")
      writer.WriteString(PIS_qBCProd.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vAliqProd")
      writer.WriteString(PIS_vAliqProd.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()
    Else 'cálculo do PIS em percentual
      writer.WriteStartElement("vBC")
      writer.WriteString(PIS_vBC.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("pPIS")
      writer.WriteString(PIS_pPIS.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()
    End If

    writer.WriteStartElement("vPIS")
    writer.WriteString(PIS_vPIS.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteEndElement() 'FIM PISOutr	
  End Sub

  Private Sub PISST(ByVal writer As XmlTextWriter, ByVal PISST_vBC As Decimal, ByVal PISST_pPIS As Decimal, ByVal PISST_qBCProd As Decimal, _
                      ByVal PISST_vAliqProd As Decimal, ByVal PISST_vPIS As Decimal)
    writer.WriteStartElement("PISST")

    If PISST_vBC > 0 Or PISST_pPIS > 0 Then
      writer.WriteStartElement("vBC")
      writer.WriteString(PISST_vBC.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("pPIS")
      writer.WriteString(PISST_pPIS.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

    ElseIf PISST_qBCProd > 0 Or PISST_vAliqProd > 0 Then
      writer.WriteStartElement("qBCProd")
      writer.WriteString(PISST_qBCProd.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vAliqProd")
      writer.WriteString(PISST_vAliqProd.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()
    End If

    writer.WriteStartElement("vPIS")
    writer.WriteString(PISST_vPIS.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteEndElement() 'FIM PISST	
  End Sub

  Private Sub COFINSAliq(ByVal writer As XmlTextWriter, ByVal COFINS_CST As Integer, ByVal COFINS_vBC As Decimal, ByVal COFINS_pCOFINS As Decimal, ByVal COFINS_vCOFINS As Decimal)
    writer.WriteStartElement("COFINSAliq")

    writer.WriteStartElement("CST")
    writer.WriteString(Right("00" & COFINS_CST, 2))
    writer.WriteEndElement()

    writer.WriteStartElement("vBC")
    writer.WriteString(COFINS_vBC.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("pCOFINS")
    writer.WriteString(COFINS_pCOFINS.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("vCOFINS")
    writer.WriteString(COFINS_vCOFINS.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteEndElement() 'FIM COFINSAliq	
  End Sub

  Private Sub COFINSQtde(ByVal writer As XmlTextWriter, ByVal COFINS_qBCProd As Decimal, ByVal COFINS_vAliqProd As Decimal, ByVal COFINS_vCOFINS As Decimal)
    writer.WriteStartElement("COFINSQtde")

    writer.WriteStartElement("CST")
    writer.WriteString("03")
    writer.WriteEndElement()

    writer.WriteStartElement("qBCProd")
    writer.WriteString(COFINS_qBCProd.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("vAliqProd")
    writer.WriteString(COFINS_vAliqProd.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteStartElement("vCOFINS")
    writer.WriteString(COFINS_vCOFINS.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteEndElement() 'FIM COFINSQtde	
  End Sub

  ''' <summary>
  ''' Grupo COFINS não tributado
  ''' </summary>
  ''' <param name="writer"></param>
  ''' <param name="COFINS_CST"></param>
  Private Sub COFINSNT(ByVal writer As XmlTextWriter, ByVal COFINS_CST As Integer)
    writer.WriteStartElement("COFINSNT")

    writer.WriteStartElement("CST")
    writer.WriteString(Right("00" & COFINS_CST, 2))
    writer.WriteEndElement()

    writer.WriteEndElement() 'FIM COFINSNT	
  End Sub

  Private Sub COFINSOutr(ByVal writer As XmlTextWriter, ByVal COFINS_CST As Decimal, ByVal COFINS_vBC As Decimal, ByVal COFINS_pCOFINS As Decimal, _
                           ByVal COFINS_qBCProd As Decimal, ByVal COFINS_vAliqProd As Decimal, ByVal COFINS_vCOFINS As Decimal)
    writer.WriteStartElement("COFINSOutr")

    writer.WriteStartElement("CST")
    writer.WriteString(Right("00" & COFINS_CST, 2))
    writer.WriteEndElement()

    If COFINS_qBCProd > 0 Or COFINS_vAliqProd > 0 Then 'para cálculo da COFINS em valor
      writer.WriteStartElement("qBCProd")
      writer.WriteString(COFINS_qBCProd.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vAliqProd")
      writer.WriteString(COFINS_vAliqProd.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()
    Else 'para cálculo da COFINS em percentual
      writer.WriteStartElement("vBC")
      writer.WriteString(COFINS_vBC.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("pCOFINS")
      writer.WriteString(COFINS_pCOFINS.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()
    End If

    writer.WriteStartElement("vCOFINS")
    writer.WriteString(COFINS_vCOFINS.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteEndElement() 'FIM COFINSOutr	
  End Sub

  Private Sub COFINSST(ByVal writer As XmlTextWriter, ByVal COFINSST_vBC As Decimal, ByVal COFINSST_pCOFINS As Decimal, ByVal COFINSST_qBCProd As Decimal, _
                         ByVal COFINSST_vAliqProd As Decimal, ByVal COFINSST_vCOFINS As Decimal)
    writer.WriteStartElement("COFINSST")

    If COFINSST_qBCProd > 0 Or COFINSST_vAliqProd > 0 Then 'cálculo da COFINS-ST em valor
      writer.WriteStartElement("qBCProd")
      writer.WriteString(COFINSST_qBCProd.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vAliqProd")
      writer.WriteString(COFINSST_vAliqProd.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()
    Else 'cálculo da COFINS-ST em percentual
      writer.WriteStartElement("vBC")
      writer.WriteString(COFINSST_vBC.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("pCOFINS")
      writer.WriteString(COFINSST_pCOFINS.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()
    End If

    writer.WriteStartElement("vCOFINS")
    writer.WriteString(COFINSST_vCOFINS.ToString("0.00").Replace(",", "."))
    writer.WriteEndElement()

    writer.WriteEndElement() 'FIM COFINSST	
  End Sub

  ''' <summary>
  ''' Grupo Totais referentes ao ICMS
  ''' </summary>
  ''' <param name="writer"></param>
  Private Sub ICMSTot(ByVal writer As XmlTextWriter)
    Dim nfe As New clsNFEide

    Try
      nfe.PegaTotalUmaNfe(Me.id_empresa, Me.id_nf)

      writer.WriteStartElement("ICMSTot")

      writer.WriteStartElement("vBC")
      writer.WriteString(nfe.ICMSTot_vBC.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vICMS")
      writer.WriteString(nfe.ICMSTot_vICMS.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vICMSDeson")
      writer.WriteString(nfe.ICMSTot_vICMSDeson.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vFCPUFDest")
      writer.WriteString(nfe.ICMSDestTot_vFCP.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vICMSUFDest")
      writer.WriteString(nfe.ICMSTot_vICMSUFDest.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vICMSUFRemet")
      writer.WriteString(nfe.ICMSTot_vICMSUFRemet.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      'If nfe.ICMSDestTot_vFCP > 0 Then
      writer.WriteStartElement("vFCP")
      'writer.WriteString(nfe.ICMSDestTot_vFCP.ToString("0.00").Replace(",", "."))	  
      writer.WriteString("0.00")
      writer.WriteEndElement()
      'End If      

      writer.WriteStartElement("vBCST")
      writer.WriteString(nfe.ICMSTot_vBCST.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vST")
      writer.WriteString(nfe.ICMSTot_vST.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vFCPST")
      writer.WriteString(nfe.ICMSDestTot_vFCPST.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vFCPSTRet")
      writer.WriteString(nfe.ICMSDestTot_vFCPSTRet.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vProd")
      writer.WriteString(nfe.ICMSTot_vProd.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vFrete")
      writer.WriteString(nfe.ICMSTot_vFrete.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vSeg")
      writer.WriteString(nfe.ICMSTot_vSeg.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vDesc")
      writer.WriteString(nfe.ICMSTot_vDesc.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vII")
      writer.WriteString(nfe.ICMSTot_vII.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vIPI")
      writer.WriteString(nfe.ICMSTot_vIPI.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vIPIDevol")
      writer.WriteString(nfe.ICMSTot_vIPIDevol.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vPIS")
      writer.WriteString(nfe.ICMSTot_vPIS.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vCOFINS")
      writer.WriteString(nfe.ICMSTot_vCOFINS.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vOutro")
      writer.WriteString(nfe.ICMSTot_vOutro.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vNF")
      writer.WriteString(nfe.ICMSTot_vNF.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vTotTrib")
      writer.WriteString(nfe.vTotTrib.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteEndElement() 'FIM ICMSTot	  


      'W01. Total da NF-e / ISSQN
      If nfe.ISSQNtot_vISS > 0 Then
        writer.WriteStartElement("ISSQNtot")

        If nfe.ISSQNtot_vServ > 0 Then
          writer.WriteStartElement("vServ")
          writer.WriteString(nfe.ISSQNtot_vServ.ToString("0.00").Replace(",", "."))
          writer.WriteEndElement()
        End If

        If nfe.ISSQNtot_vBC > 0 Then
          writer.WriteStartElement("vBC")
          writer.WriteString(nfe.ISSQNtot_vBC.ToString("0.00").Replace(",", "."))
          writer.WriteEndElement()
        End If

        writer.WriteStartElement("vISS")
        writer.WriteString(nfe.ISSQNtot_vISS.ToString("0.00").Replace(",", "."))
        writer.WriteEndElement()


        If nfe.ISSQNtot_vPIS > 0 Then
          writer.WriteStartElement("vPIS")
          writer.WriteString(nfe.ISSQNtot_vPIS.ToString("0.00").Replace(",", "."))
          writer.WriteEndElement()
        End If

        If nfe.ISSQNtot_vCOFINS > 0 Then
          writer.WriteStartElement("vCOFINS")
          writer.WriteString(nfe.ISSQNtot_vCOFINS.ToString("0.00").Replace(",", "."))
          writer.WriteEndElement()
        End If

        writer.WriteEndElement() 'FIM ISSQNtot

      End If


      'W02. Total da NF-e / Retenção de Tributos
      TotalRetencaoTributos(writer, nfe.retTrib_vRetPIS, nfe.retTrib_vRetCOFINS, nfe.retTrib_vRetCSLL, nfe.retTrib_vBCIRRF, nfe.retTrib_vIRRF, nfe.retTrib_vBCRetPrev, nfe.retTrib_vRetPrev)

    Catch ex As Exception
      'MsgBox("ERRO AO PEGAR AS INFORMAÇÕES DO TOTAL DE ICMS PARA ENVIO DA NFE: " & ex.Message() & "---------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
      writer.WriteStartElement("ERRO")
      writer.WriteString("ERRO AO PEGAR AS INFORMAÇÕES DO TOTAL DE ICMS PARA ENVIO DA NFE: " & ex.Message() & "---------" & ex.StackTrace())
      writer.WriteEndElement()
    End Try
  End Sub

  ''' <summary>
  ''' Grupo Transportador
  ''' </summary>
  ''' <param name="writer"></param>
  ''' <param name="CNPJ"></param>
  ''' <param name="tipo_pessoa"></param>
  ''' <param name="xNome"></param>
  ''' <param name="IE"></param>
  ''' <param name="xEnder"></param>
  ''' <param name="xMun"></param>
  ''' <param name="UF"></param>
  Private Sub Transporta(ByVal writer As XmlTextWriter, ByVal CNPJ As String, ByVal tipo_pessoa As String, ByVal xNome As String, _
                           ByVal IE As String, ByVal xEnder As String, ByVal xMun As String, ByVal UF As String)

    If CNPJ = "-1" Then CNPJ = ""

    If CNPJ <> "" Then 'Todos os campos têm de ser informados... com exceção do IE...
      writer.WriteStartElement("transporta")

      If tipo_pessoa = "J" Then
        writer.WriteStartElement("CNPJ")
        writer.WriteString(Right("00000000000000" & CNPJ, 14))
        writer.WriteEndElement()
      Else
        writer.WriteStartElement("CPF")
        writer.WriteString(Right("00000000000" & CNPJ, 11))
        writer.WriteEndElement()
      End If

      writer.WriteStartElement("xNome")
      writer.WriteString(xNome)
      writer.WriteEndElement()

      If IE <> "" Then
        writer.WriteStartElement("IE")
        writer.WriteString(IE)
        writer.WriteEndElement()
      End If

      writer.WriteStartElement("xEnder")
      writer.WriteString(xEnder)
      writer.WriteEndElement()

      writer.WriteStartElement("xMun")
      writer.WriteString(xMun)
      writer.WriteEndElement()

      writer.WriteStartElement("UF")
      writer.WriteString(UF)
      writer.WriteEndElement()

      writer.WriteEndElement() 'FIM transporta	  
    End If
  End Sub

  ''' <summary>
  ''' Grupo Retenção ICMS transporte
  ''' </summary>
  ''' <param name="writer"></param>
  ''' <param name="vServ"></param>
  ''' <param name="vBCRet"></param>
  ''' <param name="pICMSRet"></param>
  ''' <param name="vICMSRet"></param>
  ''' <param name="CFOP"></param>
  ''' <param name="cMunFG"></param>
  Private Sub RetTransp(ByVal writer As XmlTextWriter, ByVal vServ As Decimal, ByVal vBCRet As Decimal, ByVal pICMSRet As Decimal, _
                          ByVal vICMSRet As Decimal, ByVal CFOP As Integer, ByVal cMunFG As Integer)
    If vICMSRet > 0 Then
      writer.WriteStartElement("retTransp")

      writer.WriteStartElement("vServ")
      writer.WriteString(vServ.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vBCRet")
      writer.WriteString(vBCRet.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("pICMSRet")
      writer.WriteString(pICMSRet.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vICMSRet")
      writer.WriteString(vICMSRet.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()


      writer.WriteStartElement("CFOP")
      writer.WriteString(CFOP)
      writer.WriteEndElement()

      writer.WriteStartElement("cMunFG")
      writer.WriteString(cMunFG)
      writer.WriteEndElement()


      writer.WriteEndElement() 'FIM retTransp	  
    End If
  End Sub

  ''' <summary>
  ''' Informar o veículo trator (v2.0)
  ''' </summary>
  ''' <param name="writer"></param>
  ''' <param name="placa"></param>
  ''' <param name="UF"></param>
  ''' <param name="RNTC"></param>
  Private Sub VeicTransp(ByVal writer As XmlTextWriter, ByVal placa As String, ByVal UF As String, ByVal RNTC As String)
    If placa <> "" Or UF <> "--" Or RNTC <> "" Then
      writer.WriteStartElement("veicTransp")

      writer.WriteStartElement("placa")
      writer.WriteString(placa)
      writer.WriteEndElement()

      writer.WriteStartElement("UF")
      writer.WriteString(UF)
      writer.WriteEndElement()

      If RNTC <> "" Then
        writer.WriteStartElement("RNTC")
        writer.WriteString(RNTC)
        writer.WriteEndElement()
      End If

      writer.WriteEndElement() 'FIM veicTransp

    End If
  End Sub

  ''' <summary>
  ''' Informar os reboques/Dolly (v2.0)
  ''' </summary>
  ''' <param name="writer"></param>
  Private Sub Reboque(ByVal writer As XmlTextWriter)
    Dim reboque As New clsNfeReboque
    Dim dv As New DataView

    Try
      dv = reboque.ListaReboqueNfe(Me.id_nf)

      If dv.Count > 0 Then
        writer.WriteStartElement("reboque")

        For Each drv As DataRowView In dv
          writer.WriteStartElement("placa")
          writer.WriteString(drv(1))
          writer.WriteEndElement()

          writer.WriteStartElement("UF")
          writer.WriteString(drv(2))
          writer.WriteEndElement()

          writer.WriteStartElement("RNTC")
          writer.WriteString(drv(3))
          writer.WriteEndElement()

          If drv(4) <> "" Then
            writer.WriteStartElement("vagao")
            writer.WriteString(drv(4))
            writer.WriteEndElement()
          End If

          If drv(5) <> "" Then
            writer.WriteStartElement("balsa")
            writer.WriteString(drv(5))
            writer.WriteEndElement()
          End If
        Next

        writer.WriteEndElement() 'FIM reboque

      End If
    Catch ex As Exception
      'MsgBox("ERRO AO PEGAR AS INFORMAÇÕES DO REBOQUE PARA ENVIO DA NFE: " & ex.Message() & "-----------" & ex.StackTrace(), MsgBoxStyle.Critical, "mAXCONT")
      writer.WriteStartElement("ERRO")
      writer.WriteString("ERRO AO PEGAR AS INFORMAÇÕES DO REBOQUE PARA ENVIO DA NFE: " & ex.Message() & "-----------" & ex.StackTrace())
      writer.WriteEndElement()
    End Try
  End Sub

  ''' <summary>
  ''' Grupo Volumes
  ''' </summary>
  ''' <param name="writer"></param>
  Private Sub Volume(ByVal writer As XmlTextWriter)
    Dim volume As New clsNfeVolumes
    Dim dv As New DataView
    Dim pesoL, pesoB As Decimal
    Dim esp As String

    Try
      dv = volume.ListaVolumeUmaNfe(Me.id_nf)

      If dv.Count > 0 Then
        For Each drv As DataRowView In dv
          esp = drv(2)

          If esp = "" Then esp = "VOL"

          writer.WriteStartElement("vol")

          writer.WriteStartElement("qVol")
          writer.WriteString(drv(1))
          writer.WriteEndElement()

          writer.WriteStartElement("esp")
          writer.WriteString(esp)
          writer.WriteEndElement()

          If drv(3) <> "" Then
            writer.WriteStartElement("marca")
            writer.WriteString(drv(3))
            writer.WriteEndElement()
          End If

          If drv(4) <> "" Then
            writer.WriteStartElement("nVol")
            writer.WriteString(drv(4))
            writer.WriteEndElement()
          End If

          pesoL = drv(5)
          pesoB = drv(6)

          writer.WriteStartElement("pesoL")
          writer.WriteString(pesoL.ToString("0.000").Replace(",", "."))
          writer.WriteEndElement()

          writer.WriteStartElement("pesoB")
          writer.WriteString(pesoB.ToString("0.000").Replace(",", "."))
          writer.WriteEndElement()

          writer.WriteEndElement() 'FIM vol

        Next
      End If
    Catch ex As Exception
      'MsgBox("ERRO AO PEGAR AS INFORMAÇÕES DO VOLUME PARA ENVIO DA NFE: " & ex.Message() & "---------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
      writer.WriteStartElement("ERRO")
      writer.WriteString("ERRO AO PEGAR AS INFORMAÇÕES DO VOLUME PARA ENVIO DA NFE: " & ex.Message() & "---------" & ex.StackTrace())
      writer.WriteEndElement()
    End Try
  End Sub

  Private Sub Fatura(ByVal writer As XmlTextWriter, ByVal nFat As String, ByVal vOrig As Decimal, _
                       ByVal vDesc As Decimal, ByVal vLiq As Decimal)
    If vLiq > 0 Then
      writer.WriteStartElement("fat")

      writer.WriteStartElement("nFat")
      writer.WriteString(nFat)
      writer.WriteEndElement()

      writer.WriteStartElement("vOrig")
      writer.WriteString(vOrig.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vDesc")
      writer.WriteString(vDesc.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vLiq")
      writer.WriteString(vLiq.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteEndElement() 'FIM fat

    End If
  End Sub

  ''' <summary>
  ''' Grupo Duplicata
  ''' </summary>
  ''' <param name="writer"></param>
  Private Sub Duplicata(ByVal writer As XmlTextWriter)
    Dim duplicata As New clsNfeDuplicata
    Dim dv As New DataView
    Dim ajuste As New clsAjuste
    Dim vDup As Decimal

    Try
      dv = duplicata.ListaDuplicataUmaNfe(Me.id_empresa, Me.id_nf)

      If dv.Count > 0 Then
        For Each drv As DataRowView In dv
          writer.WriteStartElement("dup")

          writer.WriteStartElement("nDup")
          writer.WriteString(drv(0))
          writer.WriteEndElement()

          writer.WriteStartElement("dVenc")
          writer.WriteString(ajuste.AMD(drv(1)))
          writer.WriteEndElement()

          vDup = drv(2)
          writer.WriteStartElement("vDup")
          writer.WriteString(vDup.ToString("0.00").Replace(",", "."))
          writer.WriteEndElement()

          writer.WriteEndElement() 'FIM dup

        Next
      End If
    Catch ex As Exception
      'MsgBox("ERRO AO PEGAR AS INFORMAÇÕES DA DUPLICATA PARA ENVIO DA NFE: " & ex.Message() & "---------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
      writer.WriteStartElement("ERRO")
      writer.WriteString("ERRO AO PEGAR AS INFORMAÇÕES DA DUPLICATA PARA ENVIO DA NFE: " & ex.Message() & "---------" & ex.StackTrace())
      writer.WriteEndElement()
    End Try
  End Sub

  ''' <summary>
  ''' Campo de uso livre do contribuinte, Informar o nome do campo no atributo xCampo e o conteúdo Do campo no xTexto
  ''' </summary>
  ''' <param name="writer"></param>
  Private Sub ObsCont(ByVal writer As XmlTextWriter)
    Dim inf_contrib As New clsNfeInfoContrib
    Dim dv As New DataView

    Try
      dv = inf_contrib.ListaInfContribUmaNfe(Me.id_nf)

      If dv.Count > 0 Then
        For Each drv As DataRowView In dv
          writer.WriteStartElement("obsCont")
          writer.WriteAttributeString("xCampo", drv(1))

          writer.WriteStartElement("xTexto")
          writer.WriteString(drv(2))
          writer.WriteEndElement()

          writer.WriteEndElement() 'FIM obsCont

        Next
      End If
    Catch ex As Exception
      'MsgBox("ERRO AO PEGAR AS INFORMAÇÕES DO CONTRIBUINTE PARA O ENVIO DA NFE: " & ex.Message() & "---------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
      writer.WriteStartElement("ERRO")
      writer.WriteString("ERRO AO PEGAR AS INFORMAÇÕES DO CONTRIBUINTE PARA O ENVIO DA NFE: " & ex.Message() & "---------" & ex.StackTrace())
      writer.WriteEndElement()
    End Try
  End Sub

  ''' <summary>
  ''' Grupo Processo referenciado
  ''' </summary>
  ''' <param name="writer"></param>
  Private Sub ProcRef(ByVal writer As XmlTextWriter)
    Dim inf_proc As New clsNfeInfoProc
    Dim dv As New DataView

    Try
      dv = inf_proc.ListaInfoProcNfe(Me.id_nf)

      If dv.Count > 0 Then
        For Each drv As DataRowView In dv
          writer.WriteStartElement("procRef")

          writer.WriteStartElement("nProc")
          writer.WriteString(drv(1))
          writer.WriteEndElement()

          writer.WriteStartElement("indProc")
          writer.WriteString(drv(2))
          writer.WriteEndElement()

          writer.WriteEndElement()

        Next
      End If
    Catch ex As Exception
      'MsgBox("ERRO AO PEGAR AS INFORMAÇÕES DO PROCESSO REFERENCIADO PARA ENVIO DA NFE: " & ex.Message() & "-----------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
      writer.WriteStartElement("ERRO")
      writer.WriteString("ERRO AO PEGAR AS INFORMAÇÕES DO PROCESSO REFERENCIADO PARA ENVIO DA NFE: " & ex.Message() & "-----------" & ex.StackTrace())
      writer.WriteEndElement()
    End Try

  End Sub

  Private Sub InformacoesComercioExterior(ByVal writer As XmlTextWriter)
    Dim exportar As New clsNFEide

    Try
      exportar.PegaExpComprasNfe(Me.id_nf)

      If exportar.exp_UFEmbarq <> "--" Then
        writer.WriteStartElement("exporta")

        writer.WriteStartElement("UFSaidaPais")
        writer.WriteString(exportar.exp_UFEmbarq)
        writer.WriteEndElement()

        writer.WriteStartElement("xLocExporta")
        writer.WriteString(exportar.exp_xLocEmbarq)
        writer.WriteEndElement()

        writer.WriteStartElement("xLocDespacho")
        writer.WriteString(exportar.exp_xLocDespacho)
        writer.WriteEndElement()

        writer.WriteEndElement() 'FIM exporta

      End If

      'ZB. Informações de Compras
      If exportar.exp_xNEmp <> "" Or exportar.exp_xPed <> "" Or exportar.exp_xCont <> "" Then
        writer.WriteStartElement("compra")

        writer.WriteStartElement("xNEmp")
        writer.WriteString(exportar.exp_xNEmp)
        writer.WriteEndElement()

        writer.WriteStartElement("xPed")
        writer.WriteString(exportar.exp_xPed)
        writer.WriteEndElement()

        writer.WriteStartElement("xCont")
        writer.WriteString(exportar.exp_xCont)
        writer.WriteEndElement()

        writer.WriteEndElement() 'FIM compra

      End If

      'ZC. Informações do Registro de Aquisição de Cana
    Catch ex As Exception
      'MsgBox("ERRO AO PEGAR AS INFORMAÇÕES DE EXPORTAÇÃO PARA ENVIO DA NFE: " & ex.Message() & "----------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
      writer.WriteStartElement("ERRO")
      writer.WriteString("ERRO AO PEGAR AS INFORMAÇÕES DE EXPORTAÇÃO PARA ENVIO DA NFE: " & ex.Message() & "----------" & ex.StackTrace())
      writer.WriteEndElement()
    End Try
  End Sub
  ''' <summary>
  ''' W02. Total da NF-e / Retenção de Tributos (retTrib)
  ''' </summary>
  ''' <param name="writer"></param>
  ''' <param name="retTrib_vRetPIS"></param>
  ''' <param name="retTrib_vRetCOFINS"></param>
  ''' <param name="retTrib_vRetCSLL"></param>
  ''' <param name="retTrib_vBCIRRF"></param>
  ''' <param name="retTrib_vIRRF"></param>
  ''' <param name="retTrib_vBCRetPrev"></param>
  ''' <param name="retTrib_vRetPrev"></param>
  Private Sub TotalRetencaoTributos(ByVal writer As XmlTextWriter, ByVal retTrib_vRetPIS As Decimal, ByVal retTrib_vRetCOFINS As Decimal, ByVal retTrib_vRetCSLL As Decimal, _
                                      ByVal retTrib_vBCIRRF As Decimal, ByVal retTrib_vIRRF As Decimal, ByVal retTrib_vBCRetPrev As Decimal, ByVal retTrib_vRetPrev As Decimal)

    If retTrib_vRetPIS > 0 Or retTrib_vRetCOFINS > 0 Or retTrib_vRetCSLL > 0 Or retTrib_vBCIRRF > 0 Or retTrib_vIRRF > 0 Or retTrib_vBCRetPrev > 0 Or retTrib_vRetPrev > 0 Then
      writer.WriteStartElement("retTrib")

      writer.WriteStartElement("vRetPIS")
      writer.WriteString(retTrib_vRetPIS.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vRetCOFINS")
      writer.WriteString(retTrib_vRetCOFINS.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vRetCSLL")
      writer.WriteString(retTrib_vRetCSLL.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vBCIRRF")
      writer.WriteString(retTrib_vBCIRRF.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vIRRF")
      writer.WriteString(retTrib_vIRRF.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vBCRetPrev")
      writer.WriteString(retTrib_vBCRetPrev.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteStartElement("vRetPrev")
      writer.WriteString(retTrib_vRetPrev.ToString("0.00").Replace(",", "."))
      writer.WriteEndElement()

      writer.WriteEndElement() 'FIM retTrib

    End If
  End Sub

  ''' <summary>
  ''' Cria a pasta de armazenamento do XML... 
  ''' </summary>
  ''' <returns></returns>
  Public Function CriaPastaXmlNfe(ByVal caminho_salva_xml As String) As Boolean
    Dim ajuste As New clsAjuste
    Dim ano, mes As String

    Try
      ano = Year(Date.Now())
      mes = Month(Date.Now())

      Me.caminho_pasta = ajuste.CriaPastaXmlNFe(caminho_salva_xml, mes, ano)

    Catch ex As Exception
      id_retorno = 1
      xMotivo = "FALHA AO CRIAR A PASTA DE ARMAZENAMENTO DO XML."
      Return False
    End Try

    Return True

  End Function

  ''' <summary>
  ''' Cria o XML e retorna o caminho do arquivo criado para consulta(caminho_pasta)... 
  ''' </summary>
  ''' <param name="chNFe"></param>
  ''' <param name="nNF"></param>
  ''' <param name="ambiente"></param>
  ''' <returns></returns>
  Public Function CriaXmlConsultaProtocolo(ByVal chNFe As String, ByVal nNF As Integer, ByVal ambiente As AmbienteEnvioNFe) As Boolean
    Me.caminho_pasta = CriaPastaConsultaXml()

    Me.caminho_pasta = Me.caminho_pasta & "\" & chNFe & "_" & nNF & ".xml"

    Dim writer As New XmlTextWriter(Me.caminho_pasta, System.Text.Encoding.UTF8)
    Dim tpAmb As Integer

    If ambiente = AmbienteEnvioNFe.Producao Then
      tpAmb = 1
    Else
      tpAmb = 2
    End If


    writer.WriteStartDocument(True) 'INICIO DOCUMENTO

    writer.WriteStartElement("consSitNFe", "http://www.portalfiscal.inf.br/nfe")
    writer.WriteAttributeString("versao", "4.00")

    writer.WriteStartElement("tpAmb")
    writer.WriteString(tpAmb)
    writer.WriteEndElement()

    writer.WriteStartElement("xServ")
    writer.WriteString("CONSULTAR")
    writer.WriteEndElement()

    writer.WriteStartElement("chNFe")
    writer.WriteString(chNFe)
    writer.WriteEndElement()

    writer.WriteEndElement() 'FIM consSitNFe

    writer.WriteEndDocument() 'FIM DOCUMENTO


    writer.Close()

    Return True
  End Function

  ''' <summary>
  ''' Retorna o endereço que o XML para consulta foi salvo(local_xml)...
  ''' </summary>
  ''' <param name="nRec"></param>
  ''' <param name="nNF"></param>
  ''' <param name="ambiente"></param>
  Public Function CriaXmlRetornoAutorizacao(ByVal nRec As String, ByVal nNF As Integer, ByVal ambiente As AmbienteEnvioNFe) As Boolean
    Dim caminho_pasta As String = CriaPastaConsultaXml()
    Me.local_xml = caminho_pasta & "\" & nRec & "_" & nNF & "_consReciNFe.xml"
    Dim writer As New XmlTextWriter(Me.local_xml, System.Text.Encoding.UTF8)
    Dim tpAmb As Integer

    If ambiente = AmbienteEnvioNFe.Producao Then
      tpAmb = 1
    Else
      tpAmb = 2
    End If

    Try
      writer.WriteStartDocument(True) 'INICIO DOCUMENTO

      writer.WriteStartElement("consReciNFe", "http://www.portalfiscal.inf.br/nfe")
      writer.WriteAttributeString("versao", "4.00")

      writer.WriteStartElement("tpAmb")
      writer.WriteString(tpAmb)
      writer.WriteEndElement()

      writer.WriteStartElement("nRec")
      writer.WriteString(nRec)
      writer.WriteEndElement()

      writer.WriteEndElement() 'FIM consReciNFe

      writer.WriteEndDocument()


      writer.Close()
    Catch ex As Exception
      'MsgBox("ERRO AO GERAR O XML PARA CONSULTA DO STATUS DA NOTA FISCAL NO SEFAZ: " & ex.Message() & "----------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxxcont")
      writer.WriteStartElement("ERRO")
      writer.WriteString("ERRO AO GERAR O XML PARA CONSULTA DO STATUS DA NOTA FISCAL NO SEFAZ: " & ex.Message() & "----------" & ex.StackTrace())
      writer.WriteEndElement()
      Return False
    End Try

    Return True
  End Function




  'CARTA DE CORREÇÃO
  ''' <summary>
  ''' Retorno xMotivo, caso FALSE...
  ''' Retorna caminho_pasta, caso TRUE...
  ''' </summary>
  ''' <param name="id_nf"></param>
  ''' <param name="xCorrecao"></param>
  ''' <returns></returns>
  Public Function CriaXmlCartaCorrecao(ByVal id_nf As Integer, ByVal xCorrecao As String, ByVal xCondUso As String) As Boolean
    Me.id_nf = id_nf

    Try
      Dim evento As New clsNFeEventos
      evento.PegaInfGerarCartaCorrecao(Me.id_nf)

      Me.caminho_pasta = CriaPastaConsultaXml()
      Me.caminho_pasta = Me.caminho_pasta & "/" & evento.sig_chNFe & "_" & evento.nNF & "_envEvento_CC.xml"


      Dim writer As New XmlTextWriter(Me.caminho_pasta, System.Text.Encoding.UTF8)

      writer.WriteStartDocument(True) 'INICIO DOCUMENTO

      writer.WriteStartElement("envEvento", "http://www.portalfiscal.inf.br/nfe")
      writer.WriteAttributeString("versao", "1.00")

      writer.WriteStartElement("idLote")
      writer.WriteString("1")
      writer.WriteEndElement()

      CriaXmlEventoCC(writer, evento.cUF, evento.tpAmb, evento.cnpj, evento.sig_chNFe, xCorrecao, evento.nSeqEvento, xCondUso)

      writer.WriteEndDocument() 'FIM do documento


      writer.Close()

    Catch ex As Exception
      Me.xMotivo = "ERRO AO PEGAR AS INFORMAÇÕES PARA CRIAÇÃO DO XML DA CARTA DE CORREÇÃO: " & ex.Message() & "-----------" & ex.StackTrace()

      Return False
    End Try


    Return True

  End Function

  Private Sub CriaXmlEventoCC(ByVal writer As XmlTextWriter, ByVal cUF As Integer, ByVal tpAmb As Integer, ByVal cnpj As String, _
                              ByVal chNFe As String, ByVal xCorrecao As String, ByVal nSeqEvento As Integer, ByVal xCondUso As String)
    Dim ajuste As New clsAjuste

    writer.WriteStartElement("evento")
    writer.WriteAttributeString("xmlns", "http://www.portalfiscal.inf.br/nfe")
    writer.WriteAttributeString("versao", "1.00")

    writer.WriteStartElement("infEvento")
    writer.WriteAttributeString("Id", "ID110110" & chNFe & Right("00" & nSeqEvento, 2))

    writer.WriteStartElement("cOrgao")
    writer.WriteString(cUF)
    writer.WriteEndElement()

    writer.WriteStartElement("tpAmb")
    writer.WriteString(tpAmb)
    writer.WriteEndElement()

    If Len(cnpj) = 11 Then 'CPF - pessoa física
      writer.WriteStartElement("CPF")
      writer.WriteString(cnpj)
      writer.WriteEndElement()
    Else 'CNPJ - pessoa jurídica
      writer.WriteStartElement("CNPJ")
      writer.WriteString(cnpj)
      writer.WriteEndElement()
    End If

    writer.WriteStartElement("chNFe")
    writer.WriteString(chNFe)
    writer.WriteEndElement()

    writer.WriteStartElement("dhEvento")
    writer.WriteString(ajuste.PegaDataAtual(TipoData.AAAAMMDD).Replace("/", "-") & "T" & Right("00" & Hour(Date.Now()), 2) & ":" & Right("00" & Minute(Date.Now()), 2) & ":" & Right("00" & Second(Date.Now()), 2) & "-03:00")
    writer.WriteEndElement()

    writer.WriteStartElement("tpEvento")
    writer.WriteString("110110")
    writer.WriteEndElement()

    writer.WriteStartElement("nSeqEvento")
    writer.WriteString(nSeqEvento)
    writer.WriteEndElement()

    writer.WriteStartElement("verEvento")
    writer.WriteString("1.00")
    writer.WriteEndElement()


    writer.WriteStartElement("detEvento")
    writer.WriteAttributeString("versao", "1.00")

    writer.WriteStartElement("descEvento")
    writer.WriteString("Carta de Correção")
    writer.WriteEndElement()

    writer.WriteStartElement("xCorrecao")
    writer.WriteString(xCorrecao)
    writer.WriteEndElement()

    writer.WriteStartElement("xCondUso")
    writer.WriteString(xCondUso)
    writer.WriteEndElement()


    writer.WriteEndElement() 'FIM detEvento

    writer.WriteEndElement() 'FIM infEvento

    writer.WriteEndElement() 'FIM evento
  End Sub



  'CRIA XML CANCELAMENTO
  Public Function CriaXmlCancelamento(ByVal id_nf As Integer, ByVal xJust As String) As Boolean
    Me.id_nf = id_nf

    Try
      Dim evento As New clsNFeEventos
      evento.PegaInfGerarCancelamentoNFe(Me.id_nf)

      Me.caminho_pasta = CriaPastaConsultaXml()
      Me.caminho_pasta = Me.caminho_pasta & "/" & evento.sig_chNFe & "_" & evento.nNF & "_envEvento_CANC.xml"


      Dim writer As New XmlTextWriter(Me.caminho_pasta, System.Text.Encoding.UTF8)

      writer.WriteStartDocument(True) 'INICIO DOCUMENTO

      writer.WriteStartElement("envEvento", "http://www.portalfiscal.inf.br/nfe")
      writer.WriteAttributeString("versao", "1.00")

      writer.WriteStartElement("idLote")
      writer.WriteString("1")
      writer.WriteEndElement()

      CriaXmlEventoCancelamento(writer, evento.cUF, evento.tpAmb, evento.cnpj, evento.sig_chNFe, evento.sig_nProt, evento.nSeqEvento, xJust)

      writer.WriteEndDocument() 'FIM do documento


      writer.Close()

    Catch ex As Exception
      Me.xMotivo = "ERRO AO PEGAR AS INFORMAÇÕES PARA CRIAÇÃO DO XML DA CARTA DE CORREÇÃO: " & ex.Message() & "-----------" & ex.StackTrace()

      Return False
    End Try


    Return True

  End Function

  Private Sub CriaXmlEventoCancelamento(ByVal writer As XmlTextWriter, ByVal cUF As Integer, ByVal tpAmb As Integer, ByVal cnpj As String, _
                                        ByVal chNFe As String, ByVal nProt As String, ByVal nSeqEvento As Integer, ByVal xJust As String)
    Dim ajuste As New clsAjuste

    writer.WriteStartElement("evento")
    writer.WriteAttributeString("xmlns", "http://www.portalfiscal.inf.br/nfe")
    writer.WriteAttributeString("versao", "1.00")

    writer.WriteStartElement("infEvento")
    writer.WriteAttributeString("Id", "ID110111" & chNFe & Right("00" & nSeqEvento, 2))

    writer.WriteStartElement("cOrgao")
    writer.WriteString(cUF)
    writer.WriteEndElement()

    writer.WriteStartElement("tpAmb")
    writer.WriteString(tpAmb)
    writer.WriteEndElement()

    If Len(cnpj) = 11 Then 'CPF - pessoa física
      writer.WriteStartElement("CPF")
      writer.WriteString(cnpj)
      writer.WriteEndElement()
    Else 'CNPJ - pessoa jurídica
      writer.WriteStartElement("CNPJ")
      writer.WriteString(cnpj)
      writer.WriteEndElement()
    End If

    writer.WriteStartElement("chNFe")
    writer.WriteString(chNFe)
    writer.WriteEndElement()

    writer.WriteStartElement("dhEvento")
    writer.WriteString(ajuste.PegaDataAtual(TipoData.AAAAMMDD).Replace("/", "-") & "T" & Right("00" & Hour(Date.Now()), 2) & ":" & Right("00" & Minute(Date.Now()), 2) & ":" & Right("00" & Second(Date.Now()), 2) & "-03:00")
    writer.WriteEndElement()

    writer.WriteStartElement("tpEvento")
    writer.WriteString("110111")
    writer.WriteEndElement()

    writer.WriteStartElement("nSeqEvento")
    writer.WriteString(nSeqEvento)
    writer.WriteEndElement()

    writer.WriteStartElement("verEvento")
    writer.WriteString("1.00")
    writer.WriteEndElement()


    writer.WriteStartElement("detEvento")
    writer.WriteAttributeString("versao", "1.00")

    writer.WriteStartElement("descEvento")
    writer.WriteString("Cancelamento")
    writer.WriteEndElement()

    writer.WriteStartElement("nProt")
    writer.WriteString(nProt)
    writer.WriteEndElement()

    writer.WriteStartElement("xJust")
    writer.WriteString(xJust)
    writer.WriteEndElement()


    writer.WriteEndElement() 'FIM detEvento

    writer.WriteEndElement() 'FIM infEvento

    writer.WriteEndElement() 'FIM evento
  End Sub



  'CRIAÇÃO XML INUTILIZAÇÃO
  Public Sub CriaXmlInutilizacao(ByVal id_empresa As Integer, ByVal serie As Integer, ByVal tpAmb As Integer, _
                                 ByVal nNFIni As Integer, ByVal nNFFin As Integer, ByVal ano As Integer, ByVal xJust As String)

    Try
      Dim evento As New clsNFeEventos
      evento.PegaInfGerarInutilizacaoNFe(id_empresa)

      Me.caminho_pasta = CriaPastaConsultaXml()
      Me.caminho_pasta = Me.caminho_pasta & "/" & nNFIni & "_" & nNFFin & "_inutNFe_INUTI.xml"

      Dim writer As New XmlTextWriter(Me.caminho_pasta, System.Text.Encoding.UTF8)

      writer.WriteStartDocument(True) 'INICIO DOCUMENTO

      writer.WriteStartElement("inutNFe", "http://www.portalfiscal.inf.br/nfe")
      writer.WriteAttributeString("versao", "2.00")

      writer.WriteStartElement("idLote")
      writer.WriteString("1")
      writer.WriteEndElement()


      CriaXmlEventoInutilizacao(writer, tpAmb, evento.cUF, evento.cnpj_emp, serie, nNFIni, nNFFin, ano, xJust)

      writer.WriteEndDocument() 'FIM do documento


      writer.Close()

    Catch ex As Exception
      Me.result = -1
      Me.xMotivo = "ERRO AO GERAR O XML DE INUTILIZAÇÃO: " & ex.Message() & "----------" & ex.StackTrace()
    End Try
  End Sub

  Private Sub CriaXmlEventoInutilizacao(ByVal writer As XmlTextWriter, ByVal tpAmb As Integer, ByVal cUF As Integer, _
                                        ByVal CNPJ As String, ByVal serie As Integer, ByVal nNFIni As Integer, _
                                        ByVal nNFFin As Integer, ByVal ano As Integer, ByVal xJust As String)

    writer.WriteStartElement("infInut")
    writer.WriteAttributeString("Id", "ID" & cUF & Right(ano, 2) & CNPJ & "55" & "00" & serie & Right("000000000" & nNFIni, 9) & Right("000000000" & nNFFin, 9))


    writer.WriteStartElement("tpAmb")
    writer.WriteString(tpAmb)
    writer.WriteEndElement()

    writer.WriteStartElement("xServ")
    writer.WriteString("INUTILIZAR")
    writer.WriteEndElement()

    writer.WriteStartElement("cUF")
    writer.WriteString(cUF)
    writer.WriteEndElement()

    writer.WriteStartElement("ano")
    writer.WriteString(ano)
    writer.WriteEndElement()

    writer.WriteStartElement("CNPJ")
    writer.WriteString(CNPJ)
    writer.WriteEndElement()

    writer.WriteStartElement("mod")
    writer.WriteString("55")
    writer.WriteEndElement()

    writer.WriteStartElement("serie")
    writer.WriteString(serie)
    writer.WriteEndElement()

    writer.WriteStartElement("nNFIni")
    writer.WriteString(nNFIni)
    writer.WriteEndElement()

    writer.WriteStartElement("nNFFin")
    writer.WriteString(nNFFin)
    writer.WriteEndElement()

    writer.WriteStartElement("xJust")
    writer.WriteString(xJust)
    writer.WriteEndElement()


    writer.WriteEndElement() 'FIM infInut

  End Sub



  'CRIAÇÃO DE PASTAS
  Private Function CriaPastaCartaCorrecao() As String
    Dim ajuste As New clsAjuste
    Dim ano As Integer = Year(Date.Now())
    Dim mes As String = Month(Date.Now())
    Dim caminho_pasta As String = ""

    Try
      caminho_pasta = ajuste.CriaPastaNFeCartaCorrecao(ano, mes)
    Catch ex As Exception
      'MsgBox("ERRO AO CRIAR PASTA DE CONSULTA DO XML:" & ex.Message() & "-------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
    End Try

    Return caminho_pasta

  End Function

  Private Function CriaPastaConsultaXml() As String
    Dim ajuste As New clsAjuste
    Dim ano As Integer = Year(Date.Now())
    Dim mes As String = Month(Date.Now())
    Dim caminho_pasta As String = ""

    Try
      caminho_pasta = ajuste.CriaPastaXmlNfeConsulta(ano, mes)
    Catch ex As Exception
      'MsgBox("ERRO AO CRIAR PASTA DE CONSULTA DO XML:" & ex.Message() & "-------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
    End Try

    Return caminho_pasta

  End Function
End Class
