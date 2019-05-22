<%@ Page Language="VB" AutoEventWireup="false" CodeFile="eventos_participantes_edita.aspx.vb" Inherits="eventos_participantes_edita" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Prefeitura de Barueri</title>
  <link href="../StyleSheet.css" rel="stylesheet" type="text/css" />
  <script src="../inc/jscript.js" type="text/javascript"></script>
<style type="text/css">
    .style_texto_r
    {
      font-family: Arial, Helvetica, sans-serif;
      text-align: right;
      font-size: 11px;
    }
    
  </style>
  <style type="text/css">
  @media print {
    .info {display: none}
  }
  @media screen {
    .info  {
    background-color: #000080;
    color: #FFFFFF;
    font-weight: 600;
    font-family: Arial, Helvetica, sans-serif;
    }
  }
    .style1
    {
      font-family: Arial, Helvetica, sans-serif;
      text-align: right;
      font-size: 11px;
      width: 209px;
    }
    .style2
    {
      font-size: small;
      color: #FFFFFF;
    }
    .style3
    {
      background-color: #808080;
    }
    </style>

<script language="javascript" type="text/javascript">
// <!CDATA[

  function Button1_onclick() {
    window.print();
  }

// ]]>
</script>
</head>
<body>
  <form id="form1" runat="server">
    <div style="position: absolute">
      <asp:Image ID="Image1" runat="server" ImageAlign="Left" ImageUrl="../img/logo_barueri.jpg" EnableViewState="False" />
    </div>
    <div style="position: absolute; left: 350px;">
      <asp:Image ID="img_foto_usuario" runat="server" ImageAlign="Left" 
        ImageUrl="../img/logo_barueri.jpg" EnableViewState="False" Height="120px" 
        Width="120px" />
    </div>
    <br />
    <div id="Div3" style="position: absolute; top: 160px">
      <div id="Div1" align="center">
        <asp:Label ID="lbl_erro" runat="server" Font-Bold="True" ForeColor="#CC3300" 
          Text="lbl_erro" Visible="False" Font-Size="X-Small" BackColor="#FFFFCC" 
          Width="60%"></asp:Label>
        <br />
          <table id="tbl_dados_requerente" style="border: 1px solid #000000; width:100%; border-collapse: collapse; background-color: #E8E8E8;" 
            cellpadding="2" cellspacing="0" border="1">
            <tr>
              <td colspan="4" align="center" style="background-color: #808080; "height="22" 
                class="style2">
                <span lang="pt-br"><span class="style3">GABINETE DO PREFEITO</span></span></td>
            </tr>
            <tr>
              <td align="right" class="style1">Nome:</td>
              <td>
                <asp:TextBox ID="txt_nome" runat="server" MaxLength="120" Width="352px" CssClass="input_texto"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                  ControlToValidate="txt_nome" ErrorMessage="* Informe o nome" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:Label ID="lbl_nome" runat="server" CssClass="lbl_texto" Text="Label" 
                  Visible="False"></asp:Label>
              </td>
              <td class="style_texto_r"><span class="style_texto_r" lang="pt-br">CPF:</span></td>
              <td>
                <asp:Label ID="lbl_CPF" runat="server" CssClass="lbl_texto" Text="Label"></asp:Label>
              </td>
            </tr>
            <tr>
              <td align="right" class="style1">
                <span lang="pt-br">RG:</span></td>
              <td>
                <asp:TextBox ID="txt_RG" runat="server" CssClass="input_texto"></asp:TextBox>
                <asp:Label ID="lbl_RG" runat="server" CssClass="lbl_texto" Text="Label"></asp:Label>
              </td>
              <td class="style_texto_r">
                <span lang="pt-br">Nascimento:</span></td>
              <td>
                <asp:TextBox ID="txt_dt_nasc_2" runat="server" CssClass="input_texto" 
                  MaxLength="10"></asp:TextBox>
                <asp:Label ID="lbl_dt_nasc" runat="server" CssClass="lbl_texto" Text="Label"></asp:Label>
              </td>
            </tr>
            <tr>
              <td class="style1">Logradouro, nro:</td>
              <td>
                <asp:TextBox ID="txt_xLgr" runat="server" MaxLength="60" Width="295px" 
                  CssClass="input_texto"></asp:TextBox>
                <asp:Label ID="lbl_xLgr" runat="server" CssClass="lbl_texto" Text="Label" 
                  Visible="False"></asp:Label>
                <span lang="pt-br">&nbsp;,&nbsp; </span>
                <asp:TextBox ID="txt_nro" runat="server" MaxLength="10" Width="90px" 
                  CssClass="input_texto"></asp:TextBox>
                <span lang="pt-br">&nbsp;</span><asp:Label ID="lbl_nro" runat="server" 
                  CssClass="lbl_texto" Text="Label"></asp:Label>
              </td>
              <td class="style_texto_r">
                <span lang="pt-br">Complemento:</span></td>
              <td>
                <span lang="pt-br">
                <asp:TextBox ID="txt_xCpl" runat="server" Width="246px" CssClass="input_texto"></asp:TextBox>
                </span>
                <asp:Label ID="lbl_xCpl" runat="server" CssClass="lbl_texto" Text="Label"></asp:Label>
              </td>
            </tr>
            <tr>
              <td align="right" class="style1">Bairro:</td>
              <td>
                <asp:TextBox ID="txt_xBairro" runat="server" MaxLength="60" Width="244px" CssClass="input_texto"></asp:TextBox>
                <span lang="pt-br" class="style_texto_r">&nbsp;<asp:Label ID="lbl_xBairro" 
                  runat="server" Text="Label"></asp:Label>
                &nbsp;CEP:&nbsp;<asp:TextBox ID="txt_CEP" 
                  runat="server" MaxLength="9" Width="75px" CssClass="input_texto"></asp:TextBox>
                &nbsp;<asp:Label ID="lbl_CEP" runat="server" Text="Label" Visible="False"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>

                <asp:SqlDataSource ID="sql_dt_UF" runat="server" ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
                  SelectCommand="SELECT UF FROM tbUF WHERE (UF &lt;&gt; @UF) AND (UF &lt;&gt; 'EX') ORDER BY UF">
                  <SelectParameters>
                    <asp:Parameter DefaultValue="--" Name="UF" Type="String" />
                  </SelectParameters>
                </asp:SqlDataSource>
              </td>
              <td class="style_texto_r">&nbsp;</td>
              <td>
              </td>
            </tr>
            <tr>
              <td class="style1">Telefone:</td>
              <td>
                <asp:TextBox ID="txt_telefone" runat="server" MaxLength="50" Width="237px" CssClass="input_texto"></asp:TextBox>
                <asp:Label ID="lbl_telefone" runat="server" CssClass="lbl_texto" Text="Label" 
                  Visible="False"></asp:Label>
              </td>
              <td class="style_texto_r">E-mail:</td>
              <td>
                <asp:TextBox ID="txt_email" runat="server" Width="242px" CssClass="input_texto"></asp:TextBox>
                <asp:Label ID="lbl_email" runat="server" CssClass="lbl_texto" Text="Label" 
                  Visible="False"></asp:Label>
              </td>
            </tr>
            <tr>
              <td class="style1">
                <span lang="pt-br">Assunto</span></td>
              <td colspan="3">
                <asp:TextBox ID="txt_assunto" runat="server" Columns="80" Rows="6" 
                  TextMode="MultiLine"></asp:TextBox>
                <asp:Label ID="lbl_descr_assunto" runat="server" CssClass="lbl_texto" 
                  Text="Label"></asp:Label>
              </td>
            </tr>
            <tr>
              <td class="style1">
                <span lang="pt-br">Encaminhado para:</span></td>
              <td colspan="3">
                <asp:TextBox ID="txt_encaminhado" runat="server" 
                  CssClass="input_texto" Height="20px" style="width: 320px"></asp:TextBox>
                <asp:Label ID="lbl_encaminhado" runat="server" CssClass="lbl_texto" 
                  Text="Label"></asp:Label>
              </td>
            </tr>
            <tr>
              <td class="style1">
                <span lang="pt-br">Atendimento:</span></td>
              <td colspan="3">
                <asp:TextBox ID="txt_atendimento" runat="server" CssClass="input_texto" 
                  Width="320px"></asp:TextBox>
                <asp:Label ID="lbl_atendimento" runat="server" CssClass="lbl_texto" 
                  Text="Label"></asp:Label>
              </td>
            </tr>
            <tr>
              <td class="style1">
                <span lang="pt-br">Foto:</span></td>
              <td colspan="3">
                <asp:FileUpload ID="fld_up_foto" runat="server" />
                <asp:Button ID="btn_grava_foto" runat="server" Text="Gravar Foto" />
                <asp:Label ID="labERRO" runat="server" Font-Size="X-Small" ForeColor="#CC3300" 
                  Text="labERRO" Visible="False"></asp:Label>
              </td>
            </tr>
          </table>

        </div>
        <br />
        <div id="Div2" align="center">
            <table id="tbl_salvar" style="border: 1px solid #000000; width:100%; border-collapse: collapse; background-color: #E8E8E8;">
              <tr>
                <td class="style_texto_r">
                  <asp:Button ID="btn_Salvar" runat="server" CssClass="button_tb" Text="Salvar" 
                    UseSubmitBehavior="False" />
                </td>
                <td>
                  <span lang="pt-br" 
                    style="font-family: Arial, Helvetica, sans-serif; font-size: 11px">&lt;-- ~clique 
                  aqui para salvar o comentário do Cidadão</span></td>
              </tr>
            </table>
          </div>



      </div>

  </form>
</body>
</html>