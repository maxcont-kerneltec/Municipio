
Partial Class Cart_guias
  Inherits System.Web.UI.Page

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    If (Session("logado")) <> "T" Then
      Response.Redirect("../login_cartorio.aspx")
    End If

  End Sub

  Protected Sub botNovo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles botNovo.Click
    Session("id_guia") = "0"
    Response.Redirect("cart_guias_edita.aspx")
  End Sub

  Protected Sub Acessa_Cadastro(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)
    '    Session("id_guia") = e.CommandArgument.ToString
    '   Page.Response.Redirect("cart_guias_edita.aspx")

    Response.Write("<SCRIPT language=javascript>")
    Response.Write("var boleto=window.open('guia_itbi.asp?g_=" & e.CommandArgument.ToString & "','_Boleto','width=680,height=600,location=no,menubar=yes,scrollbars=yes,toolbar=yes,resizable=yes,fullscreen=no,status=no,titlebar=no');")
    'Response.Write("boleto.moveTo(0,0);")
    Response.Write("</SCRIPT>")

  End Sub

  Protected Sub botPesquisa_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles botPesquisa.Click
    GridView_Guias.DataBind()
  End Sub
End Class
