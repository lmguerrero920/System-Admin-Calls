using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultoriaSAS.Entity.Entities
{
   public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [Display(Name = "Documento")]
        public int Document { get; set; }

        [Display(Name = "Tipo Documento")]
        public int DocumentTypeId { get; set; }
        [Display(Name = "Tipo Documento")]

        public virtual DocumentType DocumentType { get; set; }


        [Display(Name = "Telefono fijo")]
        public string Phone { get; set; }

        [Display(Name = "Celular")]
        public string CellPhone { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}",
                  ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Nacimiento")]
        public DateTime DateOfBirth { get; set; }

        public ICollection<Request> Requests { get; set; }
        public ICollection<Call> Calls { get; set; }
        public ICollection<Pqr> Pqrs { get; set; }



    }
}
