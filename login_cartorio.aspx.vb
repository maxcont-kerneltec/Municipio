Imports System.Data.SqlClient
Imports System.Data

Partial Class login_cartorio
  Inherits System.Web.UI.Page

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    If Request.QueryString("a") = "logout" Then
      Session.Abandon()
      errorLabel.Text = "Sessão Encerrada."
      Response.Redirect("login_cartorio.aspx")
    Else
      Session("id_municipio") = "10"
      'Session("cnxStr") = "Data Source=krnserver3;Initial Catalog=municipio;Persist Security Info=True;User ID=plesk;Password=kfool60"
      Session("cnxStr") = "Data Source=189.126.98.131;Initial Catalog=municipio;Persist Security Info=True;User ID=municipio;Password=fooliut5660"
      Session("id_usuario") = "0"
      txtLogin.Focus()
      errorLabel.Text = ""
    End If

  End Sub

  Protected Sub btLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btLogin.Click
    Session("cnxStr") = "Data Source=189.126.98.131;Initial Catalog=municipio;Persist Security Info=True;User ID=municipio;Password=fooliut5660"
    Dim cnnSQL As String = Session("cnxStr").ToString
    Dim strSQL As String = "EXEC sp3Cartorio_Login '" & Session("id_municipio") & "','" & Replace(txtLogin.Text, "'", "''") & "','" & Replace(txtSenha.Text, "'", "''") & "','0',''"

    errorLabel.Text = ""
    'Try
    Using adapter As New SqlDataAdapter(strSQL, cnnSQL)
      Dim ds As New DataSet
      adapter.Fill(ds, "Login_Result")

      errorLabel.Text = ""
      Dim conta As Integer = 0
      For Each dr As DataRow In ds.Tables("Login_Result").Rows
        Session("Logado") = dr("logado").ToString
        Session("id_municipio") = dr("id_municipio").ToString
        Session("id_cartorio") = dr("id_cartorio").ToString
        Session("id_usuario") = dr("id_usuario").ToString
        Session("permissao") = dr("permissao").ToString
      Next

      'For Each dc As DataColumn In ds.Tables("Login_Result").Columns
      'errorLabel.Text = errorLabel.Text & String.Format("{0} ({1}) {2} ", dc.ColumnName, dc.DataType, dc.value)
      'Next dc
      If Session("Logado") = "T" Then
        Response.Redirect("Cartorio/cart_inicio.aspx")
        errorLabel.Text = "OK"
      Else
        errorLabel.Text = "Falha na autenticação"
      End If

    End Using

  End Sub

End Class
