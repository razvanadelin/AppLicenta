using FitnessApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FitnessApp
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<MealPlan> MealPlan { get; set; }
        public DbSet<Training> Training { get; set; }
        public DbSet<Measurements> Measurements { get; set; }
        public DbSet<Achievements> Achievements { get; set; }
        public DbSet<ExerciseTraining> ExerciseTraining { get; set; }
        public DbSet<Exercices> Exercices { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<MealPlan>()
                .HasOne(u => u.User)
                .WithMany(mo => mo.MealPlans);

            modelBuilder.Entity<Training>()
                .HasOne(u => u.User)
                .WithMany(mo => mo.Trainings);

            modelBuilder.Entity<Measurements>()
                .HasOne(u => u.User)
                .WithMany(me => me.Measurements);
            modelBuilder.Entity<Achievements>()
               .HasOne(u => u.User)
               .WithMany(me => me.Achievements);

            modelBuilder.Entity<ExerciseTraining>()
               .HasOne(u => u.Training)
               .WithMany(me => me.ExerciseTraining)
               .HasForeignKey("ProgramID");

            modelBuilder.Entity<ExerciseTraining>()
              .HasOne(u => u.Exercices)
              .WithMany(me => me.ExerciseTraining)
              .HasForeignKey("ExID");

        }
    }
}
