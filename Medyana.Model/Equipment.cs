﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Medyana.Model
{
    public class Equipment
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200), MinLength(1)]
        public string Name { get; set; }

        public DateTime? SupplyDate { get; set; }

        [Required]
        [Range(1.0, Double.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, Double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public decimal UnitPrice { get; set; }

        [Required]
        public decimal UsageRate { get; set; }
        public int ClinicId { get; set; }
    }
}
