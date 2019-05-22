Imports System.Data.SqlClient
Imports System.Data

Partial Class login_cidadao
  Inherits System.Web.UI.Page

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Session("dominio") = "BARUERI"
    Session("id_usuario") = "0"
    txtLogin.Focus()
    errorLabel.Text = ""
  End Sub

  Protected Sub btLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btLogin.Click
    Dim cnnSQL As String = "Data Source=krnserver3;Initial Catalog=municipio;Persist Security Info=True;User ID=plesk;Password=kfool60"
    Dim strSQL As String = "EXEC spLogin_Municipio '" & Session("dominio") & "','" & Replace(txtLogin.Text, "'", "''") & "','" & Replace(txtSenha.Text, "'", "''") & "','" & Session.SessionID & "','" & Request.ServerVariables("REMOTE_ADDR") & "'"

    errorLabel.Text = ""
    'Try
    Using adapter As New SqlDataAdapter(strSQL, cnnSQL)
      Dim ds As New DataSet
      adapter.Fill(ds, "Login_Result")

      errorLabel.Text = ""
      Dim conta As Integer = 0
      For Each dr As DataRow In ds.Tables("Login_Result").Rows
        conta = conta + 1
        Session("id_usuario") = dr("id_usuario").ToString
        Session("permissao") = dr("permissao").ToString
      Next

      'For Each dc As DataColumn In ds.Tables("Login_Result").Columns
      'errorLabel.Text = errorLabel.Text & String.Format("{0} ({1}) {2} ", dc.ColumnName, dc.DataType, dc.value)
      'Next dc

      Response.Redirect("declaracoes.aspx")
      errorLabel.Text = "OK"

    End Using

    'Catch
    'Session("ID_Contrib") = "J0"
    'End Try

  End Sub

End Class
