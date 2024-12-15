using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace B221200015_WP_ODEV.Helper
{
    public interface IMailHelper
    {
        void Gonder(string email, string konu, string mesaj);
    }

    public class MailHelper : IMailHelper
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;

        public MailHelper(IConfiguration configuration)
        {
            // appsettings.json'dan SMTP ayarlarını al
            _smtpHost = configuration["Smtp:Host"];
            _smtpPort = int.Parse(configuration["Smtp:Port"]);
            _smtpUsername = configuration["Smtp:Username"];
            _smtpPassword = configuration["Smtp:Password"];
        }

        /// <summary>
        /// Tek bir e-posta gönderir.
        /// </summary>
        /// <param name="to">Alıcı e-posta adresi</param>
        /// <param name="subject">E-posta konusu</param>
        /// <param name="body">E-posta içeriği</param>
        public void Gonder(string to, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Pediatri Nöbet Sistemi", _smtpUsername));
                message.To.Add(new MailboxAddress("", to));
                message.Subject = subject;
                message.Body = new TextPart("plain")
                {
                    Text = body
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate("wpodevpy@gmail.com", "fdro hffq mkky qbdd\r\n"); // Uygulama şifresini kullanın
                    client.Send(message);
                    client.Disconnect(true);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("E-posta gönderimi sırasında bir hata oluştu.", ex);
            }
        }

        /// <summary>
        /// Birden fazla e-postayı toplu olarak gönderir.
        /// </summary>
        /// <param name="recipients">Alıcıların e-posta adresleri</param>
        /// <param name="subject">E-posta konusu</param>
        /// <param name="body">E-posta içeriği</param>
        public void TopluGonder(IEnumerable<string> recipients, string subject, string body)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect(_smtpHost, _smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate(_smtpUsername, _smtpPassword);

                    foreach (var to in recipients.Where(email => !string.IsNullOrEmpty(email)))
                    {
                        var message = new MimeMessage();
                        message.From.Add(new MailboxAddress("Pediatri Nöbet Yönetim Sistemi", _smtpUsername));
                        message.To.Add(new MailboxAddress("", to));
                        message.Subject = subject;
                        message.Body = new TextPart("plain")
                        {
                            Text = body
                        };

                        client.Send(message);
                    }

                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Toplu e-posta gönderimi sırasında bir hata oluştu.", ex);
            }
        }
    }
}
