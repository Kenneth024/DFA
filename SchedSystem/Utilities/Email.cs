using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchedSystem.Utilities
{
    public class Email
    {
        public void SendMail(string to, string link)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("kneth.villafuerte@gmail.com"));
            message.To.Add(new MailboxAddress(to));
            message.Subject = "DFA Verification";
            message.Body = new TextPart("html")
            {
                Text = $"<a href=\"{link}\">Verify</a>" +
                    $"<br> If the above link is not working, please copy this url and paste to your browser" +
                    $"<br> {link}"
            };

            using (var client = new SmtpClient())
            {
                //client.ServerCertificateValidationCallback =
                //    (sender, certificate, certChainType, errors) => true;
                //client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Connect("smtp-relay.sendinblue.com", 587);
                //client.SslProtocols = System.Security.Authentication.SslProtocols.Ssl2;
                //client.Capabilities &= ~SmtpCapabilities.Chunking;
                //client.Capabilities &= ~SmtpCapabilities.BinaryMime;
                client.Authenticate("kneth.villafuerte@gmail.com", "hIdBYvGzkCDXASZq");

                client.Send(message);
                client.Disconnect(true);
            }

        }
    }
}