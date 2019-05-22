<%@ Page Title="" Language="VB" MasterPageFile="cart_MasterPage.master" AutoEventWireup="false" CodeFile="cart_usuario.aspx.vb" Inherits="cart_usuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
        
      SelectCommand="SELECT [id_usuario], [nome], dbo.fFormata_Le([id_cpf],'F') as cpf_usuario, [id_usuario] as login, [permissao], [conta], [dt_ultimo], [sts_bloq] FROM [Cartorio_Usuario] WHERE (([id_municipio] = @id_municipio) AND ([id_cartorio] = @id_cartorio) AND ((rtrim(@nome_usuario) = '') OR (@nome_usuario is null) OR ((@nome_usuario &lt;&gt; '') AND ([nome] LIKE '%' + @nome_usuario + '%'))))">
      <SelectParameters>
        <asp:SessionParameter Name="id_municipio" SessionField="id_municipio" 
          Type="Int32" DefaultValue="10" />
        <asp:SessionParameter DefaultValue="0" Name="id_cartorio" 
          SessionField="id_cartorio" />
        <asp:ControlParameter ControlID="txt_nome_pesq" Name="nome_usuario" PropertyName="Text" Type="String" DefaultValue=" " />
      </SelectParameters>
      </asp:SqlDataSource>
    <br />
  <table style="width:100%;" bgcolor="#66CCFF">
  <tr>
    <td class="style8">Nome:</td>
    <td class="style9">
      <asp:TextBox ID="txt_nome_pesq" runat="server" Width="295px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
      <asp:Button ID="botPesquisa" runat="server" Text="Pesquisar"  Width="146px" CssClass="button_tb" />
    </td>
    <td>&nbsp;</td>
  </tr>
  </table>
    <br />
    <asp:Button ID="botNovo" runat="server" CssClass="button_tb" Text="Novo" />&lt;-- clique aqui para cadastrar novo usuário    <br />
    <br />
    <asp:GridView ID="GridView_Usuarios" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" 
      EmptyDataText="Nenhum Registro Localizado" Width="100%" PageSize="50">
      <RowStyle BackColor="#99CCFF" />
      <EmptyDataRowStyle BackColor="#0099CC" />
      <Columns>
      <asp:TemplateField HeaderText="Login" SortExpression="id_usuario">
          <EditItemTemplate>
              <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("id_usuario") %>' Width="80px"></asp:TextBox>
          </EditItemTemplate>
          <ItemTemplate>
              <asp:Button ID="Button4" runat="server" CommandArgument='<%# Bind("id_usuario") %>' oncommand="Acessa_Cadastro" 
                          Text='<%# Bind("id_usuario") %>' UseSubmitBehavior="False" Width="80px" CssClass="button_tb" />
          </ItemTemplate>
          <ItemStyle Width="83px" HorizontalAlign="Center" />
        </asp:TemplateField>
    <asp:BoundField DataField="nome" HeaderText="nome" 
          SortExpression="nome" >
      </asp:BoundField>
    <asp:BoundField DataField="cpf_usuario" HeaderText="cpf_usuario" 
          SortExpression="cpf_usuario" ReadOnly="True" >
      </asp:BoundField>
    <asp:BoundField DataField="permissao" HeaderText="permissao" 
          SortExpression="permissao" >
      </asp:BoundField>
      <asp:BoundField DataField="conta" HeaderText="conta" 
          SortExpression="conta" >
      </asp:BoundField>
    <asp:BoundField DataField="dt_ultimo" HeaderText="dt_ultimo" 
          SortExpression="dt_ultimo" >
      </asp:BoundField>
        <asp:BoundField DataField="sts_bloq" HeaderText="sts_bloq" 
          SortExpression="sts_bloq" />
  </Columns>
      <HeaderStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
      <AlternatingRowStyle BackColor="White" />
</asp:GridView>
    <br />
</asp:Content>
