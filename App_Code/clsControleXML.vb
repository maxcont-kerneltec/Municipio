Imports Microsoft.VisualBasic
Imports clsEnum
Imports System
Imports System.Web
Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Xml

Public Class clsControleXML

  Public Function SalvaXML(ByVal id_nf As Integer, ByVal tpEvento As Integer, ByVal arq_XML As String) As Boolean

    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_Salva_XML_NFe '" & id_nf & "','" & tpEvento & "','" & arq_XML & "'")

    Dim valid As Boolean = False

    Try

      conexao.ExecutaComando(str_builder.ToString())

      valid = True

    Catch ex As Exception

      valid = False

    End Try

    Return valid

  End Function

End Class
