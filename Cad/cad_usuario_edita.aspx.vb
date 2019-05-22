Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration

Partial Class Cad_usuario_edita
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
      'usuário não é Master, elimina outras pemissões além das do usuário  
      If Session("permissao") <> 10 then
        Dim Conta_Item As Integer = ddlPermissao.Items.Count - 1
        For x = Conta_Item to 0 Step - 1
          If ddlPermissao.Items(x).Value.ToString <> Session("permissao")
            ddlPermissao.Items.RemoveAt(x)
          End If
        Next

      End If

      Me.txtCPF_usuario.Attributes.Add("onkeypress", "formatar(this,'###.###.###-##'); return SomenteNumero(event);")
      If Session("id_usuario_edita") = "0" Then
          lblTitulo.Text = "Novo Cadastro de Usuário"
      End If

      If Not IsPostBack Then
          If Request.QueryString("msg_") <> "" then
            labERRO.Text = Request.QueryString("msg_")
            labERRO.Visible = True
          End If
          Dim strSQL As String = "EXEC sp1Usuario_Pega_Um '" & Session("id_municipio") & "','" & Session("id_usuario_edita") & "'"
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
                  txtLogin.Text = "" & dr("login")
                  txtSenha.Text = ""
                  ddlPermissao.SelectedValue = "" & dr("permissao")
                  ddlAcesso.SelectedValue = "" & dr("acesso")
                  ddlSts_Bloq.SelectedValue = dr("sts_bloq")
                  If Session("permissao") <> 10 then
                    Dim Conta_Item_Sts As Integer = ddlSts_Bloq.Items.Count - 1
                    For x = Conta_Item_Sts to 0 Step - 1
                      If ddlSts_Bloq.Items(x).Value.ToString <> dr("sts_bloq")
                        ddlSts_Bloq.Items.RemoveAt(x)
                      End If
                    Next

                  End If

                  lblAcessos.Text = "" & dr("conta") & " com último acesso em " & dr("dt_ultimo")
                  lblDt_Cadastro.Text = "" & dr("dt_cadastro")
                  lblDt_altera.Text = "" & dr("dt_altera")
                  txtEmail.Text = "" & dr("email")
                  txtTelefone.Text = "" & dr("telefone")
                  lblNome_Usuario_altera.Text = "" & dr("nome_usuario_altera")
              Next
          End Using
      End If
    End Sub

    Protected Sub botCancela_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles botCancela.Click
        Page.Response.Redirect("cad_usuario.aspx")
    End Sub

    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click

        Dim strSQL As String = "EXEC dbo.sp1Usuario_Salva '" & Session("id_municipio").ToString & "','" & Session("id_usuario_edita").ToString & "'"
        strSQL = strSQL & ",'" & txtNome_Usuario.Text & "'"
        strSQL = strSQL & ",'" & txtCPF_usuario.Text & "'"
        strSQL = strSQL & ",'" & txtLogin.Text & "'"
        strSQL = strSQL & ",'" & txtSenha.Text & "'"
        strSQL = strSQL & ",'" & ddlPermissao.SelectedValue & "'"
        strSQL = strSQL & ",'" & ddlAcesso.SelectedValue & "'"
        strSQL = strSQL & ",'" & ddlSts_Bloq.SelectedValue & "'"
        strSQL = strSQL & ",'" & txtEmail.Text & "'"
        strSQL = strSQL & ",'" & txtTelefone.Text & "'"
        strSQL = strSQL & ",'" & Session("id_usuario") & "'"

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
                Page.Response.Redirect("cad_usuario_edita.aspx?msg_=" & dr("resultado"))
            Next
        End Using

    End Sub

    Protected Sub txtTelefone_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTelefone.TextChanged
    End Sub
End Class
