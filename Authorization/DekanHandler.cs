using System.Threading.Tasks;
using Software_Projekt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Software_Projekt.Authorization
{
    public class DekanHandler
                    : DekanBase<OperationAuthorizationRequirement, Änderung, Modul,  Modulhandbuch, ModulverantwortlicheR, ProgrammverantwortlicheR, Studiengang>



    {
        protected override Task HandleRequirementAsync(
                                              AuthorizationHandlerContext context,
                                    OperationAuthorizationRequirement requirement,
                                     Änderung resource, Modul r, Modulhandbuch h, ModulverantwortlicheR i, ProgrammverantwortlicheR j, Studiengang k)
        {
            //if (context.User == null)
            //{
            //    return Task.CompletedTask;
            //}

            // Administrators can do anything.
            if (context.User.IsInRole(Constants.DekanRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }


    }
}