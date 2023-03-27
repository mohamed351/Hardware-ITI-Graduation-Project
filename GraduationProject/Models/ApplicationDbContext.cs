using AutoMapper.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace GraduationProject.Models
{
    public partial class  ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }
        public virtual DbSet<Attributes> Attributes { get; set; }
        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Buys> Buys { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<CategoryAttributes> CategoryAttributes { get; set; }
        public virtual DbSet<Model> Model { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductAttributes> ProductAttributes { get; set; }
        public virtual DbSet<Suggestions> Suggestions { get; set; }
        public virtual DbSet<UserProduct> UserProduct { get; set; }
        public virtual DbSet<UserProductImages> UserProductImages { get; set; }
        public virtual DbSet<ApplicationUser> Users { get; set; }
        public virtual DbSet<UsersRatings> UsersRatings { get; set; }
        public virtual DbSet<UsersReviews> UsersReviews { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Filter> Filters { get; set; }
        public DbSet<FilterChoice> FilterChoices { get; set; }
        public DbSet<Filter_FilterChoice> filter_FilterChoices { get; set; }
        public DbSet<DynamicChoice> dynamicChoices { get; set; }
        public DbSet<ChatMessages> ChatMessages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attributes>(entity =>
            {
             

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Buys>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.UserProductId })
                    .HasName("PK__Buys__7D0B4B083DAE476E");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Buys)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Buys__UserId__36B12243");

                entity.HasOne(d => d.UserProduct)
                    .WithMany(p => p.Buys)
                    .HasForeignKey(d => d.UserProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Buys__UserProduc__37A5467C");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CategoryAttributes>(entity =>
            {
                entity.HasKey(e => new { e.CategoryId, e.CategoryAttributeId })
                    .HasName("PK__Category__2B1402DC791F1288");

                entity.HasOne(d => d.CategoryAttribute)
                    .WithMany(p => p.CategoryAttributes)
                    .HasForeignKey(d => d.CategoryAttributeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CategoryA__Categ__1ED998B2");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategoryAttributes)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CategoryA__Categ__1DE57479");
            });

         


            modelBuilder.Entity<Model>(entity =>
            {
               

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Model)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Model__CategoryI__276EDEB3");
            });

            modelBuilder.Entity<Product>(entity =>
            {
             

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__BrandId__2A4B4B5E");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__ModelId__2B3F6F97");
            });

          

            modelBuilder.Entity<Suggestions>(entity =>
            {
                 

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.SuggestedModel)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Suggestions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Suggestio__UserI__182C9B23");
            });

            modelBuilder.Entity<UserProduct>(entity =>
            {
                 

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Price).HasColumnType("decimal(9, 2)");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.UserProduct)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserProdu__Produ__33D4B598");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserProduct)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserProdu__UserI__32E0915F");
            });

            modelBuilder.Entity<UserProductImages>(entity =>
            {
                entity.HasKey(e => new { e.Images, e.UserProductId })
                    .HasName("PK__UserProd__5ED6B798A1DCD7A9");

                entity.ToTable("UserProduct_Images");

                entity.Property(e => e.Images).HasMaxLength(255);

                entity.HasOne(d => d.UserProduct)
                    .WithMany(p => p.UserProductImages)
                    .HasForeignKey(d => d.UserProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserProdu__UserP__3A81B327");
            });

          

            modelBuilder.Entity<UsersRatings>(entity =>
            {
            

                entity.ToTable("Users_Ratings");

            });

            modelBuilder.Entity<UsersReviews>(entity =>
            {
            

                entity.ToTable("Users_Reviews");

                entity.Property(e => e.Review)
                    .IsRequired()
                    .HasMaxLength(500);

            
            });

            base.OnModelCreating(modelBuilder);
        }

        public static async Task CreateDefaultAccounts(IServiceProvider service, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            UserManager<ApplicationUser> userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
            RoleManager<IdentityRole> roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
           var result = roleManager.Roles.FirstOrDefault(a => a.Name == "Admin");
            if(result == null)
            {
               await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
             result = roleManager.Roles.FirstOrDefault(a => a.Name == "User");
            if(result == null)
            {
                await roleManager.CreateAsync(new IdentityRole("User"));

            }

            string userName = configuration["DefaultAdmin:UserName"];
            string password = configuration["DefaultAdmin:Password"];
            string email = configuration["DefaultAdmin:Email"];
            string address = configuration["DefaultAdmin:Address"];
            string UserName = configuration["DefaultAdmin:UserName"];
            string role = configuration["DefaultAdmin:Role"];
            string firstName = configuration["DefaultAdmin:FirstName"];
            string lastName = configuration["DefaultAdmin:LastName"];
            string phone = configuration["DefaultAdmin:Phone"];
            string city = configuration["DefaultAdmin:City"];
            string area = configuration["DefaultAdmin:Area"];
            ApplicationUser checkUser = await userManager.FindByNameAsync(userName);
            if(checkUser == null)
            {
                ApplicationUser defaultUser = new ApplicationUser()
                {
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    Address = address,
                    UserName = userName,
                    PhoneNumber = phone,
                    Area = new Area()
                    {
                        AreaName = area,
                        City = new City()
                        {
                            CityName = city
                        }
                    }


                };
              IdentityResult identityResult =  await userManager.CreateAsync(defaultUser,password);
                if (identityResult.Succeeded)
                {
                  await  userManager.AddToRoleAsync(defaultUser, role);
                }
            }







        }

    }
}
