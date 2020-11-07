Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataTable
Imports System
Imports System.Configuration


Public Class clsConexao
  'Dim conexao As String = "Data Source=189.126.98.131;Initial Catalog=sites_gerencia;Persist Security Info=True;User ID=sites_gerencia;Password=bd0106"
  'Dim conexao As String = "Data Source=Kernel-PC\Kernel;server=(local);uid=plesk;pwd=kfool604;database=maxcont;Max pool size=1500;Min pool size=200;Pooling=true"
  'retirei da conexao acima: Min Pool Size=5;Max Pool Size=250; Connect Timeout=3
  Dim conexao As String = GetConnectionString("maxcont_cloud") '"Data Source=marcelo-pc\maxcont;Initial Catalog=maxcont;Persist Security Info=True;User ID=sql-maxcont;Password=kerneltec"

  Public Function AbreBanco() As SqlConnection 'Abre o SQL

    Dim conectar As New SqlConnection

    With conectar
      .ConnectionString = conexao
      .Open()
    End With

    Return conectar
  End Function

  Public Sub FechaBanco(ByVal conectar As SqlConnection) 'Fecha o SQL

    If conectar.State = ConnectionState.Open Then

      conectar.Close()

    End If
  End Sub

  Public Sub ExecutaComando(ByVal funcao As String)
    Dim conectar As New SqlConnection

    Try
      conectar = AbreBanco()

      Dim comandos As New SqlCommand

      With comandos
        .CommandText = funcao
        .CommandType = CommandType.Text
        .Connection = conectar
        .ExecuteNonQuery()
      End With

    Catch ex As Exception
      Throw ex

    Finally
      FechaBanco(conectar)

    End Try
  End Sub

  Public Function RetornaDataSet(ByVal funcao As String) As DataSet

    Dim conectar As New SqlConnection

    Try
      conectar = AbreBanco()
      Dim comandos As New SqlCommand
      With comandos
        .CommandText = funcao
        .CommandType = CommandType.Text
        .Connection = conectar
      End With

      Dim adaptador As New SqlDataAdapter
      Dim data_set As New DataSet

      adaptador.SelectCommand = comandos
      adaptador.Fill(data_set)

      Return data_set

    Catch ex As Exception

      Throw New Exception("Erro na camada" & ex.Message)

    Finally
      FechaBanco(conectar)

    End Try
  End Function

  Public Function RetornaDataReader(ByVal funcao As String) As SqlDataReader

    Dim conexao As New SqlConnection

    Try
      conexao = AbreBanco()

      Dim comandos As New SqlCommand

      With comandos
        .CommandText = funcao
        .CommandType = CommandType.Text
        .Connection = conexao
      End With

      Return comandos.ExecuteReader

    Catch ex As Exception
      Throw ex
    End Try
  End Function


  Public Function RetornaDataReader_Conexao(ByVal funcao As String, conexao_data_reader As SqlConnection) As SqlDataReader

    Try
      'conexao1 = AbreBanco()

      'Dim SQLcomando As SqlDataReader

      Dim comandos As New SqlCommand

      With comandos
        .CommandText = funcao
        .CommandType = CommandType.Text
        .Connection = conexao_data_reader
      End With

      'SQLcomando = comandos.ExecuteReader(CommandBehavior.CloseConnection) 'Return comandos.ExecuteReader(CommandBehavior.CloseConnection)
      Return comandos.ExecuteReader(CommandBehavior.CloseConnection)
      'FechaBanco(conexao1)

    Catch ex As Exception
      Throw ex
      FechaBanco(conexao_data_reader)

      'Finally

      'FechaBanco(conexao1)

    End Try

  End Function

  Public Function RetornarDataTable(ByVal cmd As SqlCommand) As DataTable

    Dim conexao As New SqlConnection
    Dim dt As New DataTable
    Dim sda As New SqlDataAdapter

    conexao = AbreBanco()

    cmd.CommandType = CommandType.Text
    cmd.Connection = conexao
    Try
      sda.SelectCommand = cmd
      sda.Fill(dt)
      Return dt

    Catch ex As Exception
      Throw ex

    End Try

  End Function

  Private Shared Function GetConnectionString(ByVal name As String) As String

    Dim settings As ConnectionStringSettings = ConfigurationManager.ConnectionStrings(name)

    If Not settings Is Nothing Then
      Return settings.ConnectionString
    Else
      Return Nothing
    End If
  End Function
End Class


