using System.Threading.Tasks;
using Software_Projekt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Software_Projekt.Authorization
{
    public class AdminHandler
                    : AuthorizationHandler<OperationAuthorizationRequirement, Modul>
    {
        protected override Task HandleRequirementAsync(
                                              AuthorizationHandlerContext context,
                                    OperationAuthorizationRequirement requirement,
                                     Modul resource)
        {

            if (
                requirement.Name != Constants.ReadOperationName
                )
            {
                return Task.CompletedTask;
            }
            //if (
            //   requirement.Name != Constants.CreateOperationName
            //   )
            //{
            //    return Task.CompletedTask;
            //}


            if (context.User.IsInRole(Constants.AdminRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}