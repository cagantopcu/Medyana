using Medyana.BM.DbObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medyana.BM
{
    public class MedyanaDbContext : DbContext
    {
        public MedyanaDbContext(DbContextOptions<MedyanaDbContext> options) : base(options)
        { }

        public virtual DbSet<ClinicDbObject> ClinicsDbSet { get; set; }
        public virtual DbSet<EquipmentDbObject> EquipmentsDbSet { get; set; }

    }
}
