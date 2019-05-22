Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration

Partial Class rel_endereco_conta_cidadao
  Inherits System.Web.UI.Page

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    If (Session("logado")) <> "T" then
      Response.Redirect ("../login_usuario.aspx")
    End If

  End Sub

  Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
    If (Session("logado")) <> "T" then
      Response.Redirect ("../login_usuario.aspx")
    End If
  End Sub  
End Class
