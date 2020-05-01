using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Spring2020InternProject2Nick.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecondFactorController : ControllerBase
    {
        //[HttpPost]
        //public async Task<ActionResult<string>> Post([FromBody] SecondFactorRequestViewModel model)
        //{ 

        //}
    }

    public class SecondFactorRequestViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int SecondFactorType { get; set; }
        public int SecondFactorValue { get; set; }
    }

}