using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexus.Server.Entity;
using Nexus.Server.Models;
using Nexus.Server.NexusContext;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Nexus.Server.NexusListener;

namespace Nexus.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase 
    {


        [Route(("login"))]
        [HttpPost]
        public JsonResult login()
        {

            ClaimsPrincipal? principal = HttpContext.User;

            if (principal.Identity.IsAuthenticated)
            {
                return new JsonResult("/nexus");
            }
            else
            {
                return new JsonResult("/signup");
            }

        }

        [Route("signup")]
        [HttpPost]
        public async Task<JsonResult> signup([FromBody] S_User u)
        {
                var claims = new List<Claim> { new Claim(ClaimTypes.Email, u.UserEmail) };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

                await Request.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return new JsonResult('/');
        }

        [Route("auth")]
        [HttpGet]
        public JsonResult auth()
        {
            ClaimsPrincipal? principal = HttpContext.User;

            Console.WriteLine(principal.Identity.Name);

            if (principal.Identity.IsAuthenticated)
                return new JsonResult("/nexus");
            else
                return new JsonResult("/login");
        }

        [Route("logout")]
        [HttpPost]
        public async Task Logout()
        {
            await Request.HttpContext.SignOutAsync();
        }
    }
}
