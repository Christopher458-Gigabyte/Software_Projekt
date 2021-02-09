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

namespace Software_Projekt.Pages.Änderungen
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
        public Änderung Änderung { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Änderung = await Context.Änderung.FirstOrDefaultAsync(
                                                 m => m.ÄnderungId == id);

            if (Änderung == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                      User, Änderung,
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
                .Änderung.AsNoTracking()
                .FirstOrDefaultAsync(m => m.ÄnderungId == id);

            if (contact == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, contact,
                                                     ContactOperations.Update);


            var iA = User.IsInRole(Constants.DozentRole);
            var isA = User.IsInRole(Constants.DekanRole);



            if (! isA)
            {
                return Forbid();
            }

            Änderung.OwnerID = contact.OwnerID;

            Context.Attach(Änderung).State = EntityState.Modified;

            if (Änderung.Status == ÄndernStatus.Approved)
            {
                // If the contact is updated after approval, 
                // and the user cannot approve,
                // set the status back to submitted so the update can be
                // checked and approved.
                var canApprove = await AuthorizationService.AuthorizeAsync(User,
                                        Änderung,
                                        ContactOperations.Approve);

                if (!isA)
                {
                    Änderung.Status = ÄndernStatus.Submitted;
                }
            }

            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

