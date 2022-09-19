<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Prefeitura de Barueri</title>
<style type="text/css">
    .style_texto_clica
    {
      font-family: Arial, Helvetica, sans-serif;
      text-align: center;
      font-size: 14px;
      color: #0000FF;
      text-decoration: underline;
      cursor: pointer
    }
    
  </style>
</head>
<body>
  <form id="form1" runat="server">
    <div style="position: absolute">
      <asp:Image ID="Image1" runat="server" ImageAlign="Left" ImageUrl="img/logo_barueri.jpg" EnableViewState="False" />
    </div>
    <div style="position: absolute; top: 200px;">
      
      <table width="640" border="1" style="border: 1px solid #000000; border-collapse: collapse">
        <tr>
          <td height="35" colspan="2" style="font-family: Arial, Helvetica, sans-serif; font-size: 13px; font-weight: bold; background-color: #C0C0C0; text-align: center;">
            <span lang="pt-br">SELECIONE O ACESSO</span></td>
        </tr>

        <tr>
          <td height="60" class="style_texto_clica">
            <span onclick="window.location='cad/login_usuario.aspx'">Acesso ao cadastro de Cidadão</span>
          </td>
          <td class="style_texto_clica" height="60">
            <span onclick="window.location='login_cartorio.aspx'">Acesso ITBI e para Cartórios</span>
          </td>
        </tr>

        <tr>
          <td height="60" class="style_texto_clica">
            <span onclick="window.location='requerimento/termo_enquadra.aspx'">Acessar Termo de Enquadramento</span>
          </td>
          <td class="style_texto_clica" height="60">
            <span onclick="window.location='login_usuario.aspx?SIS_=1'">Prefeito no seu Bairro</span>
          </td>
        </tr>
      </table>
    </div>

  </form>
</body>
</html>
