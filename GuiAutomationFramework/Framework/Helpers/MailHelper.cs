using System;
using MailKit.Net.Smtp;
using MimeKit;
using System.IO;

namespace GuiAutomationFramework.Framework.Helpers
{
    public class MailHelper
    {
        public static void SendMail(string recipients)
        {
            try
            {
                string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Test1.html";
                var attachment = new MimePart("image", "gif")
                {
                    ContentObject = new ContentObject(File.OpenRead(path), ContentEncoding.Default),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = Path.GetFileName(path)
                };
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Chetan Khalkar", "ChetanKhalkar@gmail.com"));
                message.To.Add(new MailboxAddress("Chetan Khalkar", recipients));
                message.Subject = "Test Mail for Mail Module With attachment";

                var multipart = new Multipart("mixed");
                multipart.Add(new TextPart("html") { Text = @"Test mail to send generated reports as an attachments." });
                multipart.Add(attachment);

                message.Body = multipart;

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect("smtp.gmail.com", 587, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    client.Authenticate("chetankhalkar@gmail.com", "Skadoosh!");

                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception e)
            {
                Console.Write("Error : " + e.Message);
            }
        }
    }
}
