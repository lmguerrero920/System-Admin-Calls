 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultoriaSAS.Entity.Entities
{
   public  class Call
    {
        [Key]
        public int CallId { get; set; }

        [DataType(DataType.MultilineText)]

        [Display(Name =  "Descripción")]
        public string Description { get; set; }


        [Display(Name = "Cliente")]
        public int CustomerId { get; set; }
        [Display(Name = "Cliente")]
        public virtual Customer Customers { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}",
                 ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Registro")]
        public DateTime DateRegister { get; set; }


    }
}
