using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Software_Projekt.Authorization;
using Software_Projekt.Data;
using Software_Projekt.Models;

namespace Software_Projekt.Pages.Modulhandbuche
{
    public class IndexModel : BasePageModel
    {
        public IndexModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }
        public IList<Modulhandbuch> Modulhandbuch { get;set; }

        public async Task OnGetAsync()
        {
            var contacts = from c in Context.Modulhandbuch
                           select c;

            var isAuthorized =
                               User.IsInRole(Constants.DekanRole);

            var currentUserId = UserManager.GetUserId(User);

            // Only approved contacts are shown UNLESS you're authorized to see them
           

            Modulhandbuch = await contacts.ToListAsync();
        }
    }
}