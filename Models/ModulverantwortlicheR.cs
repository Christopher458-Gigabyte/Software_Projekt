using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Software_Projekt.Models
{
    public partial class ModulverantwortlicheR
    {
        [Key]
        public int ModulverantwortlichenId { get; set; }
        public string Name { get; set; }
       
        public string EMail { get; set; }
    }
}
