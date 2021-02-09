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
using Software_Projekt.Pages.Shared;

namespace Software_Projekt.Pages.testmodul
{




    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly Software_Projekt.Data.ApplicationDbContext _context;

        public IndexModel(Software_Projekt.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Modul> Modul { get; set; }

    
       
        public async Task OnGetAsync()
        {
           var Procedure_Update = await _context.Database.ExecuteSqlInterpolatedAsync($"exec Chris4");
           
           Modul = await _context.Modul.FromSqlRaw("select * from Modul").ToListAsync();
        }

       
    }
}
