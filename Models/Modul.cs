using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Software_Projekt.Models
{
    public partial class Modul
    {
        public int ModulId { get; set; }

        // user ID from AspNetUser table.
        public string OwnerID { get; set; }
        public string Name { get; set; }
        public int ModulverantwortlichenId { get; set; }
        public string ModulverantwortlicheR { get; set; }
        public int? Semester { get; set; }
        public int? WorkloadH { get; set; }
        public int? Semesterwochenstunden { get; set; }
        public int Credits { get; set; }
        public string Turnus { get; set; }
        public string CuriculareZuordnung { get; set; }
        public string Lernziele { get; set; }
        public string Schlüsselqualifikationen { get; set; }
        public string InhaltilicheBeschreibung { get; set; }
        public string Unterrichtsform { get; set; }
        public string Prüfungsart { get; set; }
        public string Prüfungsform { get; set; }
        public string Verbindlichkeit { get; set; }
        public string Literaturangaben { get; set; }
        public int ModulhandbuchId { get; set; }

        
    }

 


   

}