Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration

Partial Class Cad_cidadao
  Inherits System.Web.UI.Page

  'Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs)
  '  'If Page.Request.ServerVariables("http_user_agent").ToString.ToLower  = "safari" then
  '      Page.ClientTarget = "uplevel"
  '  'end if
  'End Sub 

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    'If (Session("logado")) <> "T" then
    '  Response.Redirect ("../login_usuario.aspx")
    'End If
    Me.txt_CPF_inclui.Attributes.Add("onkeypress", "formatar(this,'###.###.###-##'); return SomenteNumero(event);")
    Me.txt_CPF_pesq.Attributes.Add("onkeypress", "formatar(this,'###.###.###-##'); return SomenteNumero(event);")
    Me.txt_SUS_Num_Pesq.Attributes.Add("onkeypress", "return SomenteNumero(event);")

    If Session("permissao") = "15" then 'Permissão de Consulta
      GridView_Cidadaos.Columns(0).Visible = False
    Else
      GridView_Cidadaos.Columns(1).Visible = False
    End If

    If Not IsPostBack Then
      If Request.QueryString("sel_filtro") <> "" then
        drp_tipo_filtro.SelectedValue = Request.QueryString("sel_filtro")
      Else
        drp_tipo_filtro.SelectedValue = "0"
      End If

      If Request.QueryString("pg_") <> "" then
        GridView_Cidadaos.PageIndex = Request.QueryString("pg_")
      End If
      if drp_tipo_filtro.SelectedValue = "7" then
        GridView_Cidadaos.HeaderRow.cells(5).Text="Erro" 
        GridView_Cidadaos.HeaderRow.cells(6).Text="Aviso"
      End If
    End If
      if Request.QueryString("sel_filtro") = "7" then
        GridView_Cidadaos.HeaderRow.cells(5).Text="Erro" 
        GridView_Cidadaos.HeaderRow.cells(6).Text="Aviso"
      End If
  End Sub

  Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
    If (Session("logado")) <> "T" then
      Response.Redirect ("../login_usuario.aspx")
    End If
  End Sub  

  Protected Sub Acessa_Cadastro(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)
    Session("id_cidadao") = e.CommandArgument.ToString
    Page.Response.Redirect("cad_cidadao_edita.aspx?sel_filtro=" & drp_tipo_filtro.SelectedValue.ToString & "&pg_=" & GridView_Cidadaos.PageIndex)
  End Sub


  Protected Sub Button3_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
    GridView_Cidadaos.DataBind()
  End Sub

  Protected Sub btn_incluir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_incluir.Click
    Dim reg_num_nasc As string
    Dim reg_novo As string
    If drp_tipo_reg_nasc.SelectedValue.ToString = "0" then
      reg_num_nasc = txt_num_reg_inclui.Text.ToString & txt_num_reg_folha_inclui.text.ToString & txt_num_reg_livro_inclui.Text.ToString
      reg_novo = ""
    Else
      reg_num_nasc = right("000000" & txt_cod_nac.Text, 6) & right("00" & txt_cod_acervo.Text, 2) & right("00" & txt_serv.text, 2) & right("0000" & txt_ano_registro.Text, 4) & right("0" & txt_tipo_livro.Text, 1) & right("00000" & txt_num_livro.Text, 5) & right("000" & txt_folha_livro.Text, 3) & right("0000000" & txt_num_termo.Text, 7) & right("00" & txt_dig_verif.Text, 2)
      reg_novo = reg_num_nasc
    End If

    Dim strSQL As String = "EXEC sp1Cidadao_Pega '" & Session("id_municipio") & "','" & txt_SUS_num.Text & "','','" & txt_CPF_inclui.text.ToString & "','" & String_SQL(reg_num_nasc) & "','" & drp_tipo_cfp.SelectedValue & "'"

    'Try
    Using adapter As New SqlDataAdapter(strSQL, Session("cnxStr").ToString)
      Dim ds As New DataSet
      adapter.Fill(ds, "CID")

      For Each dr As DataRow In ds.Tables("CID").Rows
        Session("id_cidadao") = dr("id_cidadao").ToString

        If Session("id_cidadao") <> "0" then 'já existe cadastro
          Page.Response.Redirect("cad_cidadao_edita.aspx?a=SCAD")
        ElseIf Session("id_cidadao") = "0" and dr("id_result").ToString = "0" then 'novo cadastro
          Page.Response.Redirect("cad_cidadao_edita.aspx?CPF=" & txt_CPF_inclui.text.ToString & "&regnum=" & txt_num_reg_inclui.Text.ToString & "&regfolha=" & txt_num_reg_folha_inclui.text.ToString & "&reglivro=" & txt_num_reg_livro_inclui.text.ToString & "&reg_novo=" & reg_novo & "&tpCPF=" & drp_tipo_cfp.SelectedValue & "&SUS_novo=" & txt_SUS_num.Text)
        Else 'falha na geração do cadastro
          lbl_resultado.Visible = true
          lbl_resultado.Text = dr("resultado")
        end if
      Next

    End Using

  End Sub

  Protected Sub drp_tipo_reg_nasc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drp_tipo_reg_nasc.SelectedIndexChanged
    If drp_tipo_reg_nasc.SelectedValue.ToString = "0" then
      pnl_reg_antigo.Visible = True
      pnl_reg_novo.Visible = False
    Else
      pnl_reg_antigo.Visible = false
      pnl_reg_novo.Visible = true
    End If
  End Sub

  Protected Sub btn_novo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_novo.Click
    pnl_novo.Visible = True
  End Sub

  Protected Sub btn_cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancelar.Click
    pnl_novo.Visible = false
  End Sub

  Function String_SQL(texto As String) As String
    String_SQL = Replace(texto, "'", "''")
  End Function
End Class
