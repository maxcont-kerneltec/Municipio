Imports Microsoft.VisualBasic
Imports System.Xml
Imports System.Data
Imports System
Imports System.Data.SqlClient
Imports System.Text

Public Class clsCriaXmlNotaServico
  Private _caminho_pasta, _xMotivo As String
  Private _id_empresa, _id_lote As Integer
  Private _dr As SqlDataReader

  Property id_empresa() As Integer
    Get
      Return _id_empresa
    End Get
    Set(value As Integer)
      _id_empresa = value
    End Set
  End Property

  Property id_lote() As Integer
    Get
      Return _id_lote
    End Get
    Set(value As Integer)
      _id_lote = value
    End Set
  End Property

  Property xMotivo() As String
    Get
      Return _xMotivo
    End Get
    Set(value As String)
      _xMotivo = value
    End Set
  End Property

  Property caminho_pasta() As String
    Get
      Return _caminho_pasta
    End Get
    Set(value As String)
      _caminho_pasta = value
    End Set
  End Property

  Property dr() As SqlDataReader
    Get
      Return _dr
    End Get
    Set(value As SqlDataReader)
      _dr = value
    End Set
  End Property


  Public Sub New()

  End Sub

  Public Function CriaXml(ByVal id_empresa As Integer, ByVal id_lote As Integer, ByVal caminho_salva_xml As String) As Boolean
    Me.id_empresa = id_empresa
    Me.id_lote = id_lote

    Me.caminho_pasta = caminho_salva_xml & "\lote_" & Convert.ToString(id_lote) & ".xml"

    Try
      Using writer As New XmlTextWriter(caminho_pasta, System.Text.Encoding.UTF8)
        MontagemXML(writer)
      End Using
    Catch ex As Exception

      Return False
    End Try

    Return True

  End Function

  Private Sub MontagemXML(ByVal writer As XmlTextWriter)
    Dim str_builder As New StringBuilder
    Dim conexao As New clsConexao

    str_builder.Append("EXEC sp9_Pega_NFE_Servico_Lote '" & Me.id_empresa & "','" & Me.id_lote & "'")

    Try
      Me.dr = conexao.RetornaDataReader(str_builder.ToString())
    Catch ex As Exception
      Me.xMotivo = "FALHA NA CONEXÃO: " & ex.Message() & "-----------" & ex.StackTrace()
    End Try


    writer.WriteStartDocument(True) 'INICIO DOCUMENTO

    writer.WriteStartElement("loteNFSe")

    RPS(writer) 'Lista os RPS's do lote gerado...
    InfAdc(writer) 'Informações adicionais

    writer.WriteEndElement() 'FIM loteNFSe

    writer.WriteEndDocument() 'FIM DOCUMENTO

    writer.Flush()
    writer.Close()

    Me.dr.Close()

  End Sub


  Private Sub RPS(ByVal writer As XmlTextWriter)
    Try
      writer.WriteStartElement("listaRPS")

      Do While dr.Read()
        writer.WriteStartElement("RPS")

        writer.WriteStartElement("IM_prestador")
        writer.WriteString(dr(0))
        writer.WriteEndElement()

        writer.WriteStartElement("serie_RPS")
        writer.WriteString(dr(1))
        writer.WriteEndElement()

        writer.WriteStartElement("serv_num_RPS")
        writer.WriteString(dr(2))
        writer.WriteEndElement()

        writer.WriteStartElement("tipo_rps")
        writer.WriteString(dr(3))
        writer.WriteEndElement()

        writer.WriteStartElement("dEmi")
        writer.WriteString(dr(4))
        writer.WriteEndElement()

        writer.WriteStartElement("ISSQNtot_vServ")
        writer.WriteString(dr(5))
        writer.WriteEndElement()

        writer.WriteStartElement("ISSQNtot_vDeducoes")
        writer.WriteString(dr(6))
        writer.WriteEndElement()

        writer.WriteStartElement("retTrib_vRetPIS")
        writer.WriteString(dr(7))
        writer.WriteEndElement()

        writer.WriteStartElement("retTrib_vRetCOFINS")
        writer.WriteString(dr(8))
        writer.WriteEndElement()

        writer.WriteStartElement("retTrib_vRetPrev")
        writer.WriteString(dr(9))
        writer.WriteEndElement()

        writer.WriteStartElement("retTrib_vIRRF")
        writer.WriteString(dr(10))
        writer.WriteEndElement()

        writer.WriteStartElement("retTrib_vRetCSLL")
        writer.WriteString(dr(11))
        writer.WriteEndElement()

        writer.WriteStartElement("ISSQN_vAliq")
        writer.WriteString(dr(12))
        writer.WriteEndElement()

        writer.WriteStartElement("tipo_tomador")
        writer.WriteString(dr(13))
        writer.WriteEndElement()

        writer.WriteStartElement("cnpj_tomador")
        writer.WriteString(dr(14))
        writer.WriteEndElement()

        writer.WriteStartElement("IM_tomador")
        writer.WriteString(dr(15))
        writer.WriteEndElement()

        writer.WriteStartElement("IE_tomador")
        writer.WriteString(dr(16))
        writer.WriteEndElement()

        writer.WriteStartElement("xNome_tomador")
        writer.WriteString(dr(17))
        writer.WriteEndElement()

        writer.WriteStartElement("xLgr_tomador")
        writer.WriteString(dr(18))
        writer.WriteEndElement()

        writer.WriteStartElement("nro_tomador")
        writer.WriteString(dr(19))
        writer.WriteEndElement()

        writer.WriteStartElement("xCpl_tomador")
        writer.WriteString(dr(20))
        writer.WriteEndElement()

        writer.WriteStartElement("xBairro_tomador")
        writer.WriteString(dr(21))
        writer.WriteEndElement()

        writer.WriteStartElement("cMun_tomador")
        writer.WriteString(dr(22))
        writer.WriteEndElement()

        writer.WriteStartElement("UF_tomador")
        writer.WriteString(dr(23))
        writer.WriteEndElement()

        writer.WriteStartElement("CEP_tomador")
        writer.WriteString(dr(24))
        writer.WriteEndElement()

        writer.WriteStartElement("email_tomador")
        writer.WriteString(dr(25))
        writer.WriteEndElement()

        writer.WriteStartElement("descr_serv")
        writer.WriteString(dr(26))
        writer.WriteEndElement()

        writer.WriteStartElement("cod_serv")
        writer.WriteString(dr(27))
        writer.WriteEndElement()

        writer.WriteStartElement("ISSQN_cSitTrib")
        writer.WriteString(dr(28))
        writer.WriteEndElement()

        writer.WriteStartElement("cMunFG")
        writer.WriteString(dr(29))
        writer.WriteEndElement()

        writer.WriteStartElement("ISSQN_vISSQN")
        writer.WriteString(dr(30))
        writer.WriteEndElement()

        writer.WriteStartElement("vTotTrib")
        writer.WriteString(dr(31))
        writer.WriteEndElement()

        writer.WriteStartElement("qCom")
        writer.WriteString(dr(32))
        writer.WriteEndElement()

        writer.WriteStartElement("uCom")
        writer.WriteString(dr(33))
        writer.WriteEndElement()

        writer.WriteStartElement("vUnCom")
        writer.WriteString(dr(34))
        writer.WriteEndElement()

        writer.WriteStartElement("inf_pagamento")
        writer.WriteString(Trim(dr(35)))
        writer.WriteEndElement()

        writer.WriteStartElement("indPag")
        writer.WriteString(dr(36))
        writer.WriteEndElement()

        writer.WriteStartElement("conta_dup")
        writer.WriteString(dr(37))
        writer.WriteEndElement()

        writer.WriteStartElement("infCpl")
        writer.WriteString(dr(38))
        writer.WriteEndElement()

        writer.WriteStartElement("cPais_tomador")
        writer.WriteString(dr(39))
        writer.WriteEndElement()

        writer.WriteStartElement("xMun_tomador")
        writer.WriteString(dr(40))
        writer.WriteEndElement()

        writer.WriteStartElement("cob_vOrig")
        writer.WriteString(dr(41))
        writer.WriteEndElement()

        writer.WriteStartElement("xPais_tomador")
        writer.WriteString(dr(42))
        writer.WriteEndElement()

        writer.WriteStartElement("xMunFG")
        writer.WriteString(dr(43))
        writer.WriteEndElement()

        writer.WriteStartElement("ISSQN_vAliq")
        writer.WriteString(dr(44))
        writer.WriteEndElement()

        writer.WriteStartElement("cnpj_tomador_fmt")
        writer.WriteString(dr(45))
        writer.WriteEndElement()

        writer.WriteStartElement("cnpj_Prestador")
        writer.WriteString(dr(46))
        writer.WriteEndElement()

        writer.WriteStartElement("fone_tomador")
        writer.WriteString(dr(47))
        writer.WriteEndElement()

        writer.WriteStartElement("vlr_liquido_NFSe")
        writer.WriteString(dr(48))
        writer.WriteEndElement()

        writer.WriteStartElement("descr_cond_pag")
        writer.WriteString(dr(49))
        writer.WriteEndElement()

        writer.WriteStartElement("cob_nFat")
        writer.WriteString(dr(50))
        writer.WriteEndElement()

        writer.WriteStartElement("IM_eventual")
        writer.WriteString(dr(51))
        writer.WriteEndElement()

        writer.WriteStartElement("retTrib_vISS")
        writer.WriteString(dr(52))
        writer.WriteEndElement()

        writer.WriteStartElement("aliq_vTrib")
        writer.WriteString(dr(53))
        writer.WriteEndElement()

        writer.WriteStartElement("vTotTrib")
        writer.WriteString(dr(54))
        writer.WriteEndElement()

        writer.WriteStartElement("fonte_tribut")
        writer.WriteString(dr(55))
        writer.WriteEndElement()

        writer.WriteStartElement("retTrib_vRetPIS")
        writer.WriteString(dr(56))
        writer.WriteEndElement()

        writer.WriteStartElement("retTrib_vRetCOFINS")
        writer.WriteString(dr(57))
        writer.WriteEndElement()

        writer.WriteStartElement("retTrib_vRetPrev")
        writer.WriteString(dr(58))
        writer.WriteEndElement()

        writer.WriteStartElement("retTrib_vIRRF")
        writer.WriteString(dr(59))
        writer.WriteEndElement()

        writer.WriteStartElement("retTrib_vRetCSLL")
        writer.WriteString(dr(60))
        writer.WriteEndElement()

        writer.WriteStartElement("dEmi")
        writer.WriteString(dr(61))
        writer.WriteEndElement()

        writer.WriteStartElement("cMun_SIAFI_tomador")
        writer.WriteString(dr(62))
        writer.WriteEndElement()

        writer.WriteStartElement("IRRF_pIRRF")
        writer.WriteString(dr(63))
        writer.WriteEndElement()

        writer.WriteStartElement("PIS_pPIS")
        writer.WriteString(dr(64))
        writer.WriteEndElement()

        writer.WriteStartElement("COFINS_pCOFINS")
        writer.WriteString(dr(65))
        writer.WriteEndElement()

        writer.WriteStartElement("CSLL_pCSLL")
        writer.WriteString(dr(66))
        writer.WriteEndElement()

        writer.WriteStartElement("INSS_pINSS")
        writer.WriteString(dr(67))
        writer.WriteEndElement()

        writer.WriteStartElement("fone_prestador")
        writer.WriteString(dr(68))
        writer.WriteEndElement()

        writer.WriteStartElement("razao_prestador")
        writer.WriteString(dr(69))
        writer.WriteEndElement()

        writer.WriteStartElement("pAliq_Simples_ISS")
        writer.WriteString(dr(70))
        writer.WriteEndElement()

        writer.WriteStartElement("cMunFG_SIAFI")
        writer.WriteString(dr(71))
        writer.WriteEndElement()

        writer.WriteStartElement("xMunFG_SIAFI")
        writer.WriteString(dr(72))
        writer.WriteEndElement()

        writer.WriteStartElement("cnae_serv")
        writer.WriteString(dr(73))
        writer.WriteEndElement()

        writer.WriteStartElement("aliq_ISS_do_serv")
        writer.WriteString(dr(74))
        writer.WriteEndElement()


        writer.WriteEndElement() 'FIM RPS

      Loop

      writer.WriteEndElement()  'FIm listaRPS
    Catch ex As Exception
      Me.xMotivo = "ERRO AO GERAR O XML: " & ex.Message() & "--------" & ex.StackTrace()
    End Try

  End Sub

  Private Sub InfAdc(ByVal writer As XmlTextWriter)
    Me.dr.NextResult() 'Pega o próximo resultado da procedure

    writer.WriteStartElement("InfAdc")

    Do While dr.Read()
      writer.WriteStartElement("dt_ini")
      writer.WriteString(dr(0))
      writer.WriteEndElement()

      writer.WriteStartElement("dt_fim")
      writer.WriteString(dr(1))
      writer.WriteEndElement()

      writer.WriteStartElement("cMun_empresa")
      writer.WriteString(dr(2))
      writer.WriteEndElement()

      writer.WriteStartElement("IM_empresa")
      writer.WriteString(dr(3))
      writer.WriteEndElement()

      writer.WriteStartElement("CRT")
      writer.WriteString(dr(4))
      writer.WriteEndElement()

      writer.WriteStartElement("pAliq_Simples_ISS")
      writer.WriteString(dr(5))
      writer.WriteEndElement()
    Loop

    writer.WriteEndElement()  'FIm InfAdc

  End Sub


End Class
