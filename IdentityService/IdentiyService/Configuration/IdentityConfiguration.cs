using Duende.IdentityServer.Models;

namespace IdentiyService.Configuration
{
    public static class IdentityConfiguration
    {

        public static IEnumerable<IdentityResource> GetIdentityResources =>
              new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };

        //Declare new Scopes
        public static IEnumerable<ApiScope> GetApiScopes =>
            new List<ApiScope>
            {
                new ApiScope(name:"admin_scope",displayName:"Admin Scope"),
                new ApiScope(name:"owner_scope",displayName:"Owner Scope"),
                new ApiScope(name:"manager_scope",displayName:"Manager Scope"),
                new ApiScope(name:"teacher_scope",displayName:"Teacher Scope"),
                new ApiScope(name:"student_scope",displayName:"Student Scope"),
            };
        public static IEnumerable<Client> GetClients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "admin_client",
                    ClientSecrets = {new Secret("admin123".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = {"admin_scope"},
                    //RedirectUris = { "https://localhost:5000/signin-oidc" },
                    //PostLogoutRedirectUris = { "https://localhost:5000/signout-callback-oidc" },
                },
                new Client
                {
                    ClientId = "owner_client",
                    ClientSecrets = {new Secret("admin123".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { "owner_scope" }
                },
                new Client
                {
                    ClientId = "manager_client",
                    ClientSecrets = {new Secret("admin123".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { "manager_scope" }
                },
                new Client
                {
                    ClientId = "teacher_client",
                    ClientSecrets = {new Secret("admin123".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { "teacher_scope" }
                },
                new Client
                {
                    ClientId = "student_client",
                    ClientSecrets = {new Secret("admin123".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { "student_scope" }
                },

            };

    }
}
