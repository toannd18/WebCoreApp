using DataContext.WebCoreApp;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCoreApp.Infrastructure.Interfaces;

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public void Get()
        {
        }

        // GET: api/ApiAccount/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id, string search)
        {
            (IEnumerable<AppUser> data, int totals, int filter) tbl = await _userRepository.GetTable(id, 5, "", "", "search");
            return Ok(tbl.data);
        }

        // POST: api/ApiAccount/token

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