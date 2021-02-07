namespace SysTracker.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Data_Models : DbContext
    {
        public Data_Models()
            : base("name=DataModels")
        {
        }

        public virtual DbSet<Link> Links { get; set; }
        public virtual DbSet<Server> Servers { get; set; }
        public virtual DbSet<ServerType> ServerTypes { get; set; }
        public virtual DbSet<Systems> Systems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Link>()
                .HasKey(e => new { e.ServerID, e.SystemsID, e.ServerTypeID });

            modelBuilder.Entity<Link>()
                .HasRequired(e => e.Server)
                .WithMany(e => e.Links)
                .HasForeignKey(e => e.ServerID);


            modelBuilder.Entity<Link>()
                .HasRequired(e => e.System)
                .WithMany(e => e.Links)
                .HasForeignKey(e => e.SystemsID);


            modelBuilder.Entity<Link>()
                .HasRequired(e => e.ServerType)
                .WithMany(e => e.Links)
                .HasForeignKey(e => e.ServerTypeID);

            /*
            modelBuilder.Entity<UserAccount>()
                .HasKey(e => new { e.UserId, e.AccountId });

            modelBuilder.Entity<UserAccount>()
                .HasRequired(e => e.User)
                .WithMany(e => e.UserAccounts)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<UserAccount>()
                .HasRequired(e => e.Account)
                .WithMany(e => e.UserAccounts)
                .HasForeignKey(e => e.AccountId);

            modelBuilder.Entity<UserAccount>()
                .HasRequired(e => e.Role)
                .WithMany(e => e.UserAccounts)
                .HasForeignKey(e => e.RoleId);
                */

            /*modelBuilder.Entity<Link>()
                .Property(e => e.ServerID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Link>()
                .Property(e => e.SystemsID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Link>()
                .Property(e => e.ServerTypeID)
                .IsFixedLength()
                .IsUnicode(false);*/
        }
    }
}
