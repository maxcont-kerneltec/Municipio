Imports WS_GIBI_Default
Imports System.Web.Services

Partial Class WS_GIBI_Default
    Inherits System.Web.UI.Page
    
  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

   Call_Web_Service_Method()

  End Sub

    
    Private Sub Call_Web_Service_Method() 
        Dim CallWebService2 as New  WS_GIBI.CompletaServiceSoapClient()
            'Dim CallWebService as New  WS_GIBI.PrevisaoSaida() 
        Dim previsao As New WS_GIBI.PrevisaoSaida

        previsao.sCodigoEmpresa = "0"

        Dim resultado As String = CallWebService2.CadastraPrevisaoSaida(previsao,"34137695")

        Response.Write (resultado)
     End Sub 
End Class
