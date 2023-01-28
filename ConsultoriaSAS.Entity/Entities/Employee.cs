using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultoriaSAS.Entity.Entities
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

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

        [Display(Name="Cargo")]
        public int WorkStationId { get; set; }
        [Display(Name = "Cargo")]
        public virtual WorkStation WorkStation { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        public string ConfirmPassword { get; set; }

    }
}
