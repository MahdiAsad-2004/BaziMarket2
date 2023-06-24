using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaziMarket2.Models.ViewModel;

namespace BaziMarket2.Models
{
    public class Db_BaziMarket : DbContext
    {
        public static string DatabaseName = "Db_BaziMarket";
        

        public DbSet<User> T_User { get; set; }
        public DbSet<Role> T_Roles { get; set; }
        public DbSet<Category> T_Category { get; set; }
        public DbSet<Product> T_Product { get; set; }
        public DbSet<Cart> T_Cart { get; set; }
        public DbSet<Comment> T_Comment { get; set; }
        public DbSet<Description> T_Description { get; set; }
        public DbSet<Discount> T_Discount { get; set; }
        public DbSet<Picture> T_Picture { get; set; }
        public DbSet<ProductCart> T_ProductCart { get; set; }
        public DbSet<Property> T_Property { get; set; }
        public DbSet<Question> T_Question { get; set; }
        public DbSet<Specification> T_Specification { get; set; }
        public DbSet<Answer> T_Answer { get; set; }








        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("T_User");
            modelBuilder.Entity<User>().HasKey(t => t.Id);
            modelBuilder.Entity<User>().Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<User>().HasIndex(t => t.Username).IsUnique();
            modelBuilder.Entity<User>().Property(t => t.Username).IsRequired().HasMaxLength(30);
            modelBuilder.Entity<User>().Property(t => t.Password).IsRequired().HasMaxLength(64).HasColumnType("VarChar");
            modelBuilder.Entity<User>().Property(t => t.FirstName).IsRequired().HasMaxLength(30).HasColumnType("NvarChar");
            modelBuilder.Entity<User>().Property(t => t.LastName).IsRequired().HasMaxLength(30).HasColumnType("NvarChar");
            modelBuilder.Entity<User>().Property(t => t.RegisterDate).IsRequired();
            modelBuilder.Entity<User>().Property(t => t.IsActive).IsRequired();
            modelBuilder.Entity<User>().Property(t => t.Address).IsOptional().HasMaxLength(200);
            modelBuilder.Entity<User>().Property(t => t.ProfileImage).IsOptional().HasMaxLength(50).HasColumnType("VarChar");
            modelBuilder.Entity<User>().Property(t => t.RoleId).IsOptional();



            modelBuilder.Entity<Role>().ToTable("T_Role");
            modelBuilder.Entity<Role>().HasKey(t => t.Id);
            modelBuilder.Entity<Role>().Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Role>().HasIndex(t => t.Name).IsUnique(true);
            modelBuilder.Entity<Role>().Property(t => t.Name).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Role>().Property(t => t.Title).IsRequired().HasMaxLength(20).HasColumnType("NVarChar");



            modelBuilder.Entity<Category>().ToTable("T_Category");
            modelBuilder.Entity<Category>().HasKey(t => t.Id);
            modelBuilder.Entity<Category>().Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Category>().HasIndex(t => t.Name).IsUnique(true);
            modelBuilder.Entity<Category>().Property(t => t.Name).IsRequired().HasMaxLength(30).HasColumnType("NVarChar");
            modelBuilder.Entity<Category>().Property(t => t.Image).IsOptional().HasMaxLength(50).HasColumnType("VarChar");
            modelBuilder.Entity<Category>().Property(t => t.ParentId).IsOptional();



            modelBuilder.Entity<Product>().ToTable("T_Product");
            modelBuilder.Entity<Product>().HasKey(t => t.Id);
            modelBuilder.Entity<Product>().Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Product>().HasIndex(t => t.Name).IsUnique(true);
            modelBuilder.Entity<Product>().Property(t => t.Name).IsRequired().HasMaxLength(50).HasColumnType("NVarChar");
            modelBuilder.Entity<Product>().Property(t => t.InStockCount).IsRequired();
            modelBuilder.Entity<Product>().Property(t => t.Sold).IsRequired();
            modelBuilder.Entity<Product>().Property(t => t.IsActive).IsRequired();
            modelBuilder.Entity<Product>().Property(t => t.RegisterDate).IsRequired();
            modelBuilder.Entity<Product>().Property(t => t.Discount).IsOptional();
            modelBuilder.Entity<Product>().Property(t => t.Image).IsOptional().HasMaxLength(100);
            modelBuilder.Entity<Product>().Property(t => t.Price).IsRequired();





