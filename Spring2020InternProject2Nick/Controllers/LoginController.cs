using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public LoginController(SignInManager<HRUser> signInManager, UserManager<HRUser> userManager, IHRServices hrServices)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _hrServices = hrServices;
        }

        // GET: api/Authentication
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Authentication/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //POST: api/Login

        private static string _LoginFailureMessage = "Login Failed.";

        [HttpPost]
        public async Task<ActionResult<ResponseStatusViewModel>> Post([FromBody] LoginRequestViewModel model)
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
                    await _signInManager.SignInAsync(user,false);
                    return new OkObjectResult(new ResponseStatusViewModel { Result = true });
                }
            }

            responseModel.Result = false;
            responseModel.Messages.Add(_LoginFailureMessage);
            return new UnauthorizedObjectResult(responseModel);
        }

        //// PUT: api/Authentication/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
