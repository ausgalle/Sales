using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Sales.Web.Auth
{
    public class AuthenticationProviderTest : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var anonimous = new ClaimsIdentity();
            var usmeUser = new ClaimsIdentity(new List<Claim>
        {
            new Claim("FirstName", "Alejandro"),
            new Claim("LastName", "Usme"),
            new Claim(ClaimTypes.Name, "alejandrousme@yopmail.com"),
            new Claim(ClaimTypes.Role, "Admin")
        },
       authenticationType: "test");
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(usmeUser)));
        }
    }
}
