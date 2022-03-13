using System.Collections.Generic;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace WebApp.Identity
{
    public static class ISConfiguration
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("StoryBlogWebAPI", "Web API"),
                new ApiScope("blazor", "Blazor WebAssembly")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("StoryBlogWebAPI", "Web API", new [] { JwtClaimTypes.Name})
                {
                    Scopes = {"StoryBlogWebAPI"}
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "story-blog-web-app",
                    ClientName = "Story Blog Web",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    RequirePkce = true,
                    RedirectUris =
                    {
                        "https://localhost:41678/authentication/login-callback"
                    },
                    AllowedCorsOrigins =
                    {
                        "http://localhost:41678"
                    },
                    PostLogoutRedirectUris =
                    {
                        "http://localhost:41678"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "StoryBlogWebAPI",
                        "blazor"
                    },
                    AllowAccessTokensViaBrowser = true
                }
            };
    }
}
