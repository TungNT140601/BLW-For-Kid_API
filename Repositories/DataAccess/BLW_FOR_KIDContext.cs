﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Repositories.EntityModels;
using Repositories.Ultilities;

namespace Repositories.DataAccess
{
    public partial class BLW_FOR_KIDContext : DbContext
    {
        public BLW_FOR_KIDContext()
        {
        }

        public BLW_FOR_KIDContext(DbContextOptions<BLW_FOR_KIDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Age> Ages { get; set; } = null!;
        public virtual DbSet<Baby> Babies { get; set; } = null!;
        public virtual DbSet<Chat> Chats { get; set; } = null!;
        public virtual DbSet<ChatHistory> ChatHistories { get; set; } = null!;
        public virtual DbSet<CustomerAccount> CustomerAccounts { get; set; } = null!;
        public virtual DbSet<Direction> Directions { get; set; } = null!;
        public virtual DbSet<Expert> Experts { get; set; } = null!;
        public virtual DbSet<GrowHistory> GrowHistories { get; set; } = null!;
        public virtual DbSet<GrowImage> GrowImages { get; set; } = null!;
        public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;
        public virtual DbSet<IngredientOfRecipe> IngredientOfRecipes { get; set; } = null!;
        public virtual DbSet<Meal> Meals { get; set; } = null!;
        public virtual DbSet<PaymentHistory> PaymentHistories { get; set; } = null!;
        public virtual DbSet<Plan> Plans { get; set; } = null!;
        public virtual DbSet<PlanDetail> PlanDetails { get; set; } = null!;
        public virtual DbSet<Rating> Ratings { get; set; } = null!;
        public virtual DbSet<Recipe> Recipes { get; set; } = null!;
        public virtual DbSet<StaffAccount> StaffAccounts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //                optionsBuilder.UseSqlServer("server=(local);uid=sa;pwd=1;database=BLW_FOR_KID;TrustServerCertificate=True;");
                optionsBuilder.UseSqlServer(GetConnectionString.ConnectionString());

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Age>(entity =>
            {
                entity.ToTable("Age");

                entity.Property(e => e.AgeId).HasMaxLength(20);

                entity.Property(e => e.AgeName).HasMaxLength(255);

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Baby>(entity =>
            {
                entity.ToTable("Baby");

                entity.Property(e => e.BabyId).HasMaxLength(20);

                entity.Property(e => e.Avatar).HasColumnType("text");

                entity.Property(e => e.Bmi).HasColumnName("BMI");

                entity.Property(e => e.CustomerId).HasMaxLength(20);

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Fullname).HasMaxLength(50);

                entity.Property(e => e.HealthType).HasMaxLength(255);

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Babies)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Baby__CustomerId__47DBAE45");
            });

            modelBuilder.Entity<Chat>(entity =>
            {
                entity.ToTable("Chat");

                entity.Property(e => e.ChatId).HasMaxLength(20);

                entity.Property(e => e.CustomerId).HasMaxLength(20);

                entity.Property(e => e.ExpertId).HasMaxLength(20);

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.StartChat).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Chats)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Chat__CustomerId__0C85DE4D");

                entity.HasOne(d => d.Expert)
                    .WithMany(p => p.Chats)
                    .HasForeignKey(d => d.ExpertId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Chat__ExpertId__0B91BA14");
            });

            modelBuilder.Entity<ChatHistory>(entity =>
            {
                entity.HasKey(e => new { e.ChatId, e.SendTime })
                    .HasName("PK__ChatHist__A8D990B1E49C9482");

                entity.ToTable("ChatHistory");

                entity.Property(e => e.ChatId).HasMaxLength(20);

                entity.Property(e => e.SendTime).HasColumnType("datetime");

                entity.Property(e => e.Image).HasColumnType("text");

                entity.Property(e => e.Message).HasColumnType("text");

                entity.Property(e => e.SendPerson).HasMaxLength(20);

                entity.HasOne(d => d.Chat)
                    .WithMany(p => p.ChatHistories)
                    .HasForeignKey(d => d.ChatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChatHisto__ChatI__10566F31");
            });

            modelBuilder.Entity<CustomerAccount>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK__Customer__A4AE64D88F74733B");

                entity.ToTable("CustomerAccount");

                entity.HasIndex(e => e.FacebookId, "UQ__Customer__4D6564654B7EDC80")
                    .IsUnique();

                entity.HasIndex(e => e.GoogleId, "UQ__Customer__A6FBF2FBBCAB3317")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Customer__A9D10534B6C8937D")
                    .IsUnique();

                entity.HasIndex(e => e.PhoneNum, "UQ__Customer__DF8F1A02227D342A")
                    .IsUnique();

                entity.Property(e => e.CustomerId).HasMaxLength(20);

                entity.Property(e => e.Avatar).HasColumnType("text");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.EndTriedDate).HasColumnType("datetime");

