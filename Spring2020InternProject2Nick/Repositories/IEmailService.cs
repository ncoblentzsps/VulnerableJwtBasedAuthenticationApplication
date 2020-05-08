using Microsoft.AspNetCore.Identity.UI.Services;
using Spring2020InternProject2Nick.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spring2020InternProject2Nick.Repositories
{
    public interface IEmailService : IEmailSender
    {
        public void EmailTwoFactorCode(HRUser user);        
    }
}
