
using AccessoADatos.Core.Interfaces;
using Clases;
using Clases.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessoADatos
{
    public class AppIngresoTemperatrasContext :DbContext, IContext
    {
        private const string ConnectionStringName = "Name=AppIngresoTemperatrasContext";

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Land> Lands { get; set; }
        public virtual DbSet<Temperature> Temperatures { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<QR> QRs { get; set; }
        public virtual DbSet<BlackList> BlackLists { get; set; }
        public virtual DbSet<Contractor> Contractors { get; set; }
        public virtual DbSet<IngressEgress> IngressEgress { get; set; }
        public virtual DbSet<FeaturedEvent> FeaturedEvents { get; set; }
        

        public DbSet<Role> Roles { get; set; }
        public DbSet<TypeEmployee> TypeEmployee { get; set; }

        public static AppIngresoTemperatrasContext Create()
        {
            return new AppIngresoTemperatrasContext();
        }
        public AppIngresoTemperatrasContext() : base(ConnectionStringName)
        {
            // this.Configuration.LazyLoadingEnabled = false;
            // this.Configuration.ProxyCreationEnabled = false;
            //Database.SetInitializer<AppIngresoTemperatrasContext>(null);
        }

        public void EmptyContext()
        {
            Users.RemoveRange(Users);
            Lands.RemoveRange(Lands);
            Temperatures.RemoveRange(Temperatures);  
        }

        public System.Data.Entity.DbSet<Clases.Models.AdminQR> AdminQRs { get; set; }

        public System.Data.Entity.DbSet<Clases.Models.Contract> Contracts { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    // Configure ApplicationUser & StudentAddress entity
        //    modelBuilder.Entity<ApplicationUser>()
        //                .HasOptional(u => u.UserName) // Mark UserProfile property optional in ApplicationUser entity
        //                .WithRequired(c => c.ApplicationUser); // mark ApplicationUser property as required in UserProfile entity. Cannot save UserProfile without ApplicationUser
        //                                                       //.Map(c => c.MapKey("ApplicationUserId")); 
        //}


        //   protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //   {
        //       modelBuilder.Entity<User>()
        //           .HasMany(u => u.Roles)
        //           .WithMany(r => r.Users)
        //           .Map(m =>
        //           {
        //               m.ToTable("UserRoles");
        //               m.MapLeftKey("UserId");
        //               m.MapRightKey("RoleId");
        //           });
        //   }

    }

}
