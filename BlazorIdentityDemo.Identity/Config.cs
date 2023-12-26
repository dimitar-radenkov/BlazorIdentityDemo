using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace BlazorIdentityDemo.Identity;

public static class Config
{
    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientId = "blazor-client",
                ClientName = "Blazor Client",
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = false,
                RequireClientSecret = false,
                AllowedScopes =
                {
                    "BlazorClient",
                    "openid",
                    "profile",
                },
                RedirectUris = { "https://localhost:51010/authentication/login-callback" },
                PostLogoutRedirectUris = { "https://localhost:51010/authentication/logout-callback" },
                Enabled = true
            }
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("BlazorClient", "Blazor Client"),
            new ApiScope("openid", "OpenID"),
            new ApiScope("profile", "Profile")
        };

    public static List<TestUser> TestUsers =>
        new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "80e2c322-027b-4626-b8a6-ce0fa4f61ea1",
                Username = "mitko",
                Password = "mitko123",
                Claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Mitko"),
                    new Claim(ClaimTypes.Email, "mitko@gmail.com"),
                },
            },
        };
}
