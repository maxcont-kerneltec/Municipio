Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration

Partial Class login_usuario
  Inherits System.Web.UI.Page

'busca string de conexão a partir do WebConfig
Private Sub GetAllConnectionStrings()
    Dim collCS As ConnectionStringSettingsCollection
    Try
        collCS = ConfigurationManager.ConnectionStrings
    Catch ex As Exception
        collCS = Nothing
    End Try

    If collCS IsNot Nothing Then
        For Each cs As ConnectionStringSettings In collCS
          If cs.Name = "municipio_cloud" then
            Session("cnxStr") = (cs.ConnectionString)
          Else
            Session("cnxStr") = ""
          end if
        Next
    Else
      Session("cnxStr") = ""
    End If

    collCS = Nothing
End Sub


  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    GetAllConnectionStrings()
    If Request.QueryString("a") = "logout" then
      Session.Abandon()
      FormsAuthentication.SignOut()
      errorLabel.Text = "Sessão Encerrada."
      Response.Redirect ("login_usuario.aspx")
    Else
      Session("id_municipio") = "10"
      If Request.QueryString("SIS_") <> "" then
        drp_sel_sistema.SelectedValue = Request.QueryString("SIS_")
      End If

      Session("id_usuario") = "0"
      txtLogin.Focus()
      errorLabel.Text = ""
    end if

  End Sub

  Protected Sub btLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btLogin.Click
    GetAllConnectionStrings()

    Dim cnnSQL As String = Session("cnxStr").ToString
    Dim strSQL As String = "EXEC spLogin_Usuario '" & Session("id_municipio") & "','" & String_SQL(txtLogin.Text) & "','" & String_SQL(txtSenha.Text) & "','0',''"

    errorLabel.Text = ""
    'Try
    Using adapter As New SqlDataAdapter(strSQL, cnnSQL)
      Dim ds As New DataSet
      adapter.Fill(ds, "Login_Result")

      errorLabel.Text = ""
      Dim conta As Integer = 0
      For Each dr As DataRow In ds.Tables("Login_Result").Rows

        If dr("id_usuario").ToString <> "0" then

          Dim authTicket As New FormsAuthenticationTicket(dr("id_usuario").ToString, False, 20)
          Dim encryptTicket = FormsAuthentication.Encrypt(authTicket)
          Dim authCookie As HttpCookie = New HttpCookie(FormsAuthentication.FormsCookieName, encryptTicket)
          Response.Cookies.Add(authCookie)
          Session.Add("id_usuario", dr("id_usuario").ToString)
          Session.Add("id_municipio", dr("id_municipio").ToString)
          Session.Add("permissao", dr("permissao").ToString)
          Session.Add("Logado", dr("logado").ToString)

          FormsAuthentication.RedirectFromLoginPage(dr("id_usuario").ToString, False)
        Else
          errorLabel.Text = "Falha na autenticação"
        End If

      Next

      'For Each dc As DataColumn In ds.Tables("Login_Result").Columns
      'errorLabel.Text = errorLabel.Text & String.Format("{0} ({1}) {2} ", dc.ColumnName, dc.DataType, dc.value)
      'Next dc
      If Session("Logado") = "T" then
        If drp_sel_sistema.SelectedValue.ToString = "0" then
          Response.Redirect("cad_inicio.aspx")
        ElseIf drp_sel_sistema.SelectedValue.ToString = "1" then
          Response.Redirect("Diversos/eventos_participantes_lista.aspx")
        End If
        errorLabel.Text = "OK"
      Else
        errorLabel.Text = "Falha na autenticação"
      end if

    End Using

  End Sub
    Function String_SQL(texto As String) As String
    String_SQL = Replace(texto, "'", "''")
  End Function
End Class
