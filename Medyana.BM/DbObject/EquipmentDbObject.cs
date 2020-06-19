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
        [MaxLength(200), MinLength(1)]
        public string Name { get; set; }

        public DateTime? SupplyDate { get; set; }

        [Required]
        [Range(1.0, Double.MaxValue)]
        public double Quantity { get; set; }

        [Required]
        [Range(0.01, Double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public decimal UnitPrice { get; set; }

        [Required]
        public decimal UsageRate { get; set; }

        [ForeignKey("ClinicId")]
        public virtual ClinicDbObject Clinic { get; set; }
        public int ClinicId { get; set; }

        public bool IsDeleted { get; set; }

    }
}
