using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Software_Projekt.Models
{
    public partial class ProgrammverantwortlicheR
    {
        [Key]
        public int ProgrammverantwortlichenId { get; set; }
        public string Name { get; set; }
        public int StudiengangId { get; set; }
        public string EMail { get; set; }
    }
}
