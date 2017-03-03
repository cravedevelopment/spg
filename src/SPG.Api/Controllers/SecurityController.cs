using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SPG.Data.EF;
using SPG.Data.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SPG.Api.Controllers
{
    [Route("api/[controller]")]
    public class SecurityController : Controller
    {
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly IRepository<User> _repoUser;
        public SecurityController(IRepository<User> repoUser)
        {
            _repoUser = repoUser;
        }

        [HttpPost]
        public IActionResult LoginAttempt([FromBody]User applicationUser)
        {
            var response = _repoUser.Get(u => u.UserName == applicationUser.UserName && u.Password == applicationUser.Password);

            var json = JsonConvert.SerializeObject(response, _serializerSettings);
            return new OkObjectResult(json);
        }
    }
}
