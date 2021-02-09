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
using Software_Projekt.Pages.Modules;

namespace Software_Projekt.Pages.Modulhandbuche
{
    public class DeleteModel : BasePageModel
    {
        public DeleteModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public Modulhandbuch Modulhandbuch { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Modulhandbuch = await Context.Modulhandbuch.FirstOrDefaultAsync(
                                                 m => m.ModulhandbuchId == id);

            if (Modulhandbuch == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, Modulhandbuch,
                                                     ContactOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var contact = await Context
                .Modulhandbuch.AsNoTracking()
                .FirstOrDefaultAsync(m => m.ModulhandbuchId == id);

            if (contact == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, contact,
                                                     ContactOperations.Delete);


            var isA =
                              User.IsInRole(Constants.DekanRole);
            if (!isA)
            {
                return Forbid();
            }

            Context.Modulhandbuch.Remove(contact);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}