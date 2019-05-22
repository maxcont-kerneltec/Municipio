<%@ Language=VBScript %>
<%
Session.LCID = 1046

Response.AddHeader "cache-control", "private"
Response.AddHeader "Pragma","No-Cache"
Response.Buffer = TRUE
Response.Expires = 0
Response.ExpiresAbsolute = 0

asp_guia = Request.QueryString ("g_")

if asp_guia = "" then asp_guia = "1"

Dim mConsulta
Set RecSt = Server.CreateObject("ADODB.Recordset")
RecSt.Open "EXEC sp3Cartorio_Uma_Guia_Pega '10','" & asp_guia & "'" , "driver={SQL Server};server=189.126.98.131;uid=municipio;pwd=fooliut5660;database=municipio"
asp_num_mConsulta = -1
asp_num_mInfo = -1
if not Recst.EOF then
  mConsulta = RecSt.GetRows
  asp_num_mConsulta = ubound(mConsulta,2) 
else
  asp_num_mConsulta = -1
end if  
RecSt.Close
set RecSt=nothing

if asp_num_mConsulta >= 0 then 
  asp_nosso_numero = "24" & right("0000000" & cstr(mConsulta(0,0)),7)
  asp_iss_apagar = mConsulta(1,0)
  asp_dt_vcto = mConsulta(2,0)
  asp_dt_cria = mConsulta(3,0)
  asp_dt_altera = mConsulta(4,0)
  asp_docs_assoc = "" & mConsulta(5,0)

  asp_RazaoSocial = "" & mConsulta(6,0)
  asp_CNPJ = "" & mConsulta(7,0)
  asp_end = "" & mConsulta(8,0)
  asp_bairro = "" & mConsulta(9,0)
  asp_municipio_cnpj =  "" & mConsulta(10,0)
  asp_uf = "" & mConsulta(11,0)
  asp_cep = "" & mConsulta(12,0)
  
  if isdate(asp_dt_altera) then
    asp_dt_doc = day(asp_dt_altera) & "/" & month(asp_dt_altera) & "/" & year(asp_dt_altera)
  elseif isdate(asp_dt_cria) then
    asp_dt_doc = day(asp_dt_cria) & "/" & month(asp_dt_cria) & "/" & year(asp_dt_cria)
  else
    asp_dt_doc = date()  
  end if
    
  if isdate(asp_dt_vcto) then
    asp_vcto = day(asp_dt_vcto) & "/" & month(asp_dt_vcto) & "/" & year(asp_dt_vcto)
  else
    asp_vcto = "?"
  end if

	asp_valor = formatnumber(asp_iss_apagar,2)
else
  Response.clear
  Response.Write ("Guia de ITBI-e não encontrada.")
  Response.End
end if  

asp_finalidade = "ITBI-e"
asp_linha4 = "Boleto referente pagamento do ITBI-e" 

'=============== DADOS DO CEDENTE (Emissor do Boleto) =================
'cedente = "PREFEITURA DO MUNICIPIO DE MANHUAÇU"
'cpf_cnpj = "18.385.088/0001-72"
'agencia = "194" ' 3 Digitos
'conta = "1908170" '"1716352" ' 7 digitos
'dac_conta = "4" '"7" ' 1 Digito
carteira = "SR"

'============= DADOS DO BOLETO ========================================
data_vencimento = asp_vcto ' Data de Vencimento
valor = asp_valor  ' Valor do Boleto
nosso_numero = asp_nosso_numero ' Nosso numero
asp_num_documento = asp_nosso_numero
asp_dt_emissao = asp_dt_doc 'Data de Emissão

'===========================DADOS DO SACADO===========================
nome_sacado = asp_RazaoSocial ' Nome do Sacado
endereco_sacado = asp_end & " - " & asp_bairro                       ' Endereço linha 1
endereco_sacado2 = asp_municipio_cnpj & " - " & asp_uf  ' Endereço linha 2
instrucoes1 = "- NÃO RECEBER APÓS O VENCIMENTO."
instrucoes2 = ""
instrucoes3 = ""
instrucoes4 = ""
instrucoes5 = ""

'========================DADOS PADRÕES===============================
especie = "REAL"
especie_doc = "OU"
aceite = "N"
uso_do_banco = "" 'Uso do Banco
'=== COD DB E-MAIL ===============================

'INTERFACE COM BOLETO
'Carteira 01 cobrança SINCO; Demais carteiras cobrança SICOB

'============== DADOS DO CEDENTE (EMISOR DO BOLETO) =================================
x136= "PREFEITURA DO MUNICIPIO DE BARUERI" ' Nome do Cedente Titular da Conta
x49= "10.000.088/0001-72" ' CNPJ Cedente

'============== DADOS DA CONTA DO CEDENTE (EMISOR DO BOLETO) ==========================
x64 = "00202767" 'Numero da Conta (8) - Todas carteiras menos a 01
dac_conta = "7" ' Digito da Conta - Todas carteiras menos a 01
asp_cod_cliente = "202767"
x83 = "0131" 'Numero da Agência para todas as carteiras
asp_par_extra = ""
if asp_par_extra = "" then asp_par_extra = "870" 'Todas carteiras menos a 01
x9 =  right(asp_par_extra,3) '"870" CNPJ PV da Caixa  operação 870 para carteira SR (8)

'============= DADOS DO BOLETO A SER GERADO =======================================
x101 = asp_valor ' Valor do Boleto
x33 = asp_dt_vcto ' Data de Vencimento
x274= asp_dt_emissao ' Data do Documento
x10 = "2"  'Código da Carteira (8 ou 9 ou 80 ou 81 ou 82 ou 00 ou 01)
x81 = asp_nosso_numero ' Nosso numero
x34= "OU" ' Especie de Documento DS-Duplicata Simples(Serviços) DM-Duplicata Mercantil OU-Outros

