using System;
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
        public virtual DbSet<Favorite> Favorites { get; set; } = null!;
        public virtual DbSet<GrowHistory> GrowHistories { get; set; } = null!;
        public virtual DbSet<GrowImage> GrowImages { get; set; } = null!;
        public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;
        public virtual DbSet<IngredientOfRecipe> IngredientOfRecipes { get; set; } = null!;
        public virtual DbSet<Meal> Meals { get; set; } = null!;
        public virtual DbSet<PaymentHistory> PaymentHistories { get; set; } = null!;
        public virtual DbSet<Plan> Plans { get; set; } = null!;
        public virtual DbSet<PlanDetail> PlanDetails { get; set; } = null!;
        public virtual DbSet<PremiumPackage> PremiumPackages { get; set; } = null!;
        public virtual DbSet<Rating> Ratings { get; set; } = null!;
        public virtual DbSet<Recipe> Recipes { get; set; } = null!;
        public virtual DbSet<StaffAccount> StaffAccounts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
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
                    .HasConstraintName("FK__Baby__CustomerId__72C60C4A");
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
                    .HasConstraintName("FK__Chat__CustomerId__778AC167");

                entity.HasOne(d => d.Expert)
                    .WithMany(p => p.Chats)
                    .HasForeignKey(d => d.ExpertId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Chat__ExpertId__76969D2E");
            });

            modelBuilder.Entity<ChatHistory>(entity =>
            {
                entity.ToTable("ChatHistory");

                entity.Property(e => e.ChatHistoryId).HasMaxLength(20);

                entity.Property(e => e.ChatId).HasMaxLength(20);

                entity.Property(e => e.Image).HasColumnType("text");

                entity.Property(e => e.Message).HasColumnType("text");

                entity.Property(e => e.SendPerson).HasMaxLength(20);

                entity.Property(e => e.SendTime).HasColumnType("datetime");

                entity.HasOne(d => d.Chat)
                    .WithMany(p => p.ChatHistories)
                    .HasForeignKey(d => d.ChatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChatHisto__ChatI__7B5B524B");
            });

            modelBuilder.Entity<CustomerAccount>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK__Customer__A4AE64D8F6EB5E9C");

                entity.ToTable("CustomerAccount");

                entity.HasIndex(e => e.Email, "idx_unique_email")
                    .IsUnique()
                    .HasFilter("([Email] IS NOT NULL)");

                entity.HasIndex(e => e.FacebookId, "idx_unique_facebookid")
                    .IsUnique()
                    .HasFilter("([FacebookId] IS NOT NULL)");

                entity.HasIndex(e => e.GoogleId, "idx_unique_googleid")
                    .IsUnique()
                    .HasFilter("([GoogleId] IS NOT NULL)");

                entity.HasIndex(e => e.PhoneNum, "idx_unique_phone")
                    .IsUnique()
                    .HasFilter("([PhoneNum] IS NOT NULL)");

                entity.Property(e => e.CustomerId).HasMaxLength(20);

                entity.Property(e => e.Avatar).HasColumnType("text");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.EndTriedDate).HasColumnType("datetime");

                entity.Property(e => e.FacebookId).HasMaxLength(255);

                entity.Property(e => e.Fullname).HasMaxLength(255);

                entity.Property(e => e.GoogleId).HasMaxLength(255);

                entity.Property(e => e.LastEndPremiumDate).HasColumnType("datetime");

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.LastPurchaseDate).HasColumnType("datetime");

                entity.Property(e => e.LastStartPremiumDate).HasColumnType("datetime");

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.PhoneNum).HasMaxLength(10);

                entity.Property(e => e.SigupDate).HasColumnType("datetime");

                entity.Property(e => e.StartTriedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
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
                    .HasConstraintName("FK__Direction__Recip__3A179ED3");
            });

            modelBuilder.Entity<Expert>(entity =>
            {
                entity.ToTable("Expert");

                entity.HasIndex(e => e.Email, "idx_unique_email")
                    .IsUnique()
                    .HasFilter("([Email] IS NOT NULL)");

                entity.HasIndex(e => e.FacebookId, "idx_unique_facebookid")
                    .IsUnique()
                    .HasFilter("([FacebookId] IS NOT NULL)");

                entity.HasIndex(e => e.GoogleId, "idx_unique_googleid")
                    .IsUnique()
                    .HasFilter("([GoogleId] IS NOT NULL)");

                entity.HasIndex(e => e.PhoneNum, "idx_unique_phone")
                    .IsUnique()
                    .HasFilter("([PhoneNum] IS NOT NULL)");

                entity.Property(e => e.ExpertId).HasMaxLength(20);

                entity.Property(e => e.Achievements).HasColumnType("text");

                entity.Property(e => e.Avatar).HasColumnType("text");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.FacebookId).HasMaxLength(255);

                entity.Property(e => e.GoogleId).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.PhoneNum).HasMaxLength(10);

                entity.Property(e => e.Position).HasMaxLength(255);

                entity.Property(e => e.ProfessionalQualification).HasColumnType("text");

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.Username).HasMaxLength(255);

                entity.Property(e => e.WorkProgress).HasColumnType("text");

                entity.Property(e => e.WorkUnit).HasMaxLength(255);
            });

            modelBuilder.Entity<Favorite>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.RecipeId })
                    .HasName("PK__Favorite__BB73FC53EA91971B");

                entity.ToTable("Favorite");

                entity.Property(e => e.CustomerId).HasMaxLength(20);

                entity.Property(e => e.RecipeId).HasMaxLength(20);

                entity.Property(e => e.FavoriteTime).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favorite__Custom__54CB950F");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favorite__Recipe__55BFB948");
            });

            modelBuilder.Entity<GrowHistory>(entity =>
            {
                entity.HasKey(e => e.GrowId)
                    .HasName("PK__GrowHist__241DFFF056336CB6");

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
                    .HasConstraintName("FK__GrowHisto__BabyI__00200768");
            });

            modelBuilder.Entity<GrowImage>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PK__GrowImag__7516F70CE42CB2AB");

                entity.ToTable("GrowImage");

                entity.Property(e => e.ImageId).HasMaxLength(20);

                entity.Property(e => e.GrowId).HasMaxLength(20);

                entity.Property(e => e.ImageLink).HasColumnType("text");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Grow)
                    .WithMany(p => p.GrowImages)
                    .HasForeignKey(d => d.GrowId)
                    .HasConstraintName("FK__GrowImage__GrowI__03F0984C");
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
                    .HasConstraintName("FK__Ingredien__Staff__07C12930");

                entity.HasOne(d => d.StaffDeleteNavigation)
                    .WithMany(p => p.IngredientStaffDeleteNavigations)
                    .HasForeignKey(d => d.StaffDelete)
                    .HasConstraintName("FK__Ingredien__Staff__09A971A2");

                entity.HasOne(d => d.StaffUpdateNavigation)
                    .WithMany(p => p.IngredientStaffUpdateNavigations)
                    .HasForeignKey(d => d.StaffUpdate)
                    .HasConstraintName("FK__Ingredien__Staff__08B54D69");
            });

            modelBuilder.Entity<IngredientOfRecipe>(entity =>
            {
                entity.HasKey(e => new { e.IngredientId, e.RecipeId })
                    .HasName("PK__Ingredie__A1732AD10552D447");

                entity.ToTable("IngredientOfRecipe");

                entity.Property(e => e.IngredientId).HasMaxLength(20);

                entity.Property(e => e.RecipeId).HasMaxLength(20);

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.IngredientOfRecipes)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ingredien__Ingre__3587F3E0");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.IngredientOfRecipes)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ingredien__Recip__367C1819");
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
                    .HasConstraintName("FK__Meal__StaffCreat__114A936A");

                entity.HasOne(d => d.StaffDeleteNavigation)
                    .WithMany(p => p.MealStaffDeleteNavigations)
                    .HasForeignKey(d => d.StaffDelete)
                    .HasConstraintName("FK__Meal__StaffDelet__1332DBDC");

                entity.HasOne(d => d.StaffUpdateNavigation)
                    .WithMany(p => p.MealStaffUpdateNavigations)
                    .HasForeignKey(d => d.StaffUpdate)
                    .HasConstraintName("FK__Meal__StaffUpdat__123EB7A3");
            });

            modelBuilder.Entity<PaymentHistory>(entity =>
            {
                entity.HasKey(e => e.PaymentId)
                    .HasName("PK__PaymentH__9B556A387A274352");

                entity.ToTable("PaymentHistory");

                entity.Property(e => e.PaymentId).HasMaxLength(20);

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerId).HasMaxLength(20);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.PackageId).HasMaxLength(20);

                entity.Property(e => e.PayTime).HasColumnType("datetime");

                entity.Property(e => e.PrivateCode).HasMaxLength(1);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.PaymentHistories)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PaymentHi__Custo__17036CC0");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.PaymentHistories)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PaymentHi__Packa__17F790F9");
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
                    .HasConstraintName("FK__Plan__AgeId__5FB337D6");
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
                    .HasConstraintName("FK__PlanDetai__PlanI__3C34F16F");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.PlanDetails)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PlanDetai__Recip__3D2915A8");
            });

            modelBuilder.Entity<PremiumPackage>(entity =>
            {
                entity.HasKey(e => e.PackageId)
                    .HasName("PK__PremiumP__322035CC418AE46A");

                entity.ToTable("PremiumPackage");

                entity.Property(e => e.PackageId).HasMaxLength(20);

                entity.Property(e => e.PackageAmount).HasColumnType("money");

                entity.Property(e => e.PackageName).HasMaxLength(255);
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
                    .HasConstraintName("FK__Rating__Customer__41EDCAC5");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rating__RecipeId__42E1EEFE");
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
                    .HasConstraintName("FK__Recipe__AgeId__282DF8C2");

                entity.HasOne(d => d.Meal)
                    .WithMany(p => p.Recipes)
                    .HasForeignKey(d => d.MealId)
                    .HasConstraintName("FK__Recipe__MealId__2739D489");

                entity.HasOne(d => d.StaffCreateNavigation)
                    .WithMany(p => p.RecipeStaffCreateNavigations)
                    .HasForeignKey(d => d.StaffCreate)
                    .HasConstraintName("FK__Recipe__StaffCre__245D67DE");

                entity.HasOne(d => d.StaffDeleteNavigation)
                    .WithMany(p => p.RecipeStaffDeleteNavigations)
                    .HasForeignKey(d => d.StaffDelete)
                    .HasConstraintName("FK__Recipe__StaffDel__2645B050");

                entity.HasOne(d => d.StaffUpdateNavigation)
                    .WithMany(p => p.RecipeStaffUpdateNavigations)
                    .HasForeignKey(d => d.StaffUpdate)
                    .HasConstraintName("FK__Recipe__StaffUpd__25518C17");
            });

            modelBuilder.Entity<StaffAccount>(entity =>
            {
                entity.HasKey(e => e.StaffId)
                    .HasName("PK__StaffAcc__96D4AB17BA81E33A");

                entity.ToTable("StaffAccount");

                entity.HasIndex(e => e.Email, "idx_unique_email")
                    .IsUnique()
                    .HasFilter("([Email] IS NOT NULL)");

                entity.HasIndex(e => e.GoogleId, "idx_unique_googleid")
                    .IsUnique()
                    .HasFilter("([GoogleId] IS NOT NULL)");

                entity.Property(e => e.StaffId).HasMaxLength(20);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Fullname).HasMaxLength(255);

                entity.Property(e => e.GoogleId).HasMaxLength(255);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
