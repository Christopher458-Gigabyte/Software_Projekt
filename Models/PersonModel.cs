using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Software_Projekt.Models
{
  
    public class PersonModel
    {
        [BindProperty]
        [Key]
        public string FirstName { get; set; }

        
        [BindProperty]
        public string LastName { get; set; }
    }
}
