<%@ Page Title="" Language="VB" MasterPageFile="cart_MasterPage.master" AutoEventWireup="false" CodeFile="cart_guias_edita.aspx.vb" Inherits="cart_guias_edita" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <br />
      <table class="style18" style="background-color: #EFF3FB;">
        <tr>
          <td id="td_Titulo">
            <asp:Label ID="lblTitulo" runat="server" Text="Geração de guia de ITBI-e" ForeColor="Black"></asp:Label>
          </td>
        </tr>
        </table>
      <table class="style1" width="100%">
        <tr>
          <td align="left" colspan="2" style="background-color: #507CD1;" width="50%">
              <asp:Button ID="botCancela" runat="server" CausesValidation="False" Text="Voltar" Width="80px" />
            </td>
          <td align="right" colspan="2" style="background-color: #507CD1; "  width="50%">
              <asp:Button ID="btn_Salvar" runat="server" Text="Salvar" Width="80px" />
            </td>
        </tr>
        <tr>
          <td align="right" width="20%" bgcolor="#CCFFFF">Cartório: </td>
          <td align="left" width="30%" bgcolor="#CCFFFF"><asp:Label ID="lblCartorio" runat="server" ForeColor="#FF3300"></asp:Label></td>
          <td align="right" width="15%" bgcolor="#CCFFFF">CNPJ: </td>
          <td align="left" width="35%" bgcolor="#CCFFFF"><asp:Label ID="lblCartorio_CNPJ" runat="server" ForeColor="#FF3300"></asp:Label></td>
        </tr>
        <tr>
          <td align="right" width="20%" bgcolor="#CCFFFF">Natureza da Transação: </td>
          <td align="left" width="30%" bgcolor="#CCFFFF"><asp:Label ID="Label1" runat="server" ForeColor="#FF3300"></asp:Label>
            <asp:DropDownList ID="DropDownList1" runat="server" 
              DataSourceID="SqlDataSource1" DataTextField="descr" 
              DataValueField="cod_natureza">
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
              ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
              SelectCommand="SELECT [cod_natureza], [descr] FROM [Cartorio_ITBI_Natureza] ORDER BY [descr]">
            </asp:SqlDataSource>
          </td>
          <td align="right" width="15%" bgcolor="#CCFFFF">Município/UF: </td>
          <td align="left" width="35%" bgcolor="#CCFFFF"><asp:Label ID="lblCartorio_Municipio" runat="server" ForeColor="#FF3300"></asp:Label></td>
        </tr>
        <tr>
          <td align="right" width="20%" bgcolor="#CCFFFF" colspan="2">&nbsp;</td>
          <td align="right" width="15%" bgcolor="#CCFFFF">Responsável: </td>
          <td align="left" width="35%" bgcolor="#CCFFFF"><asp:Label ID="lblCartorio_Responsavel" runat="server" ForeColor="#FF3300"></asp:Label></td>
        </tr>
        <tr>
          <td align="right" width="20%">INSCRIÇÃO CADASTRAL: </td>
          <td align="left" colspan="3">
            <asp:TextBox ID="txtInscricao_Cadastral" runat="server" MaxLength="20" 
              Width="180px"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="botConsulta" runat="server" Text="Consultar" Width="80px" />
            &nbsp;&nbsp;&nbsp;
            <asp:Label ID="labERRO" runat="server" ForeColor="#FF3300"></asp:Label>
          </td>
        </tr>
        <tr>
          <td align="right" width="20%" bgcolor="#CCFFFF">Inscrição Anterior: </td>
          <td align="left" width="30%" bgcolor="#CCFFFF"><asp:Label ID="lblInsc_Anterior" runat="server" ForeColor="#FF3300"></asp:Label></td>
          <td align="right" width="15%" bgcolor="#CCFFFF">Valor Venal: </td>
          <td align="left" width="35%" bgcolor="#CCFFFF"><asp:Label ID="lblVal_Venal" runat="server" ForeColor="#FF3300"></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#CCFFFF">Quadra: </td>
          <td align="left" colspan="3" bgcolor="#CCFFFF">
             <asp:Label ID="lblQuadra" runat="server" ForeColor="#FF3300"></asp:Label>&nbsp;&nbsp;&nbsp;
             Lote: <asp:Label ID="lblLote" runat="server" ForeColor="#FF3300"></asp:Label>
          </td>
        </tr>
        <tr>
          <td align="right" bgcolor="#CCFFFF" class="style21">Proprietário: </td>
          <td align="left" colspan="3" bgcolor="#CCFFFF" class="style21"><asp:Label ID="lblProprietario" runat="server" ForeColor="#FF3300"></asp:Label><br /></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#CCFFFF">Endereço: </td>
          <td align="left" colspan="3" bgcolor="#CCFFFF">
            <asp:Label ID="lblEndereco1" runat="server" ForeColor="#FF3300"></asp:Label><br />
            <asp:Label ID="lblEndereco2" runat="server" ForeColor="#FF3300"></asp:Label>
          </td>
        </tr>
        <tr>
          <td align="right">Valor Parte Financiada: </td>
          <td align="left">
            <asp:TextBox ID="txtVal_Financ" runat="server" MaxLength="20" Width="180px"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            Alíquota:&nbsp;
            <asp:TextBox ID="txtAliq_Financ" runat="server" MaxLength="5" Width="40px"></asp:TextBox> %
          </td>
          <td align="right" width="15%" bgcolor="#CCFFFF">Valor do Instrumento: </td>
          <td align="left" width="35%" bgcolor="#CCFFFF"><asp:Label ID="lblValor_Instrumento" runat="server" ForeColor="#FF3300"></asp:Label></td>
        </tr>
        <tr>
          <td align="right">Valor Parte Não Financiada: </td>
          <td align="left">
            <asp:TextBox ID="txtVal_NAOFinanc" runat="server" MaxLength="20" Width="180px"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            Alíquota:&nbsp;
            <asp:TextBox ID="txtAliq_NAOFinanc" runat="server" MaxLength="5" Width="40px"></asp:TextBox> %
          </td>
          <td align="right" width="15%" bgcolor="#CCFFFF">Valor do Tributo: </td>
          <td align="left" width="35%" bgcolor="#CCFFFF"><asp:Label ID="lblValor_Tributo" runat="server" ForeColor="#FF3300"></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#CCFFFF">CONTRIBUINTE: </td>
          <td align="left" colspan="3" bgcolor="#CCFFFF">&nbsp;</td>
        </tr>
        <tr>
          <td align="right">Nome: </td>
          <td align="left"><asp:TextBox ID="lblNome_comp" runat="server" MaxLength="20" Width="300px"></asp:TextBox></td>
          <td align="right">CPF ou CNPJ: </td>
          <td align="left"><asp:TextBox ID="lbCpf_comp" runat="server" MaxLength="20" Width="180px"></asp:TextBox></td>
        </tr>
        <tr>
          <td align="right">Endereço: </td>
          <td align="left"><asp:TextBox ID="lblEnd_comp" runat="server" MaxLength="20" Width="300px"></asp:TextBox></td>
          <td align="right">UF/Municipio: </td>
          <td align="left">
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
      .style18
      {
        width: 907px;
      }
        .style21
      {
        height: 20px;
      }
      </style>
    <link href="~/StyleSheet.css" rel="stylesheet" type="text/css" />
</asp:Content>

