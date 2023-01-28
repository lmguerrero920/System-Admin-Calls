using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultoriaSAS.Entity.Entities
{
   public class PqrType
    {
        [Key]
        public int PqrTypeId { get; set; }

        [Display(Name = "Nombre ")]
        public string Name { get; set; }

        public ICollection<Pqr> Pqrs { get; set; }

    }
}
