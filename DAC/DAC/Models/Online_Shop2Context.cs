using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DAC.Models
{
    public partial class Online_Shop2Context : DbContext
    {
        public Online_Shop2Context()
        {
        }

        public Online_Shop2Context(DbContextOptions<Online_Shop2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<ItemCart> ItemCarts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserAddress> UserAddresses { get; set; }
        public virtual DbSet<UserPayment> UserPayments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=Online_Shop2;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => e.IdCart)
                    .HasName("PK__cart__C71FE31729FFA02F");

                entity.ToTable("cart");

                entity.Property(e => e.IdCart).HasColumnName("id_cart");

                entity.Property(e => e.IdItemCart).HasColumnName("id_item_cart");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.HasOne(d => d.IdItemCartNavigation)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.IdItemCart)
                    .HasConstraintName("cart_fk1");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("cart_fk0");
            });

            modelBuilder.Entity<ItemCart>(entity =>
            {
                entity.HasKey(e => e.IdCartItem)
                    .HasName("PK__item_car__9E92CDADAFB10C71");

                entity.ToTable("item_cart");

                entity.Property(e => e.IdCartItem).HasColumnName("id_cart_item");

                entity.Property(e => e.IdProduct).HasColumnName("id_product");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.ItemCarts)
                    .HasForeignKey(d => d.IdProduct)
                    .HasConstraintName("item_cart_fk0");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.IdOrder)
                    .HasName("PK__order__DD5B8F3FA3CB8C61");

                entity.ToTable("order");

                entity.Property(e => e.IdOrder).HasColumnName("id_order");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.OrderStatus)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("order_status")
                    .HasDefaultValueSql("('Processing')");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("order_fk1");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.IdOrderItem)
                    .HasName("PK__order_it__2453F0126EDF5823");

                entity.ToTable("order_item");

                entity.Property(e => e.IdOrderItem).HasColumnName("id_order_item");

                entity.Property(e => e.IdOrder).HasColumnName("id_order");

                entity.Property(e => e.IdProduct).HasColumnName("id_product");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.IdOrder)
                    .HasConstraintName("order_item_fk1");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.IdProduct)
                    .HasConstraintName("order_item_fk0");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.IdProduct)
                    .HasName("PK__product__BA39E84F961905CB");

                entity.ToTable("product");

                entity.Property(e => e.IdProduct).HasColumnName("id_product");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("price");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK__user__D2D14637F4993F48");

                entity.ToTable("user");

                entity.HasIndex(e => e.Username, "UQ__user__F3DBC57269999E3C")
                    .IsUnique();

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<UserAddress>(entity =>
            {
                entity.ToTable("user_address");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("country");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("mobile_number");

                entity.Property(e => e.PostalCod)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("postal_cod");

                entity.Property(e => e.StreetName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("street_name");

                entity.Property(e => e.StreetNr).HasColumnName("street_nr");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.UserAddresses)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("user_address_fk0");
            });

            modelBuilder.Entity<UserPayment>(entity =>
            {
                entity.HasKey(e => e.IdUserPayment)
                    .HasName("PK__user_pay__160A651C4565CA06");

                entity.ToTable("user_payment");

                entity.Property(e => e.IdUserPayment).HasColumnName("id_user_payment");

                entity.Property(e => e.CardNumber)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("card_number");

                entity.Property(e => e.Csv)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("csv");

                entity.Property(e => e.DateExp)
                    .HasColumnType("date")
                    .HasColumnName("date_exp");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.UserPayments)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("user_payment_fk0");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
