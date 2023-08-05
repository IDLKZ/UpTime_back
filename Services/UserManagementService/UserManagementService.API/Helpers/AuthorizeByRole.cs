using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace UserManagementService.API.Helpers
{
    public class AuthorizeByRole : AuthorizeAttribute
    {
        public AuthorizeByRole(params string[] roles) : base()
        {
            Roles = String.Join(",", roles);
        }
    }
}
