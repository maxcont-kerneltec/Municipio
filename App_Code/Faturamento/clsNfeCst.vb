Imports Microsoft.VisualBasic
Imports System.Data.SqlClient

Public Class clsNfeCst

  Private _id_imposto, _cst, _icms_orig, _result As Integer
  Private _descr_cst, _descr_orig, _msg_result As String

  Public Sub New()

  End Sub

  Property id_imposto() As Integer
    Get
      Return _id_imposto
    End Get
    Set(value As Integer)
      _id_imposto = value
    End Set
  End Property

  Property cst() As Integer
    Get
      Return _cst
    End Get
    Set(value As Integer)
      _cst = value
    End Set
  End Property

  Property icms_orig() As Integer
    Get
      Return _icms_orig
    End Get
    Set(value As Integer)
      _icms_orig = value
    End Set
  End Property

  Property result() As Integer
    Get
      Return _result
    End Get
    Set(value As Integer)
      _result = value
    End Set
  End Property

  Property descr_cst() As String
    Get
      Return _descr_cst
    End Get
    Set(value As String)
      _descr_cst = value
    End Set
  End Property

  Property descr_orig() As String
    Get
      Return _descr_orig
    End Get
    Set(value As String)
      _descr_orig = value
    End Set
  End Property

  Property msg_result() As String
    Get
      Return _msg_result
    End Get
    Set(value As String)
      _msg_result = value
    End Set
  End Property

  ''' <summary>
  ''' Retorna a lista de CST IPI para dropDownList: value(-1) | Text(SELECIONE)
  ''' </summary>
  ''' <param name="tipo"></param>
  ''' <returns></returns>
  Public Function ListaIpiCstDDList(ByVal tipo As OpcaoSelecione) As ArrayList
    Dim array_list As New ArrayList
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader
    Dim list As New ListItem

    str_builder.Append("SELECT cst, descr_cst ")
    str_builder.Append("FROM NFe_CST ")
    str_builder.Append("WHERE (id_imposto = 2) AND (cst <> -1)")

    If tipo = OpcaoSelecione.comSelecione Then
      list.Value = -1
      list.Text = "SELECIONE"
      array_list.Add(list)
    End If

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read
        list = New ListItem

        list.Value = dr(0)
        list.Text = dr(0) & " - " & dr(1)

        array_list.Add(list)
      Loop

      dr.Close()
    Catch ex As Exception
      MsgBox("ERRO AO CARREGAR O CST IPI: " & ex.Message() & "-------" & ex.StackTrace(), MsgBoxStyle.Critical)
    End Try

    Return array_list
  End Function

  ''' <summary>
  ''' Retorna a LIsta de ICMS CST para dropDownList: value(-1) | text(SELECIONE)
  ''' </summary>
  ''' <param name="id_empresa"></param>
  ''' <param name="tipo"></param>
  ''' <returns></returns>
  Public Function ListaIcmsCstDDList(ByVal id_empresa As Integer, ByVal tipo As OpcaoSelecione) As ArrayList
    Dim array_list As New ArrayList
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader
    Dim conexao As New clsConexao
    Dim lista As New ListItem

    str_builder.Append("declare @crt int ")
    str_builder.Append("set @crt = (SELECT crt FROM tbEmpresas WHERE (id_empresa = " & id_empresa & ")) ")

    str_builder.Append("if @crt = 3 ") 'regime normal
    str_builder.Append("SELECT cst, descr_cst ")
    str_builder.Append("FROM NFe_CST ")
    str_builder.Append("WHERE(id_imposto = 1) ")
    str_builder.Append("else ") 'simples nacional
    str_builder.Append("SELECT cst, descr_cst ")
    str_builder.Append("FROM NFe_CST ")
    str_builder.Append("WHERE (id_imposto = 5) ")

    If tipo = OpcaoSelecione.comSelecione Then
      lista.Value = -1
      lista.Text = "SELECIONE"
      array_list.Add(lista)
    End If

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read
        lista = New ListItem

        lista.Value = dr(0)
        lista.Text = dr(0) & " - " & dr(1)

        array_list.Add(lista)
      Loop

      dr.Close()
    Catch ex As Exception
      MsgBox("ERRO AO CARREGAR O ICMS-CST: " & ex.Message() & "-------" & ex.StackTrace(), MsgBoxStyle.Critical)
    End Try

    Return array_list
  End Function

  ''' <summary>
  ''' Retorna um arrayList do PIS CST para DropDownList: value(0) | text(SELECIONE)
  ''' </summary>
  ''' <param name="tipo"></param>
  ''' <returns></returns>
  Public Function ListaPisCstDDList(ByVal tipo As OpcaoSelecione) As ArrayList
    Dim array_list As New ArrayList
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader
    Dim conexao As New clsConexao
    Dim list As New ListItem

    str_builder.Append("SELECT cst, descr_cst ")
    str_builder.Append("FROM NFe_CST ")
    str_builder.Append("WHERE (id_imposto = 3) ")

    If tipo = OpcaoSelecione.comSelecione Then
      list.Value = 0
      list.Text = "SELECIONE"
      array_list.Add(list)
    End If


    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read
        list = New ListItem

        list.Value = dr(0)
        list.Text = dr(0) & " - " & dr(1)

        array_list.Add(list)
      Loop

      dr.Close()
    Catch ex As Exception
      MsgBox("ERRO AO CARREGAR PIS-CST: " & ex.Message() & "-------" & ex.StackTrace(), MsgBoxStyle.Critical)
    End Try

    Return array_list
  End Function

  ''' <summary>
  ''' Retorna um arrayList do Cofins CST para DropDownList: value(0) | text(SELECIONE)
  ''' </summary>
  ''' <param name="tipo"></param>
  ''' <returns></returns>
  Public Function ListaCofinsCstDDList(ByVal tipo As OpcaoSelecione) As ArrayList
    Dim array_list As New ArrayList
    Dim dr As SqlDataReader
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim list As New ListItem

    str_builder.Append("SELECT cst, descr_cst ")
    str_builder.Append("FROM NFe_CST ")
    str_builder.Append("WHERE (id_imposto = 4) ")

    If tipo = OpcaoSelecione.comSelecione Then
      list.Value = 0
      list.Text = "SELECIONE"
      array_list.Add(list)
    End If

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read
        list = New ListItem

        list.Value = dr(0)
        list.Text = dr(0) & " - " & dr(1)

        array_list.Add(list)
      Loop

      dr.Close()
    Catch ex As Exception
      MsgBox("ERRO AO CARREGAR O COFINS-ST: " & ex.Message() & "----------" & ex.StackTrace(), MsgBoxStyle.Critical)
    End Try

    Return array_list
  End Function

  ''' <summary>
  ''' Lista a origem do CST para DropDownList: value(-1) | text(SELECIONE)
  ''' </summary>
  ''' <param name="tipo"></param>
  ''' <returns></returns>
  Public Function ListaOrigemCstDDlist(ByVal tipo As OpcaoSelecione) As ArrayList
    Dim array_list As New ArrayList
    Dim dr As SqlDataReader
    Dim str_builder As New StringBuilder
    Dim obj_conexao As New clsConexao
    Dim lista As New ListItem

    If tipo = OpcaoSelecione.comSelecione Then
      lista.Text = "SELECIONE"
      lista.Value = "-1"
      array_list.Add(lista)
    End If

    str_builder.Append("SELECT ICMS_orig, descr ")
    str_builder.Append("FROM NFE_CST_orig ")
    str_builder.Append("ORDER BY ICMS_orig ")

    Try
      dr = obj_conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read
        lista = New ListItem

        lista.Value = dr(0)
        lista.Text = dr(0) & " - " & dr(1)

        array_list.Add(lista)
      Loop

      dr.Close()

    Catch ex As Exception
      MsgBox("ERRO AO CARREGAR A ORIGEM: " & ex.Message() & "-----" & ex.StackTrace(), MsgBoxStyle.Critical)
    End Try

    Return array_list
  End Function

  ''' <summary>
  ''' Lista Motivo da desoneração do ICMS...
  ''' </summary>
  ''' <param name="opcao"></param>
  ''' <returns></returns>
  Public Function ListaICMSmotDesICMS(ByVal opcao As OpcaoSelecione) As ArrayList
    Dim array As New ArrayList
    Dim item As New ListItem

    If opcao = OpcaoSelecione.comSelecione Then
      item.Value = 0
      item.Text = "SELECIONE"
      array.Add(item)
    End If

    item = New ListItem
    item.Value = 1
    item.Text = "1 - Táxi"
    array.Add(item)

    item = New ListItem
    item.Value = 2
    item.Text = "2 - Deficiente Físico"
    array.Add(item)

    item = New ListItem
    item.Value = 3
    item.Text = "3 - Produtor Agropecuário"
    array.Add(item)

    item = New ListItem
    item.Value = 4
    item.Text = "4 - Frotista/Locadora"
    array.Add(item)

    item = New ListItem
    item.Value = 5
    item.Text = "5 - Diplomático/Consular"
    array.Add(item)

    item = New ListItem
    item.Value = 6
    item.Text = "6 - Utilit. e Motoc. da Amazônia Ocid. e Áreas de Livre Comércio"
    array.Add(item)

    item = New ListItem
    item.Value = 7
    item.Text = "7 - SUFRAMA"
    array.Add(item)

    item = New ListItem
    item.Value = 9
    item.Text = "9 - Outros"
    array.Add(item)

    Return array

  End Function

  ''' <summary>
  ''' Opção Selecione... Value(-1)... Text(SELECIONE)
  ''' </summary>
  ''' <param name="opcao"></param>
  ''' <returns></returns>
  Public Function ListaICMSmodBCST(ByVal opcao As OpcaoSelecione) As ArrayList
    Dim array As New ArrayList
    Dim item As New ListItem

    If opcao = OpcaoSelecione.comSelecione Then
      item.Value = -1
      item.Text = "SELECIONE"
      array.Add(item)
    End If

    item = New ListItem
    item.Value = 0
    item.Text = "0 - Preço tabelado ou máximo sugerido"
    array.Add(item)

    item = New ListItem
    item.Value = 1
    item.Text = "1 - Lista Negativa (valor)"
    array.Add(item)

    item = New ListItem
    item.Value = 2
    item.Text = "2 - Lista Positiva (valor)"
    array.Add(item)

    item = New ListItem
    item.Value = 3
    item.Text = "3 - Lista Neutra (valor)"
    array.Add(item)

    item = New ListItem
    item.Value = 4
    item.Text = "4 - Margem Valor Agregado (%)"
    array.Add(item)

    item = New ListItem
    item.Value = 5
    item.Text = "5 - Pauta (valor)"
    array.Add(item)

    Return array

  End Function

  ''' <summary>
  ''' Opção Selecione... Value(-2) | Text(SELECIONE)...
  ''' </summary>
  ''' <param name="opcao"></param>
  ''' <returns></returns>
  Public Function ListaIpiCST(ByVal opcao As OpcaoSelecione) As ArrayList
    Dim array As New ArrayList
    Dim item As New ListItem

    If opcao = OpcaoSelecione.comSelecione Then
      item.Value = -2
      item.Text = "SELECIONE"
      array.Add(item)
    End If

    item = New ListItem
    item.Value = -1
    item.Text = ""
    array.Add(item)

    item = New ListItem
    item.Value = 0
    item.Text = "00 - Entrada com recuperação de crédito"
    array.Add(item)

    item = New ListItem
    item.Value = 1
    item.Text = "01 - Entrada tributada com aliquota zero"
    array.Add(item)

    item = New ListItem
    item.Value = 2
    item.Text = "02 - Entrada isenta"
    array.Add(item)

    item = New ListItem
    item.Value = 3
    item.Text = "03 - Entrada não-tributada"
    array.Add(item)

    item = New ListItem
    item.Value = 4
    item.Text = "04 - Entrada imune"
    array.Add(item)

    item = New ListItem
    item.Value = 5
    item.Text = "05 - Entrada com suspensão"
    array.Add(item)

    item = New ListItem
    item.Value = 49
    item.Text = "49 - Outras entradas"
    array.Add(item)

    item = New ListItem
    item.Value = 50
    item.Text = "50 - Saída Tributada"
    array.Add(item)

    item = New ListItem
    item.Value = 51
    item.Text = "51 - Saída tributada com alíquota zero"
    array.Add(item)

    item = New ListItem
    item.Value = 52
    item.Text = "52 - Saída isenta"
    array.Add(item)

    item = New ListItem
    item.Value = 53
    item.Text = "53 - Saída não-tributada"
    array.Add(item)

    item = New ListItem
    item.Value = 54
    item.Text = "54 - Saída imune"
    array.Add(item)

    item = New ListItem
    item.Value = 55
    item.Text = "55 - Saída com suspensão"
    array.Add(item)

    item = New ListItem
    item.Value = 99
    item.Text = "99 - Outras saídas"
    array.Add(item)

    Return array

  End Function

  ''' <summary>
  ''' Lista todos os CST para empresas do Regime Normal
  ''' </summary>
  ''' <param name="selecione"></param>
  ''' <returns></returns>
  Public Function ListaIcmsCstRegNormal(ByVal selecione As OpcaoSelecione) As ArrayList
    Dim array_list As New ArrayList
    Dim item As New ListItem
    Dim conexao As New clsConexao
    Dim dr As SqlDataReader
    Dim str_builder As New StringBuilder

    If selecione = OpcaoSelecione.comSelecione Then
      item.Value = -1
      item.Text = "SELECIONE"
      array_list.Add(item)
    End If

    str_builder.Append("SELECT cst, descr_cst ")
    str_builder.Append("FROM NFe_CST ")
    str_builder.Append("WHERE(id_imposto = 1) ")

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        item = New ListItem

        item.Value = dr(0)
        item.Text = dr(0) & " - " & dr(1)

        array_list.Add(item)
      Loop

      dr.Close()
    Catch ex As Exception
      Me.result = -1
      Me.msg_result = "ERRO AO LISTAR CST REGIME NORMAL: " & ex.Message() & "----------" & ex.StackTrace
    End Try

    Return array_list

  End Function

  ''' <summary>
  ''' Lista todos os CST para empresas do Simples Nacional
  ''' </summary>
  ''' <param name="selecione"></param>
  ''' <returns></returns>
  Public Function ListaIcmsCstSimples(ByVal selecione As OpcaoSelecione) As ArrayList
    Dim array_list As New ArrayList
    Dim item As New ListItem
    Dim conexao As New clsConexao
    Dim dr As SqlDataReader
    Dim str_builder As New StringBuilder

    If selecione = OpcaoSelecione.comSelecione Then
      item.Value = -1
      item.Text = "SELECIONE"
      array_list.Add(item)
    End If

    str_builder.Append("SELECT cst, descr_cst ")
    str_builder.Append("FROM NFe_CST ")
    str_builder.Append("WHERE(id_imposto = 5) ")

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        item = New ListItem

        item.Value = dr(0)
        item.Text = dr(0) & " - " & dr(1)

        array_list.Add(item)
      Loop

      dr.Close()
    Catch ex As Exception
      Me.result = -1
      Me.msg_result = "ERRO AO LISTAR CST REGIME NORMAL: " & ex.Message() & "----------" & ex.StackTrace
    End Try

    Return array_list

  End Function

End Class
