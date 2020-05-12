using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using MailKit.Net.Smtp;
using MimeKit;

namespace SchedSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SendMail(string name, string email, string msg)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("kneth.villafuerte@gmail.com"));
            message.To.Add(new MailboxAddress("kenneth.villafuerte@basecamptech.ph"));
            message.Subject = name;
            message.Body = new TextPart("html")
            {
                Text = "From " + name + " <br> " +
                "Contact Information: " + email + "<br>" + 
                "Message:" + msg
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


            return View("Contact");
        }

        private async Task ConfigurSMTP(MimeMessage message)
        {
            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback =
                    (sender, certificate, certChainType, errors) => true;
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                await client.ConnectAsync("in-v3.mailjet.com", 587, false).ConfigureAwait(false);
                await client.AuthenticateAsync("094da7cd3d0c892dcfe90a5877c186d5", "2b009ab4cebba97b8e7af5e00f38b347").ConfigureAwait(false);

                await client.SendAsync(message).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }

    }
}