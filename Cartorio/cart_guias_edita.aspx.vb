Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration

Partial Class Cart_guias_edita
  Inherits System.Web.UI.Page

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    If (Session("logado")) <> "T" Then
      Response.Redirect("../login_cartorio.aspx")
    End If

    If Session("id_usuario_edita") = "0" Then
      lblTitulo.Text = "Novo Cadastro de Usuário"
    End If

    If Not IsPostBack Then
      Dim strSQL As String = "EXEC sp3Cartorio_Inicio " & Session("id_municipio") & "," & Session("id_cartorio") & "," & Session("id_usuario")

      'Try
      Using adapter As New SqlDataAdapter(strSQL, Session("cnxStr").ToString)
        Dim ds As New DataSet
        adapter.Fill(ds, "CID")

        For Each dr As DataRow In ds.Tables("CID").Rows
          lblCartorio.Text = "" & dr("nome_cart")
          lblCartorio_CNPJ.Text = "" & dr("cnpj_cart")
          lblCartorio_Municipio.Text = "" & dr("municipio_cart") & "/" & dr("uf_cart")
          lblCartorio_Responsavel.Text = dr("nome_usuario") & " - " & dr("CPF_usuario")
        Next
      End Using

      ddlUF_comp.SelectedValue = "SP"
      ddlMunicipio_comp.SelectedValue = "3505708"
    End If

    If Not IsPostBack Then
      Dim strSQL As String = "EXEC sp3Cartorio_Uma_Guia_Pega '" & Session("id_municipio") & "','0'"
      'Try
      Using adapter As New SqlDataAdapter(strSQL, Session("cnxStr").ToString)
        Dim ds As New DataSet
        adapter.Fill(ds, "USUARIO")

        For Each dr As DataRow In ds.Tables("USUARIO").Rows
          'asp_nosso_numero = "24" & Right("0000000" & CStr(mConsulta(0, 0)), 7)
          'asp_iss_apagar = mConsulta(1, 0)
          'asp_dt_vcto = mConsulta(2, 0)
          'asp_dt_cria = mConsulta(3, 0)
          'asp_dt_altera = mConsulta(4, 0)
          'asp_docs_assoc = "" & mConsulta(5, 0)

          'asp_RazaoSocial = "" & mConsulta(6, 0)
          'asp_CNPJ = "" & mConsulta(7, 0)
          'asp_end = "" & mConsulta(8, 0)
          'asp_bairro = "" & mConsulta(9, 0)
          'asp_municipio_cnpj = "" & mConsulta(10, 0)
          'asp_uf = "" & mConsulta(11, 0)
          'asp_cep = "" & mConsulta(12, 0)

          'txtNome_Usuario.Text = dr("nome_usuario")
          'txtCPF_usuario.Text = "" & dr("cpf_usuario")
          'txtSenha.Text = "" & dr("senha")
          'ddlPermissao.SelectedValue = "" & dr("permissao")
          'ddlSts_Bloq.SelectedValue = dr("sts_bloq")
          'lblAcessos.Text = "" & dr("conta") & " com último acesso em " & dr("dt_ultimo")
          'lblDt_Cadastro.Text = "" & dr("dt_cadastro")
          'txtEmail.Text = "" & dr("email")
        Next
      End Using
    End If
  End Sub

  Protected Sub botCancela_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles botCancela.Click
    Page.Response.Redirect("cart_guias.aspx")
  End Sub

  Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click

    'Dim strSQL As String = "EXEC dbo.sp3Cartorio_Guia_Gera '" & Session("id_municipio").ToString & "','" & Session("id_cartorio").ToString & "','" & Session("id_usuario_edita").ToString & "'"
    'strSQL = strSQL & ",'" & txtInscricao_Cadastral.Text & "'"
    ''strSQL = strSQL & ",'" & txtCPF_usuario.Text & "'"
    ''strSQL = strSQL & ",'" & txtSenha.Text & "'"
    ''strSQL = strSQL & ",'" & ddlPermissao.SelectedValue & "'"
    ''strSQL = strSQL & ",'" & ddlSts_Bloq.SelectedValue & "'"
    ''strSQL = strSQL & ",'" & txtEmail.Text & "'"
    ''Try
    'Using adapter As New SqlDataAdapter(strSQL, Session("cnxStr").ToString)
    '  Dim ds As New DataSet
    '  adapter.Fill(ds, "NFS")

    '  For Each dr As DataRow In ds.Tables("NFS").Rows
    '    labERRO.Text = dr("resultado")
    '    Session("id_usuario_edita") = dr("id_usuario")
    '    If Session("id_usuario_edita") <> "0" Then
    '      lblTitulo.Text = "Edição de Cadastro"
    '    End If
    '    Page.Response.Redirect("cart_guia_edita.aspx")
    '  Next
    'End Using

  End Sub

  Protected Sub botConsulta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles botConsulta.Click

    Dim strSQL As String = "EXEC dbo.sp3Cartorio_Consulta_Inscricao '" & txtInscricao_Cadastral.Text & "'"
    'Try
    Using adapter As New SqlDataAdapter(strSQL, Session("cnxStr").ToString)
      Dim ds As New DataSet
      adapter.Fill(ds, "IMOVEL")

      For Each dr As DataRow In ds.Tables("IMOVEL").Rows
        If dr("resultado") = "OK" Then
          labERRO.Text = ""
          lblQuadra.Text = dr("quadra")
          lblLote.Text = dr("lote")
          lblInsc_Anterior.Text = dr("InscCadastralAnt")
          lblVal_Venal.Text = dr("ValTerreno") + dr("ValEdificacao")
          lblEndereco1.Text = dr("tipo_logradouro") & dr("logradouro") & ", " & dr("logradouro_num") & ", " & dr("complemento")
          lblEndereco2.Text = dr("bairro") & " - " & dr("cidade") & " / " & dr("uf")
          lblProprietario.Text = dr("NomeProp")

        Else
          labERRO.Text = dr("resultado")
        End If

      Next
    End Using

  End Sub

End Class
