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

namespace Software_Projekt.Pages.Modulhandbuche
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


        public Modulhandbuch Modulhandbuch { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Modulhandbuch = await Context.Modulhandbuch.FirstOrDefaultAsync(m => m.ModulhandbuchId == id);

            if (Modulhandbuch == null)
            {
                return NotFound();
            }
          //  var isA =
              //                             User.IsInRole(Constants.DekanRole);

           // var iA =
           ///                    User.IsInRole(Constants.DozentRole);
          //  var i =
           //                    User.IsInRole(Constants.AdminRole);
//
          //  var currentUserId = UserManager.GetUserId(User);

          //  if (!isA || !iA || !i
             // /*  && Änderung.Status != ÄndernStatus.Approved*/)
            //{
            //    return Forbid();
           // }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var contact = await Context.Änderung.FirstOrDefaultAsync(
                                                      m => m.ÄnderungId == id);

            if (contact == null)
            {
                return NotFound();
            }



            var isA =
                              User.IsInRole(Constants.DekanRole);
           
            if (!isA)
            {
                return Forbid();
            }
          
            Context.Änderung.Update(contact);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}