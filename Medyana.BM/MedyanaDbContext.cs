using Medyana.BM.DbObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medyana.BM
{
    public class MedyanaDbContext : DbContext
    {
        public virtual DbSet<ClinicDbObject> ClinicsDbSet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=MedyanaDb;Trusted_Connection=True;");
        }
    }
}