'================ DADOS DO SACADO (CLIENTE QUEM PAGA O BOLETO) ======================
x109 = 	asp_RazaoSocial & "&nbsp;&nbsp;&nbsp;CNPJ/CPF:&nbsp;" & asp_cnpj ' Nome do seu cliente - (Sacado)
x70 = 	asp_end ' Endereço do seu cliente - (Sacado)
x79 =	  asp_bairro ' Bairro Sacado
x74 = 	asp_cep ' CEP Sacado
x67 = 	asp_municipio_cnpj ' Cidade
x28 = 	asp_uf ' Estado

'=============== INSTRUÇÕES DO BOLETO ==========================================
x80= 	"Após vencimento cobrar:" ' Instruções do Boleto Linha 1
x171= "&nbsp;a) juros de 1% ao mês;" ' Instruções do Boleto Linha 2
x130=	"&nbsp;b) mais multa de 0,33% ao dia, até limite de 20%" ' Instruções do Boleto Linha 3
x97= 	"<br>" & asp_linha4 ' Instruções do Boleto Linha 4
x73= 	"" ' Instruções do Boleto Linha 5

'============== CAMPOS EM GERAL NÃO ALTERADOS ====================================
x174= "N" ' Aceite
x160 = "REAL" ' Especie
x98= "" ' Quantida 
x163 ="" ' Uso do Banco 

'============ NÃO ALTERA DAQUI PARA BAIXO, CASO CONTRARIO O SCRIPT PODE NÃO FUNCIONAR =======
Function x16(x15, x36, x94)'???
  Dim x87, x69, x45
  x45 = x36 + 1
  x87 = ""
  Do
    If IsNumeric(Mid(x15, (x45), 1)) Then
      x87 = x87 & Mid(x15, (x45), 1)
      x45 = x45 + 1
    End If
  Loop While IsNumeric(Mid(x15, (x45), 1))
    For x69 = 1 To x87
      x94 = x94 & (x91(53))
    Next
  x36 = x36 + 2
  x16 = x94
End Function

Function x23(x15) '???
  For x18 = 1 To Len(x15) Step 3
    x78 = 0 : x78 = Mid(x15, (x18), 3)
    x94 = x94 & x91(x78)
  Next
  x23 = x94 
End Function

Function x91(x78) '???
  x91 = Mid(x135, x78, 1)
End Function

Function x32(x126, x13) 'define length do campo
'x126 = campo
'x13 = tamanho_campo
  Dim x51, x27, x122, x150, x151, cnum
  x126 = RTrim(LTrim(x126))
  For x150 = 1 To Len(x126)
    x151 = Mid(x126, x150, 1)
    If IsNumeric(x151) Then
      cnum = cnum & x151
    End If
  Next
  x126 = cnum
  x122 = "0000000000000000000000000000"
  x51 = CInt(x51)
  If Len(x126) < x13 Then
    x51 = Abs(x13) - Abs(Len(x126))
    x27 = Mid(x122, 1, x51) & CStr(x126)
    x32 = x27
  ElseIf Len(x126) > x13 Then
    x32 = Right(x126, x13)
  Else
    x32 = x126
  End If
End Function

Function x31(x36)
  'digito verificador geral
  Dim xPos, x30, x173, xFator, x82, x120, x113
  xFator = 2
  For xPos = 1 To 43
    x113 = Mid(Right(x36, xPos), 1, 1)
    If xFator > 9 Then
      xFator = 2
      x30 = 0
    End If
    x30 = x113 * xFator
    x173 = x173 + x30
    xFator = xFator + 1
  Next
  x82 = x173 Mod 11
  x120 = 11 - x82
  If x120 = 0 Or x120 = 1 Or x120 > 9 Then
    x31 = 1
  Else
    x31 = x120
  End If
End Function

Function x102(x124)
  'calcula digito verifcador do campo
  Dim x59, x30, x173, x68, x82, x25 
  If Not IsNumeric(x124) Then
    x102 = ""
    Exit Function
  End If
  x68 = 2
  For x59 = Len(x124) To 1 Step -1
    x30 = CInt(Mid(x124, x59, 1)) * x68
    If x30 > 9 Then
      x30 = CInt(Left(x30, 1) + CInt(Right(x30, 1)))
    End If
    x173 = x173 + x30
    If x68 = 2 Then
      x68 = 1
    Else
      x68 = 2
    End If
  Next
  x25 = (x173 / 10) * -1
  x25 = Int([x25]) * -1
  x25 = x25 * 10
  x82 = x25 - x173
  x102 = x82
  If x102 = 10 Then x102 = 0
End Function

Function Calc_Dig_Mod11(xValor) 'calcula digito verificador Modulo 11
  'xValor = nosso numero completo
  Dim xPos, xMult, xSoma, xFator, xRes, xVal_Sub, xValor_Mult, xTam
  xTam = Len(xValor)
  xFator = 2
  For xPos = 1 To xTam
    xValor_Mult = Mid(Right(xValor, xPos), 1, 1)
    If xFator > 9 Then
      xFator = 2
      xMult = 0
    End If
    xMult = xValor_Mult * xFator
    xSoma = xSoma + xMult
    xFator = xFator + 1
  Next
  xRes = xSoma Mod 11
  xVal_Sub = 11 - xRes
  If xVal_Sub > 9 Then
    Calc_Dig_Mod11 = 0
  Else
    Calc_Dig_Mod11 = xVal_Sub
  End If
End Function

