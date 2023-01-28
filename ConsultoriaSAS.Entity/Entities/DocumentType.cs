using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultoriaSAS.Entity.Entities
{
   public  class DocumentType
    {
        [Key]
        public int DocumentTypeId { get; set; }
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        public ICollection<Customer> Customers { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
