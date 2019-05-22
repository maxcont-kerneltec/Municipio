<%@ Page Title="" Language="VB" MasterPageFile="Cad_MasterPage.master" AutoEventWireup="false" CodeFile="cad_cidadao_edita.aspx.vb" Inherits="cad_cidadao" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <br />
      <table style="background-color: #EFF3FB;" width="100%">
        <tr>
          <td 
            id="td_Titulo">
            <asp:Label ID="lblTitulo" runat="server" Text="Edição de Cadastro" 
              ForeColor="Black"></asp:Label>
          </td>
          <td colspan="4">alterado por:
            <asp:Label ID="lbl_nome_usuario" runat="server"></asp:Label>&nbsp;em
            <asp:Label ID="lbl_dt_alt" runat="server" Text="dt_alt"></asp:Label>
            . Local:
            <asp:Label ID="lbl_descr_local" runat="server" Text="Label"></asp:Label>
          </td>
        </tr>
      </table>
      
      <table class="style1" width="100%">
        <tr>
          <td align="center" colspan="4">
            <asp:Label ID="labERRO" runat="server" ForeColor="#FF3300" Font-Size="15pt" 
              BackColor="#FFFF99"></asp:Label>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
          </td>
        </tr>
        <tr>
          <td style="background-color: #507CD1;">
            <asp:Button ID="botCancela" runat="server" CausesValidation="False" 
              Text="Voltar" CssClass="button_t1" />
          </td>
          <td align="center" colspan="2" style="background-color: #507CD1; " 
            class="campo_cadastro_titulo">
            Dados do Cidadão:
            <asp:Label ID="lbl_id_cidadao" runat="server" Text="lbl_id_cidadao"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Button ID="bnt_anterior" runat="server" CssClass="button_tp3" 
              Text="Anterior" />
          &nbsp;<asp:Button ID="btn_proximo" runat="server" CssClass="button_tp3" 
              Text="Próximo" />
          </td>
          <td align="right" colspan="1" style="background-color: #507CD1; " 
            class="campo_cadastro_titulo">
            <span style="padding: 5px; text-align: left; display: block; float: left; vertical-align: middle;">
              <asp:Label ID="lbl_idade_YMD" runat="server" Text="Label" ForeColor="White"></asp:Label>
            </span>
            <asp:Button ID="btn_Salvar" runat="server" Text="Salvar" ValidationGroup="grp_salvar" CssClass="button_t1" />
          </td>
        </tr>

        <tr>
          <td class="label_coluna_r">Nome Registro:</td>
          <td class="td_coluna_l">
            <asp:TextBox ID="txtNome_registro" runat="server" MaxLength="100" 
              CssClass="input_texto_l1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rqrd_nome_registro" runat="server" 
              ControlToValidate="txtNome_registro" 
              ErrorMessage="Nome de registro é obrigatório." Font-Size="X-Small" 
              SetFocusOnError="True" ValidationGroup="grp_salvar" 
              CssClass="aviso_campo_erro_p"></asp:RequiredFieldValidator>
          </td>
          <td class="label_coluna_r">Nome Social:</td>
          <td class="td_coluna_l">
            <asp:TextBox ID="txtNome_Social" runat="server" Width="206px" 
              CssClass="input_texto" MaxLength="60"></asp:TextBox>
            </td>
        </tr>

        <tr>
          <td align="right" class="label_coluna_r">Cartão do SUS:</td>
          <td class="td_coluna_l">
            <asp:TextBox ID="txtSUS_num" runat="server" Text='' MaxLength="15" 
              CssClass="input_texto_t1" />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
              ControlToValidate="txtSUS_num" ErrorMessage="Atenção: número do cartão deve ter 15 posições." 
              SetFocusOnError="True" ValidationExpression="\d{15}" Font-Size="X-Small" 
              ValidationGroup="grp_salvar" CssClass="aviso_campo_erro_p"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
              ControlToValidate="txtSUS_num" ErrorMessage="Informe o número do cartão do SUS" 
              Font-Size="X-Small" SetFocusOnError="True" ValidationGroup="grp_salvar" 
              CssClass="aviso_campo_erro_p"></asp:RequiredFieldValidator>
          </td>
          <td class="label_coluna_r">&nbsp;</td>
          <td class="td_coluna_l">
            <asp:DropDownList ID="DrpDwn_fSit_Cadastro" runat="server" 
              DataSourceID="sql_situacao" DataTextField="descr_sts" 
              DataValueField="id_sts" Visible="False" CssClass="drop_down_class">
              <asp:ListItem Value="0">Ativo</asp:ListItem>
              <asp:ListItem Value="1">Aguardando Atualização</asp:ListItem>
              <asp:ListItem Value="9">Suspenso</asp:ListItem>
            </asp:DropDownList>
            <asp:SqlDataSource ID="sql_situacao" runat="server" 
              ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
              SelectCommand="SELECT [id_sts], [descr_sts] FROM [Cad_Cidado_STS] ORDER BY [descr_sts]">
            </asp:SqlDataSource>
            </td>
        </tr>

        <tr>
          <td class="label_coluna_r">CPF:</td>
          <td class="td_coluna_l">
            <asp:TextBox ID="txtCPF" runat="server" Text='' MaxLength="14" 
              CssClass="input_texto_t1" />
          </td>
          <td class="label_coluna_r">CPF Responsável:</td>
          <td class="td_coluna_l">
            <asp:TextBox ID="txt_CPF_resp" runat="server" MaxLength="14" 
              CssClass="input_texto_t1"></asp:TextBox>
            <asp:Label ID="lbl_nome_resp" runat="server" Text=""></asp:Label>
            <asp:Button ID="btn_dados_resp" runat="server" Text="Button" 
              UseSubmitBehavior="False" Visible="False" CssClass="button_tp2_t1" />
          </td>
        </tr>

        <tr>
          <td class="label_coluna_r">Número Reg. Nasc:</td>
          <td class="td_coluna_l" colspan="3">
            <asp:Label ID="lbl_resultado" runat="server" Font-Size="Large" ForeColor="Red" Visible="False"></asp:Label>
            <asp:UpdatePanel ID="updt_pnl_reg_nasc" runat="server">
              <ContentTemplate>
              <asp:DropDownList ID="drp_tipo_reg_nasc" runat="server" AutoPostBack="True" 
                style="height: 22px" CssClass="drop_down_class">
                <asp:ListItem Value="0">Antigo</asp:ListItem>
                <asp:ListItem Value="1">Novo</asp:ListItem>
              </asp:DropDownList>
              &nbsp;
              <asp:Panel ID="pnl_reg_antigo" runat="server" CssClass="style10">
              Termo:
            <asp:TextBox ID="txt_num_reg_nasc" runat="server" 
              CssClass="input_texto" MaxLength="50"></asp:TextBox>
