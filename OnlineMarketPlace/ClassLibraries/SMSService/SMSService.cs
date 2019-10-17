//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OnlineMarketPlace.Areas.Identity.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace OnlineMarketPlace.ClassLibraries.SMSService
{
    public class SMSService : ISMSService
    {
        private static readonly HttpClient client = new HttpClient();
        private List<SMSResult> smsResults = new List<SMSResult>();

        private string _SMSApi;
        public SMSService(string SMSApi)
        {
            _SMSApi = SMSApi;
            var b = Task.Run(() => this.GetBalance());
            var balance = b.Result;
        }
        public void BalanceAlert(int balance, List<string> nums)
        {
            if (balance <= 1000)
            {
                SendSMS(nums, "موجودی پنل ارسال پیامک شما رو به اتمام میباشد");
            }
        }

        public async Task<int> GetBalance()
        {
            try
            {
                //Uri targetUri = new Uri($"https://api.sabanovin.com/v1/{_SMSApiKey}/account/balance.json");
                Uri targetUri = new Uri(_SMSApi);
                var response = await client.GetAsync(targetUri);

                string json;
                using (var content = response.Content)
                {
                    json = await content.ReadAsStringAsync();
                }

                var balance = JsonConvert.DeserializeObject<BalanceResult>(json);

                return balance.Entry.Balance;
            }
            catch (Exception )
            {
                return -1;
            }
        }

        //[ValidateAntiForgeryToken]
        public void SendSMS(List<string> to, string message, bool saveHistory = false)
        {
            try
            {
                smsResults.Clear();
                foreach (var item in to)
                {
                    //Uri targetUri = new Uri($"https://api.sabanovin.com/v1/{_SMSApiKey}/sms/send.json?gateway={_SMSProviderNumber}&to={item}&text={message}");
                    Uri targetUri = new Uri(_SMSApi);

                    System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(targetUri);

                    request.Credentials = CredentialCache.DefaultCredentials;
                    // Get the response.  
                    WebResponse response = request.GetResponse();
                    //HttpWebResponse myHttpWebResponse = (HttpWebResponse)request.GetResponse();
                }
            }
            catch (Exception )
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                throw new NotImplementedException();
            }
        }

        public List<SMSResult> SMSResults()
        {
            return smsResults;
        }
    }
}
