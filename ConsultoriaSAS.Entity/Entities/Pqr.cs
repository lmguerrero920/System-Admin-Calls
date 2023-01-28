using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultoriaSAS.Entity.Entities
{
   public  class Pqr
    {
        [Key]
        public int PqrId { get; set; }

        [Display(Name = "Numero Radicado")]
        public int NumberSettled { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Display(Name = "Estado")]
        public bool IsActive { get; set; }

        [Display(Name = "Tipo Petición")]

        public int PqrTypeId { get; set; }
        [Display(Name = "Tipo Petición")]
        public virtual PqrType PqrTypes { get; set; }


        [Display(Name = "Cliente")]
        public int CustomerId { get; set; }
        [Display(Name = "Cliente")]
        public virtual Customer Customers { get; set; }

    }
}
