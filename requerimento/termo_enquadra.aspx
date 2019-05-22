<%@ Page Language="VB" AutoEventWireup="false" CodeFile="termo_enquadra.aspx.vb" Inherits="termo_enquadra" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Prefeitura de Barueri</title>
  <link href="../StyleSheet.css" rel="stylesheet" type="text/css" />
  <script src="../inc/jscript.js" type="text/javascript"></script>
<style type="text/css">
    .style_texto_r
    {
      font-family: Arial, Helvetica, sans-serif;
      text-align: right;
      font-size: 11px;
    }
    
  </style>
  <style type="text/css">
  @media print {
    .info {display: none}
  }
  @media screen {
    .info  {
    background-color: #000080;
    color: #FFFFFF;
    font-weight: 600;
    font-family: Arial, Helvetica, sans-serif;
    }
  }
    .style2
    {
      width: 514px;
      text-align: right;
    }
    .style5
    {
      text-align: left;
      width: 408px;
    }
    .style6
    {
      text-align: left;
      height: 50px;
    }
    .style7
    {
      text-align: center;
    }
    .span_titulo
    {
       font-family: Arial, Helvetica, sans-serif; 
       font-size: 11px; color: #000000; 
       width: 150px; 
       display: inline-block; 
       text-align: right;
    }
    </style>

<script language="javascript" type="text/javascript">
// <!CDATA[

  function Button1_onclick() {
    window.print();
  }

// ]]>
</script>

<script type ="text/javascript">
  function visualiza_docto(caminho_docto) {
    window.open('termos_docs/' + caminho_docto, '', 'toolbar=yes, resizable=yes, scrollbars=yes, width=670px, height=500px')
    return false;
  }

  function Mostra_Aviso() {
    document.getElementById('dv_aviso_documento').style.display = 'block';
  }
