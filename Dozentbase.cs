using Software_Projekt.Authorization;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Microsoft.AspNetCore.Authorization
{

    public abstract class Dozentbase<TRequirement, T1, T2> : IAuthorizationHandler where TRequirement : IAuthorizationRequirement
    {
        //protected AuthorizationHandler();

      
        



        //if ((context.User.IsInRole(Constants.DozentRole))
        //   )
        // {
        //     context.Succeed(requirement);
        //  }



        [DebuggerStepThrough]
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            var pendingRequirements = context.PendingRequirements.ToList();

            foreach (var requirement in pendingRequirements)
            {
                if (
                 /*requirement == Create &&*/ context.User.IsInRole(Constants.DozentRole)
                 )
                {
                    context.Succeed(requirement);
                }
                


            }
            return Task.CompletedTask;
        }
        protected abstract Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement, T1 a, T2 b);
    }
}