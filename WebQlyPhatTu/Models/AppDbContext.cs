using Microsoft.EntityFrameworkCore;

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
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = LOANANH\\TA; Database = QuanLyPhatTu; Trusted_Connection = True; TrustServerCertificate = True");
        }
    }
}
