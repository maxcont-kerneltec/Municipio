<%@ Page Title="" Language="VB" MasterPageFile="Cad_MasterPage.master" AutoEventWireup="false" CodeFile="eventos_participantes.aspx.vb" Inherits="eventos_participantes" %>

<%@ Register assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.DynamicData" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <link href="../StyleSheet.css" rel="stylesheet" type="text/css" />
<style type="text/css">
  .style6
  {
    width: 286px;
  }
  </style>
<script type ="text/javascript">
  function btn_imprimir_onclick(id_cidadao) {
    window.open('cad_cidadao_imprime.aspx?tpImp=COMP&id_cidadao=' + id_cidadao, '', 'toolbar=yes, resizable=yes, scrollbars=yes, width=670px, height=500px')
    return false;
  }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:SqlDataSource ID="sql_eventos_participantes" runat="server" 
      ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
      SelectCommand="sp1Evento_Pega_Participantes" SelectCommandType="StoredProcedure" 
      ProviderName="<%$ ConnectionStrings:municipio_cloud.ProviderName %>">
      <SelectParameters>
        <asp:SessionParameter Name="sp_id_municipio" SessionField="id_municipio" 
          Type="String" />
        <asp:Parameter DefaultValue="1" Name="spid_evento" Type="String" />
        <asp:Parameter DefaultValue="0" Name="spCPF" Type="String" />
        <asp:Parameter DefaultValue="P" Name="spacao" Type="String" />
      </SelectParameters>
    </asp:SqlDataSource>
    <br />

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <br />
    <asp:UpdatePanel ID="updt_pnl_reg_nasc" runat="server">
      <ContentTemplate>
        <asp:Panel ID="pnl_novo" runat="server">

          <table id="tb_inclui" style="border: 2px solid #000000;" bgcolor="#66CCFF" 
            cellpadding="0" cellspacing="2" align="center" width="640">
            <tr>
              <td colspan="2" style="font-weight: bold; color: #FF0000;" align="center">
                <asp:Label ID="lbl_resultado" runat="server" Font-Size="Small" ForeColor="Red">
                    Informe o CPF e clique em Incluir            </asp:Label>
              </td>
            </tr>
            <tr style="border-style: solid; border-width: 2px; border-color: #000000" bgcolor="White">
              <td align="right" height="23" width="300">CPF:</td>
              <td align="left" width="300">
                <asp:TextBox ID="txt_CPF_inclui" runat="server" MaxLength="14" 
                  ValidationGroup="grp_novo" Width="136px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rqrd_CPF_novo" runat="server" 
                  ControlToValidate="txt_CPF_inclui" ErrorMessage="Informe o CPF." 
                  ValidationGroup="grp_novo"></asp:RequiredFieldValidator>
              </td>
            </tr>
            <tr>
              <td colspan="2" style="font-weight: bold" align="center">
                <asp:Button ID="btn_incluir" runat="server" Text="Incluir" CssClass="button_tb" Width="65px" 
                  EnableTheming="False" EnableViewState="False" Height="26px" 
                  ValidationGroup="grp_novo" />
              &nbsp;&nbsp;&nbsp;&nbsp;
              &nbsp;<asp:Button ID="btn_cancelar" runat="server" CssClass="button_tb" EnableViewState="False" Text="Cancelar" UseSubmitBehavior="False"
                  Width="73px" />
                
              </td>
            </tr>
          </table>
          <br /><br />
                <asp:GridView ID="GridView_participantes" runat="server" AllowPaging="True" 
                  AllowSorting="True" AutoGenerateColumns="False" 
                  DataSourceID="sql_eventos_participantes" 
                  EmptyDataText="Nenhum Registro Localizado" Font-Size="Small" PageSize="50" 
                  Width="100%">
                  <RowStyle BackColor="#99CCFF" />
                  <EmptyDataRowStyle BackColor="#0099CC" />
                  <Columns>
                    <asp:TemplateField ShowHeader="False">
                      <ItemTemplate>
                        <asp:Button ID="Button1" runat="server" 
                          CommandArgument='<%# Bind("cpf_usuario") %>' CssClass="button_tb" 
                          oncommand="Acessa_Cadastro" Text='<%# Bind("cpf_usuario") %>' 
                          UseSubmitBehavior="False" Width="120px" />
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="nome" HeaderText="Nome" SortExpression="nome" />
                    <asp:BoundField DataField="descr_assunto" HeaderText="Assunto" 
                      SortExpression="descr_assunto" />
                  </Columns>
                  <FooterStyle BorderStyle="None" />
                  <HeaderStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                  <AlternatingRowStyle BackColor="White" />
                </asp:GridView>

      </asp:Panel>
      </ContentTemplate>

    </asp:UpdatePanel>

    <br /><br />
    <div id="dv_resultado">
    </div>
    <br />
    </asp:Content>
