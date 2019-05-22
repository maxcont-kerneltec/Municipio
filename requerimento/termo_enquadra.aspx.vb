Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration
Imports System.IO

Partial Class termo_enquadra
  Inherits System.Web.UI.Page

  Dim tbMunicipio As New DataTable

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
            If cs.Name = "municipio_cloud" then
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

  Protected Sub btn_verifica_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_verifica.Click
      GetAllConnectionStrings()
      Session("id_municipio") = "10"
      
      Dim strSQL As String = "EXEC sp2Termo_Enquadra_Dados '" & Session("id_municipio") & "','" & txt_CPF_requer.Text & "','" & txt_cnpj_pesq.Text & "','" & txt_termo_pesq.Text & "'"

      'Try
      Using adapter As New SqlDataAdapter(strSQL, Session("cnxStr").ToString)
        Dim ds As New DataSet
        adapter.Fill(ds, "REQUER")

        For Each dr As DataRow In ds.Tables("REQUER").Rows
          If dr("cpf_requer").ToString = "0" then 'CPF inválido
            lbl_erro.Visible = true
            lbl_erro.Text = "CPF Inválido! Informe um novo CPF"
          Else 'CPF válido
            If txt_termo_pesq.Text <> "" then
              if dr("id_termo_enquadra").ToString = "0" then
                lbl_erro.Visible = true
                lbl_erro.Text = "Termo de enquandramento não foi localizado."
              Else
                'Page.Response.Redirect ("termo_enquadra.aspx?id_termo_=" & dr("id_termo_enquadra") & "&mod_=IMP&cpf_=" & dr("cpf_requer").ToString)
                Page.Response.Redirect ("termo_enquadra.aspx?id_termo_=" & dr("id_termo_enquadra") & "&mod_=EDT&cpf_=" & dr("cpf_requer").ToString)
              End If
            
            ElseIf dr("cpf_requer").ToString <> "0" then
              'If "" & dr("id_termo_enquadra") = "0" then
               pnl_documentos.Visible = False
              'End If

              lbl_aviso_documentos.Text = "Preencha o requerimento e clique em Salvar. Após Salvar anexe os documentos solicitados."
              pnl_requerente.Visible = true
              lbl_CPF_requer.Text = "" & dr("cpf_requer")
              txt_nome.Text = "" & dr("nome")
              lbl_nome.Text = "" & dr("nome")
              txt_xLgr.Text = "" & dr("xLgr")
              lbl_xLgr.Text = "" & dr("xLgr")
              txt_nro.Text = "" & dr("nro")
              lbl_nro.Text = "" & dr("nro")
              txt_xCpl.Text = "" & dr("xCpl")
              lbl_xCpl.Text = "" & dr("xCpl")
              txt_xBairro.text = "" & dr("xBairro")
              lbl_xBairro.text = "" & dr("xBairro")
              txt_CEP.text = "" & dr("CEP")
              lbl_CEP.text = "" & dr("CEP")
              drp_UF.text = "" & dr("UF")
              txt_telefone.text = "" & dr("telefone")
              lbl_telefone.text = "" & dr("telefone")
              txt_email.text = "" & dr("email")
              lbl_email.text = "" & dr("email")


              lbl_cnpj_estabelecimento.Text = "" & dr("cnpj_estabelecimento")
              txt_cnpj_estabelecimento.Text = "" & dr("cnpj_estabelecimento")

              lbl_IM_estabelecimento.Text = "" & dr("IM_estabelecimento")
              txt_IM_estabelecimento.Text = "" & dr("IM_estabelecimento")

              lbl_IE_estabelecimento.Text = "" & dr("IE_estabelecimento")
              txt_IE_estabelecimento.Text = "" & dr("IE_estabelecimento")

              lbl_razao_estabelecimento.Text = "" & dr("razao_estabelecimento")
              txt_razao_estabelecimento.Text = "" & dr("razao_estabelecimento")

              lbl_lgr_estabelecimento.Text = "" & dr("lgr_estabelecimento")
              txt_lgr_estabelecimento.Text = "" & dr("lgr_estabelecimento")

              lbl_nro_estabelecimento.Text = "" & dr("nro_estabelecimento")
              txt_nro_estabelecimento.Text = "" & dr("nro_estabelecimento")

              lbl_xCpl_estabelecimento.Text = "" & dr("xCpl_estabelecimento")
              txt_xCpl_estabelecimento.Text = "" & dr("xCpl_estabelecimento")

              lbl_xBairro_estabelecimento.Text = "" & dr("xBairro_estabelecimento")
              txt_xBairro_estabelecimento.Text = "" & dr("xBairro_estabelecimento")

              lbl_CEP_estabelecimento.Text = "" & dr("CEP_estabelecimento")
              txt_CEP_estabelecimento.Text = "" & dr("CEP_estabelecimento")

              txt_telefone_estabelecimento.Text = "" & dr("telefone_estabelecimento")
              lbl_telefone_estabelecimento.Text = "" & dr("telefone_estabelecimento")

              txt_email_estabelecimento.Text = "" & dr("email_estabelecimento")
              lbl_email_estabelecimento.Text = "" & dr("email_estabelecimento")

              Dim strSQL2 As String

              strSQL2 = "SELECT cMun, xMun FROM dbo.tbCad_Municipios WHERE (UF = '" & "" & dr("UF") & "')"
              drp_cMun.DataSourceID = ""
              Using adapter2 As New SqlDataAdapter(strSQL2, Session("cnxStr").ToString)
                adapter2.Fill(tbMunicipio)
                drp_cMun.DataSource = tbMunicipio
                drp_cMun.DataTextField = "xMun"
                drp_cMun.DataValueField = "cMun"
                drp_cMun.DataBind()
              End Using
              drp_cMun.SelectedValue = "" & dr("cMun")
              
              pnl_requerente.Visible = True
              pnl_consulta_CPF.Visible = False
              pnl_botoes.Visible = True
              lbl_erro.Visible = True
              lbl_erro.Text = "Informe os dados do requerente e do estabelecimento e clique em Salvar."
              lbl_aviso_descr_atividade.Text = "(Descrever somente as atividades que serão exercidas no local, observando o texto contido no Contrato Social)"
            end if
          end if
        Next

      End Using   
  End Sub

  Protected Sub drp_UF_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drp_UF.SelectedIndexChanged
   Dim tbMunicipio As New DataTable
   Dim strSQL2 As String
    
    strSQL2 = "SELECT cMun, xMun FROM dbo.tbCad_Municipios WHERE (UF = '" & drp_UF.SelectedValue.ToString & "')"
    drp_cMun.DataSourceID = ""
    Using adapter2 As New SqlDataAdapter(strSQL2, Session("cnxStr").ToString)
      adapter2.Fill(tbMunicipio)
      drp_cMun.DataSource = tbMunicipio
      drp_cMun.DataTextField = "xMun"
      drp_cMun.DataValueField = "cMun"
      drp_cMun.DataBind()
    End Using

  End Sub

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    GetAllConnectionStrings()
    Me.txt_CPF_requer.Attributes.Add("onkeypress", "formatar(this,'###.###.###-##'); return SomenteNumero(event);")
    Me.txt_cnpj_estabelecimento.Attributes.Add("onkeypress", "formatar(this,'##.###.###/####-##'); return SomenteNumero(event);")
    Me.txt_cnpj_pesq.Attributes.Add("onkeypress", "formatar(this,'##.###.###/####-##'); return SomenteNumero(event);")

    Dim asp_id_termo_enquadra as String
    Dim asp_cpf_requer As String

    If (Request.QueryString("id_termo_") <> "") and (Not IsPostBack) then
      asp_id_termo_enquadra = Request.QueryString("id_termo_")
      asp_cpf_requer = Request.QueryString("cpf_")
      Session("id_municipio") = "10"

      Dim strSQL As String = "EXEC sp2Termo_Enquadra_Pega '" & Session("id_municipio") & "','" & asp_cpf_requer & "','" & asp_id_termo_enquadra & "'"

      'Try
      Using adapter As New SqlDataAdapter(strSQL, Session("cnxStr").ToString)
        Dim ds As New DataSet
        adapter.Fill(ds, "TERMO")

        For Each dr As DataRow In ds.Tables("TERMO").Rows
            pnl_requerente.Visible = true

            lbl_id_termo_enquadra.Text = Right("000000" & dr("id_termo_enquadra"), 6)
            lbl_dt_gera.Text = dr("dt_gera")
            lbl_CPF_requer.Text = "" & dr("cpf_requer")
            txt_CPF_requer.Text = "" & dr("cpf_requer")
            txt_nome.Text = "" & dr("nome")
            lbl_nome.Text = "" & dr("nome")
            txt_xLgr.Text = "" & dr("xLgr")
            lbl_xLgr.Text = "" & dr("xLgr")
            txt_nro.Text = "" & dr("nro")
            lbl_nro.Text = "" & dr("nro")
            txt_xCpl.Text = "" & dr("xCpl")
            lbl_xCpl.Text = "" & dr("xCpl")
            txt_xBairro.text = "" & dr("xBairro")
            lbl_xBairro.text = "" & dr("xBairro")
            txt_CEP.text = "" & dr("CEP")
            lbl_CEP.text = "" & dr("CEP")
            drp_UF.text = "" & dr("UF")
            lbl_UF.Text = "" & dr("UF")
            lbl_xMunicipio.Text = "" & dr("xMun")
            txt_telefone.text = "" & dr("telefone")
            lbl_telefone.text = "" & dr("telefone")
            txt_email.text = "" & dr("email")
            lbl_email.text = "" & dr("email")

            lbl_cnpj_estabelecimento.Text = "" & dr("cnpj")
            txt_cnpj_estabelecimento.Text = "" & dr("cnpj")

            lbl_IM_estabelecimento.Text = "" & dr("IM")
            txt_IM_estabelecimento.Text = "" & dr("IM")

            lbl_IE_estabelecimento.Text = "" & dr("IE")
            txt_IE_estabelecimento.Text = "" & dr("IE")

            lbl_razao_estabelecimento.Text = "" & dr("razao_social")
            txt_razao_estabelecimento.Text = "" & dr("razao_social")

            lbl_lgr_estabelecimento.Text = "" & dr("xLgr")
            txt_lgr_estabelecimento.Text = "" & dr("xLgr")

            lbl_nro_estabelecimento.Text = "" & dr("nro")
            txt_nro_estabelecimento.Text = "" & dr("nro")

            lbl_xCpl_estabelecimento.Text = "" & dr("xCpl")
            txt_xCpl_estabelecimento.Text = "" & dr("xCpl")

            lbl_xBairro_estabelecimento.Text = "" & dr("xBairro")
            txt_xBairro_estabelecimento.Text = "" & dr("xBairro")

            lbl_CEP_estabelecimento.Text = "" & dr("CEP")
            txt_CEP_estabelecimento.Text = "" & dr("CEP")

            txt_telefone_estabelecimento.Text = "" & dr("telefone_estabelecimento")
            lbl_telefone_estabelecimento.Text = "" & dr("telefone_estabelecimento")

            txt_email_estabelecimento.Text = "" & dr("email_estabelecimento")
            lbl_email_estabelecimento.Text = "" & dr("email_estabelecimento")

            lbl_num_inscr_IPTU.Text = "" & dr("num_inscr_IPTU")
            txt_num_inscr_IPTU.Text = "" & dr("num_inscr_IPTU")

            lbl_IPTU_lote.Text = "" & dr("lote")
            txt_IPTU_lote.Text = "" & dr("lote")

            lbl_IPTU_quadra.Text = "" & dr("quadra")
            txt_IPTU_quadra.Text = "" & dr("quadra")

            lbl_tp_objetivo.Text = "" & dr("descr_tp_objetivo")
            drp_dwn_tp_objetivo.Text = "" & dr("tp_objetivo")

            lbl_descr_atividade.Text = replace("" & dr("descr_atividade"), vbCrLf, "<br>")
            txt_descr_atividade.Text = "" & dr("descr_atividade")
            
            lbl_pasta_docs.Text = "" & dr("doc_pasta")

            If "" & dr("doc_contrato") <> "" then 
              btn_visualiza_contrato.Visible = True
              btn_visualiza_contrato.OnClientClick = "visualiza_docto('" & "" & dr("doc_pasta") & "/" & dr("doc_contrato") & "')"
            Else
              btn_visualiza_contrato.Visible = False
            end if

            If "" & dr("doc_planta") <> "" then 
              btn_visualiza_planta.Visible = True
              btn_visualiza_planta.OnClientClick = "visualiza_docto('" & "" & dr("doc_pasta") & "/" & dr("doc_planta") & "')"
            Else
              btn_visualiza_planta.Visible = False
            end if

            If "" & dr("doc_iptu") <> "" then 
              btn_visualiza_iptu.Visible = True
              btn_visualiza_iptu.OnClientClick = "visualiza_docto('" & "" & dr("doc_pasta") & "/" & dr("doc_iptu") & "')"
            Else
              btn_visualiza_iptu.Visible = False
            end if

            lbl_aviso_documentos.Text = "(*) Documentos obrigatórios: Contrato (página do objeto social) e IPTU.<br />Selecione os documentos e clique em Gravar Documentos."

            Dim strSQL2 As String

            strSQL2 = "SELECT cMun, xMun FROM dbo.tbCad_Municipios WHERE (UF = '" & "" & dr("UF") & "')"
            drp_cMun.DataSourceID = ""
            Using adapter2 As New SqlDataAdapter(strSQL2, Session("cnxStr").ToString)
              adapter2.Fill(tbMunicipio)
              drp_cMun.DataSource = tbMunicipio
              drp_cMun.DataTextField = "xMun"
              drp_cMun.DataValueField = "cMun"
              drp_cMun.DataBind()
            End Using
            drp_cMun.SelectedValue = "" & dr("cMun")
            pnl_requerente.Visible = True

        Next

      End Using  
    End If

    'Modo impressão
    If ucase(Request.QueryString("mod_")) = "IMP" then
      pnl_documentos.Visible = False
      pnl_consulta_CPF.Visible = False
      'lbl_aviso_documentos.Text = ""
      pnl_botoes.Visible = False
      pnl_botao_imprimir.Visible = True
      lbl_nome.Visible = True
      lbl_xLgr.Visible = True
      lbl_nro.Visible = True
      lbl_xCpl.Visible = True
      lbl_xBairro.Visible = True
      lbl_CEP.Visible = True
      lbl_UF.visible = True
      lbl_xMunicipio.Visible = True
      lbl_telefone.Visible = True
      lbl_email.Visible = True

      txt_nome.Visible = False
      txt_xLgr.Visible = False
      txt_nro.Visible = False
      txt_xCpl.Visible = False
      txt_xBairro.Visible = False
      txt_CEP.Visible = False
      drp_UF.Visible = False
      drp_cMun.Visible = False
      txt_telefone.Visible = False
      txt_email.Visible = False

      txt_cnpj_estabelecimento.Visible = False
      txt_IM_estabelecimento.Visible = False
      txt_IE_estabelecimento.Visible = False
      txt_razao_estabelecimento.Visible = False
      txt_lgr_estabelecimento.Visible = False
      txt_nro_estabelecimento.Visible = False
      txt_xCpl_estabelecimento.Visible = False
      txt_xBairro_estabelecimento.Visible = False
      txt_CEP_estabelecimento.Visible = False
      txt_telefone_estabelecimento.Visible = False
      txt_email_estabelecimento.Visible = False

      txt_num_inscr_IPTU.Visible = False
      txt_IPTU_lote.Visible = False
      txt_IPTU_quadra.Visible = False

      drp_dwn_tp_objetivo.Visible = False
      txt_descr_atividade.Visible = False
      lbl_aviso_descr_atividade.Visible = False

    Else 'Edição do Termo
      pnl_documentos.Visible = True
      'lbl_aviso_documentos.Visible = False
      lbl_aviso_documentos.Visible = True
      lbl_aviso_documentos.Text = "(*)Documentos obrigatórios: Contrato (página do objeto social) e IPTU.<br />Selecione os documentos e clique em Gravar Documentos."

      lbl_nome.Visible = False
      lbl_xLgr.Visible = False
      lbl_nro.Visible = False
      lbl_xCpl.Visible = False
      lbl_xBairro.Visible = False
      lbl_CEP.Visible = False
      lbl_UF.Visible = False
      lbl_telefone.Visible = False
      lbl_email.Visible = False
      lbl_xMunicipio.Visible = False

      lbl_cnpj_estabelecimento.Visible = False
      lbl_IM_estabelecimento.Visible = False
      lbl_IE_estabelecimento.Visible = False
      lbl_razao_estabelecimento.Visible = False

      lbl_lgr_estabelecimento.Visible = False
      lbl_nro_estabelecimento.Visible = False
      lbl_xCpl_estabelecimento.Visible = False
      lbl_xBairro_estabelecimento.Visible = False
      lbl_CEP_estabelecimento.Visible = False
      lbl_telefone_estabelecimento.Visible = False
      lbl_email_estabelecimento.Visible = False

      lbl_num_inscr_IPTU.Visible = False
      lbl_IPTU_lote.Visible = False
      lbl_IPTU_quadra.Visible = False

      lbl_aviso_descr_atividade.Visible = False

      lbl_tp_objetivo.Visible = False
      lbl_descr_atividade.Visible = False
      If Request.QueryString("id_termo_") <> "" then
        pnl_requerente.Visible = True
        pnl_consulta_CPF.Visible = False
        pnl_botoes.Visible = True
      End If 
    End If
  End Sub

  Protected Sub Gravar_Dados(tipo_grava As string)
    Dim result As String
    Dim asp_id_termo_enquadra As String

    asp_id_termo_enquadra = lbl_id_termo_enquadra.Text

    Dim strSQL As String = "EXEC dbo.sp2Termo_Enquadra_Salva '" & Session("id_municipio").ToString & "','" & asp_id_termo_enquadra & "','" & txt_CPF_requer.Text & "'"
    strSQL = strSQL & ",'" & txt_nome.Text & "','" & txt_xLgr.Text & "','" & txt_nro.text & "','" & txt_xCpl.text & "'"
    strSQL = strSQL & ",'" & txt_xBairro.Text & "','" & txt_CEP.Text & "','" & drp_cMun.SelectedValue.ToString & "','" & txt_email.text & "'"
    strSQL = strSQL & ",'" & txt_telefone.Text & "','" & txt_cnpj_estabelecimento.Text & "','" & txt_IM_estabelecimento.Text & "'"
    strSQL = strSQL & ",'" & txt_IE_estabelecimento.Text & "','" & txt_razao_estabelecimento.Text & "','" & txt_lgr_estabelecimento.Text & "'"
    strSQL = strSQL & ",'" & txt_nro_estabelecimento.Text & "','" & txt_xCpl_estabelecimento.Text & "','" & txt_xBairro_estabelecimento.Text & "'"
    strSQL = strSQL & ",'" & txt_CEP_estabelecimento.Text & "','" & txt_num_inscr_IPTU.Text & "','" & txt_IPTU_lote.Text & "'"
    strSQL = strSQL & ",'" & txt_IPTU_quadra.Text & "','" & drp_dwn_tp_objetivo.SelectedValue.ToString & "','" & txt_descr_atividade.Text & "'"
    strSQL = strSQL & ",'" & txt_telefone_estabelecimento.Text & "','" & txt_email_estabelecimento.Text & "','" & drp_dwn_cod_servicos.SelectedValue.ToString & "'"

    'Try
    Using adapter As New SqlDataAdapter(strSQL, Session("cnxStr").ToString)
      Dim ds As New DataSet
      adapter.Fill(ds, "NFS")

      For Each dr As DataRow In ds.Tables("NFS").Rows
        result = dr("result")
        Page.Response.Redirect ("termo_enquadra.aspx?id_termo_=" & dr("id_termo_enquadra") & "&cpf_=" & txt_CPF_requer.Text)
      Next
    End Using
  End Sub

  Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click
    Gravar_Dados("R")
  End Sub

  Protected Sub btn_grava_foto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_grava_doctos.Click

    Dim FlName_Contrato As String
    Dim FlName_Planta As String
    Dim FlName_IPTU As String
    Dim Pasta As String

    'Contrato
    FlName_Contrato = fld_up_contrato.FileName
    If (Path.GetExtension(fld_up_contrato.FileName).ToLower <> ".pdf") and (Path.GetExtension(fld_up_contrato.FileName).ToLower <> ".png" and (Path.GetExtension(fld_up_contrato.FileName).ToLower <> ".jpg")) Then
      labERRO.Text = "O arquivo deve ser PDF, PNG ou JPG." & fld_up_contrato.PostedFile.FileName
      FlName_Contrato = ""
      labERRO.Visible = True
    End If

    'Planta
    FlName_Planta = fld_up_planta.FileName
    If (Path.GetExtension(fld_up_planta.FileName).ToLower <> ".pdf") and (Path.GetExtension(fld_up_planta.FileName).ToLower <> ".png" and (Path.GetExtension(fld_up_planta.FileName).ToLower <> ".jpg")) Then
      labERRO.Text = "O arquivo deve ser PDF, PNG ou JPG." & fld_up_contrato.PostedFile.FileName
      FlName_Planta = ""
      labERRO.Visible = True
    End If

    'IPTU
    FlName_IPTU = fld_up_iptu.FileName
    If (Path.GetExtension(fld_up_iptu.FileName).ToLower <> ".pdf") and (Path.GetExtension(fld_up_iptu.FileName).ToLower <> ".png" and (Path.GetExtension(fld_up_iptu.FileName).ToLower <> ".jpg")) Then
      labERRO.Text = "O arquivo deve ser PDF, PNG ou JPG." & fld_up_contrato.PostedFile.FileName
      FlName_IPTU = ""
      labERRO.Visible = True
    End If

    Dim CurrentPath As String
    If lbl_pasta_docs.Text = "" then

      Pasta = Session("id_municipio")

      CurrentPath = Server.MapPath("termos_docs/" & Pasta)

      If Not Directory.Exists(CurrentPath) Then
        Directory.CreateDirectory(CurrentPath)
      End If

      Pasta = Pasta & "/" & Year(DateAndTime.Now)
      CurrentPath = CurrentPath & "/" & Year(DateAndTime.Now)

      If Not Directory.Exists(CurrentPath) Then
        Directory.CreateDirectory(CurrentPath)
      End If

      Pasta = Pasta & "/" & right("00" & Month(DateAndTime.Now).ToString, 2)
      CurrentPath = CurrentPath & "/" & right("00" & Month(DateAndTime.Now).ToString, 2)

      If Not Directory.Exists(CurrentPath) Then
        Directory.CreateDirectory(CurrentPath)
      End If

      Pasta = Pasta & "/" & lbl_id_termo_enquadra.Text

      CurrentPath = CurrentPath & "/" & lbl_id_termo_enquadra.Text

      If Not Directory.Exists(CurrentPath) Then
          ' Create the directory.
          Directory.CreateDirectory(CurrentPath)
      End If
    Else
      Pasta = lbl_pasta_docs.Text
      CurrentPath = Server.MapPath("termos_docs/" & Pasta)
    End If

    'Dim Caminho_Completo As String

    'CONTRATO
    If fld_up_contrato.HasFile and FlName_Contrato <> "" Then
      'Caminho_Completo = CurrentPath & "/" & FlName_Contrato
      'CurrentPath += "/" & FlName_Contrato

      fld_up_contrato.SaveAs(CurrentPath & "/" & FlName_Contrato)
      labERRO.Text = "Arquivo enviado com sucesso."
    Else
      labERRO.Text = "Falha no envio do arquivo."
    End if

    'PLANTA
    If fld_up_planta.HasFile and FlName_Planta <> "" Then
      'Caminho_Completo = CurrentPath & "/" & FlName_Planta
      'CurrentPath += "/" & FlName_Planta

      fld_up_planta.SaveAs(CurrentPath & "/" & FlName_Planta)
      labERRO.Text = "Arquivo enviado com sucesso."
    Else
      labERRO.Text = "Falha no envio do arquivo."
    End if

    'IPTU
    If fld_up_iptu.HasFile and FlName_IPTU <> "" Then
      'Caminho_Completo = CurrentPath & "/" & FlName_IPTU
      'CurrentPath += "/" & FlName_IPTU

      fld_up_iptu.SaveAs(CurrentPath & "/" & FlName_IPTU)
      labERRO.Text = "Arquivo enviado com sucesso."
    Else
      labERRO.Text = "Falha no envio do arquivo."
    End if

    Dim asp_id_termo_enquadra As String = Request.QueryString("id_termo_")

    Dim strSQL As String = "EXEC dbo.sp2Termo_Enquadra_Grava_Docs '" & Session("id_municipio").ToString & "','" & asp_id_termo_enquadra & "'"
    strSQL = strSQL & ",'" & Pasta & "','" & FlName_Contrato & "','" & FlName_Planta & "','" & FlName_IPTU & "'"
    'Try
    Using adapter As New SqlDataAdapter(strSQL, Session("cnxStr").ToString)
      Dim ds As New DataSet
      adapter.Fill(ds, "NFS")
    End Using
    Page.Response.Redirect ("termo_enquadra.aspx?id_termo_=" & lbl_id_termo_enquadra.Text & "&cpf_=" & txt_CPF_requer.Text)
  End Sub


  Protected Sub Btn_confirmar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_confirmar.Click
    Gravar_Dados("C")
  End Sub

  Protected Sub btn_incluir_cod_servico_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_incluir_cod_servico.Click
    Gravar_Dados("C")
  End Sub


  Protected Sub Excluir_Cod_Servico(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)

    Dim asp_cod_servico As String = e.CommandArgument.ToString
    Dim asp_id_termo_enquadra As String = Request.QueryString("id_termo_")

    Dim strSQL As String = "EXEC dbo.sp2Termo_Enquadra_Cod_Servico '" & Session("id_municipio").ToString & "','" & asp_id_termo_enquadra & "','" & asp_cod_servico & "'"
    'Try
    Using adapter As New SqlDataAdapter(strSQL, Session("cnxStr").ToString)
      Dim ds As New DataSet
      adapter.Fill(ds, "NFS")
    End Using

    Page.Response.Redirect("termo_enquadra.aspx?id_termo_=" & asp_id_termo_enquadra & "&cpf_=" & txt_CPF_requer.Text)
  End Sub

  Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cod_servico_pesq.Click
    Dim Cod_Servico As New datatable
    Dim strSQL2 As String = "SELECT cod_servico, descr FROM Cod_Servicos WHERE (id_municipio = " & Session("id_municipio").ToString & ") AND (descr like '%" & txt_cod_serv_pesq.Text & "%') ORDER BY descr"

    drp_dwn_cod_servicos.DataSourceID = ""

    Using adapter As New SqlDataAdapter(strSQL2, Session("cnxStr").ToString)
      adapter.Fill(Cod_Servico)
      drp_dwn_cod_servicos.DataSource = Cod_Servico
      drp_dwn_cod_servicos.DataTextField = "descr"
      drp_dwn_cod_servicos.DataValueField = "cod_servico"
      drp_dwn_cod_servicos.DataBind()
    End Using
    If drp_dwn_cod_servicos.Items.Count = 0
      drp_dwn_cod_servicos.DataTextField = "descr_cbo"
      drp_dwn_cod_servicos.DataValueField = "cod_cbo"
      drp_dwn_cod_servicos.DataBind()
      drp_dwn_cod_servicos.Items.Insert (0, "NENHUM CÓDIGO DE SERVIÇO LOCALIZADO. PESQUISE NOVAMENTE.")
      drp_dwn_cod_servicos.Items(0).value = "0"
    End If
  End Sub

End Class
