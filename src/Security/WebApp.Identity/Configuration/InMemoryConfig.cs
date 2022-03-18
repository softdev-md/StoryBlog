using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace WebApp.Identity.Configuration
{
    public static class InMemoryConfig
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource("roles", "User role(s)", new List<string> { "role" })
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("storyBlogApi", "StoryBlog API"),
                new ApiScope("GRPC", "GRPC Client")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("storyBlogApi", "StoryBlog API")
                {
                    Scopes = { "storyBlogApi" }
                },
                new ApiResource("GRPC", "GRPC Client")
                {
                    Scopes = { "GRPC" },
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "blazorWASM",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedCorsOrigins = { "https://localhost:44333" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "storyBlogApi",
                        "GRPC"
                    },
                    RedirectUris = { "https://localhost:44333/authentication/login-callback" },
                    PostLogoutRedirectUris = { "https://localhost:44333/authentication/logout-callback" }
                }
            };
    }
}
