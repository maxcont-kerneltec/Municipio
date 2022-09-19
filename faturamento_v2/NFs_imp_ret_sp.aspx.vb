Imports System.Text
Imports System.Data.OleDb
Imports System.Data
Imports System.IO
Imports System.Data.SqlClient
Imports System.Configuration

Partial Class Municipio_faturamento_v2_NFs_imp_ret_sp
  Inherits System.Web.UI.Page

  Dim dts As New DataSet
  Private da As OleDbDataAdapter

  Dim cnnSQL As String = ""
  Dim Session_ID As String = ""
  Dim id_empresa As String = ""
  Dim Caminho_Arquivo_Upload As String = ""

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    GetAllConnectionStrings()

    cnnSQL = Session("cnxStr").ToString

    If Me.txt_id_empresa.Text = "" Then
      id_empresa = Request.QueryString("id_emp").ToString.Replace(",", "")
      Me.txt_id_empresa.Text = id_empresa
    Else
      id_empresa = Me.txt_id_empresa.Text
    End If

  End Sub

  Private Sub btn_importa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_importa.Click
    Dim fileExt As String = ""

    If file_importa.HasFile Then
      Try
        Caminho_Arquivo_Upload = "E:\home\maxcont4\web\temp\" & file_importa.FileName
        'Caminho_Arquivo_Upload = "C:\home\web\temp\" & file_importa.FileName
        fileExt = System.IO.Path.GetExtension(file_importa.FileName)
        file_importa.SaveAs(Caminho_Arquivo_Upload)

        'Informações do arquivo
        'lbl_aviso.Text = "File name: " & file_importa.PostedFile.FileName & "<br>" & _
        '   "File Size: " & file_importa.PostedFile.ContentLength & " kb<br>" & _
        '   "Content type: " & file_importa.PostedFile.ContentType & "<br>" & _
        '   "Extensão type: " & fileExt
      Catch ex As Exception
        lbl_aviso.Text = "ERROR: " & ex.Message.ToString()
      End Try
    Else
      lbl_aviso.Text = "Nenhum arquivo selecionado."
    End If

    If fileExt <> ".csv" Then
      lbl_aviso.Text = "Extensão do arquivo inválida."
    Else
      ImportaCsv(Caminho_Arquivo_Upload)
    End If

    'Me.lbl_aviso.Text = ""
    'Me.lbl_erro.Text = ""

    'Dim caminho_arquivo As String = ""
    'Dim extensao_arquivo As String = ""

    'extensao_arquivo = PegaExtensaoArquivo()
    'caminho_arquivo = PegaCaminhoArquivo()

    'If caminho_arquivo = "ERRO_CAMINHO" Then
    '  Me.lbl_erro.Text = "<b>ERRO:</b> Favor informar o caminho do aquivo..."

    'ElseIf caminho_arquivo = "ERRO_EXTENSAO" Then
    '  Me.lbl_erro.Text = "<b>ERRO:</b> Extensão do arquivo inválido..."

    'Else

    '  Try
    '    If extensao_arquivo <> ".csv" Then
    '      Me.lbl_erro.Text = "Arquivo no formato inválido. Por favor, selecionar um arquivo no formato <b>.csv</b>..."

    '    ElseIf extensao_arquivo = ".csv" Then 'Importa csv
    '      ImportaCsv(caminho_arquivo)
    '      'dgrid_excel.DataSource = ImportaCsv(caminho_arquivo)
    '      'dgrid_excel.DataBind()
    '      'AjustaGrid() 'AJUSTA O GRIDVIEW
    '      'Me.btn_importa.Visible = False
    '      'Me.btn_salvar.CssClass = "button"

    '    Else
    '      Me.lbl_erro.Text = "ERRO desconhecido"
    '    End If

    '  Catch ex As Exception
    '    Me.lbl_erro.Text = ex.Message & " / " & ex.StackTrace
    '  End Try

    'End If

  End Sub

  Sub Salvar()
    Dim qtde_linhas_grid As Integer

    qtde_linhas_grid = dgrid_excel.Rows.Count

    Response.Write("estamosnfdsjndkfj")

  End Sub

  Public Function PegaExtensaoArquivo() As String
    Dim filePatch As String = ""
    Dim extensao As String = ""
    Dim filename As String = ""

    If Me.file_importa.HasFile Then
      filePatch = Me.file_importa.PostedFile.FileName
      filename = Path.GetFileName(filePatch) 'Pega o nome do arquivo
      extensao = Path.GetExtension(filename) 'Pega a extensão através do nome do arquivo
    Else
      extensao = "ERRO_EXTENSAO"
    End If

    Return extensao

  End Function

  Public Function PegaCaminhoArquivo() As String

    Dim filePatch As String = ""
    Dim filename As String = ""
    Dim ext As String = ""
    Dim retorno As String = ""
    'Dim contenttype As String = String.Empty

    If Me.file_importa.HasFile Then
      filePatch = Me.file_importa.PostedFile.FileName 'Pega o nome do arquivo
      filename = Path.GetFileName(filePatch)
      ext = Path.GetExtension(filename) 'Pega a extensão do arquivo
      retorno = filePatch
    Else
      retorno = "ERRO_CAMINHO"
    End If


    Return retorno

  End Function

  Public Function ImportaXls(ByVal caminho_arquivo As String) As DataSet

    'String de Conexão
    Dim conexao_excel As String = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
                                  "Data Source=" & caminho_arquivo & ";" & _
                                  "Extended Properties=Excel 8.0;" '& _


    '"Data Source=C:\inetpub\wwwroot\maxcont\Municipio\faturamento_v2\DevolucaoSP.xls;" & _
    '"ReadOnly=False; HDR=Yes"
    'HDR=Yes (informa que a primeira linha é cabeçalho

    Dim sql As String = "select * from [Plan1$]"
    'Dim sql As String = "select * from [DevolucaoSP]"
    Dim conn As OleDbConnection = Nothing

    Try
      conn = New OleDbConnection(conexao_excel)
      conn.Open()

      Dim cmd As New OleDbCommand(sql, conn)
      Dim da As New OleDbDataAdapter(cmd)

      da.Fill(dts) 'Aqui Preenchemos o DataSet

      'For i = 0 To dts.Tables(0).Rows.Count - 1 ***SALVO SOMENTE PARA CASO UM DIA PRECISE PERCORRER AS LINHAS***
      '  lbl_teste.Text = (dts.Tables(0).Rows(i).Item(0))
      '  lbl_teste2.Text = (dts.Tables(0).Rows(i).Item(1))
      '  dgrid_excel.DataSource = (dts.Tables(0).Rows(i).Item(0))
      'Next

    Catch ex As Exception
      lbl_erro.Text = ex.Message & " / " & ex.StackTrace

    Finally
      If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
        conn.Close()
      End If
    End Try

    Return dts

  End Function

  Public Sub ImportaCsv(ByVal caminho_arquivo As String)
    Dim dt As New DataTable
    Dim linha As DataRow
    Dim linha_texto As String = ""
    Dim arrrayDeLinhas() As String
    Dim obj_reader As New StreamReader(caminho_arquivo, System.Text.Encoding.UTF7) 'UTILIZADO PARA AJUSTAR PROBLEMAS DE ACENTUAÇÃO

    dt.Columns.Add("Tipo de Registro", Type.GetType("System.String"))
    dt.Columns.Add("Nº NFS-e", Type.GetType("System.String"))
    dt.Columns.Add("Data Hora NFE", Type.GetType("System.String"))
    dt.Columns.Add("Código de Verificação da NFS-e", Type.GetType("System.String"))
    dt.Columns.Add("Tipo de RPS", Type.GetType("System.String"))
    dt.Columns.Add("Série do RPS", Type.GetType("System.String"))
    dt.Columns.Add("Número do RPS", Type.GetType("System.String"))
    dt.Columns.Add("Data de Emissão do RPS", Type.GetType("System.String"))
    dt.Columns.Add("Inscrição Municipal do Prestador", Type.GetType("System.String"))
    dt.Columns.Add("Indicador de CPF/CNPJ do Prestador", Type.GetType("System.String"))
    dt.Columns.Add("CPF/CNPJ do Prestador", Type.GetType("System.String"))
    dt.Columns.Add("Razão Social do Prestador", Type.GetType("System.String"))
    dt.Columns.Add("Tipo do Endereço do Prestador", Type.GetType("System.String"))
    dt.Columns.Add("Endereço do Prestador", Type.GetType("System.String"))
    dt.Columns.Add("Número do Endereço do Prestador", Type.GetType("System.String"))
    dt.Columns.Add("Complemento do Endereço do Prestador", Type.GetType("System.String"))
    dt.Columns.Add("Bairro do Prestador", Type.GetType("System.String"))
    dt.Columns.Add("Cidade do Prestador", Type.GetType("System.String"))
    dt.Columns.Add("UF do Prestador", Type.GetType("System.String"))
    dt.Columns.Add("CEP do Prestador", Type.GetType("System.String"))
    dt.Columns.Add("Email do Prestador", Type.GetType("System.String"))
    dt.Columns.Add("Opção Pelo Simples", Type.GetType("System.String"))
    dt.Columns.Add("Situação da Nota Fiscal", Type.GetType("System.String"))
    dt.Columns.Add("Data de Cancelamento", Type.GetType("System.String"))
    dt.Columns.Add("Nº da Guia", Type.GetType("System.String"))
    dt.Columns.Add("Data de Quitação da Guia Vinculada a Nota Fiscal", Type.GetType("System.String"))
    dt.Columns.Add("Valor dos Serviços", Type.GetType("System.String"))
    dt.Columns.Add("Valor das Deduções", Type.GetType("System.String"))
    dt.Columns.Add("Código do Serviço Prestado na Nota Fiscal", Type.GetType("System.String"))
    dt.Columns.Add("Alíquota", Type.GetType("System.String"))
    dt.Columns.Add("ISS devido", Type.GetType("System.String"))
    dt.Columns.Add("Valor do Crédito", Type.GetType("System.String"))
    dt.Columns.Add("ISS Retido", Type.GetType("System.String"))
    dt.Columns.Add("Indicador de CPF/CNPJ do Tomador", Type.GetType("System.String"))
    dt.Columns.Add("CPF/CNPJ do Tomador", Type.GetType("System.String"))
    dt.Columns.Add("Inscrição Municipal do Tomador", Type.GetType("System.String"))
    dt.Columns.Add("Inscrição Estadual do Tomador", Type.GetType("System.String"))
    dt.Columns.Add("Razão Social do Tomador", Type.GetType("System.String"))
    dt.Columns.Add("Tipo do Endereço do Tomador", Type.GetType("System.String"))
    dt.Columns.Add("Endereço do Tomador", Type.GetType("System.String"))
    dt.Columns.Add("Número do Endereço do Tomador", Type.GetType("System.String"))
    dt.Columns.Add("Complemento do Endereço do Tomador", Type.GetType("System.String"))
    dt.Columns.Add("Bairro do Tomador", Type.GetType("System.String"))
    dt.Columns.Add("Cidade do Tomador", Type.GetType("System.String"))
    dt.Columns.Add("UF do Tomador", Type.GetType("System.String"))
    dt.Columns.Add("CEP do Tomador", Type.GetType("System.String"))
    dt.Columns.Add("Email do Tomador", Type.GetType("System.String"))
    dt.Columns.Add("Nº NFS-e Substituta", Type.GetType("System.String"))
    dt.Columns.Add("ISS pago", Type.GetType("System.String"))
    dt.Columns.Add("ISS a pagar", Type.GetType("System.String"))
    dt.Columns.Add("Indicador de CPF/CNPJ do Intermediário", Type.GetType("System.String"))
    dt.Columns.Add("CPF/CNPJ do Intermediário", Type.GetType("System.String"))
    dt.Columns.Add("Inscrição Municipal do Intermediário", Type.GetType("System.String"))
    dt.Columns.Add("Razão Social do Intermediário", Type.GetType("System.String"))
    dt.Columns.Add("Repasse do Plano de Saúde", Type.GetType("System.String"))
    dt.Columns.Add("PIS/PASEP", Type.GetType("System.String"))
    dt.Columns.Add("COFINS", Type.GetType("System.String"))
    dt.Columns.Add("INSS", Type.GetType("System.String"))
    dt.Columns.Add("IR", Type.GetType("System.String"))
    dt.Columns.Add("CSLL", Type.GetType("System.String"))
    dt.Columns.Add("Carga tributária: Valor", Type.GetType("System.String"))
    dt.Columns.Add("Carga tributária: Porcentagem", Type.GetType("System.String"))
    dt.Columns.Add("Carga tributária: Fonte", Type.GetType("System.String"))
    dt.Columns.Add("CEI", Type.GetType("System.String"))
    dt.Columns.Add("Matrícula da Obra", Type.GetType("System.String"))
    dt.Columns.Add("Município Prestação - cód. IBGE", Type.GetType("System.String"))
    dt.Columns.Add("Situação do Aceite", Type.GetType("System.String"))
    dt.Columns.Add("Encapsulamento", Type.GetType("System.String"))
    dt.Columns.Add("Valor Total Recebido", Type.GetType("System.String"))
    dt.Columns.Add("Tipo de Consolidação", Type.GetType("System.String"))
    dt.Columns.Add("Nº NFS-e Consolidada", Type.GetType("System.String"))
    dt.Columns.Add("Campo Reservado", Type.GetType("System.String"))
    dt.Columns.Add("Discriminação dos Serviços", Type.GetType("System.String"))


    Dim strSQL As String = ""
    Dim ds As New DataSet

    Try
      Do While obj_reader.Peek() <> -1
        linha_texto = obj_reader.ReadLine()
        arrrayDeLinhas = Split(linha_texto.Trim(), ";")
        'Response.Write(Split(linha_texto.Trim(), ";").Length)
        If Split(linha_texto.Trim(), ";").Length <= 73 Then 'Número de colunas que deve ter o arquivo .csv
          Response.Write(Split(linha_texto.Trim(), ";").Length)

          linha = dt.NewRow()
          linha.ItemArray = arrrayDeLinhas

          If linha(0) <> "" And linha(0) <> "Tipo de Registro" And linha(0) <> "Total" Then
            strSQL = "EXEC sp9_Importa_ArqNFSe_SP_Temp '" & id_empresa & "'"
            strSQL = strSQL & ",'" & linha(0) & "','" & linha(1) & "','" & AMD(Left(linha(2), 10)) & "','" & linha(3) & "'"
            strSQL = strSQL & ",'" & linha(6) & "','" & linha(8) & "','" & linha(23) & "','" & linha(28) & "'"

            'Response.Write(strSQL)

            'Response.End()

            'strSQL = "EXEC sp9_Importa_ArqNFSe_SP '" & id_empresa & "'"
            'strSQL = strSQL & ",'" & linha(0) & "','" & linha(1) & "','" & AMD(Left(linha(2), 10)) & "','" & linha(3) & "','" & linha(4) & "','" & linha(5) & "'"
            'strSQL = strSQL & ",'" & linha(6) & "','" & AMD(Left(linha(7), 10)) & "','" & linha(8) & "','" & linha(9) & "','" & linha(10) & "'"
            'strSQL = strSQL & ",'" & linha(11) & "','" & linha(12) & "','" & linha(13) & "','" & linha(14) & "','" & linha(15) & "'"
            'strSQL = strSQL & ",'" & linha(16) & "','" & linha(17) & "','" & linha(18) & "','" & linha(19) & "','" & linha(20) & "'"
            'strSQL = strSQL & ",'" & linha(21) & "','" & linha(22) & "','" & linha(23) & "','" & AMD(linha(24)) & "','" & AMD(Left(linha(25), 10)) & "'"
            'strSQL = strSQL & ",'" & linha(26) & "','" & linha(27) & "','" & linha(28) & "','" & linha(29) & "','" & linha(30) & "'"
            'strSQL = strSQL & ",'" & linha(31) & "','" & linha(32) & "','" & linha(33) & "','" & linha(34) & "','" & linha(35) & "'"
            'strSQL = strSQL & ",'" & linha(36) & "','" & linha(37) & "','" & linha(38) & "','" & linha(39) & "','" & linha(40) & "'"
            'strSQL = strSQL & ",'" & linha(41) & "','" & linha(42) & "','" & linha(43) & "','" & linha(44) & "','" & linha(45) & "'"
            'strSQL = strSQL & ",'" & linha(46) & "','" & linha(47) & "'"
            'Response.Write(strSQL & "<br /><br />")
            Using adapter As New SqlDataAdapter(strSQL, cnnSQL)
              adapter.Fill(ds, "mResult")
            End Using
          End If 'Fim do  Split(linha_texto.Trim(), ";").Length <= 48

        End If
      Loop

      obj_reader.Close()
      obj_reader = Nothing

      Response.Redirect("../../Faturamento/imp_nfse_sp_result.asp?cmun=3550308")

    Catch ex As Exception
      Me.lbl_aviso.Text = ex.Message & " / " & ex.StackTrace

    End Try


  End Sub

  Protected Sub AjustaGrid()

    Me.dgrid_excel.HorizontalAlign = HorizontalAlign.Center
    Me.dgrid_excel.BorderColor = Drawing.Color.Black
    Me.dgrid_excel.AutoGenerateColumns = False
    Me.dgrid_excel.BorderWidth = "1"
    Me.dgrid_excel.CellPadding = "3"
    Me.dgrid_excel.Font.Size = "8"
    Me.dgrid_excel.Font.Name = "Verdana"

  End Sub

  Protected Sub LoadGrid(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
    'FUNÇÃO QUE DEIXA INVISÍVEL ALGUMAS COLUNAS
    Dim i As Integer

    Select Case e.Row.RowType
      Case DataControlRowType.Header
        e.Row.Cells(4).Visible = False
        e.Row.Cells(5).Visible = False
        e.Row.Cells(8).Visible = False
        For i = 9 To 22
          e.Row.Cells(i).Visible = False
        Next i
        e.Row.Cells(25).Visible = False
        For i = 28 To 47
          e.Row.Cells(i).Visible = False
        Next i
        Exit Select

      Case DataControlRowType.Footer
        e.Row.Cells(4).Visible = False
        e.Row.Cells(5).Visible = False
        e.Row.Cells(8).Visible = False
        For i = 9 To 22
          e.Row.Cells(i).Visible = False
        Next i
        e.Row.Cells(25).Visible = False
        For i = 28 To 47
          e.Row.Cells(i).Visible = False
        Next i

        e.Row.Attributes.Add("onMouseOver", "this.style.backgroundColor='#E9EEFE';")
        e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor='#ffffff'")
        Exit Select

      Case DataControlRowType.DataRow
        e.Row.Cells(4).Visible = False
        e.Row.Cells(5).Visible = False
        e.Row.Cells(8).Visible = False
        For i = 9 To 22
          e.Row.Cells(i).Visible = False
        Next i
        e.Row.Cells(25).Visible = False
        For i = 28 To 47
          e.Row.Cells(i).Visible = False
        Next i

        e.Row.Attributes.Add("onMouseOver", "this.style.backgroundColor='#E9EEFE';")
        e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor='#ffffff'")
        Exit Select
    End Select

  End Sub

  'busca string de conexão a partir do WebConfig
  Private Sub GetAllConnectionStrings()
    Dim collCS As ConnectionStringSettingsCollection

    Try
      collCS = ConfigurationManager.ConnectionStrings
    Catch ex As Exception
      collCS = Nothing
    End Try

    If collCS IsNot Nothing Then
      For Each cs As ConnectionStringSettings In collCS

        If cs.Name = "maxcont_cloud" Then
          Session("cnxStr") = (cs.ConnectionString)
        Else
          Session("cnxStr") = ""
        End If
      Next
    Else
      Session("cnxStr") = ""
    End If
    'Session("cnxStr") = "Data Source=marcelo-pc\maxcont;Initial Catalog=maxcont;Persist Security Info=True;User ID=sql-maxcont;Password=kerneltec"
    collCS = Nothing
  End Sub

  Public Function AMD(ByVal strData As String) As String

    Dim asp_mm, asp_dd, asp_aaaa As String
    Dim pos1, pos2 As Integer

    pos1 = InStr(1, strData, "/")
    pos2 = InStr(pos1 + 1, strData, "/")

    If (pos1 > 0) And (pos2 > pos1) Then
      asp_aaaa = Mid(strData, pos2 + 1, 4)

      If Len(asp_aaaa) = 2 Then asp_aaaa = "20" & asp_aaaa
      asp_mm = Mid(strData, pos1 + 1, pos2 - pos1 - 1)
      asp_dd = Left(strData, pos1 - 1)
      AMD = asp_aaaa & "-" & Right("00" & asp_mm, 2) & "-" & Right("00" & asp_dd, 2)

      If Not IsDate(AMD) Then AMD = ""
    Else
      AMD = ""
    End If

    Return AMD

  End Function

  Public Function ChangeExt(ByVal OriginalName As String, ByVal NewName As String, ByVal CustExt As String) As String

    'Dim CustExt As String

    ' Set your custom extension
    'CustExt = "*.cxt"

    Try
      ' Change to the appropriate folder
      ChDir(InputBox("What folder are the files in", "C:\inetpub\wwwroot\maxcont\Municipio\faturamento_v2\DevolucaoSP.csv"))

      ' Retrieve the first entry.
      OriginalName = Dir(CustExt, vbDirectory)

      Do While OriginalName <> ""   ' Start the loop.
        ' Replace your custom extension with "txt"
        NewName = Left(OriginalName, Len(OriginalName) - 3) & "xls" '"csv"
        OriginalName = NewName ' Rename the file
        OriginalName = Dir()   ' Get next entry.
      Loop

    Catch ex As Exception
      lbl_erro.Text = ex.Message & " / " & ex.StackTrace & " / "
    End Try


    Return OriginalName

    Me.lbl_erro.Text = "Finished renaming files."

  End Function

End Class
