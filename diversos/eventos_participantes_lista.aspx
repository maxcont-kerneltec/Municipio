<%@ Page Language="VB" AutoEventWireup="false" CodeFile="eventos_participantes_lista.aspx.vb" Inherits="eventos_participantes_lista" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>O PREFEITO NO SEU BAIRRO</title>
  <link href="../StyleSheet.css" rel="stylesheet" type="text/css" />
  <script src="../inc/jscript.js" type="text/javascript"></script>
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
</head>
<body>
  <form id="form1" runat="server">
    <div style="position: absolute; width: 95%;">
      <table width="100%" bgcolor="#EFF3FB">
        <tr>
          <td colspan="1" valign="top">
            <asp:Image ID="Image1" runat="server" ImageAlign="Left" ImageUrl="../img/logo_barueri.jpg" EnableViewState="False" />
            <br />
            <strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </strong><span title="Prefeitura de Barueri" style="color: #000080">&nbsp;</span>
          </td>
          <td align="right">
            <span title="Prefeitura de Barueri" style="color: #000080; font-size: 14px; font-weight: bold;">O PREFEITO NO SEU BAIRRO.<br /></span>
            <br />
            <span lang="pt-br" style="text-decoration: underline; font-size: small; cursor: pointer; color: #000080; font-weight: bold;" 
              onclick="window.location='../login_usuario.aspx?a=logout&SIS_=1'">Sair</span>
          </td>
        </tr>
      </table>
    </div>
    <br />

    <div style="position: absolute; top: 140px; width: 95%;">

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
    <asp:UpdatePanel ID="updt_pnl_reg_nasc" runat="server">
      <ContentTemplate>
        
        <asp:Panel ID="pnl_novo" runat="server">

          <table id="tb_inclui" style="border: 2px solid #000000;" bgcolor="#66CCFF" 
            cellpadding="0" cellspacing="2" align="center" width="100%">
            <tr>
              <td colspan="2" style="font-weight: bold; color: #FF0000;" align="center">
                <asp:Label ID="lbl_resultado" runat="server" Font-Size="Small" ForeColor="Red"> Informe o CPF e clique em Incluir </asp:Label>
              </td>
            </tr>
            <tr style="border-style: solid; border-width: 2px; border-color: #000000" bgcolor="White">
              <td align="right" height="23" width="300" 
                style="font-family: Arial, Helvetica, sans-serif; font-size: medium">CPF:</td>
              <td align="left" width="300">
                <asp:TextBox ID="txt_CPF_inclui" runat="server" MaxLength="14" 
                  ValidationGroup="grp_novo"  InputScope="Number" Width="136px" CssClass="input_texto_grande"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rqrd_CPF_novo" runat="server" 
                  ControlToValidate="txt_CPF_inclui" ErrorMessage="Informe o CPF." 
                  ValidationGroup="grp_novo"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                  ControlToValidate="txt_CPF_inclui" ErrorMessage="CompareValidator" 
                  Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
              </td>
            </tr>
            <tr>
              <td colspan="2" style="font-weight: bold" align="center">
                <asp:Button ID="btn_incluir" runat="server" Text="Incluir" CssClass="button_tb" Width="65px" 
                  EnableTheming="False" EnableViewState="False" Height="26px" 
                  ValidationGroup="grp_novo" Font-Size="Small" />
              &nbsp;&nbsp;&nbsp;&nbsp;
              &nbsp;<asp:Button ID="btn_cancelar" runat="server" CssClass="button_tb" 
                  EnableViewState="False" Text="Cancelar" UseSubmitBehavior="False"
                  Width="73px" Font-Size="Small" Height="26px" />
              </td>
            </tr>
          </table>
          <br />
          <div class="aviso_vermelho">
            Clique no CPF para consultar o assunto.
          </div>
          <br />
          <asp:GridView ID="GridView_participantes" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" 
            DataSourceID="sql_eventos_participantes" 
            EmptyDataText="Nenhum Registro Localizado" Font-Size="Small" PageSize="50" 
            Width="100%">
            <RowStyle BackColor="#99CCFF" />
            <EmptyDataRowStyle BackColor="#0099CC" />
            <Columns>
              <asp:TemplateField ShowHeader="False" HeaderText="CPF">
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
    </div>
  </form>
</body>
</html>
