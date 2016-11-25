using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDemo.Services
{
    public interface IMailService
    {
        void SendMail(string subject, string msg);
    }
}
