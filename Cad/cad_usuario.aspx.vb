
Partial Class Cad_usuario
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
      Me.txt_cpf_pesq.Attributes.Add("onkeypress", "formatar(this,'###.###.###-##'); return SomenteNumero(event);")
      If Session("permissao") <> 10 'Apenas permissão Master permite incluir usuário
        pnl_novo.Visible = False
      End If
    End Sub

    Protected Sub botNovo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles botNovo.Click
        Session("id_usuario_edita") = "0"
        Response.Redirect("cad_usuario_edita.aspx")
    End Sub

    Protected Sub Acessa_Cadastro(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)
        Session("id_usuario_edita") = e.CommandArgument.ToString
        Page.Response.Redirect("cad_usuario_edita.aspx")
    End Sub

    Protected Sub botPesquisa_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles botPesquisa.Click
        GridView_Usuarios.DataBind()
    End Sub
End Class
