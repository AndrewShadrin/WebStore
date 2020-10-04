using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Clients.Base;
using WebStore.Domain;
using WebStore.Domain.DTO.Identity;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces.Services.Identity;

namespace WebStore.Clients.Identity
{
    public class UsersClient : BaseClient, IUsersClient
    {
        public UsersClient(IConfiguration configuration) : base(configuration, WebApi.Identity.Users){}

        public async Task AddClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            await PostAsync($"{serviceAddress}/AddClaims", new AddClaimDTO { User = user, Claims = claims }, cancellationToken);
        }

        public async Task AddLoginAsync(User user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            await PostAsync($"{serviceAddress}/AddLogin", new AddLoginDTO { User = user, UserLoginInfo = login }, cancellationToken);
        }

        public async Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            await PostAsync($"{serviceAddress}/Role/{roleName}", user, cancellationToken);
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            return await (await PostAsync($"{serviceAddress}/User", user, cancellationToken))
                .Content
                .ReadAsAsync<bool>(cancellationToken)
                ? IdentityResult.Success
                : IdentityResult.Failed();
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            return await (await PostAsync($"{serviceAddress}/User/Delete", user, cancellationToken))
                .Content
                .ReadAsAsync<bool>(cancellationToken)
                ? IdentityResult.Success
                : IdentityResult.Failed();
        }

        public async Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return await GetAsync<User>($"{serviceAddress}/User/FindByEmail/{normalizedEmail}", cancellationToken);
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await GetAsync<User>($"{serviceAddress}/User/Find/{userId}", cancellationToken);
        }

        public async Task<User> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            return await GetAsync<User>($"{serviceAddress}/User/FindByLogin/{loginProvider}/{providerKey}", cancellationToken);
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return await GetAsync<User>($"{serviceAddress}/User/Normal/{normalizedUserName}", cancellationToken);
        }

        public async Task<int> GetAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            return await(await PostAsync($"{serviceAddress}/GetAccessFailedCount", user, cancellationToken))
                .Content
                .ReadAsAsync<int>(cancellationToken);
        }

        public async Task<IList<Claim>> GetClaimsAsync(User user, CancellationToken cancellationToken)
        {
            return await(await PostAsync($"{serviceAddress}/GetClaims", user, cancellationToken))
                .Content
                .ReadAsAsync<List<Claim>>(cancellationToken);
        }

        public async Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            return await(await PostAsync($"{serviceAddress}/GetEmail", user, cancellationToken))
                .Content
                .ReadAsAsync<string>(cancellationToken);
        }

        public async Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            return await(await PostAsync($"{serviceAddress}/GetEmailConfirmed", user, cancellationToken))
                .Content
                .ReadAsAsync<bool>(cancellationToken);
        }

        public async Task<bool> GetLockoutEnabledAsync(User user, CancellationToken cancellationToken)
        {
            return await(await PostAsync($"{serviceAddress}/GetLockoutEnabled", user, cancellationToken))
                .Content
                .ReadAsAsync<bool>(cancellationToken);
        }

        public async Task<DateTimeOffset?> GetLockoutEndDateAsync(User user, CancellationToken cancellationToken)
        {
            return await(await PostAsync($"{serviceAddress}/GetLockoutEndDate", user, cancellationToken))
                .Content
                .ReadAsAsync<DateTimeOffset>(cancellationToken);
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(User user, CancellationToken cancellationToken)
        {
            return await (await PostAsync($"{serviceAddress}/GetLogins", user, cancellationToken))
                .Content
                .ReadAsAsync<List<UserLoginInfo>>(cancellationToken);
        }

        public async Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            return await(await PostAsync($"{serviceAddress}/GetNormalizedEmail", user, cancellationToken))
                .Content
                .ReadAsAsync<string>(cancellationToken);
        }

        public async Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return await(await PostAsync($"{serviceAddress}/NormalUserName", user, cancellationToken))
                .Content
                .ReadAsAsync<string>(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return await (await PostAsync($"{serviceAddress}/GetPasswordHash", user, cancellationToken))
                .Content
                .ReadAsAsync<string>(cancellationToken);
        }

        public async Task<string> GetPhoneNumberAsync(User user, CancellationToken cancellationToken)
        {
            return await(await PostAsync($"{serviceAddress}/GetPhoneNumber", user, cancellationToken))
                .Content
                .ReadAsAsync<string>(cancellationToken);
        }

        public async Task<bool> GetPhoneNumberConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            return await(await PostAsync($"{serviceAddress}/GetPhoneNumberConfirmed", user, cancellationToken))
                .Content
                .ReadAsAsync<bool>(cancellationToken);
        }

        public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            return await (await PostAsync($"{serviceAddress}/roles", user, cancellationToken))
                .Content
                .ReadAsAsync<IList<string>>(cancellationToken);
        }

        public async Task<bool> GetTwoFactorEnabledAsync(User user, CancellationToken cancellationToken)
        {
            return await(await PostAsync($"{serviceAddress}/GetTwoFactorEnabled", user, cancellationToken))
                .Content
                .ReadAsAsync<bool>(cancellationToken);
        }

        public async Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return await(await PostAsync($"{serviceAddress}/UserId", user, cancellationToken))
                .Content
                .ReadAsAsync<string>(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return await(await PostAsync($"{serviceAddress}/UserName", user, cancellationToken))
                .Content
                .ReadAsAsync<string>(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IList<User>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            return await (await PostAsync($"{serviceAddress}/GetUsersForClaim", claim, cancellationToken))
                .Content
                .ReadAsAsync<List<User>>(cancellationToken);
        }

        public async Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            return await GetAsync<List<User>>($"{serviceAddress}/UsersInRole/{roleName}", cancellationToken);
        }

        public async Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return await (await PostAsync($"{serviceAddress}/HasPassword", user, cancellationToken))
                .Content
                .ReadAsAsync<bool>(cancellationToken);
        }

        public async Task<int> IncrementAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            return await (await PostAsync($"{serviceAddress}/IncrementAccessFailedCount", user, cancellationToken))
                .Content
                .ReadAsAsync<int>(cancellationToken);
        }

        public async Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            return await(await PostAsync($"{serviceAddress}/InRole/{roleName}", user, cancellationToken))
                .Content
                .ReadAsAsync<bool>(cancellationToken);
        }

        public async Task RemoveClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            await PostAsync($"{serviceAddress}/RemoveClaims", new RemoveClaimDTO { User = user, Claims = claims }, cancellationToken);
        }

        public async Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            await PostAsync($"{serviceAddress}/Role/Delete/{roleName}", user, cancellationToken);
        }

        public async Task RemoveLoginAsync(User user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            await PostAsync($"{serviceAddress}/RemoveLogin/{loginProvider}/{providerKey}", user, cancellationToken);
        }

        public async Task ReplaceClaimAsync(User user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            await PostAsync($"{serviceAddress}/ReplaceClaim", new ReplaceClaimDTO { User = user, Claim = claim, NewClaim = newClaim }, cancellationToken);
        }

        public async Task ResetAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            await PostAsync($"{serviceAddress}/ResetAccessFailedCount", user, cancellationToken);
        }

        public async Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            await PostAsync($"{serviceAddress}/SetEmail/{email}", user, cancellationToken);
        }

        public async Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            await PostAsync($"{serviceAddress}/SetEmailConfirmed/{confirmed}", user, cancellationToken);
        }

        public async Task SetLockoutEnabledAsync(User user, bool enabled, CancellationToken cancellationToken)
        {
            user.LockoutEnabled = enabled;
            await PostAsync($"{serviceAddress}/SetLockoutEnabled/{enabled}", user, cancellationToken);
        }

        public async Task SetLockoutEndDateAsync(User user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            user.LockoutEnd = lockoutEnd;
            await PostAsync($"{serviceAddress}/SetLockoutEndDate", new SetLockoutDTO { User = user, LockoutEnd = lockoutEnd }, cancellationToken);
        }

        public async Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            await PostAsync($"{serviceAddress}/SetNormalizedEmail/{normalizedEmail}", user, cancellationToken);
        }

        public async Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            await PostAsync($"{serviceAddress}/NormalUserName/{normalizedName}", user, cancellationToken);
        }

        public async Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            await PostAsync($"{serviceAddress}/SetPasswordHash", new PasswordHashDTO { User = user, Hash = passwordHash }, cancellationToken);
        }

        public async Task SetPhoneNumberAsync(User user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;
            await PostAsync($"{serviceAddress}/SetPhoneNumber/{phoneNumber}", user, cancellationToken);
        }

        public async Task SetPhoneNumberConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;
            await PostAsync($"{serviceAddress}/SetPhoneNumberConfirmed/{confirmed}", user, cancellationToken);
        }

        public async Task SetTwoFactorEnabledAsync(User user, bool enabled, CancellationToken cancellationToken)
        {
            user.TwoFactorEnabled = enabled;
            await PostAsync($"{serviceAddress}/SetTwoFactorEnabled/{enabled}", user, cancellationToken);
        }

        public async Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            await PostAsync($"{serviceAddress}/UserName/{userName}", user, cancellationToken);
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            return await(await PutAsync($"{serviceAddress}/User", user, cancellationToken))
                .Content
                .ReadAsAsync<bool>(cancellationToken)
                ? IdentityResult.Success
                : IdentityResult.Failed();
        }
    }
}
