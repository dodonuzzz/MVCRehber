using MVCRehber.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCRehber.Models.Context
{
    public class MvcRehberContext:DbContext
    {
        public MvcRehberContext():base("Server = LAPTOP-M2LJ0R6T; Database = MVCRehber; Trusted_Connection = true")
        {
            
        }
        public DbSet<Kisi> Kisiler { get; set; }
        public DbSet<Sehir> Sehirler { get; set; }
    }
}