Function x77(x36, x66, x92, x142)
'x36 - "104" & "9" & campo livre
'x66 - digito de controle
'x92 - fator de vencimento
'x142 - valor do boleto
  Dim x112, x55, x42, x65, x105, x87, x69, x45
  x112 = Left(x36, 9)
  x55 = Mid(x36, 10, 10)
  x42 = Right(x36, 10)
  x105 = CCur(x142)
  x65 = x32(x105, 10)
  x87 = x102(x112)
  x69 = x102(x55)
  x45 = x102(x42)
  x112 = Left(x112 & x87, 5) & "." & Right(x112 & x87, 5)
  x55 = Left(x55 & x69, 5) & "." & Right(x55 & x69, 6)
  x42 = Left(x42 & x45, 5) & "." & Right(x42 & x45, 6)
  x77 = x112 & " &nbsp;" & x55 & " &nbsp;" & x42 & " &nbsp;" & x66 & " &nbsp;" & x92 & x65
End Function

Function x17(x108, x140, x106, x142, x58)
'x108 = "104" ' número do banco (Caixa)
'x140 = "9" - moeda
'x106 = asp_dt_vcto
'x142 = asp_valor 
'x58 - campo livre
  Dim x116, x146, x75, x46
  If x106 <> "" Then
    x75 = CDate("7/10/1997")
    x146 = DateDiff("d", x75, CDate(x106))
  Else
    x146 = "0000"
  End If
  x142 = Int(x142 * 100)
  x116 = x108 & x140 & x146 & x32(x142, 10) & x58
  x46 = x31(x116)
  x17 = (Left(x116, 4) & x46 & Right(x116, 39))
End Function

Function x147(x108, x140, x106, x142, x58) 'linha digitaval completa
'x108 - "104"  número do banco (Caixa)
'x140 - "9" Real
'x106 - data de vencimento
'x142 - valor do boleto
'x58 - campo livre
'x46 - digito controle geral
  Dim x116, x146, x75, x46
  If x106 <> "" Then
    x75 = CDate("7/10/1997")
    x146 = DateDiff("d", x75, CDate(x106))
  Else
    x146 = "0000"
  End If
  x142 = Int(x142 * 100)
  x116 = x108 & x140 & x146 & x32(x142, 10) & x58
  x46 = x31(x116)
  x147 = x77(x108 & x140 & x58, CStr(x46), x146, x32(x142, 10))
End Function

'x64 = asp_conta
'x83 = asp_age 'Numero da Agência
'x9 = "870" CNPJ PV
'x81 = asp_nosso_numero_atual
'x10 = asp_carteira  'Código da Carteira (8 ou 9 ou 80 ou 81 ou 82 ou 00)
'x119 = fixo + nosso_numero
'x5 - campo livre

x64 = x32(x64, 8)
x157 = x32(x157, 4)
x128 = "104" ' número do banco (Caixa)
x19 = "9"
x101 = FormatNumber((x101), 2, -2, -2, -2)
x83 = x32(x83, 4)
asp_cod_cliente = x32(asp_cod_cliente, 6)
x103 = x83 & "." & x9 & "." & x64 '"0240.870.00000026" 'agencia completa
x127 = x103 & "-" & dac_conta
'x89 = nosso_numero = impresso no boleto
asp_texto_local_pag = "Casas Lotéricas, Ag da Caixa e Rede Bancária."
If x10 = "8" Then
	v_cart="SR"
	x81 = x32(x81, 14) ' (14 posições para carteira 8) SR Eletrônica
	x89 = x81
	x119 = "8" & CStr(x81) ' N/número será o 8 + nº do ped ou doc com 14 dígitos
	x64 =  x32(x64,5) 
	x5 = x64 & x83 & x10 & "7" & x81 ' carteira SR (8)
	x104 = Left(x119, 6)  & Right(x119, 9) & "-" & Calc_Dig_Mod11(x32(x119, 15)) ' nosso número completo
ElseIf x10 = "80" Or x10 = "81" Or x10 = "82" or x10 = "00" Then
	' aqui código para carteira  80, 81 ou 82 SR Convencional
	if x10 = "00" then
		v_cart= "CS"
	else	
		v_cart="SR"
	end if
	x81 = x32(x81, 8) ' (8 posições para carteira 80, 81 ou 82)
	x89 = x81
	x119 = x10 & CStr(x81) ' N/número será o 80 ou 81 ou 82 + nº do ped ou doc com 8 dígitos
	x5 = x119 & x83 & x9 & x32(x64, 8) ' carteira SR (8)
	x104 = x119 & "-" & Calc_Dig_Mod11(x32(x119, 10)) ' nosso número completo
ElseIf x10 = "9" Then
	'aqui código para carteira = de 9 CR (Carteira rápida)
	v_cart="CR"
	x81 = x32(x81, 9) ' (9 posições para carteira 9)
	x89 = x81
	x119 = x10 & CStr(x81) ' N/número será o 9 + nº do ped ou doc com 9 dígitos
	x5 = x119 & x83 & x9 & x32(x64, 8) ' carteira CR (9)
	x104 = x119 & "-" & Calc_Dig_Mod11(x32(x119, 10)) ' nosso número completo
ElseIf x10 = "1" Then
	'Carteira 01 Cobranca SINCO
	v_cart="01"
	x81 = x32(x81, 17) ' (17 posicaoes para carteira 1)
	x89 = x81
	x119 = "9" & CStr(x81) ' N/número será o 9 + nº do ped ou doc com 17 digitos
	asp_id_cliente_completo = x32(x83,4) & x32(asp_cod_cliente,6)
	asp_dig_cod_cliente = Calc_Dig_Mod11(asp_id_cliente_completo)
	x5 = "1" & asp_cod_cliente & x119 ' carteira SR5 25 posicoes campo livre
	x104 = x119 & "-" & Calc_Dig_Mod11(x32(x119, 18)) ' nosso número completo
	x127 = x83 & "/" & asp_cod_cliente & "-" & asp_dig_cod_cliente
	asp_texto_local_pag = "PAGÁVEL EM QUALQUER BANCO ATÉ O VENCIMENTO"
