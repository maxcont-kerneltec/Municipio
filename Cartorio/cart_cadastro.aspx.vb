Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration

Partial Class Cart_cadastro
  Inherits System.Web.UI.Page

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    If Not IsPostBack Then
      ddlUF_comp.SelectedValue = "SP"
    End If

  End Sub

  Protected Sub botCancela_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles botCancela.Click
    Page.Response.Redirect("../login_cartorio.aspx")
  End Sub

  Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click

    Dim strSQL As String = "EXEC dbo.sp3Cartorio_Novo '10'"
    strSQL = strSQL & ",'" & txtCartorio_Nome.Text & "'"
    strSQL = strSQL & ",'" & txtCartorio_CNPJ.Text & "'"
    strSQL = strSQL & ",'" & ddlMunicipio_comp.SelectedValue & "'"
    strSQL = strSQL & ",'" & txtNome_Usuario.Text & "'"
    strSQL = strSQL & ",'" & txtCPF_usuario.Text & "'"
    strSQL = strSQL & ",'" & txtEmail.Text & "'"
    'Try
    Using adapter As New SqlDataAdapter(strSQL, Session("cnxStr").ToString)
      Dim ds As New DataSet
      adapter.Fill(ds, "NFS")

      For Each dr As DataRow In ds.Tables("NFS").Rows
        labERRO.Text = dr("resultado")
      Next
    End Using

  End Sub

End Class
