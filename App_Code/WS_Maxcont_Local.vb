Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml
Imports System.Xml.Xsl
Imports clsEnum
Imports System.IO

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://www.maxcont.com.br/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class WS_Maxcont_Local
  Inherits System.Web.Services.WebService

  <WebMethod(Description:="Lista as notas fiscais pendentes de uma empresa para envio ao Sefaz")> _
  Public Function ListaNFePendente(ByVal id_empresa As Integer) As XmlElement
    Dim doc As New XmlDocument
    Dim nfe As New clsNFEide

    doc = nfe.ListaNFePendenteEnvio(id_empresa)

    Return doc.DocumentElement
  End Function

  <WebMethod(Description:="Lista as notas fiscais pendentes de mais de uma empresa para envio ao Sefaz... separar o id_empresa por |")> _
  Public Function ListaNFePendenteAut(ByVal str_id_empresa As String) As XmlElement
    Dim doc As New XmlDocument
    Dim nfe As New clsNFEide

    doc = nfe.ListaNFePendenteEnvioAut(str_id_empresa)

    Return doc.DocumentElement
  End Function

  <WebMethod()> _
  Public Function ListaLotesNFServPendente(ByVal id_empresa As Integer) As XmlElement
    Dim doc As New XmlDocument
    Dim nfe As New clsNFEide

    doc = nfe.ListaLoteNFServPendente(id_empresa)

    Return doc.DocumentElement
  End Function

  <WebMethod(Description:="Cria o Xml para envio da nota fiscal")> _
  Public Function CriaXMLNotaFiscal(ByVal id_empresa As Integer, ByVal id_nf As Integer, ByVal nNF As Integer) As XmlElement
    Dim doc As New XmlDocument
    Dim cria_xml As New clsCriaXmlNFeVersao4
    Dim ajuste As New clsAjuste
    'Dim caminho_pasta As String = ajuste.GetFilePathFaturamentoSaveNFe(id_empresa)

    'SERVIDOR
    'Dim caminho_salva As String = ajuste.GetFilePathFaturamentoSaveNFe(id_empresa)
    'Dim caminho_salva_temp As String = ajuste.GetFilePathTempEmpresa(id_empresa)

    'TESTE LOCAL
    Dim caminho_salva_temp = "C:\inetpub\wwwroot\Maxcont2017\Temp"
    Dim caminho_salva As String = "C:\inetpub\wwwroot\Maxcont2017\Temp"

    Dim aaaamm As String

    aaaamm = Year(Date.Now()) & Right("00" & Month(Date.Now()), 2)
    caminho_salva = caminho_salva & aaaamm

    If Not System.IO.Directory.Exists(caminho_salva) Then 'Cria a pasta no diretório com o ano e mês
      System.IO.Directory.CreateDirectory(caminho_salva)
    End If

    'Try
    'cria_xml.CriaXmlNFe(id_nf, id_empresa, nNF, caminho_salva)
    cria_xml.CriaXmlNFe(id_nf, id_empresa, nNF, caminho_salva_temp)

    doc.Load(cria_xml.caminho_pasta) 'O caminho completo de onde se encontra o XML...     
    'Catch ex As System.Exception
    'Throw New SoapException("Hello World Exception", SoapException.ServerFaultCode, "HelloWorld", ex)
    'End Try			

    Return doc.DocumentElement
  End Function

  <WebMethod(Description:="Pega todas as informações necessárias, do lote selecionado, para criação do XML da nota fiscal de serviço")> _
  Public Function CriaXmlNotaServico(ByVal id_empresa As Integer, ByVal id_lote As Integer) As XmlElement
    Dim doc As New XmlDocument
    Dim cria_xml As New clsCriaXmlNotaServico
    Dim ajuste As New clsAjuste

    Dim caminho_salva As String = ajuste.GetFilePathFaturamentoSaveNFe(id_empresa)
    Dim aaaamm As String

    aaaamm = Year(Date.Now()) & Right("00" & Month(Date.Now()), 2)
    caminho_salva = caminho_salva & aaaamm

    If Not System.IO.Directory.Exists(caminho_salva) Then 'Cria a pasta no diretório com o ano e mês
      System.IO.Directory.CreateDirectory(caminho_salva)
    End If

    cria_xml.CriaXml(id_empresa, id_lote, caminho_salva)

    doc.Load(cria_xml.caminho_pasta) 'O caminho completo de onde se encontra o XML...     

    Return doc.DocumentElement

  End Function

  <WebMethod()> _
  Public Function PegaUmaNFe(ByVal id_empresa As Integer, ByVal nNF As Integer) As XmlElement
    Dim doc As New XmlDocument()
    Dim nfe As New clsNFEide

    doc = nfe.CriaXmlUmaNFe(id_empresa, nNF)

    Return doc.DocumentElement

  End Function

  <WebMethod()> _
  Public Function PegaInfCartaCorrecao(ByVal id_nf As Integer) As XmlElement
    Dim doc As New XmlDocument()
    Dim evento As New clsNFeEventos
    Dim notas As XmlElement
    Dim cUF, tpAmb, sig_chNFe, cnpj, nSeqEvento As XmlElement


    evento.PegaInfGerarCartaCorrecao(id_nf)

    notas = doc.CreateElement("CartaCorrecao")
    doc.AppendChild(notas)

    cUF = doc.CreateElement("cUF")
    cUF.InnerText = evento.cUF
    notas.AppendChild(cUF)

    tpAmb = doc.CreateElement("tpAmb")
    tpAmb.InnerText = evento.tpAmb
    notas.AppendChild(tpAmb)

    sig_chNFe = doc.CreateElement("sig_chNFe")
    sig_chNFe.InnerText = evento.sig_chNFe
    notas.AppendChild(sig_chNFe)

    cnpj = doc.CreateElement("cnpj")
    cnpj.InnerText = Right("00000000000000" & evento.cnpj, 14) 'Completa com 0 a esquerda, devido cnpj ser inteiro
    notas.AppendChild(cnpj)

    nSeqEvento = doc.CreateElement("nSeqEvento")
    nSeqEvento.InnerText = evento.nSeqEvento
    notas.AppendChild(nSeqEvento)

    Return doc.DocumentElement
  End Function

  <WebMethod()> _
  Public Function PegaInfCancelamento(ByVal id_nf As Integer) As XmlElement
    Dim doc As New XmlDocument()
    Dim evento As New clsNFeEventos
    Dim notas As XmlElement
    Dim cUF, tpAmb, sig_chNFe, sig_nProt, cnpj, nSeqEvento As XmlElement

    evento.PegaInfGerarCancelamentoNFe(id_nf)

    notas = doc.CreateElement("Cancelamento")
    doc.AppendChild(notas)

    cUF = doc.CreateElement("cUF")
    cUF.InnerText = evento.cUF
    notas.AppendChild(cUF)

    tpAmb = doc.CreateElement("tpAmb")
    tpAmb.InnerText = evento.tpAmb
    notas.AppendChild(tpAmb)

    sig_chNFe = doc.CreateElement("sig_chNFe")
    sig_chNFe.InnerText = evento.sig_chNFe
    notas.AppendChild(sig_chNFe)

    sig_nProt = doc.CreateElement("sig_nProt")
    sig_nProt.InnerText = evento.sig_nProt
    notas.AppendChild(sig_nProt)

    cnpj = doc.CreateElement("cnpj")
    cnpj.InnerText = Right("00000000000000" & evento.cnpj, 14) 'Completa com 0 a esquerda, devido cnpj ser inteiro
    notas.AppendChild(cnpj)

    nSeqEvento = doc.CreateElement("nSeqEvento")
    nSeqEvento.InnerText = evento.nSeqEvento
    notas.AppendChild(nSeqEvento)

    Return doc.DocumentElement
  End Function

  <WebMethod(Description:="Recepção do arquivo XML de retorno do Sefaz, com o motivo do erro na transmissão ou na validação")> _
  Public Function RecepcaoXmlErroNFe(ByVal id_empresa As Integer, ByVal id_nf As Integer, ByVal doc As XmlDocument) As Boolean
    Dim ajuste As New clsAjuste
    Dim caminho_salva As String = ajuste.GetFilePathTemp() 'Pega o caminho completo da pasta Temp
    Dim aaaamm As String

    caminho_salva = caminho_salva & "ErroEmissaoNFe.xml" 'Inser o nome do arquivo no caminho...
    doc.Save(caminho_salva)

    Dim xml As New clsXmlRetornoStatusSefaz()

    xml.ArquivoRetornoConsReciNFe(id_empresa, id_nf, caminho_salva)

    Return True
  End Function

  <WebMethod(Description:="Recepção do arquivo XML de retorno do Sefaz, com o motivo do erro na transmissão ou na validação, retornando a Solução para o erro")> _
  Public Function RecepcaoXmlErroNFe2(ByVal id_empresa As Integer, ByVal id_nf As Integer, ByVal doc As XmlDocument) As String
    Dim ajuste As New clsAjuste
    Dim caminho_salva As String = ajuste.GetFilePathTemp() 'Pega o caminho completo da pasta Temp
    Dim aaaamm As String
    Dim xSolucao As String = ""

    caminho_salva = caminho_salva & "ErroEmissaoNFe.xml" 'Inser o nome do arquivo no caminho...
    doc.Save(caminho_salva)

    Dim xml As New clsXmlRetornoStatusSefaz()

    Try
      xml.ArquivoRetornoConsReciNFe(id_empresa, id_nf, caminho_salva)
      xSolucao = xml.xSolucao

    Catch ex As Exception

    End Try

    Return xSolucao
  End Function

  <WebMethod(Description:="Recepção do arquivo XML da nota fiscal eletrônica com a confirmação do Sefaz")> _ 
  Public Function RecepcaoXmlNFe(ByVal id_empresa As Integer, ByVal id_nf As Integer, ByVal doc As XmlDocument, _
                                 ByVal nome_arquivo As String) As Boolean
    Dim ajuste As New clsAjuste
    Dim caminho_salva As String = ajuste.GetFilePathFaturamentoSaveNFe(id_empresa)
    Dim aaaamm As String

    aaaamm = Year(Date.Now()) & Right("00" & Month(Date.Now()), 2)
    caminho_salva = caminho_salva & aaaamm

    If Not System.IO.Directory.Exists(caminho_salva) Then 'Cria a pasta no diretório com o ano e mês
      System.IO.Directory.CreateDirectory(caminho_salva)
    End If

    caminho_salva = caminho_salva & "\" & nome_arquivo 'Inser o nome do arquivo no caminho...
    doc.Save(caminho_salva)

    Dim xml As New clsXmlRetornoStatusSefaz()

    xml.ArquivoRetornoConsReciNFe(id_empresa, id_nf, caminho_salva)

    If xml.xMotivo <> "" And xml.xMotivo <> "Autorizado o uso da NF-e" Then
      ajuste.SalvaErroLog(id_empresa, id_nf, xml.xMotivo)
    End If

    Dim gravaXML As New clsControleXML
    gravaXML.SalvaXML(id_nf, 1, doc.InnerXML)

    Return True
  End Function

  <WebMethod(Description:="Recepção do arquivo XML de inutilização com a confirmação do Sefaz")> _
  Public Function RecepcaoXmlInutiliza(ByVal id_empresa As Integer, ByVal doc As XmlDocument, ByVal xJust As String, _
                     ByVal nome_arquivo As String) As Boolean
    Dim ajuste As New clsAjuste
    Dim caminho_salva As String = ajuste.GetFilePathFaturamentoSaveNFe(id_empresa)
    Dim aaaamm As String

    aaaamm = Year(Date.Now()) & Right("00" & Month(Date.Now()), 2)
    caminho_salva = caminho_salva & aaaamm

    If Not System.IO.Directory.Exists(caminho_salva) Then 'Cria a pasta no diretório com o ano e mês
      System.IO.Directory.CreateDirectory(caminho_salva)
    End If

    caminho_salva = caminho_salva & "\" & nome_arquivo 'Inser o nome do arquivo no caminho...

    doc.Save(caminho_salva)

    Dim xml As New clsXmlRetornoStatusSefaz()

    If Not xml.ArquivoRetornoRetInutNFe(id_empresa, xJust, caminho_salva) Then
      Return False
    End If

    Return True
  End Function

  <WebMethod(Description:="Recepção do arquivo XML da carta de correção com a confirmação do evento ao Sefaz.")> _
  Public Function RecepcaoXmlCartaCorrecao(ByVal id_empresa As Integer, ByVal doc As XmlDocument, ByVal nome_arquivo As String, _
                                           ByVal xCorrecao As String) As Boolean
    Dim ajuste As New clsAjuste
    Dim caminho_salva As String = ajuste.GetFilePathFaturamentoSaveNFe(id_empresa)
    Dim aaaamm As String

    aaaamm = Year(Date.Now()) & Right("00" & Month(Date.Now()), 2)
    caminho_salva = caminho_salva & aaaamm

    If Not System.IO.Directory.Exists(caminho_salva) Then 'Cria a pasta no diretório com o ano e mês
      System.IO.Directory.CreateDirectory(caminho_salva)
    End If

    caminho_salva = caminho_salva & "\" & nome_arquivo 'Inser o nome do arquivo no caminho...

    doc.Save(caminho_salva)

    Dim xml As New clsXmlRetornoStatusSefaz()
    Dim xCondUso As String = "A Carta de Correção é disciplinada pelo § 1º-A do art. 7º do Convênio S/N, de 15 de dezembro de 1970 e pode ser utilizada para regularização de erro ocorrido na emissão de documento fiscal, desde que o erro não esteja relacionado com: I - as variáveis que determinam o valor do imposto tais como: base de cálculo, alíquota, diferença de preço, quantidade, valor da operação ou da prestação; II - a correção de dados cadastrais que implique mudança do remetente ou do destinatário; III - a data de emissão ou de saída."

    If Not xml.ArquivoRetornoRetEnvEvento(id_empresa, caminho_salva, xCorrecao, xCondUso, "") = 0 Then
      Return False
    End If

    Return True
  End Function

  <WebMethod(Description:="Recepção do arquivo XML do cancelamento com a confirmação do evento no Sefaz")> _
  Public Function RecepcaoXmlCancelamento(ByVal id_empresa As Integer, ByVal doc As XmlDocument, ByVal nome_arquivo As String, _
                                          ByVal xJust As String) As Boolean
    Dim ajuste As New clsAjuste
    Dim caminho_salva As String = ajuste.GetFilePathFaturamentoSaveNFe(id_empresa)
    Dim aaaamm As String

    aaaamm = Year(Date.Now()) & Right("00" & Month(Date.Now()), 2)
    caminho_salva = caminho_salva & aaaamm

    If Not System.IO.Directory.Exists(caminho_salva) Then 'Cria a pasta no diretório com o ano e mês
      System.IO.Directory.CreateDirectory(caminho_salva)
    End If

    caminho_salva = caminho_salva & "\" & nome_arquivo 'Inser o nome do arquivo no caminho...

    doc.Save(caminho_salva)

    Dim xml As New clsXmlRetornoStatusSefaz()

    If Not xml.ArquivoRetornoRetEnvEvento(id_empresa, caminho_salva, "", "", xJust) = 0 Then
      Return False
    End If

    Return True
  End Function

  <WebMethod(Description:="Pega os dados de uma empresa")> _
  Public Function PegaDadosEmpresa(ByVal empresa As String, ByVal login As String, ByVal senha As String) As XmlElement
    Dim doc As New XmlDocument()
    Dim acesso As New clsLogin()
    Dim cls_empresa As New clsEmpresa

    Dim ListaEmpresas As XmlElement
    Dim DadosEmpresa, cStat, xMotivo, id_empresa, descr_empresa, cnpj_emp, cmun, uf As XmlElement
    Dim sts_bloq, cUF, fNFe_Dest As XmlElement

    Dim status As Integer = acesso.AcessaSistema(empresa, login, senha) 'Verifica as informações...

    ListaEmpresas = cls_empresa.PegaInfEmpresa(acesso.id_empresa, status)
    
    Return ListaEmpresas
  End Function

  <WebMethod(Description:="Lista as notas a serem inutilizadas")> _
  Public Function ListaNotasInutiliza(ByVal id_empresa As Integer) As XmlElement
    Dim doc As New XmlDocument()
    Dim nfe As New clsNFEide()

    doc = nfe.CriaXmlNFeParaInutilizacao(id_empresa, 2000, 1)

    Return doc.DocumentElement
  End Function

  <WebMethod(Description:="Pega o último número gerado de NSU para o Manifesto")> _
  Public Function PegaUltimoNSU(ByVal id_empresa As Integer) As Integer
    Dim NSU As Integer = 0
    Dim empresa As New clsEmpresa

    NSU = empresa.PegaUltimoNSU(id_empresa)

    Return NSU
  End Function

  <WebMethod(Description:="Retorna a versão atual do emissor NFe publicada para donwload")> _
  Public Function ConsultaVersaoAtualEmissor(ByVal id_empresa As Integer) As String
    Dim versao As String = ""
    Dim empresa As New clsEmpresa

    versao = empresa.PegaUltimaVersao(id_empresa)

    Return versao
  End Function

  <WebMethod(Description:="Retorna o xml com as informações atualizadas da empresa")> _
  Public Function AtualizaDadosEmpresa(ByVal id_empresa As Integer) As XmlElement
    Dim cls_empresa As New clsEmpresa
    Dim InfEmpresa As XmlElement

    InfEmpresa = cls_empresa.PegaInfEmpresa(id_empresa, 0)

    Return InfEmpresa
  End Function

End Class