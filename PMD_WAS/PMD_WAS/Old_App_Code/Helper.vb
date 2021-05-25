Imports System.Data.SqlClient
Imports System.IO
Imports System.Net.Mail
Imports Microsoft.VisualBasic
Imports System.Net
Imports System.Net.Sockets
Imports System.Web

Public Class Helper

    Public Sub EnviaCoreoCalendario(destinatario As String, asunto As String, titulo As String, fechaIni As DateTime, fechaFin As DateTime, mensaje As String, lugar As String)

        Try
            Dim msg As New System.Net.Mail.MailMessage("intelipolis@gmail.com", destinatario)
            msg.Body = mensaje
            msg.Subject = asunto
            msg.IsBodyHtml = True
            msg.Bcc.Add("intelipolis@gmail.com")

            Dim Str As New StringBuilder()
            Str.AppendLine("BEGIN:VCALENDAR")
            Str.AppendLine("PRODID:-//Schedule a Meeting")
            Str.AppendLine("VERSION:2.0")
            Str.AppendLine("METHOD:REQUEST")
            Str.AppendLine("BEGIN:VEVENT")
            Str.AppendLine(String.Format("DTSTART:{0}", fechaIni.ToUniversalTime().ToString("yyyyMMddTHHmmssZ")))
            'Str.AppendLine(String.Format("DTSTAMP:{0:yyyyMMddTHHmmssZ}", DateTime.UtcNow)) '
            Str.AppendLine(String.Format("DTEND:{0}", fechaFin.ToUniversalTime().ToString("yyyyMMddTHHmmssZ")))
            Str.AppendLine("LOCATION: " + lugar)
            Str.AppendLine("TITLE: " + titulo)
            Str.AppendLine(String.Format("UID:{0}", Guid.NewGuid()))
            Str.AppendLine(String.Format("DESCRIPTION:{0}", msg.Body))
            Str.AppendLine(String.Format("X-ALT-DESC;FMTTYPE=text/html:{0}", msg.Body))
            Str.AppendLine(String.Format("SUMMARY:{0}", msg.Subject))
            Str.AppendLine(String.Format("ORGANIZER:MAILTO:{0}", msg.From.Address))

            Str.AppendLine(String.Format("ATTENDEE;CN=""{0}"";RSVP=TRUE:mailto:{1}", msg.To(0).DisplayName, msg.To(0).Address))

            Str.AppendLine("BEGIN:VALARM")
            Str.AppendLine("TRIGGER:-PT15M")
            Str.AppendLine("ACTION:DISPLAY")
            Str.AppendLine("DESCRIPTION:Reminder")
            Str.AppendLine("END:VALARM")
            Str.AppendLine("END:VEVENT")
            Str.AppendLine("END:VCALENDAR")

            Dim byteArray = Encoding.ASCII.GetBytes(Str.ToString())
            Dim stream = New MemoryStream(byteArray)

            Dim attach = New Attachment(stream, "evento.ics")
            msg.Attachments.Add(attach)

            Dim contype = New System.Net.Mime.ContentType("text/calendar")
            contype.Parameters.Add("method", "REQUEST")
            'contype.Parameters.Add("name", "Meeting.ics")
            Dim avCal = AlternateView.CreateAlternateViewFromString(Str.ToString(), contype)
            msg.AlternateViews.Add(avCal)

            'Now sending a mail with attachment ICS file.                     
            Dim SmtpClient = New System.Net.Mail.SmtpClient()
            SmtpClient.Port = "587"
            SmtpClient.Host = "smtp.gmail.com" '//-------this has To given the Mailserver IP
            SmtpClient.EnableSsl = True
            SmtpClient.Credentials = New System.Net.NetworkCredential("intelipolis@gmail.com", "IntelipolisEventos2020")
            SmtpClient.Send(msg)
        Catch ex As Exception
            'Throw ex
        End Try

    End Sub

    Public Sub EnviaCorreo(destinatario As String, asunto As String, mensaje As String)
        Try
            Dim msg As New System.Net.Mail.MailMessage("intelipolis@gmail.com", destinatario)
            msg.Body = mensaje
            msg.Subject = asunto
            msg.IsBodyHtml = True
            msg.Bcc.Add("intelipolis@gmail.com")

            'Now sending a mail with attachment ICS file.                     
            Dim SmtpClient = New System.Net.Mail.SmtpClient()
            SmtpClient.UseDefaultCredentials = False
            SmtpClient.Port = 587
            SmtpClient.Host = "smtp.gmail.com" '//-------this has To given the Mailserver IP
            SmtpClient.EnableSsl = True
            SmtpClient.Credentials = New System.Net.NetworkCredential("intelipolis@gmail.com", "IntelipolisEventos2020")
            SmtpClient.Send(msg)

        Catch ex As Exception
            'Throw ex
        End Try
    End Sub

    Public Shared Sub GuardaLog(empleado As Integer, log As String)
        Try
            Dim con As New Class1

            Dim ip As String = ""
            Try
                ip = HttpContext.Current.Request.UserHostAddress
            Catch ex As Exception
            End Try

            Dim maquina As String = ""
            Try
                maquina = Dns.GetHostEntry(HttpContext.Current.Request.ServerVariables("REMOTE_ADDR")).HostName
            Catch ex As Exception
            End Try

            'Carga el evento
            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@empleado", empleado), New SqlParameter("@log", log), New SqlParameter("@ip", ip), New SqlParameter("@maquina", maquina)}
                data.EjecutaCommand("GuardaLog", params)
            End Using

        Catch ex As Exception

        End Try
    End Sub
End Class
