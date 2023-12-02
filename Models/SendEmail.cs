using System.Net.Mail;
using System.Net;

namespace GraduationProject.Models
{
    public class SendEmail
    {
        
    
        
            public SendEmail()
            {

            }
            public static bool EmailSend(string SenderEmail, string Subject, string Message, bool IsBodyHtml = false)
            {
                bool status = false;
                try
                {
                    var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json");
                    var configuration = builder.Build();

                    string HostAddress = configuration.GetSection("EmailSettings:Host").Value;
                    string Port = configuration.GetSection("EmailSettings:Port").Value;
                    string FormEmailId = configuration.GetSection("EmailSettings:MailFrom").Value;
                    string Password = configuration.GetSection("EmailSettings:Password").Value;
                    //string HostAddress = ConfigurationManager.AppSettings["Host"].ToString();
                    //string FormEmailId = ConfigurationManager.AppSettings["MailFrom"].ToString();
                    //string Password = ConfigurationManager.AppSettings["Password"].ToString();
                    //string Port = ConfigurationManager.AppSettings["Port"].ToString();
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(FormEmailId);
                    mailMessage.Subject = Subject;
                    mailMessage.Body = Message;
                    mailMessage.IsBodyHtml = IsBodyHtml;
                    mailMessage.To.Add(new MailAddress(SenderEmail));
                    SmtpClient smtp = new SmtpClient
                    {
                        Host = HostAddress,
                        EnableSsl = true
                    };
                    NetworkCredential networkCredential = new NetworkCredential
                    {
                        UserName = mailMessage.From.Address,
                        Password = Password
                    };
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = networkCredential;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Port = Convert.ToInt32(Port);
                    smtp.Send(mailMessage);
                    status = true;
                    return status;
                }
                catch (Exception e)
                {
                    return status;
                }
            }
        }
    }


