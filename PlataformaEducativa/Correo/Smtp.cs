using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace PlataformaEducativa.Correo
{
    public class Smtp
    {
        private readonly SmtpSettings _smtpSettings;
        public Smtp(SmtpSettings smtpSettings)
        {
            _smtpSettings = smtpSettings;
        }
        public  void  EnviarCorreo(string Correo,string password, string CorreoDestino,string mensa)
        {

            using(MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(_smtpSettings.SenderEmail);
                mail.To.Add(CorreoDestino);
                mail.Subject = "tu Password";
                mail.Body = $"<p>Password:{mensa}";
                mail.IsBodyHtml = true;

                using (SmtpClient smtpClient = new SmtpClient())
                {
                    smtpClient.Host = _smtpSettings.Host;
                    smtpClient.Credentials = new NetworkCredential(_smtpSettings.UserName, _smtpSettings.Password);
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mail);
                }
            }



            //SmtpClient smtp = new SmtpClient("smtp-mail.outlook.com");

            //smtp.Port = 587;
            //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtp.UseDefaultCredentials = false;
            //System.Net.NetworkCredential credential = 
            //    new System.Net.NetworkCredential(Correo,password);
            //smtp.EnableSsl = true;
            //smtp.Credentials = credential;

            //MailMessage message = new MailMessage(Correo,CorreoDestino);
            //message.Subject = "Restablecimiento";
            //message.Body = $"<b style=color:red>Eres un <b> <br> <b>password:</b>{mensa}";
            //message.IsBodyHtml = true;
            //smtp.Send(message);
        }
    }
}
