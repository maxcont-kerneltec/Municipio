Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.Xml
Imports System

Public Class clsNFEide

  Private _msg_grava, _natOp, _mod_descr, _dEmi, _dSaiEnt, _verProc, _hSaiEnt, _infCpl, _UF, _infAdFisco, _origem_nf As String
  Private _msg_retorno, _sig_versao, _sig_verAplic, _sig_chNFe, _sig_dhRecbto, _sig_nProt, _sig_digVal, _sig_xMotivo, _f_Total As String
  Private _arq_xml, _canc_nProt, _canc_dhRecbto, _canc_xJust, _inut_nProt, _inut_dhRecbto, _inut_xJust, _cob_nFat As String
  Private _transp_tipo_pessoa, _transp_CNPJ, _transp_xNome, _transp_IE, _transp_xEnder, _transp_xMun, _transp_UF As String
  Private _veicTransp_placa, _veicTransp_UF, _veicTransp_RNTC, _retTransp_cUF, _exp_UFEmbarq, _exp_xLocEmbarq, _exp_xNEmp As String
  Private _exp_xPed, _exp_xCont, _exp_xLocDespacho, _msg_erro As String
  Private _result, _qtde_linhas, _cUF, _cNF, _indPag, _serie, _nNF, _tpNF, _cMunFG, _tpImp, _tpEmis, _cDV As Integer
  Private _tpAmb, _finNFe, _procEmi, _cob_cond_pag, _tipo_doc, _sts_nf, _tb_cfop, _id_nf_dev, _id_pedido, _retTransp_cMunFG As Integer
  Private _id_pedido_amostra, _id_orcamento, _idDest, _indFinal, _indPres, _nNF_anterior, _conta_dupls_pedido As Integer
  Private _id_nf_retorno, _sig_tpAmb, _sig_cStat, _sts_NF_c, _id_emit, _id_dest, _transp_modFrete, _retTransp_CFOP As Integer
  Private _valor_nfe_total, _ICMSTot_vNF, _ICMSTot_vICMSDeson, _ICMSTot_vICMS, _ICMSTot_vST, _ICMSTot_vBC, _ICMSTot_vBCST, _ICMSTot_vDesc As Decimal
  Private _ICMSTot_vProd, _ICMSTot_vFrete, _ICMSTot_vSeg, _ICMSTot_vII, _ICMSTot_vIPI, _ICMSTot_vPIS, _ICMSTot_vCOFINS As Decimal
  Private _ICMSTot_vOutro, _vTotTrib, _ISSQNtot_vBC, _ISSQNtot_vISS, _ISSQNtot_vPIS, _ISSQNtot_vCOFINS, _retTrib_vRetPIS As Decimal
  Private _retTrib_vRetCOFINS, _retTrib_vRetCSLL, _retTrib_vBCIRRF, _retTrib_vIRRF, _retTrib_vBCRetPrev, _retTrib_vRetPrev As Decimal
  Private _ISSQNtot_vServ, _cob_nFatob_vOrig, _cob_vDesc, _cob_vLiq, _ICMS_vCredICMSSN, _pCred_ICMS, _retTransp_pICMSRet As Decimal
  Private _TOTvFCPUFDest, _ICMSTot_vICMSUFDest, _ICMSTot_vICMSUFRemet, _cob_vOrig, _retTransp_vServ, _retTransp_vBCRet, _retTransp_vICMSRet As Decimal
  Private _ICMSDestTot_vFCP, _ICMSDestTot_vFCPST, _ICMSDestTot_vFCPSTRet, _ICMSTot_vIPIDevol As Decimal
  Private _indIntermed As String

  Private conexao1 As New SqlConnection

  Public Sub New()

  End Sub

  Property indIntermed() As String
    Get
      Return _indIntermed
    End Get
    Set(value As String)
      _indIntermed = value
    End Set
  End Property

  Property msg_grava() As String
    Get
      Return _msg_grava
    End Get
    Set(value As String)
      _msg_grava = value
    End Set
  End Property

  Property result() As Integer
    Get
      Return _result
    End Get
    Set(value As Integer)
      _result = value
    End Set
  End Property

  Property qtde_linhas() As Integer
    Get
      Return _qtde_linhas
    End Get
    Set(value As Integer)
      _qtde_linhas = value
    End Set
  End Property

  Property valor_nfe_total() As Decimal
    Get
      Return _valor_nfe_total
    End Get
    Set(value As Decimal)
      _valor_nfe_total = value
    End Set
  End Property

  Property natOp() As String
    Get
      Return _natOp
    End Get
    Set(value As String)
      _natOp = value
    End Set
  End Property

  Property mod_descr() As String
    Get
      Return _mod_descr
    End Get
    Set(value As String)
      _mod_descr = value
    End Set
  End Property

  Property dEmi() As String
    Get
      Return _dEmi
    End Get
    Set(value As String)
      _dEmi = value
    End Set
  End Property

  Property dSaiEnt() As String
    Get
      Return _dSaiEnt
    End Get
    Set(value As String)
      _dSaiEnt = value
    End Set
  End Property

  Property verProc() As String
    Get
      Return _verProc
    End Get
    Set(value As String)
      _verProc = value
    End Set
  End Property

  Property hSaiEnt() As String
    Get
      Return _hSaiEnt
    End Get
    Set(value As String)
      _hSaiEnt = value
    End Set
  End Property

  Property infCpl() As String
    Get
      Return _infCpl
    End Get
    Set(value As String)
      _infCpl = value
    End Set
  End Property

  Property UF() As String
    Get
      Return _UF
    End Get
    Set(value As String)
      _UF = value
    End Set
  End Property

  Property infAdFisco() As String
    Get
      Return _infAdFisco
    End Get
    Set(value As String)
      _infAdFisco = value
    End Set
  End Property

  Property origem_nf() As String
    Get
      Return _origem_nf
    End Get
    Set(value As String)
      _origem_nf = value
    End Set
  End Property

  Property cUF() As Integer
    Get
      Return _cUF
    End Get
    Set(value As Integer)
      _cUF = value
    End Set
  End Property

  Property cNF() As Integer
    Get
      Return _cNF
    End Get
    Set(value As Integer)
      _cNF = value
    End Set
  End Property

  Property indPag() As Integer
    Get
      Return _indPag
    End Get
    Set(value As Integer)
      _indPag = value
    End Set
  End Property

  Property serie() As Integer
    Get
      Return _serie
    End Get
    Set(value As Integer)
      _serie = value
    End Set
  End Property

  Property nNF() As Integer
    Get
      Return _nNF
    End Get
    Set(value As Integer)
      _nNF = value
    End Set
  End Property

  Property tpNF() As Integer
    Get
      Return _tpNF
    End Get
    Set(value As Integer)
      _tpNF = value
    End Set
  End Property

  Property cMunFG() As Integer
    Get
      Return _cMunFG
    End Get
    Set(value As Integer)
      _cMunFG = value
    End Set
  End Property

  Property tpImp() As Integer
    Get
      Return _tpImp
    End Get
    Set(value As Integer)
      _tpImp = value
    End Set
  End Property

  Property tpEmis() As Integer
    Get
      Return _tpEmis
    End Get
    Set(value As Integer)
      _tpEmis = value
    End Set
  End Property

  Property cDV() As Integer
    Get
      Return _cDV
    End Get
    Set(value As Integer)
      _cDV = value
    End Set
  End Property

  Property tpAmb() As Integer
    Get
      Return _tpAmb
    End Get
    Set(value As Integer)
      _tpAmb = value
    End Set
  End Property

  Property finNFe() As Integer
    Get
      Return _finNFe
    End Get
    Set(value As Integer)
      _finNFe = value
    End Set
  End Property

  Property procEmi() As Integer
    Get
      Return _procEmi
    End Get
    Set(value As Integer)
      _procEmi = value
    End Set
  End Property

  Property cob_cond_pag() As Integer
    Get
      Return _cob_cond_pag
    End Get
    Set(value As Integer)
      _cob_cond_pag = value
    End Set
  End Property

  Property tipo_doc() As Integer
    Get
      Return _tipo_doc
    End Get
    Set(value As Integer)
      _tipo_doc = value
    End Set
  End Property

  Property sts_nf() As Integer
    Get
      Return _sts_nf
    End Get
    Set(value As Integer)
      _sts_nf = value
    End Set
  End Property

  Property tb_cfop() As Integer
    Get
      Return _tb_cfop
    End Get
    Set(value As Integer)
      _tb_cfop = value
    End Set
  End Property

  Property id_nf_dev() As Integer
    Get
      Return _id_nf_dev
    End Get
    Set(value As Integer)
      _id_nf_dev = value
    End Set
  End Property

  Property id_pedido() As Integer
    Get
      Return _id_pedido
    End Get
    Set(value As Integer)
      _id_pedido = value
    End Set
  End Property

  Property id_pedido_amostra() As Integer
    Get
      Return _id_pedido_amostra
    End Get
    Set(value As Integer)
      _id_pedido_amostra = value
    End Set
  End Property

  Property id_orcamento() As Integer
    Get
      Return _id_orcamento
    End Get
    Set(value As Integer)
      _id_orcamento = value
    End Set
  End Property

  Property idDest() As Integer
    Get
      Return _idDest
    End Get
    Set(value As Integer)
      _idDest = value
    End Set
  End Property

  Property indFinal() As Integer
    Get
      Return _indFinal
    End Get
    Set(value As Integer)
      _indFinal = value
    End Set
  End Property

  Property indPres() As Integer
    Get
      Return _indPres
    End Get
    Set(value As Integer)
      _indPres = value
    End Set
  End Property

  Property nNF_anterior() As Integer
    Get
      Return _nNF_anterior
    End Get
    Set(value As Integer)
      _nNF_anterior = value
    End Set
  End Property

  Property conta_dupls_pedido() As Integer
    Get
      Return _conta_dupls_pedido
    End Get
    Set(value As Integer)
      _conta_dupls_pedido = value
    End Set
  End Property

  Property id_nf_retorno() As Integer
    Get
      Return _id_nf_retorno
    End Get
    Set(value As Integer)
      _id_nf_retorno = value
    End Set
  End Property

  Property sig_versao() As String
    Get
      Return _sig_versao
    End Get
    Set(value As String)
      _sig_versao = value
    End Set
  End Property

  Property sig_verAplic() As String
    Get
      Return _sig_verAplic
    End Get
    Set(value As String)
      _sig_verAplic = value
    End Set
  End Property

  Property sig_chNFe() As String
    Get
      Return _sig_chNFe
    End Get
    Set(value As String)
      _sig_chNFe = value
    End Set
  End Property

  Property sig_dhRecbto() As String
    Get
      Return _sig_dhRecbto
    End Get
    Set(value As String)
      _sig_dhRecbto = value
    End Set
  End Property

  Property sig_nProt() As String
    Get
      Return _sig_nProt
    End Get
    Set(value As String)
      _sig_nProt = value
    End Set
  End Property

  Property sig_digVal() As String
    Get
      Return _sig_digVal
    End Get
    Set(value As String)
      _sig_digVal = value
    End Set
  End Property

  Property sig_xMotivo() As String
    Get
      Return _sig_xMotivo
    End Get
    Set(value As String)
      _sig_xMotivo = value
    End Set
  End Property

  Property msg_retorno() As String
    Get
      Return _msg_retorno
    End Get
    Set(value As String)
      _msg_retorno = value
    End Set
  End Property

  Property arq_xml() As String
    Get
      Return _arq_xml
    End Get
    Set(value As String)
      _arq_xml = value
    End Set
  End Property

  Property transp_tipo_pessoa() As String
    Get
      Return _transp_tipo_pessoa
    End Get
    Set(value As String)
      _transp_tipo_pessoa = value
    End Set
  End Property

  Property transp_CNPJ() As String
    Get
      Return _transp_CNPJ
    End Get
    Set(value As String)
      _transp_CNPJ = value
    End Set
  End Property

  Property transp_xNome() As String
    Get
      Return _transp_xNome
    End Get
    Set(value As String)
      _transp_xNome = value
    End Set
  End Property

  Property transp_IE() As String
    Get
      Return _transp_IE
    End Get
    Set(value As String)
      _transp_IE = value
    End Set
  End Property

  Property transp_xEnder() As String
    Get
      Return _transp_xEnder
    End Get
    Set(value As String)
      _transp_xEnder = value
    End Set
  End Property

  Property transp_xMun() As String
    Get
      Return _transp_xMun
    End Get
    Set(value As String)
      _transp_xMun = value
    End Set
  End Property

  Property transp_UF() As String
    Get
      Return _transp_UF
    End Get
    Set(value As String)
      _transp_UF = value
    End Set
  End Property

  Property veicTransp_placa() As String
    Get
      Return _veicTransp_placa
    End Get
    Set(value As String)
      _veicTransp_placa = value
    End Set
  End Property

  Property veicTransp_UF() As String
    Get
      Return _veicTransp_UF
    End Get
    Set(value As String)
      _veicTransp_UF = value
    End Set
  End Property

  Property veicTransp_RNTC() As String
    Get
      Return _veicTransp_RNTC
    End Get
    Set(value As String)
      _veicTransp_RNTC = value
    End Set
  End Property

  Property retTransp_cUF() As String
    Get
      Return _retTransp_cUF
    End Get
    Set(value As String)
      _retTransp_cUF = value
    End Set
  End Property

  Property canc_nProt() As String
    Get
      Return _canc_nProt
    End Get
    Set(value As String)
      _canc_nProt = value
    End Set
  End Property

  Property canc_dhRecbto() As String
    Get
      Return _canc_dhRecbto
    End Get
    Set(value As String)
      _canc_dhRecbto = value
    End Set
  End Property

  Property canc_xJust() As String
    Get
      Return _canc_xJust
    End Get
    Set(value As String)
      _canc_xJust = value
    End Set
  End Property

  Property inut_nProt() As String
    Get
      Return _inut_nProt
    End Get
    Set(value As String)
      _inut_nProt = value
    End Set
  End Property

  Property inut_dhRecbto() As String
    Get
      Return _inut_dhRecbto
    End Get
    Set(value As String)
      _inut_dhRecbto = value
    End Set
  End Property

  Property inut_xJust() As String
    Get
      Return _inut_xJust
    End Get
    Set(value As String)
      _inut_xJust = value
    End Set
  End Property

  Property cob_nFat() As String
    Get
      Return _cob_nFat
    End Get
    Set(value As String)
      _cob_nFat = value
    End Set
  End Property

  Property sig_tpAmb() As Integer
    Get
      Return _sig_tpAmb
    End Get
    Set(value As Integer)
      _sig_tpAmb = value
    End Set
  End Property

  Property sig_cStat() As Integer
    Get
      Return _sig_cStat
    End Get
    Set(value As Integer)
      _sig_cStat = value
    End Set
  End Property

  Property sts_NF_c() As Integer
    Get
      Return _sts_NF_c
    End Get
    Set(value As Integer)
      _sts_NF_c = value
    End Set
  End Property

  Property id_emit() As Integer
    Get
      Return _id_emit
    End Get
    Set(value As Integer)
      _id_emit = value
    End Set
  End Property

  Property id_dest() As Integer
    Get
      Return _id_dest
    End Get
    Set(value As Integer)
      _id_dest = value
    End Set
  End Property

  Property transp_modFrete() As Integer
    Get
      Return _transp_modFrete
    End Get
    Set(value As Integer)
      _transp_modFrete = value
    End Set
  End Property

  Property retTransp_CFOP() As Integer
    Get
      Return _retTransp_CFOP
    End Get
    Set(value As Integer)
      _retTransp_CFOP = value
    End Set
  End Property

  Property retTransp_cMunFG() As Integer
    Get
      Return _retTransp_cMunFG
    End Get
    Set(value As Integer)
      _retTransp_cMunFG = value
    End Set
  End Property

  Property ICMSTot_vNF() As Decimal
    Get
      Return _ICMSTot_vNF
    End Get
    Set(value As Decimal)
      _ICMSTot_vNF = value
    End Set
  End Property

  Property ICMSTot_vICMSDeson() As Decimal
    Get
      Return _ICMSTot_vICMSDeson
    End Get
    Set(value As Decimal)
      _ICMSTot_vICMSDeson = value
    End Set
  End Property

  Property ICMSTot_vICMS() As Decimal
    Get
      Return _ICMSTot_vICMS
    End Get
    Set(value As Decimal)
      _ICMSTot_vICMS = value
    End Set
  End Property

  Property ICMSTot_vST() As Decimal
    Get
      Return _ICMSTot_vST
    End Get
    Set(value As Decimal)
      _ICMSTot_vST = value
    End Set
  End Property

  Property ICMSTot_vBC() As Decimal
    Get
      Return _ICMSTot_vBC
    End Get
    Set(value As Decimal)
      _ICMSTot_vBC = value
    End Set
  End Property

  Property ICMSTot_vBCST() As Decimal
    Get
      Return _ICMSTot_vBCST
    End Get
    Set(value As Decimal)
      _ICMSTot_vBCST = value
    End Set
  End Property

  Property ICMSTot_vDesc() As Decimal
    Get
      Return _ICMSTot_vDesc
    End Get
    Set(value As Decimal)
      _ICMSTot_vDesc = value
    End Set
  End Property

  Property ICMSTot_vProd() As Decimal
    Get
      Return _ICMSTot_vProd
    End Get
    Set(value As Decimal)
      _ICMSTot_vProd = value
    End Set
  End Property

  Property ICMSTot_vFrete() As Decimal
    Get
      Return _ICMSTot_vFrete
    End Get
    Set(value As Decimal)
      _ICMSTot_vFrete = value
    End Set
  End Property

  Property ICMSTot_vSeg() As Decimal
    Get
      Return _ICMSTot_vSeg
    End Get
    Set(value As Decimal)
      _ICMSTot_vSeg = value
    End Set
  End Property

  Property ICMSTot_vII() As Decimal
    Get
      Return _ICMSTot_vII
    End Get
    Set(value As Decimal)
      _ICMSTot_vII = value
    End Set
  End Property

  Property ICMSTot_vIPI() As Decimal
    Get
      Return _ICMSTot_vIPI
    End Get
    Set(value As Decimal)
      _ICMSTot_vIPI = value
    End Set
  End Property

  Property ICMSTot_vPIS() As Decimal
    Get
      Return _ICMSTot_vPIS
    End Get
    Set(value As Decimal)
      _ICMSTot_vPIS = value
    End Set
  End Property

  Property ICMSTot_vCOFINS() As Decimal
    Get
      Return _ICMSTot_vCOFINS
    End Get
    Set(value As Decimal)
      _ICMSTot_vCOFINS = value
    End Set
  End Property

  Property ICMSTot_vOutro() As Decimal
    Get
      Return _ICMSTot_vOutro
    End Get
    Set(value As Decimal)
      _ICMSTot_vOutro = value
    End Set
  End Property

  Property vTotTrib() As Decimal
    Get
      Return _vTotTrib
    End Get
    Set(value As Decimal)
      _vTotTrib = value
    End Set
  End Property

  Property ISSQNtot_vBC() As Decimal
    Get
      Return _ISSQNtot_vBC
    End Get
    Set(value As Decimal)
      _ISSQNtot_vBC = value
    End Set
  End Property

  Property ISSQNtot_vISS() As Decimal
    Get
      Return _ISSQNtot_vISS
    End Get
    Set(value As Decimal)
      _ISSQNtot_vISS = value
    End Set
  End Property

  Property ISSQNtot_vPIS() As Decimal
    Get
      Return _ISSQNtot_vPIS
    End Get
    Set(value As Decimal)
      _ISSQNtot_vPIS = value
    End Set
  End Property

  Property ISSQNtot_vCOFINS() As Decimal
    Get
      Return _ISSQNtot_vCOFINS
    End Get
    Set(value As Decimal)
      _ISSQNtot_vCOFINS = value
    End Set
  End Property

  Property retTrib_vRetPIS() As Decimal
    Get
      Return _retTrib_vRetPIS
    End Get
    Set(value As Decimal)
      _retTrib_vRetPIS = value
    End Set
  End Property

  Property retTrib_vRetCOFINS() As Decimal
    Get
      Return _retTrib_vRetCOFINS
    End Get
    Set(value As Decimal)
      _retTrib_vRetCOFINS = value
    End Set
  End Property

  Property retTrib_vRetCSLL() As Decimal
    Get
      Return _retTrib_vRetCSLL
    End Get
    Set(value As Decimal)
      _retTrib_vRetCSLL = value
    End Set
  End Property

  Property retTrib_vBCIRRF() As Decimal
    Get
      Return _retTrib_vBCIRRF
    End Get
    Set(value As Decimal)
      _retTrib_vBCIRRF = value
    End Set
  End Property

  Property retTrib_vIRRF() As Decimal
    Get
      Return _retTrib_vIRRF
    End Get
    Set(value As Decimal)
      _retTrib_vIRRF = value
    End Set
  End Property

  Property retTrib_vBCRetPrev() As Decimal
    Get
      Return _retTrib_vBCRetPrev
    End Get
    Set(value As Decimal)
      _retTrib_vBCRetPrev = value
    End Set
  End Property

  Property retTrib_vRetPrev() As Decimal
    Get
      Return _retTrib_vRetPrev
    End Get
    Set(value As Decimal)
      _retTrib_vRetPrev = value
    End Set
  End Property

  Property ISSQNtot_vServ() As Decimal
    Get
      Return _ISSQNtot_vServ
    End Get
    Set(value As Decimal)
      _ISSQNtot_vServ = value
    End Set
  End Property

  Property cob_nFatob_vOrig() As Decimal
    Get
      Return _cob_nFatob_vOrig
    End Get
    Set(value As Decimal)
      _cob_nFatob_vOrig = value
    End Set
  End Property

  Property cob_vDesc() As Decimal
    Get
      Return _cob_vDesc
    End Get
    Set(value As Decimal)
      _cob_vDesc = value
    End Set
  End Property

  Property cob_vLiq() As Decimal
    Get
      Return _cob_vLiq
    End Get
    Set(value As Decimal)
      _cob_vLiq = value
    End Set
  End Property

  Property f_Total() As String
    Get
      Return _f_Total
    End Get
    Set(value As String)
      _f_Total = value
    End Set
  End Property

  Property ICMS_vCredICMSSN() As Decimal
    Get
      Return _ICMS_vCredICMSSN
    End Get
    Set(value As Decimal)
      _ICMS_vCredICMSSN = value
    End Set
  End Property

  Property pCred_ICMS() As Decimal
    Get
      Return _pCred_ICMS
    End Get
    Set(value As Decimal)
      _pCred_ICMS = value
    End Set
  End Property

  Property TOTvFCPUFDest() As Decimal
    Get
      Return _TOTvFCPUFDest
    End Get
    Set(value As Decimal)
      _TOTvFCPUFDest = value
    End Set
  End Property

  Property ICMSTot_vICMSUFDest() As Decimal
    Get
      Return _ICMSTot_vICMSUFDest
    End Get
    Set(value As Decimal)
      _ICMSTot_vICMSUFDest = value
    End Set
  End Property

  Property ICMSTot_vICMSUFRemet() As Decimal
    Get
      Return _ICMSTot_vICMSUFRemet
    End Get
    Set(value As Decimal)
      _ICMSTot_vICMSUFRemet = value
    End Set
  End Property

  Property cob_vOrig() As Decimal
    Get
      Return _cob_vOrig
    End Get
    Set(value As Decimal)
      _cob_vOrig = value
    End Set
  End Property

  Property retTransp_vServ() As Decimal
    Get
      Return _retTransp_vServ
    End Get
    Set(value As Decimal)
      _retTransp_vServ = value
    End Set
  End Property

  Property retTransp_vBCRet() As Decimal
    Get
      Return _retTransp_vBCRet
    End Get
    Set(value As Decimal)
      _retTransp_vBCRet = value
    End Set
  End Property

  Property retTransp_pICMSRet() As Decimal
    Get
      Return _retTransp_pICMSRet
    End Get
    Set(value As Decimal)
      _retTransp_pICMSRet = value
    End Set
  End Property

  Property retTransp_vICMSRet() As Decimal
    Get
      Return _retTransp_vICMSRet
    End Get
    Set(value As Decimal)
      _retTransp_vICMSRet = value
    End Set
  End Property

  Property exp_UFEmbarq() As String
    Get
      Return _exp_UFEmbarq
    End Get
    Set(value As String)
      _exp_UFEmbarq = value
    End Set
  End Property

  Property exp_xLocEmbarq() As String
    Get
      Return _exp_xLocEmbarq
    End Get
    Set(value As String)
      _exp_xLocEmbarq = value
    End Set
  End Property

  Property exp_xNEmp() As String
    Get
      Return _exp_xNEmp
    End Get
    Set(value As String)
      _exp_xNEmp = value
    End Set
  End Property

  Property exp_xPed() As String
    Get
      Return _exp_xPed
    End Get
    Set(value As String)
      _exp_xPed = value
    End Set
  End Property

  Property exp_xCont() As String
    Get
      Return _exp_xCont
    End Get
    Set(value As String)
      _exp_xCont = value
    End Set
  End Property

  Property exp_xLocDespacho() As String
    Get
      Return _exp_xLocDespacho
    End Get
    Set(value As String)
      _exp_xLocDespacho = value
    End Set
  End Property

  Property msg_erro() As String
    Get
      Return _msg_erro
    End Get
    Set(value As String)
      _msg_erro = value
    End Set
  End Property

  Property ICMSDestTot_vFCP() As Decimal
    Get
      Return _ICMSDestTot_vFCP
    End Get
    Set(value As Decimal)
      _ICMSDestTot_vFCP = value
    End Set
  End Property

  Property ICMSDestTot_vFCPST() As Decimal
    Get
      Return _ICMSDestTot_vFCPST
    End Get
    Set(value As Decimal)
      _ICMSDestTot_vFCPST = value
    End Set
  End Property

  Property ICMSDestTot_vFCPSTRet() As Decimal
    Get
      Return _ICMSDestTot_vFCPSTRet
    End Get
    Set(value As Decimal)
      _ICMSDestTot_vFCPSTRet = value
    End Set
  End Property

  Property ICMSTot_vIPIDevol() As Decimal
    Get
      Return _ICMSTot_vIPIDevol
    End Get
    Set(value As Decimal)
      _ICMSTot_vIPIDevol = value
    End Set
  End Property


  ''' <summary>
  ''' Reabre um recibo... retorna result(2=Não reaberto | 1=Reaberto) e msg_grava
  ''' </summary>
  ''' <param name="id_empresa"></param>
  ''' <param name="nNF"></param>
  Public Sub AlteraStsRecibo(ByVal id_empresa As Integer, ByVal nNF As Integer)

    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    str_builder.Append("EXEC sp9_NFe_Altera_Sts '0','0','LIBERAR_REC','" & id_empresa & "','1','" & nNF & "'")

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        Me.result = dr(0)
        Me.msg_grava = dr(1)
      Loop

      dr.Close()
    Catch ex As Exception
      Me.msg_retorno = "ERRO AO ALTERAR O STATUS DO RECIBO: " & ex.Message() & "---------" & ex.StackTrace()
    End Try

  End Sub

  Public Function ListaNotas(ByVal id_empresa As Integer, ByVal mes As Integer, ByVal ano As Integer, ByVal situacao As Integer, _
                             ByVal nNF As Integer, ByVal id_pedido As Integer) As DataView
    Dim conexao As New clsConexao
    Dim table As New DataTable
    Dim row As DataRow
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader
    Dim aaaamm As String
    Dim i As Integer = 0
    Dim tipo_doc As Integer = 0

    aaaamm = ano & Right("00" & mes, 2)

    table.Columns.Add(New DataColumn("id_nf"))
    table.Columns.Add(New DataColumn("nNF"))
    table.Columns.Add(New DataColumn("id_pedido"))
    table.Columns.Add(New DataColumn("cliente"))
    table.Columns.Add(New DataColumn("dt_emit"))
    table.Columns.Add(New DataColumn("valor_nfe"))
    table.Columns.Add(New DataColumn("sts_nf"))
    table.Columns.Add(New DataColumn("sts_nf_descr"))
    table.Columns.Add(New DataColumn("dt_entrega"))
    table.Columns.Add(New DataColumn("natOp"))
    table.Columns.Add(New DataColumn("arq_xml"))
    table.Columns.Add(New DataColumn("tipo_doc"))
    table.Columns.Add(New DataColumn("canc_arq_xml"))
    table.Columns.Add(New DataColumn("conta_eventos"))
    table.Columns.Add(New DataColumn("xml_correcao"))
    table.Columns.Add(New DataColumn("xml_inutilizada"))
    table.Columns.Add(New DataColumn("conta_financ"))
    table.Columns.Add(New DataColumn("conta_contab"))
    table.Columns.Add(New DataColumn("conta_estoque"))
    table.Columns.Add(New DataColumn("serv_id_lote"))
    table.Columns.Add(New DataColumn("serv_num_RPS"))
    table.Columns.Add(New DataColumn("url_NFSe"))
    table.Columns.Add(New DataColumn("id_pedido_amostra"))
    table.Columns.Add(New DataColumn("dt_visualiza_DANFE"))
    table.Columns.Add(New DataColumn("dt_visualiza_XML"))
    table.Columns.Add(New DataColumn("qtde_orcamento_nfe"))
    table.Columns.Add(New DataColumn("id_orcamento"))
    table.Columns.Add(New DataColumn("qtde_pedido_nfe"))

    str_builder.Append("EXEC sp9_Pega_NFe '" & id_empresa & "','" & aaaamm & "','99999','" & situacao & "'")
    str_builder.Append(",'0','0','" & tipo_doc & "','" & nNF & "','0'")
    str_builder.Append(",'','" & id_pedido & "','0','0'")

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        Me.qtde_linhas = dr.RecordsAffected
        i = 0
        row = table.NewRow()

        Do While i <= 27
          row(i) = dr(i)

          If i = 5 Then 'valor_nfe
            Me.valor_nfe_total = Me.valor_nfe_total + dr(5)
          End If

          i = i + 1
        Loop

        table.Rows.Add(row)
      Loop
      Me.qtde_linhas = table.Rows.Count()

      dr.Close()
    Catch ex As Exception
      Me.msg_retorno = "ERRO AO LISTAR AS NOTAS: " & ex.Message() & "---------" & ex.StackTrace()
    End Try


    Dim dv As New DataView(table)

    Return dv

  End Function

  Public Function ListaRelVendasProd(ByVal id_empresa As Integer, ByVal dt_ini As String, ByVal dt_fim As String, _
                                     ByVal tipo_doc As Integer, ByVal id_usuario As Integer, ByVal status As String, _
                                     ByVal exibe_fornec As String) As DataView
    Dim table As New DataTable
    Dim row As DataRow
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader
    Dim ajuste As New clsAjuste

    If exibe_fornec = "" Then exibe_fornec = "N"

    table.Columns.Add(New DataColumn("cProd"))
    table.Columns.Add(New DataColumn("xProd"))
    table.Columns.Add(New DataColumn("uCom"))
    table.Columns.Add(New DataColumn("soma_qCom"))
    table.Columns.Add(New DataColumn("soma_vProd"))
    table.Columns.Add(New DataColumn("preco_medido"))
    table.Columns.Add(New DataColumn("soma_nfe"))
    table.Columns.Add(New DataColumn("soma_rec"))
    table.Columns.Add(New DataColumn("id_item"))
    table.Columns.Add(New DataColumn("id_tipo"))
    table.Columns.Add(New DataColumn("id_subgrupo_item"))
    table.Columns.Add(New DataColumn("descr_tipo"))
    table.Columns.Add(New DataColumn("descr_subgrupo"))
    table.Columns.Add(New DataColumn("vProd_Compra"))
    table.Columns.Add(New DataColumn("id_fornec"))
    table.Columns.Add(New DataColumn("cnpj_fornec"))
    table.Columns.Add(New DataColumn("nome_fornec"))
    table.Columns.Add(New DataColumn("tipo_reg"))
    table.Columns.Add(New DataColumn("nNF"))
    table.Columns.Add(New DataColumn("dEmi"))
    table.Columns.Add(New DataColumn("id_nf"))
    table.Columns.Add(New DataColumn("descr_grupo"))

    str_builder.Append("EXEC sp9_Rel_Vendas_Prod '" & id_empresa & "','" & ajuste.AMD(dt_ini) & "','" & ajuste.AMD(dt_fim) & "','0'")
    str_builder.Append(",'VENDA','" & tipo_doc & "','99999','99999','99999','','','','0','0','" & id_usuario & "'")
    str_builder.Append(",'" & status & "','','0','" & exibe_fornec & "'")

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      dr.NextResult()

      Do While dr.Read()
        row = table.NewRow()

        row(0) = dr(0)
        row(1) = dr(1)
        row(2) = dr(2)
        row(3) = FormatNumber(dr(3), 2)
        row(4) = FormatNumber(dr(4), 2)
        row(5) = FormatNumber(dr(5), 2)
        row(6) = FormatNumber(dr(6), 2)
        row(7) = FormatNumber(dr(7), 2)
        row(8) = dr(8)
        row(9) = dr(9)
        row(10) = dr(10)
        row(11) = dr(11)
        row(12) = dr(12)
        row(13) = FormatNumber(dr(13), 2)
        row(14) = dr(14)
        row(15) = dr(15)
        row(16) = dr(16)
        row(17) = dr(17)
        row(18) = dr(18)
        row(19) = dr(19)
        row(20) = dr(20)
        row(21) = dr(21)

        table.Rows.Add(row)
      Loop

      dr.Close()
    Catch ex As Exception
      Me.msg_retorno = "ERRO AO CARREGAR RELATÓRIO: " & ex.Message() & "-----------" & ex.StackTrace()
    End Try

    Dim dv As New DataView(table)

    Return dv
  End Function

  Public Sub PegaIdeNfe(ByVal id_empresa As Integer, ByVal id_nf As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    str_builder.Append("EXEC sp9_Pega_NfeIde '" & id_empresa & "','" & id_nf & "'")


    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      Do While dr.Read()
        Me.cUF = dr(0)
        Me.cNF = dr(1)
        Me.natOp = dr(2)
        Me.indPag = dr(3)
        Me.mod_descr = dr(4)
        Me.serie = dr(5)
        Me.nNF = dr(6)
        Me.dEmi = dr(7)
        Me.dSaiEnt = dr(8)
        Me.tpNF = dr(9)
        Me.cMunFG = dr(10)
        Me.tpImp = dr(11)
        Me.tpEmis = dr(12)
        Me.cDV = dr(13)
        Me.tpAmb = dr(14)
        Me.finNFe = dr(15)
        Me.procEmi = dr(16)
        Me.verProc = dr(17)
        Me.hSaiEnt = dr(18)
        Me.infCpl = dr(19)
        Me.UF = dr(20)
        Me.cob_cond_pag = dr(21)
        Me.tipo_doc = dr(22)
        Me.sts_nf = dr(23)
        Me.infAdFisco = dr(24)
        Me.tb_cfop = dr(25)
        Me.origem_nf = dr(26)
        Me.id_nf_dev = dr(27)
        Me.id_pedido = dr(28)
        Me.id_pedido_amostra = dr(29)
        Me.id_orcamento = dr(30)
        Me.idDest = dr(31)
        Me.indFinal = dr(32)
        Me.indPres = dr(33)
        Me.nNF_anterior = dr(34)
        Me.conta_dupls_pedido = dr(35)
        Me.id_nf_retorno = dr(36)
        Me.ICMSTot_vNF = dr(37)
        Me.ICMSTot_vICMS = dr(38)
        Me.ICMSTot_vST = dr(39)
        Me.sts_NF_c = dr(40)
        Me.id_emit = dr(41)
        Me.id_dest = dr(42)
        Me.indIntermed = dr(45)
      Loop

      dr.NextResult() 'Informações de retorno da nota transimita ao governo...

      If dr.HasRows Then
        Do While dr.Read
          Me.sig_versao = dr(0)
          Me.sig_tpAmb = dr(1)
          Me.sig_verAplic = dr(2)
          Me.sig_chNFe = dr(3)
          Me.sig_dhRecbto = dr(4)
          Me.sig_nProt = dr(5)
          Me.sig_digVal = dr(6)
          Me.sig_cStat = dr(7)
          Me.sig_xMotivo = dr(8)
          Me.arq_xml = dr(9)
          Me.canc_nProt = dr(10)
          Me.canc_dhRecbto = dr(11)
          Me.canc_xJust = dr(12)
          Me.inut_nProt = dr(13)
          Me.inut_dhRecbto = dr(14)
          Me.inut_xJust = dr(15)
        Loop
      End If
      conexao.FechaBanco(conexao1)
      dr.Close()
    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      Me.msg_retorno = "ERRO AO CARREGAR A ABA DADOS: " & ex.StackTrace() & "---------" & ex.StackTrace()
    End Try

  End Sub

  Public Sub NfeIdeAtualiza(ByVal id_empresa As Integer, ByVal id_nf As Integer, ByVal cUF As Integer, ByVal cNF As Integer, _
                            ByVal natOp As String, ByVal indPag As Integer, ByVal mod_descr As String, ByVal serie As Integer, _
                            ByVal nNF As Integer, ByVal dEmi As String, ByVal dSaiEnt As String, ByVal hSaiEnt As String, _
                            ByVal tpNF As Integer, ByVal cMunFG As Integer, ByVal tpImp As Integer, ByVal tpEmis As Integer, _
                            ByVal cDV As Integer, ByVal tpAmb As Integer, ByVal finNFe As Integer, ByVal procEmi As Integer, _
                            ByVal verProc As String, ByVal tipo_doc As Integer, ByVal id_novo_natOp As Integer, ByVal id_nf_dev As Integer, _
                            ByVal indFinal As Integer, ByVal idDest As Integer, ByVal indPres As Integer)

    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader
    Dim acao As String = ""
    Dim ajuste As New clsAjuste

    dEmi = ajuste.FormataDataNFe(dEmi)
    dSaiEnt = ajuste.FormataDataNFe(dSaiEnt)

    str_builder.Append("EXEC sp9_NFE_ide_Atualiza '" & id_empresa & "','" & id_nf & "','" & cUF & "','" & cNF & "'")
    str_builder.Append(",'" & natOp & "','" & indPag & "','" & mod_descr & "','" & serie & "','" & nNF & "'")
    str_builder.Append(",'" & dEmi & "','" & dSaiEnt & "','" & hSaiEnt & "','" & tpNF & "','" & cMunFG & "'")
    str_builder.Append(",'" & tpImp & "','" & tpEmis & "','" & cDV & "','" & tpAmb & "','" & finNFe & "'")
    str_builder.Append(",'" & procEmi & "','" & verProc & "','" & tipo_doc & "','" & id_novo_natOp & "'")
    str_builder.Append(",'" & id_nf_dev & "','" & acao & "','" & indFinal & "','" & idDest & "','" & indPres & "'")


    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        Me.result = dr(0)
        Me.msg_retorno = dr(1)
      Loop

      dr.Close()
    Catch ex As Exception
      Me.result = -1
      Me.msg_retorno = "ERRO AO SALVAR OS DADOS DA NFE: " & ex.Message() & "-----------" & ex.StackTrace()
    End Try

  End Sub

  Public Sub PegaTotalUmaNfe(ByVal id_empresa As Integer, ByVal id_nf As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    str_builder.Append("EXEC sp9_Pega_Totais_Uma_NFe '" & id_empresa & "','" & id_nf & "'")

    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      Do While dr.Read()
        Me.ICMSTot_vBC = dr(0)
        Me.ICMSTot_vICMS = dr(1)
        Me.ICMSTot_vBCST = dr(2)
        Me.ICMSTot_vST = dr(3)
        Me.ICMSTot_vProd = dr(4)
        Me.ICMSTot_vFrete = dr(5)
        Me.ICMSTot_vSeg = dr(6)
        Me.ICMSTot_vDesc = dr(7)
        Me.ICMSTot_vII = dr(8)
        Me.ICMSTot_vIPI = dr(9)
        Me.ICMSTot_vPIS = dr(10)
        Me.ICMSTot_vCOFINS = dr(11)
        Me.ICMSTot_vOutro = dr(12)
        Me.ICMSTot_vNF = dr(13)
        Me.ISSQNtot_vServ = dr(14)
        Me.ISSQNtot_vBC = dr(15)
        Me.ISSQNtot_vISS = dr(16)
        Me.ISSQNtot_vPIS = dr(17)
        Me.ISSQNtot_vCOFINS = dr(18)
        Me.retTrib_vRetPIS = dr(19)
        Me.retTrib_vRetCOFINS = dr(20)
        Me.retTrib_vRetCSLL = dr(21)
        Me.retTrib_vBCIRRF = dr(22)
        Me.retTrib_vIRRF = dr(23)
        Me.retTrib_vBCRetPrev = dr(24)
        Me.retTrib_vRetPrev = dr(25)
        Me.cob_nFat = dr(26)
        Me.cob_vOrig = dr(27)
        Me.cob_vDesc = dr(28)
        Me.cob_vLiq = dr(29)
        Me.f_Total = dr(30)
        Me.ICMS_vCredICMSSN = dr(31)
        Me.pCred_ICMS = dr(32)
        Me.vTotTrib = dr(33)
        Me.TOTvFCPUFDest = dr(34)
        Me.ICMSTot_vICMSUFDest = dr(35)
        Me.ICMSTot_vICMSUFRemet = dr(36)
        Me.cob_cond_pag = dr(37)
        Me.ICMSTot_vICMSDeson = dr(38)
        Me.ICMSDestTot_vFCP = dr(39)
        Me.ICMSDestTot_vFCPST = dr(40)
        Me.ICMSDestTot_vFCPSTRet = dr(41)
        Me.ICMSTot_vIPIDevol = dr(42)
      Loop
      conexao.FechaBanco(conexao1)
      dr.Close()
    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      Me.msg_retorno = "ERRO AO PEGAR OS TOTAIS DA NFE: " & ex.Message() & "--------------" & ex.StackTrace()
    End Try
  End Sub

  Public Sub SalvaTotaisUmaNFe(ByVal id_nf As Decimal, ByVal ICMSTot_vBC As Decimal, ByVal ICMSTot_vICMS As Decimal, ByVal ICMSTot_vBCST As Decimal, ByVal ICMSTot_vST As Decimal, _
                               ByVal ICMSTot_vProd As Decimal, ByVal ICMSTot_vFrete As Decimal, _
                               ByVal ICMSTot_vSeg As Decimal, ByVal ICMSTot_vDesc As Decimal, ByVal ICMSTot_vII As Decimal, ByVal ICMSTot_vIPI As Decimal, _
                               ByVal ICMSTot_vPIS As Decimal, ByVal ICMSTot_vCOFINS As Decimal, ByVal ICMSTot_vOutro As Decimal, ByVal ICMSTot_vNF As Decimal, _
                               ByVal ISSQNtot_vServ As Decimal, ByVal ISSQNtot_vBC As Decimal, ByVal ISSQNtot_vISS As Decimal, ByVal ISSQNtot_vPIS As Decimal, _
                               ByVal ISSQNtot_vCOFINS As Decimal, ByVal retTrib_vRetPIS As Decimal, ByVal retTrib_vRetCOFINS As Decimal, ByVal retTrib_vRetCSLL As Decimal, _
                               ByVal retTrib_vBCIRRF As Decimal, ByVal retTrib_vIRRF As Decimal, ByVal retTrib_vBCRetPrev As Decimal, ByVal retTrib_vRetPrev As Decimal, _
                               ByVal vTotTrib As Decimal)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim ICMSTot_vBC_str, ICMSTot_vProd_str, ICMSTot_vFrete_str, ICMSTot_vSeg_str, ICMSTot_vDesc_str, ICMSTot_vII_str, ICMSTot_vIPI_str As String
    Dim ICMSTot_vPIS_str, ICMSTot_vCOFINS_str, ICMSTot_vOutro_str, ICMSTot_vNF_str, ISSQNtot_vServ_str, ISSQNtot_vBC_str, ISSQNtot_vISS_str As String
    Dim ISSQNtot_vPIS_str, ISSQNtot_vCOFINS_str, retTrib_vRetPIS_str, retTrib_vRetCOFINS_str, retTrib_vRetCSLL_str, retTrib_vBCIRRF_str As String
    Dim retTrib_vIRRF_str, retTrib_vBCRetPrev_str, retTrib_vRetPrev_str, vTotTrib_str, ICMSTot_vBCST_str, ICMSTot_vICMS_str, ICMSTot_vST_str As String

    ICMSTot_vBCST_str = Replace(Replace(ICMSTot_vBCST, ".", ""), ",", ".")
    ICMSTot_vICMS_str = Replace(Replace(ICMSTot_vICMS, ".", ""), ",", ".")
    ICMSTot_vST_str = Replace(Replace(ICMSTot_vST, ".", ""), ",", ".")
    ICMSTot_vBC_str = Replace(Replace(ICMSTot_vBC, ".", ""), ",", ".")
    ICMSTot_vProd_str = Replace(Replace(ICMSTot_vProd, ".", ""), ",", ".")
    ICMSTot_vFrete_str = Replace(Replace(ICMSTot_vFrete, ".", ""), ",", ".")
    ICMSTot_vSeg_str = Replace(Replace(ICMSTot_vSeg, ".", ""), ",", ".")
    ICMSTot_vDesc_str = Replace(Replace(ICMSTot_vDesc, ".", ""), ",", ".")
    ICMSTot_vII_str = Replace(Replace(ICMSTot_vII, ".", ""), ",", ".")
    ICMSTot_vIPI_str = Replace(Replace(ICMSTot_vIPI, ".", ""), ",", ".")
    ICMSTot_vPIS_str = Replace(Replace(ICMSTot_vPIS, ".", ""), ",", ".")
    ICMSTot_vCOFINS_str = Replace(Replace(ICMSTot_vCOFINS, ".", ""), ",", ".")
    ICMSTot_vOutro_str = Replace(Replace(ICMSTot_vOutro, ".", ""), ",", ".")
    ICMSTot_vNF_str = Replace(Replace(ICMSTot_vNF, ".", ""), ",", ".")
    ISSQNtot_vServ_str = Replace(Replace(ISSQNtot_vServ, ".", ""), ",", ".")
    ISSQNtot_vBC_str = Replace(Replace(ISSQNtot_vBC, ".", ""), ",", ".")
    ISSQNtot_vISS_str = Replace(Replace(ISSQNtot_vISS, ".", ""), ",", ".")
    ISSQNtot_vPIS_str = Replace(Replace(ISSQNtot_vPIS, ".", ""), ",", ".")
    ISSQNtot_vCOFINS_str = Replace(Replace(ISSQNtot_vCOFINS, ".", ""), ",", ".")
    retTrib_vRetPIS_str = Replace(Replace(retTrib_vRetPIS, ".", ""), ",", ".")
    retTrib_vRetCOFINS_str = Replace(Replace(retTrib_vRetCOFINS, ".", ""), ",", ".")
    retTrib_vRetCSLL_str = Replace(Replace(retTrib_vRetCSLL, ".", ""), ",", ".")
    retTrib_vBCIRRF_str = Replace(Replace(retTrib_vBCIRRF, ".", ""), ",", ".")
    retTrib_vIRRF_str = Replace(Replace(retTrib_vIRRF, ".", ""), ",", ".")
    retTrib_vBCRetPrev_str = Replace(Replace(retTrib_vBCRetPrev, ".", ""), ",", ".")
    retTrib_vRetPrev_str = Replace(Replace(retTrib_vRetPrev, ".", ""), ",", ".")
    vTotTrib_str = Replace(Replace(vTotTrib, ".", ""), ",", ".")

    str_builder.Append("EXEC sp9_NFE_ide_totais_Atualiza '" & id_nf & "','" & ICMSTot_vBC_str & "','" & ICMSTot_vICMS_str & "'")
    str_builder.Append(",'" & ICMSTot_vBCST_str & "','" & ICMSTot_vST_str & "','" & ICMSTot_vProd_str & "','" & ICMSTot_vFrete_str & "'")
    str_builder.Append(",'" & ICMSTot_vSeg_str & "','" & ICMSTot_vDesc_str & "','" & ICMSTot_vII_str & "','" & ICMSTot_vIPI_str & "','" & ICMSTot_vPIS_str & "'")
    str_builder.Append(",'" & ICMSTot_vCOFINS_str & "','" & ICMSTot_vOutro_str & "','" & ICMSTot_vNF_str & "','" & ISSQNtot_vServ_str & "','" & ISSQNtot_vBC_str & "'")
    str_builder.Append(",'" & ISSQNtot_vISS_str & "','" & ISSQNtot_vPIS_str & "','" & ISSQNtot_vCOFINS_str & "','" & retTrib_vRetPIS_str & "','" & retTrib_vRetCOFINS_str & "'")
    str_builder.Append(",'" & retTrib_vRetCSLL_str & "','" & retTrib_vBCIRRF_str & "','" & retTrib_vIRRF_str & "','" & retTrib_vBCRetPrev_str & "','" & retTrib_vRetPrev_str & "'")
    str_builder.Append(",'" & vTotTrib_str & "'")

    Try
      conexao.ExecutaComando(str_builder.ToString())

    Catch ex As Exception
      Me.msg_retorno = "ERRO AO SALVAR A ABA TOTAIS: " & ex.Message() & "---------------" & ex.StackTrace()
    End Try

  End Sub

  Public Sub PegaTransporteUmaNfe(ByVal id_nf As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    str_builder.Append("SELECT transp_modFrete, transp_tipo_pessoa, transp_CNPJ, ISNULL((dbo.fLC(transp_xNome)), ''), ")
    str_builder.Append("ISNULL((transp_IE), ''), ISNULL((dbo.fLC(transp_xEnder)), ''), ISNULL((dbo.fLC(transp_xMun)), ''), ISNULL((transp_UF), '--'), ")
    str_builder.Append("retTransp_vServ, retTransp_vBCRet, retTransp_pICMSRet, retTransp_vICMSRet, retTransp_CFOP, A.retTransp_cMunFG, ")
    str_builder.Append("ISNULL((veicTransp_placa), ''), ISNULL((veicTransp_UF), '--'), ISNULL((veicTransp_RNTC), ''), B.UF as retTransp_cUF ")
    str_builder.Append("FROM NFE_ide AS A ")
    str_builder.Append("INNER JOIN NFE_Municipio as B ON (B.cMun = A.retTransp_cMunFG) ")
    str_builder.Append("WHERE (id_nf =  " & id_nf & ") ")

    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      Do While dr.Read()
        transp_modFrete = dr(0)
        transp_tipo_pessoa = dr(1)
        transp_CNPJ = dr(2)
        transp_xNome = dr(3)
        transp_IE = dr(4)
        transp_xEnder = dr(5)
        transp_xMun = dr(6)
        transp_UF = dr(7)
        retTransp_vServ = dr(8)
        retTransp_vBCRet = dr(9)
        retTransp_pICMSRet = dr(10)
        retTransp_vICMSRet = dr(11)
        retTransp_CFOP = dr(12)
        retTransp_cMunFG = dr(13)
        veicTransp_placa = dr(14)
        veicTransp_UF = dr(15)
        veicTransp_RNTC = dr(16)
        retTransp_cUF = dr(17)
      Loop
      conexao.FechaBanco(conexao1)
      dr.Close()
    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      Me.msg_retorno = "ERRO AO PEGAR INFORMAÇÕES ABA TRANSPORTE: " & ex.Message() & "-----------" & ex.StackTrace()
    End Try
  End Sub

  Public Sub SalvaTranspUmaNFe(ByVal id_nf As Integer, ByVal transp_modFrete As String, ByVal transp_CNPJ As String, ByVal transp_CPF As String, _
                               ByVal transp_xNome As String, ByVal transp_IE As String, ByVal transp_xEnder As String, ByVal transp_xMun As String, _
                               ByVal transp_UF As String, ByVal retTransp_vServ As Decimal, ByVal retTransp_vBCRet As Decimal, _
                               ByVal retTransp_pICMSRet As Decimal, ByVal retTransp_vICMSRet As Decimal, ByVal retTransp_CFOP As Integer, _
                               ByVal retTransp_cMunFG As Integer, ByVal veicTransp_placa As String, ByVal veicTransp_UF As String, _
                               ByVal veicTransp_RNTC As String)

    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    If transp_CNPJ = "" Then transp_CNPJ = "0"
    If transp_CPF = "" Then transp_CPF = "0"

    str_builder.Append("EXEC sp9_NFE_ide_transp_Atualiza '" & id_nf & "','" & transp_modFrete & "','" & transp_CNPJ & "'")
    str_builder.Append(",'" & transp_CPF & "','" & transp_xNome & "','" & transp_IE & "','" & transp_xEnder & "','" & transp_xMun & "'")
    str_builder.Append(",'" & transp_UF & "','" & retTransp_vServ & "','" & retTransp_vBCRet & "','" & retTransp_pICMSRet & "'")
    str_builder.Append(",'" & retTransp_vICMSRet & "','" & retTransp_CFOP & "','" & retTransp_cMunFG & "','" & veicTransp_placa & "'")
    str_builder.Append(",'" & veicTransp_UF & "','" & veicTransp_RNTC & "'")

    Try
      conexao.ExecutaComando(str_builder.ToString())

    Catch ex As Exception
      Me.msg_retorno = "ERRO AO SALVAR AS INFORMAÇÕES DA ABA TRANSPORTE: " & ex.Message() & "----------" & ex.StackTrace()
    End Try

  End Sub

  Public Sub PegaInfAdicUmaNfe(ByVal id_nf As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    str_builder.Append("SELECT ISNULL((dbo.fLC(infCpl)), '') AS infCpl,ISNULL(( dbo.fLC(infAdFisco)), '') AS infAdFisco ")
    str_builder.Append("FROM NFE_ide ")
    str_builder.Append("WHERE (id_nf = " & id_nf & ")")

    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      Do While dr.Read()
        Me.infCpl = dr(0)
        Me.infAdFisco = dr(1)
      Loop
      conexao.FechaBanco(conexao1)
      dr.Close()
    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      Me.msg_retorno = "ERRO AO PEGAR AS INFORMAÇÕES ADICIONAIS: " & ex.Message() & "--------" & ex.StackTrace()
    End Try
  End Sub

  Public Sub SalvaInfAdicUmaNfe(ByVal id_nf As Integer, ByVal infCpl As String, ByVal infAdFisco As String)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    infCpl = infCpl.Replace("'", "''")
    infAdFisco = infAdFisco.Replace("'", "''")

    str_builder.Append("EXEC sp9_NFE_ide_infAdic_Atualiza '" & id_nf & "','" & infCpl & "','" & infAdFisco & "'")

    Try
      conexao.ExecutaComando(str_builder.ToString())

    Catch ex As Exception
      Me.msg_retorno = "ERRO AO SALVAR AS INFORMAÇÕES ADICIONAIS: " & ex.Message() & "---------" & ex.StackTrace()
    End Try
  End Sub

  Public Sub PegaExpComprasNfe(ByVal id_nf As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    str_builder.Append("SELECT ISNULL((exp_UFEmbarq), '--'), ISNULL((exp_xLocEmbarq), ''), ISNULL((exp_xNEmp), ''), ")
    str_builder.Append("ISNULL((exp_xPed), ''), ISNULL((exp_xCont), ''), ISNULL((exp_xLocDespacho), '') ")
    str_builder.Append("FROM NFE_ide ")
    str_builder.Append("WHERE (id_nf = " & id_nf & ") ")

    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      Do While dr.Read()
        Me.exp_UFEmbarq = dr(0)
        Me.exp_xLocEmbarq = dr(1)
        Me.exp_xNEmp = dr(2)
        Me.exp_xPed = dr(3)
        Me.exp_xCont = dr(4)
        Me.exp_xLocDespacho = dr(5)
      Loop
      conexao.FechaBanco(conexao1)
      dr.Close()
    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      Me.msg_retorno = "ERRO AO LISTAR EXPORTAÇÃO E COMPRAS: " & ex.Message() & "----------" & ex.StackTrace()
    End Try

  End Sub

  Public Sub SalvaExportComprasUmaNfe(ByVal id_nf As Integer, ByVal exp_UFEmbarq As String, ByVal exp_xLocEmbarq As String, _
                                      ByVal exp_xNEmp As String, ByVal exp_xPed As String, ByVal exp_xCont As String, ByVal exp_xLocDespacho As String)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_NFE_ide_exp_Atualiza '" & id_nf & "','" & exp_UFEmbarq & "','" & exp_xLocEmbarq & "'")
    str_builder.Append(",'" & exp_xNEmp & "','" & exp_xPed & "','" & exp_xCont & "','" & exp_xLocDespacho & "'")

    Try
      conexao.ExecutaComando(str_builder.ToString())

    Catch ex As Exception
      Me.msg_retorno = "ERRO AO SALVAR EXPORTAÇÃO E COMPRAS: " & ex.Message() & "---------" & ex.StackTrace()
    End Try
  End Sub

  Public Function PegaChaveAcessoNFE(ByVal id_nf As Integer) As String
    'http://www.javac.com.br/jc/posts/list/134-nfe-exemplo-de-geracao-da-chave-de-acesso-nfe-20.page

    Dim chave_acesso As String = ""
    Dim conexao As New clsConexao
    Dim dr As SqlDataReader
    Dim str_builder As New StringBuilder
    Dim ano As String = ""
    Dim mes As String = ""
    Dim cnpj As String = ""

    str_builder.Append("SELECT A.cUF,  A.cNF, A.mod, A.serie, A.nNF, MONTH(A.dEmi) AS mes_dEmit,  ")
    str_builder.Append("YEAR(A.dEmi) AS ano_dEmit, A.tpEmis, B.cnpj ")
    str_builder.Append("FROM NFE_ide AS A ")
    str_builder.Append("INNER JOIN NFE_emit AS B ON (B.id_emit = A.id_emit) ")
    str_builder.Append("WHERE (id_nf = " & id_nf & ") ")

    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      Do While dr.Read()
        Me.cUF = dr(0)
        Me.cNF = dr(1)
        Me.mod_descr = dr(2)
        Me.serie = dr(3)
        Me.nNF = dr(4)
        mes = dr(5)
        ano = dr(6)
        Me.tpEmis = dr(7)
        cnpj = dr(8)
      Loop
      conexao.FechaBanco(conexao1)
      dr.Close()

      chave_acesso = CriaChaveNFe2G(Me.cUF, ano, mes, cnpj, Me.mod_descr, Me.serie, Me.nNF, Me.tpEmis, Me.cNF)
    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      Me.msg_retorno = "ERRO PEGAR OS DADOS PARA GERAR A CHAVE DE ACESSO: " & ex.Message() & "----------" & ex.StackTrace()
    End Try

    Return chave_acesso

  End Function

  ''' <summary>
  ''' ano = AAAA
  ''' mes = MM
  ''' </summary>
  ''' <param name="cUF"></param>
  ''' <param name="ano"></param>
  ''' <param name="mes"></param>
  ''' <param name="CNPJ"></param>
  ''' <param name="modelo"></param>
  ''' <param name="serie"></param>
  ''' <param name="nNF"></param>
  ''' <param name="tpEmis"></param>
  ''' <param name="cNF"></param>
  ''' <returns></returns>
  Protected Function CriaChaveNFe2G(ByVal cUF As Integer, ByVal ano As String, ByVal mes As String, ByVal CNPJ As String, _
                                    ByVal modelo As Integer, ByVal serie As Integer, ByVal nNF As String, _
                                    ByVal tpEmis As String, ByVal cNF As String) As String
    Dim chave_acesso As New StringBuilder
    Dim calcula_mod As New clsCalculaMod
    Dim dv As Integer = 0

    Try
      chave_acesso.Append(Right("00" & cUF, 2))
      chave_acesso.Append(Right(ano, 2) & Right("00" & mes, 2))
      chave_acesso.Append(Right("00000000000000" & CNPJ, 14))
      chave_acesso.Append(Right("00" & modelo, 2))
      chave_acesso.Append(Right("000" & serie, 3))
      chave_acesso.Append(Right("000000000" & nNF, 9))
      chave_acesso.Append(tpEmis)
      chave_acesso.Append(Right("00000000" & cNF, 8))

      dv = calcula_mod.Mod11(chave_acesso.ToString())

      chave_acesso.Append(dv)

    Catch ex As Exception
      Me.msg_retorno = "ERRO AO CRIAR A CHAVE DE ACESSO DA NOTA FISCAL: " & ex.Message() & "----------" & ex.StackTrace()
    End Try

    Return chave_acesso.ToString()

  End Function

  ''' <summary>
  ''' Retorno o id_nf da nota gerada... Caso o valor seja 0, erro ao gerar a nf...
  ''' Caso a nota seja para um cliente, informar id_fornec = 0 e vice-versa...
  ''' </summary>
  ''' <param name="id_empresa"></param>
  ''' <param name="id_nat_op"></param>
  ''' <param name="id_cliente"></param>
  ''' <param name="id_fornec"></param>
  ''' <param name="id_usuario"></param>
  ''' <returns></returns>
  Public Function GeraNovaNFe(ByVal id_empresa As Integer, ByVal id_nat_op As Integer, ByVal id_cliente As Integer, _
                              ByVal id_fornec As Integer, ByVal id_usuario As Integer) As Integer
    Dim id_nf As Integer = 0
    Dim conexao As New clsConexao
    Dim dr As SqlDataReader
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_Nova_NFE '" & id_empresa & "','" & id_nat_op & "','" & id_cliente & "','" & id_fornec & "'")
    str_builder.Append(",'" & id_usuario & "'")

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        id_nf = dr(0)
      Loop

      dr.Close()
    Catch ex As Exception
      msg_retorno = "ERRO AO GERAR A NOTA FISCAL: " & ex.Message() & "----------" & ex.StackTrace()
    End Try

    Return id_nf

  End Function

  ''' <summary>
  ''' Retorna: result e msg_retorno...
  ''' result(-1 = ERRO NA EXECUSÃO | 0 = OK | >0 NOTA NÃO EXCLUÍDA)
  ''' </summary>
  ''' <param name="id_empresa"></param>
  ''' <param name="id_nf"></param>
  ''' <param name="id_usuario"></param>
  Public Sub ExcluiUmaNFe(ByVal id_empresa As Integer, ByVal id_nf As Integer, ByVal id_usuario As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    str_builder.Append("EXEC sp9_NFE_Apaga '" & id_empresa & "','" & id_nf & "','" & id_usuario & "'")

    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      Do While dr.Read()
        Me.result = dr(0)
        Me.msg_retorno = dr(1)
      Loop
      conexao.FechaBanco(conexao1)
      dr.Close()
    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      Me.result = -1
      Me.msg_retorno = "ERRO AO EXCLUÍR A NOTA FISCAL: " & ex.Message() & "----------" & ex.StackTrace()
    End Try

  End Sub

  Public Function ListaNFeParaInutilizacao(ByVal id_empresa As Integer, ByVal ano As Integer, ByVal mes As Integer) As DataView
    Dim table As New DataTable
    Dim row As DataRow
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader
    Dim aaaamm As String = ano & Right("00" & mes, 2)

    table.Columns.Add(New DataColumn("serie"))
    table.Columns.Add(New DataColumn("ini_interv"))
    table.Columns.Add(New DataColumn("fim_interv"))
    table.Columns.Add(New DataColumn("ano"))
    table.Columns.Add(New DataColumn("tpAmb"))

    str_builder.Append("EXEC sp9_NFe_Inutiliza_Pega '" & id_empresa & "','" & aaaamm & "'")

    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      Do While dr.Read()
        row = table.NewRow()

        row(0) = dr(0)
        row(1) = dr(1)
        row(2) = dr(2)
        row(3) = dr(3)
        row(4) = dr(4)

        table.Rows.Add(row)
      Loop
      conexao.FechaBanco(conexao1)
      dr.Close()
    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      Me.result = -1
      Me.msg_retorno = "ERRO AO LISTAR AS NOTAS FISCAIS PARA INUTILIZAÇÃO: " & ex.Message() & "--------" & ex.StackTrace()
    End Try

    Dim dv As New DataView(table)

    Return dv
  End Function

  Public Function ListaNfeInutilizadas(ByVal id_empresa As Integer, ByVal ano As Integer, ByVal mes As Integer) As DataView
    Dim table As New DataTable
    Dim row As DataRow
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader
    Dim aaaamm As String = ano & Right("00" & mes, 2)

    table.Columns.Add(New DataColumn("inut_dtRecbto"))
    table.Columns.Add(New DataColumn("inut_nProt"))
    table.Columns.Add(New DataColumn("inut_xJust"))
    table.Columns.Add(New DataColumn("serie"))
    table.Columns.Add(New DataColumn("nNFIni"))
    table.Columns.Add(New DataColumn("nNFFin"))
    table.Columns.Add(New DataColumn("xml_inutiliza"))

    str_builder.Append("EXEC sp9_NFe_Inutiliza_Pega '" & id_empresa & "','" & aaaamm & "','0', '50'")

    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      dr.NextResult()

      Do While dr.Read()
        row = table.NewRow()

        row(0) = dr(0)
        row(1) = dr(1)
        row(2) = dr(2)
        row(3) = dr(3)
        row(4) = dr(4)
        row(5) = dr(5)
        row(6) = dr(6)

        table.Rows.Add(row)
      Loop
      conexao.FechaBanco(conexao1)
      dr.Close()
    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      Me.result = -1
      Me.msg_retorno = "ERRO AO LISTAR AS NOTAS FISCAIS PARA INUTILIZAÇÃO: " & ex.Message() & "--------" & ex.StackTrace()
    End Try

    Dim dv As New DataView(table)

    Return dv

  End Function

  Public Function ListaNFePendenteEnvio(ByVal id_empresa As Integer) As XmlDocument
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader = Nothing
    Dim NotasFiscais As New XmlDocument
    Dim notas, nota, nNF, id_nf, ICMSTot_vNF, xNome As XmlElement

    str_builder.Append("SELECT A.id_nf, A.nNF, A.ICMSTot_vNF, B.xNome ")
    str_builder.Append("FROM NFE_ide AS A ")
    str_builder.Append("INNER JOIN NFE_dest AS B ON (B.id_dest = A.id_dest) ")
    str_builder.Append("WHERE (A.tipo_doc = 0) AND (A.id_empresa = " & id_empresa & ") AND ")
    str_builder.Append("(A.sts_nf = 0) ")

    If id_empresa = 1504 Then 'Retap
      str_builder.Append("AND (dEmi > '2018-01-01') ")
    End If

    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      notas = NotasFiscais.CreateElement("NotasFiscais")
      NotasFiscais.AppendChild(notas)

      Do While dr.Read()
        nota = NotasFiscais.CreateElement("NFe")
        notas.AppendChild(nota)

        id_nf = NotasFiscais.CreateElement("id_nf")
        id_nf.InnerText = dr(0)
        nota.AppendChild(id_nf)

        nNF = NotasFiscais.CreateElement("nNF")
        nNF.InnerText = dr(1)
        nota.AppendChild(nNF)

        ICMSTot_vNF = NotasFiscais.CreateElement("ICMSTot_vNF")
        ICMSTot_vNF.InnerText = dr(2)
        nota.AppendChild(ICMSTot_vNF)

        xNome = NotasFiscais.CreateElement("xNome")
        xNome.InnerText = Trim(dr(3))
        nota.AppendChild(xNome)
      Loop
      conexao.FechaBanco(conexao1)
      dr.Close()

    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      Me.msg_retorno = "ERRO AO LISTAR AS NOTAS FISCAIS: " & ex.Message() & "----------" & ex.StackTrace()
    End Try

    Return NotasFiscais

  End Function

  Public Function ListaNFePendenteEnvioAut(ByVal str_id_empresa As String) As XmlDocument
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader = Nothing
    Dim NotasFiscais As New XmlDocument
    Dim notas, nota, nNF, id_nf, ICMSTot_vNF, xNome, id_empresa_element As XmlElement

    notas = NotasFiscais.CreateElement("NotasFiscais")
    NotasFiscais.AppendChild(notas)


    Dim i As Integer = 100
    Dim id_empresa As Integer
    Dim j As Integer

    Do While i > 0
      If str_id_empresa.Length <= 0 Then
        Exit Do
      End If

      str_builder = New StringBuilder
      i = str_id_empresa.IndexOf("|", 1)
      id_empresa = Left(str_id_empresa, i)


      If id_empresa = 328 Or id_empresa = 1438 Then 'CDA e Fabrica de Histórinhas... tem que gerar as notas antes de buscá-las...
        GeraNotasLote(id_empresa)
      End If


      str_builder.Append("SELECT A.id_nf, A.nNF, A.ICMSTot_vNF, B.xNome ")
      str_builder.Append("FROM NFE_ide AS A ")
      str_builder.Append("INNER JOIN NFE_dest AS B ON (B.id_dest = A.id_dest) ")
      str_builder.Append("WHERE (A.tipo_doc = 0) AND (A.id_empresa = " & id_empresa & ") AND ")
      str_builder.Append("(A.sts_nf = 0) ")

      If id_empresa = 328 Then 'CDA
        str_builder.Append("AND (dEmi > '2018-01-01') ")
      End If

      Try
        'dr = conexao.RetornaDataReader(str_builder.ToString())
        conexao1 = conexao.AbreBanco()
        dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

        Do While dr.Read()
          nota = NotasFiscais.CreateElement("NFe")
          notas.AppendChild(nota)

          id_empresa_element = NotasFiscais.CreateElement("id_empresa")
          id_empresa_element.InnerText = id_empresa
          nota.AppendChild(id_empresa_element)

          id_nf = NotasFiscais.CreateElement("id_nf")
          id_nf.InnerText = dr(0)
          nota.AppendChild(id_nf)

          nNF = NotasFiscais.CreateElement("nNF")
          nNF.InnerText = dr(1)
          nota.AppendChild(nNF)

          ICMSTot_vNF = NotasFiscais.CreateElement("ICMSTot_vNF")
          ICMSTot_vNF.InnerText = dr(2)
          nota.AppendChild(ICMSTot_vNF)

          xNome = NotasFiscais.CreateElement("xNome")
          xNome.InnerText = Trim(dr(3))
          nota.AppendChild(xNome)
        Loop
        conexao.FechaBanco(conexao1)
        dr.Close()
        dr = Nothing

      Catch ex As Exception
        conexao.FechaBanco(conexao1)
        Dim erro As XmlElement
        erro = NotasFiscais.CreateElement("xErro")
        erro.InnerText = Trim("ERRO AO LISTAR AS NOTAS FISCAIS: " & ex.Message() & "----------" & ex.StackTrace())
        nota.AppendChild(erro)
        'Me.msg_retorno = 
      End Try


      j = (str_id_empresa.Length - i) - 1
      str_id_empresa = str_id_empresa.Substring(i + 1, j)
    Loop

    Return NotasFiscais

  End Function

  Private Sub GeraNotasLote(ByVal id_empresa As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dt_ini As String = Year(Date.Now()) & "-" & Right("00" & Month(Date.Now()), 2) & "-01" 'AAAAMMDD
    Dim dt_fim As String = Year(Date.Now()) & "-" & Right("00" & Month(Date.Now()), 2) & "-" & Right("00" & DateTime.DaysInMonth(Year(Date.Now()), Month(Date.Now())), 2)

    str_builder.Append("EXEC sp9_Importa_Gera_NFE '" & id_empresa & "','" & dt_ini & "','" & dt_fim & "'")

    Try
      conexao.ExecutaComando(str_builder.ToString())

    Catch ex As Exception
      Me.msg_retorno = "ERRO AO GERAR AS NOTAS FISCAIS ATRAVÉS DE LOTES: " & ex.Message() & "----------" & ex.StackTrace()
    End Try
  End Sub

  Public Function ListaLoteNFServPendente(ByVal id_empresa As Integer) As XmlDocument
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader = Nothing
    Dim NotasFiscais As New XmlDocument
    Dim nota, lotes, id_lote, dt_cria, vlr_total_lote As XmlElement

    str_builder.Append("SELECT A.id_lote, dbo.fFormata_Data(A.dt_cria, 2) AS dt_cria, ")
    str_builder.Append("(SELECT SUM(Z.ICMSTot_vProd) FROM NFE_ide AS Z WHERE (Z.id_empresa = A.id_empresa) AND (Z.serv_id_lote = A.id_lote)) AS vlr_total_lote ")
    str_builder.Append("FROM NFe_Serv_Lote AS A ")
    str_builder.Append("WHERE (A.id_empresa = " & id_empresa & ") AND (A.sts_lote = 0) ")


    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      nota = NotasFiscais.CreateElement("LotesNotaFiscalServ")
      NotasFiscais.AppendChild(nota)

      Do While dr.Read()
        lotes = NotasFiscais.CreateElement("Lote")
        nota.AppendChild(lotes)

        id_lote = NotasFiscais.CreateElement("id_lote")
        id_lote.InnerText = dr(0)
        lotes.AppendChild(id_lote)

        dt_cria = NotasFiscais.CreateElement("dt_cria")
        dt_cria.InnerText = dr(1)
        lotes.AppendChild(dt_cria)

        vlr_total_lote = NotasFiscais.CreateElement("vlr_total_lote")
        vlr_total_lote.InnerText = dr(2)
        lotes.AppendChild(vlr_total_lote)
      Loop

      conexao.FechaBanco(conexao1)
      dr.Close()
    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      Me.msg_retorno = "ERRO AO LISTAR OS LOTES DAS NOTAS FISCAIS DE SERVIÇO: " & ex.Message() & "----------" & ex.StackTrace()
    End Try

    Return NotasFiscais
  End Function

  Public Sub SalvaCaminhoArquivoXml(ByVal id_nf As Integer, ByVal id_empresa As Integer, ByVal tipo_doc As String, _
                                    ByVal caminho_xml As String)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

	'Somente atualiza o caminho do xml as notas que estão PROVISÓRIAS...
	str_builder.Append("if ISNULL((SELECT sts_nf FROM NFE_ide WHERE (id_nf = " & id_nf & ") AND (id_empresa = " & id_empresa & ")), 0) = 0 ")
    str_builder.Append("UPDATE NFE_ide ")
    str_builder.Append("SET arq_xml = '" & caminho_xml & "' ")
    str_builder.Append("WHERE (id_nf = " & id_nf & ") AND (tipo_doc = " & tipo_doc & ") AND (id_empresa = " & id_empresa & ") ")

    If id_empresa = 328 Or id_empresa = 830 Then 'Usado para gravar o caminho do arquivo da CDA... que está dando erro...
      str_builder.Append("INSERT Aux_Loga_Procedure(id_empresa, qual_proc, id_nf, id_usuario, dt_hora) ")
      str_builder.Append("VALUES(" & id_empresa & ", '" & caminho_xml & "', " & id_nf & ", 1, GETDATE()) ")
    End If

    Try
      conexao.ExecutaComando(str_builder.ToString())

    Catch ex As Exception
      Me.msg_retorno = "ERRO AO SALVAR O CAMINHO DO ARQUIVO XML: " & ex.Message() & "--------" & ex.StackTrace()
    End Try
  End Sub

  Public Function PegaCaminhoArquivoXml(ByVal id_nf As Integer, ByVal tipo_doc As Integer, ByVal id_empresa As Integer) As String
    Dim caminho_xml As String = ""
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    str_builder.Append("SELECT arq_xml FROM NFE_ide WHERE (id_nf = " & id_nf & ") AND (tipo_doc = " & tipo_doc & ") ")
    str_builder.Append("AND (id_empresa = " & id_empresa & ") ")

    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      Do While dr.Read()
        caminho_xml = dr(0)
      Loop

      conexao.FechaBanco(conexao1)
      dr.Close()


    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      Me.msg_retorno = "ERRO AO PEGAR O CAMINHO DO XML: " & ex.Message() & "----------" & ex.StackTrace()
    End Try

    Return caminho_xml
  End Function

  Public Function CriaXmlUmaNFe(ByVal id_empresa As Integer, ByVal nNF As Integer) As XmlDocument
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader = Nothing
    Dim NotasFiscais As New XmlDocument
    Dim notas As XmlElement
    Dim nota, id_nf, dEmi, cnpj, xNome, sts_nf As XmlElement

    str_builder.Append("SELECT id_nf, dbo.fFormata_Data(dEmi, 2), dbo.fCNPJ_Le(B.cnpj), B.xNome, A.sts_nf ")
    str_builder.Append("FROM NFE_ide AS A ")
    str_builder.Append("INNER JOIN NFE_dest AS B ON (B.id_dest = A.id_dest) ")
    str_builder.Append("WHERE (tipo_doc = 0) AND (id_empresa = " & id_empresa & ") AND (nNF = " & nNF & ") ")


    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      notas = NotasFiscais.CreateElement("NotasFiscais")
      NotasFiscais.AppendChild(notas)

      Do While dr.Read()
        nota = NotasFiscais.CreateElement("NFe")
        notas.AppendChild(nota)

        id_nf = NotasFiscais.CreateElement("id_nf")
        id_nf.InnerText = dr(0)
        nota.AppendChild(id_nf)

        dEmi = NotasFiscais.CreateElement("dEmi")
        dEmi.InnerText = dr(1)
        nota.AppendChild(dEmi)

        cnpj = NotasFiscais.CreateElement("cnpj")
        cnpj.InnerText = dr(2)
        nota.AppendChild(cnpj)

        xNome = NotasFiscais.CreateElement("xNome")
        xNome.InnerText = Trim(dr(3))
        nota.AppendChild(xNome)

        sts_nf = NotasFiscais.CreateElement("sts_nf")
        sts_nf.InnerText = dr(4)
        nota.AppendChild(sts_nf)
      Loop
      conexao.FechaBanco(conexao1)
      dr.Close()
    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      Me.msg_retorno = "ERRO AO GERAR O XML: " & ex.Message() & "-------------" & ex.StackTrace()
    End Try


    Return NotasFiscais

  End Function
  
  Public Function CriaXmlNFeParaInutilizacao(ByVal id_empresa As Integer, ByVal ano As Integer, ByVal mes As Integer) As XmlDocument
    Dim NotasFiscais As New XmlDocument
    Dim notas, nota, serie, ini_interv, fim_interv, ano_inter, tpAmb, cnpj_emp, cUF As XmlElement
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader
    Dim aaaamm As String = ano & Right("00" & mes, 2)


    str_builder.Append("EXEC sp9_NFe_Inutiliza_Pega '" & id_empresa & "','" & aaaamm & "'")

    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      notas = NotasFiscais.CreateElement("NotasFiscais")
      NotasFiscais.AppendChild(notas)

      Do While dr.Read()
        nota = NotasFiscais.CreateElement("NFe")
        notas.AppendChild(nota)

        serie = NotasFiscais.CreateElement("serie")
        serie.InnerText = dr(0)
        nota.AppendChild(serie)

        ini_interv = NotasFiscais.CreateElement("ini_interv")
        ini_interv.InnerText = dr(1)
        nota.AppendChild(ini_interv)

        fim_interv = NotasFiscais.CreateElement("fim_interv")
        fim_interv.InnerText = dr(2)
        nota.AppendChild(fim_interv)

        ano_inter = NotasFiscais.CreateElement("ano_inter")
        ano_inter.InnerText = dr(3)
        nota.AppendChild(ano_inter)

        tpAmb = NotasFiscais.CreateElement("tpAmb")
        tpAmb.InnerText = dr(4)
        nota.AppendChild(tpAmb)

        cnpj_emp = NotasFiscais.CreateElement("cnpj_emp")
        cnpj_emp.InnerText = dr(5)
        nota.AppendChild(cnpj_emp)

        cUF = NotasFiscais.CreateElement("cUF")
        cUF.InnerText = dr(6)
        nota.AppendChild(cUF)
      Loop
      conexao.FechaBanco(conexao1)
      dr.Close()
    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      Me.result = -1
      Me.msg_retorno = "ERRO AO LISTAR AS NOTAS FISCAIS PARA INUTILIZAÇÃO: " & ex.Message() & "--------" & ex.StackTrace()
    End Try

    Return NotasFiscais

  End Function

End Class

