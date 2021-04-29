using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using System.Net;
using System.Net.Mail;


namespace EmailAPI.Controllers
{
    public class EmailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("sendemail")]
        public async Task<IActionResult> SendEmail([FromBody] Email email)
        {
            try
            {
              
                    using(SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
                    {
                        client.EnableSsl = true;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential("kenkataservice@gmail.com", "password");

                        MailMessage message = new MailMessage();
                        message.To.Add(email.To);
                        message.From = new MailAddress("Mattias@outlook.com");
                        message.Subject = "Welcome New Subscriber!!";
                        message.Body = "It also features quite detailed e-mail tracking";
                        client.Send(message);


                        MailMessage mess = new MailMessage();
                        mess.To.Add("kenkataservice@gmail.com");
                        mess.From = new MailAddress("Mattias@outlook.com");
                        mess.Subject = "A New Subscriber!!";
                        mess.Body = $"{email.To} is now subscribing!";
                        client.Send(mess);




                       

                    }
            }
            catch {
                return new BadRequestResult();
            }

            return new OkResult();
        }
    }
}
