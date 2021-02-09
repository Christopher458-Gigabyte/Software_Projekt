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

namespace Software_Projekt.Pages.Studiengänge
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
        public Studiengang Studiengang { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }



            var isA = User.IsInRole(Constants.DekanRole);

          
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                        User, Studiengang,
                                                        ContactOperations.Create);
            if (isA)
            {
                Context.Studiengang.Add(Studiengang);


            }
            else { return Forbid(); }
        
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
