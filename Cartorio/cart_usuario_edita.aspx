<%@ Page Title="" Language="VB" MasterPageFile="cart_MasterPage.master" AutoEventWireup="false" CodeFile="cart_usuario_edita.aspx.vb" Inherits="cart_usuario_edita" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <br />
      <table class="style18" style="background-color: #EFF3FB;">
        <tr>
          <td id="td_Titulo">
            <asp:Label ID="lblTitulo" runat="server" Text="Edição de Cadastro de Usuário do Cartório" ForeColor="Black"></asp:Label>
          </td>
        </tr>
        </table>
      <table class="style1" width="100%">
        <tr>
          <td align="center" colspan="4">
            <asp:Label ID="labERRO" runat="server" ForeColor="#FF3300"></asp:Label>
          </td>
        </tr>
        <tr>
          <td align="left" colspan="2" style="background-color: #507CD1;" >
              <asp:Button ID="botCancela" runat="server" CausesValidation="False" Text="Voltar" Width="80px" />
            </td>
          <td align="right" colspan="2" style="background-color: #507CD1; " >
              <asp:Button ID="btn_Salvar" runat="server" Text="Salvar" Width="80px" />
            </td>
        </tr>
        <tr>
          <td align="right" class="style12">Nome Usuario:</td>
          <td class="style13">
            <asp:TextBox ID="txtNome_Usuario" runat="server" MaxLength="60" Width="377px"></asp:TextBox>
          </td>
          <td align="right" colspan="2" class="style16">Cadastrado em:
              <asp:Label ID="lblDt_Cadastro" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
          <td align="right">CPF:</td>
          <td class="style9">
            <asp:TextBox ID="txtCPF_usuario" runat="server" Width="206px"></asp:TextBox>
          </td>
          <td align="right">&nbsp;</td>
          <td align="left">
              &nbsp;</td>
        </tr>
        <tr>
          <td align="right">Permissão:</td>
          <td class="style9">
              <asp:DropDownList ID="ddlPermissao" runat="server">
                  <asp:ListItem Value="10">MASTER</asp:ListItem>
                  <asp:ListItem Value="12">EDIÇÂO</asp:ListItem>
                  <asp:ListItem Value="15">CONSULTA</asp:ListItem>
              </asp:DropDownList>
          </td>
          <td align="right" colspan="1" bgcolor="#FFFFCC">Login:</td>
          <td align="left" bgcolor="#FFFFCC">
            <asp:Label ID="lblID_Usuario" runat="server" Text="1"></asp:Label>
            </td>
        </tr>
        <tr>
          <td align="right">Situação:</td>
          <td align="left">
              <asp:DropDownList ID="ddlSts_Bloq" runat="server">
                  <asp:ListItem Value="S">Normal</asp:ListItem>
                  <asp:ListItem Value="B">Bloqueado</asp:ListItem>
              </asp:DropDownList>
            </td>
          <td align="right" colspan="1" bgcolor="#FFFFCC">Senha:</td>
          <td align="left" bgcolor="#FFFFCC">
              <asp:TextBox ID="txtSenha" runat="server" TextMode="Password"></asp:TextBox>
            </td>
          <td align="right"></td>
          <td class="style11" align="left">
          </td>
        </tr>
        <tr>
          <td align="right">Email:</td>
          <td class="style9">
            <asp:TextBox ID="txtEmail" runat="server" Width="316px"></asp:TextBox>
          </td>
          <td align="right" colspan="1" bgcolor="#FFFFCC">Confirmação Senha:</td>
          <td align="left" bgcolor="#FFFFCC">
              <asp:TextBox ID="txtSenha_Conf" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
          <td align="right">&nbsp;</td>
          <td>
              &nbsp;</td>
          <td align="center" bgcolor="#FFFFCC" class="style19" colspan="2">- preencha e 
              confirme a senha para altera-la</td>
        </tr>
        <tr class="style16">
          <td align="right" class="style20">Acessos:</td>
          <td colspan="3">
              <asp:Label ID="lblAcessos" runat="server" Text="0" CssClass="style20"></asp:Label>
            </td>
        </tr>
        </table>
  <br />
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
      .style1
      {
        width: 100%;
      }
      .style3
      {
        width: 505px;
      }
      .style6
      {
        font-weight: bold;
        font-size: medium;
        color: #FFFFFF;
      }
      .style9
      {
    width: 495px;
    font-size: large;
    border-left-color: #808080;
    border-right-color: #C0C0C0;
    border-top-color: #808080;
    border-bottom-color: #C0C0C0;
  }
      .style11
      {
    width: 495px;
  }
      .style12
      {
        height: 26px;
      }
    .style13
  {
    width: 495px;
    font-size: large;
    border-left-color: #808080;
    border-right-color: #C0C0C0;
    border-top-color: #808080;
    border-bottom-color: #C0C0C0;
    height: 26px;
  }
      .style16
      {            font-size: x-small;
        }
      .style17
      {
        height: 26px;
            font-size: x-small;
        }
      .style18
      {
        width: 907px;
      }
        .style19
        {
            color: #000099;
            font-size: x-small;
        }
        .style20
        {
            color: #000099;
        }
      </style>
    <link href="~/StyleSheet.css" rel="stylesheet" type="text/css" />
</asp:Content>

