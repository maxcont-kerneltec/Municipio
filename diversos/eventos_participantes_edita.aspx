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
    .tbInfo {width: 640px}
  }
  @media screen {
    .info {display: block}
    .tbInfo {width: 100%}
  }
  .style1
  {
    font-family: Arial, Helvetica, sans-serif;
    text-align: right;
    font-size: 13px;
    width: 250px;
    height: 25px;
    text-transform:uppercase
  }
  .style4
  {
    font-size: 11px;
  }
    .style5
    {
      width: 70%;
    }
    .style6
    {
      font-family: Arial, Helvetica, sans-serif;
      text-align: right;
      font-size: 11px;
      width: 50%;
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
  <asp:ScriptManager ID="ScriptManager1" runat="server">
  </asp:ScriptManager>
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

    <div style="position: absolute; width: 95%;">
      <table class="tbInfo" bgcolor="#EFF3FB">
        <tr>
          <td colspan="1" valign="top">
            <asp:Image ID="Image2" runat="server" ImageAlign="Left" ImageUrl="../img/logo_barueri.jpg" EnableViewState="False" />
            <br />
            <strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </strong>
          </td>
          <td align="right">
            <span title="Prefeitura de Barueri" style="color: #000080; font-size: 14px; font-weight: bold;">O PREFEITO NO SEU BAIRRO<br /></span>
            <br />
            <span lang="pt-br" class="info" style="text-decoration: underline; font-size: small; cursor: pointer; color: #000080; font-weight: bold;" 
              onclick="window.location='../login_usuario.aspx?a=logout&SIS_=1'">Sair</span>
            <div style="position: absolute; left: 305px; background-color: #66CCFF; top: 10px;">
              <asp:Image ID="img_foto_usuario" runat="server" ImageAlign="Left" 
                ImageUrl="../img/logo_barueri.jpg" EnableViewState="False" Height="120px" 
                Width="120px" Visible="False" />
            </div>

          </td>
        </tr>
      </table>
    </div>

    <br />
    <div id="Div3" style="position: absolute; top: 160px; width: 95%;">
      <div id="Div1" align="left">
        <asp:Label ID="lbl_erro" runat="server" Font-Bold="True" ForeColor="#CC3300" 
          Text="lbl_erro" Visible="False" Font-Size="X-Small" BackColor="#FFFFCC" 
          Width="60%"></asp:Label>
        <br />
        <div id="Div4" align="left">
          <table id="Table1" class="tbInfo" style="border: 1px solid #000000; border-collapse: collapse; background-color: #E8E8E8;" class="info">
            <tr>
              <td align="left">
                <asp:Button ID="Button1" runat="server" CssClass="button_verde" Text="Voltar" 
                  UseSubmitBehavior="False" CausesValidation="False" />
              </td>
            </tr>
          </table>

          <br />
        </div>

        <table id="tbl_dados2" class="tbInfo style="border: 1px solid #000000; border-collapse: collapse; background-color: #E8E8E8;" 
          cellpadding="2" cellspacing="0" border="1">
          <tr>
            <td colspan="4" align="center">
              <span lang="pt-br">GABINETE DO PREFEITO</span></td>
          </tr>
          <tr>
            <td align="right" class="style1">CPF:</td>
            <td width="35%">
              <asp:Label ID="lbl_CPF" runat="server" CssClass="lbl_texto" Text="Label" 
                Font-Size="Small"></asp:Label>
            </td>
            <td class="style6" width="15%" colspan="2">
              &nbsp;</td>
          </tr>
          <tr>
            <td align="right" class="style1" width="15%">
              Nome:</td>
            <td class="style5" colspan="3">
              <asp:TextBox ID="txt_nome" runat="server" CssClass="input_texto_grande" 
                MaxLength="120" Width="352px"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="txt_nome" Display="Dynamic" 
                ErrorMessage="* Informe o nome" Font-Size="X-Small"></asp:RequiredFieldValidator>
              <asp:Label ID="lbl_nome" runat="server" CssClass="lbl_texto" Text="Label" 
                Visible="False"></asp:Label>
            </td>
          </tr>
          <tr>
            <td class="style1">RG:</td>
            <td>
              <asp:TextBox ID="txt_RG" runat="server" CssClass="input_texto_grande"></asp:TextBox>
              <asp:Label ID="lbl_RG" runat="server" CssClass="lbl_texto" Text="Label"></asp:Label>
            </td>
            <td class="style1">Nascimento:</td>
            <td>
              <asp:TextBox ID="txt_dt_nasc_2" runat="server" CssClass="input_texto_grande" 
                MaxLength="10" Width="121px"></asp:TextBox>
              <asp:Label ID="lbl_dt_nasc" runat="server" CssClass="lbl_texto" Text="Label"></asp:Label>
              <asp:RequiredFieldValidator ID="rqrd_dt_nasc" runat="server" 
                ControlToValidate="txt_dt_nasc_2" Display="Dynamic" 
                ErrorMessage="Informe a data de nascimento" Font-Size="X-Small"></asp:RequiredFieldValidator>
              <asp:CompareValidator ID="cv_dt_nasc_2" runat="server" 
                ControlToValidate="txt_dt_nasc_2" Display="Dynamic" 
                ErrorMessage="Informe a data no formato DD/MM/AAAA." Font-Size="X-Small" 
                Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
            </td>
          </tr>
          <tr>
            <td class="style1">Logradouro, nro:</td>
            <td colspan="3">
              <asp:TextBox ID="txt_xLgr" runat="server" MaxLength="60" Width="295px" 
                CssClass="input_texto_grande"></asp:TextBox>
              <asp:Label ID="lbl_xLgr" runat="server" CssClass="lbl_texto" Text="Label" 
                Visible="False"></asp:Label>
              <span lang="pt-br">&nbsp;,&nbsp; </span>
              <asp:TextBox ID="txt_nro" runat="server" MaxLength="10" Width="90px" 
                CssClass="input_texto_grande"></asp:TextBox>
              <span lang="pt-br">&nbsp;</span><asp:Label ID="lbl_nro" runat="server" 
                CssClass="lbl_texto" Text="Label"></asp:Label>
            </td>
          </tr>
          <tr>
            <td class="style1">
              <span lang="pt-br">Complemento:</span></td>
            <td colspan="3">
              <span lang="pt-br">
              <asp:TextBox ID="txt_xCpl" runat="server" CssClass="input_texto_grande" Width="246px"></asp:TextBox>
              <asp:Label ID="lbl_xCpl" runat="server" CssClass="lbl_texto" Text="Label"></asp:Label>
              </span>
            </td>
          </tr>
          <tr>
            <td align="right" class="style1">Bairro:</td>
            <td>
              <asp:TextBox ID="txt_xBairro" runat="server" MaxLength="60" Width="244px" CssClass="input_texto_grande"></asp:TextBox>
              <span lang="pt-br" class="style_texto_r">&nbsp;<asp:Label ID="lbl_xBairro" 
                runat="server" Text="Label"></asp:Label>
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>

            </td>
            <td class="style1">CEP:&nbsp;</td>
            <td>
              <span class="style_texto_r" lang="pt-br">
              <asp:TextBox ID="txt_CEP" runat="server" CssClass="input_texto_grande" MaxLength="9" 
                Width="112px"></asp:TextBox>
              <asp:Label ID="lbl_CEP" runat="server" Text="Label" Visible="False"></asp:Label>
              </span>
            </td>
          </tr>
          <tr>
            <td class="style1">Telefone:</td>
            <td colspan="3">
              <asp:TextBox ID="txt_telefone" runat="server" MaxLength="50" Width="497px" 
                CssClass="input_texto_grande"></asp:TextBox>
              <asp:Label ID="lbl_telefone" runat="server" CssClass="lbl_texto" Text="Label" 
                Visible="False"></asp:Label>
            </td>
          </tr>
          <tr>
            <td class="style1">
              E-mail:</td>
            <td colspan="3">
              <asp:TextBox ID="txt_email" runat="server" CssClass="input_texto_grande" 
                Width="497px"></asp:TextBox>
              <asp:Label ID="lbl_email" runat="server" CssClass="lbl_texto" Text="Label" 
                Visible="False"></asp:Label>
            </td>
          </tr>
          <tr>
            <td class="style1">
              <span lang="pt-br">Assunto</span></td>
            <td colspan="3">
              <asp:TextBox ID="txt_assunto" runat="server" Columns="60" Rows="6" 
                TextMode="MultiLine" CssClass="input_texto_grande"></asp:TextBox>
              <asp:Label ID="lbl_descr_assunto" runat="server" CssClass="lbl_texto" 
                Text="Label"></asp:Label>
              <asp:RequiredFieldValidator ID="rqrd_assunto" runat="server" 
                ControlToValidate="txt_assunto" ErrorMessage="Informe o Assunto." 
                Font-Size="X-Small" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
          </tr>
          <tr>
            <td class="style1">
              <span lang="pt-br">Encaminhado para:</span></td>
            <td colspan="3">
              <asp:TextBox ID="txt_encaminhado" runat="server" 
                CssClass="input_texto_grande" style="width: 320px"></asp:TextBox>
              <asp:Label ID="lbl_encaminhado" runat="server" CssClass="lbl_texto" 
                Text="Label"></asp:Label>
            </td>
          </tr>
          <tr>
            <td class="style1">
              <span lang="pt-br">Atendimento:</span></td>
            <td colspan="3">
              <asp:TextBox ID="txt_atendimento" runat="server" CssClass="input_texto_grande" 
                Width="320px"></asp:TextBox>
              <asp:Label ID="lbl_atendimento" runat="server" CssClass="lbl_texto" 
                Text="Label"></asp:Label>
            </td>
          </tr>
        </table>
        <br />
        <asp:Panel ID="pnl_foto" runat="server" Visible="False">
          <table id="tbl_dados" style="border: 1px solid #000000; width:640px; border-collapse: collapse; background-color: #E8E8E8;" class="info">
            <tr>
              <td colspan="2">
                <span lang="pt-br" style="font-family: Arial, Helvetica, sans-serif; font-size: 11px">Foto:</span><asp:FileUpload ID="fld_up_foto" 
                  runat="server" style="margin-top: 0px" />
                <asp:Button ID="btn_grava_foto" runat="server" Text="Gravar Foto" 
                  CausesValidation="False" EnableViewState="False" />
                <asp:Label ID="labERRO" runat="server" Font-Size="X-Small" ForeColor="#CC3300" 
                  Text="labERRO" Visible="False"></asp:Label>
              </td>
            </tr>
          </table>
        </asp:Panel>

        </div>
        <br />
        <div id="dv_botoes" align="left">
          <asp:Panel ID="pnl_botoes" runat="server">
            <table id="tbl_salvar" class="tbInfo" style="border: 1px solid #000000; border-collapse: collapse; background-color: #E8E8E8;">
              <tr>
                <td class="style_texto_r">
                  <asp:Button ID="btn_Salvar" runat="server" CssClass="button_tb" Text="Salvar" 
                    Width="90px" />
                </td>
                <td>
                  <span lang="pt-br" 
                    style="font-family: Arial, Helvetica, sans-serif; font-size: 11px">&lt;-- clique 
                  aqui para salvar o comentário do Cidadão</span></td>
              </tr>
              <tr>
                <td class="style_texto_r">
                  <asp:Button ID="btn_prepara_imprimir" runat="server" CssClass="button_tb" Text="Impressão" 
                    UseSubmitBehavior="False" Width="90px" />
                </td>
                <td>
                  <span lang="pt-br" 
                    style="font-family: Arial, Helvetica, sans-serif; font-size: 11px">&lt;-- clique 
                  aqui </span><span lang="pt-br"><span class="style4">para preparar para Impressão</span></span></td>
              </tr>
            </table>
          </asp:Panel>

          <asp:Panel ID="pnl_botao_imprimir" runat="server">
            <table id="Table2" class="tbInfo" style="border: 1px solid #000000; border-collapse: collapse; background-color: #E8E8E8;" class="info">
              <tr>
                <td class="style_texto_r">
                  <asp:Button ID="btn_imprimir_2" runat="server" CssClass="button_tb" 
                    onclientclick="window.print();return(false);" Text="Imprimir" 
                    UseSubmitBehavior="False" />
                </td>
                <td>
                  <span lang="pt-br" 
                    style="font-family: Arial, Helvetica, sans-serif; font-size: 11px; color: #000000;">&lt;-- clique 
                  aqui para imprmir</span></td>
              </tr>
            </table>
          </asp:Panel>

        </div>
      </div>
    </ContentTemplate>

    <Triggers>
      <asp:PostBackTrigger ControlID="btn_grava_foto" />
      <asp:PostBackTrigger ControlID="btn_Salvar" />
    </Triggers>

  </asp:UpdatePanel>

  </form>
</body>
</html>