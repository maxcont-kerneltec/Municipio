Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration

Partial Class Cart_usuario_edita
  Inherits System.Web.UI.Page

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    If (Session("logado")) <> "T" Then
      Response.Redirect("../login_cartorio.aspx")
    End If

    If Session("id_usuario_edita") = "0" Then
      lblTitulo.Text = "Novo Cadastro de Usuário"
    End If

    If Not IsPostBack Then
      Dim strSQL As String = "EXEC sp3Cartorio_Usuario_Pega_Um '" & Session("id_municipio") & "','" & Session("id_cartorio") & "','" & Session("id_usuario_edita") & "'"
      'Try
      Using adapter As New SqlDataAdapter(strSQL, Session("cnxStr").ToString)
        Dim ds As New DataSet
        adapter.Fill(ds, "USUARIO")

        For Each dr As DataRow In ds.Tables("USUARIO").Rows
          If dr("id_usuario") = 0 Then
            lblID_Usuario.Text = "NOVO"
          Else
            lblID_Usuario.Text = dr("id_usuario")
          End If
          txtNome_Usuario.Text = dr("nome_usuario")
          txtCPF_usuario.text = "" & dr("cpf_usuario")
          txtSenha.Text = "" & dr("senha")
          ddlPermissao.SelectedValue = "" & dr("permissao")
          ddlSts_Bloq.SelectedValue = dr("sts_bloq")
          lblAcessos.Text = "" & dr("conta") & " com último acesso em " & dr("dt_ultimo")
          lblDt_Cadastro.Text = "" & dr("dt_cadastro")
          txtEmail.Text = "" & dr("email")
        Next
      End Using
    End If
  End Sub

  Protected Sub botCancela_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles botCancela.Click
    Page.Response.Redirect("cart_usuario.aspx")
  End Sub

  Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click

    Dim strSQL As String = "EXEC dbo.sp3Cartorio_Usuario_Salva '" & Session("id_municipio").ToString & "','" & Session("id_cartorio").ToString & "','" & Session("id_usuario_edita").ToString & "'"
    strSQL = strSQL & ",'" & txtNome_Usuario.Text & "'"
    strSQL = strSQL & ",'" & txtCPF_usuario.Text & "'"
    strSQL = strSQL & ",'" & txtSenha.Text & "'"
    strSQL = strSQL & ",'" & ddlPermissao.SelectedValue & "'"
    strSQL = strSQL & ",'" & ddlSts_Bloq.SelectedValue & "'"
    strSQL = strSQL & ",'" & txtEmail.Text & "'"
    'Try
    Using adapter As New SqlDataAdapter(strSQL, Session("cnxStr").ToString)
      Dim ds As New DataSet
      adapter.Fill(ds, "NFS")

      For Each dr As DataRow In ds.Tables("NFS").Rows
        labERRO.Text = dr("resultado")
        Session("id_usuario_edita") = dr("id_usuario")
        If Session("id_usuario_edita") <> "0" Then
          lblTitulo.Text = "Edição de Cadastro"
        End If
        Page.Response.Redirect("cart_usuario_edita.aspx")
      Next
    End Using

  End Sub

End Class