</script>
</head>
<body>
  <form id="form1" runat="server">
    <div style="position: absolute">
      <asp:Image ID="Image1" runat="server" ImageAlign="Left" ImageUrl="../img/logo_barueri.jpg" EnableViewState="False" />
    </div>
    <br />

    <div id="Div3" style="position: absolute; top: 160px">
    <asp:ScriptManager ID="srpt_mngr" runat="server">
    </asp:ScriptManager>
      <asp:UpdatePanel ID="updt_pnl_requer" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
        <ContentTemplate>
          <asp:Panel ID="pnl_consulta_CPF" runat="server">
            <table id="tb_pesquisa" 
              style="border: 1px solid #000000; width:100%; border-collapse: collapse;" 
              cellpadding="0" cellspacing="1" border="0">
              <tr>
                <td align="center" 
                  style="font-family: Arial, Helvetica, sans-serif; font-size: 11px; background-color: #000066; color: #FFFFFF; font-weight: bold;" 
                  height="22" colspan="3">Requerimento para obtenção do &quot;Termo de Enquadramento de 
                  Atividade&quot; visando a expedição do Alvará de Liberação Fiscal</td>
              </tr>
            
              <tr>
                <td height="25" bgcolor="#ffffff" 
                  style="font-size: 12px; font-family: Arial, Helvetica, sans-serif" 
                  class="style7" colspan="3">
                  <span lang="pt-br">Para gerar um novo termo de Enquadramento informe o CPF e o 
                  CNPJ (opcional) e clique em Continuar.</span></td>
              </tr>
              <tr>
                <td bgcolor="#E8E8E8" class="style2" height="25" 
                  style="font-size: 12px; font-family: Arial, Helvetica, sans-serif">
                  Informe o CPF do requerente e clique em Continuar&nbsp;-&nbsp; CPF:</td>
                <td bgcolor="#E8E8E8" class="style5" height="25" 
                  style="font-size: 11px; font-family: Arial, Helvetica, sans-serif">
                  &nbsp;&nbsp;<asp:TextBox ID="txt_CPF_requer" runat="server" CssClass="input_texto" 
                    MaxLength="14" Width="172px"></asp:TextBox>
                  &nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txt_CPF_requer" ErrorMessage="Informe o CPF!"></asp:RequiredFieldValidator>
                </td>
                <td bgcolor="#E8E8E8" class="style6" height="25" rowspan="4" 
                  style="font-size: 11px; font-family: Arial, Helvetica, sans-serif">
                  <asp:Button ID="btn_verifica" runat="server" CssClass="button_tb" 
                    Text="Continuar" UseSubmitBehavior="False" />
                </td>
              </tr>
              <tr>
                <td bgcolor="#E8E8E8" class="style2" height="25" 
                  style="font-size: 12px; font-family: Arial, Helvetica, sans-serif">
                  <span lang="pt-br">Informe o CNPJ (Opcional):</span></td>
                <td align="center" bgcolor="#E8E8E8" class="style5" height="25" 
                  style="font-size: 11px; font-family: Arial, Helvetica, sans-serif">&nbsp;
                  <asp:TextBox ID="txt_cnpj_pesq" runat="server" CssClass="input_texto" 
                    Width="172px"></asp:TextBox>
                </td>
              </tr>
              <tr>
                <td bgcolor="#E8E8E8" class="style2" height="25" 
                  style="font-size: 12px; font-family: Arial, Helvetica, sans-serif">
                  <span lang="pt-br">Termo de Enquadramento: </span>
                </td>
                <td align="center" bgcolor="#E8E8E8" class="style5" height="25" 
                  style="font-size: 11px; font-family: Arial, Helvetica, sans-serif">&nbsp;
                  <asp:TextBox ID="txt_termo_pesq" runat="server" CssClass="input_texto"></asp:TextBox>
                </td>
              </tr>
              <tr>
                <td bgcolor="#ffffff" class="style7" colspan="3" height="25" 
                  style="font-size: 12px; font-family: Arial, Helvetica, sans-serif">
                  <span lang="pt-br">Para pesquisar um Termo de Enquadramento informe o CPF e o 
                  número do Termo e clique em Continuar.<br />
                  </span>
                  <asp:UpdateProgress ID="updt_prg_pnl_requer" runat="server" 
                    AssociatedUpdatePanelID="updt_pnl_requer" DisplayAfter="10">
                    <ProgressTemplate>
                      <br />
                      <asp:Label ID="lbl_aviso" runat="server" Font-Size="Large" ForeColor="Red" 
                        Text="Aguarde..."></asp:Label>
                    </ProgressTemplate>
                  </asp:UpdateProgress>
                </td>
              </tr>
            </table>
          </asp:Panel>
          <asp:Panel ID="pnl_botao_imprimir" runat="server" Visible="False">
            <input id="Button1" type="button" value="Imprimir" class="info" onclick="return Button1_onclick()" />
            <br />
            <div align="center" style="font-family: Arial, Helvetica, sans-serif; font-size: 12px">
              Termo de Enquadramento de Atividade: <asp:Label ID="lbl_id_termo_enquadra" runat="server" Text="Label"></asp:Label>
              <br />
              gerado em: <asp:Label ID="lbl_dt_gera" runat="server" Text="Label"></asp:Label>
            </div>
          </asp:Panel>

          <div id="Div1" align="center">
            <asp:Label ID="lbl_erro" runat="server" Font-Bold="True" ForeColor="#CC3300" 
              Text="lbl_erro" Visible="False" Font-Size="X-Small" BackColor="#FFFFCC" 
              Width="60%"></asp:Label>
            <br />
            <asp:Panel ID="pnl_requerente" runat="server" Visible="False">
              <table id="tbl_dados_requerente" style="border: 1px solid #000000; width:100%; border-collapse: collapse; background-color: #E8E8E8;" 
                cellpadding="2" cellspacing="0" border="1">
                <tr>
                  <td colspan="4" align="center" style="font-family: Arial, Helvetica, sans-serif; font-size: small; background-color: #808080; color: #FFFFFF;"height="22">
                    Dados do Requerente
                  </td>
                </tr>
                <tr>
                  <td align="right" width="150" class="style_texto_r">Nome:</td>
                  <td>
                    <asp:TextBox ID="txt_nome" runat="server" MaxLength="120" Width="352px" CssClass="input_texto"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_nome" ErrorMessage="* Informe o nome" 
                      ValidationGroup="grp_requerente" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:Label ID="lbl_nome" runat="server" CssClass="lbl_texto" Text="Label" 
                      Visible="False"></asp:Label>
                  </td>
                  <td class="style_texto_r"><span class="style_texto_r" lang="pt-br">CPF:</span></td>
                  <td>
                    <asp:Label ID="lbl_CPF_requer" runat="server" CssClass="lbl_texto" Text="Label"></asp:Label>
                  </td>
                </tr>
                <tr>
                  <td class="style_texto_r" width="150">Logradouro, nro:</td>
                  <td>
                    <asp:TextBox ID="txt_xLgr" runat="server" MaxLength="60" Width="295px" 
                      CssClass="input_texto"></asp:TextBox>
                    <asp:Label ID="lbl_xLgr" runat="server" CssClass="lbl_texto" Text="Label" 
                      Visible="False"></asp:Label>
                    <span lang="pt-br">&nbsp;,&nbsp; </span>
                    <asp:TextBox ID="txt_nro" runat="server" MaxLength="10" Width="90px" 
                      CssClass="input_texto"></asp:TextBox>
                    <span lang="pt-br">&nbsp;</span><asp:Label ID="lbl_nro" runat="server" 
                      CssClass="lbl_texto" Text="Label"></asp:Label>
                  </td>
                  <td class="style_texto_r">
                    <span lang="pt-br">Complemento:</span></td>
                  <td>
                    <span lang="pt-br">
                    <asp:TextBox ID="txt_xCpl" runat="server" Width="246px" CssClass="input_texto"></asp:TextBox>
                    </span>
                    <asp:Label ID="lbl_xCpl" runat="server" CssClass="lbl_texto" Text="Label"></asp:Label>
                  </td>
                </tr>
                <tr>
                  <td align="right" class="style_texto_r" width="150">Bairro:</td>
                  <td>
                    <asp:TextBox ID="txt_xBairro" runat="server" MaxLength="60" Width="244px" CssClass="input_texto"></asp:TextBox>
                    <span lang="pt-br" class="style_texto_r">&nbsp;<asp:Label ID="lbl_xBairro" 
                      runat="server" Text="Label"></asp:Label>
                    &nbsp;CEP:&nbsp;<asp:TextBox ID="txt_CEP" 
                      runat="server" MaxLength="9" Width="75px" CssClass="input_texto"></asp:TextBox>
                    &nbsp;<asp:Label ID="lbl_CEP" runat="server" Text="Label" Visible="False"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>

                    <asp:SqlDataSource ID="sql_dt_UF" runat="server" ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
                      SelectCommand="SELECT UF FROM tbUF WHERE (UF &lt;&gt; @UF) AND (UF &lt;&gt; 'EX') ORDER BY UF">
                      <SelectParameters>
                        <asp:Parameter DefaultValue="--" Name="UF" Type="String" />
                      </SelectParameters>
                    </asp:SqlDataSource>
                  </td>
                  <td class="style_texto_r">UF / Municício:</td>
                  <td>
                    <asp:DropDownList ID="drp_UF" runat="server" AutoPostBack="True" 
                      DataSourceID="sql_dt_UF" DataTextField="UF" DataValueField="UF">
                    </asp:DropDownList>
                    <asp:Label ID="lbl_UF" runat="server" CssClass="lbl_texto" Text="Label"></asp:Label>
                    /
                    <asp:DropDownList ID="drp_cMun" runat="server">
                    </asp:DropDownList>
                    <asp:Label ID="lbl_xMunicipio" runat="server" CssClass="lbl_texto" Text="Label"></asp:Label>
                  </td>
                </tr>
                <tr>
                  <td class="style_texto_r" width="150">Telefone:</td>
                  <td>
                    <asp:TextBox ID="txt_telefone" runat="server" MaxLength="50" Width="237px" CssClass="input_texto"></asp:TextBox>
                    <asp:Label ID="lbl_telefone" runat="server" CssClass="lbl_texto" Text="Label" 
                      Visible="False"></asp:Label>
                  </td>
                  <td class="style_texto_r">E-mail:</td>
                  <td>
                    <asp:TextBox ID="txt_email" runat="server" Width="242px" CssClass="input_texto"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rqrd_email_requer" runat="server" 
                      ControlToValidate="txt_email" ErrorMessage="* Informe o e-mail" 
                      ValidationGroup="grp_requerente" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:Label ID="lbl_email" runat="server" CssClass="lbl_texto" Text="Label" 
                      Visible="False"></asp:Label>
                  </td>
                </tr>
              </table>

              <br /><br />

              <table id="tbl_dados_estabelecimento" style="border: 1px solid #000000; width:100%; border-collapse: collapse; background-color: #E8E8E8;" 
                cellpadding="2" cellspacing="0" border="1">
                <tr>
                  <td colspan="2" align="center" style="font-family: Arial, Helvetica, sans-serif; font-size: small; background-color: #808080; color: #FFFFFF;" 
                    height="22">Dados do Estabelecimento </td>
                </tr>
                <tr>
                  <td class="style_texto_r">CNPJ:</td>
                  <td>
                    <asp:TextBox ID="txt_cnpj_estabelecimento" runat="server" 
                      CssClass="input_texto" Width="212px" MaxLength="18"></asp:TextBox>
                    <asp:Label ID="lbl_cnpj_estabelecimento" runat="server" CssClass="lbl_texto" 
                      Text="Label"></asp:Label>
                  </td>
                </tr>
                <tr>
                  <td class="style_texto_r">Inscrição Municipal:</td>
                  <td>
                    <asp:TextBox ID="txt_IM_estabelecimento" runat="server" CssClass="input_texto" 
                      Width="211px"></asp:TextBox>
                    <asp:Label ID="lbl_IM_estabelecimento" runat="server" CssClass="lbl_texto" 
                      Text="Label"></asp:Label>
                  </td>
                </tr>
                <tr>
                  <td class="style_texto_r">Inscrição Estadual:</td>
                  <td>
                    <asp:TextBox ID="txt_IE_estabelecimento" runat="server" CssClass="input_texto" 
                      Width="213px"></asp:TextBox>
                    <asp:Label ID="lbl_IE_estabelecimento" runat="server" CssClass="lbl_texto" 
                      Text="Label"></asp:Label>
                  </td>
                </tr>
                <tr>
                  <td class="style_texto_r">Nome do Estabelecimento:</td>
                  <td>
                    <asp:TextBox ID="txt_razao_estabelecimento" runat="server" 
                      CssClass="input_texto" MaxLength="120" Width="352px"></asp:TextBox>
                    <asp:Label ID="lbl_razao_estabelecimento" runat="server" CssClass="lbl_texto" 
                      Text="Label"></asp:Label>
                    <asp:RequiredFieldValidator ID="rqrd_razao_estabelecimento" runat="server" 
                      ControlToValidate="txt_razao_estabelecimento" 
                      ErrorMessage="Informe o nome do estabelecimento" Font-Size="XX-Small" 
                      SetFocusOnError="True"></asp:RequiredFieldValidator>
                  </td>
                </tr>

                <tr>
                  <td class="style_texto_r">Inscr. Cadastral IPTU:</td>
                  <td>
                    <asp:TextBox ID="txt_num_inscr_IPTU" runat="server" CssClass="input_texto" 
                      Width="361px"></asp:TextBox>
                    <span lang="pt-br" class="style_texto_r">&nbsp;&nbsp;<asp:Label ID="lbl_num_inscr_IPTU" 
                      runat="server" Text="Label"></asp:Label>
                    &nbsp;<asp:RequiredFieldValidator ID="rqrd_num_inscr_IPTU" runat="server" 
                      ControlToValidate="txt_num_inscr_IPTU" 
                      ErrorMessage="Informe o número de inscrição cadastral" Font-Size="XX-Small" 
                      SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                    &nbsp;Lote: </span>
                    <asp:TextBox ID="txt_IPTU_lote" runat="server" CssClass="input_texto"></asp:TextBox>
                    <span lang="pt-br" class="style_texto_r">&nbsp;&nbsp;<asp:Label ID="lbl_IPTU_lote" 
                      runat="server" CssClass="lbl_texto" Text="Label"></asp:Label>
                    &nbsp; Quadra: </span>
                    <asp:TextBox ID="txt_IPTU_quadra" runat="server" CssClass="input_texto"></asp:TextBox>
                    <asp:Label ID="lbl_IPTU_quadra" runat="server" CssClass="lbl_texto" 
                      Text="Label"></asp:Label>
                  </td>
                </tr>
                <tr>
                  <td class="style_texto_r">Logradouro, nro:</td>
                  <td>
                    <asp:TextBox ID="txt_lgr_estabelecimento" runat="server" Width="291px" 
                      CssClass="input_texto"></asp:TextBox>
                    <asp:Label ID="lbl_lgr_estabelecimento" runat="server" CssClass="lbl_texto" 
                      Text="Label"></asp:Label>
                    <span lang="pt-br">, </span>
                    <asp:TextBox ID="txt_nro_estabelecimento" runat="server" CssClass="input_texto" 
                      Width="74px"></asp:TextBox>
                    <asp:Label ID="lbl_nro_estabelecimento" runat="server" CssClass="lbl_texto" 
                      Text="Label"></asp:Label>
                  </td>
                </tr>

                <tr>
                  <td class="style_texto_r">
                    <span lang="pt-br">Complemento:</span></td>
                  <td>
                    <asp:TextBox ID="txt_xCpl_estabelecimento" runat="server" 
                      CssClass="input_texto" Width="252px"></asp:TextBox>
                    <asp:Label ID="lbl_xCpl_estabelecimento" runat="server" CssClass="lbl_texto" 
                      Text="Label"></asp:Label>
                    <span lang="pt-br" class="style_texto_r">&nbsp;&nbsp; Bairro: </span>
                    <asp:TextBox ID="txt_xBairro_estabelecimento" runat="server" 
                      CssClass="input_texto" Width="212px"></asp:TextBox>
                    <span lang="pt-br" class="style_texto_r">&nbsp;<asp:Label 
                      ID="lbl_xBairro_estabelecimento" runat="server" CssClass="lbl_texto" 
                      Text="Label"></asp:Label>
                    CEP: </span>
                    <asp:TextBox ID="txt_CEP_estabelecimento" runat="server" MaxLength="9"></asp:TextBox>
                    <asp:Label ID="lbl_CEP_estabelecimento" runat="server" CssClass="lbl_texto" 
                      Text="Label"></asp:Label>
                  </td>
                </tr>
                <tr>
                  <td class="style_texto_r">
                    <span lang="pt-br">Telefone:</span></td>
                  <td>
                    <asp:TextBox ID="txt_telefone_estabelecimento" runat="server" 
                      CssClass="input_texto" Height="18px" MaxLength="50" Width="325px"></asp:TextBox>
                    <asp:Label ID="lbl_telefone_estabelecimento" runat="server" 
                      CssClass="lbl_texto" Text="Label"></asp:Label>
                  </td>
                </tr>
                <tr>
                  <td class="style_texto_r">
                    <span lang="pt-br">E-mail:</span></td>
                  <td>
                    <asp:TextBox ID="txt_email_estabelecimento" runat="server" 
                      CssClass="input_texto" MaxLength="100" Width="325px"></asp:TextBox>
                    <asp:Label ID="lbl_email_estabelecimento" runat="server" CssClass="lbl_texto" 
                      Text="Label"></asp:Label>
                  </td>
                </tr>
                <tr>
                  <td class="style_texto_r">Objetivo:</td>
                  <td>
                    <asp:DropDownList ID="drp_dwn_tp_objetivo" runat="server">
                      <asp:ListItem Value="0">Abertura</asp:ListItem>
                      <asp:ListItem Value="1">Alteração</asp:ListItem>
                      <asp:ListItem Value="2">Alvará de Funcionamento</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lbl_tp_objetivo" runat="server" CssClass="lbl_texto" 
                      Text="Label"></asp:Label>
                  </td>
                </tr>
                <tr>
                  <td class="style_texto_r">Descrição Atividade:</td>
                  <td align="left" style="text-align: left">
                    <asp:TextBox ID="txt_descr_atividade" runat="server" CssClass="input_texto" 
                      MaxLength="1200" Rows="4" TextMode="MultiLine" Width="605px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rqrd_descr_atividade" runat="server" 
                      ControlToValidate="txt_descr_atividade" ErrorMessage="Informe a descrição da atividade" 
                      Font-Size="XX-Small" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:Label ID="lbl_descr_atividade" runat="server" CssClass="lbl_texto" 
                      Text="Label"></asp:Label>
                    <br />
                    <asp:Label ID="lbl_aviso_descr_atividade" runat="server" Text="Label" Font-Size="X-Small"></asp:Label>
                  </td>
                </tr>

                <tr>
                  <td class="style_texto_r">
                    Códigos de Serviço:</td>
                  <td align="left" style="text-align: left">
                    <asp:Panel ID="pnl_cod_servico_inclui" runat="server">
                    <asp:SqlDataSource ID="sql_cod_servicos" runat="server" 
                      ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
                        
                        SelectCommand="SELECT 0 AS cod_servico, ' SELECIONE' AS descr UNION SELECT cod_servico, descr FROM Cod_Servicos WHERE (id_municipio = @id_municipio ) ORDER BY descr">
                      <SelectParameters>
                        <asp:SessionParameter DefaultValue="10" Name="id_municipio" 
                          SessionField="id_municipio" Type="Int32" />
                      </SelectParameters>
                    </asp:SqlDataSource>
                      <asp:TextBox ID="txt_cod_serv_pesq" runat="server" Width="179px"></asp:TextBox>
                      <asp:Button ID="btn_cod_servico_pesq" runat="server" Text="Pesquisar" />
                      <asp:UpdateProgress ID="updt_prgrs_cod_servico_pes" runat="server" 
                        AssociatedUpdatePanelID="updt_pnl_requer" DisplayAfter="10" 
                        EnableViewState="False">
                        <ProgressTemplate>
                          <asp:Label ID="lbl_aviso_requerimento" runat="server" Font-Size="Medium" 
                            ForeColor="Red" Text="Aguarde..."></asp:Label>
                        </ProgressTemplate>
                      </asp:UpdateProgress>
                    <asp:DropDownList ID="drp_dwn_cod_servicos" runat="server" 
                      DataSourceID="sql_cod_servicos" DataTextField="descr" 
                      DataValueField="cod_servico">
                    </asp:DropDownList>
                      <asp:Button ID="btn_incluir_cod_servico" runat="server" Text="Incluir" 
                        CssClass="button_tb" />
                    </asp:Panel>

                    <asp:Panel ID="pnl_lista_cod_servicos" runat="server" ForeColor="Red">
                    <br />
                    <asp:GridView ID="grd_vw_cod_servicos" runat="server" AutoGenerateColumns="False" 
                        DataSourceID="sql_dt_termo_cod_servicos" Width="100%" BackColor="White">
                      <RowStyle Font-Size="Small" />
                      <Columns>
                        <asp:BoundField DataField="cod_servico" HeaderText="Código de Serviço" 
                          SortExpression="cod_servico" >
                          <HeaderStyle Width="25%" />
                          <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descr" HeaderText="Descrição" 
                          SortExpression="descr" >
                          <HeaderStyle Width="50%" />
                        </asp:BoundField>
                        <asp:TemplateField ShowHeader="False" HeaderText="Excluir">
                          <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                              CommandName="Delete" CommandArgument='<%# Bind("cod_servico") %>'  oncommand="Excluir_Cod_Servico"  Text="Excluir"></asp:LinkButton>
                          </ItemTemplate>
                          <HeaderStyle Width="25%" />
                          <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                      </Columns>
                      <HeaderStyle BackColor="#000066" Font-Size="Small" ForeColor="White" />
                      <EditRowStyle Font-Size="Small" />
                      <AlternatingRowStyle BackColor="#99CCFF" />
                    </asp:GridView>
                      <br />
                      <asp:SqlDataSource ID="sql_dt_termo_cod_servicos" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" SelectCommand="SELECT A.cod_servico, B.descr
