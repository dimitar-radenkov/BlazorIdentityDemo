using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorIdentityDemo.Web.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddOidcAuthentication(options =>
            {
                options.ProviderOptions.Authority = "https://localhost:51000"; // IdentityServer
                options.ProviderOptions.ClientId = "blazor-client";
                options.ProviderOptions.ResponseType = "code";
                options.ProviderOptions.DefaultScopes.Add("BlazorClient");
                options.ProviderOptions.DefaultScopes.Add("profile");
                options.ProviderOptions.DefaultScopes.Add("openid");

                options.ProviderOptions.PostLogoutRedirectUri = "authentication/logout-callback";
                options.ProviderOptions.RedirectUri = "authentication/login-callback";
            });

            builder.Services.AddHttpClient("BlazorIdentityDemo.Web.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            builder.Services.AddHttpClient("BlazorIdentityDemo.WebApi", client => client.BaseAddress = new Uri("https://localhost:51020"))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorIdentityDemo.Web.ServerAPI"));
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorIdentityDemo.WebApi"));

            builder.Services.AddApiAuthorization();

            await builder.Build().RunAsync();
        }
    }
}
