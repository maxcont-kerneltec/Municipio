Imports Microsoft.VisualBasic
Imports System.Net.Mail

Public Class clsEmail

  Private _result As Integer
  Private _msg_retorno As String

  Public Sub New()

  End Sub

  Property result() As Integer
    Get
      Return _result
    End Get
    Set(value As Integer)
      _result = value
    End Set
  End Property

  Property msg_retorno() As String
    Get
      Return _msg_retorno
    End Get
    Set(value As String)
      _msg_retorno = value
    End Set
  End Property

  ''' <summary>
  ''' Retorna result(-1) ERRO e msg_retorno
  ''' </summary>
  ''' <param name="email_remetente"></param>
  ''' <param name="email_dest"></param>
  ''' <param name="msg"></param>
  'Public Sub EnviaEmail(ByVal email_remetente As String, ByVal email_dest As String, ByVal msg As String,
  '                      ByVal assunto_msg As String)
  '  'https://wiki.locaweb.com.br/pt-br/Envio_de_e-mails_via_.NET_utilizando_o_System.Net.Mail

  '  'Define os dados do e-mail
  '  'Dim nomeRemetente As String
  '  Dim senha As String

  '  'nomeRemetente = "maxcont@kerneltec.com.br"
  '  senha = "YCDTajzh2335"

  '  'Dim emailDestinatario As String
  '  'Dim emailComCopia As String
  '  'Dim emailComCopiaOculta As String

  '  'emailDestinatario = "email@destinatario.com.br"
  '  'emailComCopia = "email@comcopia.com.br"
  '  'emailComCopiaOculta = "email@comcopiaoculta.com.br"


  '  'Host da porta SMTP
  '  Dim SMTP As String
  '  SMTP = "smtplw.com.br" '"smtp.kerneltec.com.br"

  '  'Cria objeto com dados do e-mail.
  '  Dim objEmail As New System.Net.Mail.MailMessage()

  '  'Define o Campo From e ReplyTo do e-mail.
  '  objEmail.From = New System.Net.Mail.MailAddress("naoresponder@maxcont.com.br")
  '  'objEmail.ReplyTo = New System.Net.Mail.MailAddress("Nome <email@seudominio.com.br>")

  '  'Define os destinatários do e-mail.
  '  Dim i As Integer = 1000
  '  Dim email_envio As String
  '  Dim j As Integer = 0
  '  email_dest = email_dest & ";"

  '  Do While i > 0
  '    If email_dest.Length <= 0 Then
  '      Exit Do
  '    End If

  '    i = email_dest.IndexOf(";", 1)
  '    email_envio = Left(email_dest, i)

  '    objEmail.To.Add("<" & email_envio & ">")

  '    j = (email_dest.Length - i) - 1
  '    email_dest = email_dest.Substring(i + 1, j)
  '  Loop
  '  'objEmail.To.Add("<" & email_dest & ">")

  '  'Define a prioridade do e-mail.
  '  objEmail.Priority = System.Net.Mail.MailPriority.Normal

  '  'Define o formato do e-mail HTML (caso não queira HTML alocar valor false)
  '  objEmail.IsBodyHtml = True

  '  'Define o título do e-mail.
  '  objEmail.Subject = assunto_msg 'assuntoMensagem

  '  'Define o corpo do e-mail.
  '  objEmail.Body = msg

  '  'Para evitar problemas com caracteres "estranhos", configuramos o Charset para "ISO-8859-1"
  '  objEmail.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1")
  '  objEmail.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1")



  '  'Cria objeto com os dados do SMTP
  '  Dim objSmtp As New System.Net.Mail.SmtpClient(SMTP, 587)

  '  'Alocamos o endereço do host para enviar os e-mails  
  '  objSmtp.Credentials = New System.Net.NetworkCredential(email_remetente, senha)
  '  objSmtp.Host = SMTP
  '  objSmtp.Port = 587

  '  'Caso utilize conta de email do exchange da locaweb deve habilitar o SSL
  '  'objEmail.EnableSsl = True
  '  objSmtp.EnableSsl = True

  '  'Enviamos o e-mail através do método .send()
  '  Try
  '    objSmtp.Send(objEmail)
  '    result = 0
  '    Me.msg_retorno = "E-mail enviado com sucesso !"

  '  Catch ex As Exception
  '    result = -1
  '    Me.msg_retorno = "Ocorreram problemas no envio do e-mail. Erro = " & ex.Message() & "-----" & ex.StackTrace()
  '  End Try

  '  'excluímos o objeto de e-mail da memória
  '  objEmail.Dispose()
  '  'anexo.Dispose();

  'End Sub

End Class
