<%@ Page Title="" Language="VB" MasterPageFile="Cad_MasterPage.master" AutoEventWireup="false" CodeFile="cad_inicio.aspx.vb" Inherits="cad_inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <style type="text/css">
    .style9
    {
      text-align: left;
      width: 200px;
    }
    .style10
    {
      text-align: right;
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
    <br />
<table style="border: 1px solid #000000; width:100%; border-collapse: collapse;" 
      bgcolor="#DFDFDF" cellpadding="2" cellspacing="1" border="1">
  <tr>
    <td class="style10" width="25%">Nome:</td>
    <td class="style7" width="25%">
      <asp:Label ID="lbl_nome" runat="server" Text="Label"></asp:Label>
    </td>
    <td class="style10" width="25%">CPF:</td>
    <td class="style8" width="25%">
      <asp:Label ID="lbl_CPF" runat="server" Text="Label"></asp:Label>
    </td>
  </tr>
  <tr>
    <td class="style10">
      Último Acesso:
      </td>
    <td class="style7">
      <asp:Label ID="lbl_ult_acesso" runat="server" Text="Label"></asp:Label>
    </td>
    <td class="style10">
      Número de Acessos:
      </td>
    <td class="style8">
      <asp:Label ID="lbl_conta_acessos" runat="server" Text="Label"></asp:Label>
    </td>
  </tr>
  <tr>
    <td class="style10">
      Telefone:
      </td>
    <td class="style7">
      <asp:Label ID="lbl_telefone" runat="server" Text="Label"></asp:Label>&nbsp;
    </td>
    <td class="style10">
      E-mail:
      </td>
    <td class="style8">
      <asp:Label ID="lbl_email" runat="server" Text="Label"></asp:Label>
    </td>
  </tr>
  </table>
<br /><br />

<table style="border: 1px solid #000000; width:100%; border-collapse: collapse; border-spacing: 1px;" 
      bgcolor="#DFDFDF" cellpadding="2" cellspacing="1" border="1">
  <tr>
    <td colspan="4" align="center">Estatísticas</td>
  </tr>

  <tr>
    <td class="style10" width="25%">Cidadãos Cadastrados:</td>
    <td class="style9" width="25%">
      <asp:Label ID="lbl_cidadao" runat="server" Text="Label"></asp:Label>
      &nbsp;&nbsp;
      <asp:Button ID="btn_consulta_cidadaos" runat="server" CssClass="button_claro" 
        Text="Consultar" BorderWidth="1px" />
    </td>
    <td width="25%" align="right">Eleitores do Município:</td>
    <td width="25%">
      <asp:Label ID="lbl_conta_eleitores_municipio" runat="server" Text="Label"></asp:Label>
    &nbsp;(<asp:Label ID="lbl_perc_eleitor_municipio" runat="server" Text="Label"></asp:Label>
      %)
    </td>
  </tr>

  <tr>
    <td class="style10">
      Moradores fora do município:
      </td>
    <td class="style9">
      <asp:Label ID="lbl_conta_cidadao_fora" runat="server" Text="Label"></asp:Label>
    &nbsp;(<asp:Label ID="lbl_perc_morador_fora" runat="server" Text="Label"></asp:Label>
      %)</td>
    <td class="style10">
      Eleitores fora do município:
      </td>
    <td class="style8">
      <asp:Label ID="lbl_conta_eleitores_fora" runat="server" Text="Label"></asp:Label>
    &nbsp;(<asp:Label ID="lbl_perc_eleitor_fora" runat="server" Text="Label"></asp:Label>
      %)</td>
  </tr>

  <tr>
    <td class="style10">
      Deficientes:
      </td>
    <td class="style9">
      <asp:Label ID="lbl_conta_deficientes" runat="server" Text="Label"></asp:Label>
    &nbsp;(<asp:Label ID="lbl_perc_deficientes" runat="server" Text="Label"></asp:Label>
      %)
    </td>
    <td class="style8">
      &nbsp;</td>
    <td class="style8">
      &nbsp;</td>
  </tr>

  <tr>
    <td class="style10">
      Endereços Cadastrados:
      </td>
    <td class="style9">
      <asp:Label ID="lbl_localizacao" runat="server" Text="Label"></asp:Label>
      &nbsp;&nbsp;
      <asp:Button ID="btn_consultar_instalacao" runat="server" CssClass="button_claro" 
        Text="Consultar" BorderWidth="1px" />
    </td>
    <td class="style8">&nbsp;</td>
    <td class="style8">&nbsp;</td>
  </tr>
</table>
</asp:Content>
