using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Clients.Base;
using WebStore.Domain;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces.Services.Identity;

namespace WebStore.Clients.Identity
{
    public class RolesClient : BaseClient, IRolesClient
    {
        public RolesClient(IConfiguration configuration) : base(configuration, WebApi.Identity.Roles){}

        public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            return await (await PostAsync(serviceAddress, role, cancellationToken))
                .Content
                .ReadAsAsync<bool>(cancellationToken)
                ? IdentityResult.Success
                : IdentityResult.Failed();
        }

        public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            return await(await PostAsync($"{serviceAddress}/Delete", role, cancellationToken))
                .Content
                .ReadAsAsync<bool>(cancellationToken)
                ? IdentityResult.Success
                : IdentityResult.Failed();
        }

        public async Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return await GetAsync<Role>($"{serviceAddress}/FindById/{roleId}", cancellationToken);
        }

        public async Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return await GetAsync<Role>($"{serviceAddress}/FindByName/{normalizedRoleName}", cancellationToken);
        }

        public async Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            return await(await PostAsync($"{serviceAddress}/GetNormalizedRoleName", role, cancellationToken))
                .Content
                .ReadAsAsync<string>(cancellationToken);
        }

        public async Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
        {
            return await (await PostAsync($"{serviceAddress}/GetRoleId", role, cancellationToken))
                .Content
                .ReadAsAsync<string>(cancellationToken);
        }

        public async Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            return await(await PostAsync($"{serviceAddress}/GetRoleName", role, cancellationToken))
                .Content
                .ReadAsAsync<string>(cancellationToken);
        }

        public async Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedName;
            await PostAsync($"{serviceAddress}/SetNormalizedRoleName/{normalizedName}", role, cancellationToken);
        }

        public async Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            await PostAsync($"{serviceAddress}/SetRoleName/{roleName}", role, cancellationToken);
        }

        public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            return await(await PutAsync(serviceAddress, role, cancellationToken))
                .Content
                .ReadAsAsync<bool>(cancellationToken)
                ? IdentityResult.Success
                : IdentityResult.Failed();
        }
    }
}
