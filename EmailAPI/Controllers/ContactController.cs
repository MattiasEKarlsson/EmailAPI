using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EmailAPI.Controllers
{
   
    public class ContactController : ControllerBase
    {
      

        [HttpPost("sendcontact")]
        public async Task<IActionResult> SendContact([FromBody] Contact contact)
        {
            try
            {
                using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.EnableSsl = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("kenkataservice@gmail.com", "bxieecxvpcowbnsl");

                    MailMessage message = new MailMessage();
                    message.To.Add("kenkataservice@gmail.com");
                    message.From = new MailAddress(contact.Email);
                    message.Subject = (contact.Subject);
                    message.Body = ($"From: {contact.Name}\n Email: {contact.Email}\n Message: {contact.Message}");
                    client.Send(message);

                }
            }
            catch
            {
                return new BadRequestResult();
            }

            return new OkResult();
        }
    }
}
