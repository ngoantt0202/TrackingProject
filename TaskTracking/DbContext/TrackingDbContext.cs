namespace TaskTracking.DbContext
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TrackingDbContext : DbContext
    {
        public TrackingDbContext()
            : base("name=TrackingDbContext")
        {
        }

        public virtual DbSet<t_group> t_group { get; set; }
        public virtual DbSet<t_memeber> t_memeber { get; set; }
        public virtual DbSet<t_task> t_task { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<t_group>()
                .Property(e => e.GroupName)
                .IsUnicode(false);

            modelBuilder.Entity<t_group>()
                .Property(e => e.GroupType)
                .IsUnicode(false);

            modelBuilder.Entity<t_group>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<t_group>()
                .Property(e => e.CreateUser)
                .IsUnicode(false);

            modelBuilder.Entity<t_group>()
                .Property(e => e.CreateUpdate)
                .IsUnicode(false);

            modelBuilder.Entity<t_memeber>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<t_memeber>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<t_memeber>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<t_memeber>()
                .Property(e => e.CreateUser)
                .IsUnicode(false);

            modelBuilder.Entity<t_memeber>()
                .Property(e => e.UpdateUser)
                .IsUnicode(false);

            modelBuilder.Entity<t_task>()
                .Property(e => e.Subject)
                .IsUnicode(false);

            modelBuilder.Entity<t_task>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<t_task>()
                .Property(e => e.Priority)
                .IsUnicode(false);

            modelBuilder.Entity<t_task>()
                .Property(e => e.Important)
                .IsUnicode(false);

            modelBuilder.Entity<t_task>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<t_task>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<t_task>()
                .Property(e => e.Assign)
                .IsUnicode(false);

            modelBuilder.Entity<t_task>()
                .Property(e => e.CreateUser)
                .IsUnicode(false);

            modelBuilder.Entity<t_task>()
                .Property(e => e.UpdateUser)
                .IsUnicode(false);
        }
    }
}
