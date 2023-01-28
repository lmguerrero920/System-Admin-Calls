using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultoriaSAS.Entity.Entities
{
   public class WorkStation
    {
        [Key]
        public int WorkStationId { get; set; }

   
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        public ICollection<Employee> Employees { get; set; }

    }
}
