<%@ Page Title="" Language="VB" MasterPageFile="Cad_MasterPage.master" AutoEventWireup="false" CodeFile="rel_endereco_conta_cidadao.aspx.vb" Inherits="rel_endereco_conta_cidadao" %>

<%@ Register assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.DynamicData" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <link href="inc/StyleSheet.css" rel="stylesheet" type="text/css" />
<style type="text/css">
  .style6
  {
    width: 286px;
  }
  </style>
<script type ="text/javascript">
  function btn_visualza_end_cidadao(num_instalacao) {
    window.location = 'rel_endereco_cidadao.aspx?num_instalacao=' + num_instalacao + '&tpVisualiza=COMP'
  }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
<Br /><br />
      <asp:SqlDataSource ID="sql_sp1Rel_Endereco_Conta_Cidadao" runat="server" 
        ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
        SelectCommand="sp1Rel_Endereco_Conta_Cidadao" 
        SelectCommandType="StoredProcedure">
        <SelectParameters>
          <asp:SessionParameter Name="sp_id_municipio" SessionField="id_municipio" 
            Type="String" />
        </SelectParameters>
      </asp:SqlDataSource>
    </div>
    <div id="dv_resultado">
      <asp:GridView ID="GridView_Enderecos" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" DataSourceID="sql_sp1Rel_Endereco_Conta_Cidadao" 
        EmptyDataText="Nenhum Registro Localizado" Width="100%" 
        PageSize="50" Font-Size="Small" DataKeyNames="id_municipio">
        <RowStyle BackColor="#99CCFF" />
    <EmptyDataRowStyle BackColor="#0099CC" />
        <Columns>
          <asp:TemplateField HeaderText="Instalação" SortExpression="num_instalacao">
            <ItemTemplate>
              <input id="Button1" type="button" onclick="btn_visualza_end_cidadao('<%# eval("num_instalacao") %>')" value="<%# eval("num_instalacao") %>" class="button_tb" style="width: 85px" />
            </ItemTemplate>
            <ControlStyle Width="75px" />
            <ItemStyle HorizontalAlign="Center" Width="75px" />
          </asp:TemplateField>
          <asp:BoundField DataField="conta" HeaderText="Conta" ReadOnly="True" 
            SortExpression="conta" >
          <ItemStyle HorizontalAlign="Center" Width="60px" />
          </asp:BoundField>
          <asp:BoundField DataField="xLgr" HeaderText="Logradouro" 
            SortExpression="xLgr" />
          <asp:BoundField DataField="nro" HeaderText="Número" SortExpression="nro" />
          <asp:BoundField DataField="xCpl" HeaderText="Complemento" 
            SortExpression="xCpl" />
          <asp:BoundField DataField="xBairro" HeaderText="Bairro" 
            SortExpression="xBairro" />
        </Columns>
        <HeaderStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
      </asp:GridView>
    </div>
    <br />
    </asp:Content>
