<%@ Page Title="" Language="VB" MasterPageFile="cart_MasterPage.master" AutoEventWireup="false" CodeFile="cart_guias.aspx.vb" Inherits="cart_guias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="~/StyleSheet.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
      ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
      SelectCommand="sp3Cartorio_Boletos_Pega" 
      SelectCommandType="StoredProcedure">
      <SelectParameters>
        <asp:SessionParameter Name="id_municipio" SessionField="id_municipio" Type="Int32" />
        <asp:SessionParameter Name="id_cartorio" SessionField="id_cartorio" Type="Int32" />
        <asp:ControlParameter ControlID="ddlSTS_Cart" Name="STS_Cart" PropertyName="SelectedValue" Type="Int32" DefaultValue="0" />
        <asp:ControlParameter ControlID="ddlSTS_Pagam" DefaultValue="0" Name="STS_Pagam" PropertyName="SelectedValue" Type="Int32" />
        <asp:ControlParameter ControlID="txt_nome_pesq" DefaultValue=" " 
          Name="spFiltro_Nome" PropertyName="Text" Type="String" />
      </SelectParameters>
    </asp:SqlDataSource>
    <br />
  <table style="width:100%;" bgcolor="#66CCFF">
  <tr>
    <td class="style8">Nome:</td>
    <td class="style9">
      <asp:TextBox ID="txt_nome_pesq" runat="server" Width="295px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
      Situação:&nbsp;
      <asp:DropDownList ID="ddlSTS_Cart" runat="server">
         <asp:ListItem Value="100">Todas</asp:ListItem>
         <asp:ListItem Value="0">Emitidas</asp:ListItem>
         <asp:ListItem Value="1">Lavradas</asp:ListItem>
         <asp:ListItem Value="2">Registradas</asp:ListItem>
         <asp:ListItem Value="3">Homologadas</asp:ListItem>
         <asp:ListItem Value="9">Canceladas</asp:ListItem>
      </asp:DropDownList>
      &nbsp;&nbsp;&nbsp;
      Pagamento:&nbsp;
      <asp:DropDownList ID="ddlSTS_Pagam" runat="server">
         <asp:ListItem Value="100">Todas</asp:ListItem>
         <asp:ListItem Value="0">Não pagas</asp:ListItem>
         <asp:ListItem Value="1">Pagas</asp:ListItem>
      </asp:DropDownList>
      &nbsp;&nbsp;&nbsp;
      <asp:Button ID="botPesquisa" runat="server" Text="Pesquisar"  Width="146px" CssClass="button_tb" />
    &nbsp;
    </td>
    <td>&nbsp;</td>
  </tr>
  </table>
    <br />
    <asp:Button ID="botNovo" runat="server" CssClass="button_tb" Text="Nova" />&lt;-- clique aqui para gerar nova guia    <br />
    <br />
    <asp:GridView ID="GridView_Guias" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" 
      EmptyDataText="Nenhuma guia localizada" Width="100%" PageSize="50">
      <RowStyle BackColor="#99CCFF" />
      <EmptyDataRowStyle BackColor="#0099CC" />
      <Columns>
      <asp:TemplateField HeaderText="Guia" SortExpression="id_guia">
          <EditItemTemplate>
              <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("id_usuario") %>' Width="80px"></asp:TextBox>
          </EditItemTemplate>
          <ItemTemplate>
              <asp:Button ID="Button4" runat="server" CommandArgument='<%# Bind("id_guia") %>' oncommand="Acessa_Cadastro" 
                          Text='<%# Bind("id_guia") %>' UseSubmitBehavior="False" Width="80px" CssClass="button_tb" />
          </ItemTemplate>
          <ItemStyle Width="83px" HorizontalAlign="Center" />
        </asp:TemplateField>
    <asp:BoundField DataField="comp_nome" HeaderText="Nome" 
          SortExpression="comp_nome" >
      </asp:BoundField>
    <asp:BoundField DataField="val_tributo" HeaderText="Valor" 
          SortExpression="val_tributo" DataFormatString="{0:c}" >
        <HeaderStyle HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
      </asp:BoundField>
    <asp:BoundField DataField="dt_vecto" HeaderText="Data Vencimento" 
          SortExpression="dt_vecto" DataFormatString="{0:dd/MM/yyyy}" >
        <ItemStyle HorizontalAlign="Center" />
      </asp:BoundField>
        <asp:BoundField DataField="sts_cartorio" HeaderText="Situação" 
          SortExpression="sts_cartorio" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="sts_pagam" HeaderText="Pagamento" 
          SortExpression="sts_pagam" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
  </Columns>
      <HeaderStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
      <AlternatingRowStyle BackColor="White" />
</asp:GridView>
    <br />
</asp:Content>
