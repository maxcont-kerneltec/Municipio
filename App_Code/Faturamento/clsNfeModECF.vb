Imports Microsoft.VisualBasic
Imports System.Data.SqlClient

Public Class clsNfeModECF

  Private _id_mod As Integer
  Private _descr_mod As String

  Public Sub New()

  End Sub

  Property id_mod() As Integer
    Get
      Return _id_mod
    End Get
    Set(value As Integer)
      _id_mod = value
    End Set
  End Property

  Property descr_mod() As String
    Get
      Return _descr_mod
    End Get
    Set(value As String)
      _descr_mod = value
    End Set
  End Property

  ''' <summary>
  ''' Opção Selecione retorna: Value(0) | Text(SELECIONE)...
  ''' </summary>
  ''' <param name="tipo"></param>
  ''' <returns></returns>
  Public Function ListaModEcfDDList(ByVal tipo As OpcaoSelecione) As ArrayList
    Dim array As New ArrayList
    Dim item As New ListItem
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    If tipo = OpcaoSelecione.comSelecione Then
      item.Value = 0
      item.Text = "SELECIONE"
      array.Add(item)
    End If

    str_builder.Append("SELECT id_mod, mod FROM NFE_modECF ")


    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        item = New ListItem

        item.Value = dr(0)
        item.Text = dr(1)

        array.Add(item)
      Loop

      dr.Close()
    Catch ex As Exception
      MsgBox("ERRO AO LISTAR O ECF: " & ex.Message() & "------------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
    End Try

    Return array

  End Function

End Class
