Imports Microsoft.VisualBasic
Imports System.Data.SqlClient

Public Class clsNfeMunicipio

  Private _cMun, _cUF, _fIntegra, _cMun_SIAFI As Integer
  Private _UF, _xMun, _url_ajuda As String

  Public Sub New()

  End Sub

  Property cMun() As Integer
    Get
      Return _cMun
    End Get
    Set(value As Integer)
      _cMun = value
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

  Property fIntegra() As Integer
    Get
      Return _fIntegra
    End Get
    Set(value As Integer)
      _fIntegra = value
    End Set
  End Property

  Property cMun_SIAFI() As Integer
    Get
      Return _cMun_SIAFI
    End Get
    Set(value As Integer)
      _cMun_SIAFI = value
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

  Property xMun() As String
    Get
      Return _xMun
    End Get
    Set(value As String)
      _xMun = value
    End Set
  End Property

  Property url_ajuda() As String
    Get
      Return _url_ajuda
    End Get
    Set(value As String)
      _url_ajuda = value
    End Set
  End Property

  ''' <summary>
  ''' Retorna a lista de Município para DropDownList: Value(0) | Text(SELECIONE)
  ''' </summary>
  ''' <param name="tipo"></param>
  ''' <returns></returns>
  Public Function ListaMunicipioDDList(ByVal UF As String, ByVal tipo As OpcaoSelecione) As ArrayList
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

    If UF = "" Then UF = "--"

    str_builder.Append("SELECT cMun, xMun FROM NFE_Municipio ")
    str_builder.Append("WHERE (UF = '" & UF & "') AND (cMun > 0)")

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
      MsgBox("ERRO AO CARREGAR O MUNICÍPIO: " & ex.Message(), "---------" & ex.StackTrace(), MsgBoxStyle.Critical)
    End Try

    Return array_list

  End Function

  ''' <summary>
  ''' Retorna o código do município
  ''' </summary>
  ''' <param name="municipio"></param>
  ''' <returns></returns>
  Public Function PegacMun(ByVal municipio As String) As Integer
    Dim cMun As Integer
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    If municipio = "" Then
      Return -1
    End If


    str_builder.Append("SELECT cMun FROM nfe_municipio WHERE (xMun = '" & municipio & "')")

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        cMun = dr(0)
      Loop

      dr.Close()
    Catch ex As Exception
      MsgBox("ERRO AO CARREGAR O cMun: " & ex.Message() & "--------------" & ex.StackTrace(), MsgBoxStyle.Critical)
    End Try

    Return cMun

  End Function

End Class
