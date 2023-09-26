using Microsoft.EntityFrameworkCore;
using WebQlyPhatTu.Dto;

namespace WebQlyPhatTu.Models
{
    public class AppDbContext:DbContext
    {
        public virtual DbSet<Chua> Chua { get; set; }
        public virtual DbSet<KieuThanhVien> KieuThanhVien { get; set; }
        public virtual DbSet<PhatTu> PhatTu { get; set; }
        public virtual DbSet<Token> Token { get; set; }
        public virtual DbSet<DaoTrang> DaoTrang { get; set; }
        public virtual DbSet<DonDangKy> DonDangKy { get; set; }
        public virtual DbSet<PhatTuDaoTrang> PhatTuDaoTrang { get; set; }
        public virtual DbSet<PasswordRefresh> PasswordRefresh { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = LOANANH\\TA; Database = QuanLyPhatTu; Trusted_Connection = True; TrustServerCertificate = True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define the 'Role' table and its columns
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, NameRole = "Quản trị viên", Code ="ADMIN"},
                new Role { RoleId = 2, NameRole = "Thành viên", Code = "MEMBER" }
            );
            modelBuilder.Entity<KieuThanhVien>().HasData(
                new KieuThanhVien { KieuThanhVienId = 1, TenKieu = "Trụ trì", Code = "TRUTRI" },
                new KieuThanhVien { KieuThanhVienId = 2, TenKieu = "Đệ tử", Code = "DETU" }
            );
            modelBuilder.Entity<PhatTu>().HasData(
                new PhatTu {PhatTuId = 1, Ten="Admin", Email="admin", PassWord=BCrypt.Net.BCrypt.HashPassword("123456")}
            );
            modelBuilder.Entity<UserRoles>().HasData(
                new UserRoles { Id=1, PhatTuId = 1, RoleId = 1 }
            );
        }
    }
}
