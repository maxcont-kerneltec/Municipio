Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class clsNfeItem

  Private _result, _nItem, _CFOP, _indTot, _ICMS_orig, _ICMS_CST, _ICMS_CSOSN, _ICMS_modBC, _ICMS_modBCST, _ICMS_modDesICMS As Integer
  Private _IPI_CST, _PIS_CST, _COFINS_CST, _ISSQN_cMunFG, _ISSQN_cListServ, _id_item, _cod_cf, _cCFOP, _cICMS_CST, _cIPI_CST As Integer
  Private _cPIS_CST, _cCOFINS_CST, _id_mov, _livro_PIS_Cofins, _fPrecisao, _id_pedido, _id_lanc_padrao, _id_item_vinculado As Integer
  Private _id_nat_op, _id_dest, _id_cliente, _nNF, _ICMS_motDesICMS, _CRT As Integer
  Private _msg_result, _cProd, _cEAN, _xProd, _NCM, _EXTIPI, _uCom, _cEANTrib, _uTrib, _xPed, _nItemPed, _ICMS_UFST, _tipo_item As String
  Private _IPI_cIEnq, _IPI_CNPJProd, _IPI_cSelo, _IPI_qSelo, _IPI_cEnq, _xDescr_Serv, _ISSQN_cSitTrib, _infAdProd, _un_estoque As String
  Private _nFCI, _fControla_Entrada, _fonte_tribut, _item_retorno_indust, _cest, _IPI_clEnq, _msg_erro As String
  Private _qCom, _vUnCom, _vProd, _qTrib, _vUnTrib, _vFrete, _vSeg, _vDesc, _vOutro, _ICMS_pRedBC, _ICMS_vBC As Decimal
  Private _ICMS_pICMS, _ICMS_vICMS, _ICMS_pMVAST, _ICMS_pRedBCST, _ICMS_vBCST, _ICMS_pICMSST, _ICMS_vICMSST, _ICMS_pBCOp As Decimal
  Private _ICMS_vBCSTRet, _ICMS_vICMSSTRet, _ICMS_pCredSN, _ICMS_vCredICMSSN, _ICMS_vBCSTDest, _ICMS_vICMSSTDest, _IPI_vBC As Decimal
  Private _IPI_pIPI, _IPI_qUnid, _IPI_vUnid, _IPI_vIPI, _II_vBC, _II_vDespAdu, _II_vII, _II_vIOF, _PIS_vBC, _PIS_pPIS As Decimal
  Private _PIS_vPIS, _PIS_qBCProd, _PIS_vAliqProd, _PISST_vBC, _PISST_pPIS, _PISST_qBCProd, _PISST_vAliqProd, _PISST_vPIS As Decimal
  Private _COFINS_vBC, _COFINS_pCOFINS, _COFINS_vCOFINS, _COFINS_qBCProd, _COFINS_vAliqProd, _COFINSST_vBC, _COFINSST_pCOFINS As Decimal
  Private _COFINSST_qBCProd, _COFINSST_vAliqProd, _COFINSST_vCOFINS, _ISSQN_vBC, _ISSQN_vAliq, _ISSQN_vISSQN, _vCred_Prod As Decimal
  Private _vCred_BC_ICMS, _vCred_pICMS, _vCred_vICMS, _vCred_BC_IPI, _vCred_pIPI, _vCred_vIPI, _vCred_BC_PIS, _vCred_pPIS As Decimal
  Private _vCred_vPIS, _vCred_BC_Cofins, _vCred_pCofins, _vCred_vCofins, _qtde_estoque, _vContabil, _ICMS_vBC2, _ICMS_pICMS2 As Decimal
  Private _ICMS_vICMS2, _ICMS_vOutra, _ICMS_vIsenta, _IPI_vBC2, _IPI_pIPI2, _IPI_vIPI2, _IPI_vOutra, _IPI_vIsenta, _vTotTrib As Decimal
  Private _aliq_vTrib, _ICMS_vICMSDeson, _IRRF_pIRRF, _CSLL_pCSLL, _INSS_pINSS, _soma_tributos, _IPI_pDevol, _IPI_vIPIDevol As Decimal
  Private _ICMS_vICMSOp, _ICMS_pDif, _ICMS_vICMSDif As Decimal
  Private _ICMS_vICMSSubstituto As Decimal

  Private conexao1 As New SqlConnection

  Public Sub New()

  End Sub

  Property result() As Integer
    Get
      Return _result
    End Get
    Set(value As Integer)
      _result = value
    End Set
  End Property

  Property msg_result() As String
    Get
      Return _msg_result
    End Get
    Set(value As String)
      _msg_result = value
    End Set
  End Property

  Property nItem() As Integer
    Get
      Return _nItem
    End Get
    Set(value As Integer)
      _nItem = value
    End Set
  End Property

  Property cProd() As String
    Get
      Return _cProd
    End Get
    Set(value As String)
      _cProd = value
    End Set
  End Property

  Property cEAN() As String
    Get
      Return _cEAN
    End Get
    Set(value As String)
      _cEAN = value
    End Set
  End Property

  Property xProd() As String
    Get
      Return _xProd
    End Get
    Set(value As String)
      _xProd = value
    End Set
  End Property

  Property NCM() As String
    Get
      Return _NCM
    End Get
    Set(value As String)
      _NCM = value
    End Set
  End Property

  Property EXTIPI() As String
    Get
      Return _EXTIPI
    End Get
    Set(value As String)
      _EXTIPI = value
    End Set
  End Property

  Property CFOP() As Integer
    Get
      Return _CFOP
    End Get
    Set(value As Integer)
      _CFOP = value
    End Set
  End Property

  Property uCom() As String
    Get
      Return _uCom
    End Get
    Set(value As String)
      _uCom = value
    End Set
  End Property

  Property vUnCom() As Decimal
    Get
      Return _vUnCom
    End Get
    Set(value As Decimal)
      _vUnCom = value
    End Set
  End Property

  Property vProd() As Decimal
    Get
      Return _vProd
    End Get
    Set(value As Decimal)
      _vProd = value
    End Set
  End Property

  Property cEANTrib() As String
    Get
      Return _cEANTrib
    End Get
    Set(value As String)
      _cEANTrib = value
    End Set
  End Property

  Property uTrib() As String
    Get
      Return _uTrib
    End Get
    Set(value As String)
      _uTrib = value
    End Set
  End Property

  Property qTrib() As Decimal
    Get
      Return _qTrib
    End Get
    Set(value As Decimal)
      _qTrib = value
    End Set
  End Property

  Property vUnTrib() As Decimal
    Get
      Return _vUnTrib
    End Get
    Set(value As Decimal)
      _vUnTrib = value
    End Set
  End Property

  Property vFrete() As Decimal
    Get
      Return _vFrete
    End Get
    Set(value As Decimal)
      _vFrete = value
    End Set
  End Property

  Property vSeg() As Decimal
    Get
      Return _vSeg
    End Get
    Set(value As Decimal)
      _vSeg = value
    End Set
  End Property

  Property vDesc() As Decimal
    Get
      Return _vDesc
    End Get
    Set(value As Decimal)
      _vDesc = value
    End Set
  End Property

  Property vOutro() As Decimal
    Get
      Return _vOutro
    End Get
    Set(value As Decimal)
      _vOutro = value
    End Set
  End Property

  Property indTot() As Integer
    Get
      Return _indTot
    End Get
    Set(value As Integer)
      _indTot = value
    End Set
  End Property

  Property xPed() As String
    Get
      Return _xPed
    End Get
    Set(value As String)
      _xPed = value
    End Set
  End Property

  Property nItemPed() As String
    Get
      Return _nItemPed
    End Get
    Set(value As String)
      _nItemPed = value
    End Set
  End Property

  Property cest() As String
    Get
      Return _cest
    End Get
    Set(value As String)
      _cest = value
    End Set
  End Property

  Property qCom() As Decimal
    Get
      Return _qCom
    End Get
    Set(value As Decimal)
      _qCom = value
    End Set
  End Property

  Property nFCI() As String
    Get
      Return _nFCI
    End Get
    Set(value As String)
      _nFCI = value
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

  Property soma_tributos() As Decimal
    Get
      Return _soma_tributos
    End Get
    Set(value As Decimal)
      _soma_tributos = value
    End Set
  End Property

  Property IPI_pDevol() As Decimal
    Get
      Return _IPI_pDevol
    End Get
    Set(value As Decimal)
      _IPI_pDevol = value
    End Set
  End Property

  Property IPI_vIPIDevol() As Decimal
    Get
      Return _IPI_vIPIDevol
    End Get
    Set(value As Decimal)
      _IPI_vIPIDevol = value
    End Set
  End Property

  Property ICMS_vICMSOp() As Decimal
    Get
      Return _ICMS_vICMSOp
    End Get
    Set(value As Decimal)
      _ICMS_vICMSOp = value
    End Set
  End Property

  Property ICMS_pDif() As Decimal
    Get
      Return _ICMS_pDif
    End Get
    Set(value As Decimal)
      _ICMS_pDif = value
    End Set
  End Property

  Property ICMS_vICMSDif() As Decimal
    Get
      Return _ICMS_vICMSDif
    End Get
    Set(value As Decimal)
      _ICMS_vICMSDif = value
    End Set
  End Property

  Property ICMS_vICMSSubstituto() As Decimal
    Get
      Return _ICMS_vICMSSubstituto
    End Get
    Set(value As Decimal)
      _ICMS_vICMSSubstituto = value
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

  Property id_cliente() As Integer
    Get
      Return _id_cliente
    End Get
    Set(value As Integer)
      _id_cliente = value
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

  Property id_lanc_padrao() As Integer
    Get
      Return _id_lanc_padrao
    End Get
    Set(value As Integer)
      _id_lanc_padrao = value
    End Set
  End Property

  Property infAdProd() As String
    Get
      Return _infAdProd
    End Get
    Set(value As String)
      _infAdProd = value
    End Set
  End Property

  Property ICMS_orig() As Integer
    Get
      Return _ICMS_orig
    End Get
    Set(value As Integer)
      _ICMS_orig = value
    End Set
  End Property

  Property ICMS_CST() As Integer
    Get
      Return _ICMS_CST
    End Get
    Set(value As Integer)
      _ICMS_CST = value
    End Set
  End Property

  Property ICMS_CSOSN() As Integer
    Get
      Return _ICMS_CSOSN
    End Get
    Set(value As Integer)
      _ICMS_CSOSN = value
    End Set
  End Property

  Property ICMS_modBC() As Integer
    Get
      Return _ICMS_modBC
    End Get
    Set(value As Integer)
      _ICMS_modBC = value
    End Set
  End Property

  Property ICMS_pRedBC() As Decimal
    Get
      Return _ICMS_pRedBC
    End Get
    Set(value As Decimal)
      _ICMS_pRedBC = value
    End Set
  End Property

  Property ICMS_vBC() As Decimal
    Get
      Return _ICMS_vBC
    End Get
    Set(value As Decimal)
      _ICMS_vBC = value
    End Set
  End Property

  Property ICMS_pICMS() As Decimal
    Get
      Return _ICMS_pICMS
    End Get
    Set(value As Decimal)
      _ICMS_pICMS = value
    End Set
  End Property

  Property ICMS_vICMS() As Decimal
    Get
      Return _ICMS_vICMS
    End Get
    Set(value As Decimal)
      _ICMS_vICMS = value
    End Set
  End Property

  Property ICMS_modBCST() As Integer
    Get
      Return _ICMS_modBCST
    End Get
    Set(value As Integer)
      _ICMS_modBCST = value
    End Set
  End Property

  Property ICMS_pMVAST() As Decimal
    Get
      Return _ICMS_pMVAST
    End Get
    Set(value As Decimal)
      _ICMS_pMVAST = value
    End Set
  End Property

  Property ICMS_pRedBCST() As Decimal
    Get
      Return _ICMS_pRedBCST
    End Get
    Set(value As Decimal)
      _ICMS_pRedBCST = value
    End Set
  End Property

  Property ICMS_vBCST() As Decimal
    Get
      Return _ICMS_vBCST
    End Get
    Set(value As Decimal)
      _ICMS_vBCST = value
    End Set
  End Property

  Property ICMS_pICMSST() As Decimal
    Get
      Return _ICMS_pICMSST
    End Get
    Set(value As Decimal)
      _ICMS_pICMSST = value
    End Set
  End Property

  Property ICMS_vICMSST() As Decimal
    Get
      Return _ICMS_vICMSST
    End Get
    Set(value As Decimal)
      _ICMS_vICMSST = value
    End Set
  End Property

  Property ICMS_pBCOp() As Decimal
    Get
      Return _ICMS_pBCOp
    End Get
    Set(value As Decimal)
      _ICMS_pBCOp = value
    End Set
  End Property

  Property ICMS_vBCSTRet() As Decimal
    Get
      Return _ICMS_vBCSTRet
    End Get
    Set(value As Decimal)
      _ICMS_vBCSTRet = value
    End Set
  End Property

  Property ICMS_vICMSSTRet() As Decimal
    Get
      Return _ICMS_vICMSSTRet
    End Get
    Set(value As Decimal)
      _ICMS_vICMSSTRet = value
    End Set
  End Property

  Property ICMS_motDesICMS() As Integer
    Get
      Return _ICMS_motDesICMS
    End Get
    Set(value As Integer)
      _ICMS_motDesICMS = value
    End Set
  End Property

  Property CRT() As Integer
    Get
      Return _CRT
    End Get
    Set(value As Integer)
      _CRT = value
    End Set
  End Property

  Property ICMS_pCredSN() As Decimal
    Get
      Return _ICMS_pCredSN
    End Get
    Set(value As Decimal)
      _ICMS_pCredSN = value
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

  Property ICMS_vBCSTDest() As Decimal
    Get
      Return _ICMS_vBCSTDest
    End Get
    Set(value As Decimal)
      _ICMS_vBCSTDest = value
    End Set
  End Property

  Property ICMS_vICMSSTDest() As Decimal
    Get
      Return _ICMS_vICMSSTDest
    End Get
    Set(value As Decimal)
      _ICMS_vICMSSTDest = value
    End Set
  End Property

  Property IPI_clEnq() As String
    Get
      Return _IPI_clEnq
    End Get
    Set(value As String)
      _IPI_clEnq = value
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

  Property IPI_CNPJProd() As String
    Get
      Return _IPI_CNPJProd
    End Get
    Set(value As String)
      _IPI_CNPJProd = value
    End Set
  End Property

  Property IPI_cSelo() As String
    Get
      Return _IPI_cSelo
    End Get
    Set(value As String)
      _IPI_cSelo = value
    End Set
  End Property

  Property IPI_qSelo() As String
    Get
      Return _IPI_qSelo
    End Get
    Set(value As String)
      _IPI_qSelo = value
    End Set
  End Property

  Property IPI_cEnq() As String
    Get
      Return _IPI_cEnq
    End Get
    Set(value As String)
      _IPI_cEnq = value
    End Set
  End Property

  Property IPI_CST() As Integer
    Get
      Return _IPI_CST
    End Get
    Set(value As Integer)
      _IPI_CST = value
    End Set
  End Property

  Property IPI_vBC() As Decimal
    Get
      Return _IPI_vBC
    End Get
    Set(value As Decimal)
      _IPI_vBC = value
    End Set
  End Property

  Property IPI_pIPI() As Decimal
    Get
      Return _IPI_pIPI
    End Get
    Set(value As Decimal)
      _IPI_pIPI = value
    End Set
  End Property

  Property IPI_qUnid() As Decimal
    Get
      Return _IPI_qUnid
    End Get
    Set(value As Decimal)
      _IPI_qUnid = value
    End Set
  End Property

  Property IPI_vUnid() As Decimal
    Get
      Return _IPI_vUnid
    End Get
    Set(value As Decimal)
      _IPI_vUnid = value
    End Set
  End Property

  Property IPI_vIPI() As Decimal
    Get
      Return _IPI_vIPI
    End Get
    Set(value As Decimal)
      _IPI_vIPI = value
    End Set
  End Property

  Property II_vBC() As Decimal
    Get
      Return _II_vBC
    End Get
    Set(value As Decimal)
      _II_vBC = value
    End Set
  End Property

  Property II_vDespAdu() As Decimal
    Get
      Return _II_vDespAdu
    End Get
    Set(value As Decimal)
      _II_vDespAdu = value
    End Set
  End Property

  Property II_vII() As Decimal
    Get
      Return _II_vII
    End Get
    Set(value As Decimal)
      _II_vII = value
    End Set
  End Property

  Property II_vIOF() As Decimal
    Get
      Return _II_vIOF
    End Get
    Set(value As Decimal)
      _II_vIOF = value
    End Set
  End Property

  Property PIS_CST() As Integer
    Get
      Return _PIS_CST
    End Get
    Set(value As Integer)
      _PIS_CST = value
    End Set
  End Property

  Property PIS_vBC() As Decimal
    Get
      Return _PIS_vBC
    End Get
    Set(value As Decimal)
      _PIS_vBC = value
    End Set
  End Property

  Property PIS_pPIS() As Decimal
    Get
      Return _PIS_pPIS
    End Get
    Set(value As Decimal)
      _PIS_pPIS = value
    End Set
  End Property

  Property PIS_vPIS() As Decimal
    Get
      Return _PIS_vPIS
    End Get
    Set(value As Decimal)
      _PIS_vPIS = value
    End Set
  End Property

  Property PIS_qBCProd() As Decimal
    Get
      Return _PIS_qBCProd
    End Get
    Set(value As Decimal)
      _PIS_qBCProd = value
    End Set
  End Property

  Property PIS_vAliqProd() As Decimal
    Get
      Return _PIS_vAliqProd
    End Get
    Set(value As Decimal)
      _PIS_vAliqProd = value
    End Set
  End Property

  Property PISST_vBC() As Decimal
    Get
      Return _PISST_vBC
    End Get
    Set(value As Decimal)
      _PISST_vBC = value
    End Set
  End Property

  Property PISST_pPIS() As Decimal
    Get
      Return _PISST_pPIS
    End Get
    Set(value As Decimal)
      _PISST_pPIS = value
    End Set
  End Property

  Property PISST_qBCProd() As Decimal
    Get
      Return _PISST_qBCProd
    End Get
    Set(value As Decimal)
      _PISST_qBCProd = value
    End Set
  End Property

  Property PISST_vAliqProd() As Decimal
    Get
      Return _PISST_vAliqProd
    End Get
    Set(value As Decimal)
      _PISST_vAliqProd = value
    End Set
  End Property

  Property PISST_vPIS() As Decimal
    Get
      Return _PISST_vPIS
    End Get
    Set(value As Decimal)
      _PISST_vPIS = value
    End Set
  End Property

  Property COFINS_CST() As Integer
    Get
      Return _COFINS_CST
    End Get
    Set(value As Integer)
      _COFINS_CST = value
    End Set
  End Property

  Property COFINS_vBC() As Decimal
    Get
      Return _COFINS_vBC
    End Get
    Set(value As Decimal)
      _COFINS_vBC = value
    End Set
  End Property

  Property COFINS_pCOFINS() As Decimal
    Get
      Return _COFINS_pCOFINS
    End Get
    Set(value As Decimal)
      _COFINS_pCOFINS = value
    End Set
  End Property

  Property COFINS_vCOFINS() As Decimal
    Get
      Return _COFINS_vCOFINS
    End Get
    Set(value As Decimal)
      _COFINS_vCOFINS = value
    End Set
  End Property

  Property COFINS_qBCProd() As Decimal
    Get
      Return _COFINS_qBCProd
    End Get
    Set(value As Decimal)
      _COFINS_qBCProd = value
    End Set
  End Property

  Property COFINS_vAliqProd() As Decimal
    Get
      Return _COFINS_vAliqProd
    End Get
    Set(value As Decimal)
      _COFINS_vAliqProd = value
    End Set
  End Property

  Property COFINSST_vBC() As Decimal
    Get
      Return _COFINSST_vBC
    End Get
    Set(value As Decimal)
      _COFINSST_vBC = value
    End Set
  End Property

  Property COFINSST_pCOFINS() As Decimal
    Get
      Return _COFINSST_pCOFINS
    End Get
    Set(value As Decimal)
      _COFINSST_pCOFINS = value
    End Set
  End Property

  Property COFINSST_qBCProd() As Decimal
    Get
      Return _COFINSST_qBCProd
    End Get
    Set(value As Decimal)
      _COFINSST_qBCProd = value
    End Set
  End Property

  Property COFINSST_vAliqProd() As Decimal
    Get
      Return _COFINSST_vAliqProd
    End Get
    Set(value As Decimal)
      _COFINSST_vAliqProd = value
    End Set
  End Property

  Property COFINSST_vCOFINS() As Decimal
    Get
      Return _COFINSST_vCOFINS
    End Get
    Set(value As Decimal)
      _COFINSST_vCOFINS = value
    End Set
  End Property

  Property ISSQN_vBC() As Decimal
    Get
      Return _ISSQN_vBC
    End Get
    Set(value As Decimal)
      _ISSQN_vBC = value
    End Set
  End Property

  Property ISSQN_vAliq() As Decimal
    Get
      Return _ISSQN_vAliq
    End Get
    Set(value As Decimal)
      _ISSQN_vAliq = value
    End Set
  End Property

  Property ISSQN_vISSQN() As Decimal
    Get
      Return _ISSQN_vISSQN
    End Get
    Set(value As Decimal)
      _ISSQN_vISSQN = value
    End Set
  End Property

  Property ISSQN_cMunFG() As Integer
    Get
      Return _ISSQN_cMunFG
    End Get
    Set(value As Integer)
      _ISSQN_cMunFG = value
    End Set
  End Property

  Property ISSQN_cListServ() As Integer
    Get
      Return _ISSQN_cListServ
    End Get
    Set(value As Integer)
      _ISSQN_cListServ = value
    End Set
  End Property

  Property ISSQN_cSitTrib() As String
    Get
      Return _ISSQN_cSitTrib
    End Get
    Set(value As String)
      _ISSQN_cSitTrib = value
    End Set
  End Property

  Property ICMS_vICMSDeson() As Decimal
    Get
      Return _ICMS_vICMSDeson
    End Get
    Set(value As Decimal)
      _ICMS_vICMSDeson = value
    End Set
  End Property

  Property ICMS_UFST() As String
    Get
      Return _ICMS_UFST
    End Get
    Set(value As String)
      _ICMS_UFST = value
    End Set
  End Property


  Public Function ListaItemAbaProdutoNfe(ByVal id_nf As Integer) As DataTable
    Dim table As New DataTable
    Dim row As DataRow
    Dim dr As SqlDataReader
    Dim str_builder As New StringBuilder
    Dim conexao As New clsConexao
    Dim i As Integer

    table.Columns.Add(New DataColumn("nItem"))
    table.Columns.Add(New DataColumn("cProd"))
    table.Columns.Add(New DataColumn("xProd"))
    table.Columns.Add(New DataColumn("NCM"))
    table.Columns.Add(New DataColumn("CFOP"))
    table.Columns.Add(New DataColumn("uCom"))
    table.Columns.Add(New DataColumn("qCom"))
    table.Columns.Add(New DataColumn("vUnCom"))
    table.Columns.Add(New DataColumn("vProd"))
    table.Columns.Add(New DataColumn("ICMS_vICMS"))
    table.Columns.Add(New DataColumn("IPI_vIPI"))
    table.Columns.Add(New DataColumn("PIS_vPIS"))
    table.Columns.Add(New DataColumn("COFINS_vCOFINS"))
    table.Columns.Add(New DataColumn("cEAN"))
    table.Columns.Add(New DataColumn("EXTIPI"))
    table.Columns.Add(New DataColumn("cEANTrib"))
    table.Columns.Add(New DataColumn("uTrib"))
    table.Columns.Add(New DataColumn("qTrib"))
    table.Columns.Add(New DataColumn("vUnTrib"))
    table.Columns.Add(New DataColumn("vFrete"))
    table.Columns.Add(New DataColumn("vSeg"))
    table.Columns.Add(New DataColumn("vDesc"))
    table.Columns.Add(New DataColumn("vOutro"))
    table.Columns.Add(New DataColumn("indTot"))
    table.Columns.Add(New DataColumn("xPed"))
    table.Columns.Add(New DataColumn("nItemPed"))
    table.Columns.Add(New DataColumn("nFCI"))
    table.Columns.Add(New DataColumn("cest"))
    table.Columns.Add(New DataColumn("vTotTrib"))
    table.Columns.Add(New DataColumn("indEscala"))
    table.Columns.Add(New DataColumn("CNPJFab"))
    table.Columns.Add(New DataColumn("cBenef"))

    str_builder.Append("SELECT nItem, cProd, xProd, NCM, CFOP, uCom, qCom, ")
    str_builder.Append("ROUND((CASE WHEN fPrecisao > 4 THEN dbo.fNumero_Precisao_Retorna(vUnCom, fPrecisao) ELSE vUnCom END), fPrecisao) AS vUnCom,  ")
    str_builder.Append("vProd, ICMS_vICMS, IPI_vIPI, PIS_vPIS, COFINS_vCOFINS, ISNULL(cEAN, ''), ISNULL(EXTIPI, ''), ISNULL(cEANTrib, ''), uTrib, qTrib, ")
    str_builder.Append("ROUND((CASE WHEN fPrecisao > 4 THEN dbo.fNumero_Precisao_Retorna(vUnTrib, fPrecisao) ELSE vUnTrib END), fPrecisao) AS vUnTrib, ")
    str_builder.Append("ROUND(vFrete, 2), ROUND(vSeg, 2), ROUND(vDesc, 2), vOutro, indTot, ISNULL((xPed), ''), ISNULL((nItemPed), ''), ISNULL((nFCI), ''), ")
    str_builder.Append("ISNULL(cest, '') AS cest, vTotTrib, ISNULL(indEscala, ''), ISNULL(CNPJFab, ''), ISNULL(cBenef, '') ")
    str_builder.Append("FROM NFE_Item ")
    str_builder.Append("WHERE id_nf = " & id_nf & "")

    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      Do While dr.Read()
        row = table.NewRow()

        For i = 0 To 31
          row(i) = dr(i)
        Next

        table.Rows.Add(row)
      Loop
      conexao.FechaBanco(conexao1)
      dr.Close()
    Catch ex As System.Exception
      conexao.FechaBanco(conexao1)
      _msg_erro = "ERRO AO LISTAR OS ITENS DA NOTA FISCAL:  " & ex.Message() & "------------" & ex.StackTrace()
    End Try

    Return table
  End Function

  ''' <summary>
  ''' Retorna result(0=OK; 1=MSG; -1=ERRO) | msg_result...
  ''' </summary>
  ''' <param name="id_nf"></param>
  ''' <param name="nItem"></param>
  Public Sub ExcluiUmItemNfe(ByVal id_nf As Integer, ByVal nItem As Integer)
    Dim conexao As New clsConexao
    Dim dr As SqlDataReader
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_NFE_item_APAGA '" & id_nf & "','" & nItem & "','S'")

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        Me.result = dr(0)
        Me.msg_result = dr(1)
      Loop

      dr.Close()
    Catch ex As Exception
      _msg_erro = "ERRO AO EXCLUÍR O ITEM DA NOTA FISCAL: " & ex.Message() & "---------" & ex.StackTrace()
    End Try
  End Sub

  Public Sub ListaItemAbaDadosNfeItem(ByVal id_nf As Integer, ByVal nItem As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    str_builder.Append("declare @soma_tributos money ")
    str_builder.Append("declare @id_dest int ")
    str_builder.Append("declare @id_cliente int ")
    str_builder.Append("declare @nNF int ")

    str_builder.Append("set @id_dest = (SELECT id_dest FROM NFE_ide WHERE (id_nf = " & id_nf & ")) ")
    str_builder.Append("set @id_cliente = (SELECT id_cliente FROM NFE_dest WHERE (id_dest = @id_dest)) ")
    str_builder.Append("set @nNF = (SELECT nNF FROM NFE_ide WHERE (id_nf = " & id_nf & ")) ")

    str_builder.Append("Set @soma_tributos = (Select ICMS_vICMS + IPI_vIPI + PIS_vPIS + COFINS_vCOFINS + ISSQN_vISSQN + ICMS_vICMSST ")
    str_builder.Append("FROM NFE_item WHERE (id_nf = " & id_nf & ") And (nItem = " & nItem & ")) ")

    str_builder.Append("SELECT nItem, cProd, ISNULL((cEAN), ''), xProd, NCM, ISNULL((EXTIPI), ''), CFOP, uCom, qCom, ")
    str_builder.Append("ROUND((CASE WHEN fPrecisao > 4 THEN dbo.fNumero_Precisao_Retorna(vUnCom, fPrecisao) ELSE vUnCom END), fPrecisao) as vUnCom, ")
    str_builder.Append("vProd, ISNULL((cEANTrib), ''), uTrib, qTrib, ")
    str_builder.Append("ROUND((CASE WHEN fPrecisao > 4 THEN dbo.fNumero_Precisao_Retorna(vUnTrib, fPrecisao) ELSE vUnTrib END), fPrecisao) as vUnTrib, ")
    str_builder.Append("vFrete, vSeg, vDesc, vOutro, indTot, ISNULL((xPed), ''), ISNULL((nItemPed), ''), ")
    str_builder.Append("vTotTrib, ISNULL((nFCI), ''), ISNULL((cest), ''), id_lanc_padrao, ISNULL((infAdProd), ''), ")
    str_builder.Append("@soma_tributos AS soma_tributos, @id_dest AS id_dest, @id_cliente AS id_cliente, @nNF as nNF ")
    str_builder.Append("FROM NFE_item ")
    str_builder.Append("WHERE (id_nf = " & id_nf & ") AND (nItem = " & nItem & ") ")

    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      Do While dr.Read()
        Me.nItem = dr(0)
        Me.cProd = dr(1)
        Me.cEAN = dr(2)
        Me.xProd = dr(3)
        Me.NCM = dr(4)
        Me.EXTIPI = dr(5)
        Me.CFOP = dr(6)
        Me.uCom = dr(7)
        Me.qCom = dr(8)
        Me.vUnCom = dr(9)
        Me.vProd = dr(10)
        Me.cEANTrib = dr(11)
        Me.uTrib = dr(12)
        Me.qTrib = dr(13)
        Me.vUnTrib = dr(14)
        Me.vFrete = dr(15)
        Me.vSeg = dr(16)
        Me.vDesc = dr(17)
        Me.vOutro = dr(18)
        Me.indTot = dr(19)
        Me.xPed = dr(20)
        Me.nItemPed = dr(21)
        Me.vTotTrib = dr(22)
        Me.nFCI = dr(23)
        Me.cest = dr(24)
        Me.id_lanc_padrao = dr(25)
        Me.infAdProd = dr(26)
        Me.soma_tributos = dr(27)
        Me.id_dest = dr(28)
        Me.id_cliente = dr(29)
        Me.nNF = dr(30)
      Loop
      conexao.FechaBanco(conexao1)
      dr.Close()
    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      _msg_erro = "ERRO AO PEGAR AS INFORMAÇÕES DO PRODUTO ABA DADOS: " & ex.Message() & "-------" & ex.StackTrace()
    End Try

  End Sub

  Public Sub SalvaAbaDadosItemNfe(ByVal id_nf As Integer, ByVal nItem As Integer, ByVal cProd As String, ByVal cEAN As String, _
                                  ByVal xProd As String, ByVal NCM As String, ByVal EXTIPI As String, ByVal CFOP As Integer, _
                                  ByVal uCom As String, ByVal qCom As Decimal, ByVal vUnCom As Decimal, ByVal vProd As Decimal, _
                                  ByVal cEANTrib As String, ByVal uTrib As String, ByVal qTrib As Decimal, ByVal vUnTrib As Decimal, _
                                  ByVal vFrete As Decimal, ByVal vSeg As Decimal, ByVal vDesc As Decimal, ByVal vOutro As Decimal, _
                                  ByVal indTot As Integer, ByVal xPed As String, ByVal nItemPed As String, ByVal infAdProd As String, _
                                  ByVal atualiza_tributos As String, ByVal id_nat_op_novo As Integer, ByVal vTotTrib As Decimal, _
                                  ByVal nFCI As String, ByVal id_lanc_padrao As Integer, ByVal cProdANP As Integer, ByVal pMixGN As Decimal, _
                                  ByVal CODIF As Integer, ByVal qTemp As Integer, ByVal UFCons As String, ByVal qBcProd As Decimal, _
                                  ByVal vAliqProd As Decimal, ByVal vCIDE As Decimal, ByVal cest As String, ByVal descANP As String, _
                                  ByVal pGLP As Decimal, ByVal pGNn As Decimal, ByVal pGNi As Decimal, ByVal vPart As Decimal)

    Dim fAtu_Tot As String = "S"
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    Dim qCom_str, vUnCom_str, qTrib_str, vUnTrib_str, vFrete_str, vSeg_str, vDesc_str, vOutro_str, vProd_str As String
    Dim qTemp_str, pMixGN_str, qBcProd_str, vAliqProd_str, vCIDE_str, pGLP_str, pGNn_str, pGNi_str, vPart_str As String

    qCom_str = Replace(Replace(qCom, ".", ""), ",", ".")
    vUnCom_str = Replace(Replace(vUnCom, ".", ""), ",", ".")
    qTrib_str = Replace(Replace(qTrib, ".", ""), ",", ".")
    vUnTrib_str = Replace(Replace(vUnTrib, ".", ""), ",", ".")
    vFrete_str = Replace(Replace(vFrete, ".", ""), ",", ".")
    vSeg_str = Replace(Replace(vSeg, ".", ""), ",", ".")
    vDesc_str = Replace(Replace(vDesc, ".", ""), ",", ".")
    vOutro_str = Replace(Replace(vOutro, ".", ""), ",", ".")
    vProd_str = Replace(Replace(vProd, ".", ""), ",", ".")
    qTemp_str = Replace(Replace(qTemp, ".", ""), ",", ".")
    pMixGN_str = Replace(Replace(pMixGN, ".", ""), ",", ".")
    qBcProd_str = Replace(Replace(qBcProd, ".", ""), ",", ".")
    vAliqProd_str = Replace(Replace(vAliqProd, ".", ""), ",", ".")
    vCIDE_str = Replace(Replace(vCIDE, ".", ""), ",", ".")
    pGLP_str = Replace(Replace(pGLP, ".", ""), ",", ".")
    pGNn_str = Replace(Replace(pGNn, ".", ""), ",", ".")
    pGNi_str = Replace(Replace(pGNi, ".", ""), ",", ".")
    vPart_str = Replace(Replace(vPart, ".", ""), ",", ".")


    infAdProd = Replace(infAdProd, vbCrLf, " ")

    str_builder.Append("EXEC sp9_NFE_item_DADOS '" & id_nf & "','" & nItem & "','" & cProd & "','" & cEAN & "','" & xProd & "'")
    str_builder.Append(",'" & NCM & "','" & EXTIPI & "','" & CFOP & "','" & uCom & "','" & qCom_str & "','" & vUnCom_str & "','" & vProd_str & "'")
    str_builder.Append(",'" & cEANTrib & "','" & uTrib & "','" & qTrib_str & "','" & vUnTrib_str & "','" & vFrete_str & "','" & vSeg_str & "'")
    str_builder.Append(",'" & vDesc_str & "','" & vOutro_str & "','" & indTot & "','" & xPed & "','" & nItemPed & "','" & infAdProd & "'")
    str_builder.Append(",'" & fAtu_Tot & "','" & atualiza_tributos & "','" & id_nat_op_novo & "','" & vTotTrib & "','" & nFCI & "'")
    str_builder.Append(",'" & id_lanc_padrao & "','" & cProdANP & "','" & pMixGN_str & "','" & CODIF & "','" & qTemp_str & "','" & UFCons & "'")
    str_builder.Append(",'" & qBcProd_str & "','" & vAliqProd_str & "','" & vCIDE_str & "','" & cest & "','" & descANP & "'")
    str_builder.Append(",'" & pGLP_str & "','" & pGNn_str & "','" & pGNi_str & "','" & vPart_str & "'")

    Try
      conexao.ExecutaComando(str_builder.ToString())
    Catch ex As Exception
      _msg_erro = "ERRO AO SALVAR AS INFORMAÇÕES DO ITEM NA NOTA FISCAL: " & ex.Message() & "--------" & ex.StackTrace()
    End Try
  End Sub

  Public Sub ListaAbaTributosNfeItemICMS(ByVal id_nf As Integer, ByVal nItem As Integer)
    Dim conexao As New clsConexao
    Dim dr As SqlDataReader
    Dim str_builder As New StringBuilder

    str_builder.Append("declare @CRT int ")
    str_builder.Append("set @CRT = ISNULL((SELECT CRT FROM NFE_emit AS EMI INNER JOIN NFE_ide AS NFE ON NFE.id_emit = EMI.id_emit WHERE  (id_nf = " & id_nf & ")), 3) ")

    str_builder.Append("SELECT ICMS_orig, ICMS_CST, ICMS_CSOSN, ICMS_modBC, ICMS_pRedBC, ICMS_vBC, ICMS_pICMS, ")
    str_builder.Append("ICMS_vICMS, ICMS_modBCST, ICMS_pMVAST, ICMS_pRedBCST, ICMS_vBCST, ")
    str_builder.Append("ICMS_pICMSST, ICMS_vICMSST, ICMS_pBCOp, ICMS_vBCSTRet, ICMS_vICMSSTRet, ")
    str_builder.Append("ICMS_motDesICMS, ICMS_pCredSN, ICMS_vCredICMSSN, ICMS_vBCSTDest, ICMS_vICMSSTDest, ")
    str_builder.Append("ISNULL(IPI_clEnq, ''), IPI_CNPJProd, ISNULL(IPI_cSelo, ''), IPI_qSelo, IPI_cEnq, IPI_CST, ")
    str_builder.Append("IPI_vBC, IPI_pIPI, IPI_qUnid, IPI_vUnid, IPI_vIPI, II_vBC, II_vDespAdu, II_vII, II_vIOF, ")
    str_builder.Append("PIS_CST, PIS_vBC, PIS_pPIS, PIS_vPIS, PIS_qBCProd, PIS_vAliqProd, ")
    str_builder.Append("PISST_vBC, PISST_pPIS, PISST_qBCProd, PISST_vAliqProd, PISST_vPIS, ")
    str_builder.Append("COFINS_CST, COFINS_vBC, COFINS_pCOFINS, COFINS_vCOFINS, COFINS_qBCProd, COFINS_vAliqProd, ")
    str_builder.Append("COFINSST_vBC, COFINSST_pCOFINS, COFINSST_qBCProd, COFINSST_vAliqProd, COFINSST_vCOFINS, ")
    str_builder.Append("ISSQN_vBC, ISSQN_vAliq, ISSQN_vISSQN, ISSQN_cMunFG, ISSQN_cListServ, ISSQN_cSitTrib, @CRT, ")
    str_builder.Append("ICMS_vICMSDeson, ISNULL(ICMS_UFST, '--'), vTotTrib, ISNULL(ICMS_vICMSOp, 0), ISNULL(ICMS_pDif, 0), ISNULL(ICMS_vICMSDif, 0), ")
    str_builder.Append("ISNULL(vICMSSubstituto, 0) ")
    str_builder.Append("FROM NFE_item ")
    str_builder.Append("WHERE (id_nf = " & id_nf & ") AND (nItem = " & nItem & ") ")

    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      Do While dr.Read()
        Me.ICMS_orig = dr(0)
        Me.ICMS_CST = dr(1)
        Me.ICMS_CSOSN = dr(2)
        Me.ICMS_modBC = dr(3)
        Me.ICMS_pRedBC = dr(4)
        Me.ICMS_vBC = dr(5)
        Me.ICMS_pICMS = dr(6)
        Me.ICMS_vICMS = dr(7)
        Me.ICMS_modBCST = dr(8)
        Me.ICMS_pMVAST = dr(9)
        Me.ICMS_pRedBCST = dr(10)
        Me.ICMS_vBCST = dr(11)
        Me.ICMS_pICMSST = dr(12)
        Me.ICMS_vICMSST = dr(13)
        Me.ICMS_pBCOp = dr(14)
        Me.ICMS_vBCSTRet = dr(15)
        Me.ICMS_vICMSSTRet = dr(16)
        Me.ICMS_motDesICMS = dr(17)
        Me.ICMS_pCredSN = dr(18)
        Me.ICMS_vCredICMSSN = dr(19)
        Me.ICMS_vBCSTDest = dr(20)
        Me.ICMS_vICMSSTDest = dr(21)
        Me.IPI_clEnq = dr(22)
        Me.IPI_CNPJProd = dr(23)
        Me.IPI_cSelo = dr(24)
        Me.IPI_qSelo = dr(25)
        Me.IPI_cEnq = dr(26)
        Me.IPI_CST = dr(27)
        Me.IPI_vBC = dr(28)
        Me.IPI_pIPI = dr(29)
        Me.IPI_qUnid = dr(30)
        Me.IPI_vUnid = dr(31)
        Me.IPI_vIPI = dr(32)
        Me.II_vBC = dr(33)
        Me.II_vDespAdu = dr(34)
        Me.II_vII = dr(35)
        Me.II_vIOF = dr(36)
        Me.PIS_CST = dr(37)
        Me.PIS_vBC = dr(38)
        Me.PIS_pPIS = dr(39)
        Me.PIS_vPIS = dr(40)
        Me.PIS_qBCProd = dr(41)
        Me.PIS_vAliqProd = dr(42)
        Me.PISST_vBC = dr(43)
        Me.PISST_pPIS = dr(44)
        Me.PISST_qBCProd = dr(45)
        Me.PISST_vAliqProd = dr(46)
        Me.PISST_vPIS = dr(47)
        Me.COFINS_CST = dr(48)
        Me.COFINS_vBC = dr(49)
        Me.COFINS_pCOFINS = dr(50)
        Me.COFINS_vCOFINS = dr(51)
        Me.COFINS_qBCProd = dr(52)
        Me.COFINS_vAliqProd = dr(53)
        Me.COFINSST_vBC = dr(54)
        Me.COFINSST_pCOFINS = dr(55)
        Me.COFINSST_qBCProd = dr(56)
        Me.COFINSST_vAliqProd = dr(57)
        Me.COFINSST_vCOFINS = dr(58)
        Me.ISSQN_vBC = dr(59)
        Me.ISSQN_vAliq = dr(60)
        Me.ISSQN_vISSQN = dr(61)
        Me.ISSQN_cMunFG = dr(62)
        Me.ISSQN_cListServ = dr(63)
        Me.ISSQN_cSitTrib = dr(64)
        Me.CRT = dr(65)
        Me.ICMS_vICMSDeson = dr(66)
        Me.ICMS_UFST = dr(67)
        Me.vTotTrib = dr(68)
        _ICMS_vICMSOp = dr(69)
        _ICMS_pDif = dr(70)
        _ICMS_vICMSDif = dr(71)
        Me.ICMS_vICMSSubstituto = dr(72)
      Loop
      conexao.FechaBanco(conexao1)
      dr.Close()
    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      _msg_erro = "ERRO AO PEGAR AS INFORMAÇÕES DA ABA TRIBUTOS: " & ex.Message() & "-------" & ex.StackTrace()
    End Try
  End Sub

  Public Sub SalvaICMSNfeItem(ByVal id_nf As Integer, ByVal nItem As Integer, ByVal ICMS_orig As Integer, ByVal ICMS_CST As Integer, _
                              ByVal ICMS_CSOSN As Integer, ByVal ICMS_modBC As Integer, ByVal ICMS_pRedBC As Decimal, ByVal ICMS_vBC As Decimal, _
                              ByVal ICMS_pICMS As Decimal, ByVal ICMS_vICMS As Decimal, ByVal ICMS_modBCST As Integer, ByVal ICMS_pMVAST As Decimal, _
                              ByVal ICMS_pRedBCST As Decimal, ByVal ICMS_vBCST As Decimal, ByVal ICMS_pICMSST As Decimal, ByVal ICMS_vICMSST As Decimal, _
                              ByVal ICMS_UFST As String, ByVal ICMS_pBCOp As Decimal, ByVal ICMS_vBCSTRet As Decimal, ByVal ICMS_vICMSSTRet As Decimal, _
                              ByVal ICMS_motDesICMS As Integer, ByVal ICMS_pCredSN As Decimal, ByVal ICMS_vCredICMSSN As Decimal, ByVal ICMS_vBCSTDest As Decimal, _
                              ByVal ICMS_vICMSSTDest As Decimal, ByVal ICMS_vICMSDeson As Decimal)

    Dim str_builder As New StringBuilder
    Dim conexao As New clsConexao
    Dim ICMS_pRedBC_str, ICMS_vBC_str, ICMS_pICMS_str, ICMS_vICMS_str, ICMS_pMVAST_str, ICMS_pRedBCST_str As String
    Dim ICMS_vBCST_str, ICMS_pICMSST_str, ICMS_vICMSST_str, ICMS_pCredSN_str, ICMS_vCredICMSSN_str As String
    Dim ICMS_vBCSTRet_str, ICMS_vICMSSTRet_str, ICMS_vBCSTDest_str, ICMS_vICMSSTDest_str, ICMS_vICMSDeson_str As String

    ICMS_pRedBC_str = Replace(Replace(ICMS_pRedBC, ".", ""), ",", ".")
    ICMS_vBC_str = Replace(Replace(ICMS_vBC, ".", ""), ",", ".")
    ICMS_pICMS_str = Replace(Replace(ICMS_pICMS, ".", ""), ",", ".")
    ICMS_vICMS_str = Replace(Replace(ICMS_vICMS, ".", ""), ",", ".")
    ICMS_pMVAST_str = Replace(Replace(ICMS_pMVAST, ".", ""), ",", ".")
    ICMS_pRedBCST_str = Replace(Replace(ICMS_pRedBCST, ".", ""), ",", ".")
    ICMS_vBCST_str = Replace(Replace(ICMS_vBCST, ".", ""), ",", ".")
    ICMS_pICMSST_str = Replace(Replace(ICMS_pICMSST, ".", ""), ",", ".")
    ICMS_vICMSST_str = Replace(Replace(ICMS_vICMSST, ".", ""), ",", ".")
    ICMS_pCredSN_str = Replace(Replace(ICMS_pCredSN, ".", ""), ",", ".")
    ICMS_vCredICMSSN_str = Replace(Replace(ICMS_vCredICMSSN, ".", ""), ",", ".")
    ICMS_vBCSTRet_str = Replace(Replace(ICMS_vBCSTRet, ".", ""), ",", ".")
    ICMS_vICMSSTRet_str = Replace(Replace(ICMS_vICMSSTRet, ".", ""), ",", ".")
    ICMS_vBCSTDest_str = Replace(Replace(ICMS_vBCSTDest, ".", ""), ",", ".")
    ICMS_vICMSSTDest_str = Replace(Replace(ICMS_vICMSSTDest, ".", ""), ",", ".")
    ICMS_vICMSDeson_str = Replace(Replace(ICMS_vICMSDeson, ".", ""), ",", ".")

    str_builder.Append("EXEC sp9_NFE_item_ICMS_Atualiza '" & id_nf & "','" & nItem & "','" & ICMS_orig & "','" & ICMS_CST & "'")
    str_builder.Append(",'" & ICMS_CSOSN & "','" & ICMS_modBC & "','" & ICMS_pRedBC_str & "','" & ICMS_vBC_str & "'")
    str_builder.Append(",'" & ICMS_pICMS_str & "','" & ICMS_vICMS_str & "','" & ICMS_modBCST & "','" & ICMS_pMVAST_str & "'")
    str_builder.Append(",'" & ICMS_pRedBCST_str & "','" & ICMS_vBCST_str & "','" & ICMS_pICMSST_str & "','" & ICMS_vICMSST_str & "'")
    str_builder.Append(",'" & ICMS_UFST & "','" & ICMS_pBCOp & "','" & ICMS_vBCSTRet_str & "','" & ICMS_vICMSSTRet_str & "'")
    str_builder.Append(",'" & ICMS_motDesICMS & "','" & ICMS_pCredSN_str & "','" & ICMS_vCredICMSSN_str & "','" & ICMS_vBCSTDest_str & "'")
    str_builder.Append(",'" & ICMS_vICMSSTDest_str & "','S','" & ICMS_vICMSDeson_str & "'")

    Try
      conexao.ExecutaComando(str_builder.ToString())

    Catch ex As Exception
      _msg_erro = "ERRO AO SALVAR O ICMS: " & ex.Message() & "------------" & ex.StackTrace()
    End Try
  End Sub

  Public Sub ListaAbaTributosNfeItemIPI(ByVal id_nf As Integer, ByVal nItem As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    str_builder.Append("SELECT ISNULL(IPI_clEnq, ''), IPI_CNPJProd, ISNULL(IPI_cSelo, ''), IPI_qSelo, IPI_cEnq, IPI_CST, IPI_vBC, IPI_pIPI, ")
    str_builder.Append("IPI_qUnid, IPI_vUnid, IPI_vIPI ")
    str_builder.Append("FROM NFE_Item ")
    str_builder.Append("WHERE (id_nf = " & id_nf & ") AND (nItem = " & nItem & ") ")

    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      Do While dr.Read()
        Me.IPI_clEnq = dr(0)
        Me.IPI_CNPJProd = dr(1)
        Me.IPI_cSelo = dr(2)
        Me.IPI_qSelo = dr(3)
        Me.IPI_cEnq = dr(4)
        Me.IPI_CST = dr(5)
        Me.IPI_vBC = dr(6)
        Me.IPI_pIPI = dr(7)
        Me.IPI_qUnid = dr(8)
        Me.IPI_vUnid = dr(9)
        Me.IPI_vIPI = dr(10)
      Loop
      conexao.FechaBanco(conexao1)
      dr.Close()
    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      _msg_erro = "ERRO AO CARREGAR A ABA IPI: " & ex.Message() & "----------" & ex.StackTrace()
    End Try

  End Sub

  Public Sub SalvaIPINfeItem(ByVal id_nf As Integer, ByVal nItem As Integer, ByVal IPI_clEnq As String, ByVal IPI_CNPJProd As String, _
                             ByVal IPI_cSelo As String, ByVal IPI_qSelo As String, ByVal IPI_cEnq As String, ByVal IPI_CST As Integer, _
                             ByVal IPI_vBC As Decimal, ByVal IPI_pIPI As Decimal, ByVal IPI_qUnid As Decimal, ByVal IPI_vUnid As Decimal, _
                             ByVal IPI_vIPI As Decimal)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim IPI_vBC_str, IPI_pIPI_str, IPI_qUnid_str, IPI_vUnid_str, IPI_vIPI_str As String

    IPI_vBC_str = Replace(Replace(IPI_vBC, ".", ""), ",", ".")
    IPI_pIPI_str = Replace(Replace(IPI_pIPI, ".", ""), ",", ".")
    IPI_qUnid_str = Replace(Replace(IPI_qUnid, ".", ""), ",", ".")
    IPI_vUnid_str = Replace(Replace(IPI_vUnid, ".", ""), ",", ".")
    IPI_vIPI_str = Replace(Replace(IPI_vIPI, ".", ""), ",", ".")

    str_builder.Append("EXEC sp9_NFE_Item_IPI_Atualiza '" & id_nf & "','" & nItem & "','" & IPI_clEnq & "','" & IPI_CNPJProd & "'")
    str_builder.Append(",'" & IPI_cSelo & "','" & IPI_qSelo & "','" & IPI_cEnq & "','" & IPI_CST & "','" & IPI_vBC_str & "'")
    str_builder.Append(",'" & IPI_pIPI_str & "','" & IPI_qUnid_str & "','" & IPI_vUnid_str & "','" & IPI_vIPI_str & "','S'")

    Try
      conexao.ExecutaComando(str_builder.ToString())

    Catch ex As Exception
      _msg_erro = "ERRO AO SALVAR AS INFORMAÇÕES DO IPI: " & ex.Message() & "---------" & ex.StackTrace()
    End Try
  End Sub

  Public Sub ListaAbaTributosNfeItemPIS(ByVal id_nf As Integer, ByVal nItem As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    str_builder.Append("SELECT PIS_CST, PIS_vBC, PIS_pPIS,PIS_vPIS, PIS_qBCProd, PIS_vAliqProd, PIS_vBC, PIS_pPIS, ")
    str_builder.Append("PISST_qBCProd, PISST_vAliqProd, PISST_vPIS ")
    str_builder.Append("FROM NFE_item ")
    str_builder.Append("WHERE (id_nf = " & id_nf & ") AND (nItem = " & nItem & ") ")

    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      Do While dr.Read()
        Me.PIS_CST = dr(0)
        Me.PIS_vBC = dr(1)
        Me.PIS_pPIS = dr(2)
        Me.PIS_vPIS = dr(3)
        Me.PIS_qBCProd = dr(4)
        Me.PIS_vAliqProd = dr(5)
        Me.PIS_vBC = dr(6)
        Me.PIS_pPIS = dr(7)
        Me.PISST_qBCProd = dr(8)
        Me.PISST_vAliqProd = dr(9)
        Me.PISST_vPIS = dr(10)
      Loop
      conexao.FechaBanco(conexao1)
      dr.Close()
    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      _msg_erro = "ERRO AO PEGAR AS INFORMAÇÕES DA ABA PIS: " & ex.Message() & "--------------" & ex.StackTrace()
    End Try

  End Sub

  Public Sub SalvaPISNfeItem(ByVal id_nf As Integer, ByVal nItem As Integer, ByVal PIS_CST As Integer, ByVal PIS_vBC As Decimal, _
                             ByVal PIS_pPIS As Decimal, ByVal PIS_vPIS As Decimal, ByVal PIS_qBCProd As Decimal, ByVal PIS_vAliqProd As Decimal, _
                             ByVal PISST_vBC As Decimal, ByVal PISST_pPIS As Decimal, ByVal PISST_qBCProd As Decimal, ByVal PISST_vAliqProd As Decimal, _
                             ByVal PISST_vPIS As Decimal)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim PIS_vBC_str, PIS_pPIS_str, PIS_vPIS_str, PIS_qBCProd_str, PIS_vAliqProd_str, PISST_vBC_str As String
    Dim PISST_pPIS_str, PISST_qBCProd_str, PISST_vAliqProd_str, PISST_vPIS_str As String

    PIS_vBC_str = Replace(Replace(PIS_vBC, ".", ""), ",", ".")
    PIS_pPIS_str = Replace(Replace(PIS_pPIS, ".", ""), ",", ".")
    PIS_vPIS_str = Replace(Replace(PIS_vPIS, ".", ""), ",", ".")
    PIS_qBCProd_str = Replace(Replace(PIS_qBCProd, ".", ""), ",", ".")
    PIS_vAliqProd_str = Replace(Replace(PIS_vAliqProd, ".", ""), ",", ".")
    PISST_vBC_str = Replace(Replace(PISST_vBC, ".", ""), ",", ".")
    PISST_pPIS_str = Replace(Replace(PISST_pPIS, ".", ""), ",", ".")
    PISST_qBCProd_str = Replace(Replace(PISST_qBCProd, ".", ""), ",", ".")
    PISST_vAliqProd_str = Replace(Replace(PISST_vAliqProd, ".", ""), ",", ".")
    PISST_vPIS_str = Replace(Replace(PISST_vPIS, ".", ""), ",", ".")

    str_builder.Append("EXEC sp9_NFE_Item_PIS_Atualiza '" & id_nf & "','" & nItem & "','" & PIS_CST & "','" & PIS_vBC_str & "'")
    str_builder.Append(",'" & PIS_pPIS_str & "','" & PIS_vPIS_str & "','" & PIS_qBCProd_str & "','" & PIS_vAliqProd_str & "'")
    str_builder.Append(",'" & PISST_vBC_str & "','" & PISST_pPIS_str & "','" & PISST_qBCProd_str & "','" & PISST_vAliqProd_str & "'")
    str_builder.Append(",'" & PISST_vPIS_str & "','S'")

    Try
      conexao.ExecutaComando(str_builder.ToString())

    Catch ex As Exception
      _msg_erro = "ERRO AO SALVAR AS INFORMAÇÕES DO PIS: " & ex.Message() & "----------" & ex.StackTrace()
    End Try
  End Sub

  Public Sub ListaAbaTributosNfeItemCOFINS(ByVal id_nf As Integer, ByVal nItem As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    str_builder.Append("SELECT COFINS_CST, COFINS_vBC, COFINS_pCOFINS, COFINS_vCOFINS, COFINS_qBCProd, ")
    str_builder.Append("COFINSST_vAliqProd, COFINSST_vBC, COFINSST_pCOFINS, COFINSST_qBCProd, COFINS_vAliqProd, ")
    str_builder.Append("COFINSST_vCOFINS ")
    str_builder.Append("FROM NFE_item ")
    str_builder.Append("WHERE (id_nf = " & id_nf & ") AND (nItem = " & nItem & ") ")

    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      Do While dr.Read()
        Me.COFINS_CST = dr(0)
        Me.COFINS_vBC = dr(1)
        Me.COFINS_pCOFINS = dr(2)
        Me.COFINS_vCOFINS = dr(3)
        Me.COFINS_qBCProd = dr(4)
        Me.COFINSST_vAliqProd = dr(5)
        Me.COFINSST_vBC = dr(6)
        Me.COFINSST_pCOFINS = dr(7)
        Me.COFINSST_qBCProd = dr(8)
        Me.COFINS_vAliqProd = dr(9)
        Me.COFINSST_vCOFINS = dr(10)
      Loop
      conexao.FechaBanco(conexao1)
      dr.Close()
    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      _msg_erro = "ERRO AO LISTAR AS INFORMAÇÕES DA ABA COFINS: " & ex.Message() & "---------" & ex.StackTrace()
    End Try
  End Sub

  Public Sub SalvaCOFINSNfeItem(ByVal id_nf As Integer, ByVal nItem As Integer, ByVal COFINS_CST As Integer, ByVal COFINS_vBC As Decimal, _
                                ByVal COFINS_pCOFINS As Decimal, ByVal COFINS_vCOFINS As Decimal, ByVal COFINS_qBCProd As Decimal, _
                                ByVal COFINS_vAliqProd As Decimal, ByVal COFINSST_vBC As Decimal, ByVal COFINSST_pCOFINS As Decimal, _
                                ByVal COFINSST_qBCProd As Decimal, ByVal COFINSST_vAliqProd As Decimal, ByVal COFINSST_vCOFINS As Decimal)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim COFINS_vBC_str, COFINS_pCOFINS_str, COFINS_vCOFINS_str, COFINS_qBCProd_str, COFINS_vAliqProd_str As String
    Dim COFINSST_vBC_str, COFINSST_pCOFINS_str, COFINSST_qBCProd_str, COFINSST_vAliqProd_str, COFINSST_vCOFINS_str As String

    COFINS_vBC_str = Replace(Replace(COFINS_vBC, ".", ""), ",", ".")
    COFINS_pCOFINS_str = Replace(Replace(COFINS_pCOFINS, ".", ""), ",", ".")
    COFINS_vCOFINS_str = Replace(Replace(COFINS_vCOFINS, ".", ""), ",", ".")
    COFINS_qBCProd_str = Replace(Replace(COFINS_qBCProd, ".", ""), ",", ".")
    COFINS_vAliqProd_str = Replace(Replace(COFINS_vAliqProd, ".", ""), ",", ".")
    COFINSST_vBC_str = Replace(Replace(COFINSST_vBC, ".", ""), ",", ".")
    COFINSST_pCOFINS_str = Replace(Replace(COFINSST_pCOFINS, ".", ""), ",", ".")
    COFINSST_qBCProd_str = Replace(Replace(COFINSST_qBCProd, ".", ""), ",", ".")
    COFINSST_vAliqProd_str = Replace(Replace(COFINSST_vAliqProd, ".", ""), ",", ".")
    COFINSST_vCOFINS_str = Replace(Replace(COFINSST_vCOFINS, ".", ""), ",", ".")

    str_builder.Append("EXEC sp9_NFE_item_COFINS_Atualiza '" & id_nf & "','" & nItem & "','" & COFINS_CST & "'")
    str_builder.Append(",'" & COFINS_vBC_str & "','" & COFINS_pCOFINS_str & "','" & COFINS_vCOFINS_str & "'")
    str_builder.Append(",'" & COFINS_qBCProd_str & "','" & COFINS_vAliqProd_str & "','" & COFINSST_vBC_str & "'")
    str_builder.Append(",'" & COFINSST_pCOFINS_str & "','" & COFINSST_qBCProd_str & "','" & COFINSST_vAliqProd_str & "'")
    str_builder.Append(",'" & COFINSST_vCOFINS_str & "','S'")

    Try
      conexao.ExecutaComando(str_builder.ToString())

    Catch ex As Exception
      _msg_erro = "ERRO AO SALVAR O COFINS: " & ex.Message() & "-----------" & ex.StackTrace()
    End Try
  End Sub

  Public Sub ListaAbaTributosNfeItemISSQN(ByVal id_nf As Integer, ByVal nItem As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    str_builder.Append("SELECT ISSQN_vBC, ISSQN_vAliq, ISSQN_vISSQN, ISSQN_cMunFG, ISSQN_cListServ, ISSQN_cSitTrib ")
    str_builder.Append("FROM NFE_item ")
    str_builder.Append("WHERE (id_nf = " & id_nf & ") AND (nItem = " & nItem & ") ")

    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      Do While dr.Read()
        Me.ISSQN_vBC = dr(0)
        Me.ISSQN_vAliq = dr(1)
        Me.ISSQN_vISSQN = dr(2)
        Me.ISSQN_cMunFG = dr(3)
        Me.ISSQN_cListServ = dr(4)
        Me.ISSQN_cSitTrib = dr(5)
      Loop

      conexao.FechaBanco(conexao1)
      dr.Close()
    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      _msg_erro = "ERRO AO LISTAR AS INFORMAÇÕES DA ABA COFINS: " & ex.Message() & "-------------" & ex.StackTrace()
    End Try

  End Sub

  Public Sub SalvaISSQNNfeItem(ByVal id_nf As Integer, ByVal nItem As Integer, ByVal ISSQN_vBC As Decimal, ByVal ISSQN_vAliq As Decimal, _
                               ByVal ISSQN_vISSQN As Decimal, ByVal ISSQN_cMunFG As Integer, ByVal ISSQN_cListServ As Integer, _
                               ByVal ISSQN_cSitTrib As String)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim ISSQN_vBC_str, ISSQN_vAliq_str, ISSQN_vISSQN_str As String

    ISSQN_vBC_str = Replace(Replace(ISSQN_vBC, ".", ""), ",", ".")
    ISSQN_vAliq_str = Replace(Replace(ISSQN_vAliq, ".", ""), ",", ".")
    ISSQN_vISSQN_str = Replace(Replace(ISSQN_vISSQN, ".", ""), ",", ".")

    str_builder.Append("EXEC sp9_NFE_item_ISSQN_Atualiza '" & id_nf & "','" & nItem & "','" & ISSQN_vBC_str & "'")
    str_builder.Append(",'" & ISSQN_vAliq_str & "','" & ISSQN_vISSQN_str & "','" & ISSQN_cMunFG & "','" & ISSQN_cListServ & "'")
    str_builder.Append(",'" & ISSQN_cSitTrib & "','S'")

    Try
      conexao.ExecutaComando(str_builder.ToString())

    Catch ex As Exception
      _msg_erro = "ERRO AO SALVAR AS INFORMAÇÕES DO ISSQN: " & ex.Message() & "-----------" & ex.StackTrace()
    End Try
  End Sub

  Public Sub ListaAbaTributosNfeItemII(ByVal id_nf As Integer, ByVal nItem As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    str_builder.Append("SELECT II_vBC, II_vDespAdu, II_vII, II_vIOF ")
    str_builder.Append("FROM NFE_item ")
    str_builder.Append("WHERE (id_nf = " & id_nf & ") AND (nItem = " & nItem & ") ")

    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      Do While dr.Read()
        Me.II_vBC = dr(0)
        Me.II_vDespAdu = dr(1)
        Me.II_vII = dr(2)
        Me.II_vIOF = dr(3)
      Loop

      conexao.FechaBanco(conexao1)
      dr.Close()
    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      _msg_erro = "ERRO AO LISTAR AS INFORMAÇOES DA ABA II: " & ex.Message() & "---------" & ex.StackTrace()
    End Try
  End Sub

  ''' <summary>
  ''' Retorna TRUE... caso o processo seja efetuado com sucesso...
  ''' </summary>
  ''' <param name="id_nf"></param>
  ''' <param name="nItem"></param>
  ''' <param name="II_vBC"></param>
  ''' <param name="II_vDespAdu"></param>
  ''' <param name="II_vII"></param>
  ''' <param name="II_vIOF"></param>
  ''' <returns></returns>
  Public Function SalvaIINfeItem(ByVal id_nf As Integer, ByVal nItem As Integer, ByVal II_vBC As Decimal, ByVal II_vDespAdu As Decimal, _
                            ByVal II_vII As Decimal, ByVal II_vIOF As Decimal) As Boolean
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim II_vBC_str, II_vDespAdu_str, II_vII_str, II_vIOF_str As String
    Dim retorno As Boolean = True

    II_vBC_str = Replace(Replace(II_vBC, ".", ""), ",", ".")
    II_vDespAdu_str = Replace(Replace(II_vDespAdu, ".", ""), ",", ".")
    II_vII_str = Replace(Replace(II_vII, ".", ""), ",", ".")
    II_vIOF_str = Replace(Replace(II_vIOF, ".", ""), ",", ".")

    str_builder.Append("EXEC sp9_NFE_Item_II_Atualiza '" & id_nf & "','" & nItem & "','" & II_vBC_str & "'")
    str_builder.Append(",'" & II_vDespAdu_str & "','" & II_vII_str & "','" & II_vIOF_str & "','S'")

    Try
      conexao.ExecutaComando(str_builder.ToString())

    Catch ex As Exception
      retorno = False
      _msg_erro = "ERRO AO SALVAR A ABA II: " & ex.Message() & "-----------" & ex.StackTrace()
    End Try

    Return retorno
  End Function

  Public Sub ListaAbaTributosNFeItemIPIDevolvido(ByVal id_nf As Integer, ByVal nItem As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    str_builder.Append("SELECT IPI_pDevol, IPI_vIPIDevol FROM NFE_item ")
    str_builder.Append("WHERE (id_nf = " & id_nf & ") AND (nItem = " & nItem & ") ")

    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      Do While dr.Read()
        Me.IPI_pDevol = dr(0)
        Me.IPI_vIPIDevol = dr(1)
      Loop
      conexao.FechaBanco(conexao1)
      dr.Close()
    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      _msg_erro = "ERRO AO LISTAR AS INFORMAÇÕES DA ABA IPI DEVOLVIDO: " & ex.Message() & "------------" & ex.StackTrace()
    End Try
  End Sub

  ''' <summary>
  ''' Retorna TRUE... caso o processo seja efetuado com sucesso...
  ''' </summary>
  ''' <param name="id_nf"></param>
  ''' <param name="nItem"></param>
  ''' <param name="IPI_pDevol"></param>
  ''' <param name="IPI_vIPIDevol"></param>
  ''' <returns></returns>
  Public Function SalvaIPIDevolvidoNFeItem(ByVal id_nf As Integer, ByVal nItem As Integer, ByVal IPI_pDevol As Decimal, ByVal IPI_vIPIDevol As Decimal) As Boolean
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim IPI_pDevol_str, IPI_vIPIDevol_str As String
    Dim retorno As Boolean = True

    IPI_pDevol_str = Replace(Replace(IPI_pDevol, ".", ""), ",", ".")
    IPI_vIPIDevol_str = Replace(Replace(IPI_vIPIDevol, ".", ""), ",", ".")

    str_builder.Append("UPDATE NFE_item ")
    str_builder.Append("SET IPI_pDevol = " & IPI_pDevol_str & ", IPI_vIPIDevol = " & IPI_vIPIDevol_str & " ")
    str_builder.Append("WHERE (id_nf = " & id_nf & ") AND (nItem = " & nItem & ") ")

    Try
      conexao.ExecutaComando(str_builder.ToString())

    Catch ex As Exception
      retorno = False
      _msg_erro = "ERRO AO SALVAR AS INFORMAÇÕES DA ABA IPI DEVOLVIDO: " & ex.Message() & "-------" & ex.StackTrace()
    End Try

    Return retorno

  End Function

  ''' <summary>
  ''' Retorna o nItem do item incluído na NFe...
  ''' Se retornar MENOR ou IGUAL a 0... ERRO...
  ''' </summary>
  ''' <param name="id_empresa"></param>
  ''' <param name="id_nf"></param>
  ''' <param name="id_item"></param>
  ''' <param name="qtde"></param>
  ''' <param name="vlr_unitario"></param>
  ''' <returns></returns>
  Public Function InsereUmItemNFe(ByVal id_empresa As Integer, ByVal id_nf As Integer, ByVal id_item As Integer, _
                                  ByVal qtde As Decimal, ByVal vlr_unitario As Decimal) As Integer
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    str_builder.Append("EXEC sp9_Insere_Item_NFe '" & id_nf & "','" & id_item & "','" & id_empresa & "'")
    str_builder.Append(",'" & qtde & "','" & vlr_unitario & "','S'")

    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)


      Do While dr.Read()
        Me.nItem = dr(0)
      Loop

      conexao.FechaBanco(conexao1)
      dr.Close()
    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      Me.nItem = -1
      _msg_erro = "ERRO AO INSERIR O ITEM AO NOTA FISCAL: " & ex.Message() & "----------" & ex.StackTrace()
    End Try

    Return Me.nItem

  End Function

  Public Function CalculaTotalItemNfe(ByVal qtde As Decimal, ByVal vlr_unit As Decimal) As Decimal
    Dim resultado As Decimal

    resultado = qtde * vlr_unit

    Return resultado
  End Function
End Class
