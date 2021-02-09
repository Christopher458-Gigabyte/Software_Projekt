using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Software_Projekt.Data;
using Software_Projekt.Models;

namespace Software_Projekt.Pages.personmodel
{
    public class DeleteModel : PageModel
    {
        private readonly Software_Projekt.Data.ApplicationDbContext _context;

        public DeleteModel(Software_Projekt.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PersonModel PersonModel { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PersonModel = await _context.PersonModel.FirstOrDefaultAsync(m => m.FirstName == id);

            if (PersonModel == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PersonModel = await _context.PersonModel.FindAsync(id);

            if (PersonModel != null)
            {
                _context.PersonModel.Remove(PersonModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
