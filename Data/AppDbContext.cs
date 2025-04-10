using System;
namespace MasterProject.Data
{
	public class AppDbContext : IdentityDbContext<AppUser>
	{
        private readonly IOptions<ConnectionStrings> _ConnectionString;

		public AppDbContext(DbContextOptions<AppDbContext> options, IOptions<ConnectionStrings> options1): base(options){

            _ConnectionString = options1;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_ConnectionString.Value.DefaultConnection);
            base.OnConfiguring(optionsBuilder);	
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ImageModel>()
                .HasOne(p => p.ToDo)
                .WithMany(p => p.Images)
                .HasForeignKey(F=>F.ToDoId);
            builder.Entity<ToDoModel>().HasOne(p => p.AppUser)
                .WithMany(I => I.toDoModels).HasForeignKey(F => F.AppUserId);
        }
        public DbSet<ToDoModel> ToDos { get; set; }
        public DbSet<ImageModel> Images { get; set; }
    }
}

