Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml
Imports System.IO
Imports System.Text
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration


<WebService(Namespace:="http://www.maxcont.com.br/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class WS_PDV_Consulta
     Inherits System.Web.Services.WebService

    'CONSULTA VERSÃO DO PDV DO CLIENTE
    <WebMethod()> _
    Public Function Pega_PDV_Versao(ByVal id_empresa As String, cnpj As String, versao As String) As String
      Dim result As String = ""
      Dim cod_result As String = ""
      Dim strCnn As String = GetConnectionString("maxcont_cloud")
      Dim strSQL As String = "EXEC spWS_PDV_Pega_Versao '" & id_empresa & "','" & cnpj & "','VALIDAR'"

      Dim cnn As New SqlConnection(strCnn)
      cnn.Open()
      Dim cmd As New SqlClient.SqlCommand(strSQL, cnn)
      'cmd.Parameters.Add(New SqlParameter("@ProductID", productID))

      Dim resultado As String = ""

      Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
      If dr.HasRows Then
        dr.Read()
        result = dr("result")
        cod_result = dr("cod_result")
      End If
      cnn.Close()


      Dim txt_cabecalho As String = ""

      If cod_result = "1" Then

        Dim strCnn2 As String = GetConnectionString("maxcont_cloud")
        Dim cnn2 As New SqlConnection(strCnn2)

        Dim command As SqlCommand = New SqlCommand("EXEC spWS_PDV_Pega_Versao '" & id_empresa & "','" & cnpj & "','CONSULTAR','" & versao & "'", cnn2)

        cnn2.Open()
        Dim reader As SqlDataReader = command.ExecuteReader()

        txt_cabecalho = "id_empresa|"
        txt_cabecalho = txt_cabecalho & "versao|"
        txt_cabecalho = txt_cabecalho & "data|"
        txt_cabecalho = txt_cabecalho & "url_instala|"
        txt_cabecalho = txt_cabecalho & "obs|{"

        If reader.HasRows Then
          Do While reader.Read()

            txt_cabecalho = txt_cabecalho & id_empresa & "|" 'id_empresa
            txt_cabecalho = txt_cabecalho & reader.GetString(0) & "|" 'versao
            txt_cabecalho = txt_cabecalho & reader.GetString(1) & "|" 'data
            txt_cabecalho = txt_cabecalho & reader.GetString(2) & "|" 'url de instalação
            txt_cabecalho = txt_cabecalho & reader.GetString(3) & "|" 'obs

            txt_cabecalho = txt_cabecalho & "{"

            'sw.WriteLine(txt_cabecalho)
          Loop
        End If
        cnn.Close()
      End If
      Return txt_cabecalho
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
