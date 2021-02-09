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

namespace Software_Projekt.Pages.Modules
{
    public class DetailsModel : DI_BasePageModel
    {
        public DetailsModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public Modul Modul { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Modul = await Context.Modul.FirstOrDefaultAsync(m => m.ModulId == id);

            if (Modul == null)
            {
                return NotFound();
            } 


            //var isA =
                  //              User.IsInRole(Constants.DekanRole);

          //  var iA =
                 //              User.IsInRole(Constants.DozentRole);
         //   var i =
                  //             User.IsInRole(Constants.AdminRole);
            //
           // var currentUserId = UserManager.GetUserId(User);

           

            return Page();

        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var contact = await Context.Modul.FirstOrDefaultAsync(
                                                      m => m.ModulId == id);

            if (contact == null)
            {
                return NotFound();
            }


            var isA =
                                 User.IsInRole(Constants.DekanRole);

            var iA =
                               User.IsInRole(Constants.DozentRole);
            var i =
                               User.IsInRole(Constants.AdminRole);

            var currentUserId = UserManager.GetUserId(User);

            if ( !iA 
              /*  && Änderung.Status != ÄndernStatus.Approved*/)
            {
                return Forbid();
            }
            Context.Modul.Update(contact);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
