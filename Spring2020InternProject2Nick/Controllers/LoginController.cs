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

namespace Spring2020InternProject2Nick.Controllers
{
    [Route("api/[controller]")]
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
       [HttpPost]
        public void Post([FromBody] LoginViewModel model)
        {
            var blah = model;
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
