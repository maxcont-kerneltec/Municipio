<%@ Page Language="VB" AutoEventWireup="false" CodeFile="cart_cadastro.aspx.vb" Inherits="cart_cadastro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Prefeitura de Barueri</title>
    <link href="~/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
  <form id="form1" runat="server">
  <div>
    <asp:Image ID="Image1" runat="server" ImageAlign="Left" ImageUrl="../img/logo_barueri.jpg" EnableViewState="False" />
    <table align="center" bgcolor="#EFF3FB" class="style4" style="background-color: #EFF3FB; border: thin solid #000000; font-size: x-small; width: 400px;">
        <tr>
          <td align="center" colspan="4">
            <asp:Label ID="labERRO" runat="server" ForeColor="#FF3300"></asp:Label>
          </td>
        </tr>
        <tr>
          <td align="left" colspan="2" >
              <asp:Button ID="botCancela" runat="server" CausesValidation="False" Text="Voltar" Width="80px" />
            </td>
          <td align="right" colspan="2" >
              <asp:Button ID="btn_Salvar" runat="server" Text="Salvar" Width="80px" />
            </td>
        </tr>
        <tr>
          <td align="left" class="style12" colspan="4">Cartório de Notas:</td>
        </tr>
        <tr>
          <td align="right" class="style12">Nome:</td>
          <td class="style13" colspan="3">
            <asp:TextBox ID="txtCartorio_Nome" runat="server" MaxLength="60" Width="500px"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td align="right" class="style12">CNPJ:</td>
          <td class="style13" colspan="3">
            <asp:TextBox ID="txtCartorio_CNPJ" runat="server" MaxLength="20" Width="153px"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            UF/Município:&nbsp;
            <asp:DropDownList ID="ddlUF_comp" runat="server" AutoPostBack="True" 
              DataSourceID="sqlUF_comp" DataTextField="UF" DataValueField="UF">
            </asp:DropDownList>
            /
            <asp:DropDownList ID="ddlMunicipio_comp" runat="server" 
              DataSourceID="sqlMunicipio_comp" DataTextField="xMun" DataValueField="cMun">
            </asp:DropDownList>
            <asp:SqlDataSource ID="sqlMunicipio_comp" runat="server" 
              ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
              SelectCommand="SELECT [cMun], [xMun] FROM [tbCad_Municipios] WHERE ([UF] = @UF) ORDER BY [xMun]">
              <SelectParameters>
                <asp:ControlParameter ControlID="ddlUF_comp" Name="UF" PropertyName="SelectedValue" Type="String" DefaultValue="SP" />
              </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="sqlUF_comp" runat="server" 
              ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
              SelectCommand="SELECT [UF] FROM [tbUF] WHERE ([UF] &lt;&gt; @UF) ORDER BY [UF]">
              <SelectParameters>
                <asp:Parameter DefaultValue="--" Name="UF" Type="String" />
              </SelectParameters>
            </asp:SqlDataSource>
          </td>
        </tr>
        <tr>
          <td align="right" class="style12" colspan="4">&nbsp;</td>
        </tr>
        <tr>
          <td align="left" class="style12" colspan="4">Usuário MASTER:</td>
        </tr>
        <tr>
          <td align="right" class="style12">Nome:</td>
          <td class="style13" colspan="3">
            <asp:TextBox ID="txtNome_Usuario" runat="server" MaxLength="60" Width="500px"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td align="right">CPF:</td>
          <td class="style9">
            <asp:TextBox ID="txtCPF_usuario" runat="server" Width="153px"></asp:TextBox>
          </td>
          <td align="right">&nbsp;</td>
          <td align="left">
              &nbsp;</td>
        </tr>
        <tr>
          <td align="right">Email:</td>
          <td class="style9" colspan="3">
            <asp:TextBox ID="txtEmail" runat="server" Width="500px"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td align="center" colspan="4" style="color: #FF0000; font-weight: bold">
          - Ao Salvar seus dados (Login e Senha) serão enviados para o seu email</td>
        </tr>
    </table>
  </div>
  </form>
</body>
</html>
