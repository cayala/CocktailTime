using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmsService
{
    public interface ISMSClient
    {
        Task Send(string from, string to, string message);
    }
}
