Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration
Imports System.IO

Partial Class eventos_participantes_edita
  Inherits System.Web.UI.Page

  Dim tbMunicipio As New DataTable

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Session("cnxStr") = "Data Source=189.126.98.131;Initial Catalog=municipio;Persist Security Info=True;User ID=municipio;Password=fooliut5660"

    Session("id_municipio") = "10"
    'If (Session("logado")) <> "T" then
    '  Response.Redirect ("../login_usuario.aspx")
    'End If
    If Not IsPostBack Then
      Dim asp_cpf As String
      Me.txt_dt_nasc_2.Attributes.Add("onkeydown", "return Formata_Data(this.id,event);")

      If Request.QueryString("CPF_") <> "" then
        asp_cpf = Request.QueryString("CPF_")

        Dim strSQL As String = "EXEC sp1Evento_Pega_Um_Participantes '" & Session("id_municipio") & "','1','" & asp_cpf & "'"

        'Try
        Using adapter As New SqlDataAdapter(strSQL, Session("cnxStr").ToString)
          Dim ds As New DataSet
          adapter.Fill(ds, "TERMO")

          For Each dr As DataRow In ds.Tables("TERMO").Rows
              'lbl_dt_gera.Text = dr("dt_gera")
              lbl_CPF.Text = "" & dr("CPF")
              txt_RG.Text = "" & dr("RG")
              txt_dt_nasc_2.Text = "" & dr("dt_nasc")
              txt_nome.Text = "" & dr("nome")
              lbl_nome.Text = "" & dr("nome")
              txt_xLgr.Text = "" & dr("xLgr")
              lbl_xLgr.Text = "" & dr("xLgr")
              txt_nro.Text = "" & dr("nro")
              lbl_nro.Text = "" & dr("nro")
              txt_xCpl.Text = "" & dr("xCpl")
              lbl_xCpl.Text = "" & dr("xCpl")
              txt_xBairro.text = "" & dr("xBairro")
              lbl_xBairro.text = "" & dr("xBairro")
              'drp_UF.text = "" & dr("UF")
              'lbl_UF.Text = "" & dr("UF")
              'lbl_xMunicipio.Text = "" & dr("xMun")
              txt_telefone.text = "" & dr("telefone")
              lbl_telefone.text = "" & dr("telefone")
              txt_email.text = "" & dr("email")
              lbl_email.text = "" & dr("email")

              txt_assunto.Text = "" & dr("descr_assunto")
              txt_encaminhado.Text = "" & dr("encaminhado")
              txt_atendimento.Text = "" & dr("atendimento")

              lbl_descr_assunto.Text = "" & dr("descr_assunto")
              lbl_encaminhado.Text =  "" & dr("encaminhado")
              lbl_atendimento.Text = "" & dr("atendimento")
              If "" & dr("doc_foto") <> "" then
                img_foto_usuario.Visible = True
                img_foto_usuario.ImageUrl = "docs/" & Session("id_municipio") & "/00_cad_usuarios/" & right("00000000000" & "" & dr("CPF_limpo").ToString, 11) & "/fotos/" & dr("doc_foto")
              Else
                img_foto_usuario.Visible = False
              End If
          Next

        End Using  
      End If

      ''Modo impressão
      If ucase(Request.QueryString("mod_")) = "IMP" then
      '  'pnl_consulta_CPF.Visible = False
        lbl_nome.Visible = True
        lbl_xLgr.Visible = True
        lbl_nro.Visible = True
        lbl_xCpl.Visible = True
        lbl_xBairro.Visible = True
        lbl_CEP.Visible = True
      '  lbl_UF.visible = True
      '  lbl_xMunicipio.Visible = True
        lbl_telefone.Visible = True
        lbl_email.Visible = True

        txt_nome.Visible = False
        txt_RG.Visible = False
        txt_dt_nasc_2.Visible = False

        txt_xLgr.Visible = False
        txt_nro.Visible = False
        txt_xCpl.Visible = False
        txt_xBairro.Visible = False
        txt_CEP.Visible = False
        txt_telefone.Visible = False
        txt_email.Visible = False

        txt_assunto.Visible = False
        txt_atendimento.Visible = False
        txt_encaminhado.Visible = False

      Else
        lbl_nome.Visible = False
        lbl_RG.visible = False
        lbl_dt_nasc.Visible = False
        lbl_xLgr.Visible = False
        lbl_nro.Visible = False
        lbl_xCpl.Visible = False
        lbl_xBairro.Visible = False
        lbl_CEP.Visible = False
      '  lbl_UF.Visible = False
        lbl_telefone.Visible = False
        lbl_email.Visible = False
        lbl_descr_assunto.Visible = False
        lbl_encaminhado.Visible = False
        lbl_atendimento.Visible = False

        lbl_descr_assunto.Visible = False
        lbl_atendimento.Visible = False
        lbl_encaminhado.Visible = False

      End If
    End If
  End Sub

  Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click
    'If (Session("logado")) <> "T" then
    '  Response.Redirect ("../login_usuario.aspx")
    'End If

    Dim result As String
    Dim asp_id_evento As String
    asp_id_evento = "1"

    Dim Dt_Nasc As String

    Dt_Nasc = Right(txt_dt_nasc_2.Text, 4) & "-" & mid(txt_dt_nasc_2.Text, 4, 2) & "-" & left(txt_dt_nasc_2.Text, 2)

    Dim strSQL As String = "EXEC dbo.sp1Evento_Participante_Salva '" & Session("id_municipio").ToString & "','" & asp_id_evento & "'"
    strSQL = strSQL & ",'" & lbl_CPF.Text & "','" & txt_RG.Text & "','" & Dt_Nasc & "','" & txt_nome.text & "'"
    strSQL = strSQL & ",'" & txt_xLgr.Text & "','" & txt_nro.Text & "','" & txt_xCpl.text & "','" & txt_xBairro.text & "'"
    strSQL = strSQL & ",'" & txt_CEP.text & "','" & txt_email.Text & "','" & txt_telefone.text & "'"
    strSQL = strSQL & ",'" & txt_assunto.text & "','" & txt_encaminhado.Text & "','" & txt_atendimento.Text & "'"

    'Try
    Using adapter As New SqlDataAdapter(strSQL, Session("cnxStr").ToString)
      Dim ds As New DataSet
      adapter.Fill(ds, "NFS")

      For Each dr As DataRow In ds.Tables("NFS").Rows
        result = dr("result")
        Page.Response.Redirect ("eventos_participantes_edita.aspx?id_evento_=" & asp_id_evento & "&mod_=IMP&cpf_=" & dr("cpf") )
      Next
    End Using
  End Sub

  Protected Sub btn_grava_foto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_grava_foto.Click

    Dim CurrentFileName As String
    CurrentFileName = fld_up_foto.FileName

    If (Path.GetExtension(CurrentFileName).ToLower <> ".jpg" and (Path.GetExtension(CurrentFileName).ToLower <> ".png")) Then
        labERRO.Text = "O arquivo deve ser JPG ou PNG."
        Exit Sub
    End If

    'If fld_upld_arquivo.PostedFile.ContentLength > 131072 Then
    '    Response.Write("The size of big too file , the size of the file must 128 KB not exceed!!! ")
    '    Exit Sub
    'End If

    Dim CurrentPath As String = Server.MapPath("docs/" & Session("id_municipio"))

    If Not Directory.Exists(CurrentPath) Then
        ' Create the directory.
        Directory.CreateDirectory(CurrentPath)
    End If

    CurrentPath = CurrentPath & "/00_cad_usuarios"

    If Not Directory.Exists(CurrentPath) Then
        ' Create the directory.
        Directory.CreateDirectory(CurrentPath)
    End If

    Dim CPF_limpo As String
    CPF_limpo = Replace(lbl_CPF.Text, ".", "")
    CPF_limpo = Replace(CPF_limpo, "-", "")

    CurrentPath = CurrentPath & "/" & right("00000000000" & CPF_limpo, 11)

    If Not Directory.Exists(CurrentPath) Then
        ' Create the directory.
        Directory.CreateDirectory(CurrentPath)
    End If

    CurrentPath = CurrentPath & "/fotos"

    If Not Directory.Exists(CurrentPath) Then
        ' Create the directory.
        Directory.CreateDirectory(CurrentPath)
    End If

    If fld_up_foto.HasFile Then
      CurrentPath += "/" & CurrentFileName
      fld_up_foto.SaveAs(CurrentPath)
      labERRO.Text = "Arquivo enviado com sucesso."

      Dim strSQL As String = "EXEC dbo.sp1Cad_Usuario_Salva_Foto '" & Session("id_municipio").ToString & "','" & lbl_CPF.Text & "','" & CurrentFileName & "'"

      'Try
      Using adapter As New SqlDataAdapter(strSQL, Session("cnxStr").ToString)
        Dim ds As New DataSet
        adapter.Fill(ds, "NFS")


      End Using
      Page.Response.Redirect ("eventos_participantes_edita.aspx?msg=Foto enviada com sucesso.&CPF_=" & lbl_CPF.Text)
    Else
      labERRO.Text = "Falha no envio do arquivo."
    End If
  End Sub
End Class