            modelBuilder.Entity<Description>().ToTable("T_Description");
            modelBuilder.Entity<Description>().HasKey(t => t.Id);
            modelBuilder.Entity<Description>().Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Description>().Property(t => t.Title).IsRequired().HasMaxLength(30).HasColumnType("NVarChar");
            modelBuilder.Entity<Description>().Property(t => t.Text).IsOptional().HasColumnType("NText");
            modelBuilder.Entity<Description>().Property(t => t.Image).IsOptional().HasMaxLength(50).HasColumnType("VarChar");
            modelBuilder.Entity<Description>().Property(t => t.Index).IsRequired();
            modelBuilder.Entity<Description>().Property(t => t.ProductId).IsOptional();




            modelBuilder.Entity<Specification>().ToTable("T_Specification");
            modelBuilder.Entity<Specification>().HasKey(t => t.Id);
            modelBuilder.Entity<Specification>().Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Specification>().Property(t => t.Title).IsRequired().HasMaxLength(30).HasColumnType("NVarChar");
            modelBuilder.Entity<Specification>().Property(t => t.Text).IsRequired().HasMaxLength(100).HasColumnType("NVarChar");
            modelBuilder.Entity<Specification>().Property(t => t.Index).IsRequired();
            modelBuilder.Entity<Specification>().Property(t => t.ProductId).IsOptional();




            modelBuilder.Entity<Property>().ToTable("T_Property");
            modelBuilder.Entity<Property>().HasKey(t => t.Id);
            modelBuilder.Entity<Property>().Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Property>().Property(t => t.Text).IsRequired().HasMaxLength(30).HasColumnType("NVarChar");
            modelBuilder.Entity<Property>().Property(t => t.Index).IsRequired();
            modelBuilder.Entity<Property>().Property(t => t.ProductId).IsOptional();




            modelBuilder.Entity<Cart>().ToTable("T_Cart");
            modelBuilder.Entity<Cart>().HasKey(t => t.Id);
            modelBuilder.Entity<Cart>().Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Cart>().Property(t => t.Cost).IsRequired();



            modelBuilder.Entity<ProductCart>().ToTable("T_ProductCart");
            modelBuilder.Entity<ProductCart>().HasKey(t => t.Id);
            modelBuilder.Entity<ProductCart>().Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ProductCart>().Property(t => t.Number).IsRequired();
            modelBuilder.Entity<ProductCart>().Property(t => t.ProductId).IsOptional();



            modelBuilder.Entity<Comment>().ToTable("T_Coment");
            modelBuilder.Entity<Comment>().HasKey(t => t.Id);
            modelBuilder.Entity<Comment>().Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Comment>().Property(t => t.Text).IsRequired().HasMaxLength(100).HasColumnType("NVarChar");
            modelBuilder.Entity<Comment>().Property(t => t.Rate).IsRequired();
            modelBuilder.Entity<Comment>().Property(t => t.UserId).IsRequired();
            modelBuilder.Entity<Comment>().Property(t => t.ProductId).IsRequired();
            modelBuilder.Entity<Comment>().Property(t => t.RegisterDate).IsRequired();
            modelBuilder.Entity<Comment>().Property(t => t.IsActive).IsRequired();



            modelBuilder.Entity<Discount>().ToTable("T_Discount");
            modelBuilder.Entity<Discount>().HasKey(t => t.Id);
            modelBuilder.Entity<Discount>().Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Discount>().HasIndex(t => t.Code).IsUnique(true);
            modelBuilder.Entity<Discount>().Property(t => t.Code).IsRequired().HasMaxLength(30).HasColumnType("NVarChar");
            modelBuilder.Entity<Discount>().Property(t => t.Percent).IsRequired();
            modelBuilder.Entity<Discount>().Property(t => t.MaxPrice).IsRequired();




            modelBuilder.Entity<Picture>().ToTable("T_Picture");
            modelBuilder.Entity<Picture>().HasKey(t => t.Id);
            modelBuilder.Entity<Picture>().Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Picture>().Property(t => t.Name).IsOptional().HasMaxLength(50).HasColumnType("VarChar");
            modelBuilder.Entity<Picture>().Property(t => t.Index).IsOptional();
            modelBuilder.Entity<Picture>().Property(t => t.ProductId).IsOptional();




