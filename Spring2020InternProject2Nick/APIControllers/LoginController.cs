using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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
        public async Task<ActionResult<string>> Post([FromBody] LoginRequestViewModel model)
        {
            ResponseStatusViewModel responseModel = new ResponseStatusViewModel();
            if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
            {
                responseModel.Result = false;
                responseModel.Messages.Add(_LoginFailureMessage);
                return new UnauthorizedObjectResult(responseModel);
            }

            HRUser user = await _userManager.FindByNameAsync(model.Username);
            if (user != null)
            {
                SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (result.Succeeded)
                {
                    //await _signInManager.SignInAsync(user,false);
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();                    
                    SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtKey"]));
                    SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, user.Id.ToString())
                        }),
                        Expires = DateTime.UtcNow.AddDays(7),
                        SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature),                        
                    };
                    SecurityToken securityToken = handler.CreateToken(tokenDescriptor);
                    string token = handler.WriteToken(securityToken);
                    return new OkObjectResult(token);
                }
            }

            responseModel.Result = false;
            responseModel.Messages.Add(_LoginFailureMessage);
            return new UnauthorizedObjectResult(responseModel);
        }
    }
}
