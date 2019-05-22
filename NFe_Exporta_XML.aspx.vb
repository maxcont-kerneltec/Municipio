Imports System.Xml 
Imports System
Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration

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

    Dim XML_Document As New XmlDocument()

    Dim XML_Raiz = XML_Document.CreateElement("lote")

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

    Dim XML_Node = XML_Document.CreateProcessingInstruction("xml", "version='1.0' encoding='utf-8'")

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

    Dim strSQL As String = "SELECT dbo.fLimpa_Numero(cnpj_emp) as cnpj, razao_emp FROM dbo.tbEmpresas WHERE (id_empresa = " & asp_id_empresa & ")"

    Using adapter As New SqlDataAdapter(strSQL, cnnSQL)
      Dim ds As New DataSet
      adapter.Fill(ds, "mInfo")

      Dim conta As Integer = 0
      For Each dr As DataRow In ds.Tables("mInfo").Rows

        CNPJ = dr("cnpj").ToString()
        Razao_Social = dr("razao_emp")

      Next
    end Using

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
        XML_Filho_conteudo1.InnerText = dr("cnpj_tomador").ToString()
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
          XML_Filho_conteudo1.InnerText = dr("nro_tomador").ToString()
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

        XML_Filho_conteudo = XML_Document.CreateElement("descricao_servicos")
        XML_Filho_conteudo.InnerText = dr("descr_serv").ToString()
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
