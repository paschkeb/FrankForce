using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using DataLibrary.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace FrankForce.Functions
{
    public interface ICallEmailFunction
    {
        Task CallEmailFunctionApp(UserModel userValues);
    }

    public class CallEmailFunction : ICallEmailFunction
    {
        private static readonly HttpClient client = new HttpClient();
        private static string sendEmailFunctionAppAddress;

        public CallEmailFunction(IConfiguration config)
        {
            sendEmailFunctionAppAddress = config.GetValue<string>("SendEmailFunctionAppAddress");
        }

        public async Task CallEmailFunctionApp(UserModel userValues) 
        {
            var json = JsonConvert.SerializeObject(userValues);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var response = await client.PostAsync(sendEmailFunctionAppAddress, stringContent);
            var responseString = await response.Content.ReadAsStringAsync();
        }
    }
}
