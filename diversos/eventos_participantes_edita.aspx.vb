Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration
Imports System.IO

Partial Class eventos_participantes_edita
  Inherits System.Web.UI.Page

  Dim tbMunicipio As New DataTable

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    If (Session("logado")) <> "T" then
      Response.Redirect ("../login_usuario.aspx?SIS_=1")
    End If
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
              lbl_CPF.Text = "" & dr("CPF")
              txt_RG.Text = "" & dr("RG")
              lbl_RG.Text= "" & dr("RG")
              txt_dt_nasc_2.Text = "" & dr("dt_nasc")
              lbl_dt_nasc.Text = "" & dr("dt_nasc")
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
              txt_CEP.text = "" & dr("CEP")
              lbl_CEP.Text = "" & dr("CEP")
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
                img_foto_usuario.ImageUrl = "../cad/docs_usuarios/" & dr("pasta_doc") & "/" & dr("doc_foto")
              Else
                img_foto_usuario.Visible = False
              End If

              If "" & dr("grava_evento") = "0" then 'participante ainda não foi gravado, não permite anexar foto
                pnl_foto.Visible = False
              'Else 'participante gravado, anexa foto
                'pnl_foto.Visible = True
              End If
          Next
        End Using  
      End If

      ''Modo impressão
      If ucase(Request.QueryString("mod_")) = "IMP" then
        pnl_botoes.Visible = False
        pnl_foto.Visible = False
        pnl_botao_imprimir.Visible = True
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
        pnl_botoes.Visible = True
        pnl_foto.Visible = True
        pnl_botao_imprimir.Visible = False

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
    If (Session("logado")) <> "T" then
      Response.Redirect ("../login_usuario.aspx?SIS_=1")
    End If

    Dim result As String
    Dim asp_id_evento As String
    asp_id_evento = "1"

    Dim Dt_Nasc As String = ""

    Dt_Nasc = Right(txt_dt_nasc_2.Text, 4) & "-" & mid(txt_dt_nasc_2.Text, 4, 2) & "-" & left(txt_dt_nasc_2.Text, 2)

    Dim strSQL As String = "EXEC dbo.sp1Evento_Participante_Salva '" & Session("id_municipio").ToString & "','" & asp_id_evento & "'"
    strSQL = strSQL & ",'" & lbl_CPF.Text & "','" & txt_RG.Text & "','" & Dt_Nasc & "','" & txt_nome.text & "'"
    strSQL = strSQL & ",'" & txt_xLgr.Text & "','" & txt_nro.Text & "','" & txt_xCpl.text & "','" & txt_xBairro.text & "'"
    strSQL = strSQL & ",'" & txt_CEP.text & "','" & txt_email.Text & "','" & txt_telefone.text & "'"
    strSQL = strSQL & ",'" & txt_assunto.text & "','" & txt_encaminhado.Text & "','" & txt_atendimento.Text & "','" & Session("id_usuario") & "'"

    'Try
    Using adapter As New SqlDataAdapter(strSQL, Session("cnxStr").ToString)
      Dim ds As New DataSet
      adapter.Fill(ds, "NFS")

      For Each dr As DataRow In ds.Tables("NFS").Rows
        result = dr("result")
        Page.Response.Redirect ("eventos_participantes_edita.aspx?id_evento_=" & asp_id_evento & "&cpf_=" & dr("cpf")  )
      Next
    End Using
  End Sub

  Protected Sub btn_grava_foto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_grava_foto.Click

    Dim CurrentFileName As String
    Dim Pasta As String

    CurrentFileName = fld_up_foto.FileName
    If (Path.GetExtension(fld_up_foto.PostedFile.FileName).ToLower = "") or (Path.GetExtension(fld_up_foto.PostedFile.FileName).ToLower <> ".png") Then
      labERRO.Text = "O arquivo deve ser JPG ou PNG." & fld_up_foto.PostedFile.FileName
      labERRO.Visible = True
      'Exit Sub
    End If

    Pasta = Session("id_municipio")

    Dim CurrentPath As String = Server.MapPath("../cad/docs_usuarios/" & Pasta)

    If Not Directory.Exists(CurrentPath) Then
      Directory.CreateDirectory(CurrentPath)
    End If

    Pasta = Pasta & "/" & Year(DateAndTime.Now)
    CurrentPath = CurrentPath & "/" & Year(DateAndTime.Now)

    If Not Directory.Exists(CurrentPath) Then
      Directory.CreateDirectory(CurrentPath)
    End If

    Pasta = Pasta & "/" & right("00" & Month(DateAndTime.Now).ToString, 2)
    CurrentPath = CurrentPath & "/" & right("00" & Month(DateAndTime.Now).ToString, 2)

    If Not Directory.Exists(CurrentPath) Then
      Directory.CreateDirectory(CurrentPath)
    End If


    Dim CPF_limpo As String
    CPF_limpo = Replace(lbl_CPF.Text, ".", "")
    CPF_limpo = Replace(CPF_limpo, "-", "")

    Pasta = Pasta & "/" & right("00000000000" & CPF_limpo, 11)
    CurrentPath = CurrentPath & "/" & right("00000000000" & CPF_limpo, 11)

    If Not Directory.Exists(CurrentPath) Then
        ' Create the directory.
        Directory.CreateDirectory(CurrentPath)
    End If

    Pasta = Pasta & "/fotos"
    CurrentPath = CurrentPath & "/fotos"

    If Not Directory.Exists(CurrentPath) Then
      Directory.CreateDirectory(CurrentPath)
    End If

    If fld_up_foto.HasFile Then
      Dim Caminho_Completo = CurrentPath & "/" & CurrentFileName
      'CurrentPath += "/" & CurrentFileName

      fld_up_foto.SaveAs(Caminho_Completo)
      labERRO.Text = "Arquivo enviado com sucesso."

      Dim strSQL As String = "EXEC dbo.sp1Cad_Usuario_Salva_Foto '" & Session("id_municipio").ToString & "','" & lbl_CPF.Text & "','" & Pasta & "','" & CurrentFileName & "'"

      'Try
      Using adapter As New SqlDataAdapter(strSQL, Session("cnxStr").ToString)
        Dim ds As New DataSet
        adapter.Fill(ds, "NFS")
      End Using
      Page.Response.Redirect ("eventos_participantes_edita.aspx?msg=Foto enviada com sucesso.&mod_=IMP&CPF_=" & lbl_CPF.Text)
    Else
      labERRO.Text = "Falha no envio do arquivo."
    End If
  End Sub

  Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
    Page.Response.Redirect("eventos_participantes_lista.aspx")
  End Sub

  Protected Sub btn_imprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_prepara_imprimir.Click
      Page.Response.Redirect ("eventos_participantes_edita.aspx?msg=Foto enviada com sucesso.&mod_=IMP&CPF_=" & lbl_CPF.Text)
  End Sub
End Class
