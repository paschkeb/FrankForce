using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;
using DataLibrary.Models;
using System.Collections.Generic;
using System.Configuration;

namespace SendEmailApp
{
    public static class SendEmailFunction { 

        [FunctionName("SendEmailFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {          
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<UserModel>(requestBody);
                       
            SendEmailUsingGmail(data);

            return new OkResult();
        }

        private static void SendEmailUsingGmail(UserModel userInformation)
        {
            var fromAddress = new MailAddress("FrankForceConfirmation@gmail.com", "Frank");
            var toAddress = new MailAddress(userInformation.EmailAddress, userInformation.FirstName);
            string fromPassword = Environment.GetEnvironmentVariable("EmailPassword");
            const string subject = "Congratulations on being registered for FrankForce!";
            string body = $"Hello {userInformation.FirstName} {userInformation.LastName}! \r\n Your Phone number has been recorded as {userInformation.PhoneNumber} \r\n Congratulations on being registered for FrankForce!";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }

        }
    }
}
