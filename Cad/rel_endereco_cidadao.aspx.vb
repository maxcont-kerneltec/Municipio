Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration

Partial Class Rel_Endereco_Cidadao
  Inherits System.Web.UI.Page

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    If (Session("logado")) <> "T" then
      Response.Redirect ("../login_usuario.aspx")
    End If
    lbl_num_instala.Text = Request.QueryString("num_instalacao")

    If Request.QueryString("tpVisualiza") = "COMP" then
      pnl_acao.Visible = True
    End If

    Dim dvSql As DataView = DirectCast(sql_endereco_detalhes.Select(DataSourceSelectArguments.Empty), DataView)
    For Each drvSql As DataRowView In dvSql
      lbl_logradouro.Text = drvSql("xLgr").ToString()
      lbl_nro.Text = drvSql("nro").ToString()
      lbl_complemento.Text = drvSql("xCpl").ToString()
      lbl_bairro.Text = drvSql("xBairro").ToString()
      lbl_cep.Text = drvSql("CEP").ToString()
      lbl_xMun.Text = drvSql("xMun").ToString()
      lbl_UF.Text = drvSql("UF").ToString()
      lbl_num_inscr_imovel.Text = drvSql("num_inscr_imovel").ToString()
      lbl_num_matri_imovel.Text = drvSql("num_matri_imovel").ToString()
    Next
  End Sub

  Protected Sub btn_voltar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_voltar.Click
    Response.Redirect("rel_endereco_conta_cidadao.aspx")
  End Sub
End Class
