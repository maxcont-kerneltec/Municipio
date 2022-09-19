Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsNFeEventos

  Private _id_nf, _tpEvento, _nSeqEvento, _cUF, _tpAmb, _nNF, _result, _serie As Integer
  Private _dhEvento, _xCorrecao, _xCondUso, _cProt, _arq_xml, _xJust, _sig_chNFe, _cnpj, _sig_nProt, _msg_result As String
  Private _cnpj_emp As String

  Public Sub New()

  End Sub

  Property cUF() As Integer
    Get
      Return _cUF
    End Get
    Set(value As Integer)
      _cUF = value
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

  Property tpAmb() As Integer
    Get
      Return _tpAmb
    End Get
    Set(value As Integer)
      _tpAmb = value
    End Set
  End Property

  Property id_nf() As Integer
    Get
      Return _id_nf
    End Get
    Set(value As Integer)
      _id_nf = value
    End Set
  End Property

  Property tpEvento() As Integer
    Get
      Return _tpEvento
    End Get
    Set(value As Integer)
      _tpEvento = value
    End Set
  End Property

  Property nSeqEvento() As Integer
    Get
      Return _nSeqEvento
    End Get
    Set(value As Integer)
      _nSeqEvento = value
    End Set
  End Property

  Property dhEvento() As String
    Get
      Return _dhEvento
    End Get
    Set(value As String)
      _dhEvento = value
    End Set
  End Property

  Property xCorrecao() As String
    Get
      Return _xCorrecao
    End Get
    Set(value As String)
      _xCorrecao = value
    End Set
  End Property

  Property xCondUso() As String
    Get
      Return _xCondUso
    End Get
    Set(value As String)
      _xCondUso = value
    End Set
  End Property

  Property cnpj_emp() As String
    Get
      Return _cnpj_emp
    End Get
    Set(value As String)
      _cnpj_emp = value
    End Set
  End Property

  Property cProt() As String
    Get
      Return _cProt
    End Get
    Set(value As String)
      _cProt = value
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

  Property xJust() As String
    Get
      Return _xJust
    End Get
    Set(value As String)
      _xJust = value
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

  Property cnpj() As String
    Get
      Return _cnpj
    End Get
    Set(value As String)
      _cnpj = value
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

  Property msg_result() As String
    Get
      Return _msg_result
    End Get
    Set(value As String)
      _msg_result = value
    End Set
  End Property

  Public Function ListaEventosUmaNFe(ByVal id_nf As Integer) As DataView
    Dim table As New DataTable
    Dim row As DataRow
    Dim dr As SqlDataReader
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    table.Columns.Add(New DataColumn("tpEvento"))
    table.Columns.Add(New DataColumn("nSeqEvento"))
    table.Columns.Add(New DataColumn("dhEvento"))
    table.Columns.Add(New DataColumn("xCorrecao"))
    table.Columns.Add(New DataColumn("xCondUso"))
    table.Columns.Add(New DataColumn("nProt"))
    table.Columns.Add(New DataColumn("arq_xml"))
    table.Columns.Add(New DataColumn("descr_tpEvento"))

    str_builder.Append("SELECT tpEvento, nSeqEvento, dhEvento, ")
    str_builder.Append("xCorrecao = Case tpEvento ")
    str_builder.Append("WHEN 110110 THEN xCorrecao ")
    str_builder.Append("WHEN 110111 THEN xJust ")
    str_builder.Append("END, xCondUso, nProt, arq_xml, ")
    str_builder.Append("descr_tpEvento = Case tpEvento ")
    str_builder.Append("WHEN 110110 THEN 'Carta de correção' ")
    str_builder.Append("WHEN 110111 THEN 'Cancelamento' ")
    str_builder.Append("END  ")

    str_builder.Append("FROM dbo.NFe_eventos ")

    str_builder.Append("WHERE (id_nf = " & id_nf & ")")


    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        row = table.NewRow()

        row(0) = dr(0)
        row(1) = dr(1)
        row(2) = dr(2)
        row(3) = dr(3)
        row(4) = dr(4)
        row(5) = dr(5)
        row(6) = dr(6)
        row(7) = dr(7)

        table.Rows.Add(row)
      Loop

      dr.Close()
    Catch ex As Exception
      Me.result = -1
      Me.msg_result = "ERRO AO LISTAR AS NOTAS FISCAIS REFERENCIADAS: " & ex.Message() & "----------" & ex.StackTrace()
    End Try

    Dim dv As New DataView(table)

    Return dv
  End Function

  ''' <summary>
  ''' Pega as informações necessárias para gerar o XML da correção para envio ao SEFAZ...
  ''' </summary>
  ''' <param name="id_nf"></param>
  Public Sub PegaInfGerarCartaCorrecao(ByVal id_nf As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    str_builder.Append("SELECT A.nNF, A.cUF, A.tpAmb, A.sig_chNFe, B.cnpj, ")
    str_builder.Append("ISNULL((SELECT MAX(nSeqEvento) FROM NFe_eventos AS Z WHERE (Z.id_nf = A.id_nf) AND (Z.tpEvento = 110110)),0) + 1 AS nSeqEvento ")
    str_builder.Append("FROM NFE_ide AS A ")
    str_builder.Append("INNER JOIN NFE_emit AS B ON (B.id_emit = A.id_emit) ")
    str_builder.Append("WHERE A.id_nf = " & id_nf & "")

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        Me.nNF = dr(0)
        Me.cUF = dr(1)
        Me.tpAmb = dr(2)
        Me.sig_chNFe = dr(3)
        Me.cnpj = dr(4)
        Me.nSeqEvento = dr(5)
      Loop

      dr.Close()
    Catch ex As Exception
      Me.result = -1
      Me.msg_result = "ERRO AO PEGAR AS INFORMAÇÕES PARA CARTA DE CORREÇÃO: " & ex.Message() & "----------" & ex.StackTrace()
    End Try

  End Sub

  ''' <summary>
  ''' Pega as informações necessárias para gerar o cancelamento da nota fiscal...
  ''' </summary>
  ''' <param name="id_nf"></param>
  Public Sub PegaInfGerarCancelamentoNFe(ByVal id_nf As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    str_builder.Append("SELECT A.nNF, A.cUF, A.tpAmb, A.sig_chNFe, A.sig_nProt, B.cnpj, ")
    str_builder.Append("ISNULL((SELECT MAX(nSeqEvento) FROM NFe_eventos As Z WHERE (Z.id_nf = A.id_nf) And (Z.tpEvento = 110111)),0) + 1 As nSeqEvento ")
    str_builder.Append("From NFE_ide As A ")
    str_builder.Append("INNER Join NFE_emit AS B ON (B.id_emit = A.id_emit) ")
    str_builder.Append("WHERE(A.id_nf = " & id_nf & ") ")

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        Me.nNF = dr(0)
        Me.cUF = dr(1)
        Me.tpAmb = dr(2)
        Me.sig_chNFe = dr(3)
        Me.sig_nProt = dr(4)
        Me.cnpj = dr(5)
        Me.nSeqEvento = dr(6)
      Loop

      dr.Close()
    Catch ex As Exception
      Me.result = -1
      Me.msg_result = "ERRO AO PEGAR AS INFORMAÇÕES PARA GERAR O CANCELAMENTO DA NOTA FISCAL: " & ex.Message() & "----------" & ex.StackTrace()
    End Try
  End Sub

  Public Sub PegaInfGerarInutilizacaoNFe(ByVal id_empresa As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    str_builder.Append("SELECT dbo.fCNPJ_Grava(A.cnpj_emp) AS cnpj_emp, B.cUF ")
    str_builder.Append("FROM tbEmpresas AS A ")
    str_builder.Append("INNER JOIN NFE_UF AS B ON (B.UF = A.uf) ")
    str_builder.Append("WHERE (id_empresa = " & id_empresa & " )")

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read
        Me.cnpj_emp = dr(0)
        Me.cUF = dr(1)
      Loop

      dr.Close()
    Catch ex As Exception
      Me.result = -1
      Me.msg_result = "ERRO AO PEGAR AS INFORMAÇÕES PARA INUTILIZAÇÃO: " & ex.Message() & "----------" & ex.StackTrace()
    End Try

  End Sub

End Class
