using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DataContext.WebCoreApp;
using WebCoreApp.Enume;
using WebCoreApp.Models;
using WebCoreApp.Service.Interfaces;

namespace WebCoreApp.Controllers
{
    [Produces("application/json")]
    [Route("api/ApiAccount")]
    public class ApiAccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public ApiAccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/ApiAccount
        [HttpGet("get")]
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        public void Get()
        {
      
        }

        // GET: api/ApiAccount/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id,string search)
        {
            (IEnumerable<AppUser> data, int totals, int filter) tbl = await _userRepository.GetTable(id, 5, "", "", "search");
            return Ok(tbl.data);
        }

        // POST: api/ApiAccount/token
        [HttpPost("token")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            Status result = await _userRepository.IsUser(model.UserName, model.PassWord);
                if(result == Status.Successed)
            {
                var tbl = await _userRepository.GetByMa(model.UserName);
                string display = tbl.MaCvNavigation.Display.ToString();
                string to = tbl.MaTo == null ? "" : tbl.MaTo;
                DateTime dateTime = DateTime.Now.AddDays(14);
                List<Claim> claims = new List<Claim>
                    {
                         new Claim(ClaimTypes.Name, model.UserName),
                         new Claim("Ma_BP",tbl.MaBp),
                         new Claim("Ma_To",to),
                
                         new Claim("Display",display,ClaimValueTypes.Integer),
                };


                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("My Securety Key for ptsc.com.vn"));
                SigningCredentials sigInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
                JwtSecurityToken tokenstring = new JwtSecurityToken(
                                    issuer:"http://ptscqng.com.vn",
                                    audience: "http://ptscqng.com.vn",
                                    claims: claims,
                                    expires: dateTime,
                                    signingCredentials:sigInCred
                                    );
                string token_key = new JwtSecurityTokenHandler().WriteToken(tokenstring);
                var token = new
                {
                    access_token=token_key,
                    expires_in= dateTime
                };
                return Ok(token);
            }
            return BadRequest("Error");
        }

        // PUT: api/ApiAccount/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}