using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultoriaSAS.Entity.Entities
{
   public class RequestType
    {
        [Key]
        public int RequestTypeId { get; set; }
 
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        public ICollection<Request> Requests { get; set; }
    }
}
