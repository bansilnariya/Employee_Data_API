using Microsoft.EntityFrameworkCore;

namespace EMPLOYE__DATA_API.Model
{
    public class Empcls : DbContext
    {
        public Empcls(DbContextOptions<Empcls> options):base(options)
        { 
        }   
       public DbSet<Emp> emps {  get; set; } 


    }
}