ElseIf x10 = "2" Then 'SIGCB
				v_cart="SR"'Carteira 02 - carteira que é apresentada na tela
				'o nosso número tem 17 posições, 24=fixo e mais 15 posições variáveis
				x81 = x32(x81, 15) ' (17 posicaoes para carteira 01)
				x89 = "24" & x81 'nosso número inicia com 24 :: 2=cobrança sem registro :: 4 emissão pelo cedente
				
				x119 = CStr(x81) ' Nosso número com 15 digitos
				
				asp_cod_cliente = x32(asp_cod_cliente,6) 'codigo do cliente 6 posições
				asp_ced_digito = Calc_Dig_Mod11(asp_cod_cliente)

				' 25 posições para o campo livre
				' 6 posic codi cliente + 1 posic cod_cliente + nosso número 
				x5 = asp_cod_cliente & asp_ced_digito & left(x119,3) & "2" & mid(x119,4,3) & "4" & mid(x119,7)' & Calc_Dig_Mod11(x32(x119, 18))
				x5 = x5 & Calc_Dig_Mod11(x5)'posição 25 do campo livre é o digito módulo 11 do campo livre
				x104 = "24" & mid(x89,3) & "-" & Calc_Dig_Mod11(x89) ' nosso número completo
				x127 = x83 & "/" & asp_cod_cliente & "-" & asp_ced_digito'cedente completo = agencia + codigo_cliente +digito_cliente
				asp_texto_local_pag = "PAGÁVEL EM QUALQUER BANCO ATÉ O VENCIMENTO"
End If

