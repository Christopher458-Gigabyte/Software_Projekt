using System.Threading.Tasks;
using Software_Projekt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Software_Projekt.Authorization
{
    public class DozentHandler
                     : Dozentbase<OperationAuthorizationRequirement, Änderung, Modul>



    {
        protected override Task HandleRequirementAsync(
                                              AuthorizationHandlerContext context,
                                    OperationAuthorizationRequirement requirement,
                                     Änderung resource, Modul r)
        {

            bool a = requirement.Name == Constants.CreateOperationName;
            bool b = context.User.IsInRole(Constants.DozentRole);


           if (
              requirement.Name != Constants.CreateOperationName
             )
            {
                context.Succeed(requirement);
            }


            if (b && a)
            {
               
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}