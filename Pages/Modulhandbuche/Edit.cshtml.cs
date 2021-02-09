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


namespace Software_Projekt.Pages.Modulhandbuche
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
                .Modulhandbuch.AsNoTracking()
                .FirstOrDefaultAsync(m => m.ModulhandbuchId == id);

            if (contact == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, contact,
                                                     ContactOperations.Update);


           
            var isA = User.IsInRole(Constants.DekanRole);



            if (!isA/*isAuthorized.Succeeded*/)
            {
                return Forbid();
            }



            Context.Attach(Modulhandbuch).State = EntityState.Modified;

            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}