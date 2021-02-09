using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Software_Projekt.Authorization;
using Software_Projekt.Data;
using Software_Projekt.Models;

namespace Software_Projekt.Pages.Modules
{
    public class CreateModel : BasePageModel
    {
        public CreateModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Modul Modul { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Modul.OwnerID = UserManager.GetUserId(User);


            var iA = User.IsInRole(Constants.DozentRole);
            var isA = User.IsInRole(Constants.DekanRole);
            // requires using ContactManager.Authorization;
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                        User, Modul,
                                                        ContactOperations.Create);
            if ( isA)
            {
                Context.Modul.Add(Modul);


            }
            else { return Forbid(); }

            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
