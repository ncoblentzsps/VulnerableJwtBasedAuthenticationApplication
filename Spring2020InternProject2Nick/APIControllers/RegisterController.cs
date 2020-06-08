using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spring2020InternProject2Nick.Models;
using Spring2020InternProject2Nick.Repositories;
using Spring2020InternProject2Nick.ViewModels;

namespace Spring2020InternProject2Nick.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private SignInManager<HRUser> _signInManager;
        private UserManager<HRUser> _userManager;
        private IHRServices _hrServices;
        private RoleManager<IdentityRole> _roleManager;
        public RegisterController(SignInManager<HRUser> signInManager, UserManager<HRUser> userManager, IHRServices hrServices, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _hrServices = hrServices;
            _roleManager = roleManager;
        }

        [HttpPut]
        public async Task<ActionResult<ResponseStatusViewModel>> Put(RegisterRequestViewModel model)
        {
            ResponseStatusViewModel responseModel = new ResponseStatusViewModel();
            responseModel.Result = true;
            if(string.IsNullOrWhiteSpace(model.FirstName))
            {
                responseModel.Result = false;
                responseModel.Messages.Add("First name cannot be blank.");
            }
            if (string.IsNullOrWhiteSpace(model.LastName))
            {
                responseModel.Result = false;
                responseModel.Messages.Add("Last name cannot be blank.");
            }
            if (string.IsNullOrWhiteSpace(model.Username))
            {
                responseModel.Result = false;
                responseModel.Messages.Add("Last name cannot be blank.");
            }
            if (string.IsNullOrWhiteSpace(model.Email))
            {
                responseModel.Result = false;
                responseModel.Messages.Add("Email cannot be blank.");
            }
            if (string.IsNullOrWhiteSpace(model.Password))
            {
                responseModel.Result = false;
                responseModel.Messages.Add("Password cannot be blank.");
            }
            if (!responseModel.Result)
                return new BadRequestObjectResult(responseModel);

            HRUser hruser = new HRUser()
            {
                Email = model.Email,
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                TwoFactorEnabled = true
            };
            IdentityResult result = await _userManager.CreateAsync(hruser, model.Password);
            if(result.Succeeded)
            {
                IdentityRole userRole = _roleManager.Roles.FirstOrDefault(r => r.Name == "User");
                if(userRole==null)
                {
                    await _roleManager.CreateAsync(new IdentityRole("User"));
                }
                await _userManager.AddToRoleAsync(hruser, userRole.Name);
                responseModel.Result = true;
                responseModel.Messages.Add("Thank you for registering your account.");
                return new OkObjectResult(responseModel);
            }
            else
            {
                responseModel.Result = false;
                responseModel.Messages.Add("Unable to create your user account.");
                return new BadRequestObjectResult(responseModel);
            }

        }

    }
}