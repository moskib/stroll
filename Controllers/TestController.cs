using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Stroll.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class TestController: ControllerBase
    {
        [AllowAnonymous]
        public ActionResult TestGet()
        {
            return Ok("Hello there!");
        }
    }
}
