Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections
Imports System.Web.UI.WebControls


Public Class clsNfeNatOp

  Private _id_nat_op, _tipo_doc, _tpNF, _serie, _cfop, _cfop_st, _cfop_sem_st, _icms_CST, _icms_ST_CST As Integer
  Private _icms_sem_ST_CST, _CSOSN, _ipi_CST, _pis_CST, _confins_CST, _cfop2, _cfop2_ST, _cfop2_sem_ST As Integer
  Private _icms_CST2, _icms_ST_CST2, _icms_sem_ST_CST2, _CSOSN2, _ipi_CST2, _pis_CST2, _confins_CST2, _cfop3 As Integer
  Private _icms_CST3, _CSOSN3, _ipi_CST3, _pis_CST3, _confins_CST3, _fCalc_vTrib, _ipi_CST_prod, _fRec_Tributos As Integer
  Private _ipi_CST_prod2, _ipi_CST_prod3, _id_nat_op_retorno, _id_nat_op_remessa_venda, _result, _cofins_cst As Integer
  Private _cofins_cst2, _uso, _cofins_cst3, _CRT, _fPed_Item_Remessa As Integer
  Private _descr, _origem_nf, _f_CF, _f_Total, _fLanc_Estoque, _natOp, _obs, _f_ST, _natOp2, _obs2, _f_ST2 As String
  Private _natOp3, _obs3, _gera_nf_retorno, _nf_retorno_separada, _partilha_ICMS_UF, _msg_result, _descr_icms As String
  Private _descr_ipi, _descr_pis, _descr_cofins, _f_vendas, _f_contabilidade, _fCria_CFOP As String
  Private _p_red_BC_ICMS As Decimal


  Public Sub New()

  End Sub

  Property id_nat_op() As Integer
    Get
      Return _id_nat_op
    End Get
    Set(value As Integer)
      _id_nat_op = value
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

  Property tpNF() As Integer
    Get
      Return _tpNF
    End Get
    Set(value As Integer)
      _tpNF = value
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

  Property cfop() As Integer
    Get
      Return _cfop
    End Get
    Set(value As Integer)
      _cfop = value
    End Set
  End Property

  Property cfop_st() As Integer
    Get
      Return _cfop_st
    End Get
    Set(value As Integer)
      _cfop_st = value
    End Set
  End Property

  Property cfop_sem_st() As Integer
    Get
      Return _cfop_sem_st
    End Get
    Set(value As Integer)
      _cfop_sem_st = value
    End Set
  End Property

  Property icms_CST() As Integer
    Get
      Return _icms_CST
    End Get
    Set(value As Integer)
      _icms_CST = value
    End Set
  End Property

  Property icms_ST_CST() As Integer
    Get
      Return _icms_ST_CST
    End Get
    Set(value As Integer)
      _icms_ST_CST = value
    End Set
  End Property

  Property icms_sem_ST_CST() As Integer
    Get
      Return _icms_sem_ST_CST
    End Get
    Set(value As Integer)
      _icms_sem_ST_CST = value
    End Set
  End Property

  Property CSOSN() As Integer
    Get
      Return _CSOSN
    End Get
    Set(value As Integer)
      _CSOSN = value
    End Set
  End Property

  Property ipi_CST() As Integer
    Get
      Return _ipi_CST
    End Get
    Set(value As Integer)
      _ipi_CST = value
    End Set
  End Property

  Property pis_CST() As Integer
    Get
      Return _pis_CST
    End Get
    Set(value As Integer)
      _pis_CST = value
    End Set
  End Property

  Property confins_CST() As Integer
    Get
      Return _confins_CST
    End Get
    Set(value As Integer)
      _confins_CST = value
    End Set
  End Property

  Property cfop2() As Integer
    Get
      Return _cfop2
    End Get
    Set(value As Integer)
      _cfop2 = value
    End Set
  End Property

  Property cfop2_ST() As Integer
    Get
      Return _cfop2_ST
    End Get
    Set(value As Integer)
      _cfop2_ST = value
    End Set
  End Property

  Property cfop2_sem_ST() As Integer
    Get
      Return _cfop2_sem_ST
    End Get
    Set(value As Integer)
      _cfop2_sem_ST = value
    End Set
  End Property

  Property icms_CST2() As Integer
    Get
      Return _icms_CST2
    End Get
    Set(value As Integer)
      _icms_CST2 = value
    End Set
  End Property

  Property icms_ST_CST2() As Integer
    Get
      Return _icms_ST_CST2
    End Get
    Set(value As Integer)
      _icms_ST_CST2 = value
    End Set
  End Property

  Property icms_sem_ST_CST2() As Integer
    Get
      Return _icms_sem_ST_CST2
    End Get
    Set(value As Integer)
      _icms_sem_ST_CST2 = value
    End Set
  End Property

  Property CSOSN2() As Integer
    Get
      Return _CSOSN2
    End Get
    Set(value As Integer)
      _CSOSN2 = value
    End Set
  End Property

  Property ipi_CST2() As Integer
    Get
      Return _ipi_CST2
    End Get
    Set(value As Integer)
      _ipi_CST2 = value
    End Set
  End Property

  Property pis_CST2() As Integer
    Get
      Return _pis_CST2
    End Get
    Set(value As Integer)
      _pis_CST2 = value
    End Set
  End Property

  Property confins_CST2() As Integer
    Get
      Return _confins_CST2
    End Get
    Set(value As Integer)
      _confins_CST2 = value
    End Set
  End Property

  Property cfop3() As Integer
    Get
      Return _cfop3
    End Get
    Set(value As Integer)
      _cfop3 = value
    End Set
  End Property

  Property icms_CST3() As Integer
    Get
      Return _icms_CST3
    End Get
    Set(value As Integer)
      _icms_CST3 = value
    End Set
  End Property

  Property CSOSN3() As Integer
    Get
      Return _CSOSN3
    End Get
    Set(value As Integer)
      _CSOSN3 = value
    End Set
  End Property

  Property ipi_CST3() As Integer
    Get
      Return _ipi_CST3
    End Get
    Set(value As Integer)
      _ipi_CST3 = value
    End Set
  End Property

  Property pis_CST3() As Integer
    Get
      Return _pis_CST3
    End Get
    Set(value As Integer)
      _pis_CST3 = value
    End Set
  End Property

  Property confins_CST3() As Integer
    Get
      Return _confins_CST3
    End Get
    Set(value As Integer)
      _confins_CST3 = value
    End Set
  End Property

  Property fCalc_vTrib() As Integer
    Get
      Return _fCalc_vTrib
    End Get
    Set(value As Integer)
      _fCalc_vTrib = value
    End Set
  End Property

  Property ipi_CST_prod() As Integer
    Get
      Return _ipi_CST_prod
    End Get
    Set(value As Integer)
      _ipi_CST_prod = value
    End Set
  End Property

  Property fRec_Tributos() As Integer
    Get
      Return _fRec_Tributos
    End Get
    Set(value As Integer)
      _fRec_Tributos = value
    End Set
  End Property

  Property ipi_CST_prod2() As Integer
    Get
      Return _ipi_CST_prod2
    End Get
    Set(value As Integer)
      _ipi_CST_prod2 = value
    End Set
  End Property

  Property ipi_CST_prod3() As Integer
    Get
      Return _ipi_CST_prod3
    End Get
    Set(value As Integer)
      _ipi_CST_prod3 = value
    End Set
  End Property

  Property id_nat_op_retorno() As Integer
    Get
      Return _id_nat_op_retorno
    End Get
    Set(value As Integer)
      _id_nat_op_retorno = value
    End Set
  End Property

  Property id_nat_op_remessa_venda() As Integer
    Get
      Return _id_nat_op_remessa_venda
    End Get
    Set(value As Integer)
      _id_nat_op_remessa_venda = value
    End Set
  End Property

  Property cofins_cst() As Integer
    Get
      Return _cofins_cst
    End Get
    Set(value As Integer)
      _cofins_cst = value
    End Set
  End Property

  Property cofins_cst2() As Integer
    Get
      Return _cofins_cst2
    End Get
    Set(value As Integer)
      _cofins_cst2 = value
    End Set
  End Property

  Property uso() As Integer
    Get
      Return _uso
    End Get
    Set(value As Integer)
      _uso = value
    End Set
  End Property

  Property cofins_cst3() As Integer
    Get
      Return _cofins_cst3
    End Get
    Set(value As Integer)
      _cofins_cst3 = value
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

  Property fPed_Item_Remessa() As Integer
    Get
      Return _fPed_Item_Remessa
    End Get
    Set(value As Integer)
      _fPed_Item_Remessa = value
    End Set
  End Property

  Property descr() As String
    Get
      Return _descr
    End Get
    Set(value As String)
      _descr = value
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

  Property f_CF() As String
    Get
      Return _f_CF
    End Get
    Set(value As String)
      _f_CF = value
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

  Property fLanc_Estoque() As String
    Get
      Return _fLanc_Estoque
    End Get
    Set(value As String)
      _fLanc_Estoque = value
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

  Property obs() As String
    Get
      Return _obs
    End Get
    Set(value As String)
      _obs = value
    End Set
  End Property

  Property f_ST() As String
    Get
      Return _f_ST
    End Get
    Set(value As String)
      _f_ST = value
    End Set
  End Property

  Property natOp2() As String
    Get
      Return _natOp2
    End Get
    Set(value As String)
      _natOp2 = value
    End Set
  End Property

  Property obs2() As String
    Get
      Return _obs2
    End Get
    Set(value As String)
      _obs2 = value
    End Set
  End Property

  Property f_ST2() As String
    Get
      Return _f_ST2
    End Get
    Set(value As String)
      _f_ST2 = value
    End Set
  End Property

  Property natOp3() As String
    Get
      Return _natOp3
    End Get
    Set(value As String)
      _natOp3 = value
    End Set
  End Property

  Property obs3() As String
    Get
      Return _obs3
    End Get
    Set(value As String)
      _obs3 = value
    End Set
  End Property

  Property gera_nf_retorno() As String
    Get
      Return _gera_nf_retorno
    End Get
    Set(value As String)
      _gera_nf_retorno = value
    End Set
  End Property

  Property nf_retorno_separada() As String
    Get
      Return _nf_retorno_separada
    End Get
    Set(value As String)
      _nf_retorno_separada = value
    End Set
  End Property

  Property partilha_ICMS_UF() As String
    Get
      Return _partilha_ICMS_UF
    End Get
    Set(value As String)
      _partilha_ICMS_UF = value
    End Set
  End Property

  Property p_red_BC_ICMS() As Decimal
    Get
      Return _p_red_BC_ICMS
    End Get
    Set(value As Decimal)
      _p_red_BC_ICMS = value
    End Set
  End Property

  Property descr_icms() As String
    Get
      Return _descr_icms
    End Get
    Set(value As String)
      _descr_icms = value
    End Set
  End Property

  Property descr_ipi() As String
    Get
      Return _descr_ipi
    End Get
    Set(value As String)
      _descr_ipi = value
    End Set
  End Property

  Property descr_pis() As String
    Get
      Return _descr_pis
    End Get
    Set(value As String)
      _descr_pis = value
    End Set
  End Property

  Property descr_cofins() As String
    Get
      Return _descr_cofins
    End Get
    Set(value As String)
      _descr_cofins = value
    End Set
  End Property

  Property f_vendas() As String
    Get
      Return _f_vendas
    End Get
    Set(value As String)
      _f_vendas = value
    End Set
  End Property

  Property f_contabilidade() As String
    Get
      Return _f_contabilidade
    End Get
    Set(value As String)
      _f_contabilidade = value
    End Set
  End Property

  Property fCria_CFOP() As String
    Get
      Return _fCria_CFOP
    End Get
    Set(value As String)
      _fCria_CFOP = value
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

  Property msg_result() As String
    Get
      Return _msg_result
    End Get
    Set(value As String)
      _msg_result = value
    End Set
  End Property

  ''' <summary>
  ''' Lista somente as naturezas de operação do tipo venda... para preenchimento de DropDownList
  ''' Value(0) | Text(SELECIONE)
  ''' </summary>
  ''' <param name="id_empresa"></param>
  ''' <param name="tipo"></param>
  ''' <returns></returns>
  Public Function ListaNatOpTipoVendaDDList(ByVal id_empresa As Integer, ByVal tipo As OpcaoSelecione) As ArrayList
    Dim array_list As New ArrayList
    Dim item As New ListItem
    Dim conexao As New clsConexao
    Dim dr As SqlDataReader
    Dim str_builder As New StringBuilder

    If tipo = OpcaoSelecione.comSelecione Then
      item.Value = "0"
      item.Text = "SELECIONE"

      array_list.Add(item)
    End If

    str_builder.Append("SELECT id_nat_op, descr ")
    str_builder.Append("FROM NFE_NatOp ")
    str_builder.Append("WHERE (id_empresa = " & id_empresa & ") AND (tipo_doc = 0) AND (tpNF = 1) AND (origem_nf = 'P') ")

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        item = New ListItem

        item.Value = dr(0)
        item.Text = dr(1)

        array_list.Add(item)
      Loop

      dr.Close()
    Catch ex As Exception
      Me.result = -1
      Me.msg_result = "ERRO AO LISTAR A NATUREZA DE OPERAÇÃO: " & ex.Message() & "--------" & ex.StackTrace()
    End Try

    Return array_list
  End Function

  ''' <summary>
  ''' Retorna um ArrayList para preenchimento de DropDownList... Value(0) | Text(SELECIONE)
  ''' </summary>
  ''' <param name="id_empresa"></param>
  ''' <param name="tipo"></param>
  ''' <returns></returns>
  Public Function ListaNatOpTipoRemessaDDList(ByVal id_empresa As Integer, ByVal tipo As OpcaoSelecione) As ArrayList
    Dim array_list As New ArrayList
    Dim item As New ListItem
    Dim conexao As New clsConexao
    Dim dr As SqlDataReader
    Dim str_builder As New StringBuilder

    If tipo = OpcaoSelecione.comSelecione Then
      item.Value = "0"
      item.Text = "SELECIONE"

      array_list.Add(item)
    End If


    str_builder.Append("SELECT id_nat_op, descr, cfop ")
    str_builder.Append("FROM NFE_NatOp ")
    str_builder.Append("WHERE (id_empresa = " & id_empresa & ") AND (tipo_doc = 0) AND (tpNF = 1) AND (origem_nf = 'C') ")

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        item = New ListItem

        item.Value = dr(0)
        item.Text = dr(1) & " (" & dr(2) & ")"

        array_list.Add(item)
      Loop

      dr.Close()
    Catch ex As Exception
      Me.result = -1
      Me.msg_result = "ERRO AO LISTAR A NATUREZA DE OPERAÇÃO: " & ex.Message() & "--------" & ex.StackTrace()
    End Try

    Return array_list

  End Function

  ''' <summary>
  ''' Lista todas as naturezas de operação cadastradas...
  ''' </summary>
  ''' <param name="id_empresa"></param>
  ''' <param name="tipo"></param>
  ''' <returns></returns>
  Public Function ListaNatOpDDList(ByVal id_empresa As Integer, ByVal tipo As OpcaoSelecione) As ArrayList
    Dim array_list As New ArrayList
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim item As New ListItem
    Dim dr As SqlDataReader

    If tipo = OpcaoSelecione.comSelecione Then
      item.Value = "0"
      item.Text = "SELECIONE"

      array_list.Add(item)
    End If

    str_builder.Append("SELECT id_nat_op, descr, cfop ")
    str_builder.Append("FROM NFE_NatOp ")
    str_builder.Append("WHERE (id_empresa = " & id_empresa & ") AND (tipo_doc = 0) ")
    str_builder.Append("ORDER BY descr ")

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        item = New ListItem

        item.Value = dr(0)
        item.Text = dr(1) & " (" & dr(2) & ")"

        array_list.Add(item)
      Loop

      dr.Close()
    Catch ex As Exception
      Me.result = -1
      Me.msg_result = "ERRO AO LISTAR A NATUREZA DE OPERAÇÃO: " & ex.Message() & "--------" & ex.StackTrace()
    End Try

    Return array_list

  End Function

  ''' <summary>
  ''' Lista as naturezas de operação de acordo com o SELECIONAR do cadastro deste...
  ''' </summary>
  ''' <param name="id_empresa"></param>
  ''' <param name="tipo"></param>
  ''' <param name="dest"></param>
  ''' <returns></returns>
  Public Function ListaNatOpPorDestinatario(ByVal id_empresa As Integer, ByVal tipo As OpcaoSelecione, ByVal dest As TipoDestinatario) As ArrayList
    Dim array_list As New ArrayList
    Dim item As New ListItem
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    If tipo = OpcaoSelecione.comSelecione Then
      item.Value = "0"
      item.Text = "SELECIONE"

      array_list.Add(item)
    End If

    If dest = TipoDestinatario.Clientes Then
      str_builder.Append("SELECT id_nat_op, descr, cfop ")
      str_builder.Append("FROM NFE_NatOp ")
      str_builder.Append("WHERE (id_empresa = " & id_empresa & ") AND (tipo_doc = 0) AND ((origem_nf = 'C') OR (origem_nf = 'D') OR (origem_nf = 'R') OR (origem_nf = 'P')) ")
      str_builder.Append("ORDER BY descr ")
    Else
      str_builder.Append("SELECT id_nat_op, descr, cfop ")
      str_builder.Append("FROM NFE_NatOp ")
      str_builder.Append("WHERE (id_empresa = " & id_empresa & ") AND (tipo_doc = 0) AND ((origem_nf = 'F') OR (origem_nf = 'E')) ")
      str_builder.Append("ORDER BY descr ")
    End If


    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        item = New ListItem

        item.Value = dr(0)
        item.Text = dr(1) & " (" & dr(2) & ")"

        array_list.Add(item)
      Loop

      dr.Close()
    Catch ex As Exception
      Me.result = -1
      Me.msg_result = "ERRO AO LISTAR A NATUREZA DE OPERAÇÃO: " & ex.Message() & "--------" & ex.StackTrace()
    End Try

    Return array_list

  End Function

  Public Function ListaNatOp(ByVal id_empresa As Integer, ByVal tipo_doc As TipoDoc, ByVal descr_nat_op As String, _
                             ByVal cfop As Integer) As DataView
    Dim table As New DataTable
    Dim row As DataRow
    Dim dr As SqlDataReader
    Dim str_builder As New StringBuilder
    Dim conexao As New clsConexao
    Dim tipo_documento As Integer

    If tipo_doc = tipoDoc.NFe Then
      tipo_documento = 0 'NFe
    Else
      tipo_documento = 1 'Recibo
    End If

    table.Columns.Add(New DataColumn("id_nat_op"))
    table.Columns.Add(New DataColumn("tpNF"))
    table.Columns.Add(New DataColumn("descr"))
    table.Columns.Add(New DataColumn("serie"))
    table.Columns.Add(New DataColumn("cfop"))
    table.Columns.Add(New DataColumn("cfop2"))
    table.Columns.Add(New DataColumn("cfop3"))

    str_builder.Append("EXEC sp9_NatOp_Pega '" & id_empresa & "','" & tipo_documento & "','0','" & descr_nat_op & "'")
    str_builder.Append(",'" & cfop & "'")

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        row = table.NewRow()

        row(0) = dr(0)
        row(1) = dr(1)
        row(2) = dr(2)
        row(3) = dr(4)
        row(4) = dr(8)
        row(5) = dr(15)
        row(6) = dr(27)

        table.Rows.Add(row)
      Loop

      dr.Close()
    Catch ex As Exception
      Me.result = -1
      Me.msg_result = "ERRO AO LISTAR OS CFOP's CADASTRADOS: " & ex.Message() & "--------" & ex.StackTrace()
    End Try

    Dim dv As New DataView(table)

    Return dv

  End Function

  Public Function PegaUmaNatOp(ByVal id_empresa As Integer, ByVal id_nat_op As Integer) As Boolean
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    str_builder.Append("EXEC sp9_Pega_Uma_NatOp '" & id_empresa & "','" & id_nat_op & "'")

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        Me.id_nat_op = dr(0)
        Me.tpNF = dr(1)
        Me.descr = dr(2)
        Me.origem_nf = dr(3)
        Me.serie = dr(4)
        Me.f_ST = dr(5)
        Me.p_red_BC_ICMS = dr(6)
        Me.natOp = dr(7)
        Me.cfop = dr(8)
        Me.cfop_st = dr(9)
        Me.cfop_sem_st = dr(10)
        Me.icms_CST = dr(11)
        Me.icms_ST_CST = dr(12)
        Me.icms_sem_ST_CST = dr(13)
        Me.ipi_CST = dr(14)
        Me.pis_CST = dr(15)
        Me.cofins_cst = dr(16)
        Me.obs = dr(17)
        Me.natOp2 = dr(18)
        Me.cfop2 = dr(19)
        Me.cfop2_ST = dr(20)
        Me.cfop2_sem_ST = dr(21)
        Me.icms_CST2 = dr(22)
        Me.icms_ST_CST2 = dr(23)
        Me.icms_sem_ST_CST2 = dr(24)
        Me.ipi_CST2 = dr(25)
        Me.pis_CST2 = dr(26)
        Me.cofins_cst2 = dr(27)
        Me.obs2 = dr(28)
        Me.f_Total = dr(29)
        Me.uso = dr(30)
        Me.f_ST2 = dr(31)
        Me.CSOSN = dr(32)
        Me.CSOSN2 = dr(33)
        Me.natOp3 = dr(34)
        Me.cfop3 = dr(35)
        Me.icms_CST3 = dr(36)
        Me.CSOSN3 = dr(37)
        Me.ipi_CST3 = dr(38)
        Me.pis_CST3 = dr(39)
        Me.cofins_cst3 = dr(40)
        Me.obs3 = dr(41)
        Me.f_CF = dr(42)
        Me.descr_icms = dr(43)
        Me.descr_ipi = dr(44)
        Me.descr_pis = dr(45)
        Me.descr_cofins = dr(46)
        Me.fCalc_vTrib = dr(47)
        Me.fLanc_Estoque = dr(48)
        Me.ipi_CST_prod = dr(49)
        Me.ipi_CST_prod2 = dr(50)
        Me.ipi_CST_prod3 = dr(51)
        Me.gera_nf_retorno = dr(52)
        Me.id_nat_op_retorno = dr(53)
        Me.fRec_Tributos = dr(54)
        Me.id_nat_op_remessa_venda = dr(55)
        Me.nf_retorno_separada = dr(56)
        Me.partilha_ICMS_UF = dr(57)
      Loop

      dr.Close()
    Catch ex As Exception
      Me.result = -1
      Me.msg_result = "ERRO AO PEGAR AS INFORMAÇÕES DA NATUREZA DE OPERAÇÃO: " & ex.Message() & "--------" & ex.StackTrace()

      Return False
    End Try

    Return True

  End Function

  Public Function ListaInfAdc(ByVal id_empresa As Integer, ByVal id_usuario As Integer) As Boolean
    Dim conexao As New clsConexao
    Dim dr As SqlDataReader
    Dim str_builder As New StringBuilder

    str_builder.Append("SELECT substring(acesso_empresa, 4,1) as f_vendas, CRT, ")
    str_builder.Append("substring(acesso_empresa, 1,1) as f_contabilidade, fPed_Item_Remessa, ")
    str_builder.Append("(SELECT fCria_CFOP FROM tbLogin_Empresa AS Z WHERE (Z.id_empresa = A.id_empresa) AND (Z.id_usuario =  " & id_usuario & ")) AS fCria_CFOP ")
    str_builder.Append("FROM tbEmpresas AS A ")
    str_builder.Append("WHERE (A.id_empresa = " & id_empresa & ") ")

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        Me.f_vendas = dr(0)
        Me.CRT = dr(1)
        Me.f_contabilidade = dr(2)
        Me.fPed_Item_Remessa = dr(3)
        Me.fCria_CFOP = dr(4)
      Loop

      dr.Close()

    Catch ex As Exception
      Me.result = -1
      Me.msg_result = "ERRO AO BUSCAR AS INFORMAÇÕES ADICIONAIS: " & ex.Message() & "-------" & ex.StackTrace()

      Return False
    End Try

    Return True

  End Function

  Public Function ListaNatOpRetornoCliente(ByVal id_empresa As Integer, ByVal tipo As OpcaoSelecione) As ArrayList
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader
    Dim array_list As New ArrayList
    Dim item As New ListItem

    str_builder.Append("SELECT id_nat_op, descr ")
    str_builder.Append("FROM NFE_NatOp ")
    str_builder.Append("WHERE (id_empresa = " & id_empresa & ") AND (origem_nf = 'R') ")

    If tipo = OpcaoSelecione.comSelecione Then
      item.Value = 0
      item.Text = "SELECIONE"
      array_list.Add(item)
    End If

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        item = New ListItem

        item.Value = dr(0)
        item.Text = dr(1)

        array_list.Add(item)
      Loop

      dr.Close()
    Catch ex As Exception
      Me.result = -1
      Me.msg_result = "ERRO AO LISTAR OS CFOP'S DE RETORNO: " & ex.Message() & "---------" & ex.StackTrace()
    End Try

    Return array_list

  End Function

  Public Sub SalvaUmaNatOp(ByVal id_empresa As Integer, ByVal id_nat_op As Integer, ByVal descr As String, ByVal tpNF As Integer, _
                           ByVal origem_nf As String, ByVal serie As Integer, ByVal f_ST As String, ByVal f_total As String, _
                           ByVal p_red_BC_ICMS As Decimal, ByVal cfop As Integer, ByVal ICMS_CST As Integer, ByVal IPI_CST As Integer, _
                           ByVal PIS_CST As Integer, ByVal COFINS_CST As Integer, ByVal obs As String, ByVal cfop2 As Integer, _
                           ByVal ICMS_CST2 As Integer, ByVal IPI_CST2 As Integer, ByVal PIS_CST2 As Integer, ByVal COFINS_CST2 As Integer, _
                           ByVal f_ST2 As String, ByVal CSOSN As Integer, ByVal CSOSN2 As Integer, ByVal cfop3 As Integer, ByVal ICMS_CST3 As Integer, _
                           ByVal CSOSN3 As Integer, ByVal IPI_CST3 As Integer, ByVal PIS_CST3 As Integer, ByVal COFINS_CST3 As Integer, _
                           ByVal obs3 As String, ByVal f_CF As String, ByVal fCalc_vTrib As Integer, ByVal fLanc_Estoque As String, _
                           ByVal IPI_CST_prod As Integer, ByVal IPI_CST_prod2 As Integer, ByVal IPI_CST_prod3 As Integer, _
                           ByVal gera_nf_retorno As String, ByVal id_nat_op_retorno As Integer, ByVal cfop_st As Integer, _
                           ByVal cfop_sem_st As Integer, ByVal cfop2_ST As Integer, ByVal cfop2_sem_st As Integer, ByVal icms_ST_CST As Integer, _
                           ByVal icms_sem_ST_CST As Integer, ByVal icms_ST_CST2 As Integer, ByVal icms_sem_ST_CST2 As Integer, _
                           ByVal id_nat_op_remessa_venda As Integer, ByVal nf_retorno_separada As String, ByVal partilha_ICMS_UF As String)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    str_builder.Append("EXEC sp9_NatOp_Grava '" & id_empresa & "','GRAVAR','" & id_nat_op & "','0','" & descr & "'")
    str_builder.Append(",'" & tpNF & "','" & origem_nf & "','" & serie & "','" & f_ST & "','" & f_total & "'")
    str_builder.Append(",'" & p_red_BC_ICMS & "','" & cfop & "','" & descr & "','" & ICMS_CST & "','" & IPI_CST & "'")
    str_builder.Append(",'" & PIS_CST & "','" & COFINS_CST & "','" & obs & "','" & cfop2 & "','" & descr & "'")
    str_builder.Append(",'" & ICMS_CST2 & "','" & IPI_CST2 & "','" & PIS_CST2 & "','" & COFINS_CST2 & "'")
    str_builder.Append(",'" & obs2 & "','" & f_ST2 & "','" & CSOSN & "','" & CSOSN2 & "','" & cfop3 & "'")
    str_builder.Append(",'" & descr & "','" & ICMS_CST3 & "','" & CSOSN3 & "','" & IPI_CST3 & "','" & PIS_CST3 & "'")
    str_builder.Append(",'" & COFINS_CST3 & "','" & obs3 & "','" & f_CF & "','" & fCalc_vTrib & "','" & fLanc_Estoque & "'")
    str_builder.Append(",'" & IPI_CST_prod & "','" & IPI_CST_prod2 & "','" & IPI_CST_prod3 & "','" & gera_nf_retorno & "'")
    str_builder.Append(",'" & id_nat_op_retorno & "','0','" & cfop_st & "','" & cfop_sem_st & "','" & cfop2_ST & "'")
    str_builder.Append(",'" & cfop2_sem_st & "','" & icms_ST_CST & "','" & icms_sem_ST_CST & "','" & icms_ST_CST2 & "'")
    str_builder.Append(",'" & icms_sem_ST_CST2 & "','" & id_nat_op_remessa_venda & "','" & nf_retorno_separada & "'")
    str_builder.Append(",'" & partilha_ICMS_UF & "'")

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read
        Me.result = dr(0)
        Me.msg_result = dr(1)
        Me.id_nat_op = dr(2)
      Loop

      dr.Close()

    Catch ex As Exception
      Me.result = -1
      Me.msg_result = "ERRO AO SALVAR AS ALTERAÇÕES DA NATUREZA DE OPERAÇÃO: " & ex.Message() & "---------" & ex.StackTrace()
    End Try

  End Sub

End Class
