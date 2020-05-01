using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Spring2020InternProject2Nick.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class InternalController : ControllerBase
    {
        public ActionResult Get()
        {
            string name = this.HttpContext.User.Identity.Name;
            return Ok();
        }
    }
}