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
    public class IndexModel : PageModel
    {
        private readonly Software_Projekt.Data.ApplicationDbContext _context;

        public IndexModel(Software_Projekt.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<PersonModel> PersonModel { get;set; }

        public async Task OnGetAsync()
        {
            PersonModel = await _context.PersonModel.ToListAsync();
        }

        public string Name { get; set; }

        public void OnGet()
        {
        }

        public void OnPostSubmit(PersonModel person)
        {
            this.Name = string.Format("Name: {0} {1}", person.FirstName, person.LastName);
        }


    }
}
