Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration
Imports System.IO

Partial Class Cad_cidadao
  Inherits System.Web.UI.Page
  Dim tbMunicipio As New DataTable
  Dim tbCBO_Profissoes As New DataTable
  Dim Pega As String = ""

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    'If (Session("logado")) <> "T" then
    '  Response.Redirect ("../login_usuario.aspx")
    'End If

    If  "" & Request.QueryString("pega_") <> "" then
      Pega = Request.QueryString("pega_")
    End If

    If Request.QueryString("a") = "SCAD" then
      labERRO.Text = "Atenção: Cidadão já cadastrado!"
    else
      labERRO.Text = Request.QueryString("msg")
    End If
    If Not IsPostBack Then

      Dim strSQL As String = "EXEC sp1Cidadao_Pega_Um '" & Session("id_municipio") & "','" & Session("id_cidadao") & "','" & Pega & "','" & Request.QueryString("sel_filtro") & "'"

      'Try
      Using adapter As New SqlDataAdapter(strSQL, Session("cnxStr").ToString)
        Dim ds As New DataSet
        adapter.Fill(ds, "CID")

        For Each dr As DataRow In ds.Tables("CID").Rows
          If Request.QueryString("SUS_novo") <> "" and  Session("id_cidadao") = "0" then
            txtSUS_num.Text = Request.QueryString("SUS_novo")
          Else
            txtSUS_num.Text = "" & dr("SUS_num")
          End If

          Session("id_cidadao") = "" & dr("id_cidadao")

          If Session("id_cidadao") = "0" then
            lblTitulo.Text = "Novo Cadastro"
            fld_upld_arquivo.Visible = False
            btn_envia_arq.Visible = false
            lbl_id_cidadao.Text = "Novo Cidadão"

          Else
            lbl_id_cidadao.Text = right("000000" & Session("id_cidadao"), 6)
          End If

          Me.txtSUS_num.Attributes.Add("onkeydown", "return SoNumero(this.id,event);")

          DrpDwn_fSit_Cadastro.SelectedValue = dr("id_sts")
          txtNome_registro.Text = "" & dr("nome_registro")
          txtNome_Social.Text = "" & dr("nome_social")
          txtDt_nasc.Text = "" & dr("dt_nasc")
          If "" & dr("idade_YMD") <> "" then
            lbl_idade_YMD.text = "Idade: " & dr("idade_YMD")
          Else 
            lbl_idade_YMD.Visible = False
          End If
          Me.txtDt_nasc.Attributes.Add("onkeydown", "return Formata_Data(this.id,event);")

          txtRG.Text = "" & dr("RG")
          txt_RG_orgao.Text = "" & dr("RG_orgao")
          drp_RG_UF.SelectedValue  = "" & dr("RG_UF")

          If Request.QueryString("CPF") <> "" and  Session("id_cidadao") = "0" and Request.QueryString("tpCPF") = "P" then
            txtCPF.Text = Request.QueryString("CPF")
          Else
            txtCPF.Text = "" & dr("CPF")
          End If
          Me.txtCPF.Attributes.Add("onkeypress", "formatar(this,'###.###.###-##'); return SomenteNumero(event);")
          If Request.QueryString("CPF") <> "" and  Session("id_cidadao") = "0" and Request.QueryString("tpCPF") = "R" then
            txt_CPF_resp.Text = Request.QueryString("CPF")
            lbl_nome_resp.Text = ""
          Else
            txt_CPF_resp.Text = "" & dr("CPF_resp")
            If "" & dr("nome_resp") <> "" then
              lbl_nome_resp.Text = dr("nome_resp")
            End If
            If "" & dr("id_cidadao_resp") <> "" then
              btn_dados_resp.Text = "" & dr("id_cidadao_resp")
              btn_dados_resp.Visible = True
              btn_dados_resp.OnClientClick="btn_imprimir_resp('" & dr("id_cidadao_resp") & "'); return false;"
            End If
          End If

          Me.txt_CPF_resp.Attributes.Add("onkeypress", "formatar(this,'###.###.###-##'); return SomenteNumero(event);")
          Me.txt_residencia_num_comodos.Attributes.Add("onkeypress", "return SomenteNumero(event);")

          If Request.QueryString("reg_novo") <> "" and  Session("id_cidadao") = "0" then
            drp_tipo_reg_nasc.SelectedValue = "1"
            txt_cod_nac.Text = Left(Request.QueryString("reg_novo"), 6)
            txt_cod_acervo.Text = mid(Request.QueryString("reg_novo"), 7, 2)
            txt_serv.Text = mid(Request.QueryString("reg_novo"), 9, 2)
            txt_ano_registro.Text = mid(Request.QueryString("reg_novo"), 11, 4)
            txt_tipo_livro.Text = mid(Request.QueryString("reg_novo"), 15, 1)
            txt_num_livro.Text = mid(Request.QueryString("reg_novo"), 16, 5)
            txt_folha_livro.Text = mid(Request.QueryString("reg_novo"), 21, 3)
            txt_num_termo.Text = mid(Request.QueryString("reg_novo"), 24, 7)
            txt_dig_verif.Text = mid(Request.QueryString("reg_novo"), 31, 2)
          ElseIf  Request.QueryString("regnum") <> "" and  Session("id_cidadao") = "0" then
            drp_tipo_reg_nasc.SelectedValue = "0"
            txt_num_reg_nasc.text = "" & Request.QueryString("regnum")
            txt_num_reg_nasc_livro.text = "" & Request.QueryString("regfolha")
            txt_num_reg_nasc_folha.Text = "" & Request.QueryString("reglivro")
          ElseIf "" & dr("num_reg_novo") <> "" and  Session("id_cidadao") <> "0" then
            drp_tipo_reg_nasc.SelectedValue = "1"
            pnl_reg_antigo.Visible = False
            pnl_reg_novo.Visible = true
            txt_cod_nac.Text = Left(dr("num_reg_novo"), 6)
            txt_cod_acervo.Text = mid(dr("num_reg_novo"), 7, 2)
            txt_serv.Text = mid(dr("num_reg_novo"), 9, 2)
            txt_ano_registro.Text = mid(dr("num_reg_novo"), 11, 4)
            txt_tipo_livro.Text = mid(dr("num_reg_novo"), 15, 1)
            txt_num_livro.Text = mid(dr("num_reg_novo"), 16, 5)
            txt_folha_livro.Text = mid(dr("num_reg_novo"), 21, 3)
            txt_num_termo.Text = mid(dr("num_reg_novo"), 24, 7)
            txt_dig_verif.Text = mid(dr("num_reg_novo"), 31, 2)
          Else
            drp_tipo_reg_nasc.SelectedValue = "0"

            pnl_reg_antigo.Visible = true
            pnl_reg_novo.Visible = false

            txt_num_reg_nasc.text = "" & dr("num_reg_nasc")
            txt_num_reg_nasc_livro.text = "" & dr("num_reg_nasc_livro")
            txt_num_reg_nasc_folha.Text = "" & dr("num_reg_nasc_folha")
          end if

          txt_reg_estudante.Text = "" & dr("reg_estudante")

          drp_PM_Matricula_ref.SelectedValue  = "" & dr("PM_Matricula_ref")
          txt_PM_CPF.text = "" & dr("PM_CPF")
          Me.txt_PM_CPF.Attributes.Add("onkeypress", "formatar(this,'###.###.###-##'); return SomenteNumero(event);")

          txtTitulo_eleitor.Text = "" & dr("titulo_eleitor")
          txttitulo_zona.Text = "" & dr("titulo_zona")
          txttitulo_Secao.Text = "" & dr("titulo_secao")
          DrpSexo.SelectedValue = "" & dr("sexo")
          Drp_id_estado_civil.SelectedValue = "" & dr("id_estado_civil")
          Drp_raca.SelectedValue = "" & dr("id_raca")
          drp_id_etnia.SelectedValue  = "" & dr("id_etnia")
          drp_cod_cbo.SelectedValue  = "" & dr("cod_cbo")
          drp_renda_familiar.SelectedValue  = "" & dr("id_renda_familiar")

          drp_id_religiao.SelectedValue = "" & dr("id_religiao")

          txt_nome_conjuge.Text = "" & dr("nome_conjuge")
          txt_CPF_conjuge.Text  = "" & dr("cpf_conjuge")
          Me.txt_CPF_conjuge.Attributes.Add("onkeypress", "formatar(this,'###.###.###-##'); return SomenteNumero(event);")

          drp_fDef.SelectedValue = dr("fDef")
          txt_Def_descr.Text = "" & dr("Def_descr")
          
          txt_conta_cao.Text  = "" & dr("conta_cao")
          txt_conta_gato.Text  = "" & dr("conta_gato")
          txt_conta_passaros.Text  = "" & dr("conta_passaros")
          txt_conta_outros.Text  = "" & dr("conta_outros")

          lbl_nome_usuario.Text   = "" & dr("nome_usuario")
          lbl_dt_alt.Text  = "" & dr("dt_alt")
          lbl_descr_local.Text = "" & dr("descr_local")

          txtNome_mae.Text = "" & dr("nome_mae")
          txt_CPF_mae.text = "" & dr("CPF_mae")
          Me.txt_CPF_mae.Attributes.Add("onkeypress", "formatar(this,'###.###.###-##'); return SomenteNumero(event);")
          txtNome_pai.Text = "" & dr("nome_pai")
          txt_CPF_pai.text = "" & dr("CPF_pai")
          Me.txt_CPF_pai.Attributes.Add("onkeypress", "formatar(this,'###.###.###-##'); return SomenteNumero(event);")

          txtTelefone.text = "" & dr("telefone")
          txt_celular.Text = "" & dr("celular")
          txt_tel_comercial.text = "" & dr("telefone_comercial")
          txtEmail.text = "" & dr("email")

          txt_num_instalacao.Text = "" & dr("num_instalacao")
          drptp_Residencia.SelectedValue = "" & dr("tpResidencia")
          drp_tp_moradia.SelectedValue = "" & dr("id_tp_moradia")
          drp_trata_agua.SelectedValue = "" & dr("id_trata_agua")
          drp_abast_agua.SelectedValue = "" & dr("id_abast_agua")
          drp_esgoto.SelectedValue = "" & dr("id_esgoto")
          drp_lixo_destino.SelectedValue = "" & dr("id_lixo_destino")
          txt_residencia_num_comodos.Text = "" & dr("residencia_num_comodos")

          Me.txtnum_instala_dt_emissao.Attributes.Add("onkeydown", "return Formata_Data(this.id,event);")

          If txt_num_instalacao.Text = "0" then txt_num_instalacao.Text = ""

          if dr("conta_num_instala") > 1 and txt_num_instalacao.Text <> "" then
            lbl_num_cidadao.Text = "Cidadãos Cadastrados: "
            lbl_num_cidadao.Visible = true
            btn_endereco_cidadao.Text = dr("conta_num_instala")
            btn_endereco_cidadao.OnClientClick = "btn_visualza_end_cidadao('" & txt_num_instalacao.Text & "');return false;"
            btn_endereco_cidadao.Visible = True
          End If

          If txt_num_instalacao.Text = "" then
            lbl_aviso_instala.Text = "Informe o número de instalação e clique em Editar."
            lbl_aviso_instala.Visible = true
            'pnf_endereco_visualiza.Visible = false
          Else
            lbl_cnpj_instalacao.Text = "" & dr("CPF_instala")
            lbl_num_instala_dt_emissao.Text = "" & dr("num_instala_dt_emissao")
            lbl_num_inscr_imovel.text = "" & dr("num_inscr_imovel")
            lbl_num_matri_imovel.Text = "" & dr("num_matri_imovel")
            lbl_xLgr_instalacao.text = "" & dr("xLgr_instala")
            lbl_nro_instalacao.Text = "" & dr("nro_instala")
            lbl_xCpl_instalacao.Text = "" & dr("xCpl_instala")
            lbl_xBairro_instalacao.Text = "" & dr("xBairro_instala")
            lbl_CEP_instalacao.Text = "" & dr("CEP_instala")
            lbl_UF_instalacao.Text = "" & dr("UF_instala")
            lbl_xMun_instalacao.Text = "" & dr("xMun_instalacao")
          end if

          txt_cpf_instalacao.Text = "" & dr("CPF_instala")
          txtnum_instala_dt_emissao.Text = "" & dr("num_instala_dt_emissao")

          txt_xLgr_instalacao.Text = "" & dr("xLgr_instala")
          txt_nro_instalacao.Text = "" & dr("nro_instala")
          txt_xCpl_instalacao.Text = "" & dr("xCpl_instala")
          txt_xBairro_instalacao.Text = "" & dr("xBairro_instala")
          txt_CEP_instalacao.Text = "" & dr("CEP_instala")
          txt_num_inscr_imovel.text = "" & dr("num_inscr_imovel")
          txt_num_matri_imovel.Text = "" & dr("num_matri_imovel")
          Drp_escolaridade.SelectedValue =  "" & dr("id_escolaridade")

          drp_UF_instalacao.SelectedValue = "" & dr("UF_instala")

          drp_num_reg_nasc_UF.SelectedValue  = "" & dr("num_reg_nasc_UF")

          Dim strSQL2 As String

          strSQL2 = "SELECT cMun, xMun FROM dbo.tbCad_Municipios WHERE (UF = '" & dr("UF_instala") & "')"
          drp_cMun_instalacao.DataSourceID = ""
          Using adapter2 As New SqlDataAdapter(strSQL2, Session("cnxStr").ToString)
            adapter2.Fill(tbMunicipio)
            drp_cMun_instalacao.DataSource = tbMunicipio
            drp_cMun_instalacao.DataTextField = "xMun"
            drp_cMun_instalacao.DataValueField = "cMun"
            drp_cMun_instalacao.DataBind()
          End Using
          drp_cMun_instalacao.SelectedValue = "" & dr("cMun_instala")

          strSQL2 = "SELECT cMun, xMun FROM dbo.tbCad_Municipios WHERE (UF = '" & "" & dr("num_reg_nasc_UF") & "')"
          drp_num_reg_nasc_cMun.DataSourceID = ""
          Using adapter2 As New SqlDataAdapter(strSQL2, Session("cnxStr").ToString)
            adapter2.Fill(tbMunicipio)
            drp_num_reg_nasc_cMun.DataSource = tbMunicipio
            drp_num_reg_nasc_cMun.DataTextField = "xMun"
            drp_num_reg_nasc_cMun.DataValueField = "cMun"
            drp_num_reg_nasc_cMun.DataBind()
          End Using
          drp_num_reg_nasc_cMun.SelectedValue = "" & dr("num_reg_nasc_cMun")

          If "" & dr("id_sts") <> "0" then
            If "" & dr("id_erro") <> "0" then
              labERRO.Visible = true
              labERRO.Text = "" & dr("descr_erro") & " <br />" & "" & dr("aviso_erro")
            Else
              pnl_botao_imprimir.Visible = False
              If Request.QueryString("msg") = "" then
                labERRO.Text = "Falha no cadastro."
                labERRO.Visible = true
              End If
            End If
          End If

          If "" & dr("doc1") <> "" then
            btn_doc1.Text =  "" & dr("doc1")
          Else
            btn_doc1.Visible = False
          End If

          If "" & dr("doc2") <> "" then
            btn_doc2.Text =  "" & dr("doc2")
          Else
            btn_doc2.Visible = False
          End If
          If "" & dr("doc3") <> "" then
            btn_doc3.Text =  "" & dr("doc3")
          Else
            btn_doc3.Visible = False
          End If
          If "" & dr("doc4") <> "" then
            btn_doc4.Text =  "" & dr("doc4")
          Else
            btn_doc4.Visible = False
          End If
          If "" & dr("doc5") <> "" then
            btn_doc5.Text =  "" & dr("doc5")
          Else
            btn_doc5.Visible = False
          End If

          If "" & dr("doc_foto") = "" then 
            btn_visualiza_foto.Visible = False
          Else
            btn_visualiza_foto.Visible = True
            btn_visualiza_foto.OnClientClick = "carrega_foto('" & dr("doc_foto") & "');return false;"
          end if

        Next

      End Using
    else
      drp_cMun_instalacao.DataSourceID = "sql_data_municipio"
      drp_num_reg_nasc_cMun.DataSourceID = "sql_municipio_reg_nasc"
    end if
  End Sub

  Protected Sub botCancela_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles botCancela.Click
    Page.Response.Redirect("cad_cidadao.aspx?sel_filtro=" & Request.QueryString("sel_filtro") & "&pg_=" & Request.QueryString("pg_"))
  End Sub

  Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click
    Gravar_Dados("G")
  End Sub

  Protected Sub botCancela0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles botCancela0.Click
    Page.Response.Redirect("cad_cidadao.aspx?sel_filtro=" & Request.QueryString("sel_filtro") & "&pg_=" & Request.QueryString("pg_"))
  End Sub

  Protected Sub btn_Salvar_2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar_2.Click
    Gravar_Dados("G")
  End Sub

  Private Sub Gravar_Dados (opcao As string)
    'If (Session("logado")) <> "T" then
    '  Response.Redirect ("../login_usuario.aspx")
    'End If

    Dim reg_num_nasc As String
    If drp_tipo_reg_nasc.SelectedValue.ToString = "0" then
      reg_num_nasc = txt_num_reg_nasc.Text.ToString
    Else
      reg_num_nasc = right("000000" & txt_cod_nac.Text, 6) & right("00" & txt_cod_acervo.Text, 2) & right("00" & txt_serv.text, 2) & right("0000" & txt_ano_registro.Text, 4) & right("0" & txt_tipo_livro.Text, 1) & right("00000" & txt_num_livro.Text, 5) & right("000" & txt_folha_livro.Text, 3) & right("0000000" & txt_num_termo.Text, 7) & right("00" & txt_dig_verif.Text, 2)
    End If

    Dim mDt_Nasc As Array 
    Dim Dt_Nasc As String
    mDt_Nasc = txtDt_nasc.Text.Split("/")

    Dt_Nasc = mDt_Nasc(2) & "-" & mDt_Nasc(1) & "-" & mDt_Nasc(0)

    Dim mDt_emissao As Array 
    Dim Dt_emissao As String

    If txtnum_instala_dt_emissao.Text.IndexOf("/", 0) > 0 then
      mDt_emissao = txtnum_instala_dt_emissao.Text.Split("/")
      Dt_emissao = mDt_emissao(2) & "-" & mDt_emissao(1) & "-" & mDt_emissao(0)
    Else
      Dt_emissao = ""
    end if

    Dim strSQL As String = "EXEC dbo.sp1Cidadao_Salva '" & Session("id_municipio").ToString & "','" & Session("id_cidadao").ToString & "','" & opcao & "'"
    strSQL = strSQL & ",'" & txtSUS_num.Text & "','" & txt_num_instalacao.Text & "','" & String_SQL(txtNome_registro.text) & "'"
    strSQL = strSQL & ",'" & String_SQL(txtNome_Social.text) & "','" & Dt_Nasc & "','" & Drp_id_estado_civil.SelectedValue.ToString & "'"
    strSQL = strSQL & ",'" & Drp_raca.SelectedValue.ToString & "','" & drp_id_etnia.SelectedValue & "','" & drp_id_religiao.SelectedValue & "','" & String_SQL(txtRG.text) & "'"
    strSQL = strSQL & ",'" & String_SQL(txt_RG_orgao.text) & "','" & drp_RG_UF.SelectedValue & "','','" & txtCPF.text & "','" & txt_CPF_resp.text & "','" & String_SQL(txtTitulo_eleitor.text) & "'"
    strSQL = strSQL & ",'" & txttitulo_zona.Text & "','" & txttitulo_Secao.text & "','" & drp_PM_Matricula_ref.SelectedValue & "','" & txt_PM_CPF.text & "'"
    strSQL = strSQL & ",'" & DrpSexo.SelectedValue.ToString & "','" & String_SQL(txt_nome_conjuge.Text) & "',''"
    strSQL = strSQL & ",'" & String_SQL(txtNome_mae.Text) & "','" & String_SQL(txtNome_pai.text) & "'"
    strSQL = strSQL & ",'" & String_SQL(txtTelefone.text) & "','" & String_SQL(txt_celular.Text) & "','" & String_SQL(txt_tel_comercial.Text) & "','" & String_SQL(txtEmail.text) & "'"
    strSQL = strSQL & ",'" & String_SQL(txt_conta_cao.Text) & "','" & String_SQL(txt_conta_gato.Text) & "','" & String_SQL(txt_conta_passaros.Text) & "','" & String_SQL(txt_conta_outros.Text) & "'"
    strSQL = strSQL & ",'" & DrpDwn_fSit_Cadastro.SelectedValue.ToString & "','" & Session("id_usuario") & "'"
    strSQL = strSQL & ",'" & String_SQL(reg_num_nasc) & "','" & String_SQL(txt_num_reg_nasc_livro.text) & "','" & String_SQL(txt_num_reg_nasc_folha.Text) & "','" & drp_num_reg_nasc_cMun.SelectedValue & "'"
    strSQL = strSQL & ",'" & txt_cpf_instalacao.text & "'"
    strSQL = strSQL & ",'" & String_SQL(txt_xLgr_instalacao.text) & "','" & String_SQL(txt_nro_instalacao.text) & "','" & String_SQL(txt_xCpl_instalacao.text) & "','" & String_SQL(txt_xBairro_instalacao.text) & "'"
    strSQL = strSQL & ",'" & String_SQL(txt_CEP_instalacao.text) & "','" & drp_cMun_instalacao.SelectedValue  & "','" & Drp_escolaridade.SelectedValue & "','" & drp_cod_cbo.SelectedValue & "'"
    strSQL = strSQL & ",'" & drptp_Residencia.SelectedValue & "','" & txt_CPF_conjuge.text & "','" & txt_CPF_mae.text & "','" & txt_CPF_pai.text & "'"
    strSQL = strSQL & ",'" & txt_num_inscr_imovel.text & "','" & drp_fDef.SelectedValue & "','" & txt_Def_descr.Text & "'"
    strSQL = strSQL & ",'" & String_SQL(txt_reg_estudante.text) & "','" & String_SQL(txt_num_matri_imovel.Text) & "','" & Dt_emissao & "'"
    strSQL = strSQL & ",'" & drp_tp_moradia.SelectedValue.ToString & "','" & drp_trata_agua.SelectedValue.ToString & "','" & drp_abast_agua.SelectedValue & "'"
    strSQL = strSQL & ",'" & drp_esgoto.SelectedValue.ToString & "','" & drp_lixo_destino.SelectedValue & "','" & txt_residencia_num_comodos.Text & "'"
    strSQL = strSQL & ",'" & drp_renda_familiar.SelectedValue & "'"
    
    'Try
    Using adapter As New SqlDataAdapter(strSQL, Session("cnxStr").ToString)
      Dim ds As New DataSet
      adapter.Fill(ds, "NFS")

      For Each dr As DataRow In ds.Tables("NFS").Rows
        Session("id_cidadao") = dr("id_cidadao")
        If Session("id_cidadao") <> "0" then
          lblTitulo.Text = "Edição de Cadastro"
        End If
        Page.Response.Redirect ("cad_cidadao_edita.aspx?msg=" & dr("resultado") & "&sel_filtro=" & Request.QueryString("sel_filtro") & "&pg_=" & Request.QueryString("pg_"))
      Next
    End Using
  End Sub

  Protected Sub btn_Atualiza_Endereco_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Atualiza_Endereco.Click
    Dim strSQL As String = "EXEC sp1Endereco_Pega_Um '" & Session("id_municipio") & "','" & txt_num_instalacao.text & "'"

    'Try
    Using adapter As New SqlDataAdapter(strSQL, Session("cnxStr").ToString)
      Dim ds As New DataSet
      adapter.Fill(ds, "CID")
      pnf_atualiza_endereco.Visible = True
      pnf_endereco_visualiza.Visible = false
      btn_endereco_cidadao.Visible = False
      For Each dr As DataRow In ds.Tables("CID").Rows
        txt_cpf_instalacao.Text = "" & dr("CPF_instala")
        Me.txt_cpf_instalacao.Attributes.Add("onkeypress", "formatar(this,'###.###.###-##'); return SomenteNumero(event);")

        txt_xLgr_instalacao.Text = dr("xLgr")
        txt_nro_instalacao.Text = "" & dr("nro_instala")
        txt_xCpl_instalacao.Text = "" & dr("xCpl_instala")
        txt_xBairro_instalacao.Text = "" & dr("xBairro_instala")
        txt_CEP_instalacao.Text = "" & dr("CEP_instala")
        txt_num_inscr_imovel.text = "" & dr("num_inscr_imovel")
        txt_num_matri_imovel.Text = "" & dr("num_matri_imovel") 
        drp_UF_instalacao.SelectedValue = "" & dr("UF_instala")

        Dim strSQL2 As String

        strSQL2 = "SELECT cMun, xMun FROM dbo.tbCad_Municipios WHERE (UF = '" & "" & dr("UF_instala") & "')"
        drp_cMun_instalacao.DataSourceID = ""
        Using adapter2 As New SqlDataAdapter(strSQL2, Session("cnxStr").ToString)
          adapter2.Fill(tbMunicipio)
          drp_cMun_instalacao.DataSource = tbMunicipio
          drp_cMun_instalacao.DataTextField = "xMun"
          drp_cMun_instalacao.DataValueField = "cMun"
          drp_cMun_instalacao.DataBind()
        End Using
        drp_cMun_instalacao.SelectedValue = "" & dr("cMun_instala")

        if dr("conta_num_instala") > 1 then
          lbl_num_cidadao.Text = "Cidadãos Cadastrados: " & dr("conta_num_instala")
          lbl_num_cidadao.Visible = true
          if dr("conta_num_instala") > 9 then
            lbl_erro_instalacao.Visible = True
            lbl_erro_instalacao.Text = "<br />Atenção:excedeu número de moradores na residência, aguarde a visita de um agente municipal."
          Else
            lbl_erro_instalacao.Visible = False
          End If
        Else
          lbl_erro_instalacao.Visible = False
        End If

      Next
    End Using      
  'Gravar_Dados("A")
  End Sub

  Protected Sub drp_tipo_reg_nasc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drp_tipo_reg_nasc.SelectedIndexChanged
    If drp_tipo_reg_nasc.SelectedValue.ToString = "0" then
      pnl_reg_antigo.Visible = True
      pnl_reg_novo.Visible = False
    Else
      pnl_reg_antigo.Visible = false
      pnl_reg_novo.Visible = true
    End If
  End Sub

  Protected Sub btn_envia_arq_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_envia_arq.Click
    lbl_aviso_envia.Visible = true

    Dim CurrentFileName As String
    CurrentFileName = fld_upld_arquivo.FileName

    If (Path.GetExtension(CurrentFileName).ToLower <> ".pdf") Then
        labERRO.Text = "O arquivo deve ser PDF."
        Exit Sub
    End If

    'If fld_upld_arquivo.PostedFile.ContentLength > 131072 Then
    '    Response.Write("The size of big too file , the size of the file must 128 KB not exceed!!! ")
    '    Exit Sub
    'End If

    Dim CurrentPath As String = Server.MapPath("docs/" & Session("id_municipio"))

    If Not Directory.Exists(CurrentPath) Then
        ' Create the directory.
        Directory.CreateDirectory(CurrentPath)
    End If

    CurrentPath = CurrentPath & "/" & right("0000000" & Session("id_cidadao"), 7)

    If Not Directory.Exists(CurrentPath) Then
        ' Create the directory.
        Directory.CreateDirectory(CurrentPath)
    End If

    If fld_upld_arquivo.HasFile Then
      CurrentPath += "/" & CurrentFileName
      fld_upld_arquivo.SaveAs(CurrentPath)
      labERRO.Text = "Arquivo enviado com sucesso."

      Dim strSQL As String = "EXEC dbo.sp1Cidadao_Salva_Docs '" & Session("id_municipio").ToString & "','" & Session("id_cidadao").ToString & "','G','" & CurrentFileName & "'"

      'Try
      Using adapter As New SqlDataAdapter(strSQL, Session("cnxStr").ToString)
        Dim ds As New DataSet
        adapter.Fill(ds, "NFS")

      End Using
      Page.Response.Redirect ("cad_cidadao_edita.aspx?msg=Arquivo enviado com sucesso.&sel_filtro=" & Request.QueryString("sel_filtro") & "&pg_=" & Request.QueryString("pg_"))
    Else
        labERRO.Text = "Falha no envio do arquivo."
    End If
  End Sub

  Protected Sub btn_profissao_pesq_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_profissao_pesq.Click

    Dim strSQL2 = "SELECT cod_cbo, left(descr_cbo, 50) as descr_cbo FROM dbo.tb_CBO_Profissoes WHERE (descr_cbo like '%" & txt_descr_profissao.Text & "%')"
    drp_cod_cbo.DataSourceID = ""
    Using adapter As New SqlDataAdapter(strSQL2, Session("cnxStr").ToString)
      adapter.Fill(tbCBO_Profissoes)
      drp_cod_cbo.DataSource = tbCBO_Profissoes
      drp_cod_cbo.DataTextField = "descr_cbo"
      drp_cod_cbo.DataValueField = "cod_cbo"
      drp_cod_cbo.DataBind()
    End Using
    If drp_cod_cbo.Items.Count = 0
      drp_cod_cbo.DataTextField = "descr_cbo"
      drp_cod_cbo.DataValueField = "cod_cbo"
      drp_cod_cbo.DataBind()
      drp_cod_cbo.Items.Insert (0, "NENHUM PROFISSÃO LOCALIZADA. PESQUISE NOVAMENTE.")
      drp_cod_cbo.Items(0).value = "999999"

    End If
  End Sub

  Protected Sub bnt_anterior_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bnt_anterior.Click
    Page.Response.Redirect ("cad_cidadao_edita.aspx?msg=&sel_filtro=" & Request.QueryString("sel_filtro") & "&pg_=" & Request.QueryString("pg_") & "&pega_=A")
  End Sub

  Protected Sub btn_proximo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_proximo.Click
    Page.Response.Redirect ("cad_cidadao_edita.aspx?msg=&sel_filtro=" & Request.QueryString("sel_filtro") & "&pg_=" & Request.QueryString("pg_") & "&pega_=P")
  End Sub

  Protected Sub btn_envia_foto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_envia_foto.Click
    lbl_aviso_envia.Visible = true

    Dim CurrentFileName As String
    CurrentFileName = fld_up_foto.FileName

    If (Path.GetExtension(CurrentFileName).ToLower <> ".jpg" and (Path.GetExtension(CurrentFileName).ToLower <> ".png")) Then
        labERRO.Text = "O arquivo deve ser JPG ou PNG."
        Exit Sub
    End If

    'If fld_upld_arquivo.PostedFile.ContentLength > 131072 Then
    '    Response.Write("The size of big too file , the size of the file must 128 KB not exceed!!! ")
    '    Exit Sub
    'End If

    Dim CurrentPath As String = Server.MapPath("docs/" & Session("id_municipio"))

    If Not Directory.Exists(CurrentPath) Then
        ' Create the directory.
        Directory.CreateDirectory(CurrentPath)
    End If

    CurrentPath = CurrentPath & "/" & right("0000000" & Session("id_cidadao"), 7)

    If Not Directory.Exists(CurrentPath) Then
        ' Create the directory.
        Directory.CreateDirectory(CurrentPath)
    End If

    CurrentPath = CurrentPath & "/fotos"

    If Not Directory.Exists(CurrentPath) Then
        ' Create the directory.
        Directory.CreateDirectory(CurrentPath)
    End If

    If fld_up_foto.HasFile Then
      CurrentPath += "/" & CurrentFileName
      fld_up_foto.SaveAs(CurrentPath)
      labERRO.Text = "Arquivo enviado com sucesso."

      Dim strSQL As String = "EXEC dbo.sp1Cidadao_Salva_Docs '" & Session("id_municipio").ToString & "','" & Session("id_cidadao").ToString & "','F','" & CurrentFileName & "'"

      'Try
      Using adapter As New SqlDataAdapter(strSQL, Session("cnxStr").ToString)
        Dim ds As New DataSet
        adapter.Fill(ds, "NFS")

      End Using
      Page.Response.Redirect ("cad_cidadao_edita.aspx?msg=Foto enviada com sucesso.&sel_filtro=" & Request.QueryString("sel_filtro") & "&pg_=" & Request.QueryString("pg_"))
    Else
        labERRO.Text = "Falha no envio do arquivo."
    End If
  End Sub

  Function String_SQL(texto As String) As String
    String_SQL = Replace(texto, "'", "''")
  End Function
End Class