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
                //new ApiScope("blazor", "Blazor WebAssembly")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
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
                    ClientId = "app.blazor",
                    ClientName = "Story Blog Web Blazor",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    RequirePkce = true,
                    AllowedCorsOrigins = { "https://localhost:44378" },
                    RedirectUris = { "https://localhost:44378/authentication/login-callback" },
                    PostLogoutRedirectUris = { "https://localhost:44378/authentication/logout-callback" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "StoryBlogWebAPI"
                    },
                    AllowAccessTokensViaBrowser = true
                },
                new Client
                {
                    ClientId = "app.api",
                    ClientName = "Story Blog Web API",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    RequirePkce = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "StoryBlogWebAPI"
                    },
                    AllowAccessTokensViaBrowser = true
                }
            };
    }
}
