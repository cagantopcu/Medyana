using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Medyana.BM.DbObject
{

    [Table("Equipment", Schema = "Medyana")]
    public class EquipmentDbObject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public DateTime SupplyDate { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public decimal UsageRate { get; set; }

        [ForeignKey("ClinicId")]
        public virtual ClinicDbObject Clinic { get; set; }
        public int ClinicId { get; set; }

    }
}
