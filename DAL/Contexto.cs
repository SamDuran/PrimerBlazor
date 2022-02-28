using Microsoft.EntityFrameworkCore;
using Samuel_Duran_Ap1_p1_.Entidades;

namespace Samuel_Duran_Ap1_p1_.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Productos> Productos {get; set;}
        public Contexto(){}
        public Contexto(DbContextOptions<Contexto> options) : base(options){}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source =Data/Samuel_Duran_Ap1_p1.db");
        }
        /* protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        } */
    }
}
