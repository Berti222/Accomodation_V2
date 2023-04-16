using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AccomodationModel.Models;

public partial class AccomodationContext : DbContext
{
    public AccomodationContext()
    {
    }

    public AccomodationContext(DbContextOptions<AccomodationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Allergenic> Allergenics { get; set; }

    public virtual DbSet<Equipment> Equipment { get; set; }

    public virtual DbSet<Food> Foods { get; set; }

    public virtual DbSet<Guest> Guests { get; set; }

    public virtual DbSet<Meal> Meals { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomPrice> RoomPrices { get; set; }

    public virtual DbSet<RoomType> RoomTypes { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\Accomodation;Database=Accomodation;Trusted_Connection=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Allergenic>(entity =>
        {
            entity.ToTable("Allergenic");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasMany(d => d.Rooms).WithMany(p => p.Equipment)
                .UsingEntity<Dictionary<string, object>>(
                    "EquipmentRoom",
                    r => r.HasOne<Room>().WithMany()
                        .HasForeignKey("RoomId")
                        .HasConstraintName("FK_EquipmentRoom_Room"),
                    l => l.HasOne<Equipment>().WithMany()
                        .HasForeignKey("EquipmentId")
                        .HasConstraintName("FK_EquipmentRoom_Equipment"),
                    j =>
                    {
                        j.HasKey("EquipmentId", "RoomId").HasName("PK_EquipmentRoomIds");
                        j.ToTable("EquipmentRoom");
                    });
        });

        modelBuilder.Entity<Food>(entity =>
        {
            entity.ToTable("Food");

            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.FoodType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasMany(d => d.Allergenics).WithMany(p => p.Foods)
                .UsingEntity<Dictionary<string, object>>(
                    "FoodAllergenic",
                    r => r.HasOne<Allergenic>().WithMany()
                        .HasForeignKey("AllergenicId")
                        .HasConstraintName("FK_FoodAllergenic_Allergenic"),
                    l => l.HasOne<Food>().WithMany()
                        .HasForeignKey("FoodId")
                        .HasConstraintName("FK_FoodAllergenic_Food"),
                    j =>
                    {
                        j.HasKey("FoodId", "AllergenicId").HasName("PK_FoodAllergenicIds");
                        j.ToTable("FoodAllergenic");
                    });
        });

        modelBuilder.Entity<Guest>(entity =>
        {
            entity.ToTable("Guest");

            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IdentityNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumer)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasMany(d => d.Allergenics).WithMany(p => p.Guests)
                .UsingEntity<Dictionary<string, object>>(
                    "GuestAllergenic",
                    r => r.HasOne<Allergenic>().WithMany()
                        .HasForeignKey("AllergenicId")
                        .HasConstraintName("FK_GuestAllergenic_Allergenic"),
                    l => l.HasOne<Guest>().WithMany()
                        .HasForeignKey("GuestId")
                        .HasConstraintName("FK_GuestAllergenic_Guest"),
                    j =>
                    {
                        j.HasKey("GuestId", "AllergenicId").HasName("PK_GuestAllergenicIds");
                        j.ToTable("GuestAllergenic");
                    });

            entity.HasMany(d => d.Reservations).WithMany(p => p.Guests)
                .UsingEntity<Dictionary<string, object>>(
                    "GuestReservation",
                    r => r.HasOne<Reservation>().WithMany()
                        .HasForeignKey("ReservationId")
                        .HasConstraintName("FK_GuestReservation_Reservation"),
                    l => l.HasOne<Guest>().WithMany()
                        .HasForeignKey("GuestId")
                        .HasConstraintName("FK_GuestReservation_Guest"),
                    j =>
                    {
                        j.HasKey("GuestId", "ReservationId").HasName("PK_GuestReservationIds");
                        j.ToTable("GuestReservation");
                    });
        });

        modelBuilder.Entity<Meal>(entity =>
        {
            entity.ToTable("Meal");

            entity.Property(e => e.MealType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.ServiceDate).HasColumnType("date");

            entity.HasOne(d => d.Food).WithMany(p => p.Meals)
                .HasForeignKey(d => d.FoodId)
                .HasConstraintName("FK_Meal_Food");

            entity.HasOne(d => d.Guest).WithMany(p => p.Meals)
                .HasForeignKey(d => d.GuestId)
                .HasConstraintName("FK_Meal_Guest");

            entity.HasOne(d => d.Reservation).WithMany(p => p.Meals)
                .HasForeignKey(d => d.ReservationId)
                .HasConstraintName("FK_Meal_Reservation");

            entity.HasMany(d => d.Foods).WithMany(p => p.MealsNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "MealFood",
                    r => r.HasOne<Food>().WithMany()
                        .HasForeignKey("FoodId")
                        .HasConstraintName("FK_MealFood_Food"),
                    l => l.HasOne<Meal>().WithMany()
                        .HasForeignKey("MealId")
                        .HasConstraintName("FK_MealFood_Meal"),
                    j =>
                    {
                        j.HasKey("MealId", "FoodId").HasName("PK_MealFoodIds");
                        j.ToTable("MealFood");
                    });
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.ToTable("Reservation");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Discount).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.TotalPrice).HasColumnType("money");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.ToTable("Room");

            entity.HasOne(d => d.RoomType).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.RoomTypeId)
                .HasConstraintName("FK_Room_RoomType");

            entity.HasMany(d => d.Reservations).WithMany(p => p.Rooms)
                .UsingEntity<Dictionary<string, object>>(
                    "RoomReservation",
                    r => r.HasOne<Reservation>().WithMany()
                        .HasForeignKey("ReservationId")
                        .HasConstraintName("FK_RoomReservation_Reservation"),
                    l => l.HasOne<Room>().WithMany()
                        .HasForeignKey("RoomId")
                        .HasConstraintName("FK_RoomReservation_Guest"),
                    j =>
                    {
                        j.HasKey("RoomId", "ReservationId").HasName("PK_RoomReservationIds");
                        j.ToTable("RoomReservation");
                    });
        });

        modelBuilder.Entity<RoomPrice>(entity =>
        {
            entity.ToTable("RoomPrice");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ExtraBedPrice).HasColumnType("money");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasOne(d => d.RoomType).WithMany(p => p.RoomPrices)
                .HasForeignKey(d => d.RoomTypeId)
                .HasConstraintName("FK_RoomPrice_RoomType");
        });

        modelBuilder.Entity<RoomType>(entity =>
        {
            entity.ToTable("RoomType");

            entity.Property(e => e.Direction)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Discription).HasMaxLength(2000);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.NumberOfRoom).HasDefaultValueSql("((0))");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.ToTable("Service");

            entity.Property(e => e.Discription).HasMaxLength(2000);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.TypeOfPayment)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
