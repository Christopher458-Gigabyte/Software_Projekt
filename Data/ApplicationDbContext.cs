using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Software_Projekt.Models;

namespace Software_Projekt.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Software_Projekt.Models.Modul> Modul { get; set; }
        public DbSet<Software_Projekt.Models.Studiengang> Studiengang { get; set; }
        public DbSet<Software_Projekt.Models.ProgrammverantwortlicheR> ProgrammverantwortlicheR { get; set; }
        public DbSet<Software_Projekt.Models.ModulverantwortlicheR> ModulverantwortlicheR { get; set; }
        public DbSet<Software_Projekt.Models.Modulhandbuch> Modulhandbuch { get; set; }
       
        public DbSet<Software_Projekt.Models.Änderung> Änderung { get; set; }
        public DbSet<Software_Projekt.Models.PersonModel> PersonModel { get; set; }
    }
}
