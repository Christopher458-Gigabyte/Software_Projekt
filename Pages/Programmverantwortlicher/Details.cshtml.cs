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

namespace Software_Projekt.Pages.Programmverantwortlicher
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



        public ProgrammverantwortlicheR ProgrammverantwortlicheR { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ProgrammverantwortlicheR = await Context.ProgrammverantwortlicheR.FirstOrDefaultAsync(m => m.ProgrammverantwortlichenId == id);

            if (ProgrammverantwortlicheR == null)
            {
                return NotFound();
            }
           
        
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var contact = await Context.ProgrammverantwortlicheR.FirstOrDefaultAsync(
                                                      m => m.ProgrammverantwortlichenId == id);

            if (contact == null)
            {
                return NotFound();
            }



            var isA = User.IsInRole(Constants.DekanRole);

            if (!isA)
            {
                return Forbid();
            }

            Context.ProgrammverantwortlicheR.Update(contact);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
