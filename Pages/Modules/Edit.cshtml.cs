using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Software_Projekt.Authorization;
using Software_Projekt.Data;
using Software_Projekt.Models;

namespace Software_Projekt.Pages.Modules
{
    public class EditModel : DI_BasePageModel
    {
        public EditModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public Modul Modul { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Modul = await Context.Modul.FirstOrDefaultAsync(
                                                 m => m.ModulId == id);

            if (Modul == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                      User, Modul,
                                                      ContactOperations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Fetch Contact from DB to get OwnerID.
            var contact = await Context
                .Modul.AsNoTracking()
                .FirstOrDefaultAsync(m => m.ModulId == id);

            if (contact == null)
            {
                return NotFound();
            }


            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, contact,
                                                     ContactOperations.Update);


            var iA = User.IsInRole(Constants.DozentRole);
            var isA = User.IsInRole(Constants.DekanRole);


            if (!isA/*isAuthorized.Succeeded*/)
            {
                return Forbid();
            }

            Modul.OwnerID = contact.OwnerID;

            Context.Attach(Modul).State = EntityState.Modified;

          

            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
