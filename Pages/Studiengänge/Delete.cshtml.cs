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

namespace Software_Projekt.Pages.Studiengänge
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
        public Studiengang Studiengang { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Studiengang = await Context.Studiengang.FirstOrDefaultAsync(
                                                 m => m.StudiengangId == id);

            if (Studiengang == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, Studiengang,
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
                .Studiengang.AsNoTracking()
                .FirstOrDefaultAsync(m => m.StudiengangId == id);

            if (contact == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, contact,
                                                     ContactOperations.Delete);


            var isA = User.IsInRole(Constants.DekanRole);

            if (!isA)
            {
                return Forbid();
            }

            Context.Studiengang.Remove(contact);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}