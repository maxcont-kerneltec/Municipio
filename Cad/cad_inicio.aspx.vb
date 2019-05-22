Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration

Partial Class Cad_inicio
  Inherits System.Web.UI.Page

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    'If (Session("logado")) <> "T" then
    '  Response.Redirect ("../login_usuario.aspx")
    'End If

    If Not IsPostBack Then
      Dim strSQL As String = "EXEC sp1Pega_Dados_Inicio '" & Session("id_municipio") & "','" & Session("id_usuario") & "'"

      'Try
      Using adapter As New SqlDataAdapter(strSQL, Session("cnxStr").ToString)
        Dim ds As New DataSet
        adapter.Fill(ds, "CID")

        For Each dr As DataRow In ds.Tables("CID").Rows
          lbl_nome.Text = dr("nome_usuario")
          lbl_CPF.Text = dr("CPF_usuario")
          lbl_ult_acesso.Text = dr("dt_ultimo")
          lbl_conta_acessos.Text = dr("conta_acessos")
          lbl_cidadao.text = dr("conta_cidadao")
          lbl_telefone.text = "" & dr("telefone")
          lbl_email.text = "" & dr("email")
          lbl_conta_cidadao_fora.text = "" & dr("conta_cidadao_fora")
          lbl_perc_morador_fora.Text = formatnumber(dr("perc_cidadao_fora"), 2)
          lbl_perc_eleitor_fora.Text = formatnumber(dr("perc_eleitor_fora"), 2)
          lbl_conta_eleitores_fora.text = "" & dr("conta_eleitores_fora")
          lbl_localizacao.Text = "" & dr("conta_instalacao")
          lbl_conta_deficientes.Text = "" & dr("conta_deficientes")
          lbl_perc_deficientes.Text = formatnumber(dr("perc_deficientes"), 2)

          lbl_conta_eleitores_municipio.Text = "" & dr("conta_eleitores_municipio")
          lbl_perc_eleitor_municipio.Text = formatnumber(dr("perc_eleitores_municipio"), 2)
        Next
      End Using
    end if
  End Sub

  Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_consulta_cidadaos.Click
    Page.Response.Redirect ("cad_cidadao.aspx")
  End Sub

  Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_consultar_instalacao.Click
    Page.Response.Redirect ("rel_endereco_conta_cidadao.aspx")
  End Sub
End Class
