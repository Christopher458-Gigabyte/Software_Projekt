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

namespace Software_Projekt.Pages.Programmverantwortlicher
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
        public ProgrammverantwortlicheR ProgrammverantwortlicheR { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ProgrammverantwortlicheR = await Context.ProgrammverantwortlicheR.FirstOrDefaultAsync(
                                                 m => m.ProgrammverantwortlichenId == id);

            if (ProgrammverantwortlicheR == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                      User, ProgrammverantwortlicheR,
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
                .ProgrammverantwortlicheR.AsNoTracking()
                .FirstOrDefaultAsync(m => m.ProgrammverantwortlichenId == id);

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



            Context.Attach(ProgrammverantwortlicheR).State = EntityState.Modified;

            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}