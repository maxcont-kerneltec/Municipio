<%@ Page Language="VB" AutoEventWireup="false" CodeFile="cad_cidadao_imprime.aspx.vb" Inherits="Cad_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Prefeitura Municipal de Barueri</title>
    <link href="inc/StyleSheet.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">
// <!CDATA[

function carrega_doc(doc) {
  window.open('docs/<%=Session("id_municipio") %>/<%=right("0000000" & Request.QueryString("id_cidadao"), 7) %>/' + doc, '', 'toolbar=yes, resizable=yes, scrollbars=yes, width=670px, height=500px')
}

function btn_visualza_end_cidadao(num_instalacao) {
  window.open('rel_endereco_cidadao.aspx?num_instalacao=' + num_instalacao, '', 'toolbar=yes, resizable=yes, scrollbars=yes, width=670px, height=500px')
}

// ]]>
</script>
<style type="text/css">
  .label_coluna_l
  {
    text-align: left;
  }  
  .label_coluna_l
  {
    width: 57%;
  }
</style>
</head>
<body>
    <form id="form1" runat="server">
      <div>
      <asp:Panel ID="pnl_prococolo" runat="server">

        <table width="620" align="left" border="1" 
          style="border-collapse: collapse; border: 1px solid #000000" cellpadding="5" 
          cellspacing="0">
          <tr>
            <td align="center" colspan="2" height="90" valign="middle" style="padding: 15px">
              <asp:Image ID="Image1" runat="server" ImageUrl="../img/logo_barueri.jpg" EnableViewState="False" />
            </td>
            <td align="center" width="150">
              <asp:Image ID="img_foto_cidadao" runat="server" Height="120px" Width="160px" />
            </td>
          </tr>
          <tr>
            <td align="center" colspan="3">Protocolo de cadastro do cidadão</td>
          </tr>
          <tr>
            <td width="30%" class="label_coluna_r">ID Cidadão:</td>
            <td align="left" class="label_coluna_l" colspan="2">
              <asp:Label ID="lbl_id_cidadao" runat="server" Text="Label"></asp:Label>
            </td>
          </tr>
          <tr>
            <td class="label_coluna_r">Nome:</td>
            <td align="left" class="label_coluna_l" colspan="2">
              <asp:Label ID="lbl_nome" runat="server" Text="Label"></asp:Label>
            </td>
          </tr>
          <tr>
            <td class="label_coluna_r">SUS:</td>
            <td align="left" class="label_coluna_l" colspan="2">
              <asp:Label ID="lbl_sus" runat="server" Text="Label"></asp:Label>
            </td>
          </tr>
          <tr>
            <td class="label_coluna_r">Data de cadastro:</td>
            <td align="left" class="label_coluna_l" colspan="2">
              <asp:Label ID="lbl_dt_alt" runat="server" Text="Label"></asp:Label>
            </td>
          </tr>
          <tr>
            <td class="label_coluna_r">Situação:</td>
            <td align="left" class="label_coluna_l" colspan="2">
              <asp:Label ID="lbl_situacao" runat="server" Text="Label"></asp:Label>
            </td>
          </tr>
        </table>
      </asp:Panel>

      </div>
      <asp:Panel ID="pnl_completo" runat="server">
        <table width="640" align="left" border="1" 
          style="border-collapse: collapse; border: 1px solid #000000" cellpadding="5" 
          cellspacing="0">
          <tr>
            <td colspan="4" height="140" valign="middle" 
              style="padding: 15px">
              <div  style="width: 250px; position: absolute; top: 20px;">
                <asp:Image ID="Image2" runat="server" ImageUrl="../img/logo_barueri.jpg" EnableViewState="False" />
              </div>
              <div  style="width: 250px; position: absolute; top: 20px; left: 420px;">
                <asp:Image ID="img_foto_cidadao2" runat="server" Height="120px" Width="160px" />
              </div>
            </td>
          </tr>
          <tr>
            <td align="center" colspan="4">
               Impressão de Cadastro - ID Cidadão: 
              <asp:Label ID="lbl_id_cidadao_2" runat="server" Text="lbl_id_cidadao_2"></asp:Label>
            </td>
          </tr>
          <tr>
            <td align="center" colspan="4">
              <span lang="pt-br">Situação:
              <asp:Label ID="lbl_situacao_2" runat="server" Text="Label"></asp:Label>
              </span>
            </td>
          </tr>
        <tr>
          <td colspan="4" style="background-color: #EFF3FB;">
            alterado por: <asp:Label ID="lbl_nome_usuario" runat="server"></asp:Label>
            &nbsp;em
            <asp:Label ID="lbl_dt_alt2" runat="server" Text="dt_alt"></asp:Label>
            . Local:
            <asp:Label ID="lbl_descr_local" runat="server" Text="Label"></asp:Label>
          </td>
        </tr>

        <tr>
          <td class="label_coluna_r">Nome Registro:</td>
          <td class="style13">
            <asp:Label ID="lbl_nome_registro" runat="server" Text="Label"></asp:Label>
          </td>
          <td class="label_coluna_r">Nome Social:</td>
          <td class="label_coluna_l">
            <asp:Label ID="lbl_nome_social" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>

        <tr>
          <td class="label_coluna_r">Cartão do SUS:</td>
          <td class="label_coluna_l">
            <asp:Label ID="lbl_SUS_2" runat="server" Text="lbl_SUS_2"></asp:Label>
          </td>
          <td align="right" colspan="1" class="style16">&nbsp;</td>
          <td align="left">&nbsp;</td>
        </tr>

        <tr>
          <td class="label_coluna_r">CPF:</td>
          <td class="label_coluna_l">
            <asp:Label ID="lbl_CPF" runat="server" Text="Label"></asp:Label>
          </td>
          <td class="label_coluna_r">CPF Responsável:</td>
          <td class="label_coluna_l">
            <asp:Label ID="lbl_CPF_resp" runat="server" Text="Label"></asp:Label>
            <span lang="pt-br">&nbsp; </span>
            <asp:Label ID="lbl_nome_resp" runat="server" Text=""></asp:Label>
          </td>
        </tr>

        <tr>
          <td class="label_coluna_r">Número Reg. Nasc:</td>
          <td class="label_coluna_l" colspan="3">
            <asp:Panel ID="pnl_reg_antigo" runat="server" Visible="False">
            Termo:
              <asp:Label ID="lbl_num_reg_nasc" runat="server" Text="lbl_num_reg_nasc"></asp:Label>
              &nbsp; Folha: &nbsp;<asp:Label ID="lbl_num_reg_nasc_folha" runat="server" 
                Text="lbl_num_reg_nasc_folha"></asp:Label>
              &nbsp;&nbsp; Livro:
              <asp:Label ID="lbl_num_reg_nasc_livro" runat="server" 
                Text="lbl_num_reg_nasc_livro"></asp:Label>
            </asp:Panel>

            <asp:Panel ID="pnl_reg_novo" runat="server" Visible="False">
              <asp:Label ID="lbl_cod_nac" runat="server" Text="cod_nasc"></asp:Label>
              <span lang="pt-br">&nbsp;&nbsp;&nbsp; </span>
              <asp:Label ID="lbl_cod_acervo" runat="server" Text="cod_acervo"></asp:Label>
              <span lang="pt-br">&nbsp;&nbsp; </span>
              <asp:Label ID="lbl_serv" runat="server" Text="lbl_serv"></asp:Label>
              <span lang="pt-br">&nbsp; </span>
              <asp:Label ID="lbl_ano_registro" runat="server" Text="lbl_ano_registro"></asp:Label>
              <span lang="pt-br">&nbsp; </span>
              <asp:Label ID="lbl_tipo_livro" runat="server" Text="lbl_tipo_livro"></asp:Label>
              <span lang="pt-br">&nbsp;&nbsp; </span>
              <asp:Label ID="lbl_num_livro" runat="server" Text="lbl_num_livro"></asp:Label>
              <span lang="pt-br">&nbsp;</span><asp:Label ID="lbl_folha_livro" runat="server" 
                Text="lbl_folha_livro"></asp:Label>
              <span lang="pt-br">&nbsp; </span>
              <asp:Label ID="lbl_num_termo" runat="server" Text="lbl_num_termo"></asp:Label>
              <asp:Label ID="lbl_dig_verif" runat="server" Text="lbl_dig_verif"></asp:Label>
            </asp:Panel>

          </td>
        </tr>
        <tr>
          <td class="label_coluna_r">Data Nascimento:</td>
          <td colspan="3" class="label_coluna_l">
            <asp:Label ID="lbl_dt_Nasc" runat="server" Text="Label"></asp:Label>
            &nbsp;<asp:Label ID="lbl_idade_YMD" runat="server" Text="Label"></asp:Label>
            &nbsp;Local Nascimento:&nbsp;<asp:Label ID="lbl_reg_nasc_UF" 
              runat="server" Text="lbl_reg_nasc_UF"></asp:Label>
            /<span lang="pt-br"> </span>
            <asp:Label ID="lbl_xMun_reg_nasc" runat="server" Text="lbl_xMun_reg_nasc"></asp:Label>
          &nbsp;</td>
        </tr>
        <tr>
          <td class="label_coluna_r">RG:</td>
          <td class="label_coluna_l" colspan="3">
            <asp:Label ID="lbl_RG" runat="server" Text="Label"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Orgão Emissor:
            <asp:Label ID="lbl_RG_orgao" runat="server" Text="Label"></asp:Label>
            &nbsp;&nbsp; UF Emissão:<asp:Label ID="lbl_RG_UF" runat="server" Text="Label"></asp:Label>
          &nbsp;</td>
        </tr>

        <tr>
          <td class="label_coluna_r">Número Título:</td>
          <td class="label_coluna_l">
            <asp:Label ID="lblTitulo_eleitor" runat="server" Text="lblTitulo_eleitor"></asp:Label>
            <span lang="pt-br">&nbsp;&nbsp;&nbsp;&nbsp; </span>Zona:
            <asp:Label ID="lbltitulo_zona" runat="server" Text="lbltitulo_zona"></asp:Label>
          </td>
          <td class="label_coluna_r">Seção:</td>
          <td align="left" class="label_coluna_l">
            <asp:Label ID="lbltitulo_Secao" runat="server" Text="lbltitulo_Secao"></asp:Label>
            </td>
        </tr>

        <tr>
          <td class="label_coluna_r">Registro de Estudante:</td>
          <td class="label_coluna_l">
            <asp:Label ID="lbl_reg_estudante" runat="server" Text="lbl_reg_estudante"></asp:Label>
          </td>
          <td align="right" colspan="2">&nbsp;</td>
        </tr>
        <tr>
          <td class="label_coluna_r">Funcionário da Prefeitura?</td>
          <td class="label_coluna_l">
            <asp:Label ID="lbl_fPM_Func" runat="server" Text="lbl_fPM_Func"></asp:Label>
          </td>
          <td class="label_coluna_r">CPF do funcionário: </td>
          <td class="label_coluna_l">
            <asp:Label ID="lbl_PM_CPF" runat="server" Text="lbl_PM_CPF"></asp:Label>
            </td>
        </tr>
        <tr>
          <td class="label_coluna_r">Sexo:</td>
          <td class="label_coluna_l">
            <asp:Label ID="lbl_sexo" runat="server" Text="lbl_sexo"></asp:Label>
          </td>
          <td class="label_coluna_r">Estado Civil:</td>
          <td class="label_coluna_l">
            <asp:Label ID="lbl_estado_civil" runat="server" Text="lbl_estado_civil"></asp:Label>
          </td>
        </tr>
        <tr>
          <td class="label_coluna_r">Raça / Cor:</td>
          <td class="label_coluna_l">
            <asp:Label ID="lbl_raca_cor" runat="server" Text="lbl_raca_cor"></asp:Label>
          </td>
          <td class="label_coluna_r">Etnia:</td>
          <td class="label_coluna_l">
            <asp:Label ID="lbl_descr_etnia" runat="server" Text="lbl_descr_etnia"></asp:Label>
          </td>
        </tr>
        <tr>
          <td class="label_coluna_r">Religião:</td>
          <td colspan="3" class="label_coluna_l">
            <asp:Label ID="lbl_descr_religiao" runat="server" Text="lbl_descr_religiao"></asp:Label>
          </td>
        </tr>
        <tr>
          <td class="label_coluna_r">Escolaridade:</td>
          <td colspan="3" class="label_coluna_l">
            <asp:Label ID="lbl_descr_escolaridade" runat="server" Text="lbl_descr_escolaridade"></asp:Label>
          </td>
        </tr>
        <tr>
          <td class="label_coluna_r">Profissão:</td>
          <td colspan="3" class="label_coluna_l">
            <asp:Label ID="lbl_descr_cbo" runat="server" Text="lbl_descr_cbo"></asp:Label>
          </td>
        </tr>
        <tr>
          <td class="label_coluna_r">Deficiente?</td>
          <td colspan="3" class="label_coluna_l">
            <asp:Label ID="lbl_fDef" runat="server" Text="lbl_fDef"></asp:Label>
            &nbsp;Deficiência:
            <asp:Label ID="lbl_Def_descr" runat="server" Text="lbl_Def_descr"></asp:Label>
          </td>
        </tr>
        <tr>
          <td class="label_coluna_r">Nome Conjuge: </td>
          <td class="label_coluna_l">
            <asp:Label ID="lbl_nome_conjuge" runat="server" Text="lbl_nome_conjuge"></asp:Label>
          </td>
          <td class="label_coluna_r">CPF Conjuge:</td>
          <td class="label_coluna_l">
            <asp:Label ID="lbl_CPF_conjuge" runat="server" Text="lbl_CPF_conjuge"></asp:Label>
          </td>
        </tr>
        <tr>
          <td align="center" colspan="4" style="background-color: #CCCCCC;">Endereço - informações a partir da conta de energia elétrica</td>
        </tr>

        <tr>
          <td class="label_coluna_r">Número Instalação:</td>
          <td class="label_coluna_l">
            <asp:Label ID="lbl_num_instalacao" runat="server" Text="lbl_num_instalacao"></asp:Label>
            &nbsp;&nbsp;<asp:Label ID="lbl_num_cidadao" runat="server"></asp:Label>
            <asp:Button ID="btn_endereco_cidadao" runat="server" Text="Visualizar" 
              CausesValidation="False" UseSubmitBehavior="False" 
              ToolTip="Clique aqui para visualizar..." Width="30px" 
              Visible="False" />
            &nbsp;
          </td>
          <td class="label_coluna_r">&nbsp;</td>
          <td class="label_coluna_r">&nbsp;</td>
        </tr>
        <tr>
          <td class="label_coluna_r">CPF / CNPJ Instalação:</td>
          <td class="label_coluna_l">
            <asp:Label ID="lbl_cnpj_instalacao" runat="server"></asp:Label>
          </td>
          <td class="label_coluna_r">Data de Emissão:</td>
          <td class="label_coluna_l">
            <asp:Label ID="lbl_num_instala_dt_emissao" runat="server"></asp:Label>
          </td>
        </tr>
        <tr>
          <td class="label_coluna_r">Número Inscrição - IPTU:</td>
          <td class="label_coluna_l">
            <asp:Label ID="lbl_num_inscr_imovel" runat="server"></asp:Label>
          </td>
          <td class="label_coluna_r">Matrícula do Imóvel:</td>
          <td class="label_coluna_l">
            <asp:Label ID="lbl_num_matri_imovel" runat="server"></asp:Label>
          </td>
        </tr>
        <tr>
          <td class="label_coluna_r">Logradouro:</td>
          <td class="label_coluna_l">
            <asp:Label ID="lbl_xLgr_instalacao" runat="server"></asp:Label>
          </td>
          <td class="label_coluna_r">Número:</td>
          <td class="label_coluna_l">
            <asp:Label ID="lbl_nro_instalacao" runat="server"></asp:Label>
          </td>
        </tr>
        <tr>
          <td class="label_coluna_r">Complemento:</td>
          <td class="style30">
            <asp:Label ID="lbl_xCpl_instalacao" runat="server"></asp:Label>
          </td>
          <td class="label_coluna_r">Bairro:</td>
          <td class="label_coluna_l">
            <asp:Label ID="lbl_xBairro_instalacao" runat="server"></asp:Label>
          </td>
        </tr>
        <tr>
          <td class="label_coluna_r">CEP:</td>
          <td class="label_coluna_l">
            <asp:Label ID="lbl_CEP_instalacao" runat="server"></asp:Label>
          </td>
          <td class="label_coluna_r">UF / Municipio:</td>
          <td class="label_coluna_l">
            <asp:Label ID="lbl_UF_instalacao" runat="server"></asp:Label>
            &nbsp;/
            <asp:Label ID="lbl_xMun_instalacao" runat="server"></asp:Label>
          </td>
        </tr>
        <tr>
          <td align="center" colspan="4" style="background-color: #CCCCCC;">Dados da Residência</td>
        </tr>
        <tr>
          <td class="label_coluna_r">Tipo Residência:</td>
          <td class="label_coluna_l">
            <asp:Label ID="lbl_tpResidencia" runat="server" Text="lbl_tpResidencia"></asp:Label>
          </td>
          <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
          <td class="label_coluna_r">Tipo de Moradia:</td>
          <td class="style30">
            <asp:Label ID="lbl_descr_tp_moradia" runat="server"></asp:Label>
          </td>
          <td class="label_coluna_r">Tratamento de água:</td>
          <td class="label_coluna_l">
            <asp:Label ID="lbl_descr_trata_agua" runat="server"></asp:Label>
          </td>
        </tr>
        <tr>
          <td class="label_coluna_r">Abastecimento de água:</td>
          <td class="style30">
            <asp:Label ID="lbl_descr_abast_agua" runat="server"></asp:Label>
          </td>
          <td class="label_coluna_r">Esgotamento Sanitário:</td>
          <td class="label_coluna_l">
            <asp:Label ID="lbl_descr_esgoto" runat="server"></asp:Label>
          </td>
        </tr>
        <tr>
          <td class="label_coluna_r">Destino Lixo:</td>
          <td class="style30">
            <asp:Label ID="lbl_descr_lixo_destino" runat="server"></asp:Label>
          </td>
          <td class="label_coluna_r">Número de Cômodos:</td>
          <td class="label_coluna_l">
            <asp:Label ID="lbl_residencia_num_comodos" runat="server"></asp:Label>
          </td>
        </tr>
        <tr>
          <td align="center" colspan="4" style="background-color: #CCCCCC; ">Filiação</td>
        </tr>
        <tr>
          <td class="label_coluna_r">Nome da mãe:</td>
          <td class="label_coluna_l">
            <asp:Label ID="lblNome_mae" runat="server" Text="lblNome_mae"></asp:Label>
          </td>
          <td class="label_coluna_r">CPF mãe:</td>
          <td class="label_coluna_l">
            <asp:Label ID="lbl_CPF_mae" runat="server" Text="lbl_CPF_mae"></asp:Label>
          </td>
        </tr>
        <tr>
          <td class="label_coluna_r">Nome do pai:</td>
          <td class="label_coluna_l">
            <asp:Label ID="lblNome_pai" runat="server" Text="lblNome_pai"></asp:Label>
          </td>
          <td class="label_coluna_r">CPF pai:</td>
          <td class="label_coluna_l">
            <asp:Label ID="lbl_CPF_pai" runat="server" Text="lbl_CPF_pai"></asp:Label>
          </td>
        </tr>
        <tr>
          <td align="center" colspan="4" style="background-color: #CCCCCC; " 
            class="style19">Contato</td>
        </tr>
        <tr>
          <td class="label_coluna_r">Telefone:</td>
          <td colspan="3" class="label_coluna_l">
            &nbsp;<asp:Label ID="lblTelefone" runat="server" Text="lblTelefone"></asp:Label>
            <span lang="pt-br">&nbsp;&nbsp; </span>Celular:
            <asp:Label ID="lbl_celular" runat="server" Text="lbl_celular"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp; Comercial:
            <asp:Label ID="lbl_tel_comercial" runat="server" Text="lbl_tel_comercial"></asp:Label>
          </td>
        </tr>
        <tr>
          <td class="label_coluna_r">E-mail:</td>
          <td colspan="3" class="label_coluna_l">
            <asp:Label ID="lbl_email" runat="server" Text="lbl_email"></asp:Label>
          </td>
        </tr>
        <tr>
          <td align="center" colspan="4" style="background-color: #CCCCCC; ">Outras Informações</td>
        </tr>
        <tr>
          <td class="label_coluna_r">Cão:</td>
          <td colspan= class="style3" class="style11">
            <asp:Label ID="lbl_conta_cao" runat="server" Text="lbl_conta_cao"></asp:Label>
          </td>
          <td class="label_coluna_r">Gato:</td>
          <td class="label_coluna_l">
            <asp:Label ID="lbl_conta_gato" runat="server" Text="lbl_conta_gato"></asp:Label>
          </td>
        </tr>
        <tr>
          <td class="label_coluna_r">Pássaro:</td>
          <td class="label_coluna_l">
            <asp:Label ID="lbl_conta_passaros" runat="server" Text="lbl_conta_passaros"></asp:Label>
          </td>
          <td class="label_coluna_r">Outros:</td>
          <td class="label_coluna_l">
            <asp:Label ID="lbl_conta_outros" runat="server" Text="lbl_conta_outros"></asp:Label>
          </td>
        </tr>
        <tr>
          <td align="center" colspan="4" style="background-color: #CCCCCC; " 
            class="style19">Documentos</td>
        </tr>
        <tr>
          <td colspan="4">
            <br />
            <asp:Button ID="btn_doc1" runat="server" CausesValidation="False" 
              EnableViewState="False" onclientclick="carrega_doc(this.value);return (false);" 
              Text="Button" UseSubmitBehavior="False" />
          &nbsp;&nbsp;
            <asp:Button ID="btn_doc2" runat="server" CausesValidation="False" 
              EnableViewState="False" onclientclick="carrega_doc(this.value);return (false);" 
              Text="Button" UseSubmitBehavior="False" />
          &nbsp;
            <asp:Button ID="btn_doc3" runat="server" CausesValidation="False" 
              EnableViewState="False" onclientclick="carrega_doc(this.value);return (false);" 
              Text="Button" UseSubmitBehavior="False" />
          &nbsp;
            <asp:Button ID="btn_doc4" runat="server" CausesValidation="False" 
              EnableViewState="False" onclientclick="carrega_doc(this.value);return (false);" 
              Text="Button" UseSubmitBehavior="False" />
          &nbsp;
            <asp:Button ID="btn_doc5" runat="server" CausesValidation="False" 
              EnableViewState="False" onclientclick="carrega_doc(this.value);return (false);" 
              Text="Button" UseSubmitBehavior="False" />
          </td>
        </tr>
      </table>
    </asp:Panel>
    </form>
</body>
</html>