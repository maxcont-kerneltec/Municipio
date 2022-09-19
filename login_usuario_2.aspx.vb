Imports System.Data.SqlClient
Imports System.Data

Partial Class login_usuario
  Inherits System.Web.UI.Page

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Session("id_municipio") = "0"
    'Session("cnxStr") = "Data Source=krnserver3;Initial Catalog=municipio;Persist Security Info=True;User ID=plesk;Password=kfool60"
    Session("cnxStr") = "Data Source=189.126.98.131;Initial Catalog=municipio;Persist Security Info=True;User ID=municipio;Password=fooliut5660"
    Session("id_usuario") = "0"
    txtLogin.Focus()
    errorLabel.Text = ""

  End Sub

  Protected Sub btLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btLogin.Click
    Dim cnnSQL As String = Session("cnxStr").ToString
    Session("id_municipio") = drp_municipios.SelectedValue
    Dim strSQL As String = "EXEC spLogin_Usuario '" & Session("id_municipio") & "','" & Replace(txtLogin.Text, "'", "''") & "','" & Replace(txtSenha.Text, "'", "''") & "','0','" & Request.ServerVariables("REMOTE_ADDR") & "'"

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
        Session("id_usuario") = dr("id_usuario").ToString
        Session("permissao") = dr("permissao").ToString
      Next

      'For Each dc As DataColumn In ds.Tables("Login_Result").Columns
      'errorLabel.Text = errorLabel.Text & String.Format("{0} ({1}) {2} ", dc.ColumnName, dc.DataType, dc.value)
      'Next dc
      If Session("Logado") = "T" then
        Response.Redirect("Cad/cad_inicio.aspx")
        errorLabel.Text = "OK"
      Else
        errorLabel.Text = "Falha na autenticação"
      end if

    End Using

  End Sub

End Class
