using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Software_Projekt.Data;
using Software_Projekt.Models;

namespace Software_Projekt.Pages.personmodel
{
    public class CreateModel : PageModel
    {
        private readonly Software_Projekt.Data.ApplicationDbContext _context;

        public CreateModel(Software_Projekt.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public PersonModel PersonModel { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.PersonModel.Add(PersonModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
