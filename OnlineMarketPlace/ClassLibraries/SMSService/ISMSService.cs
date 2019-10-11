using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.ClassLibraries.SMSService
{
    public interface ISMSService
    {
        void SendSMS(List<string> to, string message, bool saveHistory);
        Task<int> GetBalance();
        List<SMSResult> SMSResults();
        void BalanceAlert(int balance, List<string> num);
    }

    //Dummy Models
    public class SMSResult
    {
        public string Number { get; set; }
        public bool Result { get; set; }
    }
    public class BalanceResult
    {
        public Entry Entry { get; set; }
        public Status Status { get; set; }
    }
    public class Entry
    {
        public int Balance { get; set; }
    }
    public class Status
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
