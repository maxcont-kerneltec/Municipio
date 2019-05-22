Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration

Partial Class MasterPage
    Inherits System.Web.UI.MasterPage

  'protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs)Handles Me.Init
  '  if Page.Request.ServerVariables("http_user_agent").ToLower.IndexOf("safari") > 0 then
  '    Page.ClientTarget = "uplevel"
  '  end if
  'End Sub 

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    If Not IsPostBack Then
      Dim strSQL As String = "EXEC sp1Usuario_Pega_Um '" & Session("id_municipio") & "','" & Session("id_usuario") & "'"
      'Try
      Using adapter As New SqlDataAdapter(strSQL, Session("cnxStr").ToString)
          Dim ds As New DataSet
          adapter.Fill(ds, "USUARIO")

          For Each dr As DataRow In ds.Tables("USUARIO").Rows
            If dr("permissao") <> 10 Then 'diferente de Master permissão 10 elimina cadastro de usuário
              'Menu1.Items.Remove(Menu1.Items(1))
              Dim homeMenuItem As MenuItem = Menu1.Items(1)
              Dim movieSubMenuItem As MenuItem '= Menu1.FindItem("Cadastro/Usuário")

                  ' Remove the Movie submenu item.
              'If movieSubMenuItem IsNot Nothing Then
              '  homeMenuItem.ChildItems.Remove(movieSubMenuItem)
              'End If

              movieSubMenuItem = Menu1.FindItem("Cadastro/Entidades")
                  ' Remove the Movie submenu item.
              If movieSubMenuItem IsNot Nothing Then
                homeMenuItem.ChildItems.Remove(movieSubMenuItem)
              End If
              'movieSubMenuItem = Menu1.Items(1)
              homeMenuItem = Menu1.Items(2)
              movieSubMenuItem = Menu1.FindItem("Relatórios/Estatísticas")
                  ' Remove the Movie submenu item.
              If movieSubMenuItem IsNot Nothing Then
                homeMenuItem.ChildItems.Remove(movieSubMenuItem)
              End If

            End If

          Next
      End Using
    End If
  End Sub

  Protected Sub btn_sair_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_sair.Click
    Session.Abandon()
    FormsAuthentication.SignOut()
    Response.Redirect("login_usuario.aspx")
  End Sub
End Class

