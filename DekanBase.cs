using Software_Projekt.Authorization;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Authorization
{
 
    public abstract class DekanBase
        <TRequirement, T1 ,T2 ,T3 , T4 , T5 , T6 > : IAuthorizationHandler where TRequirement : IAuthorizationRequirement
    {
        //protected AuthorizationHandler();

        [DebuggerStepThrough]
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            var pendingRequirements = context.PendingRequirements.ToList();

            foreach (var requirement in pendingRequirements)
            {
                if ((context.User.IsInRole(Constants.DekanRole))
                    )
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
        protected abstract Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement,T1 a,T2 b,T3 c, T4 d, T5 e, T6 f);
    }
}