            modelBuilder.Entity<Question>().ToTable("T_Question");
            modelBuilder.Entity<Question>().HasKey(t => t.Id);
            modelBuilder.Entity<Question>().Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Question>().Property(t => t.Text).IsRequired().HasMaxLength(100).HasColumnType("NVarChar");
            modelBuilder.Entity<Question>().Property(t => t.RegisterDate).IsRequired();
            modelBuilder.Entity<Question>().Property(t => t.ProductId).IsRequired();
            modelBuilder.Entity<Question>().Property(t => t.IsActive).IsRequired();
            modelBuilder.Entity<Question>().Property(t => t.UserId).IsOptional();



            modelBuilder.Entity<Answer>().ToTable("T_Answer");
            modelBuilder.Entity<Answer>().HasKey(t => t.Id);
            modelBuilder.Entity<Answer>().Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Answer>().Property(t => t.Text).IsRequired().HasMaxLength(100).HasColumnType("NVarChar");
            modelBuilder.Entity<Answer>().Property(t => t.RegisterDate).IsRequired();
            modelBuilder.Entity<Answer>().Property(t => t.IsActive).IsRequired();
            modelBuilder.Entity<Answer>().Property(t => t.UserId).IsOptional();










            modelBuilder.Entity<Role>().HasMany<User>(t => t.Users).WithOptional(t => t.Role).HasForeignKey(t => t.RoleId);

            modelBuilder.Entity<Category>().HasOptional<Category>(t => t.Category1).WithMany(y => y.Categories).HasForeignKey(t => t.ParentId);

            modelBuilder.Entity<Category>().HasMany<Product>(t => t.Products).WithOptional(y => y.Category).HasForeignKey(t => t.CategoryId).WillCascadeOnDelete(true);

            modelBuilder.Entity<Description>().HasOptional(t => t.Product).WithMany(t => t.Descriptions).HasForeignKey(t => t.ProductId).WillCascadeOnDelete(true); ;

            modelBuilder.Entity<Specification>().HasOptional(t => t.Product).WithMany(t => t.Specifications).HasForeignKey(t => t.ProductId).WillCascadeOnDelete(true);

            modelBuilder.Entity<Cart>().HasRequired<User>(t => t.User).WithOptional(t => t.Cart).WillCascadeOnDelete(true);

            modelBuilder.Entity<ProductCart>().HasOptional(t => t.Cart).WithMany(t => t.ProductCarts).HasForeignKey(t => t.CartId).WillCascadeOnDelete(true);

            modelBuilder.Entity<ProductCart>().HasOptional(t => t.Product).WithMany(t => t.ProductCarts).HasForeignKey(t => t.ProductId).WillCascadeOnDelete(true);

            modelBuilder.Entity<Comment>().HasRequired(t => t.Product).WithMany(t => t.Comments).HasForeignKey(t => t.ProductId).WillCascadeOnDelete(true);

            modelBuilder.Entity<Comment>().HasRequired(t => t.User).WithMany(t => t.Comments).HasForeignKey(t => t.UserId).WillCascadeOnDelete(true);
            
            modelBuilder.Entity<Picture>().HasOptional(t => t.Product).WithMany(t => t.Pictures).HasForeignKey(t => t.ProductId).WillCascadeOnDelete(true);

            modelBuilder.Entity<Question>().HasRequired(t => t.Product).WithMany(t => t.Questions).HasForeignKey(t => t.ProductId).WillCascadeOnDelete(true);
            
            modelBuilder.Entity<Question>().HasOptional(t => t.User).WithMany(t => t.Questions).HasForeignKey(t => t.UserId).WillCascadeOnDelete(true);

            modelBuilder.Entity<Answer>().HasOptional(t => t.User).WithMany(t => t.Answers).HasForeignKey(t => t.UserId);

            modelBuilder.Entity<Answer>().HasRequired(t => t.Question).WithOptional(t => t.Answer).WillCascadeOnDelete(true);

            modelBuilder.Entity<Property>().HasOptional(t => t.Product).WithMany(t => t.Properties).HasForeignKey(t => t.ProductId).WillCascadeOnDelete(true);

            modelBuilder.Entity<User>().HasMany(t => t.Products).WithMany(t => t.Users);







            base.OnModelCreating(modelBuilder);
        }


    }
}