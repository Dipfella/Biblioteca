using Microsoft.EntityFrameworkCore;

namespace Biblioteca_API.Entities.Biblioteca
{
    public class BibliotecaDbContext : DbContext
    {
        public BibliotecaDbContext()
        {
        }

        public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ejemplares> Ejemplares { get; set; }
        public virtual DbSet<Libros> Libros { get; set; }
        public virtual DbSet<Prestamos> Prestamos { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetStringFromConfig());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP850_CI_AI");

            base.OnModelCreating(modelBuilder);
        }

        private static string GetStringFromConfig()
        {
            string filePath = "appsettings.json";
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile(filePath)
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            if (connectionString == null)
            {
                throw new InvalidOperationException("La cadena de conexión 'DefaultConnection' no está configurada.");
            }
            return connectionString;
        }
    }
