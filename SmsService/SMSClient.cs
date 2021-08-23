using Azure.Communication;
using Azure.Communication.Sms;
using System;
using System.Threading.Tasks;

namespace SmsService
{
    public class SMSClient: SmsClient, ISMSClient
    {
        public SMSClient(string connectionString) : base(connectionString) { }

        public async Task Send(string from, string to, string message)
            => await SendAsync(new PhoneNumber(from), new PhoneNumber(to), message);
    }
}