&nbsp; Folha:
            <asp:TextBox ID="txt_num_reg_nasc_folha" runat="server" CssClass="input_texto" 
                  MaxLength="50"></asp:TextBox>
          &nbsp;&nbsp;&nbsp; Livro:
            <asp:TextBox ID="txt_num_reg_nasc_livro" runat="server" CssClass="input_texto" 
                  MaxLength="50"></asp:TextBox>
                </asp:Panel>
                <asp:Panel ID="pnl_reg_novo" runat="server" Visible="False">
                  <asp:TextBox ID="txt_cod_nac" runat="server" Width="53px" MaxLength="6" 
                    CssClass="input_texto"></asp:TextBox>&nbsp;
                  <asp:TextBox ID="txt_cod_acervo" runat="server" Width="25px" MaxLength="2" 
                    CssClass="input_texto"></asp:TextBox>&nbsp;
                  <asp:TextBox ID="txt_serv" runat="server" Width="25px" MaxLength="2" 
                    CssClass="input_texto"></asp:TextBox>&nbsp;
                  <asp:TextBox ID="txt_ano_registro" runat="server" Width="44px" MaxLength="4" 
                    CssClass="input_texto"></asp:TextBox>&nbsp;
                  <asp:TextBox ID="txt_tipo_livro" runat="server" Width="20px" MaxLength="1" 
                    CssClass="input_texto"></asp:TextBox>&nbsp;
                  <asp:TextBox ID="txt_num_livro" runat="server" Width="40px" MaxLength="5" 
                    CssClass="input_texto"></asp:TextBox>&nbsp;
                  <asp:TextBox ID="txt_folha_livro" runat="server" Width="40px" MaxLength="3" 
                    CssClass="input_texto"></asp:TextBox>&nbsp;
                  <asp:TextBox ID="txt_num_termo" runat="server" Width="72px" MaxLength="7" 
                    CssClass="input_texto"></asp:TextBox>&nbsp;
                  <asp:TextBox ID="txt_dig_verif" runat="server" Width="40px" MaxLength="2" 
                    CssClass="input_texto"></asp:TextBox>
                </asp:Panel>
              </ContentTemplate>

              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="drp_tipo_reg_nasc" EventName="SelectedIndexChanged">
                </asp:AsyncPostBackTrigger>
              </Triggers>
            </asp:UpdatePanel>
          </td>
        </tr>
        <tr>
          <td class="label_coluna_r">Data Nascimento:</td>
          <td colspan="3" class="td_coluna_l">
            <asp:TextBox ID="txtDt_nasc" runat="server" Width="136px" MaxLength="10" 
              CssClass="input_texto" ValidationGroup="grp_salvar"></asp:TextBox>
            <asp:CompareValidator ID="cvDataEmissao" runat="server" 
              ControlToValidate="txtDt_nasc" Display="Dynamic" 
              ErrorMessage="(*) Data inválida. Use dd/mm/aaaa" Font-Size="X-Small" 
              Operator="DataTypeCheck" Type="Date" SetFocusOnError="True" 
              ValidationGroup="grp_salvar" CssClass="aviso_campo_erro_p"></asp:CompareValidator>
          &nbsp;<asp:RequiredFieldValidator ID="rqrd_dt_nascimento" runat="server" 
              ErrorMessage="Informe a Data de Nascimento" Font-Size="X-Small" 
              ControlToValidate="txtDt_nasc" SetFocusOnError="True" 
              ValidationGroup="grp_salvar" CssClass="aviso_campo_erro_p"></asp:RequiredFieldValidator>
            &nbsp;&nbsp;&nbsp;&nbsp; Local Nascimento (UF/Município):<asp:DropDownList 
              ID="drp_num_reg_nasc_UF" runat="server" AutoPostBack="True" 
              DataSourceID="sql_data_UF" DataTextField="UF" DataValueField="UF" 
              CssClass="drop_down_class">
            </asp:DropDownList>
            /
            <asp:DropDownList ID="drp_num_reg_nasc_cMun" runat="server" 
              DataSourceID="sql_municipio_reg_nasc" DataTextField="xMun" 
              DataValueField="cMun" CssClass="drop_down_class">
            </asp:DropDownList>
            <asp:SqlDataSource ID="sql_municipio_reg_nasc" runat="server" 
              ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
              SelectCommand="SELECT [cMun], [xMun] FROM [tbCad_Municipios] WHERE ([UF] = @UF) ORDER BY [xMun]">
              <SelectParameters>
                <asp:ControlParameter ControlID="drp_num_reg_nasc_UF" Name="UF" 
                  PropertyName="SelectedValue" Type="String" />
              </SelectParameters>
            </asp:SqlDataSource>
          </td>
        </tr>
        <tr>
          <td class="label_coluna_r">RG:</td>
          <td class="td_coluna_l" colspan="3">
            <asp:TextBox ID="txtRG" runat="server" MaxLength="20" CssClass="input_texto_t1"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Orgão Emissor:
            <asp:TextBox ID="txt_RG_orgao" runat="server" Width="55px" MaxLength="3" 
              CssClass="input_texto"></asp:TextBox>
          &nbsp;&nbsp; UF Emissão:
            <asp:DropDownList ID="drp_RG_UF" runat="server" DataSourceID="sql_data_UF" 
              DataTextField="UF" DataValueField="UF" CssClass="drop_down_class">
            </asp:DropDownList>
            <asp:SqlDataSource ID="sql_data_UF" runat="server" 
              ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
              
              SelectCommand="SELECT [UF] FROM [tbUF] WHERE ([UF] &lt;&gt; @UF) ORDER BY [UF]">
              <SelectParameters>
                <asp:Parameter DefaultValue="--" Name="UF" Type="String" />
              </SelectParameters>
            </asp:SqlDataSource>
          </td>
        </tr>

        <tr>
          <td class="label_coluna_r">Número Título:</td>
          <td class="td_coluna_l">
            <asp:TextBox ID="txtTitulo_eleitor" runat="server" CssClass="input_texto"></asp:TextBox>
            &nbsp; Zona:
            <asp:TextBox ID="txttitulo_zona" runat="server" CssClass="input_texto_S1"></asp:TextBox>
          &nbsp;&nbsp;&nbsp; Seção:<asp:TextBox ID="txttitulo_Secao" runat="server" 
              CssClass="input_texto_S1"></asp:TextBox>
          </td>
          <td>&nbsp;</td>
          <td align="left">&nbsp;</td>
        </tr>

        <tr>
          <td class="label_coluna_r">Registro de Estudante:</td>
          <td class="td_coluna_l">
            <asp:TextBox ID="txt_reg_estudante" runat="server" CssClass="input_texto" 
              MaxLength="30"></asp:TextBox>
          </td>
          <td>&nbsp;</td>
          <td align="left">&nbsp;</td>
        </tr>
        <tr>
          <td class="label_coluna_r">Funcionário da Prefeitura?</td>
          <td class="td_coluna_l">
            <asp:DropDownList ID="drp_PM_Matricula_ref" runat="server" 
              CssClass="drop_down_class">
              <asp:ListItem Value="0">não</asp:ListItem>
              <asp:ListItem Value="1">Funcionário</asp:ListItem>
              <asp:ListItem Value="2">Dependente</asp:ListItem>
            </asp:DropDownList>
            &nbsp;
          </td>
          <td class="label_coluna_r">CPF do funcionário: </td>
          <td class="td_coluna_l">
            <asp:TextBox ID="txt_PM_CPF" runat="server" MaxLength="14" 
              CssClass="input_texto_t1"></asp:TextBox>
            </td>
        </tr>
        <tr>
          <td class="label_coluna_r">Sexo:</td>
          <td class="td_coluna_l">
            <asp:DropDownList ID="DrpSexo" runat="server" Height="26px" 
              CssClass="drop_down_class" >
              <asp:ListItem Value="M">Masculino</asp:ListItem>
              <asp:ListItem Value="F">Feminino</asp:ListItem>
            </asp:DropDownList></td>
          <td align="right" class="style15">Estado Civil:</td>
          <td class="style15">
            <asp:DropDownList ID="Drp_id_estado_civil" runat="server" 
              DataSourceID="sql_estado_civil" DataTextField="descr" 
              DataValueField="id_estado_civil" CssClass="drop_down_class">
            </asp:DropDownList>
            <asp:SqlDataSource ID="sql_estado_civil" runat="server" 
              ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
              SelectCommand="SELECT [id_estado_civil], [descr] FROM [tbEstado_Civil] ORDER BY [descr]">
            </asp:SqlDataSource>
          </td>
        </tr>
        <tr>
          <td class="label_coluna_r">Raça / Cor:</td>
          <td class="td_coluna_l">
            <asp:DropDownList ID="Drp_raca" runat="server" 
              DataSourceID="sql_raca" DataTextField="descr" DataValueField="id_raca" 
              CssClass="drop_down_class">
            </asp:DropDownList>
            <asp:SqlDataSource ID="sql_raca" runat="server" 
              ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
              SelectCommand="SELECT [id_raca], [descr] FROM [tbRaca_Cor] ORDER BY [descr]">
            </asp:SqlDataSource>
          </td>
          <td class="label_coluna_r">Etnia:</td>
          <td class="td_coluna_l">
            <asp:DropDownList ID="drp_id_etnia" runat="server" DataSourceID="sql_etnias" 
              DataTextField="decr_etnia" DataValueField="id_etnia" 
              CssClass="drop_down_class">
            </asp:DropDownList>
            <asp:SqlDataSource ID="sql_etnias" runat="server" 
              ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
              
              SelectCommand="SELECT [id_etnia], left(decr_etnia, 50) as decr_etnia FROM [tbEtnias] ORDER BY [decr_etnia]">
            </asp:SqlDataSource>
          </td>
        </tr>
        <tr>
          <td align="right" class="label_coluna_r">Religião:</td>
          <td class="td_coluna_l">
            <asp:DropDownList ID="drp_id_religiao" runat="server" 
              DataSourceID="sql_religiao" DataTextField="descr_religiao" 
              DataValueField="id_religiao" CssClass="drop_down_class">
            </asp:DropDownList>
            <asp:SqlDataSource ID="sql_religiao" runat="server" 
              ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
              SelectCommand="SELECT [id_religiao], [descr_religiao] FROM [tbReligiao] ORDER BY [descr_religiao]">
            </asp:SqlDataSource>
          </td>
          <td align="right">&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td class="label_coluna_r">Escolaridade:</td>
          <td class="td_coluna_l">
            <asp:DropDownList ID="Drp_escolaridade" runat="server" 
              DataSourceID="sql_escolaridade" DataTextField="descr" 
              DataValueField="id_escolaridade" CssClass="drop_down_class">
            </asp:DropDownList>
            <asp:SqlDataSource ID="sql_escolaridade" runat="server" 
              ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
              SelectCommand="SELECT [id_escolaridade], [descr] FROM [tbEscolaridade] ORDER BY [descr]">
            </asp:SqlDataSource>
          </td>
        </tr>
        <tr>
          <td class="label_coluna_r">Renda Familiar:</td>
          <td>
            <asp:DropDownList ID="drp_renda_familiar" runat="server" 
              DataSourceID="sql_dt_renda_familiar" DataTextField="descr_renda_familiar" 
              DataValueField="id_renda_familiar" CssClass="drop_down_class">
            </asp:DropDownList>
            <asp:SqlDataSource ID="sql_dt_renda_familiar" runat="server" 
              ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
              SelectCommand="SELECT [id_renda_familiar], [descr_renda_familiar] FROM [Cad_Cidadao_Renda_Familiar]">
            </asp:SqlDataSource>
            <asp:RangeValidator ID="RangeValidator1" runat="server" 
              ControlToValidate="drp_renda_familiar" CssClass="aviso_campo_erro_p" 
              ErrorMessage="Informe a renda familiar." MaximumValue="99" MinimumValue="1" 
              SetFocusOnError="True" ValidationGroup="grp_salvar"></asp:RangeValidator>
          </td>
        </tr>
        <tr>
          <td align="right" class="label_coluna_r">Profissão:</td>
          <td colspan="3">
            <asp:UpdatePanel ID="updt_pnl_profissao" runat="server">
              <ContentTemplate>
                <asp:DropDownList ID="drp_cod_cbo" runat="server" 
                  DataSourceID="sql_cbo_profissoes" DataTextField="descr_cbo" 
                  DataValueField="cod_cbo" Height="20px" CssClass="drop_down_class">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sql_cbo_profissoes" runat="server" 
                  ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
                  SelectCommand="SELECT cod_cbo, LEFT (descr_cbo, 50) AS descr_cbo FROM tb_CBO_Profissoes ORDER BY descr_cbo">
                </asp:SqlDataSource>
                &nbsp;&nbsp;&nbsp; Pesquisar Profissão:
                <asp:TextBox ID="txt_descr_profissao" runat="server" 
                  ValidationGroup="valida_profissao" CssClass="input_texto_l1"></asp:TextBox>
                <asp:Button ID="btn_profissao_pesq" runat="server" Text="Pesquisar" 
                  ValidationGroup="valida_profissao" CssClass="button_t1" />
                <asp:RequiredFieldValidator ID="rqrd_profissao_pesq" runat="server" 
                  ControlToValidate="txt_descr_profissao" Display="Dynamic" 
                  ErrorMessage="Informe uma descrição e clique em Pesquisar." 
                  ValidationGroup="valida_profissao" CssClass="aviso_campo_erro_p"></asp:RequiredFieldValidator>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                  AssociatedUpdatePanelID="updt_pnl_profissao" DisplayAfter="100">
                  <ProgressTemplate>
                    <asp:Label ID="Label1" runat="server" ForeColor="#CC3300" 
                      Text="Pesquisando profissões, aguarde..."></asp:Label>
                  </ProgressTemplate>
                </asp:UpdateProgress>
              </ContentTemplate>
            </asp:UpdatePanel>
          </td>
        </tr>
        <tr>
          <td align="right" class="label_coluna_r">Deficiente?</td>
          <td colspan="3">
            <asp:DropDownList ID="drp_fDef" runat="server" CssClass="drop_down_class">
              <asp:ListItem Value="N">Não</asp:ListItem>
              <asp:ListItem Value="S">Sim</asp:ListItem>
            </asp:DropDownList>
            Deficiência:
            <asp:TextBox ID="txt_Def_descr" runat="server" 
              CssClass="input_texto_l1"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td class="label_coluna_r">Nome Conjuge: </td>
          <td class="td_coluna_l">
            <asp:TextBox ID="txt_nome_conjuge" runat="server" 
              CssClass="input_texto_l1" MaxLength="100"></asp:TextBox>
          </td>
          <td class="label_coluna_r">CPF Conjuge:</td>
          <td class="td_coluna_l">
            <asp:TextBox ID="txt_CPF_conjuge" runat="server" MaxLength="14" 
              CssClass="input_texto_t1"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td colspan="5" class="campo_cadastro_titulo">
            Endereço - informações a partir da conta de energia elétrica</td>
        </tr>
        <tr>
          <td id="tbLinha_End_Aviso" colspan="4">
            <asp:Label ID="lbl_aviso_instala" runat="server" 
              
              Text="Para atualizar o endereço, informe o Número da Instalação e clique em Editar." 
              ForeColor="#0000CC"></asp:Label>
