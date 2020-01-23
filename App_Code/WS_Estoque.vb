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
Public Class WS_Estoque
  Inherits System.Web.Services.WebService

  'PEGA ITENS DE ESTOQUE
  <WebMethod()> _
  Public Function Pega_Itens_Estoque(ByVal id_empresa As String, cnpj As String) As String
    Dim result As String = ""
    Dim cod_result As String = ""
    Dim strCnn As String = GetConnectionString("maxcont_cloud")
    Dim strSQL As String = "EXEC spWS_Estoque_Pega_Itens '" & id_empresa & "','" & cnpj & "','VALIDAR'"

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

      Dim command As SqlCommand = New SqlCommand("EXEC spWS_Estoque_Pega_Itens '" & id_empresa & "','" & cnpj & "','CONSULTAR'", cnn2)

      cnn2.Open()
      Dim reader As SqlDataReader = command.ExecuteReader()

      txt_cabecalho = "id_empresa|"
      txt_cabecalho = txt_cabecalho & "id_item|"
      txt_cabecalho = txt_cabecalho & "cod_produto|"
      txt_cabecalho = txt_cabecalho & "descr|"
      txt_cabecalho = txt_cabecalho & "ICMS_orig|"
      txt_cabecalho = txt_cabecalho & "id_tipo|"
      txt_cabecalho = txt_cabecalho & "descr_tipo|"
      txt_cabecalho = txt_cabecalho & "id_grp|"
      txt_cabecalho = txt_cabecalho & "descr_grp|"
      txt_cabecalho = txt_cabecalho & "id_armazem|"
      txt_cabecalho = txt_cabecalho & "descr_armazem|"
      txt_cabecalho = txt_cabecalho & "EAN|"
      txt_cabecalho = txt_cabecalho & "und|"
      txt_cabecalho = txt_cabecalho & "und_com|"
      txt_cabecalho = txt_cabecalho & "classif_NCM|"
      txt_cabecalho = txt_cabecalho & "ipi|"
      txt_cabecalho = txt_cabecalho & "ICMS_ST_entrada|"
      txt_cabecalho = txt_cabecalho & "peso_liq|"
      txt_cabecalho = txt_cabecalho & "peso_bruto|"
      txt_cabecalho = txt_cabecalho & "preco_venda1|"
      txt_cabecalho = txt_cabecalho & "preco_venda2|"
      txt_cabecalho = txt_cabecalho & "preco_venda3|"
      txt_cabecalho = txt_cabecalho & "preco_venda4|"
      txt_cabecalho = txt_cabecalho & "CFOP|"
      txt_cabecalho = txt_cabecalho & "ICMS_CST|"
      txt_cabecalho = txt_cabecalho & "descr_ICMS_CST|"
      txt_cabecalho = txt_cabecalho & "ICMS_aliq|"
      txt_cabecalho = txt_cabecalho & "ipi_CST|"
      txt_cabecalho = txt_cabecalho & "descr_ipi_CST|"
      txt_cabecalho = txt_cabecalho & "pis_CST|"
      txt_cabecalho = txt_cabecalho & "descr_pis_CST|"
      txt_cabecalho = txt_cabecalho & "pPIS|"
      txt_cabecalho = txt_cabecalho & "COFINS_CST|"
      txt_cabecalho = txt_cabecalho & "descr_COFINS_CST|"
      txt_cabecalho = txt_cabecalho & "pCOFINS|"
      txt_cabecalho = txt_cabecalho & "aliq_vTotTrib|"
      txt_cabecalho = txt_cabecalho & "aliq_vTotTrib_fonte_tribut|"
      txt_cabecalho = txt_cabecalho & "CEST|"
      txt_cabecalho = txt_cabecalho & "cProdANP|{"

      If reader.HasRows Then
        Do While reader.Read()

          txt_cabecalho = txt_cabecalho & id_empresa & "|" 'id_empresa
          txt_cabecalho = txt_cabecalho & reader.GetString(0) & "|" 'id_item
          txt_cabecalho = txt_cabecalho & reader.GetString(1) & "|" 'cod_produto

          If reader.GetString(37) = "I" Then 'Item Inativo
            txt_cabecalho = txt_cabecalho & "INATIVO|" 'INATIVO
          Else
            txt_cabecalho = txt_cabecalho & reader.GetString(2) & "|" 'descr
          End If

          txt_cabecalho = txt_cabecalho & reader.GetString(3) & "|" 'ICMS_orig
          txt_cabecalho = txt_cabecalho & reader.GetString(4) & "|" 'id_tipo
          txt_cabecalho = txt_cabecalho & reader.GetString(5) & "|" 'descr_tipo
          txt_cabecalho = txt_cabecalho & reader.GetString(6) & "|" 'id_grp
          txt_cabecalho = txt_cabecalho & reader.GetString(7) & "|" 'descr_grp
          txt_cabecalho = txt_cabecalho & reader.GetString(8) & "|" 'id_armazem
          txt_cabecalho = txt_cabecalho & reader.GetString(9) & "|" 'descr_armazem
          If reader.GetString(10) = "" Then
            txt_cabecalho = txt_cabecalho & reader.GetString(10) & "0|" 'EAN
          Else
            txt_cabecalho = txt_cabecalho & reader.GetString(10) & "|" 'EAN
          End If

          txt_cabecalho = txt_cabecalho & reader.GetString(11) & "|" 'und
          txt_cabecalho = txt_cabecalho & reader.GetString(12) & "|" 'und_com
          txt_cabecalho = txt_cabecalho & reader.GetString(13) & "|" 'classif_NCM
          txt_cabecalho = txt_cabecalho & reader.GetString(14) & "|" 'ipi
          txt_cabecalho = txt_cabecalho & reader.GetString(15) & "|" 'ICMS_ST_entrada
          txt_cabecalho = txt_cabecalho & reader.GetString(16) & "|" 'peso_liq
          txt_cabecalho = txt_cabecalho & reader.GetString(17) & "|" 'peso_bruto
          txt_cabecalho = txt_cabecalho & reader.GetString(18) & "|" 'preco_venda1
          txt_cabecalho = txt_cabecalho & reader.GetString(19) & "|" 'preco_venda2
          txt_cabecalho = txt_cabecalho & reader.GetString(20) & "|" 'preco_venda3
          txt_cabecalho = txt_cabecalho & reader.GetString(21) & "|" 'preco_venda4
          txt_cabecalho = txt_cabecalho & reader.GetString(22) & "|" 'CFOP
          txt_cabecalho = txt_cabecalho & reader.GetString(23) & "|" 'ICMS_CST
          txt_cabecalho = txt_cabecalho & reader.GetString(24) & "|" 'descr_ICMS_CST
          txt_cabecalho = txt_cabecalho & reader.GetString(25) & "|" 'ICMS_aliq
          txt_cabecalho = txt_cabecalho & reader.GetString(26) & "|" 'ipi_CST
          txt_cabecalho = txt_cabecalho & reader.GetString(27) & "|" 'descr_ipi_CST
          txt_cabecalho = txt_cabecalho & reader.GetString(28) & "|" 'pis_CST
          txt_cabecalho = txt_cabecalho & reader.GetString(29) & "|" 'descr_pis_CST
          txt_cabecalho = txt_cabecalho & reader.GetString(30) & "|" 'pPIS
          txt_cabecalho = txt_cabecalho & reader.GetString(31) & "|" 'COFINS_CST
          txt_cabecalho = txt_cabecalho & reader.GetString(32) & "|" 'descr_COFINS_CST
          txt_cabecalho = txt_cabecalho & reader.GetString(33) & "|" 'pCOFINS
          txt_cabecalho = txt_cabecalho & reader.GetString(34) & "|" 'aliq_vTotTrib
          If reader.GetString(37) = "I" Then 'Item Inativo
            txt_cabecalho = txt_cabecalho & "INATIVO|" 'INATIVO
          ElseIf reader.GetString(37) = "S" Then 'Item serviço
            txt_cabecalho = txt_cabecalho & "SERVICO|" 'SERVIÇO
          ElseIf reader.GetString(37) = "E" Then 'Item Entrada
            txt_cabecalho = txt_cabecalho & "ENTRADA|" 'Entrada
          Else
            If reader.GetString(35) <> "" Then 'Tem fonte tributária preenchida
              txt_cabecalho = txt_cabecalho & reader.GetString(35) & "|" 'aliq_vTotTrib_fonte_tribut
            Else
              txt_cabecalho = txt_cabecalho & "IBPT|" 'aliq_vTotTrib_fonte_tribut
            End If
          End If

          If reader.GetString(38) = "" Then 'CEST
            txt_cabecalho = txt_cabecalho & "0|"
          Else
            txt_cabecalho = txt_cabecalho & reader.GetString(38) & "|"
          End If

          'Código ANP
          If reader.GetString(39) = "" Then
            txt_cabecalho = txt_cabecalho & "0|"
          Else
            txt_cabecalho = txt_cabecalho & reader.GetString(39) & "|"
          End If



          txt_cabecalho = txt_cabecalho & "{"

          'sw.WriteLine(txt_cabecalho)
        Loop
      End If
      cnn.Close()
    End If
    Return txt_cabecalho
  End Function


  'PEGA ITENS DE ESTOQUE E GRAVA ARQUIVO
  <WebMethod()> _
  Public Function Pega_Itens_Estoque_Arquivo(ByVal id_empresa As String, cnpj As String) As String
    Dim result As String = ""
    Dim cod_result As String = ""
    Dim strCnn As String = GetConnectionString("maxcont_cloud")
    Dim strSQL As String = "EXEC spWS_Estoque_Pega_Itens '" & id_empresa & "','" & cnpj & "','VALIDAR'"

    Dim path_download As String = ""

    Dim path As String = Server.MapPath("../../Temp/Produtos_WS/" & CStr(id_empresa) & ".txt")

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

      Dim command As SqlCommand = New SqlCommand("EXEC spWS_Estoque_Pega_Itens '" & id_empresa & "','" & cnpj & "','CONSULTAR'", cnn2)

      cnn2.Open()
      Dim reader As SqlDataReader = command.ExecuteReader()


      txt_cabecalho = "id_empresa|"
      txt_cabecalho = txt_cabecalho & "id_item|"
      txt_cabecalho = txt_cabecalho & "cod_produto|"
      txt_cabecalho = txt_cabecalho & "descr|"
      txt_cabecalho = txt_cabecalho & "ICMS_orig|"
      txt_cabecalho = txt_cabecalho & "id_tipo|"
      txt_cabecalho = txt_cabecalho & "descr_tipo|"
      txt_cabecalho = txt_cabecalho & "id_grp|"
      txt_cabecalho = txt_cabecalho & "descr_grp|"
      txt_cabecalho = txt_cabecalho & "id_armazem|"
      txt_cabecalho = txt_cabecalho & "descr_armazem|"
      txt_cabecalho = txt_cabecalho & "EAN|"
      txt_cabecalho = txt_cabecalho & "und|"
      txt_cabecalho = txt_cabecalho & "und_com|"
      txt_cabecalho = txt_cabecalho & "classif_NCM|"
      txt_cabecalho = txt_cabecalho & "ipi|"
      txt_cabecalho = txt_cabecalho & "ICMS_ST_entrada|"
      txt_cabecalho = txt_cabecalho & "peso_liq|"
      txt_cabecalho = txt_cabecalho & "peso_bruto|"
      txt_cabecalho = txt_cabecalho & "preco_venda1|"
      txt_cabecalho = txt_cabecalho & "preco_venda2|"
      txt_cabecalho = txt_cabecalho & "preco_venda3|"
      txt_cabecalho = txt_cabecalho & "preco_venda4|"
      txt_cabecalho = txt_cabecalho & "CFOP|"
      txt_cabecalho = txt_cabecalho & "ICMS_CST|"
      txt_cabecalho = txt_cabecalho & "descr_ICMS_CST|"
      txt_cabecalho = txt_cabecalho & "ICMS_aliq|"
      txt_cabecalho = txt_cabecalho & "ipi_CST|"
      txt_cabecalho = txt_cabecalho & "descr_ipi_CST|"
      txt_cabecalho = txt_cabecalho & "pis_CST|"
      txt_cabecalho = txt_cabecalho & "descr_pis_CST|"
      txt_cabecalho = txt_cabecalho & "pPIS|"
      txt_cabecalho = txt_cabecalho & "COFINS_CST|"
      txt_cabecalho = txt_cabecalho & "descr_COFINS_CST|"
      txt_cabecalho = txt_cabecalho & "pCOFINS|"
      txt_cabecalho = txt_cabecalho & "aliq_vTotTrib|"
      txt_cabecalho = txt_cabecalho & "CEST|"
      txt_cabecalho = txt_cabecalho & "cProdANP|{"

      If File.Exists(path) Then
        File.Delete(path)
      End If

      If Not File.Exists(path) Then
        ' Create a file to write to. 
        Using sw As StreamWriter = File.CreateText(path)
          sw.WriteLine(txt_cabecalho)
          If reader.HasRows Then
            path_download = "www.maxcont.com.br/Temp/Produtos_WS/" & CStr(id_empresa) & ".txt"
            Do While reader.Read()

              txt_cabecalho = id_empresa & "|" 'id_empresa
              txt_cabecalho = txt_cabecalho & reader.GetString(0) & "|" 'id_item
              txt_cabecalho = txt_cabecalho & reader.GetString(1) & "|" 'cod_produto
              txt_cabecalho = txt_cabecalho & reader.GetString(2) & "|" 'descr
              txt_cabecalho = txt_cabecalho & reader.GetString(3) & "|" 'ICMS_orig
              txt_cabecalho = txt_cabecalho & reader.GetString(4) & "|" 'id_tipo
              txt_cabecalho = txt_cabecalho & reader.GetString(5) & "|" 'descr_tipo
              txt_cabecalho = txt_cabecalho & reader.GetString(6) & "|" 'id_grp
              txt_cabecalho = txt_cabecalho & reader.GetString(7) & "|" 'descr_grp
              txt_cabecalho = txt_cabecalho & reader.GetString(8) & "|" 'id_armazem
              txt_cabecalho = txt_cabecalho & reader.GetString(9) & "|" 'descr_armazem
              If reader.GetString(10) = "" Then
                txt_cabecalho = txt_cabecalho & reader.GetString(10) & "0|" 'EAN
              Else
                txt_cabecalho = txt_cabecalho & reader.GetString(10) & "|" 'EAN
              End If

              txt_cabecalho = txt_cabecalho & reader.GetString(11) & "|" 'und
              txt_cabecalho = txt_cabecalho & reader.GetString(12) & "|" 'und_com
              txt_cabecalho = txt_cabecalho & reader.GetString(13) & "|" 'classif_NCM
              txt_cabecalho = txt_cabecalho & reader.GetString(14) & "|" 'ipi
              txt_cabecalho = txt_cabecalho & reader.GetString(15) & "|" 'ICMS_ST_entrada
              txt_cabecalho = txt_cabecalho & reader.GetString(16) & "|" 'peso_liq
              txt_cabecalho = txt_cabecalho & reader.GetString(17) & "|" 'peso_bruto
              txt_cabecalho = txt_cabecalho & reader.GetString(18) & "|" 'preco_venda1
              txt_cabecalho = txt_cabecalho & reader.GetString(19) & "|" 'preco_venda2
              txt_cabecalho = txt_cabecalho & reader.GetString(20) & "|" 'preco_venda3
              txt_cabecalho = txt_cabecalho & reader.GetString(21) & "|" 'preco_venda4
              txt_cabecalho = txt_cabecalho & reader.GetString(22) & "|" 'CFOP
              txt_cabecalho = txt_cabecalho & reader.GetString(23) & "|" 'ICMS_CST
              txt_cabecalho = txt_cabecalho & reader.GetString(24) & "|" 'descr_ICMS_CST
              txt_cabecalho = txt_cabecalho & reader.GetString(25) & "|" 'ICMS_aliq
              txt_cabecalho = txt_cabecalho & reader.GetString(26) & "|" 'ipi_CST
              txt_cabecalho = txt_cabecalho & reader.GetString(27) & "|" 'descr_ipi_CST
              txt_cabecalho = txt_cabecalho & reader.GetString(28) & "|" 'pis_CST
              txt_cabecalho = txt_cabecalho & reader.GetString(29) & "|" 'descr_pis_CST
              txt_cabecalho = txt_cabecalho & reader.GetString(30) & "|" 'pPIS
              txt_cabecalho = txt_cabecalho & reader.GetString(31) & "|" 'COFINS_CST
              txt_cabecalho = txt_cabecalho & reader.GetString(32) & "|" 'descr_COFINS_CST
              txt_cabecalho = txt_cabecalho & reader.GetString(33) & "|" 'pCOFINS
              txt_cabecalho = txt_cabecalho & reader.GetString(34) & "|" 'aliq_vTotTrib
              txt_cabecalho = txt_cabecalho & reader.GetString(35) & "|" 'aliq_vTotTrib_fonte_tribut
              If reader.GetString(38) = "" Then 'CEST
                txt_cabecalho = txt_cabecalho & "0|"
              Else
                txt_cabecalho = txt_cabecalho & reader.GetString(38) & "|"
              End If
              'Código ANP - bigint
              txt_cabecalho = txt_cabecalho & reader.GetString(39) & "|"


              sw.WriteLine(txt_cabecalho)

            Loop
          Else 'nenhum reigstro localizado
            path_download = ""
          End If
        End Using
      End If
      cnn.Close()
    End If
    Return path_download
  End Function

  'PEGA PREÇOS TEMPORÁRIOS DOS ITENS
  <WebMethod()> _
  Public Function Pega_Estoq_Itens_Preco_Temp(ByVal id_empresa As String, cnpj As String) As String

    Dim result As String = ""
    Dim cod_result As String = ""
    Dim strCnn As String = GetConnectionString("maxcont_cloud")
    Dim strSQL As String = "EXEC spWS_Estoque_Pega_Itens_Preco_Temp '" & id_empresa & "','" & cnpj & "','VALIDAR'"

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

      Dim command As SqlCommand = New SqlCommand("EXEC spWS_Estoque_Pega_Itens_Preco_Temp '" & id_empresa & "','" & cnpj & "','CONSULTAR'", cnn2)

      cnn2.Open()
      Dim reader As SqlDataReader = command.ExecuteReader()


      txt_cabecalho = "id_empresa|"
      txt_cabecalho = txt_cabecalho & "id_item|"
      txt_cabecalho = txt_cabecalho & "id_preco_temporario|"
      txt_cabecalho = txt_cabecalho & "tipo_duracao|"
      txt_cabecalho = txt_cabecalho & "descr_tp_duracao|"
      txt_cabecalho = txt_cabecalho & "dt_criacao|"
      txt_cabecalho = txt_cabecalho & "dt_inicio|"
      txt_cabecalho = txt_cabecalho & "hora_inicio|"
      txt_cabecalho = txt_cabecalho & "dt_fim|"
      txt_cabecalho = txt_cabecalho & "hora_fim|"
      txt_cabecalho = txt_cabecalho & "preco_temporario|"
      txt_cabecalho = txt_cabecalho & "id_usuario|"
      txt_cabecalho = txt_cabecalho & "qtde|"
      txt_cabecalho = txt_cabecalho & "fAtivo|"
      txt_cabecalho = txt_cabecalho & "{"

      If reader.HasRows Then
        Do While reader.Read()

          txt_cabecalho = txt_cabecalho & reader.GetString(0) & "|" 'id_empresa
          txt_cabecalho = txt_cabecalho & reader.GetString(1) & "|" 'id_item
          txt_cabecalho = txt_cabecalho & reader.GetString(2) & "|" 'id_preco_temporario
          txt_cabecalho = txt_cabecalho & reader.GetString(3) & "|" 'tipo_duracao
          txt_cabecalho = txt_cabecalho & reader.GetString(4) & "|" 'descr_tp_duracao
          txt_cabecalho = txt_cabecalho & reader.GetString(5) & "|" 'dt_criacao
          txt_cabecalho = txt_cabecalho & reader.GetString(6) & "|" 'dt_inicio
          txt_cabecalho = txt_cabecalho & "00:00:00|" 'hora_inicio
          If reader.GetString(7) <> "" Then 'proteção contra data vazia
            txt_cabecalho = txt_cabecalho & reader.GetString(7) & "|" 'dt_fim
          Else
            txt_cabecalho = txt_cabecalho & "0|" 'dt_fim
          End If
          txt_cabecalho = txt_cabecalho & "00:00:00|" 'hora_fim
          txt_cabecalho = txt_cabecalho & reader.GetString(8) & "|" 'preco_temporario
          txt_cabecalho = txt_cabecalho & reader.GetString(11) & "|" 'id_usuario
          txt_cabecalho = txt_cabecalho & reader.GetString(9) & "|" 'qtde
          txt_cabecalho = txt_cabecalho & reader.GetString(10) & "|" 'fAtivo
          txt_cabecalho = txt_cabecalho & "{"

          'sw.WriteLine(txt_cabecalho)
        Loop
      End If
      cnn.Close()
    End If
    Return txt_cabecalho

  End Function

  'PEGA CADASTRO DA EMPRESA
  <WebMethod()> _
  Public Function Pega_Cadastro_Empresa(ByVal id_empresa As String, cnpj As String) As String

    Dim result As String = ""
    Dim cod_result As String = ""
    Dim strCnn As String = GetConnectionString("maxcont_cloud")
    Dim strSQL As String = "EXEC spWS_Empresa_Pega_Cadastro '" & id_empresa & "','" & cnpj & "','VALIDAR'"

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

      Dim command As SqlCommand = New SqlCommand("EXEC spWS_Empresa_Pega_Cadastro '" & id_empresa & "','" & cnpj & "','CONSULTAR'", cnn2)

      cnn2.Open()
      Dim reader As SqlDataReader = command.ExecuteReader()


      txt_cabecalho = "id_empresa|"
      txt_cabecalho = txt_cabecalho & "razao_empresa|"
      txt_cabecalho = txt_cabecalho & "fantasia|"
      txt_cabecalho = txt_cabecalho & "CNPJ|"
      txt_cabecalho = txt_cabecalho & "IE|"
      txt_cabecalho = txt_cabecalho & "IM|"
      txt_cabecalho = txt_cabecalho & "CRT|"
      txt_cabecalho = txt_cabecalho & "descr_CRT|"
      txt_cabecalho = txt_cabecalho & "xLgr|"
      txt_cabecalho = txt_cabecalho & "nro|"
      txt_cabecalho = txt_cabecalho & "xCpl|"
      txt_cabecalho = txt_cabecalho & "bairro|"
      txt_cabecalho = txt_cabecalho & "cMun|"
      txt_cabecalho = txt_cabecalho & "xMun|"
      txt_cabecalho = txt_cabecalho & "UF|"
      txt_cabecalho = txt_cabecalho & "CEP|"
      txt_cabecalho = txt_cabecalho & "fone|"
      txt_cabecalho = txt_cabecalho & "email|"
      txt_cabecalho = txt_cabecalho & "SAT_assinatura_digital|"
      txt_cabecalho = txt_cabecalho & "tipo_PDV|"
      txt_cabecalho = txt_cabecalho & "{"

      If reader.HasRows Then
        Do While reader.Read()

          txt_cabecalho = txt_cabecalho & reader.GetString(0) & "|" 'id_empresa
          txt_cabecalho = txt_cabecalho & reader.GetString(1) & "|" 'razao_empresa
          If reader.GetString(2) <> "" Then
            txt_cabecalho = txt_cabecalho & reader.GetString(2) & "|" 'fantasia
          Else
            txt_cabecalho = txt_cabecalho & "--|" 'fantasia
          End If
          txt_cabecalho = txt_cabecalho & reader.GetString(3) & "|" 'CNPJ_emp
          txt_cabecalho = txt_cabecalho & reader.GetString(4) & "|" 'IE
          If reader.GetString(5) <> "" Then
            txt_cabecalho = txt_cabecalho & reader.GetString(5) & "|" 'IM
          Else
            txt_cabecalho = txt_cabecalho & "--|" 'IM
          End If

          txt_cabecalho = txt_cabecalho & reader.GetString(6) & "|" 'CRT
          txt_cabecalho = txt_cabecalho & reader.GetString(7) & "|" 'descr_CRT
          txt_cabecalho = txt_cabecalho & reader.GetString(8) & "|" 'xLgr
          txt_cabecalho = txt_cabecalho & reader.GetString(9) & "|" 'nro
          If reader.GetString(10) <> "" Then
            txt_cabecalho = txt_cabecalho & reader.GetString(10) & "|" 'xCpl
          Else
            txt_cabecalho = txt_cabecalho & "--|" 'xCpl
          End If

          txt_cabecalho = txt_cabecalho & reader.GetString(11) & "|" 'bairro
          txt_cabecalho = txt_cabecalho & reader.GetString(12) & "|" 'cMun
          txt_cabecalho = txt_cabecalho & reader.GetString(13) & "|" 'xMun
          txt_cabecalho = txt_cabecalho & reader.GetString(14) & "|" 'UF
          txt_cabecalho = txt_cabecalho & reader.GetString(15) & "|" 'CEP
          If reader.GetString(16) <> "" Then
            txt_cabecalho = txt_cabecalho & reader.GetString(16) & "|" 'fone
          Else
            txt_cabecalho = txt_cabecalho & "--|" 'fone
          End If
          If reader.GetString(17) <> "" Then
            txt_cabecalho = txt_cabecalho & reader.GetString(17) & "|" 'Email
          Else
            txt_cabecalho = txt_cabecalho & "--|" 'Email
          End If

          txt_cabecalho = txt_cabecalho & reader.GetString(18) & "|" 'SAT_Assinatura_Digital

          txt_cabecalho = txt_cabecalho & reader.GetString(19) & "|" 'id_tipo_pdv

          txt_cabecalho = txt_cabecalho & reader.GetString(20) & "|" 'descr_tipo_pdv

          txt_cabecalho = txt_cabecalho & "{"

          'sw.WriteLine(txt_cabecalho)
        Loop
      End If
      cnn.Close()
    End If
    Return txt_cabecalho

  End Function

  'PEGA ITENS DE ESTOQUE - ARQUIVO XML
  '<WebMethod()> _
  'Public Function Pega_Itens_Estoque_XML(ByVal id_empresa As String, cnpj As String) As String
  '  'Atualiza último NSU consultado pela empresa
  '  Dim result As String = ""
  '  Dim cod_result As String = ""
  '  Dim strCnn As String = GetConnectionString("maxcont_cloud")
  'Dim strSQL As String = "EXEC spWS_Estoque_Pega_Itens '" & id_empresa & "','" & cnpj & "','VALIDAR'"

  'Dim cnn As New SqlConnection(strCnn)
  'cnn.Open()
  'Dim cmd As New SqlClient.SqlCommand(strSQL, cnn)
  ''cmd.Parameters.Add(New SqlParameter("@ProductID", productID))

  '  Dim resultado As String = ""

  'Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
  '  If dr.HasRows Then
  '    dr.Read()
  '    result = dr("result")
  '    cod_result = dr("cod_result")
  '  End If
  '  cnn.Close()

  '  Dim XML_Document As New XmlDocument()

  '  If cod_result = "1" THEN

  '    Dim XML_Raiz = XML_Document.CreateElement("cadastro")
  '    XML_Document.AppendChild(XML_Raiz)

  '    Dim XML_Filho_conteudo = XML_Document.CreateElement("itens")
  '    Dim XML_Filho_conteudo2 = XML_Document.CreateElement("NFE")
  '    Dim XML_Filho_conteudo3 = XML_Document.CreateElement("NFE")
  '    Dim XML_Filho_conteudo4 = XML_Document.CreateElement("NFE")

  '    Dim strCnn2 As String = GetConnectionString("maxcont_cloud")
  '  Dim cnn2 As New SqlConnection(strCnn2)

  '    XML_Filho_conteudo = XML_Document.CreateElement("empresa")
  '    XML_Raiz.AppendChild(XML_Filho_conteudo)

  '    XML_Filho_conteudo2 = XML_Document.CreateElement("id_empresa")
  '    XML_Filho_conteudo2.InnerText = id_empresa
  '    XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '    XML_Filho_conteudo2 = XML_Document.CreateElement("cnpj")
  '    XML_Filho_conteudo2.InnerText = cnpj
  '    XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '  Dim command As SqlCommand = New SqlCommand ("EXEC spWS_Estoque_Pega_Itens '" & id_empresa & "','" & cnpj & "','CONSULTAR'",cnn2)

  '  cnn2.Open()
  '  Dim reader As SqlDataReader = command.ExecuteReader()

  '    If reader.HasRows Then
  '      Do While reader.Read()
  '        XML_Filho_conteudo = XML_Document.CreateElement("itens")
  '        XML_Raiz.AppendChild(XML_Filho_conteudo)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("id_item")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(0)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("cod_produto")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(1)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("descr")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(2)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("ICMS_orig")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(3)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("id_tipo")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(4)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("descr_tipo")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(5)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("id_grp")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(6)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("descr_grp")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(7)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("id_armazem")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(8)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("descr_armazem")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(9)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("EAN")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(10)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("und")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(11)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("und_com")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(12)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("classif_NCM")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(13)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("ipi")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(14)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("ICMS_ST_entrada")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(15)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("peso_liq")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(16)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("peso_bruto")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(17)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)


  '        XML_Filho_conteudo2 = XML_Document.CreateElement("preco_venda1")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(18)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("preco_venda2")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(19)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("preco_venda3")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(20)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("preco_venda4")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(21)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("CFOP")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(22)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("ICMS_CST")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(23)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("descr_ICMS_CST")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(24)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("ICMS_aliq")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(25)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("ipi_CST")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(26)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("descr_ipi_CST")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(27)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("pis_CST")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(28)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("descr_pis_CST")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(29)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("pPIS")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(30)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("COFINS_CST")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(31)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("descr_COFINS_CST")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(32)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("pCOFINS")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(33)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("aliq_vTotTrib")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(34)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '        XML_Filho_conteudo2 = XML_Document.CreateElement("aliq_vTotTrib_fonte_tribut")
  '        XML_Filho_conteudo2.InnerText = reader.GetString(35)
  '        XML_Filho_conteudo.AppendChild(XML_Filho_conteudo2)

  '      Loop
  '    End If
  '    cnn.Close()

  '  End If

  '  Return XML_Document.OuterXml'"EXEC spWS_Estoque_Pega_Itens '" & id_empresa & "','" & cnpj & "','CONSULTAR'"'XML_Document.OuterXml 
  'End Function


  'PEGA MOVIMENTAÇÃO DE CFE
  <WebMethod()> _
  Public Function Pega_CFe_Mov(ByVal id_empresa As String, cnpj As String, CFe_txt As String) As String

    Dim result As String = ""
    Dim cod_result As String = ""

    Dim tipo_doc As String = "1"

    Dim strCnn As String = GetConnectionString("maxcont_cloud")
    Dim strSQL As String = "EXEC spWS_PDV_CFe_Importa_Mov '" & id_empresa & "','" & cnpj & "','" & CFe_txt.Replace("'", "''") & "','" & tipo_doc & "'"

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

    Return result

  End Function

  <WebMethod()> _
  Public Function Pega_CFe_Mov_XML(ByVal id_empresa As String, cnpj As String, CFe_txt As String, txt_XML As String) As String
    'Recebe dados do CFE e o XML

    'Recebe TXT com arquivo XML

    Dim xml_url As String = ""

    'INÍCIO GRAVA ARQUIVO XML
    If txt_XML <> "" Then

      ' Create the XmlDocument.
      Dim doc As XmlDocument = New XmlDocument()
      doc.LoadXml(txt_XML)

      ' Save the document to a file. White space is
      ' preserved (no white space).
      doc.PreserveWhitespace = True

      'Cria data para gerar nome de arquivo
      Dim data As Date = Date.Now


      Dim Posic As Integer = 0

      Posic = txt_XML.IndexOf("infCFe")

      Dim nome_arquivo As String = ""

      'CFE (Pega chave de acordo com conteúdo da string)
      If txt_XML.Substring(0, 5) = "<CFe>" Then
        nome_arquivo = "AD" & txt_XML.Substring(Posic + 14, 44)
        'CFE CANCELAMENTO
      ElseIf txt_XML.Substring(0, 9) = "<CFeCanc>" Then
        nome_arquivo = "ADC" & txt_XML.Substring(Posic + 14, 44)
      Else
        'NÃO LOCALIZOU ARQUIVO VÁLIDO, GRAVA NOME DE ARQUIVO COM DATA E HORA
        nome_arquivo = data.Year & data.Month & data.Day & "_" & data.Hour & data.Minute & data.Second
      End If

      'Pasta onde serão armazendos os arquivos XMLs
      Dim path As String = Server.MapPath("../../Sat/Docs/" & CStr(id_empresa))
      If Not Directory.Exists(path) Then
        Directory.CreateDirectory(path)
      End If

      'Pasta ano_mes
      Dim mes As String = "00" & data.Month
      mes = Microsoft.VisualBasic.Right(mes, 2)

      Dim Pasta As String = data.Year & mes

      path = path & "\" & Pasta
      If Not Directory.Exists(path) Then
        Directory.CreateDirectory(path)
      End If

      'caminho completo onde será salvo o arquivo
      xml_url = path & "\" & nome_arquivo & ".xml"

      'Salva arquivo XML
      doc.Save(path & "\" & nome_arquivo & ".xml")

    End If
    'FIM GRAVA ARQUIVO XML

    Dim result As String = ""
    Dim cod_result As String = ""

    Dim tipo_doc As String = "1"

    Dim strCnn As String = GetConnectionString("maxcont_cloud")
    Dim strSQL As String = "EXEC spWS_PDV_CFe_Importa_Mov '" & id_empresa & "','" & cnpj & "','" & CFe_txt.Replace("'", "''") & "','" & tipo_doc & "','" & txt_XML.Replace("'", "''") & "','" & xml_url & "'"

    'Return strSQL

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


    Return result

  End Function

  <WebMethod()> _
  Public Function Pega_CFe_Mov_XML_Grava_XML(ByVal id_empresa As String, cnpj As String, CFe_txt As String, txt_XML As String) As String
    'Recebe dados do CFE e o XML

    'Recebe TXT com arquivo XML

    Dim xml_url As String = ""

    'INÍCIO GRAVA ARQUIVO XML
    If txt_XML <> "" Then

      ' Create the XmlDocument.
      Dim doc As XmlDocument = New XmlDocument()
      doc.LoadXml(txt_XML)

      ' Save the document to a file. White space is
      ' preserved (no white space).
      doc.PreserveWhitespace = True

      'Cria data para gerar nome de arquivo
      Dim data As Date = Date.Now


      Dim Posic As Integer = 0

      Posic = txt_XML.IndexOf("infCFe")

      Dim nome_arquivo As String = ""

      'CFE (Pega chave de acordo com conteúdo da string)
      If txt_XML.Substring(0, 5) = "<CFe>" Then
        nome_arquivo = "AD" & txt_XML.Substring(Posic + 14, 44)
        'CFE CANCELAMENTO
      ElseIf txt_XML.Substring(0, 9) = "<CFeCanc>" Then
        nome_arquivo = "ADC" & txt_XML.Substring(Posic + 14, 44)
      Else
        'NÃO LOCALIZOU ARQUIVO VÁLIDO, GRAVA NOME DE ARQUIVO COM DATA E HORA
        nome_arquivo = data.Year & data.Month & data.Day & "_" & data.Hour & data.Minute & data.Second
      End If

      'Pasta onde serão armazendos os arquivos XMLs
      Dim path As String = Server.MapPath("../../Sat/Docs/" & CStr(id_empresa))
      If Not Directory.Exists(path) Then
        Directory.CreateDirectory(path)
      End If

      'Pasta ano_mes
      Dim mes As String = "00" & data.Month
      mes = Microsoft.VisualBasic.Right(mes, 2)

      Dim Pasta As String = data.Year & mes

      path = path & "\" & Pasta
      If Not Directory.Exists(path) Then
        Directory.CreateDirectory(path)
      End If

      'caminho completo onde será salvo o arquivo
      xml_url = path & "\" & nome_arquivo & ".xml"

      'Salva arquivo XML
      doc.Save(path & "\" & nome_arquivo & ".xml")

    End If
    'FIM GRAVA ARQUIVO XML

    Dim result As String = ""
    Dim cod_result As String = ""

    Dim tipo_doc As String = "1"

    Dim strCnn As String = GetConnectionString("maxcont_cloud")
    Dim strSQL As String = "EXEC spWS_PDV_CFe_Importa_Mov '" & id_empresa & "','" & cnpj & "','" & CFe_txt.Replace("'", "''") & "','" & tipo_doc & "','" & txt_XML.Replace("'", "''") & "','" & xml_url & "'"

    'Return strSQL

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


    Return result

  End Function

  <WebMethod()> _
  Public Function Pega_Cliente_Importa(ByVal id_empresa As String, cnpj As String, Cliente_txt As String) As String
    'Recebe  dados dos clientes do PDV
    Dim result As String = ""
    Dim cod_result As String = ""
    Dim Cliente_Importa As String = Cliente_txt.Replace("'", "''")

    Dim strCnn As String = GetConnectionString("maxcont_cloud")
    Dim strSQL As String = "EXEC spWS_PDV_Cliente_Importa '" & id_empresa & "','" & cnpj & "','" & Cliente_Importa & "'"

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

    Return result

  End Function


  <WebMethod()> _
  Public Function Pega_PV_Mov(ByVal id_empresa As String, cnpj As String, CFe_txt As String) As String

    Dim result As String = ""
    Dim cod_result As String = ""

    Dim tipo_doc As String = "2"

    Dim strCnn As String = GetConnectionString("maxcont_cloud")
    Dim strSQL As String = "EXEC spWS_PDV_CFe_Importa_Mov '" & id_empresa & "','" & cnpj & "','" & CFe_txt.Replace("'", "''") & "','" & tipo_doc & "'"

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

    Return result

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
