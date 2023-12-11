using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Nexus.Server.Entity;
using Nexus.Server.Models;
using Nexus.Server.NexusListener;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace Nexus.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServerController : ControllerBase
    {

        private ListenerX listener;
        private readonly IConfiguration _configuration;

        public ServerController(ListenerX server, IConfiguration configuration)
        {
            listener = server;
            _configuration = configuration;
        }


        [Route("login")]
        [HttpPost]
        public async Task<JsonResult> login([FromBody] L_User user)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.UserName) };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

            await Request.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            bool? isLoggedIn = await listener.Login(user);
            if (isLoggedIn == true)
            {

            }
            return new JsonResult(isLoggedIn);
        }

        
        [Route("signup")]
        [HttpPost]
        public async Task<JsonResult> signup([FromBody] S_User user)
        {
            return new JsonResult(await listener.SignUp(user));
        }

        [Route("close")]
        [HttpPost]
        public async Task close()
        {
            await listener.Dispose();
        }

        
        [Route("setuser")]
        [HttpPost]
        public async Task<JsonResult> setUser([FromBody] S_User user)
        {

            return new JsonResult(listener.SetListenerUserData(user.UserName, user.UserEmail));
        }


        [Route("send")]
        [HttpPost]
        public JsonResult sendMessage(string message)
        {
            //if (listener != null)
            //{
            //    await listener.InvokeSend(message);
            //    return new JsonResult("Good");
            //}
            //else
            //{
            //    return new JsonResult("Bad");

            //}


            return new JsonResult(true);
        }


        private async Task<SecurityToken> CreateCookies(L_User u)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Keys:JWT"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                     new Claim(ClaimTypes.Name, u.UserName),
                }),

                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            await Console.Out.WriteLineAsync(token.UnsafeToString() + "\n");

            return token;

        }


    }
}
