using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Software_Projekt.Models;

namespace Software_Projekt.Pages.Shared
{
    [AllowAnonymous]
    public class Database_Search : PageModel
    {

        [BindProperty]
        public int number { get; set; }

        [BindProperty]
        public int number2 { get; set; }

        public IActionResult OnPost()
       {
    
            if(number == 1 && number2 == 2){
               
                return Redirect("/testmodul/Details?id=2");

            }
            if (number == 1 && number2 == 3)
            {
                
                return Redirect("/testmodul/Details?id=3");
            }
            if (number == 1 && number2 == 4)
            {
              
                return Redirect("/testmodul/Details?id=4");
            }
            if (number == 2 && number2 == 1)
            {
                
                return Redirect("/testmodul/Details?id=2");
            }
         
            number = number;
            return null;
        }

       
    }
}





