using DrAvail.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrAvail.Services;

namespace DrAvail.Controllers
{
    public class DI_BaseController:Controller
    {
        protected IAuthorizationService AuthorizationService { get; }
        protected UserManager<IdentityUser> UserManager { get; }

        protected ApplicationDbContext Context { get; }


        public DI_BaseController(ApplicationDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager):base()
        {
            Context = context;
            AuthorizationService = authorizationService;
            UserManager = userManager;
        }
    }
}
