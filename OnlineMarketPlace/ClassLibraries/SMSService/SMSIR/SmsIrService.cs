using SmsIrRestfulNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.ClassLibraries.SMSService.SMSIR
{
    public class SmsIrService
    {
        public static string TokenSmsIr(string ApiKey, string SecurityCode)
        {
           // string tokenSmsIR = new Token().GetToken("f74ab3eb3528148b36eeb9d8", "sadrihossein!@#1234");
            string tokenSmsIR = new Token().GetToken(ApiKey, SecurityCode);

            return (tokenSmsIR);
        }
        public static bool SendSms(string ApiKey, string SecurityCode, string lineNumber, string message, string mobileNumber)
        {
            MessageSendObject messageSendObject = new MessageSendObject()
            {
                Messages = new List<string> { message }.ToArray(),
                MobileNumbers = new List<string> { mobileNumber }.ToArray(),
                LineNumber = lineNumber,
                SendDateTime = null,
                CanContinueInCaseOfError = true
            };

            string tokenSmsIR = TokenSmsIr(ApiKey, SecurityCode);
            MessageSendResponseObject messageSendResponseObject = new MessageSend().Send(tokenSmsIR, messageSendObject);

            if (messageSendResponseObject.IsSuccessful)
            {
                return true;
            }
            return false;
        }
    }
}
