<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Importa_Cadastro.aspx.vb" Inherits="Municipio_Estoque_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <p>
      Salve o arquivo XML e Importe no Portal da Prefeitura.</p>
    <form id="form1" runat="server">
    <div>


<html>
<head>
</head>
<body>
<asp:Label ID="lblSql1" Runat="server" />
<asp:DataGrid ID="dtgAgenda1" Runat="server"
              HeaderStyle-BackColor="Red"
              HeaderStyle-ForeColor="White"
              HeaderStyle-Font-Name="Verdana"
              HeaderStyle-Font-Size="10"
              ItemStyle-BackColor="Cyan"
              ItemStyle-Font-Name="Verdana"
              ItemStyle-Font-Size="10"
              CellPadding="4"
              GridLines="Both" />
<p></p>
<asp:Label ID="lblSql2" Runat="server" />
<asp:DataGrid ID="dtgAgenda2" Runat="server"
              HeaderStyle-BackColor="Red"
              HeaderStyle-ForeColor="White"
              HeaderStyle-Font-Name="Verdana"
              HeaderStyle-Font-Size="10"
              ItemStyle-BackColor="Cyan"
              ItemStyle-Font-Name="Verdana"
              ItemStyle-Font-Size="10"
              CellPadding="4"
              GridLines="Both" />
    
    </div>
    </form>
</body>
</html>
