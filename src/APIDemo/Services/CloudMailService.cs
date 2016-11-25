using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDemo.Services
{
    public class CloudMailService : IMailService
    {
        private string mailTo = Startup.Configuration["mailSettings:to"];
        private string mailFrom = Startup.Configuration["mailSettings:from"];

        public void SendMail(string subject, string msg)
        {
            Debug.WriteLine($"cloud mail from {mailFrom} to {mailTo}");
            Debug.WriteLine($"cloud subject: {subject} and message: {msg}");
        }
    }
}
