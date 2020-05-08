using Spring2020InternProject2Nick.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spring2020InternProject2Nick.Repositories
{
    public interface IHRServices
    {
        public Task SendTwoFactorCodeAsync(HRUser user);
        public bool ValidateTwoFactorCodeAsync(HRUser user, string code);
    }
}
