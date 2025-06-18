using ADO.NETtodoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ADO.NETtodoApp.Controllers
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<Todo> Todos { get; set; } = null!;
        
    }
    
}
