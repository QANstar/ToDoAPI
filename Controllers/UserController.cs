using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDo.Entity;
using ToDo.Model;

namespace ToDoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        QAN_TododbContext Context;
        public UserController(QAN_TododbContext context)
        {
            Context = context;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [EnableCors("any")]
        [HttpPost]
        public IActionResult Login(UserModel user)
        {
            var result = Context.User.Where(x => x.UserName == user.userName && x.Password == user.password);
            if(result.Count()>0)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                    new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(60*24*7)).ToUnixTimeSeconds()}"),
                    new Claim(ClaimTypes.Name, user.userName)
                };
                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("QANstarAndSuoMi1931"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: "QANstar",
                    audience: "QANstar",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(jwtToken);
            }
            else
            {
                return BadRequest();
            }
            
        }

        [EnableCors("any")]
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("test");
        }

        [EnableCors("any")]
        [HttpGet]
        [Authorize]
        public IActionResult Test2()
        {
            return Ok("test");
        }
    }
}
