using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Spring2020InternProject2Nick.Models;
using Spring2020InternProject2Nick.Repositories;
using Spring2020InternProject2Nick.ViewModels;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Spring2020InternProject2Nick.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]    
    public class LoginController : ControllerBase
    {
        private SignInManager<HRUser> _signInManager;
        private UserManager<HRUser> _userManager;
        private IHRServices _hrServices;
        private IConfiguration _configuration;        

        private static string _LoginFailureMessage = "Login Failed.";

        public LoginController(SignInManager<HRUser> signInManager, UserManager<HRUser> userManager, IHRServices hrServices, IConfiguration configration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _hrServices = hrServices;
            _configuration = configration;            
        }
        
        [HttpPost]        
        public async Task<ActionResult> Post([FromBody] LoginRequestViewModel model)
        {                        
            if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
            {                
                return new UnauthorizedResult();
            }

            HRUser user = await _userManager.FindByNameAsync(model.Username);
            if (user != null)
            {
                SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (result.Succeeded)
                {
                    //await _signInManager.SignInAsync(user,false);
                    
                    await _hrServices.SendTwoFactorCodeAsync(user);
                    return new OkResult();
                }
            }            
            return new UnauthorizedResult();
        }
    }


}
