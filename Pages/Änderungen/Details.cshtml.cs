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


namespace Software_Projekt.Pages.Änderungen
{
    public class DetailsModel : BasePageModel
    {
        public DetailsModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public Änderung Änderung { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Änderung = await Context.Änderung.FirstOrDefaultAsync(m => m.ÄnderungId == id);

            if (Änderung == null)
            {
                return NotFound();
            }

            var isAuthorized =
                               User.IsInRole(Constants.DekanRole);

            var currentUserId = UserManager.GetUserId(User);

            if (!isAuthorized
              // && currentUserId != Änderung.OwnerID
              /*  && Änderung.Status != ÄndernStatus.Approved*/)
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, ÄndernStatus status)
        {
            var contact = await Context.Änderung.FirstOrDefaultAsync(
                                                      m => m.ÄnderungId == id);

            if (contact == null)
            {
                return NotFound();
            }

            var contactOperation = (status == ÄndernStatus.Approved)
                                                       ? ContactOperations.Approve
                                                       : ContactOperations.Reject;


            var isA =
                              User.IsInRole(Constants.DekanRole);
            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, contact,
                                        contactOperation);
            if (!isA)
            {
                return Forbid();
            }
            contact.Status = status;
            Context.Änderung.Update(contact);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}