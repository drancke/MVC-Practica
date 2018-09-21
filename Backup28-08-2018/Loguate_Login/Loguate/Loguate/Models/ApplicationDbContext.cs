using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Loguate.Models
{
    public partial class ApplicationDbContext
    {
        public DbSet<Persona> Persona { get; set; }
    }
}