&nbsp;</td>
        </tr>
        <tr>
          <td align="right" class="label_coluna_r">Número Instalação:</td>
          <td>
            <asp:UpdatePanel ID="updt_pnl_endereco" runat="server">
              <ContentTemplate>
                <asp:TextBox ID="txt_num_instalacao" runat="server" CssClass="input_texto" 
                  MaxLength="15"></asp:TextBox>
                <asp:Button ID="btn_Atualiza_Endereco" runat="server" Text="Editar" 
                  CausesValidation="False" UseSubmitBehavior="False" 
                  ValidationGroup="grp_num_instala" CssClass="button_t1" />
                &nbsp;&nbsp;<asp:Label ID="lbl_num_cidadao" runat="server"></asp:Label>
                <asp:Button ID="btn_endereco_cidadao" runat="server" Text="Visualizar" 
                  CausesValidation="False" UseSubmitBehavior="False" 
                  ToolTip="Clique aqui para visualizar..." CssClass="button_tp2_t1" 
                  Visible="False" />
    &nbsp;<asp:RequiredFieldValidator ID="rqrd_num_instalacao" runat="server" 
                  ControlToValidate="txt_num_instalacao" 
                  ErrorMessage="Número da Instalação obrigatório" Font-Size="X-Small" 
                  SetFocusOnError="True" ValidationGroup="grp_num_instala" 
                  CssClass="aviso_campo_erro_p"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="rqrd_dados_endereco" runat="server" 
                  ControlToValidate="txt_num_instalacao" 
                  ErrorMessage="Informe o número da instalação e clique em Localizar." 
                  Font-Size="X-Small" SetFocusOnError="True" ValidationGroup="grp_salvar" 
                  CssClass="aviso_campo_erro_p"></asp:RequiredFieldValidator>
                <asp:Label ID="lbl_erro_instalacao" runat="server" ForeColor="Red" 
                  Text="lbl_erro" Visible="False" CssClass="aviso_campo_erro_p"></asp:Label>
              </ContentTemplate>
            </asp:UpdatePanel>
          </td>
          <td align="right" class="label_coluna_r">&nbsp;</td>
          <td align="left">&nbsp;</td>
        </tr>
        <tr>
          <td align="center" colspan="4">
            <asp:UpdatePanel ID="upd_pnl_endereco" runat="server" UpdateMode="Conditional">
              <ContentTemplate>
                <asp:Panel ID="pnf_endereco_visualiza" runat="server">
                  <table style="width:85%;">
                    <tr>
                      <td class="label_coluna_r">CPF / CNPJ Instalação:</td>
                      <td class="td_coluna_l">
                        <asp:Label ID="lbl_cnpj_instalacao" runat="server" CssClass="label_coluna_l"></asp:Label>
                      </td>
                      <td class="label_coluna_r">Data de Leitura da conta:</td>
                      <td class="td_coluna_l">
                        <asp:Label ID="lbl_num_instala_dt_emissao" runat="server" CssClass="label_coluna_l"></asp:Label>
                      </td>
                    </tr>
                    <tr>
                      <td class="label_coluna_r">Número Inscrição - IPTU:</td>
                      <td class="td_coluna_l">
                        <asp:Label ID="lbl_num_inscr_imovel" runat="server" CssClass="label_coluna_l"></asp:Label>
                      </td>
                      <td class="label_coluna_r">Matrícula do Imóvel:</td>
                      <td class="td_coluna_l">
                        <asp:Label ID="lbl_num_matri_imovel" runat="server" CssClass="label_coluna_l"></asp:Label>
                      </td>
                    </tr>
                    <tr>
                      <td class="label_coluna_r">Logradouro:</td>
                      <td class="td_coluna_l">
                        <asp:Label ID="lbl_xLgr_instalacao" runat="server" CssClass="label_coluna_l"></asp:Label>
                      </td>
                      <td class="label_coluna_r">Número:</td>
                      <td class="td_coluna_l">
                        <asp:Label ID="lbl_nro_instalacao" runat="server" CssClass="label_coluna_l"></asp:Label>
                      </td>
                    </tr>
                    <tr>
                      <td class="label_coluna_r">Complemento:</td>
                      <td class="style30">
                        <asp:Label ID="lbl_xCpl_instalacao" runat="server" CssClass="label_coluna_l"></asp:Label>
                      </td>
                      <td class="label_coluna_r">Bairro:</td>
                      <td class="td_coluna_l">
                        <asp:Label ID="lbl_xBairro_instalacao" runat="server" CssClass="label_coluna_l"></asp:Label>
                      </td>
                    </tr>
                    <tr>
                      <td class="label_coluna_r">CEP:</td>
                      <td class="td_coluna_l">
                        <asp:Label ID="lbl_CEP_instalacao" runat="server" CssClass="label_coluna_l"></asp:Label>
                      </td>
                      <td class="label_coluna_r">UF / Municipio:</td>
                      <td class="td_coluna_l">
                        <asp:Label ID="lbl_UF_instalacao" runat="server" CssClass="label_coluna_l"></asp:Label>
                        &nbsp;/
                        <asp:Label ID="lbl_xMun_instalacao" runat="server" CssClass="label_coluna_l"></asp:Label>
                      </td>
                    </tr>
                  </table>
                </asp:Panel>

                <asp:Panel ID="pnf_atualiza_endereco" runat="server" Visible="False">
                  <table style="width:100%;">
                    <tr>
                      <td class="label_coluna_r">CPF / CNPJ Instalação:</td>
                      <td class="td_coluna_l">
                        <asp:TextBox ID="txt_cpf_instalacao" runat="server" 
                          MaxLength="14" CssClass="input_texto_t1"></asp:TextBox>
                      </td>
                      <td class="label_coluna_r">Data de emissão da conta:</td>
                      <td class="td_coluna_l">
                        <asp:TextBox ID="txtnum_instala_dt_emissao" runat="server" 
                          CssClass="input_texto_t1" MaxLength="10" ValidationGroup="grp_salvar"></asp:TextBox>
                        <asp:CompareValidator ID="cvnum_instala_dt_leitura" runat="server" 
                          ControlToValidate="txtnum_instala_dt_emissao" Display="Dynamic" 
                          ErrorMessage="(*) Data inválida. Use dd/mm/aaaa" Font-Size="X-Small" 
                          Operator="DataTypeCheck" SetFocusOnError="True" Type="Date" 
                          ValidationGroup="grp_salvar" CssClass="aviso_campo_erro_p"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="rqrd_num_instala_dt_leitura" runat="server" 
                          ControlToValidate="txtnum_instala_dt_emissao" 
                          ErrorMessage="Informe a Data de Emissão da conta." Font-Size="X-Small" 
                          SetFocusOnError="True" ValidationGroup="grp_salvar" 
                          CssClass="aviso_campo_erro_p"></asp:RequiredFieldValidator>
                      </td>
                    </tr>
                    <tr>
                      <td class="label_coluna_r">Número Inscrição - IPTU:</td>
                      <td class="td_coluna_l">
                        <asp:TextBox ID="txt_num_inscr_imovel" runat="server" CssClass="input_texto_t1"></asp:TextBox>
                      </td>
                      <td class="label_coluna_r">Matrícula do Imóvel:</td>
                      <td class="td_coluna_l">
                        <asp:TextBox ID="txt_num_matri_imovel" runat="server" CssClass="input_texto_t1" 
                          Width="124px"></asp:TextBox>
                      </td>
                    </tr>
                    <tr>
                      <td class="label_coluna_r">Logradouro:</td>
                      <td class="td_coluna_l">
                        <asp:TextBox ID="txt_xLgr_instalacao" runat="server" 
                          CssClass="input_texto_l1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqrd_xLgr_instalacao" runat="server" 
                          ControlToValidate="txt_xLgr_instalacao" 
                          ErrorMessage="Logradouro da instalação é obrigatório." Font-Size="X-Small" 
                          SetFocusOnError="True" ValidationGroup="grp_salvar" 
                          CssClass="aviso_campo_erro_p"></asp:RequiredFieldValidator>
                      </td>
                      <td class="label_coluna_r">Número:</td>
                      <td class="td_coluna_l">
                        <asp:TextBox ID="txt_nro_instalacao" runat="server" CssClass="input_texto_t1"></asp:TextBox>
                      </td>
                    </tr>
                    <tr>
                      <td class="label_coluna_r">Complemento:</td>
                      <td class="td_coluna_l">
                        <asp:TextBox ID="txt_xCpl_instalacao" runat="server" 
                          CssClass="input_texto_l1"></asp:TextBox>
                      </td>
                      <td class="label_coluna_r">Bairro:</td>
                      <td class="td_coluna_l">
                        <asp:TextBox ID="txt_xBairro_instalacao" runat="server" 
                          CssClass="input_texto_l1"></asp:TextBox>
                      </td>
                    </tr>
                    <tr>
                      <td class="label_coluna_r">CEP:</td>
                      <td class="td_coluna_l">
                        <asp:TextBox ID="txt_CEP_instalacao" runat="server" CssClass="input_texto"></asp:TextBox>
                      </td>
                      <td class="label_coluna_r">UF / Municipio:</td>
                      <td class="td_coluna_l">
                        <asp:DropDownList ID="drp_UF_instalacao" runat="server" AutoPostBack="True" 
                          DataSourceID="sql_data_UF" DataTextField="UF" DataValueField="UF">
                        </asp:DropDownList>
                        <asp:DropDownList ID="drp_cMun_instalacao" runat="server" 
                          DataSourceID="sql_data_municipio" DataTextField="xMun" DataValueField="cMun">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sql_data_municipio" runat="server" 
                          ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
                          SelectCommand="SELECT [cMun], [xMun] FROM [tbCad_Municipios] WHERE ([UF] = @UF)">
                          <SelectParameters>
                            <asp:ControlParameter ControlID="drp_UF_instalacao" Name="UF" 
                              PropertyName="SelectedValue" Type="String" />
                          </SelectParameters>
                        </asp:SqlDataSource>
                      </td>
                    </tr>
                  </table>
                </asp:Panel>

              </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_Atualiza_Endereco" EventName="Click">
                </asp:AsyncPostBackTrigger>
              </Triggers>
            </asp:UpdatePanel>
          </td>
        </tr>
        <tr>
          <td align="center" colspan="5" style="background-color: #507CD1; " 
            class="campo_cadastro_titulo">Dados Residência</td>
        </tr>
        <tr>
          <td align="right" class="label_coluna_r">Tipo de Residência:</td>
          <td class="td_coluna_l">
            <asp:DropDownList ID="drptp_Residencia" runat="server" 
              DataSourceID="sqldt_tpResidencia" DataTextField="descr_tpResidencia" 
              DataValueField="tpResidencia" CssClass="drop_down_class">
            </asp:DropDownList>

            <asp:SqlDataSource ID="sqldt_tpResidencia" runat="server" 
              ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
              SelectCommand="SELECT [tpResidencia], [descr_tpResidencia] FROM [Cad_Cidadao_tpResidencia] ORDER BY [descr_tpResidencia]">
            </asp:SqlDataSource>
          </td>
          <td class="label_coluna_r">&nbsp;</td>
          <td align="left">&nbsp;</td>
        </tr>
        <tr>
          <td class="label_coluna_r">Tipo de Moradia:</td>
          <td class="td_coluna_l">
            <asp:DropDownList ID="drp_tp_moradia" runat="server" 
              DataSourceID="sql_dt_tp_moradia" DataTextField="descr_tp_moradia" 
              DataValueField="id_tp_moradia" CssClass="drop_down_class">
            </asp:DropDownList>
            <asp:SqlDataSource ID="sql_dt_tp_moradia" runat="server" 
              ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
              SelectCommand="SELECT [id_tp_moradia], [descr_tp_moradia] FROM [Cad_Cidadao_tp_Moradia] ORDER BY [descr_tp_moradia]">
            </asp:SqlDataSource>
          </td>
          <td class="label_coluna_r">Tratamento de Água:</td>
          <td class="td_coluna_l">
            <asp:DropDownList ID="drp_trata_agua" runat="server" 
              DataSourceID="sql_dt_trata_agua" DataTextField="descr_trata_agua" 
              DataValueField="id_trata_agua" CssClass="drop_down_class">
            </asp:DropDownList>
            <asp:SqlDataSource ID="sql_dt_trata_agua" runat="server" 
              ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
              SelectCommand="SELECT [id_trata_agua], [descr_trata_agua] FROM [Cad_Cidadao_Trata_Agua] ORDER BY [descr_trata_agua]">
            </asp:SqlDataSource>

          </td>
        </tr>
        <tr>
          <td class="label_coluna_r">Abastecimento Água:</td>
          <td class="td_coluna_l">
            <asp:DropDownList ID="drp_abast_agua" runat="server" 
              DataSourceID="sql_dt_abast_agua" DataTextField="descr_abast_agua" 
              DataValueField="id_abast_agua" CssClass="drop_down_class">
            </asp:DropDownList>
            <asp:SqlDataSource ID="sql_dt_abast_agua" runat="server" 
              ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
              SelectCommand="SELECT [id_abast_agua], [descr_abast_agua] FROM [Cad_Cidadao_Abast_Agua] ORDER BY [descr_abast_agua]">
            </asp:SqlDataSource>
          </td>
          <td class="label_coluna_r">Esgotamento Sanitário:</td>
          <td align="left" class="td_coluna_l">
            <asp:DropDownList ID="drp_esgoto" runat="server" DataSourceID="sql_dt_esgoto" 
              DataTextField="descr_esgoto" DataValueField="id_esgoto" 
              CssClass="drop_down_class">
            </asp:DropDownList>
            <asp:SqlDataSource ID="sql_dt_esgoto" runat="server" 
              ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
              SelectCommand="SELECT [id_esgoto], [descr_esgoto] FROM [Cad_Cidadao_Esgoto] ORDER BY [descr_esgoto]">
            </asp:SqlDataSource>

          </td>
        </tr>
        <tr>
          <td class="label_coluna_r">Destino Lixo:</td>
          <td class="td_coluna_l">
            <asp:DropDownList ID="drp_lixo_destino" runat="server" 
              DataSourceID="sql_dt_lixo_destino" DataTextField="descr_lixo_destino" 
              DataValueField="id_lixo_destino" CssClass="drop_down_class">
            </asp:DropDownList>
            <asp:SqlDataSource ID="sql_dt_lixo_destino" runat="server" 
              ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
              SelectCommand="SELECT [id_lixo_destino], [descr_lixo_destino] FROM [Cad_Cidadao_Lixo_Destino] ORDER BY [descr_lixo_destino]">
            </asp:SqlDataSource>
          </td>
          <td class="label_coluna_r">Número de Cômodos:</td>
          <td class="td_coluna_l">
            <asp:TextBox ID="txt_residencia_num_comodos" runat="server" 
              CssClass="input_texto_S1"></asp:TextBox>
          </td>
        </tr>

        <tr>
          <td class="label_coluna_r">&nbsp;</td>
          <td colspan="4">&nbsp;</td>
        </tr>
        <tr>
          <td align="center" colspan="5" style="background-color: #507CD1; " 
            class="campo_cadastro_titulo">Filiação</td>
        </tr>
        <tr>
          <td class="label_coluna_r">Nome da mãe:</td>
          <td>
            <asp:TextBox ID="txtNome_mae" runat="server" 
              CssClass="input_texto_l1" MaxLength="100"></asp:TextBox>
          </td>
          <td class="label_coluna_r">CPF mãe:</td>
          <td class="td_coluna_l">
            <asp:TextBox ID="txt_CPF_mae" runat="server" MaxLength="14" 
              CssClass="input_texto_t1"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td align="right" class="label_coluna_r">Nome do pai:</td>
          <td class="td_coluna_l">
            <asp:TextBox ID="txtNome_pai" runat="server" 
              CssClass="input_texto_l1" MaxLength="100"></asp:TextBox>
          </td>
          <td class="label_coluna_r">CPF pai:</td>
          <td class="td_coluna_l">
            <asp:TextBox ID="txt_CPF_pai" runat="server" MaxLength="14" 
              CssClass="input_texto_t1"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td colspan="4" class="campo_cadastro_titulo">Contato</td>
        </tr>
        <tr>
          <td class="label_coluna_r">Telefone:</td>
          <td colspan="3" class="td_coluna_l">
            <asp:TextBox ID="txtTelefone" runat="server" CssClass="input_texto_t1"></asp:TextBox>
            &nbsp;Celular:
            <asp:TextBox ID="txt_celular" runat="server" CssClass="input_texto_t1"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp; Comercial:
            <asp:TextBox ID="txt_tel_comercial" runat="server" CssClass="input_texto_t1"></asp:TextBox>
          </td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td class="label_coluna_r">E-mail:</td>
          <td colspan="3" class="td_coluna_l">
            <asp:TextBox ID="txtEmail" runat="server" CssClass="input_texto_l1"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td colspan="4" class="campo_cadastro_titulo">Outras Informações</td>
        </tr>
        <tr>
          <td class="label_coluna_r">Cão:</td>
          <td class="td_coluna_l">
            <asp:TextBox ID="txt_conta_cao" runat="server" CssClass="input_texto_S1"></asp:TextBox>
          </td>
          <td class="label_coluna_r">Gato:</td>
          <td class="td_coluna_l">
            <asp:TextBox ID="txt_conta_gato" runat="server" MaxLength="3" 
              CssClass="input_texto_S1"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td class="label_coluna_r">Pássaro:</td>
          <td class="td_coluna_l">
            <asp:TextBox ID="txt_conta_passaros" runat="server" MaxLength="3" 
              CssClass="input_texto_S1"></asp:TextBox>
          </td>
          <td class="label_coluna_r">Outros:</td>
          <td align="left">
            <asp:TextBox ID="txt_conta_outros" runat="server" MaxLength="3" 
              CssClass="input_texto_S1"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td colspan="4">
            Informe a quantidade de animais na residência</td>
        </tr>
        <tr>
          <td colspan="4" class="campo_cadastro_titulo">Armazenar documentos</td>
        </tr>
        <tr>
          <td colspan="2" class="td_coluna_l">
            Documento:
            Localize o arquivo que será enviado.
            <asp:FileUpload ID="fld_upld_arquivo" runat="server" />
            &nbsp;&nbsp;<asp:Button ID="btn_envia_arq" runat="server" 
              Text="Enviar" CssClass="button_t1" />
          &nbsp;&nbsp;
            <asp:Label ID="lbl_aviso_envia" runat="server" Text="Label" Visible="False"></asp:Label>
            <br />
            <asp:Button ID="btn_doc1" runat="server" CausesValidation="False" 
              EnableViewState="False" onclientclick="carrega_doc(this.value);return (false);" 
              Text="Button" UseSubmitBehavior="False" CssClass="button_tp2_t2" />
          &nbsp;&nbsp;
            <asp:Button ID="btn_doc2" runat="server" CausesValidation="False" 
              EnableViewState="False" onclientclick="carrega_doc(this.value);return (false);" 
              Text="Button" UseSubmitBehavior="False" CssClass="button_tp2_t2" />
          &nbsp;
            <asp:Button ID="btn_doc3" runat="server" CausesValidation="False" 
              EnableViewState="False" onclientclick="carrega_doc(this.value);return (false);" 
              Text="Button" UseSubmitBehavior="False" CssClass="button_tp2_t2" />
          &nbsp;
            <asp:Button ID="btn_doc4" runat="server" CausesValidation="False" 
              EnableViewState="False" onclientclick="carrega_doc(this.value);return (false);" 
              Text="Button" UseSubmitBehavior="False" CssClass="button_tp2_t2" />
          &nbsp;
            <asp:Button ID="btn_doc5" runat="server" CausesValidation="False" 
              EnableViewState="False" onclientclick="carrega_doc(this.value);return (false);" 
              Text="Button" UseSubmitBehavior="False" CssClass="button_tp2_t2" />
          </td>
          <td colspan="2" align="center">
            &nbsp;&nbsp;&nbsp; Envio de Foto:
            <asp:FileUpload ID="fld_up_foto" runat="server" />
            <asp:Button ID="btn_envia_foto" runat="server" Text="Enviar Foto" 
              CssClass="button_t1" /><br />
            &nbsp;<asp:Button ID="btn_visualiza_foto" runat="server" Text="Visualizar" 
              CssClass="button_tp2_t1" />
            <br />
          </td>
        </tr>

        <tr>
          <td class="campo_cadastro_titulo">
            <asp:Button ID="botCancela0" runat="server" CausesValidation="False" 
              Text="Voltar" CssClass="button_t1" />
          </td>
          <td class="campo_cadastro_titulo">
            <asp:Panel ID="pnl_botao_imprimir" runat="server">
              <input id="btn_imprimir" onclick="return btn_imprimir_onclick()" type="button" 
                value="Protocolo"
                class="button_tp2_t1" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <input id="btn_imprimir_completo" type="button" value="Imprimir" 
                onclick="return btn_imprimir_completo_onclick()" class="button_tp2_t1" /></asp:Panel>
          </td>
          <td class="campo_cadastro_titulo" colspan="2">
            <asp:Button ID="btn_Salvar_2" runat="server" Text="Salvar" 
              ValidationGroup="grp_salvar" CssClass="button_t1" />
          </td>
        </tr>
      </table>
  <br />

