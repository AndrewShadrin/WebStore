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
    [Route(WebApi.Identity.Roles)]
    [ApiController]
    public class RolesApiController : ControllerBase
    {
        private readonly RoleStore<Role> roleStore;

        public RolesApiController(WebStoreDB dB)
        {
            roleStore = new RoleStore<Role>(dB);
        }

        [HttpGet("all")]
        public async Task<IEnumerable<Role>> GetAllRoles() => await roleStore.Roles.ToArrayAsync();

        [HttpPost]
        public async Task<bool> CreateAsync(Role role)
        {
            var creationResult = await roleStore.CreateAsync(role);
            return creationResult.Succeeded;
        }

        [HttpPut]
        public async Task<bool> UpdateAsync(Role role)
        {
            var result = await roleStore.UpdateAsync(role);
            return result.Succeeded;
        }

        [HttpPost("Delete")]
        public async Task<bool> DeleteAsync(Role role)
        {
            var result = await roleStore.DeleteAsync(role);
            return result.Succeeded;
        }

        [HttpPost("GetRoleId")]
        public async Task<string> GetRoleIdAsync([FromBody] Role role) => await roleStore.GetRoleIdAsync(role);

        [HttpPost("GetRoleName")]
        public async Task<string> GetRoleNameAsync([FromBody] Role role) => await roleStore.GetRoleNameAsync(role);

        [HttpPost("SetRoleName/{name}")]
        public async Task SetRoleNameAsync([FromBody] Role role, string name)
        {
            await roleStore.SetRoleNameAsync(role, name);
            await roleStore.UpdateAsync(role);
        }

        [HttpPost("GetNormalizedRoleName")]
        public async Task<string> GetNormalizedRoleNameAsync([FromBody] Role role) => await roleStore.GetNormalizedRoleNameAsync(role);

        [HttpPost("SetNormalizedRoleName/{name}")]
        public async Task SetNormalizedRoleNameAsync([FromBody] Role role, string name)
        {
            await roleStore.SetNormalizedRoleNameAsync(role, name);
            await roleStore.UpdateAsync(role);
        }

        [HttpGet("FindById/{id}")]
        public async Task<Role> FindByIdAsync(string id) => await roleStore.FindByIdAsync(id);

        [HttpGet("FindByName/{name}")]
        public async Task<Role> FindByNameAsync(string name) => await roleStore.FindByNameAsync(name);



    }
}
