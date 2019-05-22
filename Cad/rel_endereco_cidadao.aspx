<%@ Page Language="VB" AutoEventWireup="false" CodeFile="rel_endereco_cidadao.aspx.vb" Inherits="Rel_Endereco_Cidadao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Prefeitura Municipal de Barueri</title>
    <link href="inc/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
      .style1
      {
        height: 81px;
      }
      .style2
      {
        text-align: left;
      }
      .style3
      {
        height: 40px;
        text-align: center;
      }
      .style4
      {
        width: 194px;
        text-align: right;
      }
      .style5
      {
        text-align: center;
      }
    </style>
<script type ="text/javascript">
  function btn_imprimir_onclick(id_cidadao) {
    window.open('cad_cidadao_imprime.aspx?tpImp=COMP&id_cidadao=' + id_cidadao, 'cad_cidadao', 'toolbar=yes, resizable=yes, scrollbars=yes, width=670px, height=500px')
    return false;
  }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <table width="95%" align="center" border="0" 
        style="border-collapse: collapse;" cellpadding="5" 
        cellspacing="0">
        <tr>
          <td align="center" height="90" valign="middle" style="padding: 15px">
            <asp:Image ID="Image1" runat="server" ImageUrl="../img/logo_barueri.jpg" EnableViewState="False" />
          </td>
        </tr>
        <tr>
          <td class="style3">
            <span lang="pt-br">
      <table width="95%" align="center" border="1" 
        style="border: 1px solid #000000; border-collapse: collapse;" cellpadding="5" 
        cellspacing="0">
        <tr>
          <td class="style5" colspan="2">
            <span lang="pt-br">Informações da Instalação
            <asp:Label ID="lbl_num_instala" runat="server" Text="Label"></asp:Label>
            </span>
          </td>
        </tr>
        <tr>
          <td class="style4">Logradouro, nro:</td>
          <td class="style2"><asp:Label ID="lbl_logradouro" runat="server" Text="Label"></asp:Label>
            , &nbsp;<asp:Label ID="lbl_nro" runat="server" Text="Label"></asp:Label>
&nbsp;&nbsp;&nbsp; Complemento:
            <asp:Label ID="lbl_complemento" runat="server" Text="Label"></asp:Label>
          </td>
        </tr>
        <tr>
          <td class="style4">Bairro: </td>
          <td class="style2">
            <asp:Label ID="lbl_bairro" runat="server" Text="Label"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp; CEP:
            <asp:Label ID="lbl_cep" runat="server" Text="Label"></asp:Label>
          </td>
        </tr>
        <tr>
          <td class="style4">Município / UF:</td>
          <td class="style2">
            <asp:Label ID="lbl_xMun" runat="server" Text="Label"></asp:Label>
&nbsp;/
            <asp:Label ID="lbl_UF" runat="server" Text="Label"></asp:Label>
          </td>
        </tr>
        <tr>
          <td class="style4">Num. Inscr. IPTU:</td>
          <td class="style2">
            <asp:Label ID="lbl_num_inscr_imovel" runat="server" Text="Label"></asp:Label>
            &nbsp;&nbsp;&nbsp;
          </td>
        </tr>
        <tr>
          <td class="style4">
            <span lang="pt-br">
            Num. Matrícula Imóvel:</span></td>
          <td class="style2">
            <span lang="pt-br">
            <asp:Label ID="lbl_num_matri_imovel" runat="server" Text="Label"></asp:Label>
            </span>
          </td>
        </tr>
       </table>
      <br />
      <br />
      <asp:Panel ID="pnl_acao" runat="server" Visible="False">
        <div align="center">
          <asp:Button ID="btn_voltar" runat="server" Text="Voltar" class="button_tb" />
        </div>
      </asp:Panel>
      <br />
      <br />
            <asp:SqlDataSource ID="sql_endereco_detalhes" runat="server" 
              ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" SelectCommand="SELECT A.num_instalacao, A.CPF, A.num_cliente, A.xLgr, A.nro, A.xCpl, A.xBairro, A.CEP, A.num_inscr_imovel, A.num_matri_imovel,
       B.xMun, B.UF
FROM dbo.Cad_Cidadao_End AS A
INNER JOIN dbo.tbCad_Municipios AS B ON (B.cMun = A.cMun)
WHERE (A.id_municipio = @id_municipio) AND (A.num_instalacao = @num_instalacao)">
              <SelectParameters>
                <asp:SessionParameter Name="id_municipio" SessionField="id_municipio" />
                <asp:QueryStringParameter Name="num_instalacao" 
                  QueryStringField="num_instalacao" />
              </SelectParameters>
            </asp:SqlDataSource>
            </span>
          </td>
        </tr>
        <tr>
          <td align="right" class="style1">
            <asp:SqlDataSource ID="sql_endereco_pega" runat="server" 
              ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
              
              
              SelectCommand="SELECT id_cidadao, SUS_num, nome_registro, RG, CASE isnull(CPF , '0') WHEN '0' THEN dbo.fFormata_Le(CPF_resp , 'F') + ' (Resp.)' ELSE dbo.fFormata_Le(CPF , 'F') END AS CPF, nome_mae, nome_pai FROM Cad_Cidadao WHERE (id_municipio = @id_municipio) AND (num_instalacao = @num_instalacao)">
              <SelectParameters>
                <asp:SessionParameter Name="id_municipio" SessionField="id_municipio" />
                <asp:QueryStringParameter Name="num_instalacao" 
                  QueryStringField="num_instalacao" />
              </SelectParameters>
            </asp:SqlDataSource>
            <div class="style2">
            <asp:GridView ID="grd_endereco_cidadao" runat="server" 
              AutoGenerateColumns="False" DataSourceID="sql_endereco_pega" Width="100%">
              <Columns>
                <asp:TemplateField HeaderText="ID Cidadão" SortExpression="id_cidadao">
                  <ItemTemplate>
                    <input id="Button1" onclick="btn_imprimir_onclick('<%# eval("id_cidadao") %>')" type="button" value="<%# eval("id_cidadao") %>" class="button_tb" style="width: 75px" />
                  </ItemTemplate>
                  <ItemStyle Height="22px" />
                </asp:TemplateField>
                <asp:BoundField DataField="SUS_num" HeaderText="SUS" 
                  SortExpression="SUS_num" >
                </asp:BoundField>
                <asp:BoundField DataField="nome_registro" HeaderText="Nome" 
                  SortExpression="nome_registro" >
                </asp:BoundField>
                <asp:BoundField DataField="RG" HeaderText="RG" 
                  SortExpression="RG" >
                </asp:BoundField>
                <asp:BoundField DataField="CPF" HeaderText="CPF" ReadOnly="True" 
                  SortExpression="CPF" >
                </asp:BoundField>
                <asp:BoundField DataField="nome_mae" HeaderText="Mãe" 
                  SortExpression="nome_mae" />
                <asp:BoundField DataField="nome_pai" HeaderText="Pai" 
                  SortExpression="nome_pai" />
              </Columns>
              <HeaderStyle BackColor="#99CCFF" />
              <AlternatingRowStyle BackColor="#99CCFF" Width="100%" />
            </asp:GridView>
            </div>
          </td>
        </tr>
      </table>

    </div>

    </form>
</body>
</html>