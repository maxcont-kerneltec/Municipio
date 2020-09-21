Imports Microsoft.VisualBasic
Imports clsEnum
Imports System
Imports System.Web
Imports System.IO

Public Class clsAjuste
  Private _xMotivo As String
  
  Property xMotivo() As String
    Get
	  Return _xMotivo
	End Get
	Set(value As String)
	  _xMotivo = value
	End Set
  End Property


  Public Function FormataData(ByVal data As String, ByVal tipo As TipoData) As String
    Dim retorno As String = ""
    Dim dia As String
    Dim mes As String
    Dim ano As String

    If tipo = Global.TipoData.DDMMAAAA Then
      data = data.Substring(0, 10) 'pega os 10 primeiros caracteres

      dia = data.Substring(8, 2)
      ano = data.Substring(0, 4)

      data = data.Substring(5, 5)

      mes = data.Substring(0, 2)

      retorno = dia + "/" + mes + "/" + ano

    End If

    Return retorno
  End Function

  Public Function AMD(ByVal strData As String) As String

    Dim pos1 = InStr(1, strData, "/")
    Dim pos2 = InStr(pos1 + 1, strData, "/")
    Dim retorno, ano, mes, dia As String

    If (pos1 > 0) And (pos2 > pos1) Then
      ano = Mid(strData, pos2 + 1, 4)

      If Len(ano) = 2 Then
        ano = "20" & ano
      End If

      mes = Mid(strData, pos1 + 1, pos2 - pos1 - 1)
      dia = Left(strData, pos1 - 1)
      retorno = ano & "-" & Right("00" & mes, 2) & "-" & Right("00" & dia, 2)

      If Not IsDate(retorno) Then
        retorno = ""
      End If
    Else
      retorno = ""
    End If

    Return retorno

  End Function

  Public Function FormataCPF(ByVal cpf As String, ByVal tipo As FormataCpf) As String
    If cpf = "" Then
      Return cpf
    End If

    If tipo = Global.FormataCpf.comCaracter Then
      If cpf.Length = 11 Then
        cpf = cpf.Substring(0, 3) & "." & cpf.Substring(3, 3) & "." & cpf.Substring(6, 3) & "-" & cpf.Substring(9, 2)
      End If

    Else
      cpf = cpf.Replace(".", "").Replace("-", "")
    End If

    Return cpf

  End Function

  Public Function FormataCNPJ(ByVal cnpj As String, ByVal tipo As FormataCnpj) As String
    If cnpj = "" Then
      Return cnpj
    End If

    If tipo = Global.FormataCnpj.comCaracter Then
      If cnpj.Length = 14 Then
        cnpj = cnpj.Substring(0, 2) & "." & cnpj.Substring(2, 3) & "." & cnpj.Substring(5, 3) & "/" & cnpj.Substring(8, 4) & "-" & cnpj.Substring(12, 2)
      End If
    Else
      cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "")
    End If

    Return cnpj
  End Function

  Public Function PegaDataAtual(ByVal tipo As TipoData) As String
    Dim data As String = ""

    If tipo = TipoData.AAAAMMDD Then
      data = Year(Date.Now()) & "/" & Right("00" & Month(Date.Now()), 2) & "/" & Right("00" & Day(Date.Now()), 2)
    Else
      data = Right("00" & Day(Date.Now()), 2) & "/" & Right("00" & Month(Date.Now()), 2) & "/" & Year(Date.Now())
    End If

    Return data

  End Function

  Public Function FormataDataNFe(ByVal data As String) As String
    Dim data_formatada() As String

    If data = "" Then data = FormatDateTime(Date.Now(), 2)
    data_formatada = data.Split("/")

    If Len(data_formatada(2)) = 4 Then
      data = data_formatada(2) & "-" & Right("00" & data_formatada(1), 2) & "-" & Right("00" & data_formatada(0), 2)
    Else
      data = Year(Date.Now()) & "-" & Right("00" & data_formatada(1), 2) & "-" & Right("00" & data_formatada(0), 2)
    End If

    Return data

  End Function


  Public Function Formata_Num(ByVal data As String, ByVal Casa As Integer) As String
    If data = "" Then
      Return data
    End If

    If Len(data) <> Casa Then
      data = data.PadLeft(Casa, "0")
    End If

    Return data

  End Function

  Public Function CriaPastaXmlNFe(ByVal caminho_salva_xml As String, ByVal mes As String, ByVal ano As Integer) As String
    'Dim caminho_salva_xml As String = "C:\MaxcontNfe\XML_transmitidas\"
    Dim aaaamm As String = ano & Right("00" & mes, 2)

    If Not Directory.Exists(caminho_salva_xml & aaaamm) Then
      Try
        Directory.CreateDirectory(caminho_salva_xml & aaaamm)
      Catch ex As Exception
        Me.xMotivo = "ERRO AO CRIAR PASTA DE XMLS:" & ex.Message() & "-------" & ex.StackTrace()
      End Try
    End If

    Return caminho_salva_xml & aaaamm
  End Function

  Public Function CriaPastaNFeCartaCorrecao(ByVal ano As Integer, ByVal mes As String) As String
    Dim caminho_salva_xml As String = "C:\MaxcontNfe\XML_eventos\"
    Dim aaaamm As String = ano & Right("00" & mes, 2)

    If Not System.IO.Directory.Exists(caminho_salva_xml & aaaamm) Then
      Try
        System.IO.Directory.CreateDirectory(caminho_salva_xml & aaaamm)
      Catch ex As Exception
        MsgBox("ERRO AO CRIAR PASTA DE CONSULTA DO XML:" & ex.Message() & "-------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
      End Try
    End If

    Return caminho_salva_xml & aaaamm
  End Function

  Public Function CriaPastaXmlNfeConsulta(ByVal ano As Integer, ByVal mes As String) As String
    Dim caminho_salva_xml As String = "C:\MaxcontNfe\XML_consulta\"
    Dim aaaamm As String = ano & Right("00" & mes, 2)

    If Not System.IO.Directory.Exists(caminho_salva_xml & aaaamm) Then
      Try
        System.IO.Directory.CreateDirectory(caminho_salva_xml & aaaamm)
      Catch ex As Exception
        MsgBox("ERRO AO CRIAR PASTA DE CONSULTA DO XML:" & ex.Message() & "-------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
      End Try
    End If

    Return caminho_salva_xml & aaaamm
  End Function

  ''' <summary>
  ''' Pega o início do caminho que será salvo o XML das notas fiscais...
  ''' *******O caminho vem sem o ano e mês...
  ''' </summary>
  ''' <param name="id_empresa"></param>
  ''' <returns></returns>
  Public Function GetFilePathSaveNFe(ByVal id_empresa As Integer) As String
    Return HttpContext.Current.Server.MapPath("/Faturamento/Docs/" & Convert.ToString(id_empresa) & "/")
  End Function

  Public Function GetFilePathFaturamentoSaveNFe(ByVal id_empresa As Integer) As String
    Return HttpContext.Current.Server.MapPath("../../max_2/Faturamento/Docs/" & Convert.ToString(id_empresa) & "/")
  End Function

  Public Function GetFilePathTemp() As String
    Return HttpContext.Current.Server.MapPath("/temp/")
  End Function


  Public Function GetFilePathTempEmpresa(ByVal id_empresa As Integer) As String
    'Dim Caminho_Temp As String = HttpContext.Current.Server.MapPath("../../max_2/Temp/" & Convert.ToString(id_empresa) & "/")
    Dim Caminho_Temp As String = HttpContext.Current.Server.MapPath("../../max_2/Faturamento/docs/Temp/")

    If Not Directory.Exists(Caminho_Temp) Then
      Try
        Directory.CreateDirectory(Caminho_Temp)
      Catch ex As Exception
        Me.xMotivo = "ERRO AO CRIAR PASTA DE XMLS:" & ex.Message() & "-------" & ex.StackTrace()
      End Try
    End If

    Caminho_Temp = HttpContext.Current.Server.MapPath("../../max_2/Faturamento/docs/Temp/" & Convert.ToString(id_empresa) & "/")

    If Not Directory.Exists(Caminho_Temp) Then
      Try
        Directory.CreateDirectory(Caminho_Temp)
      Catch ex As Exception
        Me.xMotivo = "ERRO AO CRIAR PASTA DE XMLS:" & ex.Message() & "-------" & ex.StackTrace()
      End Try
    End If


    Return Caminho_Temp
  End Function

  Public Function PegaTimeZone() As String
    Dim time_zone As String
    'https://pt.stackoverflow.com/questions/253572/datetime-utc-hor%C3%A1rio-de-ver%C3%A2o-nf-e
    'time_zone = TimeZoneInfo.Local.DisplayName
    'time_zone = Left(time_zone, 10)
    'time_zone = Right(time_zone, 6)
    time_zone = Right(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"), 6)

    Return time_zone
  End Function

  Public Sub SalvaErroLog(ByVal id_empresa As Integer, ByVal id_nf As Integer, ByVal erro As String)
    Dim caminho_salva As String = GetFilePathTemp() & "logErroNFe_" & id_empresa & "_" & id_nf & ".txt"

    Using sw As StreamWriter = File.CreateText(caminho_salva)
      sw.WriteLine("Data: " + DateTime.Now)
      sw.WriteLine("Erro: " + erro)

      sw.Close()
    End Using
  End Sub


End Class
