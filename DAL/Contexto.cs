using Microsoft.EntityFrameworkCore;
using Samuel_Duran_Ap1_p1_.Entidades;

namespace Samuel_Duran_Ap1_p1_.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Productos> Productos {get; set;}
        
        public Contexto(DbContextOptions<Contexto> options) : base(options){}

    }
}
