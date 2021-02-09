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
    public class DetailsModel : PageModel
    {
        private readonly Software_Projekt.Data.ApplicationDbContext _context;

        public DetailsModel(Software_Projekt.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