FROM dbo.Termo_Enquadra_Cod_Servicos AS A
INNER JOIN dbo.Cod_Servicos AS B ON (A.id_municipio = A.id_municipio) AND (A.cod_servico = B.cod_servico)
WHERE (A.id_municipio = @id_municipio) AND (A.id_termo_enquadra = @id_termo_enquadra)
ORDER BY B.descr
">
                        <SelectParameters>
                          <asp:SessionParameter Name="id_municipio" SessionField="id_municipio" />
                          <asp:QueryStringParameter DefaultValue="0" Name="id_termo_enquadra" 
                            QueryStringField="id_termo_" />
                        </SelectParameters>
                      </asp:SqlDataSource>
                    </asp:Panel>

                  </td>
                </tr>

                <tr>
                  <td class="style_texto_r">
                    Documentos:<br />
                    <asp:Label ID="lbl_aviso_docs_formatos" runat="server" ForeColor="Red" 
                      Text="Formatos aceitos: PDF, JPG e PNG."></asp:Label>
                  </td>
                  <td align="left" style="text-align: left">
                    <asp:Panel ID="pnl_documentos" runat="server">
                      <table id="tbl_dados" style="border: 1px solid #000000; width:640px; border-collapse: collapse; background-color: #E8E8E8;" class="info">
                        <tr>
                          <td>
                            <span lang="pt-br" class="span_titulo">Contrato (*):</span>
                              <asp:FileUpload ID="fld_up_contrato" runat="server" style="margin-top: 0px" 
                              ForeColor="Black" />
                            <asp:Button ID="btn_visualiza_contrato" runat="server" Text="Visualizar Contrato" 
                              CssClass="button_verde" Width="115px" />
                            <br />

                            <span lang="pt-br" class="span_titulo">IPTU (*):</span>
                              <asp:FileUpload ID="fld_up_iptu" runat="server" style="margin-top: 0px" 
                              ForeColor="Black" />
                            <asp:Button ID="btn_visualiza_iptu" runat="server" Text="Visualizar IPTU" 
                              CssClass="button_verde" Width="115px" />
                            <br />

                            <span lang="pt-br" class="span_titulo">Planta Baixa (opcional):</span>
                              <asp:FileUpload ID="fld_up_planta" runat="server" style="margin-top: 0px" 
                              ForeColor="Black" />
                            <asp:Button ID="btn_visualiza_planta" runat="server" Text="Visualizar Planta" 
                              CssClass="button_verde" Width="115px" />

                            <br />
                              <asp:Button ID="btn_grava_doctos" runat="server" Text="Gravar Documentos" 
                              CausesValidation="False" EnableViewState="False" CssClass="button_tb" 
                              onclientclick="Mostra_Aviso();" />

                            <div id="dv_aviso_documento" 
                              style="display:none; font-family: Arial, Helvetica, sans-serif; font-size: small; color: #FF0000; font-weight: bold;">
                              Aguarde o envio dos documentos...
                            </div>

                            <asp:Label ID="labERRO" runat="server" Font-Size="X-Small" ForeColor="#CC3300" 
                              Text="labERRO" Visible="False"></asp:Label>
                            &nbsp;&nbsp;

                            <asp:Label ID="lbl_pasta_docs" runat="server" Text="lbl_pasta_docs" 
                              Visible="False" ForeColor="Black"></asp:Label>
                          </td>
                        </tr>
                      </table>
                    </asp:Panel>       
                    <asp:Label ID="lbl_aviso_documentos" runat="server" 
                      Text="Preencha o requerimento e clique em Salvar. Após Salvar anexe os documentos solicitados." 
                      Font-Size="12px" ForeColor="#CC3300"></asp:Label>             
                  </td>
                </tr>

              </table>

           </asp:Panel>

        </div>
        <br />
        <div id="Div2" align="center">
          <asp:Panel ID="pnl_botoes" runat="server" Visible="False">
            <table id="tbl_salvar" style="border: 1px solid #000000; width:100%; border-collapse: collapse; background-color: #E8E8E8;">
              <tr>
                <td class="style_texto_r">
                  <asp:Button ID="btn_Salvar" runat="server" CssClass="button_tb" Text="Salvar" 
                    Width="85px" />
                </td>
                <td>
                  <span lang="pt-br" 
                    style="font-family: Arial, Helvetica, sans-serif; font-size: 11px">&lt;-- clique 
                  aqui para Salvar o Termo de Enquadramento</span></td>
              </tr>
              <tr>
                <td class="style_texto_r">
                  <asp:Button ID="btn_confirmar" runat="server" CssClass="button_verde" 
                    Text="Confirmar" Width="85px" />
                </td>
                <td style="font-family: Arial, Helvetica, sans-serif; font-size: 11px">
                  &lt;-- Clique aqui para confirmar os dados do enquadramento</td>
              </tr>
            </table>
            <br /><br />

            </asp:Panel>
          </div>

          </ContentTemplate>
          <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_verifica" EventName="Click">
            </asp:AsyncPostBackTrigger>
            <asp:AsyncPostBackTrigger ControlID="drp_UF" EventName="SelectedIndexChanged" />
            <asp:PostBackTrigger ControlID="btn_grava_doctos" />
            <asp:AsyncPostBackTrigger ControlID="btn_Salvar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btn_cod_servico_pesq" EventName="Click" />
          </Triggers>

      </asp:UpdatePanel>


      </div>
  </form>
</body>
</html>