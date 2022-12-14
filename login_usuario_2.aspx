<%@ Page Language="VB" AutoEventWireup="false" CodeFile="login_usuario_2.aspx.vb" Inherits="login_usuario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Sistema de Atendimento ao Cidadão</title>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
  <form id="form1" runat="server">
  <div>
    <table align="center" bgcolor="#EFF3FB" class="style4" style="background-color: #EFF3FB; border: thin solid #000000; font-size: x-small; width: 400px;">
      <tr>
        <td align="center" bgcolor="#507CD1" colspan="2" style="font-size: small; color: #FFFFFF; font-weight: bold">
          Acesso Usuário</td>
      </tr>
      <tr>
        <td align="center" colspan="2">
          Autenticação de usuário</td>
      </tr>
      <tr>
        <td align="right" style="font-size: small">
          <span lang="pt-br">Município:</span></td>
        <td valign="middle">
          <asp:DropDownList ID="drp_municipios" runat="server">
            <asp:ListItem Value="10">Barueri</asp:ListItem>
            <asp:ListItem Value="9999">Município de Teste</asp:ListItem>
          </asp:DropDownList>
        </td>
      </tr>
      <tr>
        <td align="right" style="font-size: small">
          Login:</td>
        <td valign="middle">
          <asp:TextBox ID="txtLogin" runat="server" MaxLength="20" Width="150px"></asp:TextBox>
&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ErrorMessage="(*) Digite seu login" ControlToValidate="txtLogin"></asp:RequiredFieldValidator>
        </td>
      </tr>
      <tr>
        <td align="right" style="font-size: small">
          Senha:</td>
        <td>
          <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="(*) Digite sua senha" ControlToValidate="txtSenha"></asp:RequiredFieldValidator>
        </td>
      </tr>
      <tr>
        <td>
          &nbsp;</td>
        <td>
          <asp:Button ID="btLogin" runat="server" Text="OK" class Width="60px" CssClass="button_tb" />
        </td>
      </tr>
      <tr>
        <td align="center" colspan="2">
          <asp:Label ID="errorLabel" runat="server" EnableViewState="False" 
            Font-Bold="True" ForeColor="Red" Text="RESULTADO LOGIN"></asp:Label>
        </td>
      </tr>
    </table>
  </div>
  </form>
</body>
</html>
