using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Spring2020InternProject2Nick.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spring2020InternProject2Nick.Repositories
{
    public class HRServices : IHRServices
    {

        private ApplicationDbContext _dbContext;
        private IEmailService _emailService;
        private static Random _random = new Random();
        private UserManager<HRUser> _userManager;
        public HRServices(ApplicationDbContext dbContext, IEmailService emailService, UserManager<HRUser> userManager)
        {
            _dbContext = dbContext;
            _emailService = emailService;
            _userManager = userManager;
        }

        public async Task SendTwoFactorCodeAsync(HRUser user)
        {
            int code = _random.Next(0, 999999);
            user.TwoFactorCode = code.ToString("000000");
            user.TwoFactorCodeDateTime = DateTime.Now;
            await _userManager.UpdateAsync(user);
            _emailService.EmailTwoFactorCode(user);            
        }

        public bool ValidateTwoFactorCodeAsync(HRUser user, string code)
        {
            if(user.TwoFactorEnabled && user.TwoFactorCodeDateTime!=null && !string.IsNullOrEmpty(user.TwoFactorCode))
            {
                TimeSpan codeTimeSpan = DateTime.Now - user.TwoFactorCodeDateTime;
                if(codeTimeSpan<=TimeSpan.FromMinutes(5))
                {
                    if(code == user.TwoFactorCode)
                    {
                        user.TwoFactorCode = "";
                        _userManager.UpdateAsync(user);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
