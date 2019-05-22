Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration

Partial Class Cart_inicio
  Inherits System.Web.UI.Page

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    If (Session("logado")) <> "T" Then
      Response.Redirect("../login_cartorio.aspx")
    End If


    If Not IsPostBack Then
      Dim strSQL As String = "EXEC sp3Cartorio_Inicio " & Session("id_municipio") & "," & Session("id_cartorio") & "," & Session("id_usuario")

      'Try
      Using adapter As New SqlDataAdapter(strSQL, Session("cnxStr").ToString)
        Dim ds As New DataSet
        adapter.Fill(ds, "CID")

        For Each dr As DataRow In ds.Tables("CID").Rows
          lbl_nome.Text = dr("nome_usuario")
          lbl_CPF.Text = dr("CPF_usuario")
          lbl_ult_acesso.Text = dr("dt_ultimo")
          lbl_dt_cadastro.Text = dr("dt_cadastro")
          lbl_conta_acessos.Text = dr("conta_acessos")
          lbl_email.Text = "" & dr("email")

          lbl_cartorio.Text = "" & dr("nome_cart")
          lbl_cnpj.Text = "" & dr("cnpj_cart")
          lbl_local.Text = "" & dr("municipio_cart") & "/" & dr("uf_cart")

          lbl_num_guias.Text = "" & dr("num_guias")
          lbl_num_usuarios.Text = "" & dr("num_usuarios")

        Next
      End Using
    End If
  End Sub

End Class
