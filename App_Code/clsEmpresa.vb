Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.Xml

Public Class clsEmpresa
  Private _xMotivo As String

  Property xMotivo() As String
    Get
      Return _xMotivo
    End Get
    Set(value As String)
      _xMotivo = value
    End Set
  End Property

  Public Sub New()

  End Sub

  Public Function PegaUltimoNSU(ByVal id_empresa As Integer) As Integer
    Dim NSU As Integer = 0
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    str_builder.Append("SELECT ISNULL(ultNSUConsultado, 0) FROM tbEmpresas WHERE (id_empresa = " & id_empresa & ")")

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        NSU = dr(0)
      Loop
    Catch ex As Exception
      _xMotivo = "ERRO AO PEGAR O NÚMERO DA NSU: " & ex.Message() & "---------" & ex.StackTrace()
    End Try

    Return NSU
  End Function

  ''' <summary>
  ''' Gera um xml com as informações que serão utilizadas na emissor Maxcont...
  ''' </summary>
  Public Function PegaInfEmpresa(ByVal id_empresa As Integer, ByVal status_login As Integer) As XmlElement
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    Dim doc As New XmlDocument()
    Dim ListaEmpresas As XmlElement
    Dim DadosEmpresa, cStat_xml, xMotivo, id_empresa_str, descr_empresa_xml, cnpj_emp_xml, cmun_xml, uf_xml As XmlElement
    Dim sts_bloq_xml, cUF_xml, fNFe_Dest_xml As XmlElement

    Dim razao_emp, cnpj_emp, UF, sts_bloq, fNFe_Dest As String
    Dim cmun As Integer = 0
    Dim cUF As Integer = 0

    str_builder.Append("SELECT A.razao_emp, A.cnpj_emp,  A.cmun, A.UF, A.sts_bloq, ")
    str_builder.Append("CASE A.fNFe_Dest WHEN 0 THEN 'N' ELSE 'S'	END AS fNFe_Dest, ")
    str_builder.Append("(SELECT Z.cUF FROM NFE_UF AS Z WHERE (Z.UF = A.uf)) AS cUF ")
    str_builder.Append("FROM tbEmpresas AS A ")
    str_builder.Append("WHERE (A.id_empresa = " & id_empresa & ") ")

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        razao_emp = dr(0)
        cnpj_emp = dr(1)
        cmun = dr(2)
        UF = dr(3)
        sts_bloq = dr(4)
        fNFe_Dest = dr(5)
        cUF = dr(6)
      Loop

      dr.Close()

      ListaEmpresas = doc.CreateElement("ListaEmpresas")
      doc.AppendChild(ListaEmpresas)

      DadosEmpresa = doc.CreateElement("DadosEmpresa")
      ListaEmpresas.AppendChild(DadosEmpresa)

      cStat_xml = doc.CreateElement("status")
      cStat_xml.InnerText = status_login
      DadosEmpresa.AppendChild(cStat_xml)


      If status_login = 0 Then 'Dados corretos...
        xMotivo = doc.CreateElement("xMotivo")
        xMotivo.InnerText = "Login realizado com sucesso!"
        DadosEmpresa.AppendChild(xMotivo)
      ElseIf status_login = 1 Then 'EMPRESA INVÁLIDA
        xMotivo = doc.CreateElement("xMotivo")
        xMotivo.InnerText = "Empresa inválida!"
        DadosEmpresa.AppendChild(xMotivo)
      Else
        xMotivo = doc.CreateElement("xMotivo")
        xMotivo.InnerText = "Usuário ou senha inválido!"
        DadosEmpresa.AppendChild(xMotivo)
      End If

      id_empresa_str = doc.CreateElement("id_empresa")
      id_empresa_str.InnerText = id_empresa
      DadosEmpresa.AppendChild(id_empresa_str)

      descr_empresa_xml = doc.CreateElement("descr_empresa")
      descr_empresa_xml.InnerText = razao_emp
      DadosEmpresa.AppendChild(descr_empresa_xml)

      cnpj_emp_xml = doc.CreateElement("cnpj_emp")
      cnpj_emp_xml.InnerText = cnpj_emp
      DadosEmpresa.AppendChild(cnpj_emp_xml)

      cmun_xml = doc.CreateElement("cmun")
      cmun_xml.InnerText = cmun
      DadosEmpresa.AppendChild(cmun_xml)

      uf_xml = doc.CreateElement("uf")
      uf_xml.InnerText = UF
      DadosEmpresa.AppendChild(uf_xml)

      sts_bloq_xml = doc.CreateElement("sts_bloq")
      sts_bloq_xml.InnerText = sts_bloq
      DadosEmpresa.AppendChild(sts_bloq_xml)

      cUF_xml = doc.CreateElement("cUF")
      cUF_xml.InnerText = cUF
      DadosEmpresa.AppendChild(cUF_xml)

      fNFe_Dest_xml = doc.CreateElement("acesso_manifesto")
      fNFe_Dest_xml.InnerText = fNFe_Dest
      DadosEmpresa.AppendChild(fNFe_Dest_xml)

    Catch ex As Exception
      _xMotivo = "ERRO AO PEGAR AS INFORMAÇÕES DA EMPRESA: " & ex.Message() & "------" & ex.StackTrace()
    End Try

    Return doc.DocumentElement
  End Function

  Public Function PegaUltimaVersao(ByVal id_empresa As Integer) As String
    Dim versao As String = ""
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    str_builder.Append("SELECT ISNULL(versao_emissor_NFe_maxcont, '') FROM tbEmpresas_Config WHERE (id_empresa = " & id_empresa & ")")

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        versao = dr(0)
      Loop

      dr.Close()
    Catch ex As Exception
      _xMotivo = "ERRO AO PEGAR A ÚLTIMA VERSÃO DO SISTEMA: " & ex.Message() & "--------" & ex.StackTrace()
    End Try

    Return versao
  End Function


End Class
