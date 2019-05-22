﻿Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration

Partial Class eventos_participantes
  Inherits System.Web.UI.Page

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    'If (Session("logado")) <> "T" then
    '  Response.Redirect ("../login_usuario.aspx")
    'End If
    Session("cnxStr") = "Data Source=189.126.98.131;Initial Catalog=municipio;Persist Security Info=True;User ID=municipio;Password=fooliut5660"

    Session("id_municipio") = "10"
    Me.txt_CPF_inclui.Attributes.Add("onkeypress", "formatar(this,'###.###.###-##'); return SomenteNumero(event);")

  End Sub

  Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
    'If (Session("logado")) <> "T" then
    '  Response.Redirect ("../login_usuario.aspx")
    'End If
  End Sub  

  Protected Sub btn_incluir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_incluir.Click

    Dim strSQL As String = "EXEC sp1Evento_Pega_Participantes '" & Session("id_municipio") & "','1','" & txt_CPF_inclui.text & "','I'"

    Dim asp_CPF as string

    'Try
    Using adapter As New SqlDataAdapter(strSQL, Session("cnxStr").ToString)
      Dim ds As New DataSet
      adapter.Fill(ds, "CID")

      For Each dr As DataRow In ds.Tables("CID").Rows
        asp_CPF = dr("CPF").ToString

        If asp_CPF <> "0" then 'já existe cadastro
          Page.Response.Redirect("eventos_participantes_edita.aspx?CPF_=" & asp_CPF)
        Else 'falha na geração do cadastro
          lbl_resultado.Visible = true
          lbl_resultado.Text = "CPF Inválido!"
        End if
      Next

    End Using

  End Sub

  Protected Sub Acessa_Cadastro(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)
    Page.Response.Redirect("eventos_participantes_edita.aspx?CPF_=" & e.CommandArgument.ToString)
  End Sub
End Class
