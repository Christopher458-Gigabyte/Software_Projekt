using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Software_Projekt.Data;
using Software_Projekt.Models;

namespace Software_Projekt.Pages.TESTPROCEDURE
{
    public class sdModel : PageModel
    {
        private readonly Software_Projekt.Data.ApplicationDbContext _context;

        public sdModel(Software_Projekt.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Modul Modul { get; set; }

        public async Task OnGetAsync()
        {


            var q1 = await _context.Database.ExecuteSqlInterpolatedAsync($"exec Chris2");
            //Modul = await _context.Modul.FromSqlRaw(u.testadvnacemethod()).ToListAsync();
            // Modul = await _context.Modul.FromSqlRaw("select * from Modul Where  Status = 2").ToListAsync();
        }
    }
}
     //   public async Task<IActionResult> OnGetAsync(int? id)
     // {
     //   if (id == null)
     // {
     //    return NotFound();
     //}

//            Modul = await _context.Modul.FirstOrDefaultAsync(m => m.ModulId == id);

//          if (Modul == null)
//      {
//            return NotFound();
//    }
//   return Page();
//}
//}
//}
