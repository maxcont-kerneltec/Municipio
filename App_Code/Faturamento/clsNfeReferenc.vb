Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsNfeReferenc

  Private _id_nf, _item, _cUF, _modd, _serie, _nNF, _result As Integer
  Private _refNFe, _AAMM, _cnpj, _UF, _msg_result As String
  Private conexao1 As New SqlConnection
  Public Sub New()

  End Sub

  Property id_nf() As Integer
    Get
      Return _id_nf
    End Get
    Set(value As Integer)
      _id_nf = value
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

  Property item() As Integer
    Get
      Return _item
    End Get
    Set(value As Integer)
      _item = value
    End Set
  End Property

  Property cUF() As Integer
    Get
      Return _cUF
    End Get
    Set(value As Integer)
      _cUF = value
    End Set
  End Property

  Property modd() As Integer
    Get
      Return _modd
    End Get
    Set(value As Integer)
      _modd = value
    End Set
  End Property

  Property serie() As Integer
    Get
      Return _serie
    End Get
    Set(value As Integer)
      _serie = value
    End Set
  End Property

  Property nNF() As Integer
    Get
      Return _nNF
    End Get
    Set(value As Integer)
      _nNF = value
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


  Public Function ListaNotasReferenciadasUmaNota(ByVal id_nf As Integer) As DataView
    Dim table As New DataTable
    Dim row As DataRow
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    table.Columns.Add(New DataColumn("item"))
    table.Columns.Add(New DataColumn("refNFe"))
    table.Columns.Add(New DataColumn("cUF"))
    table.Columns.Add(New DataColumn("AAMM"))
    table.Columns.Add(New DataColumn("cnpj"))
    table.Columns.Add(New DataColumn("modd"))
    table.Columns.Add(New DataColumn("serie"))
    table.Columns.Add(New DataColumn("nNF"))
    table.Columns.Add(New DataColumn("UF"))
    table.Columns.Add(New DataColumn("IE"))
    table.Columns.Add(New DataColumn("tp_referenc"))

    str_builder.Append("SELECT item, refNFe, cUF, AAMM, cnpj, mod, serie, nNF, UF, '' as IE, 'R' AS tp_referenc ")
    str_builder.Append("FROM NFE_referenc ")
    str_builder.Append("WHERE (id_nf = " & id_nf & ") ")
    str_builder.Append("UNION ")
    str_builder.Append("SELECT item, '' as refNFe, cUF, AAMM, cnpj, mod, serie, nNF, UF, IE, 'P' AS tp_referenc ")
    str_builder.Append("FROM dbo.NFE_referenc_produtor ")
    str_builder.Append("WHERE (id_nf = " & id_nf & ") ")
    str_builder.Append("ORDER BY refNFe desc")


    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      Do While dr.Read()
        row = table.NewRow()

        row(0) = dr(0)
        row(1) = dr(1)
        row(2) = dr(2)
        row(3) = dr(3)
        row(4) = dr(4)
        row(5) = dr(5)
        row(6) = dr(6)
        row(7) = dr(7)
        row(8) = dr(8)
        row(9) = dr(9)
        row(10) = dr(10)

        table.Rows.Add(row)
      Loop
      conexao.FechaBanco(conexao1)
      dr.Close()
    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      MsgBox("ERRO AO LISTAR A NOTA FISCAL REFERENCIADA: " & ex.Message() & "---------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
    End Try

    Dim dv As New DataView(table)

    Return dv
  End Function

  ''' <summary>
  ''' Para inserção no banco... item=0
  ''' Retorna: result(0=Add com sucesso; 1=Atualizado com sucesso) | msg_result
  ''' </summary>
  ''' <param name="id_nf"></param>
  ''' <param name="item"></param>
  ''' <param name="refNFe"></param>
  Public Sub AtualizaNfeReferencia(ByVal id_nf As Integer, ByVal item As Integer, ByVal refNFe As String, _
                                   ByVal cUF As String, ByVal AAMM As String, ByVal cnpj As String, _
                                   ByVal modd As String, ByVal serie As Integer, ByVal nNF As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim acao As String = ""

    If item = 0 Then
      acao = "I"
    End If

    str_builder.Append("EXEC sp9_NFE_Ref_atualiza '" & id_nf & "','" & item & "','" & acao & "','" & refNFe & "'")
    str_builder.Append(",'" & cUF & "','" & AAMM & "','" & cnpj & "','" & modd & "','" & serie & "','" & nNF & "'")


    Try
      conexao.ExecutaComando(str_builder.ToString())

      If item = 0 Then
        Me.result = 0
        Me.msg_result = "Chave de acesso referenciada com sucesso!"
      Else
        Me.result = 1
        Me.msg_result = "Chave de acesso atualizada com sucesso!"
      End If

    Catch ex As Exception
      MsgBox("ERRO AO ATUALIZAR A REFERÊNCIA: " & ex.Message() & "----------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
    End Try
  End Sub

  Public Sub ExcluiNfeReferencia(ByVal id_nf As Integer, ByVal item As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_NFE_Ref_atualiza '" & id_nf & "','" & item & "','D'")


    Try
      conexao.ExecutaComando(str_builder.ToString())

      Me.result = 0
      Me.msg_result = "Chave de acesso excluída com sucesso!"
    Catch ex As Exception
      MsgBox("ERRO AO EXCLUÍR A REFERÊNCIA: " & ex.Message() & "----------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
    End Try
  End Sub

  ''' <summary>
  ''' Retorna SOMENTE as Notas Fiscais Eletrônicas referenciadas da nota...
  ''' </summary>
  ''' <param name="id_nf"></param>
  ''' <returns></returns>
  Public Function ListaNotaFiscalEletronicaReferenciada(ByVal id_nf As Integer) As DataView
    Dim table As New DataTable
    Dim row As DataRow
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    table.Columns.Add(New DataColumn("item"))
    table.Columns.Add(New DataColumn("refNFe"))

    str_builder.Append("SELECT item, refNFe ")
    str_builder.Append("FROM NFE_referenc ")
    str_builder.Append("WHERE (id_nf = " & id_nf & ") AND (refNFe <> '') ")

    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      Do While dr.Read()
        row = table.NewRow()

        row(0) = dr(0)
        row(1) = dr(1)

        table.Rows.Add(row)
      Loop
      conexao.FechaBanco(conexao1)
      dr.Close()
    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      MsgBox("ERRO AO BUSCAR AS NOTAS FISCAIS ELETRÔNICAS REFERENCIADAS PARA TRANSMISSÃO: " & ex.Message() & "----------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
    End Try

    Dim dv As New DataView(table)

    Return dv
  End Function

  ''' <summary>
  ''' Lista SOMENTE a Nota Fiscal referenciada da nota...
  ''' </summary>
  ''' <param name="id_nf"></param>
  ''' <returns></returns>
  Public Function ListaNotaFiscalReferenciada(ByVal id_nf As Integer) As DataView
    Dim table As New DataTable
    Dim row As DataRow
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    table.Columns.Add(New DataColumn("item"))
    table.Columns.Add(New DataColumn("cUF"))
    table.Columns.Add(New DataColumn("AAMM"))
    table.Columns.Add(New DataColumn("cnpj"))
    table.Columns.Add(New DataColumn("mod"))
    table.Columns.Add(New DataColumn("serie"))
    table.Columns.Add(New DataColumn("nNF"))
    table.Columns.Add(New DataColumn("UF"))
    table.Columns.Add(New DataColumn("IE"))
    table.Columns.Add(New DataColumn("tp_referenc"))

    str_builder.Append("SELECT item, cUF, AAMM, cnpj, mod, serie, nNF, UF, '' as IE, 'R' AS tp_referenc ")
    str_builder.Append("FROM NFE_referenc ")
    str_builder.Append("WHERE (id_nf = " & id_nf & ") AND ((refNFe = '') OR (refNFe IS NULL)) ")


    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      Do While dr.Read
        row = table.NewRow()

        row(0) = dr(0)
        row(1) = dr(1)
        row(2) = dr(2)
        row(3) = dr(3)
        row(4) = dr(4)
        row(5) = dr(5)
        row(6) = dr(6)
        row(7) = dr(7)
        row(8) = dr(8)
        row(9) = dr(9)

        table.Rows.Add(row)
      Loop
      conexao.FechaBanco(conexao1)
      dr.Close()
    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      MsgBox("ERRO AO BUSCAR A NOTA FISCAL REFERENCIADA AO TRANSMITIR A NOTA: " & ex.Message() & "----------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
    End Try

    Dim dv As New DataView(table)

    Return dv
  End Function

  ''' <summary>
  ''' Lista SOMENTE as notas fiscais de produtor rural da nota...
  ''' </summary>
  ''' <param name="id_nf"></param>
  ''' <returns></returns>
  Public Function ListaNotaFiscalProdutorRural(ByVal id_nf As Integer) As DataView
    Dim table As New DataTable
    Dim row As DataRow
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    table.Columns.Add(New DataColumn("item"))
    table.Columns.Add(New DataColumn("cUF"))
    table.Columns.Add(New DataColumn("AAMM"))
    table.Columns.Add(New DataColumn("cnpj"))
    table.Columns.Add(New DataColumn("mod"))
    table.Columns.Add(New DataColumn("serie"))
    table.Columns.Add(New DataColumn("nNF"))
    table.Columns.Add(New DataColumn("UF"))
    table.Columns.Add(New DataColumn("IE"))
    table.Columns.Add(New DataColumn("tp_referenc"))

    str_builder.Append("SELECT item, cUF, AAMM, cnpj, mod, serie, nNF, UF, IE, 'P' AS tp_referenc ")
    str_builder.Append("FROM dbo.NFE_referenc_produtor ")
    str_builder.Append("WHERE (id_nf = " & id_nf & ") ")

    Try
      'dr = conexao.RetornaDataReader(str_builder.ToString())
      conexao1 = conexao.AbreBanco()
      dr = conexao.RetornaDataReader_Conexao(str_builder.ToString(), conexao1)

      Do While dr.Read()
        row = table.NewRow()

        row(0) = dr(0)
        row(1) = dr(1)
        row(2) = dr(2)
        row(3) = dr(3)
        row(4) = dr(4)
        row(5) = dr(5)
        row(6) = dr(6)
        row(7) = dr(7)
        row(8) = dr(8)
        row(9) = dr(9)

        table.Rows.Add(row)
      Loop
      conexao.FechaBanco(conexao1)
      dr.Close()
    Catch ex As Exception
      conexao.FechaBanco(conexao1)
      MsgBox("ERRO AO BUSCAR O PRODUTOR RURAL AO TRANSMITIR A NF: " & ex.Message() & "--------" & ex.StackTrace(), MsgBoxStyle.Critical, "Maxcont")
    End Try

    Dim dv As New DataView(table)

    Return dv
  End Function

End Class
