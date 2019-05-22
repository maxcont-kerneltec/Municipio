<%@ Page Title="" Language="VB" MasterPageFile="Cad_MasterPage.master" AutoEventWireup="false" CodeFile="cad_usuario.aspx.vb" Inherits="cad_usuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:SqlDataSource ID="sql_dt_cad_usuarios" runat="server" 
        ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
        SelectCommand="sp1Usuarios_Pega" SelectCommandType="StoredProcedure">
      <SelectParameters>
        <asp:SessionParameter Name="sp_id_municipio" SessionField="id_municipio" 
          Type="String" />
        <asp:SessionParameter Name="spid_usuario" SessionField="id_usuario" 
          Type="String" />
        <asp:ControlParameter ControlID="txt_nome_pesq" DefaultValue=" " 
          Name="spnome_usuario" PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="txt_CPF_pesq" DefaultValue=" " 
          Name="spCPF_usuario" PropertyName="Text" Type="String" />
      </SelectParameters>
      </asp:SqlDataSource>
    <br />
  <table style="width:100%;" class="table_pesquisa">
  <tr>
    <td class="label_coluna_r">Nome:</td>
    <td class="td_coluna_l">
      <asp:TextBox ID="txt_nome_pesq" runat="server" CssClass="input_texto_l1"></asp:TextBox>&nbsp;&nbsp;&nbsp;
    </td>
    <td class="label_coluna_r">CPF:</td>
    <td class="td_coluna_l">
      <asp:TextBox ID="txt_CPF_pesq" runat="server" CssClass="input_texto_t1"></asp:TextBox>&nbsp;&nbsp;&nbsp;
      <asp:Button ID="botPesquisa" runat="server" Text="Pesquisar"  Width="146px" CssClass="button_tb" />
    </td>
  </tr>
  </table>
    <br />
    <asp:Panel ID="pnl_novo" runat="server">
      <asp:Button ID="botNovo" runat="server" CssClass="button_tb" Text="Novo" />&lt;-- clique aqui para cadastrar novo usuário    <br />
    </asp:Panel>
    <br />
    <asp:GridView ID="GridView_Usuarios" runat="server" AllowPaging="True" 
      AllowSorting="True" AutoGenerateColumns="False" DataSourceID="sql_dt_cad_usuarios" 
      EmptyDataText="Nenhum Registro Localizado" Width="100%" PageSize="50">
      <RowStyle CssClass="grd_row" />
      <EmptyDataRowStyle BackColor="#0099CC" />
      <Columns>
      <asp:TemplateField HeaderText="ID" SortExpression="id_usuario">
          <EditItemTemplate>
              <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("id_usuario") %>' Width="80px"></asp:TextBox>
          </EditItemTemplate>
          <ItemTemplate>
              <asp:Button ID="Button4" runat="server" CommandArgument='<%# Bind("id_usuario") %>' oncommand="Acessa_Cadastro" 
                          Text='<%# Bind("id_usuario") %>' UseSubmitBehavior="False" Width="80px" CssClass="button_tb" />
          </ItemTemplate>
          <ItemStyle Width="83px" HorizontalAlign="Center" />
        </asp:TemplateField>
    <asp:BoundField DataField="nome_usuario" HeaderText="Nome" SortExpression="nome_usuario" >
       <HeaderStyle HorizontalAlign="Left" />
    </asp:BoundField>
    <asp:BoundField DataField="cpf_usuario" HeaderText="CPF" 
          SortExpression="cpf_usuario" >
      <HeaderStyle HorizontalAlign="Left" />
      <ItemStyle Width="100px" />
      </asp:BoundField>
    <asp:BoundField DataField="login" HeaderText="Login" SortExpression="login" >
      <HeaderStyle HorizontalAlign="Left" />
      <ItemStyle Width="100px" />
      </asp:BoundField>
    <asp:BoundField DataField="permissao" HeaderText="Permissão" 
          SortExpression="permissao" >
      <ItemStyle HorizontalAlign="Center" Width="80px" />
      </asp:BoundField>
    <asp:BoundField DataField="acesso" HeaderText="Acesso" SortExpression="acesso" >
      <ItemStyle HorizontalAlign="Center" Width="80px" />
      </asp:BoundField>
      <asp:BoundField DataField="sts_bloq" HeaderText="Bloqueio" 
          SortExpression="sts_bloq" >
      <ItemStyle HorizontalAlign="Center" Width="80px" />
      </asp:BoundField>
    <asp:BoundField DataField="conta" HeaderText="Acessos" SortExpression="conta" >
      <ItemStyle HorizontalAlign="Center" Width="80px" />
      </asp:BoundField>
      <asp:BoundField DataField="dt_ultimo" HeaderText="Ultimo Acesso" SortExpression="dt_ultimo">
      <HeaderStyle HorizontalAlign="Left" />
      <ItemStyle Width="150px" />
      </asp:BoundField>
  </Columns>
      <HeaderStyle Font-Bold="True" ForeColor="White" CssClass="grd_header" />
      <AlternatingRowStyle CssClass="grd_row_alt" />
</asp:GridView>
    <br />
</asp:Content>
