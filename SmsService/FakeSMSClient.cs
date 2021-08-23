using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmsService
{
    public class FakeSMSClient : ISMSClient
    {
        public Task Send(string from, string to, string message)
        {
            return Task.CompletedTask;
        }
    }
}
