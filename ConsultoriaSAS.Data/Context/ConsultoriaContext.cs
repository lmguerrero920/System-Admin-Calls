using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ConsultoriaSAS.Data.Context
{
    public class ConsultoriaContext : DbContext
    { 
        public ConsultoriaContext() : base("name=ConsultoriaContext")
        {
           
        }

        public System.Data.Entity.DbSet<ConsultoriaSAS.Entity.Entities.Call> Calls { get; set; }

        public System.Data.Entity.DbSet<ConsultoriaSAS.Entity.Entities.Customer> Customers { get; set; }

        public System.Data.Entity.DbSet<ConsultoriaSAS.Entity.Entities.DocumentType> DocumentTypes { get; set; }

        public System.Data.Entity.DbSet<ConsultoriaSAS.Entity.Entities.Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<ConsultoriaSAS.Entity.Entities.WorkStation> WorkStations { get; set; }

        public System.Data.Entity.DbSet<ConsultoriaSAS.Entity.Entities.Pqr> Pqrs { get; set; }

        public System.Data.Entity.DbSet<ConsultoriaSAS.Entity.Entities.PqrType> PqrTypes { get; set; }

        public System.Data.Entity.DbSet<ConsultoriaSAS.Entity.Entities.Request> Requests { get; set; }

        public System.Data.Entity.DbSet<ConsultoriaSAS.Entity.Entities.RequestType> RequestTypes { get; set; }
    }
}
