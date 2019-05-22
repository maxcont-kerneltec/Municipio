Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration

Partial Class Cad_Default
  Inherits System.Web.UI.Page
  Dim tbMunicipio As New DataTable
  Dim id_cidadao As String

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    'If (Session("logado")) <> "T" then
    '  Response.Redirect ("../login_usuario.aspx")
    'End If

    If Request.QueryString("id_cidadao") <> "" then
      id_cidadao = Request.QueryString("id_cidadao")
    Else
      id_cidadao = Session("id_cidadao")

    End If

    If Not IsPostBack Then
      Dim strSQL As String = "EXEC sp1Cidadao_Pega_Um '" & Session("id_municipio") & "','" & id_cidadao & "'"

      If Request.QueryString("tpImp") = "PROT" then
        pnl_prococolo.Visible = true
        pnl_completo.Visible = False
      Else
        pnl_prococolo.Visible = False
        pnl_completo.Visible = true
      End If

      'Try
      Using adapter As New SqlDataAdapter(strSQL, Session("cnxStr").ToString)
        Dim ds As New DataSet
        adapter.Fill(ds, "CID")
        For Each dr As DataRow In ds.Tables("CID").Rows
          lbl_id_cidadao.Text = right("000000" & dr("id_cidadao"), 6)
          lbl_id_cidadao_2.Text  = right("000000" & dr("id_cidadao"), 6)
          lbl_nome.Text =  "" & dr("nome_registro")
          lbl_dt_alt.Text =  "" & dr("dt_alt")
          lbl_sus.Text =  "" & dr("SUS_num")

          lbl_SUS_2.Text =  "" & dr("SUS_num")
          lbl_nome_registro.Text = dr("nome_registro")
          lbl_nome_social.Text = "" & dr("nome_social")
          lbl_dt_Nasc.Text = "" & dr("dt_nasc")
          lbl_idade_YMD.Text = "Idade: " & dr("idade_YMD")
          lbl_RG.Text = "" & dr("RG")
          lbl_RG_orgao.Text = "" & dr("RG_orgao")
          lbl_RG_UF.Text  = "" & dr("RG_UF")

          lbl_CPF.Text = "" & dr("CPF")

          lbl_CPF_resp.Text = "" & dr("CPF_resp")
          If "" & dr("nome_resp") <> "" then
            lbl_nome_resp.Text = dr("nome_resp")
          End If
          'drp_tipo_reg_nasc.Enabled = False

          If "" & dr("num_reg_novo") <> "" and  Session("id_cidadao") <> "0" then
            'drp_tipo_reg_nasc.SelectedValue = "1"
            pnl_reg_antigo.Visible = False
            pnl_reg_novo.Visible = true
            lbl_cod_nac.Text = Left(dr("num_reg_novo"), 6)
            lbl_cod_acervo.Text = mid(dr("num_reg_novo"), 7, 2)
            lbl_serv.Text = mid(dr("num_reg_novo"), 9, 2)
            lbl_ano_registro.Text = mid(dr("num_reg_novo"), 11, 4)
            lbl_tipo_livro.Text = mid(dr("num_reg_novo"), 15, 1)
            lbl_num_livro.Text = mid(dr("num_reg_novo"), 16, 5)
            lbl_folha_livro.Text = mid(dr("num_reg_novo"), 21, 3)
            lbl_num_termo.Text = mid(dr("num_reg_novo"), 24, 7)
            lbl_dig_verif.Text = mid(dr("num_reg_novo"), 31, 2)
          Else
            'drp_tipo_reg_nasc.SelectedValue = "0"

            pnl_reg_antigo.Visible = true
            pnl_reg_novo.Visible = false

            lbl_num_reg_nasc.text = "" & dr("num_reg_nasc")
            lbl_num_reg_nasc_livro.text = "" & dr("num_reg_nasc_livro")
            lbl_num_reg_nasc_folha.Text = "" & dr("num_reg_nasc_folha")
          end if

          lbl_reg_nasc_UF.Text = "" & dr("num_reg_nasc_UF")
          lbl_xMun_reg_nasc.Text = "" & dr("xMun_reg_nasc")

          lbl_reg_estudante.Text = "" & dr("reg_estudante")

          If "" & dr("PM_CPF") <> "" then
            lbl_fPM_Func.Text = "Sim"
          Else
            lbl_fPM_Func.Text = "Não"
          End If

          lbl_PM_CPF.text = "" & dr("PM_CPF")

          If "" & dr("titulo_eleitor") <> "0" then
            lblTitulo_eleitor.Text = "" & dr("titulo_eleitor")
          Else
            lblTitulo_eleitor.Text = ""
          End If

          If "" & dr("titulo_zona") <> "0" then
            lbltitulo_zona.Text = "" & dr("titulo_zona")
          Else
            lbltitulo_zona.Text = ""
          End If

          If "" & dr("titulo_secao") <> "0" then
            lbltitulo_Secao.Text = "" & dr("titulo_secao")
          Else
            lbltitulo_Secao.Text = ""
          End If

          If "" & dr("sexo") = "M" then
            lbl_sexo.Text = "Masculino"
          Else
            lbl_sexo.Text = "Feminino"
          End If

          lbl_estado_civil.Text = "" & dr("descr_estado_civial")
          lbl_raca_cor.Text = "" & dr("descr_raca_cor")
          lbl_descr_etnia.Text = "" & dr("decr_etnia")
          lbl_descr_escolaridade.Text =  "" & dr("descr_escolaridade")
          lbl_descr_cbo.Text  = "" & dr("descr_cbo")
          lbl_descr_religiao.Text = "" & dr("descr_religiao")

          lbl_nome_conjuge.Text = "" & dr("nome_conjuge")
          lbl_CPF_conjuge.Text  = "" & dr("cpf_conjuge")

          If dr("fDef") = "S" then
            lbl_fDef.Text = "Sim"
          Else
            lbl_fDef.Text = "Não"
          End If
          lbl_Def_descr.Text = "" & dr("Def_descr")
          
          lbl_conta_cao.Text  = "" & dr("conta_cao")
          lbl_conta_gato.Text  = "" & dr("conta_gato")
          lbl_conta_passaros.Text  = "" & dr("conta_passaros")
          lbl_conta_outros.Text  = "" & dr("conta_outros")

          lbl_nome_usuario.Text   = "" & dr("nome_usuario")
          lbl_dt_alt2.Text   = "" & dr("dt_alt")
          lbl_descr_local.Text = "" & dr("descr_local")

          lblNome_mae.Text = "" & dr("nome_mae")
          lbl_CPF_mae.text = "" & dr("CPF_mae")

          lblNome_pai.Text = "" & dr("nome_pai")
          lbl_CPF_pai.text = "" & dr("CPF_pai")

          lblTelefone.text = "" & dr("telefone")
          lbl_celular.Text = "" & dr("celular")
          lbl_tel_comercial.text = "" & dr("telefone_comercial")
          lbl_email.text = "" & dr("email")

          lbl_num_instalacao.Text = "" & dr("num_instalacao")

          lbl_tpResidencia.Text = "" & dr("descr_tpResidencia")
          lbl_descr_tp_moradia.Text = "" & dr("descr_tp_moradia")
          lbl_descr_trata_agua.Text = "" & dr("descr_trata_agua")
          lbl_descr_abast_agua.Text = "" & dr("descr_abast_agua")
          lbl_descr_esgoto.Text = "" & dr("descr_esgoto")
          lbl_descr_lixo_destino.Text = "" & dr("descr_lixo_destino")
          lbl_residencia_num_comodos.Text = "" & dr("residencia_num_comodos")

          If lbl_num_instalacao.Text = "0" then lbl_num_instalacao.Text = ""

          if dr("conta_num_instala") > 1 and lbl_num_instalacao.Text <> "" then
            lbl_num_cidadao.Text = "Cidadãos Cadastrados: "
            lbl_num_cidadao.Visible = true
            btn_endereco_cidadao.Text = dr("conta_num_instala")
            btn_endereco_cidadao.OnClientClick = "btn_visualza_end_cidadao('" & lbl_num_instalacao.Text & "');return false;"
            btn_endereco_cidadao.Visible = True
          End If

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

          If "" & dr("id_sts") <> "0" then 'falha no cadastro
            If "" & dr("id_erro") <> "0" then
              lbl_situacao.Text = "" & dr("aviso_erro")
              lbl_situacao_2.Text = "" & dr("descr_erro") & "<br />" & "" & dr("aviso_erro")
            Else
              lbl_situacao.Text = "Falha no Cadastro"
              lbl_situacao_2.Text = "Falha no Cadastro"
            End If
          Else
            lbl_situacao.Text = "Ativo"
            lbl_situacao_2.Text = "Ativo"
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
            img_foto_cidadao.Visible = False
            img_foto_cidadao2.Visible = False
          Else
            img_foto_cidadao.Visible = True
            img_foto_cidadao2.Visible = True
            img_foto_cidadao.ImageUrl = "docs/" & Session("id_municipio") & "/" & right("0000000" & id_cidadao, 7) & "/fotos/" & dr("doc_foto")
            img_foto_cidadao2.ImageUrl = "docs/" & Session("id_municipio") & "/" & right("0000000" & id_cidadao, 7) & "/fotos/" & dr("doc_foto")
          End If

        Next
      End Using

    End If

  End Sub
End Class
