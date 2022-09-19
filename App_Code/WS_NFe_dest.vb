Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml
Imports System.IO
Imports System.Text
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration

' Define a SOAP header by deriving from the SoapHeader base class.
Public Class MyHeader : Inherits SoapHeader
    Public MyValue As String
End Class


<WebService(Namespace:="http://www.maxcont.com.br/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class WS_NFe_dest
    Inherits System.Web.Services.WebService
    Public myHeader As MyHeader
    
    ' Receive all SOAP headers other than the MyHeader SOAP header.
    'Public unknownHeaders() As SoapUnknownHeader 

    'busca string de conexão a partir do WebConfig

    <WebMethod()> _
    Public Function Atualiza_NSU(ByVal id_empresa As String, Ult_NSU As String, Chave_Emissor As String, ult_cStat as String) As String
      'Atualiza último NSU consultado pela empresa
      Dim result As String = ""
      Dim strCnn As String = GetConnectionString("maxcont_cloud")
		  Dim strSQL As String = "EXEC spWS_NFe_Ult_NSU_Grava '" & id_empresa & "','" & Ult_NSU & "','" & Chave_Emissor & "','" & ult_cStat & "'"

		  Dim cnn As New SqlConnection(strCnn)
		  cnn.Open()
		  Dim cmd As New SqlClient.SqlCommand(strSQL, cnn)
		  'cmd.Parameters.Add(New SqlParameter("@ProductID", productID))

		  Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
      If dr.HasRows Then
        dr.Read()
        result = dr("result")
      End If
      cnn.Close()

      Return result
    End Function

    'PEGA NOTAS QUE DEVEM SER MANIFESTADAS
    <WebMethod()> _
    Public Function Pega_Notas_Manifesta(ByVal id_empresa As String, Chave_Emissor As String, tpEvento As String) As String
      'Atualiza último NSU consultado pela empresa
      Dim result As String = ""
      Dim cod_result As String = ""
      Dim strCnn As String = GetConnectionString("maxcont_cloud")
		  Dim strSQL As String = "EXEC spWS_NFe_Consulta_Dest_Pega_Manifesto '" & id_empresa & "','" & Chave_Emissor & "','0'"

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
      
      If cod_result = "0" THEN 'falha no acesso
        Return "0"
      ElseIf cod_result = "1" THEN 'validou acesso
        Dim chNFe As String
        chNFe = ""

        Dim strCnn2 As String = GetConnectionString("maxcont_cloud")
		    Dim cnn2 As New SqlConnection(strCnn2)

		    Dim command As SqlCommand = New SqlCommand ("EXEC spWS_NFe_Consulta_Dest_Pega_Manifesto '" & id_empresa & "','" & Chave_Emissor & "','1','" & tpEvento & "'",cnn2)

		    cnn2.Open()
		    Dim reader As SqlDataReader = command.ExecuteReader()

        If reader.HasRows Then
          Dim XML_Document As New XmlDocument()
          Dim XML_Raiz = XML_Document.CreateElement("lote")
          XML_Document.AppendChild(XML_Raiz)

          'Return strSQL
          Dim XML_Filho_conteudo = XML_Document.CreateElement("NFE")
          Dim XML_Filho_conteudo2 = XML_Document.CreateElement("NFE")
          Dim XML_Filho_conteudo3 = XML_Document.CreateElement("NFE")

          Do While reader.Read()
            XML_Filho_conteudo = XML_Document.CreateElement("NFE")
            XML_Raiz.AppendChild(XML_Filho_conteudo)

            XML_Filho_conteudo2 = XML_Document.CreateElement("chNFe")
            XML_Filho_conteudo2.InnerText = reader.GetString(0)
            XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

            XML_Filho_conteudo3 = XML_Document.CreateElement("tpEvento")
            XML_Filho_conteudo3.InnerText = reader.GetString(1)
            XML_Filho_conteudo.AppendChild(XML_Filho_conteudo3)

            XML_Filho_conteudo2 = XML_Document.CreateElement("xJust")
            XML_Filho_conteudo2.InnerText = reader.GetString(2)
            XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)
          Loop
          cnn.Close()

          'Dim Document_XML_2 As New XmlDocument()

          'Document_XML_2.LoadXml(XML_Document.OuterXml)

          ''verifica existência e cria diretórios
          'Dim CurrentPath As String = Server.MapPath("..\..\temp")

          'If Not Directory.Exists(CurrentPath) Then
          '  Directory.CreateDirectory(CurrentPath)
          'End If

          'Document_XML_2.Save(CurrentPath & "\xml_manifesta.xml")

          Return XML_Document.OuterXml
        else 'não localizou registros
          Return "99"
          cnn.Close()
        End If
      Else 'falha no acesso
        Return "0"
      End If

    End Function



    'PEGA NOTAS QUE DEVEM TER O DOWNLOAD REALIZADO
    <WebMethod()> _
    Public Function Pega_Notas_Download(ByVal id_empresa As String, Chave_Emissor As String) As String
      'Atualiza último NSU consultado pela empresa
      Dim result As String = ""
      Dim cod_result As String = ""
      Dim strCnn As String = GetConnectionString("maxcont_cloud")

      'valida acesso
		  Dim strSQL As String = "EXEC spWS_NFe_Consulta_Dest_Pega_Manifesto '" & id_empresa & "','" & Chave_Emissor & "','0'"

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
      
      Dim XML_Document As New XmlDocument()
      Dim chNFe As String
      chNFe = ""
      If cod_result = "1" THEN

        Dim XML_Raiz = XML_Document.CreateElement("lote")
        XML_Document.AppendChild(XML_Raiz)

        'Return strSQL
        Dim XML_Filho_conteudo = XML_Document.CreateElement("NFE")
        Dim XML_Filho_conteudo2 = XML_Document.CreateElement("NFE")
        Dim XML_Filho_conteudo3 = XML_Document.CreateElement("NFE")

        Dim strCnn2 As String = GetConnectionString("maxcont_cloud")
		    Dim cnn2 As New SqlConnection(strCnn2)

		    Dim command As SqlCommand = New SqlCommand ("EXEC spWS_NFe_Consulta_Dest_Pega_Manifesto '" & id_empresa & "','" & Chave_Emissor & "','2'",cnn2)

		    cnn2.Open()
		    Dim reader As SqlDataReader = command.ExecuteReader()

        If reader.HasRows Then
          Do While reader.Read()
            XML_Filho_conteudo = XML_Document.CreateElement("NFE")
            XML_Raiz.AppendChild(XML_Filho_conteudo)

            XML_Filho_conteudo2 = XML_Document.CreateElement("chNFe")
            XML_Filho_conteudo2.InnerText = reader.GetString(0)
            XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

            XML_Filho_conteudo2 = XML_Document.CreateElement("tpDocumento")
            XML_Filho_conteudo2.InnerText = reader.GetString(1)
            XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)
          Loop
        End If
        cnn.Close()

		    ''strSQL = "EXEC spWS_NFe_Consulta_Dest_Pega_Manifesto '" & id_empresa & "','" & Chave_Emissor & "','VALIDA'"

		    ' ''Dim cnn As New SqlConnection(strCnn)
		    ''cnn.Open()
		    ' ''cmd.Parameters.Add(New SqlParameter("@ProductID", productID))
      ''  If dr.HasRows Then
      ''    dr.Read()
      ''    result = dr("result")
      ''  End If
      ''  cnn.Close()
      End If

      Return XML_Document.OuterXml
    End Function

  <WebMethod()> Public Function Recebe_Notas(ByVal nota As String, NSU As String, id_empresa As String, Chave_Emissor As String) As String
    'recebe retorno do XML da Fazenda e consulta informações de notas que foram localizadas
    Dim resultado As String = ""

    Dim Tag_Atual As String = ""

    Dim Document_XML As New XmlDocument()
    Dim result As String = ""
    Dim cod_result As String = ""

    'Dim XML_Reader As XmlTextReader = New XmlTextReader(nota)
    Dim output As StringBuilder = New StringBuilder()

    Using reader As XmlReader = XmlReader.Create(New StringReader(nota))
      Dim ws As XmlWriterSettings = New XmlWriterSettings()
      ws.Indent = True
      Using writer As XmlWriter = XmlWriter.Create(output, ws)

        ' Parse the file and display each of the nodes.
        While reader.Read()
          Select Case reader.NodeType
            Case XmlNodeType.Element
              writer.WriteStartElement(reader.Name)
              'If reader.Name = "ret" Then
              '  If resultado <> "" Then
              '    resultado = resultado & "{}"
              '  End If

              '  resultado = resultado & reader.Name
              'End If

              ' tags tratadas RESNFE ; RESCANC ; RESCCE
              If (reader.Name.ToUpper = "RESNFE") Or (reader.Name.ToUpper = "RESEVENTO") Or (reader.Name.ToUpper = "RESCANC") Or (reader.Name.ToUpper = "RESCCE") Then
                Tag_Atual = reader.Name
                resultado = resultado & "|" & reader.Name
                resultado = resultado & "|" & NSU 'reader.GetAttribute("NSU")
              End If

              'RESNFE e RESCANC - tags com mesmas informações
              If Tag_Atual.ToUpper = "RESNFE" Or Tag_Atual.ToUpper = "RESCANC" Then
                If reader.Name = "chNFe" Then
                  resultado = resultado & "|" & reader.ReadString()
                End If

                If reader.Name = "CNPJ" Then
                  resultado = resultado & "|" & reader.ReadString()
                End If
                If reader.Name = "xNome" Then
                  resultado = resultado & "|" & reader.ReadString()
                End If

                If reader.Name = "IE" Then
                  resultado = resultado & "|" & reader.ReadString()
                End If

                If reader.Name = "dhEmi" Then
                  resultado = resultado & "|" & reader.ReadString()
                End If

                If reader.Name = "tpNF" Then
                  resultado = resultado & "|" & reader.ReadString()
                End If

                If reader.Name = "vNF" Then
                  resultado = resultado & "|" & reader.ReadString()
                End If

                If reader.Name = "digVal" Then
                  resultado = resultado & "|" & reader.ReadString()
                End If

                If reader.Name = "dhRecbto" Then
                  resultado = resultado & "|" & reader.ReadString()
                End If

                If reader.Name = "cSitNFe" Then
                  resultado = resultado & "|" & reader.ReadString()
                End If

                If reader.Name = "cSitConf" Then
                  resultado = resultado & "|" & reader.ReadString()
                End If

              ElseIf (Tag_Atual.ToUpper = "RESCCE") Then
                If reader.Name = "chNFe" Then
                  resultado = resultado & "|" & reader.ReadString()
                End If

                'Eventos da nota fiscal
                If reader.Name = "dhEvento" Then
                  resultado = resultado & "|" & reader.ReadString()
                End If

                If reader.Name = "tpEvento" Then
                  resultado = resultado & "|" & reader.ReadString()
                End If

                If reader.Name = "nSeqEvento" Then
                  resultado = resultado & "|" & reader.ReadString()
                End If

                If reader.Name = "descEvento" Then
                  resultado = resultado & "|" & reader.ReadString()
                End If

                If reader.Name = "xCorrecao" Then
                  resultado = resultado & "|" & reader.ReadString()
                End If

                If reader.Name = "tpNF" Then
                  resultado = resultado & "|" & reader.ReadString()
                End If

                If reader.Name = "dhRecbto" Then
                  resultado = resultado & "|" & reader.ReadString()
                End If
              ElseIf (Tag_Atual.ToUpper = "RESEVENTO") Then
                If reader.Name = "chNFe" Then
                  resultado = resultado & "|" & reader.ReadString()
                End If

                'Eventos da nota fiscal
                If reader.Name = "dhEvento" Then
                  resultado = resultado & "|" & reader.ReadString()
                End If

                If reader.Name = "tpEvento" Then
                  resultado = resultado & "|" & reader.ReadString()
                End If

                If reader.Name = "nSeqEvento" Then
                  resultado = resultado & "|" & reader.ReadString()
                End If

                If reader.Name = "xEvento" Then
                  resultado = resultado & "|" & reader.ReadString()
                End If

                If reader.Name = "dhRecbto" Then
                  resultado = resultado & "|" & reader.ReadString()
                End If

                If reader.Name = "nProt" Then
                  resultado = resultado & "|" & reader.ReadString()
                End If
              End If
          End Select
        End While
        'If resultado <> "" Then resultado = resultado & "{}"
      End Using
    End Using
    resultado = resultado.Replace("'", "''") & "{}"

    Dim strSQL As String = ""

    If Tag_Atual.ToUpper = "RESNFE" Then


      Dim strCnn As String = GetConnectionString("maxcont_cloud")

      strSQL = "EXEC spWS_NFe_Consulta_Dest_Grava '" & id_empresa & "','" & resultado & "','" & Chave_Emissor & "'"

      Dim cnn As New SqlConnection(strCnn)
      cnn.Open()
      Dim cmd As New SqlClient.SqlCommand(strSQL, cnn)
      'cmd.Parameters.Add(New SqlParameter("@ProductID", productID))

      Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
      If dr.HasRows Then
        dr.Read()
        result = dr("result")
        cod_result = dr("cod_result")
      End If
      cnn.Close()
    End If
    Return "|" & cod_result & "|" & strSQL
  End Function

  <WebMethod()> _
    Public Function Recebe_Evento_Manifesto(ByVal nota As String, id_empresa As String, Chave_Emissor As String) As String  
      'recebe retorno do XML da Fazenda e consulta informações de notas que foram localizadas
      Dim resultado As String = ""

      Dim Tag_Atual As String = ""

      Dim Document_XML As New XmlDocument()
      Dim result As String = ""
      Dim cod_result As String = ""
      Dim xMotivo As String = ""
      Dim chNFe As String = ""
      Dim tpEvento As String = ""
      Dim xEvento As String = ""
      Dim nSeqEvento As String = ""
      Dim dhRegEvento As String = ""
      Dim nProt As String = ""

      Dim strSQL As String = "teste"

      'Dim XML_Reader As XmlTextReader = New XmlTextReader(nota)
      Dim output As StringBuilder = New StringBuilder()

      Using reader As XmlReader = XmlReader.Create(New StringReader(nota))
        Dim ws As XmlWriterSettings = New XmlWriterSettings()
        ws.Indent = True
        Using writer As XmlWriter = XmlWriter.Create(output, ws)

          ' Parse the file and display each of the nodes.
          While reader.Read()
            Select Case reader.NodeType
              Case XmlNodeType.Element
                writer.WriteStartElement(reader.Name)
                If reader.Name = "ret" then
                  If resultado <> "" then 
                    resultado = resultado & "{}"
                  end if

                  resultado = resultado & reader.Name
                end if

                If reader.Name = "xMotivo" then
                  xMotivo = reader.ReadString()
                end if

                If reader.Name = "chNFe" then
                  chNFe = reader.ReadString()
                end If

                If reader.Name = "tpEvento" then
                  tpEvento = reader.ReadString()
                end if    

                If reader.Name = "xEvento" then
                  xEvento = reader.ReadString()
                end if    

                If reader.Name = "nSeqEvento" then
                  nSeqEvento = reader.ReadString()
                end if    

                If reader.Name = "dhRegEvento" then
                  dhRegEvento = reader.ReadString()
                end if    

                If reader.Name = "nProt" then
                  nProt = reader.ReadString()
                end if    

              End Select
            End While
          If resultado <> "" then resultado = resultado & "{}"

          Dim strCnn As String = GetConnectionString("maxcont_cloud")


	        strSQL = "EXEC spWS_NFe_Recebe_Evento_Manifesto '" & id_empresa & "','" & Chave_Emissor & "','" & _
                                 chNFe & "','" & xMotivo & "','" & tpEvento & "','" & xEvento & "','" & nSeqEvento & "','" & dhRegEvento & "','" & nProt & "'"

	        Dim cnn As New SqlConnection(strCnn)
	        cnn.Open()
	        Dim cmd As New SqlClient.SqlCommand(strSQL, cnn)
	        'cmd.Parameters.Add(New SqlParameter("@ProductID", productID))

	        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
          If dr.HasRows Then
            dr.Read()
            result = dr("result")
            cod_result = dr("cod_result")
          End If
          cnn.Close()
        End Using
      End Using

      Return cod_result & "|" & result
    End Function

    <WebMethod()> _
    Public Function Grava_XML(ByVal nota As String, id_empresa As String, chaveNFe As String, tpDocumento As String, Chave_Emissor As String, nome_arquivo As String) As String 
      'Recebe arquivo XML e grava na pasta compras

      Dim result As String = ""
      Dim cod_result As String = ""

      Dim strCnn As String = GetConnectionString("maxcont_cloud")

		  Dim strSQL As String = "EXEC spWS_NFe_XML_Atualiza '" & id_empresa & "','" & Chave_Emissor & "','CONSULTA'"

		  Dim cnn As New SqlConnection(strCnn)
		  cnn.Open()
		  Dim cmd As New SqlClient.SqlCommand(strSQL, cnn)
		  'cmd.Parameters.Add(New SqlParameter("@ProductID", productID))

		  Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
      If dr.HasRows Then
        dr.Read()
        result = dr("result")
        cod_result = dr("cod_result")
      End If
      cnn.Close()

      'não passou pela validação
      If cod_result = 0 then
        Return cod_result & "|" & result
      else 'passou pela validação, grava arquivo xml

        'Dim XML_Reader As XmlTextReader = New XmlTextReader(nota)
        Dim output As StringBuilder = New StringBuilder()

        Using reader As XmlReader = XmlReader.Create(New StringReader(nota))
          Dim ws As XmlWriterSettings = New XmlWriterSettings()
          ws.Indent = True
          Using writer As XmlWriter = XmlWriter.Create(output, ws)
          End Using
        End Using

        Dim resultado As String = ""

        Dim Document_XML As New XmlDocument()

        Document_XML.LoadXml(nota)

        'verifica existência e cria diretórios
        Dim CurrentPath As String = Server.MapPath("..\..\Compras\docs\" & id_empresa)

        If Not Directory.Exists(CurrentPath) Then
          Directory.CreateDirectory(CurrentPath)
        End If

        CurrentPath = CurrentPath & "\" & Date.Now.ToString("yyyyMM")

        If Not Directory.Exists(CurrentPath) Then
          Directory.CreateDirectory(CurrentPath)
        End If

        CurrentPath = CurrentPath & "\DOWN"

        If Not Directory.Exists(CurrentPath) Then
          Directory.CreateDirectory(CurrentPath)
        End If

        Document_XML.Save(CurrentPath & "\" & nome_arquivo)

		    strSQL = "EXEC spWS_NFe_XML_Atualiza '" & id_empresa & "','" & Chave_Emissor & "','GRAVAR','" & CurrentPath & "\" & nome_arquivo & "','" & chaveNFe & "','" & tpDocumento & "'"

		    Dim cnn2 As New SqlConnection(strCnn)
		    Dim cmd2 As New SqlClient.SqlCommand(strSQL, cnn2)
		    cnn2.Open()

        Dim dr2 As SqlDataReader = cmd2.ExecuteReader(CommandBehavior.CloseConnection)
        If dr2.HasRows Then
          dr2.Read()
          result = dr2("result")
          cod_result = dr2("cod_result")
        End If
        cnn2.Close()

        'Return "EXEC spWS_NFe_XML_Atualiza '" & id_empresa & "','" & Chave_Emissor & "','GRAVAR',' " & CurrentPath & "\" & nome_arquivo & "','" & chaveNFe & "','" & tpDocumento & "'"
        Return cod_result & "|" & result
        'Return strSQL
      End If


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
