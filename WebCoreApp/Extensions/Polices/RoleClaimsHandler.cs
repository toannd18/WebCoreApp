using DataContext.WebCoreApp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebCoreApp.Extensions.Polices
{
    public class RoleClaimsHandler : AuthorizationHandler<OperationRequirement>
    {
        private readonly RoleManager<AppRole> roleManager;
        private readonly UserManager<AppUser> userManager;

        public RoleClaimsHandler(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationRequirement requirement)
        {
            var claims = context.User.Claims;

            if (claims.Count() > 0)
            {
                var hasPermission = claims.Where(c =>
                                                  (c.Type == ClaimTypes.Role && c.Value.ToUpper()=="ADMIN")||
                                                  (c.Value.ToUpper() == requirement.Actions.ToUpper() &&
                                                    c.Type.ToUpper() == requirement.Functions.ToUpper())).Any();
                if (hasPermission)
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }
}