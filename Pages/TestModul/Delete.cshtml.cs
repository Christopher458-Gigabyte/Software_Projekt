using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Software_Projekt.Data;
using Software_Projekt.Models;

namespace Software_Projekt.Pages.testmodul
{
   // [AllowAnonymous]
    public class DeleteModel : PageModel
    {
        private readonly Software_Projekt.Data.ApplicationDbContext _context;

        public DeleteModel(Software_Projekt.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Modul Modul { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Modul = await _context.Modul.FirstOrDefaultAsync(m => m.ModulId == id);

            if (Modul == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Modul = await _context.Modul.FindAsync(id);

            if (Modul != null)
            {
                _context.Modul.Remove(Modul);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
