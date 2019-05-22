Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration

Partial Class cad_entidades
  Inherits System.Web.UI.Page

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    'If (Session("logado")) <> "T" then
    '  Response.Redirect ("../login_usuario.aspx")
    'End If
    Me.txt_CNPJ.Attributes.Add("onkeypress", "formatar(this,'###.###.###-##'); return SomenteNumero(event);")
    Me.txt_CNPJ.Attributes.Add("onkeypress", "formatar(this,'##.###.###/####-##'); return SomenteNumero(event);")
    Me.txt_CPF_resp.Attributes.Add("onkeypress", "formatar(this,'###.###.###-##'); return SomenteNumero(event);")

    If Not IsPostBack Then

    End If

  End Sub

  Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
    If (Session("logado")) <> "T" then
      Response.Redirect ("../login_usuario.aspx")
    End If
  End Sub  

  Protected Sub btn_incluir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_incluir.Click
    'If (Session("logado")) <> "T" then
    '  Response.Redirect ("../login_usuario.aspx")
    'End If

    Dim strSQL As String = "EXEC dbo.sp1Cad_Entidade_Salva_Um '" & Session("id_municipio").ToString & "','" & txt_CNPJ.Text & "','" & txt_num_instalacao.Text & "'"
    strSQL = strSQL & ",'" & txt_razao_social.Text & "','" & txt_xLgr.Text & "','" & txt_nro.Text & "'"
    strSQL = strSQL & ",'" & txt_xCpl.Text & "','" & txt_xBairro.Text & "','" & txt_CEP.Text & "'"
    strSQL = strSQL & ",'" & txt_telefone.Text & "','" & txt_email.Text & "','" & drp_tp_entidade.SelectedValue.ToString & "','" & txt_num_max_permite.Text & "'"
    strSQL = strSQL & ",'" & txt_num_inscr_imovel.Text & "','" & txt_num_matri_imovel.Text & "','" & txt_CPF_resp.Text & "','" & txt_nome_responsavel.text & "','" & Session("id_usuario") & "'"
    
    'Page.Response.Redirect ("cad_endereco_social.aspx?sql_=" & strSQL)

    'Try
    Using adapter As New SqlDataAdapter(strSQL, Session("cnxStr").ToString)
      Dim ds As New DataSet
      adapter.Fill(ds, "GRAVA")

      For Each dr As DataRow In ds.Tables("GRAVA").Rows
        'Session("id_cidadao") = dr("id_cidadao")
        Page.Response.Redirect ("cad_entidades.aspx")
      Next
    End Using

  End Sub

  Protected Sub btn_novo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_novo.Click
    pnl_novo.Visible = True
    btn_incluir.Visible = False
    lbl_resultado.Text = "Digite o Número da instalação e o CNPJ e clique em Consultar"
    lbl_resultado.Visible = True
    btn_conta_num_cidadao.Text = "-"
    pnl_dados.Visible = False
    txt_CNPJ.Text = ""
    txt_CNPJ.Visible = True
    lbl_cnpj.Visible = False
    txt_num_instalacao.Text = ""
    txt_num_instalacao.Visible = True
    lbl_num_instalacao.Visible = False
    btn_consultar.Visible = True
  End Sub

  Protected Sub btn_cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancelar.Click
    pnl_novo.Visible = false
    pnl_dados.Visible = False
  End Sub

  Protected Sub Acessa_Cadastro(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)
    Consultar_Cadastro(e.CommandArgument.ToString)
    pnl_novo.Visible = True
    lbl_resultado.Text = ""
    btn_consultar.Visible = False
    'Page.Response.Redirect("cad_cidadao_edita.aspx?sel_filtro=" & drp_tipo_filtro.SelectedValue.ToString & "&pg_=" & GridView_Cidadaos.PageIndex)
  End Sub

  Protected Sub Consultar_Cadastro(cnpj As string)
    Dim strSQL As String = "EXEC sp1Cad_Entidade_Pega_Um '" & Session("id_municipio") & "','" & cnpj & "','" & txt_num_instalacao.Text & "'"

    'Try
    Using adapter As New SqlDataAdapter(strSQL, Session("cnxStr").ToString)
      Dim ds As New DataSet
      adapter.Fill(ds, "END")

      For Each dr As DataRow In ds.Tables("END").Rows
        If "" & dr("CNPJ") = "0" then
          lbl_resultado.Text = "CNPJ inválido."
        Else
          lbl_resultado.Text = "Informe os dados para continuar o cadastro."
          pnl_dados.Visible = True
          txt_num_instalacao.Text = dr("num_instalacao")
          lbl_num_instalacao.Text = dr("num_instalacao")
          lbl_num_instalacao.Visible = True

          txt_CNPJ.Text = "" & dr("CNPJ")
          lbl_cnpj.Text = "" & dr("CNPJ")
          lbl_cnpj.Visible = True
          txt_CNPJ.Visible = False
          txt_num_instalacao.Visible = False
        End If
        txt_num_max_permite.Text = "" & dr("num_max_permite")
        txt_razao_social.Text = "" & dr("razao_social")
        txt_xLgr.Text = "" & dr("xLgr")
        txt_nro.Text = "" & dr("nro")
        txt_xCpl.Text = "" & dr("xCpl")
        txt_xBairro.Text = "" & dr("xBairro")
        txt_CEP.Text = "" & dr("CEP")
        txt_num_inscr_imovel.Text = "" & dr("num_inscr_imovel")
        txt_num_matri_imovel.text = "" & dr("num_matri_imovel")
        txt_telefone.Text = "" & dr("telefone")
        txt_email.Text = "" & dr("email")
        txt_CPF_resp.Text = "" & dr("CPF_resp")
        txt_nome_responsavel.Text = "" & dr("nome_resp")
        btn_conta_num_cidadao.Text = dr("conta_num_cidadao")
        If dr("conta_num_cidadao") > 0 then
          btn_conta_num_cidadao.OnClientClick = "btn_visualza_end_cidadao('" & txt_num_instalacao.Text & "');"
        End If
      Next

    End Using
  End Sub

  Protected Sub btn_consultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_consultar.Click
    Consultar_Cadastro(txt_CNPJ.text)
  End Sub
End Class
