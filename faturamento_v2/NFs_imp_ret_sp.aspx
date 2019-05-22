<%@ Page Language="VB" AutoEventWireup="false" ContentType="text/html; charset=UTF-8" CodeFile="NFs_imp_ret_sp.aspx.vb" Inherits="Municipio_faturamento_v2_NFs_imp_ret_sp" %>
<%@ Import Namespace="System.Web.Services" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="pt-br" xml:lang="pt-br">

<head id="Head1" runat="server">
  <title></title>
  <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
  <script src="../../inc/func_javascript.js" type="text/javascript"></script>
  <link href="../../inc/adm.css" rel="stylesheet" type="text/css" />
  <style type="text/css">
  .titulo_grid
    {
      background-color: #D0DBFE;
      font-weight:bold;
    }
    a
    {
      color: Blue;
    }
    a:hover
    {
      color: Red;
      cursor: pointer;
    }
  </style>
</head>
<body>
<form id="frmClientes" name="frmClientes" runat="server" action="NFs_imp_ret_sp.aspx?">

<asp:Panel ID="Panel1" runat="server">
  <div align="center">  
    <span class="msg_orienta">Selecione o arquivo <b>.csv</b> e clique em <b>Importar</b>...</span><br /><br />
        
    <asp:FileUpload ID="file_importa" runat="server" Width="450px" /><br /><br />        
    
    <asp:Button ID="btn_importa" runat="server" Text="Importar" CssClass="button" Height="17px" />
   
    <asp:Button ID="btn_salvar" runat="server" Text="Salvar" CssClass="esconde" Height="17px" OnClick="Salvar" OnClientClick="return false;" /><br /><br />
    
    <asp:GridView ID="dgrid_excel" OnRowDataBound="LoadGrid" runat="server" EnableViewState="true">
      <HeaderStyle CssClass="titulo_grid" />
      
    </asp:GridView><br /><br />
    
    <a onclick="Consulta();">Clique aqui</a> para consultar notas que não foram importadas no sistema e constam como pendentes...<br /><br />
    
    <asp:Label ID="lbl_aviso" runat="server" CssClass="msg_orienta"></asp:Label><br /><br />
    
    <asp:Label ID="lbl_erro" runat="server" CssClass="msg_orienta"></asp:Label>    
  </div>
</asp:Panel>

<asp:TextBox ID="txt_id_empresa" runat="server" Visible="false" Width="100px" />


</form>

<script language="javascript" type="text/javascript">
function Consulta() {
  window.location = '../../Faturamento/imp_nfse_sp_result.asp?cmun=3550308';
}
</script>
</body>
</html>


