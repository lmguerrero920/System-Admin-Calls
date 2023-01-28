using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultoriaSAS.Entity.Entities
{
   public class Request
    {
        [Key]
        public int RequestId { get; set; }

        [Display(Name = "Numero Folio")]
        public int NumberInvoice { get; set; }



        [Display(Name = "Cliente")]
        public int CustomerId { get; set; }
        [Display(Name = "Cliente")]
        public virtual Customer  Customers { get; set; }



        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}",
                 ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Registro")] 
        public DateTime DateRequest { get; set; }

        
        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripcion")]
        public string Description { get; set; }

        [Display(Name = "Estado")]
        public bool IsActive { get; set; }


        [Display(Name = "Tipo Solicitud")]
        public virtual RequestType RequestTypes { get; set; } 


    }
}