<script language="javascript" type="text/javascript">
// <!CDATA[
  function btn_imprimir_resp(id_cidadao_resp) {
  window.open('cad_cidadao_imprime.aspx?tpImp=COMP&id_cidadao=' + id_cidadao_resp, '', 'toolbar=yes, resizable=yes, scrollbars=yes, width=670px, height=500px')
  return false;
}

function btn_imprimir_onclick() {
  window.open('cad_cidadao_imprime.aspx?tpImp=PROT', '', 'toolbar=yes, resizable=yes, scrollbars=yes, width=670px, height=500px')
}

function carrega_doc(doc) {
  window.open('docs/<%=Session("id_municipio") %>/<%=right("0000000" & Session("id_cidadao"), 7) %>/' + doc, '', 'toolbar=yes, resizable=yes, scrollbars=yes, width=670px, height=500px')
}

function btn_visualza_end_cidadao(num_instalacao) {
  window.open('rel_endereco_cidadao.aspx?num_instalacao=' + num_instalacao, '', 'toolbar=yes, resizable=yes, scrollbars=yes, width=670px, height=500px')
}

function btn_imprimir_completo_onclick() {
  window.open('cad_cidadao_imprime.aspx?tpImp=COMP', '', 'toolbar=yes, resizable=yes, scrollbars=yes, width=670px, height=500px')
}

function carrega_foto(doc) {
  window.open('docs/<%=Session("id_municipio") %>/<%=right("0000000" & Session("id_cidadao"), 7) %>/fotos/' + doc, '', 'toolbar=yes, resizable=yes, scrollbars=yes, width=670px, height=500px')
}

// ]]>
</script>
</asp:Content>
