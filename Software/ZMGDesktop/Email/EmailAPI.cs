using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email
{
    public static class EmailAPI
    {
        private static SmtpClient smtp = new SmtpClient();
        private static MimeMessage email = new MimeMessage();
        private static Multipart multipart = new Multipart();

        private static void Reset()
        {
            smtp = new SmtpClient();
            email = new MimeMessage();
            multipart = new Multipart();
        }

        public static int DodajPrilog(string path)
        {
            var attachment = new MimePart("image", "gif")
            {
                Content = new MimeContent(File.OpenRead(path)),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = Path.GetFileName(path)
            };
            multipart.Add(attachment);
            return 1;
        }

        public static int NapraviEmail(string from, string to, string subject, string text)
        { 
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            var body = new TextPart(TextFormat.Html) { Text = $"<p>{text}<p>" };
            multipart.Add(body);
            return 1;
        }

        public static int Posalji()
        {
            email.Body = multipart;
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("zastitametalnegalanterije@gmail.com", "vbnosleeonorxcrg");
            smtp.Send(email);
            smtp.Disconnect(true);
            Reset();
            return 1;
        }
    }
}
