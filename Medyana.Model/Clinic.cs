using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Medyana.Model
{
    public class Clinic
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200), MinLength(1)]
        public string Name { get; set; }
    }
}
