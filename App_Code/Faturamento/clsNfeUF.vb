Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Collections

Public Class clsNfeUF

  Private _UF, _WS, _WS_3, _WS_NF_dest As String
  Private _cUF, _regiao As Integer
  Private _pICMS, _pICMS_Int, _pICMS_1, _pICMS_2 As Decimal

  Public Sub New()

  End Sub

  Property UF() As String
    Get
      Return _UF
    End Get
    Set(value As String)
      _UF = value
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

  Property pICMS() As Decimal
    Get
      Return _pICMS
    End Get
    Set(value As Decimal)
      _pICMS = value
    End Set
  End Property

  Property pICMS_Int() As Decimal
    Get
      Return _pICMS_Int
    End Get
    Set(value As Decimal)
      _pICMS_Int = value
    End Set
  End Property

  Property regiao() As Integer
    Get
      Return _regiao
    End Get
    Set(value As Integer)
      _regiao = value
    End Set
  End Property

  Property pICMS_1() As Decimal
    Get
      Return _pICMS_1
    End Get
    Set(value As Decimal)
      _pICMS_1 = value
    End Set
  End Property

  Property pICMS_2() As Decimal
    Get
      Return _pICMS_2
    End Get
    Set(value As Decimal)
      _pICMS_2 = value
    End Set
  End Property

  Property WS() As String
    Get
      Return _WS
    End Get
    Set(value As String)
      _WS = value
    End Set
  End Property

  Property WS_3() As String
    Get
      Return _WS_3
    End Get
    Set(value As String)
      _WS_3 = value
    End Set
  End Property

  Property WS_NF_dest() As String
    Get
      Return _WS_NF_dest
    End Get
    Set(value As String)
      _WS_NF_dest = value
    End Set
  End Property

  ''' <summary>
  ''' Retorna a lista de UF para DropDownList: Value(--) | Text(SELECIONE)
  ''' </summary>
  ''' <param name="tipo"></param>
  ''' <returns></returns>
  Public Function ListaUFDDList(ByVal tipo As OpcaoSelecione) As ArrayList
    Dim array_list As New ArrayList
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader
    Dim item As New ListItem

    If tipo = OpcaoSelecione.comSelecione Then
      item.Value = "--"
      item.Text = "SELECIONE"
      array_list.Add(item)
    End If

    str_builder.Append("SELECT UF FROM NFE_UF WHERE (UF <> '--')")

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        item = New ListItem

        item.Value = dr(0)
        item.Text = dr(0)

        array_list.Add(item)
      Loop

      dr.Close()
    Catch ex As Exception
      MsgBox("ERRO AO CARREGAR O UF: " & ex.Message() & "----------" & ex.StackTrace(), MsgBoxStyle.Critical)
    End Try


    Return array_list

  End Function

  ''' <summary>
  ''' Pega o estado do município solicitado...
  ''' </summary>
  ''' <returns></returns>
  Public Function PegaUFmunicipio(ByVal cMun As Integer) As String
    Dim retorno As String = ""
    Dim conexao As New clsConexao
    Dim dr As SqlDataReader
    Dim str_builder As New StringBuilder

    str_builder.Append("SELECT UF FROM NFE_Municipio WHERE (cMun = " & cMun & ")")

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        retorno = dr(0)
      Loop

      dr.Close()
    Catch ex As Exception
      MsgBox("ERRO AO BUSCAR O ESTADO: " & ex.Message() & "-------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
    End Try

    Return retorno
  End Function

  ''' <summary>
  ''' Lista os estados, onde o Value é integer e o Text é string...
  ''' </summary>
  ''' <param name="tipo"></param>
  ''' <returns></returns>
  Public Function ListaCodigoUFDDList(ByVal tipo As OpcaoSelecione) As ArrayList
    Dim array_list As New ArrayList
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader
    Dim item As New ListItem

    If tipo = OpcaoSelecione.comSelecione Then
      item.Value = "0"
      item.Text = "SELECIONE"
      array_list.Add(item)
    End If

    str_builder.Append("SELECT UF, cUF FROM NFE_UF WHERE (UF <> '--')")

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        item = New ListItem

        item.Value = dr(1)
        item.Text = dr(0)

        array_list.Add(item)
      Loop

      dr.Close()
    Catch ex As Exception
      MsgBox("ERRO AO CARREGAR O UF: " & ex.Message() & "----------" & ex.StackTrace(), MsgBoxStyle.Critical)
    End Try


    Return array_list

  End Function

End Class
