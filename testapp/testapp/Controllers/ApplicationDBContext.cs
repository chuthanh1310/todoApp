using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using testapp.Models;

namespace testapp.Controllers
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext() : base("DefaultConnection") { }
        public DbSet<Todo> Todos { get; set; }
    }
}
 