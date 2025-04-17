using DATA.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class ModelDbContext : DbContext
    {
        public ModelDbContext()
        {
        }

        public ModelDbContext(DbContextOptions options) : base(options)
        {

        }


        public virtual DbSet<AddressUser> AddressUser { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<CartDetail> CartDetail { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Color> Color { get; set; }
        public virtual DbSet<Material> Material { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<Origin> Origin { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductItem> ProductItem { get; set; }
        public virtual DbSet<Promotion> Promotion { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Size> Size { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Voucher> Voucher { get; set; }
        public virtual DbSet<VoucherUser> VoucherUser { get; set; }
        public virtual DbSet<PromotionDetail> PromotionDetail { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
