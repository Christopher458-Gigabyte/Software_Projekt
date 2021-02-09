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

namespace Software_Projekt.Pages.Studiengänge
{
    public class EditModel : BasePageModel
    {
        public EditModel(
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
                .Studiengang.AsNoTracking()
                .FirstOrDefaultAsync(m => m.StudiengangId == id);

            if (contact == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, contact,
                                                     ContactOperations.Update);

            var isA = User.IsInRole(Constants.DekanRole);


            if (!isA)
            {
                return Forbid();
            }

          
            Context.Attach(Studiengang).State = EntityState.Modified;
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}