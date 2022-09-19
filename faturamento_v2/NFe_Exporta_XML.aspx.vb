Imports System.Xml 
Imports System
Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration

'Página que gera arquivo XML que será integrado com o município
' - 4209102 - Joinville (Gera XML, carrega outro módulo de em ASP.NET)
' - 4106902 - Curitiba (Gera XML, carrega outro módulo de em ASP.NET)

Partial Class Municipio_faturamento_v2_Default
  Inherits System.Web.UI.Page

  Dim cnnSQL As String = ""
  Dim asp_id_empresa As String
  Dim asp_id_usuario As String
  Dim asp_id_lote As String
  Dim Session_ID As String = ""

  'busca string de conexão a partir do WebConfig
  Private Sub GetAllConnectionStrings()
      Dim collCS As ConnectionStringSettingsCollection
      Try
          collCS = ConfigurationManager.ConnectionStrings
      Catch ex As Exception
          collCS = Nothing
      End Try

      If collCS IsNot Nothing Then
          For Each cs As ConnectionStringSettings In collCS
            If cs.Name = "maxcont_cloud" then
              Session("cnxStr") = (cs.ConnectionString)
            Else
              Session("cnxStr") = ""
            end if
          Next
      Else
        Session("cnxStr") = ""
      End If

      collCS = Nothing
  End Sub

  Private Sub Carrega_Session (txt_Session As string)
    Dim strSQL As String = "EXEC spSession_Compartilha_Pega '" & txt_Session & "'"

    Using adapter As New SqlDataAdapter(strSQL, cnnSQL)
      Dim ds As New DataSet
      adapter.Fill(ds, "mResult")

      Dim conta As Integer = 0
      For Each dr As DataRow In ds.Tables("mResult").Rows

        asp_id_empresa = dr("id_empresa").ToString()
        asp_id_usuario = dr("id_usuario").ToString()
        asp_id_lote = dr("param1").ToString()

      Next
    End Using
  End Sub

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    GetAllConnectionStrings()

    cnnSQL = Session("cnxStr").ToString

    Session_ID = Request.QueryString("_Session_2").ToString.Replace(",","")

    Carrega_Session(Session_ID)

    Grava_XML()
  End Sub

  Private Sub Grava_XML()

    Dim CurrentPath As String = Server.MapPath("../../temp/" & Session_ID)
    
    If Not Directory.Exists(CurrentPath) Then
        ' Create the directory.
        Directory.CreateDirectory(CurrentPath)
    End If

    Dim CurrentFileName As String = "XML_" & Session_ID & ".xml"

    Dim sFilename As String = CurrentPath.Replace("\","/") & "/" & CurrentFileName

    Dim valor_iss As String = ""
    Dim iss_retido As String = ""
    Dim CNPJ As String = ""
    Dim Razao_Social As String = ""
    Dim cMun As String
    Dim CRT As String = ""
    Dim Regime_Especial As String = "" 'Regime Especial Abrasf
    Dim IM_Prestador As String = ""
    Dim pAliq_Simples_ISS As Decimal = 0
    Dim pAliq_Simples_ISS_Str As String = ""
    Dim vTotTrib_str As String = "" 'valor aproximado dos tributos
    Dim nro_tomador As String = ""

    Dim strSQL As String = "SELECT dbo.fLimpa_Numero(cnpj_emp) as cnpj, razao_emp, cmun, CRT, dbo.fLimpa_Numero(inmun) as inmun, pAliq_Simples_ISS FROM dbo.tbEmpresas WHERE (id_empresa = " & asp_id_empresa & ")"

    Using adapter As New SqlDataAdapter(strSQL, cnnSQL)
      Dim ds As New DataSet
      adapter.Fill(ds, "mInfo")

      Dim conta As Integer = 0
      For Each dr As DataRow In ds.Tables("mInfo").Rows

        CNPJ = dr("cnpj").ToString()
        Razao_Social = dr("razao_emp")
        cMun = dr("cmun").ToString()
        CRT = dr("CRT").ToString()
        IM_Prestador = dr("inmun")
        pAliq_Simples_ISS = dr("pAliq_Simples_ISS")
      Next
    end Using

    Dim XML_Document As New XmlDocument()

    'cMun = "4209102"

    If cMun = "999999" then 'TESTE
      Dim writer As new XmlDocument
      'Create XML declaration
      Dim declaration As XmlNode

      declaration = writer.CreateNode(XmlNodeType.XmlDeclaration, Nothing, Nothing)
      writer.AppendChild(declaration)

      'Make the root element first
      Dim root As XmlElement
      root = writer.CreateElement("Article")
      writer.AppendChild(root)

      'Creating the <Asp.net> element
      Dim aspnet As XmlElement
      aspnet = writer.CreateElement("Asp.net")
      root.AppendChild(aspnet)

      'Creating attributes of <Asp.net> element
      Dim id As XmlAttribute
      id = writer.CreateAttribute("Id")
      id.Value = "0001"
      aspnet.Attributes.Append(id)

      Dim title As XmlAttribute
      title = writer.CreateAttribute("Title")
      title.InnerText = "Bind Dropdown List"
      aspnet.Attributes.Append(title)

      Dim visit As XmlAttribute
      visit = writer.CreateAttribute("Visit")
      visit.InnerText = "985"
      aspnet.Attributes.Append(visit)

      Dim modified As XmlAttribute
      modified = writer.CreateAttribute("Modified")
      modified.InnerText = "Jan 01, 2009"
      aspnet.Attributes.Append(modified)

      'Now save/write the XML file into server root location
      'Server.MapPath(".") is used to find the root path
      writer.Save(sFilename)

      Response.write (sFilename)
      Response.end
    elseIf cMun = "4209102" then 'Joinville

      Dim XML_Raiz As XmlElement
      XML_Raiz = XML_Document.CreateElement("lote")
      XML_Document.AppendChild(XML_Raiz)

      'Dim XML_Raiz = XML_Document.CreateElement("lote")
      'XML_Document.AppendChild(XML_Raiz)

      Dim XML_Raiz_Atributo = XML_Document.CreateAttribute("xsi","schemaLocation","http://www.nfem.joinville.sc.gov.br rps_1.0.xsd")
      XML_Raiz_Atributo.value = "http://www.nfem.joinville.sc.gov.br rps_1.0.xsd"
      XML_Raiz.SetAttributeNode(XML_Raiz_Atributo)

      XML_Raiz_Atributo = XML_Document.CreateAttribute("xmlns:xsi")
      XML_Raiz_Atributo.value = "http://www.w3.org/2001/XMLSchema-instance"
      XML_Raiz.SetAttributeNode(XML_Raiz_Atributo)

      XML_Raiz_Atributo = XML_Document.CreateAttribute("xmlns")
      XML_Raiz_Atributo.value = "http://www.nfem.joinville.sc.gov.br"
      XML_Raiz.SetAttributeNode(XML_Raiz_Atributo)

      XML_Document.AppendChild(XML_Raiz)

      Dim XML_Node = XML_Document.CreateProcessingInstruction("xml", "version=""1.0"" encoding=""utf-8""")

      XML_Document.InsertBefore(XML_Node, XML_Raiz)

      Dim XML_Node_Filho = XML_Document.CreateElement("versao")
      XML_Raiz.AppendChild(XML_Node_Filho)
      XML_Node_Filho.InnerText = "1.0"

      XML_Node_Filho = XML_Document.CreateElement("numero")
      XML_Raiz.AppendChild(XML_Node_Filho)
      XML_Node_Filho.InnerText = asp_id_lote.ToString()

      XML_Node_Filho = XML_Document.CreateElement("tipo")
      XML_Raiz.AppendChild(XML_Node_Filho)
      XML_Node_Filho.InnerText = "1"

      XML_Node_Filho = XML_Document.CreateElement("prestador")
      XML_Raiz.AppendChild(XML_Node_Filho)

      Dim XML_Filho_conteudo = XML_Document.CreateElement("documento")
      XML_Filho_conteudo.InnerText = CNPJ
      XML_Node_Filho.AppendChild(XML_Filho_conteudo)

      XML_Filho_conteudo = XML_Document.CreateElement("razao_social")
      XML_Filho_conteudo.InnerText = Razao_Social
      XML_Node_Filho.AppendChild(XML_Filho_conteudo)

      strSQL = "EXEC sp9_Pega_NFE_Servico_Lote '" & asp_id_empresa & "','" & asp_id_lote & "','" & asp_id_usuario & "'"


      Dim XML_Filho_conteudo1 = XML_Document.CreateElement("documento")

      Using adapter As New SqlDataAdapter(strSQL, cnnSQL)
        Dim ds As New DataSet
        adapter.Fill(ds, "mRPS")

        Dim conta As Integer = 0
        For Each dr As DataRow In ds.Tables("mRPS").Rows
          XML_Node_Filho = XML_Document.CreateElement("rps")
          XML_Raiz.AppendChild(XML_Node_Filho)
          'XML_Filho_conteudo1.AppendChild(XML_Node_Filho)

          XML_Filho_conteudo = XML_Document.CreateElement("numero")
          XML_Filho_conteudo.InnerText = dr("serv_num_RPS").ToString()
          XML_Node_Filho.AppendChild(XML_Filho_conteudo)

          XML_Filho_conteudo = XML_Document.CreateElement("serie")
          XML_Filho_conteudo.InnerText = dr("serie_RPS").ToString()
          XML_Node_Filho.AppendChild(XML_Filho_conteudo)

          XML_Filho_conteudo = XML_Document.CreateElement("operacao")
          XML_Filho_conteudo.InnerText = "I" 'I = inserção
          XML_Node_Filho.AppendChild(XML_Filho_conteudo)

          XML_Filho_conteudo = XML_Document.CreateElement("tipo")
          XML_Filho_conteudo.InnerText = "1" '1 = Recibo
          XML_Node_Filho.AppendChild(XML_Filho_conteudo)

          XML_Filho_conteudo = XML_Document.CreateElement("data")
          XML_Filho_conteudo.InnerText = dr("dEmi").ToString()
          XML_Node_Filho.AppendChild(XML_Filho_conteudo)

          '------------- TOMADOR
          XML_Filho_conteudo = XML_Document.CreateElement("tomador")
          XML_Node_Filho.AppendChild(XML_Filho_conteudo)

          XML_Filho_conteudo1 = XML_Document.CreateElement("documento")
          XML_Filho_conteudo1.InnerText = dr("cnpj_tomador_fmt").ToString()
          XML_Filho_conteudo.AppendChild(XML_Filho_conteudo1)

          XML_Filho_conteudo1 = XML_Document.CreateElement("nome")
          XML_Filho_conteudo1.InnerText = dr("xNome_tomador").ToString()
          XML_Filho_conteudo.AppendChild(XML_Filho_conteudo1)

          If dr("IM_tomador").ToString() <> "" then
            XML_Filho_conteudo1 = XML_Document.CreateElement("inscricao_municipal")
            XML_Filho_conteudo1.InnerText = dr("IM_tomador").ToString()
            XML_Filho_conteudo.AppendChild(XML_Filho_conteudo1)
          end if

          If dr("email_tomador").ToString() <> "" then
            XML_Filho_conteudo1 = XML_Document.CreateElement("email")
            XML_Filho_conteudo1.InnerText = dr("email_tomador").ToString()
            XML_Filho_conteudo.AppendChild(XML_Filho_conteudo1)
          end if

          XML_Filho_conteudo1 = XML_Document.CreateElement("situacao_especial")
          XML_Filho_conteudo1.InnerText = "0" 'Outro
          XML_Filho_conteudo.AppendChild(XML_Filho_conteudo1)

          If dr("CEP_tomador").ToString() <> "" then
            XML_Filho_conteudo1 = XML_Document.CreateElement("cep")
            XML_Filho_conteudo1.InnerText = dr("CEP_tomador").ToString()
            XML_Filho_conteudo.AppendChild(XML_Filho_conteudo1)
          end if

          If dr("xLgr_tomador").ToString() <> "" then
            XML_Filho_conteudo1 = XML_Document.CreateElement("endereco")
            XML_Filho_conteudo1.InnerText = dr("xLgr_tomador").ToString()
            XML_Filho_conteudo.AppendChild(XML_Filho_conteudo1)
          end if

          If dr("nro_tomador").ToString() <> "" then
            XML_Filho_conteudo1 = XML_Document.CreateElement("numero")
            If dr("nro_tomador").ToString().Length > 10
              XML_Filho_conteudo1.InnerText = dr("nro_tomador").ToString().Substring(0, 10)
            Else
              XML_Filho_conteudo1.InnerText = dr("nro_tomador").ToString()
            End If
            XML_Filho_conteudo.AppendChild(XML_Filho_conteudo1)
          end if

          If dr("xCpl_tomador").ToString() <> "" then
            XML_Filho_conteudo1 = XML_Document.CreateElement("complemento")
            XML_Filho_conteudo1.InnerText = dr("xCpl_tomador").ToString()
            XML_Filho_conteudo.AppendChild(XML_Filho_conteudo1)
          end if

          If dr("xBairro_tomador").ToString() <> "" then
            XML_Filho_conteudo1 = XML_Document.CreateElement("bairro")
            XML_Filho_conteudo1.InnerText = dr("xBairro_tomador").ToString()
            XML_Filho_conteudo.AppendChild(XML_Filho_conteudo1)
          end if

          'Não é Joinville - Preenche dados do município e UF do tomador
          If dr("cMun_Tomador").ToString() <> "4209102" then
            If dr("xMun_tomador").ToString() <> "" then
              XML_Filho_conteudo1 = XML_Document.CreateElement("cidade")
              XML_Filho_conteudo1.InnerText = dr("xMun_tomador").ToString()
              XML_Filho_conteudo.AppendChild(XML_Filho_conteudo1)
            end if

            If dr("UF_tomador").ToString() <> "" then
              XML_Filho_conteudo1 = XML_Document.CreateElement("estado")
              XML_Filho_conteudo1.InnerText = dr("UF_tomador").ToString()
              XML_Filho_conteudo.AppendChild(XML_Filho_conteudo1)
            end if
          end if

          If dr("xPais_tomador").ToString() <> "BRASIL" then
            XML_Filho_conteudo1 = XML_Document.CreateElement("pais")
            XML_Filho_conteudo1.InnerText = dr("xPais_tomador").ToString()
            XML_Filho_conteudo.AppendChild(XML_Filho_conteudo1)
          end if

          'valor aproximado dos tributos
          If dr("vTotTrib") = 0 then
            vTotTrib_str = ""
          Else
            vTotTrib_str = " | VALOR APROXIMADO DE TRIBUTOS FEDERAIS, ESTADUAIS E MUNICIPAIS CONFORME DISPOSTO NA LEI Nº 12.741/12: " & formatnumber(dr("vTotTrib"),2).ToString()
          End If

          XML_Filho_conteudo = XML_Document.CreateElement("descricao_servicos")
          XML_Filho_conteudo.InnerText = dr("descr_serv").ToString() & vTotTrib_str
          XML_Node_Filho.AppendChild(XML_Filho_conteudo)

          XML_Filho_conteudo = XML_Document.CreateElement("valor_total")
          XML_Filho_conteudo.InnerText = Retorna_Num(formatnumber(dr("ISSQNTot_vServ"),2).ToString())
          XML_Node_Filho.AppendChild(XML_Filho_conteudo)

          If dr("ISSQNtot_vDeducoes") <> 0 then
            XML_Filho_conteudo = XML_Document.CreateElement("valor_deducao")
            XML_Filho_conteudo.InnerText = Retorna_Num(formatnumber(dr("ISSQNtot_vDeducoes"),2).ToString())
            XML_Node_Filho.AppendChild(XML_Filho_conteudo)
          End If

          XML_Filho_conteudo = XML_Document.CreateElement("servico")
          XML_Filho_conteudo.InnerText = dr("cod_serv").ToString()
          XML_Node_Filho.AppendChild(XML_Filho_conteudo)

          'Município de prestação não é Joinville
          If dr("cMunFG").ToString() <> "4209102" then
            If dr("xMunFG").ToString() <> "" then
              XML_Filho_conteudo = XML_Document.CreateElement("local_servico")
              XML_Filho_conteudo.InnerText = dr("xMunFG").ToString()
              XML_Node_Filho.AppendChild(XML_Filho_conteudo)
            end if
          end if

          'Serviço Prestado no Exterior
          If dr("cMunFG").ToString() = "9999999" then
            XML_Filho_conteudo = XML_Document.CreateElement("pais_servico")
            XML_Filho_conteudo.InnerText = "1"
            XML_Node_Filho.AppendChild(XML_Filho_conteudo)
          end if

          XML_Filho_conteudo = XML_Document.CreateElement("aliquota_iss")
          XML_Filho_conteudo.InnerText = Retorna_Num(formatnumber(dr("ISSQN_vAliq"),2).ToString())
          XML_Node_Filho.AppendChild(XML_Filho_conteudo)

          If dr("ISSQN_cSitTrib").ToString() = "R" then
            iss_retido = "1"
          Else
            iss_retido = "0"
          End If

          XML_Filho_conteudo = XML_Document.CreateElement("valor_iss")
          XML_Filho_conteudo.InnerText = Retorna_Num(formatnumber(dr("ISSQN_vISSQN"),2).ToString())
          XML_Node_Filho.AppendChild(XML_Filho_conteudo)

          XML_Filho_conteudo = XML_Document.CreateElement("iss_retido")
          XML_Filho_conteudo.InnerText = iss_retido
          XML_Node_Filho.AppendChild(XML_Filho_conteudo)

          If dr("retTrib_vIRRF") <> 0 then
            XML_Filho_conteudo = XML_Document.CreateElement("valor_irrf")
            XML_Filho_conteudo.InnerText = Retorna_Num(formatnumber(dr("retTrib_vIRRF"),2).ToString())
            XML_Node_Filho.AppendChild(XML_Filho_conteudo)
          End If

          If dr("retTrib_vRetPrev") <> 0 then
            XML_Filho_conteudo = XML_Document.CreateElement("valor_inss")
            XML_Filho_conteudo.InnerText = Retorna_Num(formatnumber(dr("retTrib_vRetPrev"),2).ToString())
            XML_Node_Filho.AppendChild(XML_Filho_conteudo)
          End If

          If dr("retTrib_vRetPIS") <> 0 then
            XML_Filho_conteudo = XML_Document.CreateElement("valor_pis")
            XML_Filho_conteudo.InnerText = Retorna_Num(formatnumber(dr("retTrib_vRetPIS"),2).ToString())
            XML_Node_Filho.AppendChild(XML_Filho_conteudo)
          End If

          If dr("retTrib_vRetCOFINS") <> 0 then
            XML_Filho_conteudo = XML_Document.CreateElement("valor_cofins")
            XML_Filho_conteudo.InnerText = Retorna_Num(formatnumber(dr("retTrib_vRetCOFINS"),2).ToString())
            XML_Node_Filho.AppendChild(XML_Filho_conteudo)
          End If

          If dr("retTrib_vRetCSLL") <> 0 then
            XML_Filho_conteudo = XML_Document.CreateElement("valor_csll")
            XML_Filho_conteudo.InnerText = Retorna_Num(formatnumber(dr("retTrib_vRetCSLL"),2).ToString())
            XML_Node_Filho.AppendChild(XML_Filho_conteudo)
          End If

        Next
      End Using
    ElseIf cMun = "4106902" then 'Curitiba


      strSQL = "EXEC sp9_Pega_NFE_Servico_Lote '" & asp_id_empresa & "','" & asp_id_lote & "','" & asp_id_usuario & "'"

      Using adapter As New SqlDataAdapter(strSQL, cnnSQL)
        Dim ds As New DataSet
        adapter.Fill(ds, "mRPS")

        Dim conta2 As Integer = ds.Tables("mRPS").Rows.Count()

        Dim XML_Raiz = XML_Document.CreateElement("EnviarLoteRpsEnvio")

        XML_Document.AppendChild(XML_Raiz)

        'Dim XML_Node = XML_Document.CreateProcessingInstruction("xml", "version='1.0' encoding='utf-8'")
        Dim XML_Node = XML_Document.CreateProcessingInstruction("xml", "version='1.0'")

        XML_Document.InsertBefore(XML_Node, XML_Raiz)

        Dim XML_Node_Filho = XML_Document.CreateElement("LoteRps")
        XML_Raiz.AppendChild(XML_Node_Filho)

        Dim XML_Filho_conteudo = XML_Document.CreateElement("NumeroLote")
        XML_Filho_conteudo.InnerText = asp_id_lote.ToString()
        XML_Node_Filho.AppendChild(XML_Filho_conteudo)

        XML_Filho_conteudo = XML_Document.CreateElement("Cnpj")
        XML_Filho_conteudo.InnerText = CNPJ
        XML_Node_Filho.AppendChild(XML_Filho_conteudo)

        XML_Filho_conteudo = XML_Document.CreateElement("InscricaoMunicipal")
        XML_Filho_conteudo.InnerText = IM_Prestador
        XML_Node_Filho.AppendChild(XML_Filho_conteudo)

        XML_Filho_conteudo = XML_Document.CreateElement("QuantidadeRps")
        XML_Filho_conteudo.InnerText = conta2.ToString()
        XML_Node_Filho.AppendChild(XML_Filho_conteudo)

        Dim XML_Node_Filho2 = XML_Document.CreateElement("ListaRps")
        XML_Node_Filho.AppendChild(XML_Node_Filho2)

        Dim XML_Filho_conteudo1 = XML_Document.CreateElement("documento")

        Dim XML_Node_Filho3 = XML_Document.CreateElement("Rps")
        Dim XML_Node_InfRPS = XML_Document.CreateElement("InfRps")
        Dim XML_Node_Filho5 = XML_Document.CreateElement("IdentificacaoRps")

        Dim XML_Node_Serie = XML_Document.CreateElement("Serie")
        'XML_Node_Serie.InnerText = "1"
        Dim XML_Node_Tipo = XML_Document.CreateElement("Tipo")

        Dim XML_Node_Servico = XML_Document.CreateElement("Servico")
        Dim XML_Node_Valores = XML_Document.CreateElement("Valores")
        Dim XML_Node_Prestador = XML_Document.CreateElement("Prestador")
        Dim XML_Node_Tomador = XML_Document.CreateElement("Tomador")
        Dim XML_Node_IdentificacaoTomador = XML_Document.CreateElement("IdentificacaoTomador")
        Dim XML_Node_CpfCnpj = XML_Document.CreateElement("CpfCnpj")
        Dim XML_Node_Endereco_Tomador = XML_Document.CreateElement("Endereco")
        Dim XML_Node_Contato_Tomador = XML_Document.CreateElement("Contato")

        XML_Node_IdentificacaoTomador.AppendChild(XML_Node_CpfCnpj)
        XML_Node_Tomador.AppendChild(XML_Node_IdentificacaoTomador)

        XML_Node_Servico.AppendChild(XML_Node_Valores)

        pAliq_Simples_ISS = pAliq_Simples_ISS / 100
        pAliq_Simples_ISS_Str = pAliq_Simples_ISS.ToString()
        pAliq_Simples_ISS_Str.Replace(",",".")

        Dim conta As Integer = 0
        For Each dr As DataRow In ds.Tables("mRPS").Rows
          'valor aproximado dos tributos
          If dr("vTotTrib") = 0 then
            vTotTrib_str = ""
          Else
            vTotTrib_str = " | VALOR APROXIMADO DE TRIBUTOS FEDERAIS, ESTADUAIS E MUNICIPAIS CONFORME DISPOSTO NA LEI Nº 12.741/12: " & formatnumber(dr("vTotTrib"),2).ToString()
          End If

          '''''''''''''''''''''''''

          'XML_Node_Filho2 = XML_Document.CreateElement("ListaRps")
          'XML_Node_Filho.AppendChild(XML_Node_Filho2)

          'XML_Filho_conteudo1 = XML_Document.CreateElement("documento")

          XML_Node_Filho3 = XML_Document.CreateElement("Rps")
          XML_Node_InfRPS = XML_Document.CreateElement("InfRps")
          XML_Node_Filho5 = XML_Document.CreateElement("IdentificacaoRps")

          XML_Node_Serie = XML_Document.CreateElement("Serie")
          XML_Node_Serie.InnerText = "1"
          XML_Node_Tipo = XML_Document.CreateElement("Tipo")
          XML_Node_Tipo.InnerText = "1" '1 RPS ; 2 = Nota Fiscal Conjugada ; 3 = Cupom

          XML_Node_Servico = XML_Document.CreateElement("Servico")
          XML_Node_Valores = XML_Document.CreateElement("Valores")
          XML_Node_Prestador = XML_Document.CreateElement("Prestador")
          XML_Node_Tomador = XML_Document.CreateElement("Tomador")
          XML_Node_IdentificacaoTomador = XML_Document.CreateElement("IdentificacaoTomador")
          XML_Node_CpfCnpj = XML_Document.CreateElement("CpfCnpj")
          XML_Node_Endereco_Tomador = XML_Document.CreateElement("Endereco")
          XML_Node_Contato_Tomador = XML_Document.CreateElement("Contato")

          XML_Node_IdentificacaoTomador.AppendChild(XML_Node_CpfCnpj)
          XML_Node_Tomador.AppendChild(XML_Node_IdentificacaoTomador)

          XML_Node_Servico.AppendChild(XML_Node_Valores)

          '''''''''''''''''''''''''''''''''''''''''



          XML_Node_Filho2.AppendChild(XML_Node_Filho3)
          XML_Node_Filho3.AppendChild(XML_Node_InfRPS)
          XML_Node_InfRPS.AppendChild(XML_Node_Filho5)

          XML_Filho_conteudo = XML_Document.CreateElement("Numero")
          XML_Filho_conteudo.InnerText = dr("serv_num_RPS").ToString()
          XML_Node_Filho5.AppendChild(XML_Filho_conteudo)

          XML_Node_Filho5.AppendChild(XML_Node_Serie)
          XML_Node_Filho5.AppendChild(XML_Node_Tipo)

          XML_Filho_conteudo = XML_Document.CreateElement("DataEmissao")
          XML_Filho_conteudo.InnerText = dr("dEmi").ToString() + "T00:00:00"
          XML_Node_InfRPS.AppendChild(XML_Filho_conteudo)

          XML_Filho_conteudo = XML_Document.CreateElement("NaturezaOperacao")
          XML_Filho_conteudo.InnerText = "1"
          XML_Node_InfRPS.AppendChild(XML_Filho_conteudo)

          'Maxcont - CRT; 1 = Simples Nacional ; 2 = Simples Nacional - Excesso de sublimite de receita bruta ; 3 = Regime Normal
          If CRT <> "1" then 'Não optante pelo Simples Nacional
            'XML_Filho_conteudo = XML_Document.CreateElement("RegimeEspecialTributacao")
            'XML_Filho_conteudo.InnerText = Regime_Especial
            'XML_Node_InfRPS.AppendChild(XML_Filho_conteudo)

            '1 = Optante Simples Nacional ; 2 = Não Optante Simples Nacional
            XML_Filho_conteudo = XML_Document.CreateElement("OptanteSimplesNacional")
            XML_Filho_conteudo.InnerText = "2"
            XML_Node_InfRPS.AppendChild(XML_Filho_conteudo)

          Else 'Optante pelo Simples Nacional
            'Regime_Especial = "6" 'ME EPP – Simples Nacional

            'XML_Filho_conteudo = XML_Document.CreateElement("RegimeEspecialTributacao")
            'XML_Filho_conteudo.InnerText = Regime_Especial
            'XML_Node_InfRPS.AppendChild(XML_Filho_conteudo)

            '1 = Optante Simples Nacional ; 2 = Não Optante Simples Nacional
            XML_Filho_conteudo = XML_Document.CreateElement("OptanteSimplesNacional")
            XML_Filho_conteudo.InnerText = "1"
            XML_Node_InfRPS.AppendChild(XML_Filho_conteudo)

          End If

          'Incentivador Cultural: 1 = Sim ; 2 = Não
          XML_Filho_conteudo = XML_Document.CreateElement("IncentivadorCultural")
          XML_Filho_conteudo.InnerText = "2"
          XML_Node_InfRPS.AppendChild(XML_Filho_conteudo)

          XML_Filho_conteudo = XML_Document.CreateElement("Status")
          XML_Filho_conteudo.InnerText = "1"
          XML_Node_InfRPS.AppendChild(XML_Filho_conteudo)

          XML_Node_InfRPS.AppendChild(XML_Node_Servico)

          XML_Filho_conteudo = XML_Document.CreateElement("ValorServicos")
          XML_Filho_conteudo.InnerText = Retorna_Num(formatnumber(dr("ISSQNTot_vServ"),2).ToString())
          XML_Node_Valores.AppendChild(XML_Filho_conteudo)
          
          If dr("ISSQNtot_vDeducoes") <> 0 then
            XML_Filho_conteudo = XML_Document.CreateElement("ValorDeducoes")
            XML_Filho_conteudo.InnerText = Retorna_Num(formatnumber(dr("ISSQNtot_vDeducoes"),2).ToString())
            XML_Node_Valores.AppendChild(XML_Filho_conteudo)
          End If

          If dr("retTrib_vRetPIS") <> 0 then
            XML_Filho_conteudo = XML_Document.CreateElement("ValorPis")
            XML_Filho_conteudo.InnerText = Retorna_Num(formatnumber(dr("retTrib_vRetPIS"),2).ToString())
            XML_Node_Valores.AppendChild(XML_Filho_conteudo)
          End If

          If dr("retTrib_vRetPrev") <> 0 then
            XML_Filho_conteudo = XML_Document.CreateElement("ValorInss")
            XML_Filho_conteudo.InnerText = Retorna_Num(formatnumber(dr("retTrib_vRetPrev"),2).ToString())
            XML_Node_Valores.AppendChild(XML_Filho_conteudo)
          End If

          If dr("retTrib_vIRRF") <> 0 then
            XML_Filho_conteudo = XML_Document.CreateElement("ValorIr")
            XML_Filho_conteudo.InnerText = Retorna_Num(formatnumber(dr("retTrib_vIRRF"),2).ToString())
            XML_Node_Valores.AppendChild(XML_Filho_conteudo)
          End If

          If dr("retTrib_vRetCSLL") <> 0 then
            XML_Filho_conteudo = XML_Document.CreateElement("ValorCsll")
            XML_Filho_conteudo.InnerText = Retorna_Num(formatnumber(dr("retTrib_vRetCSLL"),2).ToString())
            XML_Node_Valores.AppendChild(XML_Filho_conteudo)
          End If

          If dr("retTrib_vRetCOFINS") <> 0 then
            XML_Filho_conteudo = XML_Document.CreateElement("ValorCofins")
            XML_Filho_conteudo.InnerText = Retorna_Num(formatnumber(dr("retTrib_vRetCOFINS"),2).ToString())
            XML_Node_Valores.AppendChild(XML_Filho_conteudo)
          End If

          If dr("ISSQN_cSitTrib").ToString() = "R" then
            iss_retido = "1"
          Else
            iss_retido = "2"
          End If

          XML_Filho_conteudo = XML_Document.CreateElement("IssRetido")
          XML_Filho_conteudo.InnerText = iss_retido
          XML_Node_Valores.AppendChild(XML_Filho_conteudo)

          'XML_Filho_conteudo = XML_Document.CreateElement("ValorIss")
          'XML_Filho_conteudo.InnerText = Retorna_Num(formatnumber(dr("ISSQN_vISSQN"),2).ToString())
          'XML_Node_Valores.AppendChild(XML_Filho_conteudo)

          XML_Filho_conteudo = XML_Document.CreateElement("OutrasRetencoes")
          XML_Filho_conteudo.InnerText = "0"
          XML_Node_Valores.AppendChild(XML_Filho_conteudo)

          XML_Filho_conteudo = XML_Document.CreateElement("BaseCalculo")
          XML_Filho_conteudo.InnerText = "0"
          XML_Node_Valores.AppendChild(XML_Filho_conteudo)

          'Empresa Optante pelo Simples Nacional - Informa alíquota ISS no Simples
          'If pAliq_Simples_ISS <> 0 and CRT = "1"
          '  XML_Filho_conteudo = XML_Document.CreateElement("Aliquota")
          '  XML_Filho_conteudo.InnerText = pAliq_Simples_ISS_Str
          '  XML_Node_Valores.AppendChild(XML_Filho_conteudo)
          'End If


          XML_Filho_conteudo = XML_Document.CreateElement("ValorLiquidoNfse")
          XML_Filho_conteudo.InnerText = Retorna_Num(formatnumber(dr("vlr_liquido_NFSe"),2).ToString())
          XML_Node_Valores.AppendChild(XML_Filho_conteudo)

          XML_Filho_conteudo = XML_Document.CreateElement("DescontoIncondicionado")
          XML_Filho_conteudo.InnerText = "0"
          XML_Node_Valores.AppendChild(XML_Filho_conteudo)

          XML_Filho_conteudo = XML_Document.CreateElement("DescontoCondicionado")
          XML_Filho_conteudo.InnerText = "0"
          XML_Node_Valores.AppendChild(XML_Filho_conteudo)


          XML_Filho_conteudo = XML_Document.CreateElement("ItemListaServico")
          XML_Filho_conteudo.InnerText = dr("cod_serv")
          XML_Node_Servico.AppendChild(XML_Filho_conteudo)

          XML_Filho_conteudo = XML_Document.CreateElement("Discriminacao")
          XML_Filho_conteudo.InnerText = dr("descr_serv") & vTotTrib_str
          XML_Node_Servico.AppendChild(XML_Filho_conteudo)

          XML_Filho_conteudo = XML_Document.CreateElement("CodigoMunicipio")
          XML_Filho_conteudo.InnerText = dr("cMunFG").ToString()
          XML_Node_Servico.AppendChild(XML_Filho_conteudo)

          XML_Node_InfRPS.AppendChild(XML_Node_Prestador)

          XML_Filho_conteudo = XML_Document.CreateElement("Cnpj")
          XML_Filho_conteudo.InnerText = dr("cnpj_Prestador").ToString()
          XML_Node_Prestador.AppendChild(XML_Filho_conteudo)
          
          XML_Filho_conteudo = XML_Document.CreateElement("InscricaoMunicipal")
          XML_Filho_conteudo.InnerText = IM_Prestador
          XML_Node_Prestador.AppendChild(XML_Filho_conteudo)
          
          XML_Node_InfRPS.AppendChild(XML_Node_Tomador)

          If dr("tipo_tomador") = "F" then
            XML_Filho_conteudo = XML_Document.CreateElement("Cpf")
            XML_Filho_conteudo.InnerText = dr("cnpj_tomador_fmt").ToString()
          Else
            XML_Filho_conteudo = XML_Document.CreateElement("Cnpj")
            XML_Filho_conteudo.InnerText = dr("cnpj_tomador_fmt").ToString()
          End If
          XML_Node_CpfCnpj.AppendChild(XML_Filho_conteudo)

          XML_Filho_conteudo = XML_Document.CreateElement("RazaoSocial")
          XML_Filho_conteudo.InnerText = dr("xNome_tomador")
          XML_Node_Tomador.AppendChild(XML_Filho_conteudo)

          XML_Node_Tomador.AppendChild(XML_Node_Endereco_Tomador)

          XML_Filho_conteudo = XML_Document.CreateElement("Endereco")
          XML_Filho_conteudo.InnerText = dr("xLgr_Tomador")
          XML_Node_Endereco_Tomador.AppendChild(XML_Filho_conteudo)

          XML_Filho_conteudo = XML_Document.CreateElement("Numero")

          nro_tomador = dr("nro_tomador").ToString()

          If nro_tomador.Length > 10
            nro_tomador = nro_tomador.Substring(0, 10)
          end if

          XML_Filho_conteudo.InnerText = nro_tomador
          XML_Node_Endereco_Tomador.AppendChild(XML_Filho_conteudo)

          If dr("xCpl_tomador") <> "" then
            XML_Filho_conteudo = XML_Document.CreateElement("Complemento")
            XML_Filho_conteudo.InnerText = dr("xCpl_tomador")
            XML_Node_Endereco_Tomador.AppendChild(XML_Filho_conteudo)
          End If

          XML_Filho_conteudo = XML_Document.CreateElement("Bairro")
          XML_Filho_conteudo.InnerText = dr("xBairro_Tomador")
          XML_Node_Endereco_Tomador.AppendChild(XML_Filho_conteudo)

          XML_Filho_conteudo = XML_Document.CreateElement("CodigoMunicipio")
          XML_Filho_conteudo.InnerText = dr("cMun_tomador")
          XML_Node_Endereco_Tomador.AppendChild(XML_Filho_conteudo)

          XML_Filho_conteudo = XML_Document.CreateElement("Uf")
          XML_Filho_conteudo.InnerText = dr("UF_tomador")
          XML_Node_Endereco_Tomador.AppendChild(XML_Filho_conteudo)

          XML_Filho_conteudo = XML_Document.CreateElement("Cep")
          XML_Filho_conteudo.InnerText = dr("CEP_tomador")
          XML_Node_Endereco_Tomador.AppendChild(XML_Filho_conteudo)

          XML_Node_Tomador.AppendChild(XML_Node_Contato_Tomador)


          XML_Filho_conteudo = XML_Document.CreateElement("Telefone")
          XML_Filho_conteudo.InnerText = dr("fone_tomador")
          XML_Node_Contato_Tomador.AppendChild(XML_Filho_conteudo)

          XML_Filho_conteudo = XML_Document.CreateElement("Email")
          XML_Filho_conteudo.InnerText = dr("email_tomador")
          XML_Node_Contato_Tomador.AppendChild(XML_Filho_conteudo)

        Next
      End Using
    End If

    XML_Document.Save(sFilename)
    Response.ContentType = "text/xml"
    Response.AppendHeader("Content-Disposition","attachment; filename=" & CNPJ & "_Lote_" & asp_id_lote & ".xml")
    Response.TransmitFile("../../temp/" & Session_ID & "/" & CurrentFileName)
    Response.End()
  End Sub

  Private Function Retorna_Num(txt As string)
    Retorna_Num = txt.Replace(".", "").Replace(",", ".")
  End Function

End Class