                entity.Property(e => e.FacebookId).HasMaxLength(255);

                entity.Property(e => e.Fullname).HasMaxLength(255);

                entity.Property(e => e.GoogleId).HasMaxLength(255);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsTried).HasDefaultValueSql("((0))");

                entity.Property(e => e.LastEndPremiumDate).HasColumnType("datetime");

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.LastPurchaseDate).HasColumnType("datetime");

                entity.Property(e => e.LastStartPremiumDate).HasColumnType("datetime");

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.PhoneNum).HasMaxLength(10);

                entity.Property(e => e.SigupDate).HasColumnType("datetime");

                entity.Property(e => e.StartTriedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.WasTried).HasDefaultValueSql("((0))");

                entity.HasMany(d => d.Recipes)
                    .WithMany(p => p.Customers)
                    .UsingEntity<Dictionary<string, object>>(
                        "Favorite",
                        l => l.HasOne<Recipe>().WithMany().HasForeignKey("RecipeId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Favorite__Recipe__08B54D69"),
                        r => r.HasOne<CustomerAccount>().WithMany().HasForeignKey("CustomerId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Favorite__Custom__07C12930"),
                        j =>
                        {
                            j.HasKey("CustomerId", "RecipeId").HasName("PK__Favorite__BB73FC5368201465");

                            j.ToTable("Favorite");

                            j.IndexerProperty<string>("CustomerId").HasMaxLength(20);

                            j.IndexerProperty<string>("RecipeId").HasMaxLength(20);
                        });
            });

            modelBuilder.Entity<Direction>(entity =>
            {
                entity.ToTable("Direction");

                entity.Property(e => e.DirectionId).HasMaxLength(20);

                entity.Property(e => e.DirectionDesc).HasColumnType("text");

                entity.Property(e => e.DirectionImage).HasColumnType("text");

                entity.Property(e => e.RecipeId).HasMaxLength(20);

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.Directions)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Direction__Recip__6FE99F9F");
            });

            modelBuilder.Entity<Expert>(entity =>
            {
                entity.ToTable("Expert");

                entity.HasIndex(e => e.FacebookId, "UQ__Expert__4D656465B3C0B35A")
                    .IsUnique();

                entity.HasIndex(e => e.GoogleId, "UQ__Expert__A6FBF2FB5D9C9B57")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Expert__A9D10534BB17B2C0")
                    .IsUnique();

                entity.HasIndex(e => e.PhoneNum, "UQ__Expert__DF8F1A02B03674F0")
                    .IsUnique();

                entity.Property(e => e.ExpertId).HasMaxLength(20);

                entity.Property(e => e.Achievements).HasColumnType("text");

                entity.Property(e => e.Avatar).HasColumnType("text");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.FacebookId).HasMaxLength(255);

                entity.Property(e => e.GoogleId).HasMaxLength(255);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.PhoneNum).HasMaxLength(10);

                entity.Property(e => e.Position).HasMaxLength(255);

                entity.Property(e => e.ProfessionalQualification).HasColumnType("text");

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.WorkProgress).HasColumnType("text");

                entity.Property(e => e.WorkUnit).HasMaxLength(255);
            });

            modelBuilder.Entity<GrowHistory>(entity =>
            {
                entity.HasKey(e => e.GrowId)
                    .HasName("PK__GrowHist__241DFFF0223283FD");

                entity.ToTable("GrowHistory");

                entity.Property(e => e.GrowId).HasMaxLength(20);

                entity.Property(e => e.BabyId).HasMaxLength(20);

                entity.Property(e => e.Bmi).HasColumnName("BMI");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.HealthType).HasMaxLength(255);

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Baby)
                    .WithMany(p => p.GrowHistories)
                    .HasForeignKey(d => d.BabyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GrowHisto__BabyI__4BAC3F29");
            });

            modelBuilder.Entity<GrowImage>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PK__GrowImag__7516F70CE98D3163");

                entity.ToTable("GrowImage");

                entity.Property(e => e.ImageId).HasMaxLength(20);

                entity.Property(e => e.GrowId).HasMaxLength(20);

                entity.Property(e => e.ImageLink).HasColumnType("text");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Grow)
                    .WithMany(p => p.GrowImages)
                    .HasForeignKey(d => d.GrowId)
                    .HasConstraintName("FK__GrowImage__GrowI__4F7CD00D");
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.ToTable("Ingredient");

                entity.Property(e => e.IngredientId).HasMaxLength(20);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.DeleteDate).HasColumnType("datetime");

                entity.Property(e => e.IngredientImage).HasColumnType("text");

                entity.Property(e => e.IngredientName).HasColumnType("text");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.Measure).HasMaxLength(255);

                entity.Property(e => e.StaffCreate).HasMaxLength(20);

                entity.Property(e => e.StaffDelete).HasMaxLength(20);

                entity.Property(e => e.StaffUpdate).HasMaxLength(20);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.HasOne(d => d.StaffCreateNavigation)
                    .WithMany(p => p.IngredientStaffCreateNavigations)
                    .HasForeignKey(d => d.StaffCreate)
                    .HasConstraintName("FK__Ingredien__Staff__5629CD9C");

                entity.HasOne(d => d.StaffDeleteNavigation)
                    .WithMany(p => p.IngredientStaffDeleteNavigations)
                    .HasForeignKey(d => d.StaffDelete)
                    .HasConstraintName("FK__Ingredien__Staff__5812160E");

                entity.HasOne(d => d.StaffUpdateNavigation)
                    .WithMany(p => p.IngredientStaffUpdateNavigations)
                    .HasForeignKey(d => d.StaffUpdate)
                    .HasConstraintName("FK__Ingredien__Staff__571DF1D5");
            });

            modelBuilder.Entity<IngredientOfRecipe>(entity =>
            {
                entity.HasKey(e => new { e.IngredientId, e.RecipeId })
                    .HasName("PK__Ingredie__A1732AD1D001E486");

                entity.ToTable("IngredientOfRecipe");

                entity.Property(e => e.IngredientId).HasMaxLength(20);

                entity.Property(e => e.RecipeId).HasMaxLength(20);

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.IngredientOfRecipes)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ingredien__Ingre__6C190EBB");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.IngredientOfRecipes)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ingredien__Recip__6D0D32F4");
            });

            modelBuilder.Entity<Meal>(entity =>
            {
                entity.ToTable("Meal");

                entity.Property(e => e.MealId).HasMaxLength(20);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.DeleteDate).HasColumnType("datetime");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.MealName).HasColumnType("text");

                entity.Property(e => e.StaffCreate).HasMaxLength(20);

                entity.Property(e => e.StaffDelete).HasMaxLength(20);

                entity.Property(e => e.StaffUpdate).HasMaxLength(20);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.HasOne(d => d.StaffCreateNavigation)
                    .WithMany(p => p.MealStaffCreateNavigations)
                    .HasForeignKey(d => d.StaffCreate)
                    .HasConstraintName("FK__Meal__StaffCreat__5BE2A6F2");

                entity.HasOne(d => d.StaffDeleteNavigation)
                    .WithMany(p => p.MealStaffDeleteNavigations)
                    .HasForeignKey(d => d.StaffDelete)
                    .HasConstraintName("FK__Meal__StaffDelet__5DCAEF64");

                entity.HasOne(d => d.StaffUpdateNavigation)
                    .WithMany(p => p.MealStaffUpdateNavigations)
                    .HasForeignKey(d => d.StaffUpdate)
                    .HasConstraintName("FK__Meal__StaffUpdat__5CD6CB2B");
            });

            modelBuilder.Entity<PaymentHistory>(entity =>
            {
                entity.HasKey(e => e.PaymentId)
                    .HasName("PK__PaymentH__9B556A38F0585C09");

                entity.ToTable("PaymentHistory");

                entity.Property(e => e.PaymentId).HasMaxLength(20);

                entity.Property(e => e.CustomerId).HasMaxLength(20);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.PurchaseTime).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.PaymentHistories)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PaymentHi__Custo__534D60F1");
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.ToTable("Plan");

                entity.Property(e => e.PlanId).HasMaxLength(20);

                entity.Property(e => e.AgeId).HasMaxLength(20);

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.PlanName).HasMaxLength(150);

                entity.HasOne(d => d.Age)
                    .WithMany(p => p.Plans)
                    .HasForeignKey(d => d.AgeId)
                    .HasConstraintName("FK__Plan__AgeId__72C60C4A");
            });

            modelBuilder.Entity<PlanDetail>(entity =>
            {
                entity.ToTable("PlanDetail");

                entity.Property(e => e.PlanDetailId).HasMaxLength(20);

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.PlanId).HasMaxLength(20);

                entity.Property(e => e.RecipeId).HasMaxLength(20);

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.PlanDetails)
                    .HasForeignKey(d => d.PlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PlanDetai__PlanI__76969D2E");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.PlanDetails)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PlanDetai__Recip__778AC167");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.ToTable("Rating");

                entity.Property(e => e.RatingId).HasMaxLength(20);

                entity.Property(e => e.Comment).HasColumnType("text");

                entity.Property(e => e.CustomerId).HasMaxLength(20);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.RatingImage).HasColumnType("text");

                entity.Property(e => e.RecipeId).HasMaxLength(20);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rating__Customer__02FC7413");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rating__RecipeId__03F0984C");
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.ToTable("Recipe");

                entity.Property(e => e.RecipeId).HasMaxLength(20);

                entity.Property(e => e.AgeId).HasMaxLength(20);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.DeleteDate).HasColumnType("datetime");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.MealId).HasMaxLength(20);

                entity.Property(e => e.RecipeImage).HasColumnType("text");

                entity.Property(e => e.RecipeName).HasColumnType("text");

                entity.Property(e => e.StaffCreate).HasMaxLength(20);

                entity.Property(e => e.StaffDelete).HasMaxLength(20);

                entity.Property(e => e.StaffUpdate).HasMaxLength(20);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Age)
                    .WithMany(p => p.Recipes)
                    .HasForeignKey(d => d.AgeId)
                    .HasConstraintName("FK__Recipe__AgeId__68487DD7");

                entity.HasOne(d => d.Meal)
                    .WithMany(p => p.Recipes)
                    .HasForeignKey(d => d.MealId)
                    .HasConstraintName("FK__Recipe__MealId__6754599E");

                entity.HasOne(d => d.StaffCreateNavigation)
                    .WithMany(p => p.RecipeStaffCreateNavigations)
                    .HasForeignKey(d => d.StaffCreate)
                    .HasConstraintName("FK__Recipe__StaffCre__6477ECF3");

                entity.HasOne(d => d.StaffDeleteNavigation)
                    .WithMany(p => p.RecipeStaffDeleteNavigations)
                    .HasForeignKey(d => d.StaffDelete)
                    .HasConstraintName("FK__Recipe__StaffDel__66603565");

                entity.HasOne(d => d.StaffUpdateNavigation)
                    .WithMany(p => p.RecipeStaffUpdateNavigations)
                    .HasForeignKey(d => d.StaffUpdate)
                    .HasConstraintName("FK__Recipe__StaffUpd__656C112C");
            });

            modelBuilder.Entity<StaffAccount>(entity =>
            {
                entity.HasKey(e => e.StaffId)
                    .HasName("PK__StaffAcc__96D4AB17C8A4EF22");

                entity.ToTable("StaffAccount");

                entity.HasIndex(e => e.GoogleId, "UQ__StaffAcc__A6FBF2FB28CAB0F7")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__StaffAcc__A9D1053404403CD9")
                    .IsUnique();

                entity.Property(e => e.StaffId).HasMaxLength(20);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Fullname).HasMaxLength(255);

                entity.Property(e => e.GoogleId).HasMaxLength(255);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Role).HasDefaultValueSql("((1))");

                entity.Property(e => e.Username).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}