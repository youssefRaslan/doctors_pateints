using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace doctors.data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<doctor> doctors { get; set; }
        public DbSet<Patient> patients { get; set; }
        public DbSet<PatientDoctor> PatientDoctors { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> dbContextOPtions) : base(dbContextOPtions)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PatientDoctor>()
                .HasKey(pd => new { pd.PatientId, pd.DoctorId });

            builder.Entity<PatientDoctor>()
                .HasOne(pd => pd.Patient)
                .WithMany(p => p.PatientDoctor)
                .HasForeignKey(pd => pd.PatientId);

            builder.Entity<PatientDoctor>()
                .HasOne(pd => pd.Doctor)
                .WithMany(d => d.PatientDoctors)
                .HasForeignKey(pd => pd.DoctorId);
        }

    }
}
