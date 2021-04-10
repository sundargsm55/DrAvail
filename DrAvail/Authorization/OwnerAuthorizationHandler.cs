using DrAvail.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrAvail.Authorization
{
    public class OwnerAuthorizationHandler: AuthorizationHandler<OperationAuthorizationRequirement, Object>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public OwnerAuthorizationHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public override async Task HandleAsync(AuthorizationHandlerContext context)
        {
                foreach (var req in context.Requirements.OfType<OperationAuthorizationRequirement>())
                {
                    await HandleRequirementAsync(context, req, (object)context.Resource);
                }
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Object resource)
        {
            if (context.User.Identity.Name == null)
            {
                return Task.CompletedTask;
            }

            if(requirement.Name != Constants.CreateOperationName && resource == null)
            {
                return Task.CompletedTask;
            } 

            // If not asking for CRUD permission, return.

            if (requirement.Name != Constants.CreateOperationName &&
                requirement.Name != Constants.ReadOperationName &&
                requirement.Name != Constants.UpdateOperationName &&
                requirement.Name != Constants.DeleteOperationName)
            {
                return Task.CompletedTask;
            }

            Console.WriteLine("Type of Resource: " + resource.GetType().ToString()); 
            dynamic doc = Convert.ChangeType(resource, resource.GetType());
            
            if (doc.OwnerID == _userManager.GetUserId(context.User))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
