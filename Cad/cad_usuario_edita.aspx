<%@ Page Title="" Language="VB" MasterPageFile="Cad_MasterPage.master" AutoEventWireup="false" CodeFile="cad_usuario_edita.aspx.vb" Inherits="cad_usuario_edita" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <br />
      <table class="style18" style="background-color: #EFF3FB;">
        <tr>
          <td id="td_Titulo">
            <asp:Label ID="lblTitulo" runat="server" Text="Edição de Cadastro de Usuário" ForeColor="Black"></asp:Label>
          </td>
        </tr>
        </table>
      <table width="100%">
        <tr>
          <td align="center" colspan="4">
            <asp:Label ID="labERRO" runat="server" CssClass="aviso_vermelho"></asp:Label>
          </td>
        </tr>
        <tr>
          <td align="left" colspan="2" style="background-color: #507CD1;" >
              <asp:Button ID="botCancela" runat="server" CausesValidation="False" 
                Text="Voltar" Width="80px" CssClass="button_t1" />
            </td>
          <td align="right" colspan="2" style="background-color: #507CD1; " >
              <asp:Button ID="btn_Salvar" runat="server" Text="Salvar" Width="80px" 
                CssClass="button_t1" />
            </td>
        </tr>
        <tr>
          <td class="label_coluna_r">ID Usuário:</td>
          <td class="style9">
            <asp:Label ID="lblID_Usuario" runat="server" Text="1"></asp:Label>
          </td>
          <td align="right" colspan="2" class="style16">Cadastrado em:
              <asp:Label ID="lblDt_Cadastro" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
          <td class="label_coluna_r">Nome Usuario:</td>
          <td class="style13">
            <asp:TextBox ID="txtNome_Usuario" runat="server" MaxLength="60" Width="377px" 
              CssClass="input_texto" TabIndex="1"></asp:TextBox>
            <asp:RegularExpressionValidator ID="rqrd_txt_nome_usuario" runat="server" 
              ControlToValidate="txtNome_Usuario" CssClass="aviso_campo_erro_p" 
              Display="Dynamic" ErrorMessage="Informe o nome e sobrenome do usuário." 
              SetFocusOnError="True" ValidationExpression="[a-zA-Z.\-' ]+ [a-zA-Z.\-']{3,15}"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rqrd_nome_usuario" runat="server" 
              ControlToValidate="txtNome_Usuario" CssClass="aviso_campo_erro_p" Display="None" 
              ErrorMessage="Informe o nome do usuário" SetFocusOnError="True"></asp:RequiredFieldValidator>
          </td>
          <td colspan="2" class="label_coluna_r">Ultima Alteração em:
              <asp:Label ID="lblDt_altera" runat="server"></asp:Label><br />
              por <asp:Label ID="lblNome_Usuario_altera" runat="server"></asp:Label><br />
            </td>
        </tr>
        <tr>
          <td class="label_coluna_r">CPF:<CPF:</td>
          <td class="style9">
            <asp:TextBox ID="txtCPF_usuario" runat="server" Width="206px" MaxLength="14" 
              CssClass="input_texto" TabIndex="1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rqrd_cpf_usuario" runat="server" 
              ControlToValidate="txtCPF_usuario" CssClass="aviso_campo_erro_p" 
              ErrorMessage="Informe o CPF do usuário."></asp:RequiredFieldValidator>
          </td>
          <td align="right">&nbsp;</td>
          <td align="left">
              &nbsp;</td>
        </tr>
        <tr>
          <td class="label_coluna_r">Permissão:</td>
          <td class="style9">
              <asp:DropDownList ID="ddlPermissao" runat="server" TabIndex="1">
                  <asp:ListItem Value="10">MASTER</asp:ListItem>
                  <asp:ListItem Value="12">EDIÇÂO</asp:ListItem>
                  <asp:ListItem Value="15">CONSULTA</asp:ListItem>
              </asp:DropDownList>
          </td>
          <td class="label_coluna_r">Situação:</td>
          <td align="left">
              <asp:DropDownList ID="ddlSts_Bloq" runat="server">
                  <asp:ListItem Value="S">Normal</asp:ListItem>
                  <asp:ListItem Value="B">Bloqueado</asp:ListItem>
                  <asp:ListItem Value="F">Falha no Cadastro</asp:ListItem>
              </asp:DropDownList>
            </td>
        </tr>
        <tr>
          <td class="label_coluna_r">Sistemas que pode acessar:</td>
          <td class="style11">
              SUS:<asp:DropDownList ID="ddlAcesso" runat="server" TabIndex="1">
                  <asp:ListItem Value="S">SIM</asp:ListItem>
                  <asp:ListItem Value="N">NÃO</asp:ListItem>
              </asp:DropDownList>
          </td>
          <td class="label_coluna_r" bgcolor="#FFFFCC">Login:</td>
          <td align="left" bgcolor="#FFFFCC">
            <asp:TextBox ID="txtLogin" runat="server" CssClass="input_texto" TabIndex="2"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rqrd_txt_login" runat="server" 
              ControlToValidate="txtLogin" Display="Dynamic" ErrorMessage="Informe o login." 
              SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
          <td class="label_coluna_r">Telefone:</td>
          <td class="style11" align="left">
            <asp:TextBox ID="txtTelefone" runat="server" Text='' Wrap="False" 
              CssClass="input_texto" TabIndex="1" />
          </td>
          <td class="label_coluna_r" bgcolor="#FFFFCC">Senha:</td>
          <td align="left" bgcolor="#FFFFCC">
              <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" 
                CssClass="input_texto" TabIndex="2"></asp:TextBox>
            </td>
        </tr>
        <tr>
          <td class="label_coluna_r">Email:</td>
          <td class="style9">
            <asp:TextBox ID="txtEmail" runat="server" Width="316px" CssClass="input_texto" 
              TabIndex="1"></asp:TextBox>
          </td>
          <td class="label_coluna_r" bgcolor="#FFFFCC">Confirmação Senha:</td>
          <td align="left" bgcolor="#FFFFCC">
              <asp:TextBox ID="txtSenha_Conf" runat="server" TextMode="Password" 
                CssClass="input_texto" TabIndex="2"></asp:TextBox>
            </td>
        </tr>
        <tr>
          <td align="right">&nbsp;</td>
          <td>
              &nbsp;</td>
          <td align="center" bgcolor="#FFFFCC" class="style19" colspan="2">- preencha e 
              confirme a senha para altera-la
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
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
</asp:Content>

