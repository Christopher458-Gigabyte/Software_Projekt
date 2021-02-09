using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Software_Projekt.Models
{
    public partial class Studiengang
    {
        public int StudiengangId { get; set; }
        public string Name { get; set; }
        public int ProgrammverantwortlichenId { get; set; }
        public int FachbereichId { get; set; }
        
    }

   


   

}
