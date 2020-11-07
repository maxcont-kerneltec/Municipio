Imports Microsoft.VisualBasic
Imports System.Data.SqlClient

Public Class clsNfeItemIcmsDest

  Private _vBCUFDest, _pFCPUFDest, _pICMSUFDest, _pICMSInter, _pICMSInterPart, _vFCPUFDest, _vICMSUFDest As Decimal
  Private _vICMSUFRemet, _vBCFCPUFDest, _vBCFCPST, _pFCPST, _vFCPST As Decimal
  Private _msg_erro As String
  Private conexao1 As New SqlConnection

  Public Sub New()

  End Sub

  Property vBCUFDest() As Decimal
    Get
      Return _vBCUFDest
    End Get
    Set(value As Decimal)
      _vBCUFDest = value
    End Set
  End Property

  Property pFCPUFDest() As Decimal
    Get
      Return _pFCPUFDest
    End Get
    Set(value As Decimal)
      _pFCPUFDest = value
    End Set
  End Property

  Property pICMSUFDest() As Decimal
    Get
      Return _pICMSUFDest
    End Get
    Set(value As Decimal)
      _pICMSUFDest = value
    End Set
  End Property

  Property pICMSInter() As Decimal
    Get
      Return _pICMSInter
    End Get
    Set(value As Decimal)
      _pICMSInter = value
    End Set
  End Property

  Property pICMSInterPart() As Decimal
    Get
      Return _pICMSInterPart
    End Get
    Set(value As Decimal)
      _pICMSInterPart = value
    End Set
  End Property

  Property vFCPUFDest() As Decimal
    Get
      Return _vFCPUFDest
    End Get
    Set(value As Decimal)
      _vFCPUFDest = value
    End Set
  End Property

  Property vICMSUFDest() As Decimal
    Get
      Return _vICMSUFDest
    End Get
    Set(value As Decimal)
      _vICMSUFDest = value
    End Set
  End Property

  Property vICMSUFRemet() As Decimal
    Get
      Return _vICMSUFRemet
    End Get
    Set(value As Decimal)
      _vICMSUFRemet = value
    End Set
  End Property

  Property vBCFCPUFDest() As Decimal
    Get
      Return _vBCFCPUFDest
    End Get
    Set(value As Decimal)
      _vBCFCPUFDest = value
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

  Property vBCFCPST() As Decimal
    Get
      Return _vBCFCPST
    End Get
    Set(value As Decimal)
      _vBCFCPST = value
    End Set
  End Property

Property pFCPST() As Decimal
    Get
      Return _pFCPST
    End Get
    Set(value As Decimal)
      _pFCPST = value
    End Set
  End Property

Property vFCPST() As Decimal
    Get
      Return _vFCPST
    End Get
    Set(value As Decimal)
      _vFCPST = value
    End Set
  End Property

  Public Sub ListaAbaTributosNfeItemIcmsInterest(ByVal id_nf As Integer, ByVal nItem As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    str_builder.Append("SELECT vBCUFDest, pFCPUFDest, pICMSUFDest, pICMSInter, pICMSInterPart, vFCPUFDest, ")
    str_builder.Append("vICMSUFDest, vICMSUFRemet, vBCFCPUFDest, vBCFCPST, pFCPST, vFCPST ")
    str_builder.Append("FROM NFe_item_ICMS_Dest ")
    str_builder.Append("WHERE (id_nf = " & id_nf & ") AND (nItem = " & nItem & ") ")

    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      Do While dr.Read()
        _vBCUFDest = dr(0)
        _pFCPUFDest = dr(1)
        _pICMSUFDest = dr(2)
        _pICMSInter = dr(3)
        _pICMSInterPart = dr(4)
        _vFCPUFDest = dr(5)
        _vICMSUFDest = dr(6)
        _vICMSUFRemet = dr(7)
        _vBCFCPUFDest = dr(8)
        _vBCFCPST = dr(9)
        _pFCPST = dr(10)
        _vFCPST = dr(11)
      Loop
      conexao.FechaBanco(conexao1)
      dr.Close()
    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      _msg_erro = "ERRO AO PEGAR AS INFORMAÇÕES DO ICMS INTERESTADUAL: " & ex.Message() & "---------" & ex.StackTrace()
    End Try
  End Sub

  Public Sub CalcICMSInter(ByVal id_nf As Integer, ByVal nItem As Integer, ByVal id_empresa As Integer, _
                           ByVal pFCPUFDest As Decimal, ByVal pICMSUFDest As Decimal)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_NFe_Item_Calc_ICMS_Inter '" & id_nf & "','" & nItem & "','" & id_empresa & "'")
    str_builder.Append(",'" & pFCPUFDest & "','" & pICMSUFDest & "'")

    Try
      conexao.ExecutaComando(str_builder.ToString())

    Catch ex As Exception
      _msg_erro = "ERRO AO CALCULAR O VALOR DO ICMS INTERESTADUAL: " & ex.Message() & "---------" & ex.StackTrace()
    End Try
  End Sub
End Class
