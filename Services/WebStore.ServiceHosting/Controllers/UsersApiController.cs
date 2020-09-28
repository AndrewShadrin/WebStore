using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.Entities.Identity;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebApi.Identity.Users)]
    [ApiController]
    public class UsersApiController : ControllerBase
    {
        private readonly UserStore<User, Role, WebStoreDB> userStore;
        public UsersApiController(WebStoreDB dB)
        {
            userStore = new UserStore<User, Role, WebStoreDB>(dB);
        }

        [HttpGet("all")]
        public async Task<IEnumerable<User>> GetAllUsers() => await userStore.Users.ToArrayAsync();
    }
}
