using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Software_Projekt.Authorization;
using Software_Projekt.Data;
using Software_Projekt.Models;
using Software_Projekt.Pages.Modules;

namespace Software_Projekt.Pages.Änderungen
{
    public class CreateModel : BasePageModel
    {
        public CreateModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Änderung Änderung { get; set; }
      

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Änderung.OwnerID = UserManager.GetUserId(User);
   
            var iA = User.IsInRole(Constants.DozentRole);
            var isA = User.IsInRole(Constants.DekanRole);

          
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                        User, Änderung,
                                                        ContactOperations.Create);
            if (iA||isA)
            {
                Context.Änderung.Add(Änderung);
                
            }

            else 
            { 
                return Forbid();
            }
          
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