'x128 = "104" ' número do banco (Caixa)
'x19 = "9" - moeda
'x33 = asp_dt_vcto
'x101 = asp_valor 
x123 = x17(x128, x19, CDate(x33), CCur(x101), x5)
x57 = x147(x128, x19, CDate(x33), CCur(x101), x5) ' linha digitável completa
%>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN'>
<HTML>
<HEAD>
<TITLE>Boleto Caixa</TITLE>
<META http-equiv=Content-Type content=text/html; charset=windows-1252>
<style type=text/css>
  .cp {  font: bold 10px Arial; color: black}
  .ti {  font: 9px Arial, Helvetica, sans-serif}
  .ld { font: bold 14px Arial; color: #000000}
  .ct { FONT: 9px "Arial Narrow"; COLOR: #000033}
  .cn { FONT: 9px Arial; COLOR: black }
  .bc { font: bold 22px Arial; color: #000000 }
@media print {
  .info {display: none;}
}
@media screen {
  .info  {
  	font-family: verdana, tahoma, arial;
  	font-size: 12px;
  	background-color: navy
  }
  .img_botao {
  	font-family: verdana, tahoma, arial;
    FONT-WEIGHT: bold;
    FONT-SIZE: 16px;
    COLOR: white;
    CURSOR: hand;
    CURSOR: pointer    
  }
}

</style> 

</HEAD>
<BODY text=#000000 bgColor=#ffffff leftmargin="0" topMargin=0 rightMargin=0>
<p align="center" class="info">
  <br>
  <span class="img_botao" onclick="window.print();"><img class="img_botao" SRC="img/icon_imprime.gif" WIDTH="32" HEIGHT="32">&nbsp;Imprimir</span>  
  <br><br>
</p>
<table width=650 cellspacing=0 cellpadding=0 border=0><tr><td valign=top class=cp><DIV ALIGN="CENTER">Instruções 
de Impressão</DIV></TD></TR><TR><TD valign=top class=ti><DIV ALIGN="CENTER">Imprimir 
em impressora jato de tinta (ink jet) ou laser em qualidade normal. (Não use modo 
econômico). <BR>Utilize folha A4 (210 x 297 mm) ou Carta (216 x 279 mm) - Corte 
na linha indicada<BR></DIV></td></tr>
</table><br>
<table cellspacing=0 cellpadding=0 width=650 border=0><TBODY><TR><TD class=ct width=650><img height=1 src=img/6.gif width=650 border=0></TD></TR><TR><TD class=ct width=650><div align=right><b class=cp>Recibo 
do Sacado&nbsp;&nbsp;</b></div></TD></tr></tbody>
</table>
<table width=650 cellspacing=5 cellpadding=0 border=0>
  <tr><td width=41></TD></tr>
</table>
<table cellspacing=0 cellpadding=0 width=650 border=0><tbody>
  <tr>
    <td class=cp width=134><img src=img/logo-caixa.jpg border=0 WIDTH="134" HEIGHT="36"></td>
    <td width=3 valign=bottom><img height=22 src=img/3.gif width=2 border=0></td>
    <td class=cpt width=61 valign=bottom><div align=center><font class=bc>104-0</font></div></td>
    <td width=3 valign=bottom><img height=22 src=img/3.gif width=2 border=0></td>
    <td class=ld align=right width=452 valign=bottom><span class=ld><%=x57%>&nbsp;&nbsp;</span></td>
  </tr>
  <tr><td colspan=5><img height=2 src=img/2.gif width=650 border=0></td></tr>
</tbody></table>
<table cellspacing=0 cellpadding=0 border=0><tbody>
<tr height=13>
  <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
  <td class=ct valign=top>Cedente</td>
  <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
  <td class=ct valign=top>Agência/Código do Cedente</td>
  <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
  <td class=ct valign=top>Espécie</td>
  <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
  <td class=ct valign=top>Nosso número</td>
</tr>
<tr height=12>
  <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
  <td class=cp valign=top><%=x136%></td>
  <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
  <td class=cp valign=top><%=x127%></td>
  <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
  <td class=cp valign=top><%=x160%></td>
  <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
  <td class=cp valign=top align=right><%=x104%>&nbsp;&nbsp;</td>
</tr>
<tr height=1>
  <td valign=top width=7><img height=1 src=img/2.gif width=7 border=0></td>
  <td valign=top width=180><img height=1 src=img/2.gif width=254 border=0></td>
  <td valign=top width=7><img height=1 src=img/2.gif width=7 border=0></td>
  <td valign=top width=137><img height=1 src=img/2.gif width=137 border=0></td>
  <td valign=top width=7><img height=1 src=img/2.gif width=7 border=0></td>
  <td valign=top width=67><img height=1 src=img/2.gif width=67 border=0></td>
  <td valign=top width=7><img height=1 src=img/2.gif width=7 border=0></td>
  <td valign=top width=164><img height=1 src=img/2.gif width=164 border=0></td>
</tr>
</tbody></table>
<table cellspacing=0 cellpadding=0 border=0><tbody>
  <tr height=13>
    <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
    <td class=ct valign=top colspan=3>Número do documento</td>
    <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
    <td class=ct valign=top>CPF/CNPJ</td>
    <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
    <td class=ct valign=top>Vencimento</td>
    <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
    <td class=ct valign=top>Valor documento</td>
  </tr>
  <tr height=12>
    <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
    <td class=cp valign=top colspan=3><%=x89%></td>
    <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
    <td class=cp valign=top><%= x49%></td>
    <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
    <td class=cp valign=top><%=x33%> </td>
    <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
    <td class=cp valign=top align=right><%=x101%>&nbsp;&nbsp;</td>
  </tr>
  <tr>
    <td valign=top width=7 height=1><img height=1 src=img/2.gif width=7 border=0></td>
    <td valign=top width=113 height=1><img height=1 src=img/2.gif width=113 border=0></td>
    <td valign=top width=7 height=1><img height=1 src=img/2.gif width=7 border=0></td>
    <td valign=top width=72 height=1><img height=1 src=img/2.gif width=72 border=0></td>
    <td valign=top width=7 height=1><img height=1 src=img/2.gif width=7 border=0></td>
    <td valign=top width=132 height=1><img height=1 src=img/2.gif width=132 border=0></td>
    <td valign=top width=7 height=1><img height=1 src=img/2.gif width=7 border=0></td>
    <td valign=top width=134 height=1><img height=1 src=img/2.gif width=134 border=0></td>
    <td valign=top width=7 height=1><img height=1 src=img/2.gif width=7 border=0></td>
    <td valign=top width=164 height=1><img height=1 src=img/2.gif width=164 border=0></td>
  </tr>
</tbody></table>
<table cellspacing=0 cellpadding=0 border=0><tbody>
  <tr height=13>
    <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
    <td class=ct valign=top>(-) Desconto / Abatimentos</td>
    <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
    <td class=ct valign=top>(-) Outras deduções</td>
    <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
    <td class=ct valign=top>(+) Mora / Multa</td>
    <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
    <td class=ct valign=top>(+) Outros acréscimos</td>
    <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
    <td class=ct valign=top>(=) Valor cobrado</td>
  </tr>
  <tr height=12>
    <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
    <td class=cp valign=top align=right></td>
    <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
    <td class=cp valign=top align=right></td>
    <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
    <td class=cp valign=top align=right><%=asp_multa%>&nbsp;&nbsp;</td>
    <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
    <td class=cp valign=top align=right></td>
    <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
    <td class=cp valign=top align=right>&nbsp;</td>
  </tr>
  <tr height=1>
    <td valign=top width=7><img height=1 src=img/2.gif width=7 border=0></td>
    <td valign=top width=113><img height=1 src=img/2.gif width=113 border=0></td>
    <td valign=top width=7><img height=1 src=img/2.gif width=7 border=0></td>
    <td valign=top width=112><img height=1 src=img/2.gif width=112 border=0></td>
    <td valign=top width=7><img height=1 src=img/2.gif width=7 border=0></td>
    <td valign=top width=113><img height=1 src=img/2.gif width=113 border=0></td>
    <td valign=top width=7><img height=1 src=img/2.gif width=7 border=0></td>
    <td valign=top width=113><img height=1 src=img/2.gif width=113 border=0></td>
    <td valign=top width=7><img height=1 src=img/2.gif width=7 border=0></td>
    <td valign=top width=164><img height=1 src=img/2.gif width=164 border=0></td>
  </tr>
</tbody></table>
<table cellspacing=0 cellpadding=0 border=0><tbody>
  <tr height=13>
    <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
    <td class=ct valign=top>Sacado</td>
  </tr>
  <tr height=12>
    <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
    <td class=cp valign=top><%=x109 %> </td>
  </tr>
  <tr height=1>
    <td valign=top width=7><img height=1 src=img/2.gif width=7 border=0></td>
    <td valign=top width=643><img height=1 src=img/2.gif width=643 border=0></td>
  </tr>
</tbody></table>
<table cellspacing=0 cellpadding=0 border=0><tbody>
  <tr height=12>
    <td class=ct width=7></td>
    <td class=ct width=548 >Instruções</td>
    <td class=ct width=7></td>
    <td class=ct width=88 >Autenticação mecânica</td>
  </tr>
  <tr>
    <td></td>
    <td class=cp><%=". " & asp_pedido & "<br>" & asp_instr0 & asp_instr_multa %></td>
    <td></td>
    <td></td>
  </tr>
</tbody></table>
<table cellspacing=0 cellpadding=0 width=650 border=0><tbody>
  <tr>
    <td width=7></td>
    <td width=500 class=cp>&nbsp;</td>
    <td width=159></td>
  </tr>
</tbody></table>
<table cellspacing=0 cellpadding=0 width=650 border=0>
  <tr><td class=ct width=650></td></tr><tbody>
  <tr><td class=ct width=650><div align=right>Corte na linha pontilhada&nbsp;&nbsp;</div></td></tr>
  <tr><td class=ct width=650><img height=1 src=img/6.gif width=650 border=0></td></tr></tbody>
</table><br><br>
<table cellspacing=0 cellpadding=0 width=650 border=0><tbody>
  <tr>
    <td class=cp width=134><img src=img/logo-caixa.jpg border=0 WIDTH="134" HEIGHT="36"></td>
    <td width=3 valign=bottom><img height=22 src=img/3.gif width=2 border=0></td>
    <td class=cpt  width=61 valign=bottom><div align=center><font class=bc>104-0</font></div></td>
    <td width=3 valign=bottom><img height=22 src=img/3.gif width=2 border=0></td>
    <td class=ld align=right width=452 valign=bottom>&nbsp;<SPAN CLASS=ld><%=x57%>&nbsp;&nbsp;</SPAN></td>
  </tr>
  <tr><td colspan=5><img height=2 src=img/2.gif width=650 border=0></td></tr>
</tbody></table>
<table cellspacing=0 cellpadding=0 border=0><tbody>
  <tr height=13>
    <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
    <td class=ct valign=top>Local de pagamento</td>
    <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
    <td class=ct valign=top>Vencimento</td>
  </tr>
  <tr height=12>
    <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
    <td class=cp valign=top><%=asp_texto_local_pag%></td>
    <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
    <td class=cp valign=bottom align=right><%=x33%>&nbsp;&nbsp;</td>
  </tr>
  <tr height=1>
    <td valign=top width=7><img height=1 src=img/2.gif width=7 border=0></td>
    <td valign=top width=472><img height=1 src=img/2.gif width=472 border=0></td>
    <td valign=top width=7><img height=1 src=img/2.gif width=7 border=0></td>
    <td valign=top width=164><img height=1 src=img/2.gif width=164 border=0></td>
  </tr>
</tbody></table>
<table cellspacing=0 cellpadding=0 border=0><tbody>
  <tr height=13>
    <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
    <td class=ct valign=top>Cedente</td>
    <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
    <td class=ct valign=top>Agência/Código cedente</td>
  </tr>
  <tr height=12>
    <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
    <td class=cp valign=top><%=x136%> </td>
    <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
    <td class=cp valign=top align=right><%=x127%>&nbsp;&nbsp;</td>
  </tr>
  <tr height=1>
    <td valign=top width=7><img height=1 src=img/2.gif width=7 border=0></td>
    <td valign=top width=472><img height=1 src=img/2.gif width=472 border=0></td>
    <td valign=top width=7><img height=1 src=img/2.gif width=7 border=0></td>
    <td valign=top width=164><img height=1 src=img/2.gif width=164 border=0></td>
  </tr>
</tbody></table>
<table cellspacing=0 cellpadding=0 border=0><tbody>
  <tr height=13>
    <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
    <td class=ct valign=top>Data do documento</td>
    <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
    <td class=ct valign=top>N<u>o</u> documento</td>
    <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
    <td class=ct valign=top>Espécie doc.</td>
    <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
    <td class=ct valign=top>Aceite</td>
    <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
    <td class=ct valign=top>Data processam.</td>
    <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
    <td class=ct valign=top>Nosso número</td>
  </tr>
  <tr height=12>
    <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
    <td class=cp valign=top><div align=left><%=x274%></div></td>
    <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
    <td class=cp valign=top><%=x89%> </td>
    <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
    <td class=cp valign=top><div align=left><%=x34%> </div></td>
    <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
    <td class=cp valign=top><div align=left><%=x174%> </div></td>
    <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
    <td class=cp valign=top><div align=left><%=x274%> </div></td>
    <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
    <td class=cp valign=top align=right><%=x104%>&nbsp;&nbsp;</td>
  </tr>
  <tr height=1>
    <td valign=top width=7><img height=1 src=img/2.gif width=7 border=0></td>
    <td valign=top width=113><img height=1 src=img/2.gif width=113 border=0></td>
    <td valign=top width=7><img height=1 src=img/2.gif width=7 border=0></td>
    <td valign=top width=163><img height=1 src=img/2.gif width=163 border=0></td>
    <td valign=top width=7><img height=1 src=img/2.gif width=7 border=0></td>
    <td valign=top width=62><img height=1 src=img/2.gif width=62 border=0></td>
    <td valign=top width=7><img height=1 src=img/2.gif width=7 border=0></td>
    <td valign=top width=34><img height=1 src=img/2.gif width=34 border=0></td>
    <td valign=top width=7><img height=1 src=img/2.gif width=7 border=0></td>
    <td valign=top width=72><img height=1 src=img/2.gif width=72 border=0></td>
    <td valign=top width=7><img height=1 src=img/2.gif width=7 border=0></td>
    <td valign=top width=164><img height=1 src=img/2.gif width=164 border=0></td>
  </tr>
</tbody></table>
<table cellspacing=0 cellpadding=0 border=0><tbody>
  <tr height=13>
    <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
    <td class=ct valign=top COLSPAN="3">Uso do banco</td>
    <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
    <td class=ct valign=top>Carteira</td>
    <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
    <td class=ct valign=top>Espécie</td>
    <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
    <td class=ct valign=top>Quantidade</td>
    <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
    <td class=ct valign=top>Valor</td>
    <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
    <td class=ct valign=top>(=) Valor documento</td>
  </tr>
  <tr height=12>
    <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
    <td valign=top class=cp COLSPAN="3"><div align=left><%=x163%> </div></td>
    <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
    <td class=cp valign=top><div align=left><%=v_cart%></div></td>
    <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
    <td class=cp valign=top><div align=left><%=x160%></div></td>
    <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
    <td class=cp valign=top><%=x98%> </td>
    <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
    <td class=cp valign=top>&nbsp;</td>
    <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
    <td class=cp valign=top align=right><%=x101%>&nbsp;&nbsp;</td>
  </tr>
  <tr height=1>
    <td valign=top width=7><img height=1 src=img/2.gif width=7 border=0></td>
    <td valign=top width=7><img height=1 src=img/2.gif width=75 border=0></td>
    <td valign=top width=7><img height=1 src=img/2.gif width=7 border=0></td>
    <td valign=top width=31><img height=1 src=img/2.gif width=31 border=0></td>
    <td valign=top width=7><img height=1 src=img/2.gif width=7 border=0></td>
    <td valign=top width=83><img height=1 src=img/2.gif width=83 border=0></td>
    <td valign=top width=7><img height=1 src=img/2.gif width=7 border=0></td>
    <td valign=top width=53><img height=1 src=img/2.gif width=53 border=0></td>
    <td valign=top width=7><img height=1 src=img/2.gif width=7 border=0></td>
    <td valign=top width=123><img height=1 src=img/2.gif width=123 border=0></td>
    <td valign=top width=7><img height=1 src=img/2.gif width=7 border=0></td>
    <td valign=top width=72><img height=1 src=img/2.gif width=72 border=0></td>
    <td valign=top width=7><img height=1 src=img/2.gif width=7 border=0></td>
    <td valign=top width=164><img height=1 src=img/2.gif width=164 border=0></td>
  </tr>
</tbody></table>
<table cellspacing=0 cellpadding=0 width=650 border=0><tbody>
  <tr>
    <td align=right width=10>
      <table cellspacing=0 cellpadding=0 border=0 align=left><tbody>
        <tr><td class=ct valign=top width=7 height=13><img height=13 src=img/1.gif width=1 border=0></td></tr>
        <tr><td class=cp valign=top width=7 height=12><img height=12 src=img/1.gif width=1 border=0></td></tr>
        <tr><td valign=top width=7 height=1><img height=1 src=img/2.gif width=1 border=0></td></tr>
      </tbody></table>
    </td>
    <td valign=top width=468 rowspan=5><font class=ct>Instruções (Texto de responsabilidade do cedente)</font><br><span class=cp> <%response.write x80 & "<br>" : response.write x171 & "<br>" : response.write x130 & "<br>" : response.write x97 & "<br>" : response.write x73 & "<br>" : Response.Write  asp_instr0 : Response.Write  asp_instr_multa %></span></td>
    <td align=right width=188>
      <table cellspacing=0 cellpadding=0 border=0><tbody>
        <tr height=13>
          <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
          <td class=ct valign=top>(-) Desconto / Abatimentos</td>
        </tr>
        <tr height=12>
          <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
          <td class=cp valign=top align=right></td>
        </tr>
        <tr height=1>
          <td valign=top width=7><img height=1 src=img/2.gif width=7 border=0></td>
          <td valign=top width=164><img height=1 src=img/2.gif width=164 border=0></td>
        </tr>
      </tbody></table>
    </td>
  </tr>
  <tr>
    <td align=right width=10> 
      <table cellspacing=0 cellpadding=0 border=0 align=left><tbody>
        <tr><td class=ct valign=top width=7 height=13><img height=13 src=img/1.gif width=1 border=0></td></tr>
        <tr><td class=cp valign=top width=7 height=12><img height=12 src=img/1.gif width=1 border=0></td></tr>
        <tr><td valign=top width=7 height=1><img height=1 src=img/2.gif width=1 border=0></td></tr>
      </tbody></table>
    </td>
    <td align=right width=188>
      <table cellspacing=0 cellpadding=0 border=0><tbody>
        <tr height=13>
          <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
          <td class=ct valign=top>(-) Outras deduções</td></tr>
        <tr height=12>
          <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
          <td class=cp valign=top align=right></td>
        </tr>
        <tr height=1>
          <td valign=top width=7><img height=1 src=img/2.gif width=7 border=0></td>
          <td valign=top width=164><img height=1 src=img/2.gif width=164 border=0></td>
        </tr>
      </tbody></table>
    </td>
  </tr>
  <tr>
    <td align=right width=10> 
      <table cellspacing=0 cellpadding=0 border=0 align=left><tbody>
        <tr><td class=ct valign=top width=7 height=13><img height=13 src=img/1.gif width=1 border=0></td></tr>
        <tr><td class=cp valign=top width=7 height=12><img height=12 src=img/1.gif width=1 border=0></td></tr>
        <tr><td valign=top width=7 height=1><img height=1 src=img/2.gif width=1 border=0></td></tr>
      </tbody></table>
    </td>
    <td align=right width=188> 
      <table cellspacing=0 cellpadding=0 border=0><tbody>
        <tr height=13>
          <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
          <td class=ct valign=top>(+) Mora / Multa</td>
        </tr>
        <tr height=12>
          <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
          <td class=cp valign=top align=right><%=asp_multa%>&nbsp;&nbsp;</td>
        </tr>
        <tr height=1>
          <td valign=top width=7><img height=1 src=img/2.gif width=7 border=0></td>
          <td valign=top width=164><img height=1 src=img/2.gif width=164 border=0></td>
        </tr>
      </tbody></table>
    </td>
  </tr>
  <tr>
    <td align=right width=10>
      <table cellspacing=0 cellpadding=0 border=0 align=left><tbody>
        <tr><td class=ct valign=top width=7 height=13><img height=13 src=img/1.gif width=1 border=0></td></tr>
        <tr><td class=cp valign=top width=7 height=12><img height=12 src=img/1.gif width=1 border=0></td></tr>
        <tr><td valign=top width=7 height=1><img height=1 src=img/2.gif width=1 border=0></td></tr>
      </tbody></table>
    </td>
    <td align=right width=188> 
      <table cellspacing=0 cellpadding=0 border=0><tbody>
        <tr height=13>
          <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
          <td class=ct valign=top>(+) Outros acréscimos</td>
        </tr>
        <tr height=12>
          <td class=cp valign=top><img height=12 src=img/1.gif width=1 border=0></td>
          <td class=cp valign=top align=right></td>
        </tr>
        <tr height=1>
          <td valign=top width=7><img height=1 src=img/2.gif width=7 border=0></td>
          <td valign=top width=164><img height=1 src=img/2.gif width=164 border=0></td>
        </tr>
      </tbody></table>
    </td>
  </tr>
  <tr>
    <td align=right width=10>
      <table cellspacing=0 cellpadding=0 border=0 align=left><tbody>
        <tr><td class=ct valign=top width=7 height=13><img height=13 src=img/1.gif width=1 border=0></td></tr>
        <tr><td class=cp valign=top width=7 height=12><img height=12 src=img/1.gif width=1 border=0></td></tr>
      </tbody></table>
    </td>
    <td align=right width=188>
      <table cellspacing=0 cellpadding=0 border=0><tbody>
        <tr height=13>
          <td class=ct valign=top><img height=13 src=img/1.gif width=1 border=0></td>
          <td class=ct valign=top>(=) Valor cobrado</td></tr>
        <tr height=12>
          <td class=cp valign=top width=7><img height=12 src=img/1.gif width=1 border=0></td>
          <td class=cp valign=top align=right width=164>&nbsp;</td>
        </tr>
      </tbody></table>
    </td>
  </tr>
</tbody></table>
<table cellspacing=0 cellpadding=0 width=650 border=0><tbody>
  <tr><td valign=top width=650 height=1><img height=1 src=img/2.gif width=650 border=0></td></tr>
</tbody></table>
<table cellspacing=0 cellpadding=0 border=0><tbody><tr><td class=ct valign=top width=7 height=13><img height=13 src=img/1.gif width=1 border=0></td><td class=ct valign=top width=643 height=13>Sacado</td></tr><tr><td class=cp valign=top width=7 height=12><img height=12 src=img/1.gif width=1 border=0></td><td class=cp valign=top width=643 height=12> 
<%=x109 %> </td></tr></tbody>
</table>
<table cellspacing=0 cellpadding=0 border=0><tbody><tr><td class=cp valign=top width=7 height=12><img height=12 src=img/1.gif width=1 border=0></td><td class=cp valign=top width=643 height=12> 
<%=x70 & " - " & x79 %> </td></tr></tbody>
</table>
<table cellspacing=0 cellpadding=0 border=0><tbody><tr><td class=ct valign=top width=7 height=13><img height=13 src=img/1.gif width=1 border=0></td><td class=cp valign=top width=472 height=13> 
<%= x74 & " - " & x67 & " - " & x28 %> </td><td class=ct valign=top width=7 height=13><img height=13 src=img/1.gif width=1 border=0></td><td class=ct valign=top width=164 height=13>Cód. 
baixa</td></tr><tr><td valign=top width=7 height=1><img height=1 src=img/2.gif width=7 border=0></td><td valign=top width=472 height=1><img height=1 src=img/2.gif width=472 border=0></td><td valign=top width=7 height=1><img height=1 src=img/2.gif width=7 border=0></td><td valign=top width=164 height=1><img height=1 src=img/2.gif width=164 border=0></td></tr></tbody>
</table>
<TABLE cellSpacing=0 cellPadding=0 border=0 width=650><TBODY><TR><TD class=ct  width=7 height=12></TD><TD class=ct  width=409 >Sacador/Avalista</TD><TD class=ct  width=250 ><div align=right>Autenticação 
mecânica - <b class=cp>Ficha de Compensação</b></div></TD></TR><TR><TD class=ct  colspan=3 ></TD></tr></tbody>
</table>
<TABLE cellSpacing=0 cellPadding=0 width=650 border=0><TBODY><TR><TD vAlign=bottom align=left height=50> 
<%x129( x123 )
Sub x129( x22 )
Dim x2(99)
Const x85 = 1 : Const x131 = 3 : Const x44 = 50
if isempty(x2(0)) then
x2(0) = "00110" : x2(1) = "10001" : x2(2) = "01001" : x2(3) = "11000"
x2(4) = "00101" : x2(5) = "10100" : x2(6) = "01100" : x2(7) = "00011"
x2(8) = "10010" : x2(9) = "01010" 
for x99 = 9 to 0 step -1
for x3 = 9 to 0 Step -1
x125 = x99 * 10 + x3 : x126 = ""
for x18 = 1 To 5
x126 = x126 & mid(x2(x99), x18, 1) + mid(x2(x3), x18, 1)
next
x2(x125) = x126
next
next
end if
%> <img src=img/p.gif width=<%=x85%> height=<%=x44%> border=0><img 
src=img/b.gif width=<%=x85%> height=<%=x44%> border=0><img 
src=img/p.gif width=<%=x85%> height=<%=x44%> border=0><img 
src=img/b.gif width=<%=x85%> height=<%=x44%> border=0><img 
<%
x126 = x22
if len(x126) mod 2 <> 0 then
x126 = "0" & x126
end if
do while len(x126) > 0
x18 = cint(left( x126, 2)) : x126 = right(x126, len(x126) - 2) : x125 = x2(x18)
for x18 = 1 to 10 step 2
if mid(x125, x18, 1) = "0" then
x99 = x85
else
x99 = x131
end if
%>
src=img/p.gif width=<%=x99%> height=<%=x44%> border=0><img 
<%if mid(x125, x18 + 1, 1) = "0" Then
x3 = x85
else
x3 = x131
end if%>
src=img/b.gif width=<%=x3%> height=<%=x44%> border=0><img 
<%next
loop%>
src=img/p.gif width=<%=x131%> height=<%=x44%> border=0><img src=img/b.gif width=<%=x85%> height=<%=x44%> border=0><img src=img/p.gif width=<%=1%> height=<%=x44%> border=0> 
<% end sub %> </TD></tr></tbody>
</table>
<TABLE cellSpacing=0 cellPadding=0 width=650 border=0><TR><TD class=ct width=650></TD></TR><TBODY><TR><TD class=ct width=650><div align=right>Corte 
na linha pontilhada&nbsp;&nbsp;</div></TD></TR><TR><TD class=ct width=650><img height=1 src=img/6.gif width=650 border=0></TD></tr></tbody>
</table>
</body>
</html>
