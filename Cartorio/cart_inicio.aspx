<%@ Page Title="" Language="VB" MasterPageFile="cart_MasterPage.master" AutoEventWireup="false" CodeFile="cart_inicio.aspx.vb" Inherits="cart_inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <link href="../StyleSheet.css" rel="stylesheet" type="text/css" />
  <style type="text/css">
    .style7
    {
      text-align: left;
    }
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
      ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
      SelectCommand="sp1Pega_Dados_Inicio" SelectCommandType="StoredProcedure">
      <SelectParameters>
        <asp:SessionParameter Name="sp_id_municipio" SessionField="id_municipio" 
          Type="String" />
        <asp:SessionParameter Name="spid_usuario" SessionField="id_usuario" 
          Type="String" />
      </SelectParameters>
    </asp:SqlDataSource>
    <br />
<table style="border-style: 1; border-width: 1px; border-color: #000000; width:100%;" 
      bgcolor="#99CCFF" cellpadding="2" cellspacing="2">
  <tr>
    <td class="style7">
      Cartório:
      <asp:Label ID="lbl_cartorio" runat="server" Text="Label"></asp:Label>
    </td>
    <td class="style8">
      CNPJ:
      <asp:Label ID="lbl_cnpj" runat="server" Text="Label"></asp:Label>
    </td>
  </tr>
  <tr>
    <td class="style7">
      Cidade:
      <asp:Label ID="lbl_local" runat="server" Text="Label"></asp:Label>
    </td>
    <td class="style8">
      &nbsp;
    </td>
  </tr>
  <tr>
    <td class="style7">
      Nome:
      <asp:Label ID="lbl_nome" runat="server" Text="Label"></asp:Label>
    </td>
    <td class="style8">
      CPF:
      <asp:Label ID="lbl_CPF" runat="server" Text="Label"></asp:Label>
    </td>
  </tr>
  <tr>
    <td class="style8">
      E-mail:
      <asp:Label ID="lbl_email" runat="server" Text="Label"></asp:Label>
    </td>
    <td class="style7">
      Cadastrado em 
      <asp:Label ID="lbl_dt_cadastro" runat="server" Text="Label"></asp:Label>
    </td>
  </tr>
  <tr>
    <td class="style7">
      Último Acesso:
      <asp:Label ID="lbl_ult_acesso" runat="server" Text="Label"></asp:Label>
    </td>
    <td class="style8">
      Número de Acessos:
      <asp:Label ID="lbl_conta_acessos" runat="server" Text="Label"></asp:Label>
    </td>
  </tr>
  </table>
<br /><br />
<table style="border-style: 1; border-width: 1px; border-color: #000000; width:100%;" bgcolor="#99CCFF" cellpadding="2" cellspacing="2">
  <tr>
    <td colspan="2" align="center">Estatísticas</td>
  </tr>

  <tr>
    <td class="style7">
      Usuários Cadastrados:
      <asp:Label ID="lbl_num_usuarios" runat="server" Text="Label"></asp:Label>
    </td>
    <td class="style8">&nbsp;</td>
  </tr>

  <tr>
    <td class="style7">
      Guias de ITBI-e emitidas:
      <asp:Label ID="lbl_num_guias" runat="server" Text="Label"></asp:Label>
    </td>
  </tr>
</table>
</asp:Content>
