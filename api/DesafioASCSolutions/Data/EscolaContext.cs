using Microsoft.EntityFrameworkCore;
using DesafioASCSolutions.Models;
namespace DesafioASCSolutions.Data{
    public class EscolaContext : DbContext
    {
        //Server adress == https://localhost/ SQLServer
        private const string ConnectionString = "Server=Endereço;Database=NomeDoBanco;User Id=sa;Password=SuaSenha;";

        public DbSet<Turma> Turmas { get; set; } 
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Premio> Premios{ get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
    
}