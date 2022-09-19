Imports Microsoft.VisualBasic
Imports System.Data.SqlClient

Public Class clsNfeItemCombustivel

  Private _nItem, _cProdANP, _CODIF As Integer
  Private _pMixGN, _qTemp, _qBcProd, _vAliqProd, _vCIDE, _pGLP, _pGNn, _pGNi, _vPart As Decimal
  Private _UFCons, _descANP, _msg_erro As String

  Public Sub New()

  End Sub

  Property nItem() As Integer
    Get
      Return _nItem
    End Get
    Set(value As Integer)
      _nItem = value
    End Set
  End Property

  Property cProdANP() As Integer
    Get
      Return _cProdANP
    End Get
    Set(value As Integer)
      _cProdANP = value
    End Set
  End Property

  Property pMixGN() As Decimal
    Get
      Return _pMixGN
    End Get
    Set(value As Decimal)
      _pMixGN = value
    End Set
  End Property

  Property CODIF() As Integer
    Get
      Return _CODIF
    End Get
    Set(value As Integer)
      _CODIF = value
    End Set
  End Property

  Property qTemp() As Decimal
    Get
      Return _qTemp
    End Get
    Set(value As Decimal)
      _qTemp = value
    End Set
  End Property

  Property qBcProd() As Decimal
    Get
      Return _qBcProd
    End Get
    Set(value As Decimal)
      _qBcProd = value
    End Set
  End Property

  Property vAliqProd() As Decimal
    Get
      Return _vAliqProd
    End Get
    Set(value As Decimal)
      _vAliqProd = value
    End Set
  End Property

  Property vCIDE() As Decimal
    Get
      Return _vCIDE
    End Get
    Set(value As Decimal)
      _vCIDE = value
    End Set
  End Property

  Property UFCons() As String
    Get
      Return _UFCons
    End Get
    Set(value As String)
      _UFCons = value
    End Set
  End Property

  Property descANP() As String
    Get
      Return _descANP
    End Get
    Set(value As String)
      _descANP = value
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

  Property pGLP() As Decimal
    Get
      Return _pGLP
    End Get
    Set(value As Decimal)
      _pGLP = value
    End Set
  End Property

  Property pGNn() As Decimal
    Get
      Return _pGNn
    End Get
    Set(value As Decimal)
      _pGNn = value
    End Set
  End Property

  Property pGNi() As Decimal
    Get
      Return _pGNi
    End Get
    Set(value As Decimal)
      _pGNi = value
    End Set
  End Property

  Property vPart() As Decimal
    Get
      Return _vPart
    End Get
    Set(value As Decimal)
      _vPart = value
    End Set
  End Property

  ''' <summary>
  ''' Retorna TRUE... caso haja informações de combustível para o item... retorna os valores dos campos...
  ''' </summary>
  ''' <param name="id_nf"></param>
  ''' <param name="nItem"></param>
  ''' <returns></returns>
  Public Function PegaCombustivelUmItemNfe(ByVal id_nf As Integer, ByVal nItem As Integer) As Boolean
    Dim retorno As Boolean = False
    Dim dr As SqlDataReader
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    str_builder.Append("SELECT cProdANP, pMixGN, CODIF, qTemp, UFCons, qBcProd, vAliqProd, vCIDE, ")
    str_builder.Append("ISNULL(descANP, ''), pGLP, pGNn, pGNi, vPart ")
    str_builder.Append("FROM NFE_item_Combustivel ")
    str_builder.Append("WHERE (id_nf = " & id_nf & ") AND (nItem = " & nItem & ") ")

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      If dr.HasRows Then
        retorno = True

        Do While dr.Read()
          Me.cProdANP = dr(0)
          Me.pMixGN = dr(1)
          Me.CODIF = dr(2)
          Me.qTemp = dr(3)
          Me.UFCons = dr(4)
          Me.qBcProd = dr(5)
          Me.vAliqProd = dr(6)
          Me.vCIDE = dr(7)
          Me.descANP = dr(8)
          Me.pGLP = dr(9)
          Me.pGNn = dr(10)
          Me.pGNi = dr(11)
          Me.vPart = dr(12)
        Loop

      End If

      dr.Close()
    Catch ex As Exception
      _msg_erro = "ERRO AO LISTAR OS DADOS DO COMBUSTÍVEL: " & ex.Message() & "----------" & ex.StackTrace()
    End Try

    Return retorno
  End Function

End Class
