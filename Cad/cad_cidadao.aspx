<%@ Page Title="" Language="VB" MasterPageFile="Cad_MasterPage.master" AutoEventWireup="false" CodeFile="cad_cidadao.aspx.vb" Inherits="cad_cidadao" %>

<%@ Register assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.DynamicData" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

  <script type ="text/javascript">
  function btn_imprimir_onclick(id_cidadao) {
    window.open('cad_cidadao_imprime.aspx?tpImp=COMP&id_cidadao=' + id_cidadao, '', 'toolbar=yes, resizable=yes, scrollbars=yes, width=670px, height=500px')
    return false;
  }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:SqlDataSource ID="sql_cad_cidadao" runat="server" 
      ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
      SelectCommand="sp1Cidadao_Pesquisa" SelectCommandType="StoredProcedure" 
      ProviderName="<%$ ConnectionStrings:municipio_cloud.ProviderName %>">
      <SelectParameters>
        <asp:SessionParameter Name="sp_id_municipio" SessionField="id_municipio" 
          Type="String" />
        <asp:ControlParameter ControlID="txt_SUS_Num_Pesq" DefaultValue="0" 
          Name="spSUS_num" PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="txt_CPF_pesq" DefaultValue="0" Name="spCPF" 
          PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="txt_nome_pesq" DefaultValue=" " Name="spnome" 
          PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="drp_tipo_filtro" DefaultValue="0" 
          Name="sp_filtro" PropertyName="SelectedValue" Type="String" />
        <asp:ControlParameter ControlID="txt_RG_pesq" DefaultValue="  " Name="spRG" 
          PropertyName="Text" />
      </SelectParameters>
    </asp:SqlDataSource>
    <br />

      <table id="tb_pesquisa" class="table_pesquisa">
        <tr>
          <td class="label_coluna_r" width="20%">SUS:</td>
          <td class="td_coluna_l">
            <asp:TextBox ID="txt_SUS_Num_Pesq" runat="server" 
              CssClass="input_texto_t1" MaxLength="15"></asp:TextBox>
          </td>
          <td class="label_coluna_r" width="20%">Nome:</td>
          <td class="td_coluna_l">
            <asp:TextBox ID="txt_nome_pesq" runat="server" 
              CssClass="input_texto_l1"></asp:TextBox>
          </td>
          <td rowspan="2" align="center">
            <asp:Button ID="Button3" runat="server" Text="Pesquisar"  Width="87px" 
              CssClass="button_tb" />
          </td>
        </tr>
        <tr>
          <td class="label_coluna_r">CPF:</td>
          <td class="td_coluna_l">
            <asp:TextBox ID="txt_CPF_pesq" runat="server" 
              CssClass="input_texto_t1" MaxLength="14"></asp:TextBox>
          </td>
          <td class="label_coluna_r">RG:</td>
          <td class="td_coluna_l">
            <asp:TextBox ID="txt_RG_pesq" runat="server" CssClass="input_texto_t1"></asp:TextBox>
          </td>
        </tr>
      </table>
    <br />

    <asp:Button ID="btn_novo" runat="server" CssClass="button_tb" Text="Novo" 
      UseSubmitBehavior="False" Width="75px" />&lt;-- clique aqui para cadastrar novo cidadão
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <br />
    <asp:UpdatePanel ID="updt_pnl_reg_nasc" runat="server">
      <ContentTemplate>
        <asp:Panel ID="pnl_novo" runat="server" Visible="false">

          <table id="tb_inclui" style="border: 2px solid #000000;" bgcolor="#66CCFF" cellpadding="0" cellspacing="2" align="center">
            <tr>
              <td colspan="2" style="font-weight: bold; color: #FF0000;" align="center">
                <asp:Label ID="lbl_resultado" runat="server" Font-Size="Small" ForeColor="Red">
                    Informe o CPF.<br />Caso o CFP informado seja do responsável, deverá ser informada a certidão de nascimento.
                </asp:Label>
              </td>
            </tr>
            <tr style="border-style: solid; border-width: 2px; border-color: #000000" bgcolor="White">
              <td align="right" height="23" width="250">SUS:</td>
              <td align="left" width="560">
                <asp:TextBox ID="txt_SUS_num" runat="server" MaxLength="15" 
                  CssClass="input_texto_t1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rqrd_SUS" runat="server" 
                  ControlToValidate="txt_SUS_num" ErrorMessage="Informe o SUS." 
                  ValidationGroup="grp_novo"></asp:RequiredFieldValidator>
              </td>
            </tr>
            <tr bgcolor="White" 
              style="border-style: solid; border-width: 2px; border-color: #000000">
              <td align="right" height="23" width="250">
                CPF:</td>
              <td align="left" width="560">
                <asp:TextBox ID="txt_CPF_inclui" runat="server" MaxLength="14" 
                  ValidationGroup="grp_novo" CssClass="input_texto_t1"></asp:TextBox>
                <asp:DropDownList ID="drp_tipo_cfp" runat="server" CssClass="drop_down_class">
                  <asp:ListItem Value="P">Próprio</asp:ListItem>
                  <asp:ListItem Value="R">Responsável</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rqrd_CPF_novo" runat="server" 
                  ControlToValidate="txt_CPF_inclui" ErrorMessage="Informe o CPF." 
                  ValidationGroup="grp_novo"></asp:RequiredFieldValidator>
              </td>
            </tr>
            <tr>
              <td align="right">
                Registro de Nascimento:
                <asp:DropDownList ID="drp_tipo_reg_nasc" runat="server" AutoPostBack="True" 
                  style="height: 22px" CssClass="drop_down_class">
                  <asp:ListItem Value="0">Antigo</asp:ListItem>
                  <asp:ListItem Value="1">Novo</asp:ListItem>
                </asp:DropDownList>

              </td>
              <td align="left">

                  <asp:Panel ID="pnl_reg_antigo" runat="server" CssClass="style10">
                            Termo:
                            <asp:TextBox ID="txt_num_reg_inclui" runat="server" 
                              ValidationGroup="grp_novo" CssClass="input_texto_t1"></asp:TextBox>
                            &nbsp; Folha: 
                            <asp:TextBox ID="txt_num_reg_folha_inclui" runat="server" 
                              MaxLength="50" CssClass="input_texto_t1"></asp:TextBox>
                            Livro:<asp:TextBox ID="txt_num_reg_livro_inclui" runat="server" MaxLength="50" 
                              CssClass="input_texto_t1"></asp:TextBox>
                  </asp:Panel>
                  <asp:Panel ID="pnl_reg_novo" runat="server" Visible="False">
                            <asp:TextBox ID="txt_cod_nac" runat="server" Width="53px" MaxLength="6" 
                              CssClass="input_texto"></asp:TextBox>&nbsp;
                            <asp:TextBox ID="txt_cod_acervo" runat="server" Width="25px" MaxLength="2" 
                              CssClass="input_texto"></asp:TextBox>&nbsp;
                            <asp:TextBox ID="txt_serv" runat="server" Width="25px" MaxLength="2" 
                              CssClass="input_texto"></asp:TextBox>&nbsp;
                            <asp:TextBox ID="txt_ano_registro" runat="server" Width="43px" MaxLength="4" 
                              CssClass="input_texto"></asp:TextBox>&nbsp;
                            <asp:TextBox ID="txt_tipo_livro" runat="server" Width="20px" MaxLength="1" 
                              CssClass="input_texto"></asp:TextBox>&nbsp;
                            <asp:TextBox ID="txt_num_livro" runat="server" Width="40px" MaxLength="5" 
                              CssClass="input_texto"></asp:TextBox>&nbsp;
                            <asp:TextBox ID="txt_folha_livro" runat="server" Width="40px" MaxLength="3" 
                              CssClass="input_texto"></asp:TextBox>&nbsp;
                            <asp:TextBox ID="txt_num_termo" runat="server" Width="72px" MaxLength="7" 
                              CssClass="input_texto"></asp:TextBox>&nbsp;
                            <asp:TextBox ID="txt_dig_verif" runat="server" Width="40px" MaxLength="2" 
                              CssClass="input_texto"></asp:TextBox>
                  </asp:Panel>

              </td>
            </tr>
            <tr>
              <td colspan="2" style="font-weight: bold" align="center" bgcolor="White">
                <asp:Button ID="btn_incluir" runat="server" Text="Incluir" CssClass="button_tb" 
                  UseSubmitBehavior="False" 
                  EnableTheming="False" EnableViewState="False" 
                  ValidationGroup="grp_novo" />
              &nbsp;&nbsp;&nbsp;&nbsp;
              &nbsp;<asp:Button ID="btn_cancelar" runat="server" CssClass="button_tb" EnableViewState="False" Text="Cancelar" UseSubmitBehavior="False"
                  Width="73px" />
              </td>
            </tr>
          </table>
      </asp:Panel>
      </ContentTemplate>

      <Triggers>
      <asp:AsyncPostBackTrigger ControlID="drp_tipo_reg_nasc" EventName="SelectedIndexChanged">
      </asp:AsyncPostBackTrigger>
      <asp:AsyncPostBackTrigger ControlID="btn_novo" EventName="Click" />
      </Triggers>
    </asp:UpdatePanel>

    <br /><br />
    <div style="padding: 3px; background-color: #CCCCCC; height: 30px; clip: rect(auto, auto, auto, auto);">
    Filtros:
      <asp:DropDownList ID="drp_tipo_filtro" runat="server" AutoPostBack="True">
        <asp:ListItem Value="7">Cadastros em Análise</asp:ListItem>
        <asp:ListItem Value="0">Selecione</asp:ListItem>
        <asp:ListItem Value="1">Eleitor fora do município</asp:ListItem>
        <asp:ListItem Value="2">Morador de outro município</asp:ListItem>
        <asp:ListItem Value="3">Cadastro com CPF do responsável</asp:ListItem>
        <asp:ListItem Value="4">Sem documentos gravados</asp:ListItem>
        <asp:ListItem Value="5">Funcionário de Barueri</asp:ListItem>
        <asp:ListItem Value="6">Com deficiência</asp:ListItem>
      </asp:DropDownList>
    &nbsp;
      &nbsp;&nbsp;&nbsp;
      </div>
    <div id="dv_resultado">
      <asp:GridView ID="GridView_Cidadaos" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" DataSourceID="sql_cad_cidadao" 
        EmptyDataText="Nenhum Registro Localizado" Width="100%" 
        PageSize="50" Font-Size="Small">
        <RowStyle CssClass="grd_row" />
    <EmptyDataRowStyle BackColor="#0099CC" />
    <Columns>
      <asp:TemplateField HeaderText="ID Cidadão" SortExpression="id_cidadao">
        <EditItemTemplate>
          <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("id_cidadao") %>'></asp:TextBox>
        </EditItemTemplate>
        <ItemTemplate>
          <asp:Button ID="bnt_id_cidadao" runat="server" 
            CommandArgument='<%# Bind("id_cidadao") %>' oncommand="Acessa_Cadastro" 
            Text='<%# Bind("id_cidadao") %>' UseSubmitBehavior="False" Width="80px" 
            CssClass="button_tb" />
        </ItemTemplate>
        <ControlStyle Width="80px" />
      </asp:TemplateField>
      <asp:BoundField DataField="id_cidadao" HeaderText="ID Cidadão" 
        SortExpression="id_cidadao">
      <ItemStyle CssClass="grd_cell" />
      </asp:BoundField>
      <asp:BoundField DataField="SUS_num" HeaderText="SUS" 
        SortExpression="SUS_num" />
      <asp:BoundField DataField="nome_registro" HeaderText="Nome" 
        SortExpression="nome_registro" />
      <asp:BoundField DataField="RG" HeaderText="RG" 
        SortExpression="RG" />
      <asp:BoundField DataField="CPF" HeaderText="CPF" SortExpression="CPF" />
      <asp:BoundField DataField="nome_mae" HeaderText="Mãe" 
        SortExpression="nome_mae" />
      <asp:BoundField DataField="nome_pai" HeaderText="Pai" 
        SortExpression="nome_pai" />
      <asp:TemplateField ShowHeader="False">
        <ItemTemplate>
          <input id="Button1" onclick="btn_imprimir_onclick('<%# eval("id_cidadao") %>')" type="button" value="visualizar" class="button_claro" />
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
      </asp:TemplateField>
      </Columns>
        <FooterStyle BorderStyle="None" />
        <HeaderStyle Font-Bold="True" ForeColor="White" CssClass="grd_header" />
        <AlternatingRowStyle CssClass="grd_row_alt" />
      </asp:GridView>
    </div>
    <br />
    </asp:Content>
