<%@ Page Title="" Language="VB" MasterPageFile="Cad_MasterPage.master" AutoEventWireup="false" CodeFile="rel_estatisticas.aspx.vb" Inherits="cad_inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <link href="../StyleSheet.css" rel="stylesheet" type="text/css" />
  <style type="text/css">
    .style9
    {
      text-align: left;
      width: 200px;
    }
    .style10
    {
      text-align: right;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
      ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
      SelectCommand="sp1Pega_Dados_Inicio" SelectCommandType="StoredProcedure">
      <SelectParameters>
        <asp:SessionParameter Name="sp_id_municipio" SessionField="id_municipio" 
          Type="String" />
        <asp:SessionParameter Name="spid_usuario" SessionField="id_usuario" 
          Type="String" />
      </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <br />
<table style="border: 1px solid #000000; width:100%; border-collapse: collapse; display: none;" 
      bgcolor="#DFDFDF" cellpadding="2" cellspacing="1" border="1">
  <tr>
    <td class="style10" width="25%">Nome:</td>
    <td class="style7" width="25%">
      <asp:Label ID="lbl_nome" runat="server" Text="Label"></asp:Label>
    </td>
    <td class="style10" width="25%">CPF:</td>
    <td class="style8" width="25%">
      <asp:Label ID="lbl_CPF" runat="server" Text="Label"></asp:Label>
    </td>
  </tr>
  <tr>
    <td class="style10">
      Último Acesso:
      </td>
    <td class="style7">
      <asp:Label ID="lbl_ult_acesso" runat="server" Text="Label"></asp:Label>
    </td>
    <td class="style10">
      Número de Acessos:
      </td>
    <td class="style8">
      <asp:Label ID="lbl_conta_acessos" runat="server" Text="Label"></asp:Label>
    </td>
  </tr>
  <tr>
    <td class="style10">
      Telefone:
      </td>
    <td class="style7">
      <asp:Label ID="lbl_telefone" runat="server" Text="Label"></asp:Label>&nbsp;
    </td>
    <td class="style10">
      E-mail:
      </td>
    <td class="style8">
      <asp:Label ID="lbl_email" runat="server" Text="Label"></asp:Label>
    </td>
  </tr>
  </table>
<br /><br />

<table style="border: 1px solid #000000; width:100%; border-collapse: collapse; border-spacing: 1px;" 
      bgcolor="#DFDFDF" cellpadding="2" cellspacing="1" border="1">
  <tr>
    <td colspan="4" align="center">Estatísticas</td>
  </tr>

  <tr>
    <td class="style10" width="25%">Cidadãos Cadastrados:</td>
    <td class="style9" width="25%">
      <asp:Label ID="lbl_cidadao" runat="server" Text="Label"></asp:Label>
      &nbsp;&nbsp;
      <asp:Button ID="btn_consulta_cidadaos" runat="server" CssClass="button_claro" 
        Text="Consultar" BorderWidth="1px" />
    </td>
    <td width="25%" align="right">Eleitores do Município:</td>
    <td width="25%">
      <asp:Label ID="lbl_conta_eleitores_municipio" runat="server" Text="Label"></asp:Label>
    &nbsp;(<asp:Label ID="lbl_perc_eleitor_municipio" runat="server" Text="Label"></asp:Label>
      %)</td>
  </tr>

  <tr>
    <td class="style10">
      Moradores fora do município:
      </td>
    <td class="style9">
      <asp:Label ID="lbl_conta_cidadao_fora" runat="server" Text="Label"></asp:Label>
    &nbsp;(<asp:Label ID="lbl_perc_morador_fora" runat="server" Text="Label"></asp:Label>
      %)</td>
    <td class="style10">
      Eleitores fora do município:
      </td>
    <td class="style8">
      <asp:Label ID="lbl_conta_eleitores_fora" runat="server" Text="Label"></asp:Label>
    &nbsp;(<asp:Label ID="lbl_perc_eleitor_fora" runat="server" Text="Label"></asp:Label>
      %)</td>
  </tr>

  <tr>
    <td class="style10">
      Deficientes:
      </td>
    <td class="style9">
      <asp:Label ID="lbl_conta_deficientes" runat="server" Text="Label"></asp:Label>
    &nbsp;(<asp:Label ID="lbl_perc_deficientes" runat="server" Text="Label"></asp:Label>
      %)
    </td>
    <td class="style8">
      &nbsp;</td>
    <td class="style8">
      &nbsp;</td>
  </tr>

  <tr>
    <td class="style10">
      Endereços Cadastrados:
      </td>
    <td class="style9">
      <asp:Label ID="lbl_localizacao" runat="server" Text="Label"></asp:Label>
      &nbsp;&nbsp;
      <asp:Button ID="btn_consultar_instalacao" runat="server" CssClass="button_claro" 
        Text="Consultar" BorderWidth="1px" />
    </td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
</table>
<br /><br />
<table style="width:100%;" 
      cellpadding="2" cellspacing="1" border="0">
  <tr>
    <td width="50%" style="vertical-align: top">
      <div style="width: 100%; position: relative;" align="center">
        <asp:GridView ID="grd_tbreligiao" runat="server" 
          DataSourceID="sql_src_tbreligiao" AutoGenerateColumns="False" Width="90%">
          <RowStyle CssClass="grd_row" />
          <Columns>
            <asp:BoundField DataField="descr_religiao" HeaderText="Religião" 
              SortExpression="descr_religiao" >
            </asp:BoundField>
            <asp:BoundField DataField="conta_cidadao" HeaderText="Cidadãos" ReadOnly="True" 
              SortExpression="conta_cidadao" >
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="percentual" HeaderText="%" ReadOnly="True" 
              SortExpression="percentual">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
          </Columns>
          <HeaderStyle CssClass="grd_header" BorderStyle="None" />
          <AlternatingRowStyle BorderStyle="None" CssClass="grd_row_alt" />
        </asp:GridView>
        <br />
        <asp:SqlDataSource ID="sql_src_tbreligiao" runat="server" 
          ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" SelectCommand="SELECT A.descr_religiao,
             (SELECT COUNT(*) FROM dbo.Cad_Cidadao AS T1 
            WHERE (T1.id_municipio = @id_municipio) AND (T1.id_religiao = A.id_religiao)) as conta_cidadao,
            (cast((SELECT COUNT(*) FROM dbo.Cad_Cidadao AS T1  WHERE (T1.id_municipio = @id_municipio) AND (T1.id_religiao = A.id_religiao)) as money)
            /
            cast((SELECT COUNT(*) FROM dbo.Cad_Cidadao AS T1 WHERE (T1.id_municipio = @id_municipio)) as money) * 100) as percentual
      FROM dbo.tbReligiao AS A
      ORDER BY A.descr_religiao">
          <SelectParameters>
            <asp:SessionParameter Name="id_municipio" SessionField="id_municipio" />
          </SelectParameters>
        </asp:SqlDataSource>
      <asp:GridView ID="grd_UF" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="UF" DataSourceID="sql_src_UF" Width="90%">
        <RowStyle CssClass="grd_row" />
        <Columns>
          <asp:BoundField DataField="UF" HeaderText="UF de nascimento" ReadOnly="True" 
            SortExpression="UF" >
          <ItemStyle HorizontalAlign="Right" />
          </asp:BoundField>
          <asp:BoundField DataField="conta_cidadao" HeaderText="Cidadãos" ReadOnly="True" 
            SortExpression="conta_cidadao">
          <ItemStyle HorizontalAlign="Right" />
          </asp:BoundField>
          <asp:BoundField DataField="percentual" HeaderText="%" ReadOnly="True" 
            SortExpression="percentual">
          <ItemStyle HorizontalAlign="Right" />
          </asp:BoundField>
        </Columns>
        <HeaderStyle CssClass="grd_header" />
        <AlternatingRowStyle CssClass="grd_row_alt" />
      </asp:GridView>
      <asp:SqlDataSource ID="sql_src_UF" runat="server" 
        ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
        ProviderName="<%$ ConnectionStrings:municipio_cloud.ProviderName %>" SelectCommand="SELECT A.UF,
       (SELECT COUNT(*)
        FROM dbo.Cad_Cidadao AS T1 
        INNER JOIN dbo.tbCad_Municipios AS T2 ON (T2.cMun = T1.num_reg_nasc_cMun)
        WHERE (T1.id_municipio = @id_municipio) AND (T2.UF = A.UF)
      ) as conta_cidadao,
      (cast((SELECT COUNT(*) 
      FROM dbo.Cad_Cidadao AS T1  
      INNER JOIN dbo.tbCad_Municipios AS T2 ON (T2.cMun = T1.num_reg_nasc_cMun)
      WHERE (T1.id_municipio = @id_municipio) AND (T2.UF = A.UF)
      ) as money)
      /
      cast((SELECT COUNT(*) FROM dbo.Cad_Cidadao AS T1 WHERE (T1.id_municipio = @id_municipio)) as money) * 100) as percentual
FROM dbo.tbUF AS A
WHERE (UF &lt;&gt; '--')
ORDER BY A.UF">
        <SelectParameters>
          <asp:SessionParameter Name="id_municipio" SessionField="id_municipio" />
        </SelectParameters>
      </asp:SqlDataSource>
      </div>
    </td>
    <td width="50%" align="center" valign="top">
      <asp:SqlDataSource ID="sql_faixa_idade" runat="server" 
        ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
        ProviderName="<%$ ConnectionStrings:municipio_cloud.ProviderName %>" SelectCommand="SELECT A.descr,
       (SELECT COUNT(*) FROM dbo.Cad_Cidadao AS T1 
       WHERE (T1.id_municipio = @id_municipio) AND (datediff(year, dt_nasc, getdate()) &gt;= A.ano_inicial)AND (datediff(year, dt_nasc, getdate()) &lt;= A.ano_final)) as conta_cidadao,
       round((cast((SELECT COUNT(*) FROM dbo.Cad_Cidadao AS T1 
       WHERE (T1.id_municipio = @id_municipio) AND (datediff(year, dt_nasc, getdate()) &gt;= A.ano_inicial)AND (datediff(year, dt_nasc, getdate()) &lt;= A.ano_final)) as money))
       /
       (cast((SELECT COUNT(*) FROM dbo.Cad_Cidadao AS T1 WHERE (T1.id_municipio = @id_municipio)) as money)) * 100, 2) as percentual
FROM dbo.tbFaixa_Idade AS A
ORDER BY A.ano_inicial">
        <SelectParameters>
          <asp:SessionParameter Name="id_municipio" SessionField="id_municipio" />
        </SelectParameters>
      </asp:SqlDataSource>
      <asp:GridView ID="grd_faixa_idade" runat="server" AutoGenerateColumns="False" 
        DataSourceID="sql_faixa_idade" Width="89%">
        <Columns>
          <asp:BoundField DataField="descr" HeaderText="Faixa Etária" 
            SortExpression="descr">
          <ItemStyle HorizontalAlign="Center" />
          </asp:BoundField>
          <asp:BoundField DataField="conta_cidadao" HeaderText="Cidadãos" ReadOnly="True" 
            SortExpression="Cidadãos">
          <ItemStyle HorizontalAlign="Right" />
          </asp:BoundField>
          <asp:BoundField DataField="percentual" HeaderText="%" ReadOnly="True" 
            SortExpression="percentual">
          <ItemStyle HorizontalAlign="Right" />
          </asp:BoundField>
        </Columns>
        <HeaderStyle CssClass="grd_header" />
        <AlternatingRowStyle BackColor="#99CCFF" />
      </asp:GridView>
      <br />
      <asp:GridView ID="grd_raca_cor" runat="server" DataSourceID="sql_tb_raca_cor" 
          AutoGenerateColumns="False" Width="90%">
        <RowStyle CssClass="grd_row" />
        <Columns>
          <asp:BoundField DataField="descr_raca" HeaderText="Raça Cor" 
            SortExpression="descr_raca">
          <HeaderStyle Width="40%" />
          </asp:BoundField>
          <asp:BoundField DataField="conta_cidadao" HeaderText="Cidadãos" ReadOnly="True" 
            SortExpression="Cidadãos">
          <HeaderStyle Width="30%" />
          <ItemStyle HorizontalAlign="Right" />
          </asp:BoundField>
          <asp:BoundField DataField="percentual" HeaderText="%" ReadOnly="True" 
            SortExpression="percentual">
          <HeaderStyle Width="30%" />
          <ItemStyle HorizontalAlign="Right" />
          </asp:BoundField>
        </Columns>
        <HeaderStyle CssClass="grd_header" />
        <AlternatingRowStyle CssClass="grd_row_alt" />
      </asp:GridView>
        <br />
        <asp:SqlDataSource ID="sql_tb_raca_cor" runat="server" SelectCommand="SELECT A.descr as descr_raca,
             (SELECT COUNT(*) FROM dbo.Cad_Cidadao AS T1 
            WHERE (T1.id_municipio = @id_municipio) AND (T1.id_raca = A.id_raca)) as conta_cidadao,
           round( (cast((SELECT COUNT(*) FROM dbo.Cad_Cidadao AS T1  WHERE (T1.id_municipio = @id_municipio) AND (T1.id_raca = A.id_raca)) as money)
            /
            cast((SELECT COUNT(*) FROM dbo.Cad_Cidadao AS T1 WHERE (T1.id_municipio = @id_municipio)) as money) * 100), 2)  as percentual
      FROM dbo.tbRaca_Cor AS A
      ORDER BY A.descr" ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
          ProviderName="<%$ ConnectionStrings:municipio_cloud.ProviderName %>">
          <SelectParameters>
            <asp:SessionParameter Name="id_municipio" SessionField="id_municipio" />
          </SelectParameters>
        </asp:SqlDataSource>

      <asp:GridView ID="grd_tbEscolaridade" runat="server" 
        AutoGenerateColumns="False" DataSourceID="sql_tbEscolaridade" Width="90%" 
        CssClass="grd_row">
        <Columns>
          <asp:BoundField DataField="descr_escolaridade" HeaderText="Escolaridade" 
            SortExpression="descr_escolaridade" />
          <asp:BoundField DataField="conta_cidadao" HeaderText="Cidadãos" ReadOnly="True" 
            SortExpression="conta_cidadao">
          <ItemStyle HorizontalAlign="Right" />
          </asp:BoundField>
          <asp:BoundField DataField="percentual" HeaderText="%" ReadOnly="True" 
            SortExpression="percentual">
          <HeaderStyle Width="85px" />
          <ItemStyle HorizontalAlign="Right" />
          </asp:BoundField>
        </Columns>
        <HeaderStyle CssClass="grd_header" />
        <AlternatingRowStyle CssClass="grd_row_alt" />
      </asp:GridView>
      <br />

      <asp:SqlDataSource ID="sql_tbEscolaridade" runat="server" 
        ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
        ProviderName="<%$ ConnectionStrings:municipio_cloud.ProviderName %>" SelectCommand="SELECT A.descr as descr_escolaridade,
             (SELECT COUNT(*) FROM dbo.Cad_Cidadao AS T1 
            WHERE (T1.id_municipio = @id_municipio) AND (T1.id_escolaridade = A.id_escolaridade)) as conta_cidadao,
           round( (cast((SELECT COUNT(*) FROM dbo.Cad_Cidadao AS T1  WHERE (T1.id_municipio = @id_municipio) AND (T1.id_escolaridade = A.id_escolaridade)) as money)
            /
            cast((SELECT COUNT(*) FROM dbo.Cad_Cidadao AS T1 WHERE (T1.id_municipio = @id_municipio)) as money) * 100), 2)  as percentual
      FROM dbo.tbEscolaridade AS A
      ORDER BY A.descr">
        <SelectParameters>
          <asp:SessionParameter Name="id_municipio" SessionField="id_municipio" />
        </SelectParameters>
      </asp:SqlDataSource>

      <asp:GridView ID="grd_sexo" runat="server" AutoGenerateColumns="False" 
        DataSourceID="sql_sexo" Width="90%" CssClass="grd_row">
        <Columns>
          <asp:BoundField DataField="sexo" HeaderText="Sexo" ReadOnly="True" 
            SortExpression="sexo" />
          <asp:BoundField DataField="conta_cidadao" HeaderText="Cidadãos" ReadOnly="True" 
            SortExpression="conta_cidadao">
          <ItemStyle HorizontalAlign="Right" />
          </asp:BoundField>
          <asp:BoundField DataField="percentual" HeaderText="%" ReadOnly="True" 
            SortExpression="percentual">
          <ItemStyle HorizontalAlign="Right" />
          </asp:BoundField>
        </Columns>
        <HeaderStyle CssClass="grd_header" />
        <AlternatingRowStyle CssClass="grd_row_alt" />
      </asp:GridView>

      <asp:SqlDataSource ID="sql_sexo" runat="server" 
        ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" 
        ProviderName="<%$ ConnectionStrings:municipio_cloud.ProviderName %>" SelectCommand="SELECT 'Masculino' as sexo,
             (SELECT COUNT(*) FROM dbo.Cad_Cidadao AS T1 
            WHERE (T1.id_municipio = @id_municipio) AND (T1.sexo = 'M')) as conta_cidadao,
           round( (cast((SELECT COUNT(*) FROM dbo.Cad_Cidadao AS T1  WHERE (T1.id_municipio = @id_municipio) AND (T1.sexo = 'M')) as money)
            /
            cast((SELECT COUNT(*) FROM dbo.Cad_Cidadao AS T1 WHERE (T1.id_municipio = @id_municipio)) as money) * 100), 2)  as percentual
UNION
SELECT 'Feminino' as sexo,
             (SELECT COUNT(*) FROM dbo.Cad_Cidadao AS T1 
            WHERE (T1.id_municipio = @id_municipio) AND (T1.sexo = 'F')) as conta_cidadao,
           round( (cast((SELECT COUNT(*) FROM dbo.Cad_Cidadao AS T1  WHERE (T1.id_municipio = @id_municipio) AND (T1.sexo = 'F')) as money)
            /
            cast((SELECT COUNT(*) FROM dbo.Cad_Cidadao AS T1 WHERE (T1.id_municipio = @id_municipio)) as money) * 100), 2)  as percentual

      ORDER BY sexo
">
        <SelectParameters>
          <asp:SessionParameter Name="id_municipio" SessionField="id_municipio" />
        </SelectParameters>
      </asp:SqlDataSource>
      <br />
        <asp:GridView ID="grd_tbEstado_Civil" runat="server" 
          AutoGenerateColumns="False" DataSourceID="sql_estado_civil" Width="90%" 
        CssClass="grd_row">
          <Columns>
            <asp:BoundField DataField="descr_estado_civil" HeaderText="Estado Civil" 
              SortExpression="descr_estado_civil" />
            <asp:BoundField DataField="conta_cidadao" HeaderText="Cidadãos" ReadOnly="True" 
              SortExpression="conta_cidadao">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="percentual" HeaderText="%" ReadOnly="True" 
              SortExpression="percentual">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
          </Columns>
          <HeaderStyle CssClass="grd_header" />
          <AlternatingRowStyle CssClass="grd_row_alt" />
        </asp:GridView>
        <asp:SqlDataSource ID="sql_estado_civil" runat="server" 
        ConnectionString="<%$ ConnectionStrings:municipio_cloud %>" SelectCommand="SELECT A.descr as descr_estado_civil,
               (SELECT COUNT(*) FROM dbo.Cad_Cidadao AS T1 
              WHERE (T1.id_municipio = @id_municipio) AND (T1.id_estado_civil = A.id_estado_civil)) as conta_cidadao,
             round( (cast((SELECT COUNT(*) FROM dbo.Cad_Cidadao AS T1  WHERE (T1.id_municipio = @id_municipio) AND (T1.id_estado_civil = A.id_estado_civil)) as money)
              /
              cast((SELECT COUNT(*) FROM dbo.Cad_Cidadao AS T1 WHERE (T1.id_municipio = @id_municipio)) as money) * 100), 2)  as percentual
        FROM dbo.tbEstado_Civil AS A
        ORDER BY A.descr">
          <SelectParameters>
            <asp:SessionParameter Name="id_municipio" SessionField="id_municipio" />
          </SelectParameters>
      </asp:SqlDataSource>

    </td>
  </tr>
  <tr>
    <td align="center">
      &nbsp;</td>
    <td>

      <br />
      <br />

      <br />

    </td>
  </tr>
</table>



</asp:Content>
