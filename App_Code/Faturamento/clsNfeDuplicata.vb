Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class clsNfeDuplicata

  Private _seq, _ano, _id_mf_receber, _id_mf_pagar, _id_parc, _id_carteira, _fComissao, _result As Integer
  Private _nDup, _dVenc, _fFinanc_Baixa_Auto, _msg_result, _msg_erro As String
  Private _vDup As Decimal

  Public Sub New()

  End Sub

  Property seq() As Integer
    Get
      Return _seq
    End Get
    Set(value As Integer)
      _seq = value
    End Set
  End Property

  Property ano() As Integer
    Get
      Return _ano
    End Get
    Set(value As Integer)
      _ano = value
    End Set
  End Property

  Property id_mf_receber() As Integer
    Get
      Return _id_mf_receber
    End Get
    Set(value As Integer)
      _id_mf_receber = value
    End Set
  End Property

  Property id_mf_pagar() As Integer
    Get
      Return _id_mf_pagar
    End Get
    Set(value As Integer)
      _id_mf_pagar = value
    End Set
  End Property

  Property id_parc() As Integer
    Get
      Return _id_parc
    End Get
    Set(value As Integer)
      _id_parc = value
    End Set
  End Property

  Property id_carteira() As Integer
    Get
      Return _id_carteira
    End Get
    Set(value As Integer)
      _id_carteira = value
    End Set
  End Property

  Property fComissao() As Integer
    Get
      Return _fComissao
    End Get
    Set(value As Integer)
      _fComissao = value
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

  Property nDup() As String
    Get
      Return _nDup
    End Get
    Set(value As String)
      _nDup = value
    End Set
  End Property

  Property dVenc() As String
    Get
      Return _dVenc
    End Get
    Set(value As String)
      _dVenc = value
    End Set
  End Property

  Property fFinanc_Baixa_Auto() As String
    Get
      Return _fFinanc_Baixa_Auto
    End Get
    Set(value As String)
      _fFinanc_Baixa_Auto = value
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

  Property msg_erro() As String
    Get
      Return _msg_erro
    End Get
    Set(value As String)
      _msg_erro = value
    End Set
  End Property

  Property vDup() As Decimal
    Get
      Return _vDup
    End Get
    Set(value As Decimal)
      _vDup = value
    End Set
  End Property

  Public Function ListaDuplicataUmaNfe(ByVal id_empresa As Integer, ByVal id_nf As Integer) As DataView
    Dim table As New DataTable
    Dim row As DataRow
    Dim conexao As New clsConexao
    Dim dr As SqlDataReader
    Dim str_builder As New StringBuilder

    table.Columns.Add(New DataColumn("nDup"))
    table.Columns.Add(New DataColumn("dVenc"))
    table.Columns.Add(New DataColumn("vDup"))
    table.Columns.Add(New DataColumn("seq"))
    table.Columns.Add(New DataColumn("id_carteira"))
    table.Columns.Add(New DataColumn("carteira_descr"))
    table.Columns.Add(New DataColumn("fComissao"))
    table.Columns.Add(New DataColumn("tipo_parcela"))

    str_builder.Append("EXEC sp9_Lista_Duplicata_NFe '" & id_empresa & "','" & id_nf & "'")


    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

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

        table.Rows.Add(row)
      Loop

      dr.Close()
    Catch ex As System.Exception
      _msg_erro = "ERRO AO LISTAR AS DUPLICATAS DA NOTA: " & ex.Message() & "----------" & ex.StackTrace()
    End Try

    Dim dv As New DataView(table)

    Return dv
  End Function

  ''' <summary>
  ''' Retorna: result(0=FALHA | 1=OK); msg_result...
  ''' </summary>
  ''' <param name="id_nf"></param>
  ''' <param name="cond_pag"></param>
  Public Sub GeraDuplicataUmaNfe(ByVal id_nf As Integer, ByVal cond_pag As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim dr As SqlDataReader

    str_builder.Append("EXEC sp9_NFE_Cobranca_Gera '" & id_nf & "','" & cond_pag & "','T'")

    Try
      dr = conexao.RetornaDataReader(str_builder.ToString())

      Do While dr.Read()
        _result = dr(0)
        _msg_result = dr(1)
      Loop

      dr.Close()
    Catch ex As Exception
      _msg_erro = "ERRO AO GERAR AS DUPLICATAS NA NOTA: " & ex.Message() & "------------" & ex.StackTrace()
    End Try
  End Sub

  Public Sub ExcluiUmaDuplicataNfe(ByVal id_nf As Integer, ByVal seq As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder

    str_builder.Append("EXEC sp9_NFE_duplicata_Atualiza '" & id_nf & "','" & seq & "','','','','D'")

    Try
      conexao.ExecutaComando(str_builder.ToString())

    Catch ex As Exception
      _msg_erro = "ERRO AO EXCLUÍR A DUPLICATA DA NOTA: " & ex.Message() & "------------" & ex.StackTrace()
    End Try
  End Sub

  Public Sub InsereUmaDuplicataNfe(ByVal id_nf As Integer, ByVal nDup As String, ByVal dVenc As String, ByVal vDup As Decimal, _
                                   ByVal id_carteira As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim ajuste As New clsAjuste

    str_builder.Append("EXEC sp9_NFE_duplicata_Atualiza '" & id_nf & "','0','" & nDup & "','" & ajuste.AMD(dVenc) & "'")
    str_builder.Append(",'" & vDup & "','I','" & id_carteira & "'")

    Try
      conexao.RetornaDataReader(str_builder.ToString())

    Catch ex As Exception
      _msg_erro = "ERRO AO INSERIR A DUPLICATA NA NOTA FISCAL: " & ex.Message() & "----------" & ex.StackTrace()
    End Try
  End Sub

  Public Sub AlteraUmaDuplicataNfe(ByVal id_nf As Integer, ByVal seq As Integer, ByVal nDup As String, ByVal dVenc As String, _
                                   ByVal vDup As Decimal, ByVal id_carteira As Integer)
    Dim conexao As New clsConexao
    Dim str_builder As New StringBuilder
    Dim ajuste As New clsAjuste

    str_builder.Append("EXEC sp9_NFE_duplicata_Atualiza '" & id_nf & "','" & seq & "','" & nDup & "','" & ajuste.AMD(dVenc) & "'")
    str_builder.Append(",'" & vDup & "','I','" & id_carteira & "'")

    Try
      conexao.RetornaDataReader(str_builder.ToString())

    Catch ex As System.Exception
      _msg_erro = "ERRO AO ALTERAR A DUPLICATA NA NOTA FISCAL: " & ex.Message() & "----------" & ex.StackTrace()
    End Try
  End Sub

End Class
