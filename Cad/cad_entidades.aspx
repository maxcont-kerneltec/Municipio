<%@ Page Title="" Language="VB" MasterPageFile="Cad_MasterPage.master" AutoEventWireup="false" CodeFile="cad_entidades.aspx.vb" Inherits="cad_entidades" %>

<%@ Register assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.DynamicData" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <link href="../StyleSheet.css" rel="stylesheet" type="text/css" />
<style type="text/css">
  .style6
  {
    width: 286px;
  }
  .style8
  {
    width: 185px;
    text-align: right;
  }
  .style9
  {
    width: 367px;
  }
  </style>
<script type ="text/javascript">
  function btn_visualza_end_cidadao(num_instalacao) {
    window.open('rel_endereco_cidadao.aspx?num_instalacao=' + num_instalacao, '', 'toolbar=yes, resizable=yes, scrollbars=yes, width=670px, height=500px')
  }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:SqlDataSource ID="sql_cad_endereco_social" runat="server" 
      ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
      SelectCommand="sp1Cad_Entidades_Pega" SelectCommandType="StoredProcedure" 
      ProviderName="<%$ ConnectionStrings:municipio_cloud.ProviderName %>">
      <SelectParameters>
        <asp:SessionParameter Name="sp_id_municipio" SessionField="id_municipio" 
          Type="String" />
      </SelectParameters>
    </asp:SqlDataSource>
    <br />

      <table id="tb_pesquisa" style="border: thin solid #FFFFFF; width:100%; display:none" bgcolor="#66CCFF" cellpadding="0" cellspacing="2">
        <tr>
          <td class="style6" width="35%">
            SUS:<asp:TextBox ID="txt_SUS_Num_Pesq" runat="server" Width="173px"></asp:TextBox>
          </td>
          <td class="style8" width="10%">
            Nome:</td>
          <td class="style9" width="35%">
            <asp:TextBox ID="txt_nome_pesq" runat="server" Width="295px"></asp:TextBox>
          </td>
          <td width="15%">
            <asp:Button ID="Button3" runat="server" Text="Pesquisar"  Width="87px" 
              CssClass="button_tb" />
          </td>
        </tr>
        <tr>
          <td class="style6">
            CPF:<asp:TextBox ID="txt_CPF_pesq" runat="server" Width="172px"></asp:TextBox>
          </td>
          <td class="style8">
          </td>
          <td class="style9">
            &nbsp;</td>
          <td>
            &nbsp;</td>
        </tr>
      </table>
    <br />

    <asp:Button ID="btn_novo" runat="server" CssClass="button_tb" Text="Novo" 
      UseSubmitBehavior="False" Height="22px" Width="80px" />&lt;-- clique aqui para cadastrar novo endereço 
    social
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <br />
    <asp:UpdatePanel ID="updt_pnl_reg_nasc" runat="server">
      <ContentTemplate>

        <asp:Panel ID="pnl_novo" runat="server" Visible="False">

          <table id="tb_inclui" style="border: 2px solid #000000;" bgcolor="#66CCFF" cellpadding="0" cellspacing="2" align="center">
            <tr>
              <td colspan="2" style="font-weight: bold; color: #FF0000;" align="center">
                <asp:Label ID="lbl_resultado" runat="server" Font-Size="Small" ForeColor="Red">
                     </asp:Label>
              </td>
            </tr>
            <tr style="border-style: solid; border-width: 2px; border-color: #000000" bgcolor="White">
              <td align="right" height="23" width="250">Número de Instalação:</td>
              <td align="left" width="560">
                &nbsp;
                <asp:TextBox ID="txt_num_instalacao" runat="server" MaxLength="15" 
                  ValidationGroup="grp_num_instalacao" CssClass="input_texto" Width="140px"></asp:TextBox>
                <asp:Label ID="lbl_num_instalacao" runat="server" Text="Label" Visible="False" 
                  Width="140px"></asp:Label>
                <asp:RequiredFieldValidator ID="rqrd_num_instalacao" runat="server" 
                  ControlToValidate="txt_num_instalacao" ErrorMessage="Informe o número de instalação" 
                  ValidationGroup="grp_num_instalacao" Display="Dynamic"></asp:RequiredFieldValidator>
              </td>
            </tr>

            <tr bgcolor="White" 
              style="border-style: solid; border-width: 2px; border-color: #000000">
              <td align="right" height="23" width="250">
                CNPJ:</td>
              <td align="left" width="560">
                &nbsp;
                <asp:TextBox ID="txt_CNPJ" runat="server" MaxLength="18" 
                  ValidationGroup="grp_novo" Width="140px" CssClass="input_texto"></asp:TextBox>
                <asp:Label ID="lbl_cnpj" runat="server" Text="Label" Visible="False" 
                  Width="140px"></asp:Label>
                <asp:RequiredFieldValidator ID="rqrd_cnpj" runat="server" 
                  ControlToValidate="txt_CNPJ" Display="Dynamic" ErrorMessage="Informe o CNPJ." 
                  SetFocusOnError="True" ValidationGroup="grp_num_instalacao"></asp:RequiredFieldValidator>
                <asp:Button ID="btn_consultar" runat="server" CssClass="button_verde" 
                  Text="Consultar" 
                  ValidationGroup="grp_num_instalacao" />
              </td>
            </tr>
          </table>
        </asp:Panel>
        <asp:Panel ID="pnl_dados" runat="server" Visible="False">

          <table id="Table1" style="border: 1px solid #000000;" cellpadding="0" cellspacing="2" align="center">
            <tr>
              <td colspan="2">Dados da entidade</td>
            </tr>
            <tr style="border-style: solid; border-width: 2px; border-color: #000000; background-color: #E8E8E8;">
              <td align="right" height="23" width="250">
                Numero Máximo de Cidadãos:</td>
              <td align="left" width="560">
                <asp:TextBox ID="txt_num_max_permite" runat="server" MaxLength="4" 
                  CssClass="input_texto"></asp:TextBox>
                &nbsp;&nbsp;Cidadãos Cadastrados:
                <asp:Button ID="btn_conta_num_cidadao" runat="server" CssClass="button_verde" 
                  Text="Button" />
                <asp:RequiredFieldValidator ID="rqrd_num_max_permite" runat="server" 
                  ErrorMessage="Informe o número máximo de cidadãos" 
                  ControlToValidate="txt_num_max_permite" Display="Dynamic" 
                  SetFocusOnError="True" ValidationGroup="grp_novo"></asp:RequiredFieldValidator>
              </td>
            </tr>
            <tr
              style="border-style: solid; border-width: 2px; border-color: #000000; background-color: #E8E8E8;">
              <td align="right" height="23" width="250">
                Razão Social:</td>
              <td align="left" width="560">
                <asp:TextBox ID="txt_razao_social" runat="server" Width="300px" MaxLength="120" 
                  CssClass="input_texto"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="rqrd_razao_social" runat="server" 
                  ControlToValidate="txt_razao_social" ErrorMessage="Informe a razão social" 
                  SetFocusOnError="True" ValidationGroup="grp_novo" Display="Dynamic"></asp:RequiredFieldValidator>
                &nbsp; Tipo de Entidade:
                <asp:DropDownList ID="drp_tp_entidade" runat="server">
                  <asp:ListItem Value="0">Asilo</asp:ListItem>
                  <asp:ListItem Value="1">Orfanato</asp:ListItem>
                </asp:DropDownList>
              </td>
            </tr>
            <tr
              style="border-style: solid; border-width: 2px; border-color: #000000; background-color: #E8E8E8;">
              <td align="right" height="23" width="250">
                Logradouro, nro</td>
              <td align="left" width="560">
                <asp:TextBox ID="txt_xLgr" runat="server" Width="315px" CssClass="input_texto"></asp:TextBox>
                ,
                <asp:TextBox ID="txt_nro" runat="server" Width="50px" CssClass="input_texto"></asp:TextBox>
              </td>
            </tr>
            <tr
              style="border-style: solid; border-width: 2px; border-color: #000000; background-color: #E8E8E8;">
              <td align="right" height="23" width="250">
                Complemento:</td>
              <td align="left" width="560">
                <asp:TextBox ID="txt_xCpl" runat="server" Width="245px" MaxLength="60" 
                  CssClass="input_texto"></asp:TextBox>
                &nbsp; Bairro:
                <asp:TextBox ID="txt_xBairro" runat="server" MaxLength="60" 
                  CssClass="input_texto"></asp:TextBox>
              </td>
            </tr>
            <tr
              style="border-style: solid; border-width: 2px; border-color: #000000; background-color: #E8E8E8;">
              <td align="right" height="23" width="250">
                CEP:</td>
              <td align="left" width="560">
                <asp:TextBox ID="txt_CEP" runat="server" MaxLength="9" CssClass="input_texto"></asp:TextBox>
              </td>
            </tr>
            <tr style="border-style: solid; border-width: 2px; border-color: #000000; background-color: #E8E8E8;">
              <td align="right" height="23" width="250">
                Número Inscrição IPTU:</td>
              <td align="left" width="560">
                <asp:TextBox ID="txt_num_inscr_imovel" runat="server" CssClass="input_texto" 
                  MaxLength="20"></asp:TextBox>
              </td>
            </tr>
            <tr style="border-style: solid; border-width: 2px; border-color: #000000; background-color: #E8E8E8;">
              <td align="right" height="23" width="250">
                Número Matrícula:</td>
              <td align="left" width="560">
                <asp:TextBox ID="txt_num_matri_imovel" runat="server" CssClass="input_texto" 
                  MaxLength="50"></asp:TextBox>
              </td>
            </tr>
            <tr
              style="border-style: solid; border-width: 2px; border-color: #000000; background-color: #E8E8E8;">
              <td align="right" height="23" width="250">
                Telefone:</td>
              <td align="left" width="560">
                <asp:TextBox ID="txt_telefone" runat="server" MaxLength="60" 
                  CssClass="input_texto"></asp:TextBox>
              </td>
            </tr>
            <tr 
              style="border-style: solid; border-width: 2px; border-color: #000000; background-color: #E8E8E8;">
              <td align="right" height="23" width="250">
                E-mail:</td>
              <td align="left" width="560">
                <asp:TextBox ID="txt_email" runat="server" Width="230px" MaxLength="100" 
                  CssClass="input_texto"></asp:TextBox>
              </td>
            </tr>
            <tr 
              style="border-style: solid; border-width: 2px; border-color: #000000; background-color: #E8E8E8;">
              <td align="right" height="23" width="250">
                CPF Responsável:</td>
              <td align="left" width="560">
                <asp:TextBox ID="txt_CPF_resp" runat="server" MaxLength="14" 
                  CssClass="input_texto" Width="230px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rqrd_CPF_resp" runat="server" 
                  ControlToValidate="txt_CPF_resp" Display="Dynamic" 
                  ErrorMessage="Informe o CPF do Responsável" SetFocusOnError="True" 
                  ValidationGroup="grp_novo"></asp:RequiredFieldValidator>
              </td>
            </tr>
            <tr
              style="border-style: solid; border-width: 2px; border-color: #000000; background-color: #E8E8E8;">
              <td align="right" height="23" width="250">
                Nome responsável:</td>
              <td align="left" width="560">
                <asp:TextBox ID="txt_nome_responsavel" runat="server" MaxLength="120" 
                  CssClass="input_texto" Width="230px"></asp:TextBox>
              </td>
            </tr>
            <tr>
              <td colspan="2" style="font-weight: bold" align="center" bgcolor="White">
                <asp:Button ID="btn_incluir" runat="server" Text="Salvar" CssClass="button_tb" Width="65px" 
                  EnableTheming="False" EnableViewState="False" Height="22px" 
                  ValidationGroup="grp_novo" />
              &nbsp;&nbsp;&nbsp;&nbsp;
              &nbsp;<asp:Button ID="btn_cancelar" runat="server" CssClass="button_tb" 
                  EnableViewState="False" Text="Cancelar" UseSubmitBehavior="False"
                  Width="73px" Height="22px" />
              </td>
            </tr>
          </table>

        </asp:Panel>

      </ContentTemplate>

      <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btn_consultar" EventName="Click" />
      </Triggers>
    </asp:UpdatePanel>

    <br /><br />

    <div id="dv_resultado">
      <asp:GridView ID="GridView_cad_endereco_social" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" DataSourceID="sql_cad_endereco_social" 
        EmptyDataText="Nenhum Registro Localizado" Width="100%" 
        PageSize="50" Font-Size="Small">
        <RowStyle BackColor="#99CCFF" />
    <EmptyDataRowStyle BackColor="#0099CC" />
    <Columns>
      <asp:TemplateField HeaderText="CNPJ" SortExpression="CNPJ">
        <EditItemTemplate>
          <asp:Label ID="Label1" runat="server" Text='<%# Bind("CNPJ") %>'></asp:Label>
        </EditItemTemplate>
        <ItemTemplate>
          <asp:Button ID="Button2" runat="server" 
            CommandArgument='<%# Bind("CNPJ") %>' oncommand="Acessa_Cadastro" 
            Text='<%# Bind("CNPJ") %>' UseSubmitBehavior="False" Width="120px" 
            CssClass="button_tb" />
        </ItemTemplate>
        <HeaderStyle Width="120px" />
      </asp:TemplateField>
      <asp:BoundField DataField="num_instalacao" HeaderText="Num. Instalação" 
        SortExpression="num_instalacao" >
      <HeaderStyle Width="120px" />
      </asp:BoundField>
      <asp:BoundField DataField="razao_social" HeaderText="Razão Social" 
        SortExpression="razao_social" />
      <asp:BoundField DataField="nome_resp" HeaderText="Responsável" 
        SortExpression="nome_resp" >
      <HeaderStyle Width="180px" />
      </asp:BoundField>
      <asp:BoundField DataField="num_max_permite" HeaderText="Número Máximo" >
      <ItemStyle HorizontalAlign="Right" />
      </asp:BoundField>
      <asp:BoundField DataField="conta_num_cidadao" HeaderText="Cidadãos Cadastrados">
      <ItemStyle HorizontalAlign="Right" />
      </asp:BoundField>
      </Columns>
        <FooterStyle BorderStyle="None" />
        <HeaderStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
      </asp:GridView>
    </div>
    <br />
    </asp:Content>
