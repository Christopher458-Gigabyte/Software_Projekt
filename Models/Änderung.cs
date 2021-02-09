using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Software_Projekt.Models
{
    public partial class Änderung
    {
        public int ÄnderungId { get; set; }
        public string OwnerID { get; set; }
        public int ProgrammverantwortlichenId { get; set; }
        public int ModulverantwortlichenId { get; set; }
        public int ModulId { get; set; }
        public string Element { get; set; }    
        public string NeuerEintrag { get; set; }
        public DateTime Datum { get; set; }
        public int? ModulhandbuchId { get; set; }      
        public ÄndernStatus Status { get; set; }
    }

    public enum ÄndernStatus
    {
        Submitted,
        Approved,
        Rejected
    }
}
