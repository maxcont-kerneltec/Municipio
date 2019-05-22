<%@ Page Language="VB" AutoEventWireup="false" CodeFile="login_usuario.aspx.vb" Inherits="login_usuario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Prefeitura de Barueri</title>
  <link href="inc/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
  <form id="form1" runat="server">
  <div>
    <asp:Image ID="Image1" runat="server" ImageAlign="Left" ImageUrl="img/logo_barueri.jpg" EnableViewState="False" />
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
        <td class="label_coluna_r">Sistema:</td>
        <td>
          <asp:DropDownList ID="drp_sel_sistema" runat="server">
            <asp:ListItem Value="0">Cadastro de Cidadao</asp:ListItem>
            <asp:ListItem Value="1">Prefeito no seu bairro</asp:ListItem>
          </asp:DropDownList>
        </td>
      </tr>
      <tr>
        <td class="label_coluna_r">
          Login:</td>
        <td valign="middle">
          <asp:TextBox ID="txtLogin" runat="server" MaxLength="20" Width="150px" 
            CssClass="input_texto"></asp:TextBox>
&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ErrorMessage="(*) Digite seu login" ControlToValidate="txtLogin" 
            CssClass="aviso_vermelho_p"></asp:RequiredFieldValidator>
        </td>
      </tr>
      <tr>
        <td class="label_coluna_r">
          Senha:</td>
        <td>
          <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" Width="150px" 
            CssClass="input_texto"></asp:TextBox>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ErrorMessage="(*) Digite sua senha" ControlToValidate="txtSenha" 
            CssClass="aviso_vermelho_p"></asp:RequiredFieldValidator>
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
            Text="RESULTADO LOGIN" 
            CssClass="aviso_vermelho_p"></asp:Label>
        </td>
      </tr>
    </table>
  </div>
  </form>
</body>
</html>
