using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Medyana.BM.DbObject
{

    [Table("Clinic", Schema = "Medyana")]
    public class ClinicDbObject
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200), MinLength(1)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}
