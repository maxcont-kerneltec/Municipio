Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class clsNfeVolumes

  Private _id_nf, _seq, _qVol As Integer
  Private _esp, _marca, _nVol, _msg_erro As String
  Private _pesoL, _pesoB As Decimal
  Private conexao1 As New SqlConnection

  Public Sub New()

  End Sub

  Property id_nf() As Integer
    Get
      Return _id_nf
    End Get
    Set(value As Integer)
      _id_nf = value
    End Set
  End Property

  Property seq() As Integer
    Get
      Return _seq
    End Get
    Set(value As Integer)
      _seq = value
    End Set
  End Property

  Property qVol() As Integer
    Get
      Return _qVol
    End Get
    Set(value As Integer)
      _qVol = value
    End Set
  End Property

  Property esp() As String
    Get
      Return _esp
    End Get
    Set(value As String)
      _esp = value
    End Set
  End Property

  Property marca() As String
    Get
      Return _marca
    End Get
    Set(value As String)
      _marca = value
    End Set
  End Property

  Property nVol() As String
    Get
      Return _nVol
    End Get
    Set(value As String)
      _nVol = value
    End Set
  End Property

  Property pesoL() As Decimal
    Get
      Return _pesoL
    End Get
    Set(value As Decimal)
      _pesoL = value
    End Set
  End Property

  Property pesoB() As Decimal
    Get
      Return _pesoB
    End Get
    Set(value As Decimal)
      _pesoB = value
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

  Public Function ListaVolumeUmaNfe(ByVal id_nf As Integer) As DataView
    Dim table As New DataTable
    Dim row As DataRow
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader
    Dim conexao As New clsConexao

    table.Columns.Add(New DataColumn("seq"))
    table.Columns.Add(New DataColumn("qVol"))
    table.Columns.Add(New DataColumn("esp"))
    table.Columns.Add(New DataColumn("marca"))
    table.Columns.Add(New DataColumn("nVol"))
    table.Columns.Add(New DataColumn("pesoL"))
    table.Columns.Add(New DataColumn("pesoB"))

    str_builder.Append("SELECT seq, qVol, esp, ISNULL(marca, ''), ISNULL(nVol, ''), pesoL, pesoB ")
    str_builder.Append("FROM NFE_volumes ")
    str_builder.Append("WHERE (id_nf = " & id_nf & ") ")


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
        row(5) = dr(5)
        row(6) = dr(6)

        table.Rows.Add(row)
      Loop
      conexao.FechaBanco(conexao1)
      dr.Close()
    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      _msg_erro = "ERRO AO LISTAR O VOLUME DA NOTA: " & ex.Message() & "-------------" & ex.StackTrace()
    End Try

    Dim dv As New DataView(table)

    Return dv
  End Function

  Public Sub AdicionaUmVolumeNfe(ByVal id_nf As Integer, ByVal qVol As Integer, ByVal esp As String, ByVal marca As String, _
                                 ByVal nVol As String, ByVal pesoL As Decimal, ByVal pesoB As Decimal)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_NFE_volumes_Atualiza '" & id_nf & "','0','" & qVol & "','" & esp & "','" & marca & "'")
    str_builder.Append(",'" & nVol & "','" & pesoL & "','" & pesoB & "','I'")

    Try
      conexao.ExecutaComando(str_builder.ToString())

    Catch ex As Exception
      _msg_erro = "ERRO AO ADICIONAR O VOLUME: " & ex.Message() & "--------------" & ex.StackTrace()
    End Try
  End Sub

  Public Sub ExcluiUmVolumeNfe(ByVal id_nf As Integer, ByVal seq As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_NFE_volumes_Atualiza '" & id_nf & "','" & seq & "','','','','','','','D'")

    Try
      conexao.ExecutaComando(str_builder.ToString())
    Catch ex As Exception
      _msg_erro = "ERRO AO EXCLUÍR O VOLUME: " & ex.Message() & "-----------" & ex.StackTrace()
    End Try
  End Sub

End Class
