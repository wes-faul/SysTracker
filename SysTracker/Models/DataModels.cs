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
            //Specify the composite key
            modelBuilder.Entity<Link>()
                .HasKey(e => new { e.ServerID, e.SystemsID, e.ServerTypeID });

            //set up joins to related records 
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
        }
    }
}
