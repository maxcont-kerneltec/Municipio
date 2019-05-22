<%@ Page Title="" Language="VB" MasterPageFile="Cad_MasterPage.master" AutoEventWireup="false" CodeFile="cad_unidade.aspx.vb" Inherits="cad_unidade" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    CADASTRO UNIDADE
  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
      CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" 
      GridLines="None">
    <RowStyle BackColor="#EFF3FB" />
    <Columns>
      <asp:BoundField DataField="id_unidade" HeaderText="id_unidade" 
        SortExpression="id_unidade" />
      <asp:BoundField DataField="nome" HeaderText="nome" SortExpression="nome" />
      <asp:BoundField DataField="xLgr" HeaderText="xLgr" SortExpression="xLgr" />
      <asp:BoundField DataField="nro" HeaderText="nro" SortExpression="nro" />
      <asp:BoundField DataField="xCpl" HeaderText="xCpl" SortExpression="xCpl" />
      <asp:BoundField DataField="xBairro" HeaderText="xBairro" 
        SortExpression="xBairro" />
      <asp:BoundField DataField="CEP" HeaderText="CEP" SortExpression="CEP" />
      <asp:BoundField DataField="telefone" HeaderText="telefone" 
        SortExpression="telefone" />
      <asp:BoundField DataField="contato" HeaderText="contato" 
        SortExpression="contato" />
      <asp:BoundField DataField="cMun" HeaderText="cMun" SortExpression="cMun" />
      <asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />
      <asp:BoundField DataField="horario" HeaderText="horario" 
        SortExpression="horario" />
    </Columns>
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <EditRowStyle BackColor="#2461BF" />
    <AlternatingRowStyle BackColor="White" />
  </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
      ConnectionString="<%$ ConnectionStrings:municipioConnectionString %>" 
      SelectCommand="SELECT [nome], [xLgr], [nro], [xCpl], [xBairro], [CEP], [telefone], [contato], [cMun], [email], [horario], [id_unidade] FROM [Atend_Unidades]">
    </asp:SqlDataSource>
</asp:Content>
