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

        [HttpPost("UserId")]
        public async Task<string> GetUserIdAsync([FromBody] User user) => await userStore.GetUserIdAsync(user);

        [HttpPost("UserName")]
        public async Task<string> GetUserNameAsync([FromBody] User user) => await userStore.GetUserNameAsync(user);

        [HttpPost("UserName/{name}")]
        public async Task SetUserNameAsync([FromBody] User user, string name)
        {
            await userStore.SetUserNameAsync(user, name);
            await userStore.UpdateAsync(user);
        }

        [HttpPost("NormalUserName")]
        public async Task<string> GetNormalizedUserNameAsync([FromBody] User user) => await userStore.GetNormalizedUserNameAsync(user);
        
        [HttpPost("NormalUserName/{name}")]
        public async Task SetNormalizedUserNameAsync([FromBody] User user, string name)
        {
            await userStore.SetNormalizedUserNameAsync(user, name);
            await userStore.UpdateAsync(user);
        }

        [HttpPost("User")]
        public async Task<bool> CreateAsync([FromBody] User user)
        {
            var result = await userStore.CreateAsync(user);
            return result.Succeeded;
        }

        [HttpPost("User")]
        public async Task<bool> UpdateAsync([FromBody] User user)
        {
            var result = await userStore.UpdateAsync(user);
            return result.Succeeded;
        }

        [HttpPost("User")]
        public async Task<bool> DeleteAsync([FromBody] User user)
        {
            var result = await userStore.DeleteAsync(user);
            return result.Succeeded;
        }
    }
}
