Imports Microsoft.VisualBasic
Imports System.Data.SqlClient

Public Class clsNfePais

  Private _cPais As Integer
  Private _xPais, _sigla As String

  Public Sub New()

  End Sub

  Property cPais() As Integer
    Get
      Return _cPais
    End Get
    Set(value As Integer)
      _cPais = value
    End Set
  End Property

  Property xPais() As String
    Get
      Return _xPais
    End Get
    Set(value As String)
      _xPais = value
    End Set
  End Property

  Property sigla() As String
    Get
      Return _sigla
    End Get
    Set(value As String)
      _sigla = value
    End Set
  End Property

  ''' <summary>
  ''' Retorna um array_list com a lista de todos os paises para DropDownList: Value(0) | Text(SELECIONE)
  ''' </summary>
  ''' <param name="tipo"></param>
  ''' <returns></returns>
  Public Function ListaPaisDDList(ByVal tipo As OpcaoSelecione) As ArrayList
    Dim array_list As New ArrayList
    Dim item As New ListItem
    Dim conexao As New clsConexao
    Dim dr As SqlDataReader
    Dim str_builder As New StringBuilder

    If tipo = OpcaoSelecione.comSelecione Then
      item.Text = "0"
      item.Value = "SELECIONE"
      array_list.Add(item)
    End If

    str_builder.Append("SELECT cPais, xPais FROM NFE_Pais WHERE (cPais > 0)")

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
      MsgBox("ERRO AO LISTAR OS PAISES: " & ex.Message() & "----------------" & ex.StackTrace(), MsgBoxStyle.Critical)
    End Try

    Return array_list

  End Function

End Class
