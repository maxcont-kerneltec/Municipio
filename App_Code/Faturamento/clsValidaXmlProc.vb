Imports Microsoft.VisualBasic
Imports System.Xml
Imports System.IO

Public Class clsValidaXmlProc
  Private _msg_erro As String

  Public Sub New()

  End Sub

  Property msg_erro() As String
    Get
      Return _msg_erro
    End Get
    Set(value As String)
      _msg_erro = value
    End Set
  End Property


  Public Sub ValidarXml(ByVal caminho_xml As String)
    Dim doc As New XmlDocument
    Dim m_node As XmlNode
    Dim xml_element As XmlElement

    doc.Load(caminho_xml)
    xml_element = doc.DocumentElement

    Try
      For Each m_node In xml_element.GetElementsByTagName("NFe")
        'If Not (xml_element.HasAttribute("xmlns")) Then
        Dim newAttr As XmlAttribute
        newAttr = doc.CreateAttribute("xmlns")
        newAttr.Value = "http://www.portalfiscal.inf.br/nfe"

        'Dim sw As StreamWriter
        'sw = File.CreateText(caminho_xml)
        'sw.Write(m_node.OuterXml)
        'sw.Close()

        doc.Save(caminho_xml)
        'End If
      Next


    Catch ex As Exception
      _msg_erro = "ERRO AO LER O XML: " & ex.Message() & "---------" & ex.StackTrace()
    End Try


  End Sub
End Class
