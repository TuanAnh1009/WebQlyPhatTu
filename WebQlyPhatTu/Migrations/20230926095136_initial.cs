using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebQlyPhatTu.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chua",
                columns: table => new
                {
                    ChuaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CapNhap = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayThanhLap = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TenChua = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TruTri = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chua", x => x.ChuaId);
                });

            migrationBuilder.CreateTable(
                name: "DaoTrang",
                columns: table => new
                {
                    DaoTrangId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DaKetThuc = table.Column<bool>(type: "bit", nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoiToChuc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoThanhVienThamGia = table.Column<int>(type: "int", nullable: false),
                    NguoiTruTri = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaoTrang", x => x.DaoTrangId);
                });

            migrationBuilder.CreateTable(
                name: "KieuThanhVien",
                columns: table => new
                {
                    KieuThanhVienId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenKieu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KieuThanhVien", x => x.KieuThanhVienId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "PhatTu",
                columns: table => new
                {
                    PhatTuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnhChup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DaHoanTuc = table.Column<bool>(type: "bit", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GioiTinh = table.Column<int>(type: "int", nullable: true),
                    Ho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayHoanTuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayXuatGia = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PassWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhapDanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenDem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChuaId = table.Column<int>(type: "int", nullable: true),
                    KieuThanhVienId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhatTu", x => x.PhatTuId);
                    table.ForeignKey(
                        name: "FK_PhatTu_Chua_ChuaId",
                        column: x => x.ChuaId,
                        principalTable: "Chua",
                        principalColumn: "ChuaId");
                    table.ForeignKey(
                        name: "FK_PhatTu_KieuThanhVien_KieuThanhVienId",
                        column: x => x.KieuThanhVienId,
                        principalTable: "KieuThanhVien",
                        principalColumn: "KieuThanhVienId");
                });

            migrationBuilder.CreateTable(
                name: "DonDangKy",
                columns: table => new
                {
                    DonDangKyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayGuiDon = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayXuLy = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiXuLy = table.Column<int>(type: "int", nullable: true),
                    TrangThaiDon = table.Column<int>(type: "int", nullable: false),
                    DaoTrangId = table.Column<int>(type: "int", nullable: false),
                    PhatTuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonDangKy", x => x.DonDangKyId);
                    table.ForeignKey(
                        name: "FK_DonDangKy_DaoTrang_DaoTrangId",
                        column: x => x.DaoTrangId,
                        principalTable: "DaoTrang",
                        principalColumn: "DaoTrangId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonDangKy_PhatTu_PhatTuId",
                        column: x => x.PhatTuId,
                        principalTable: "PhatTu",
                        principalColumn: "PhatTuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PasswordRefresh",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhatTuId = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expired = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordRefresh", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasswordRefresh_PhatTu_PhatTuId",
                        column: x => x.PhatTuId,
                        principalTable: "PhatTu",
                        principalColumn: "PhatTuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhatTuDaoTrang",
                columns: table => new
                {
                    PhatTuDaoTrangId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DaThamGia = table.Column<bool>(type: "bit", nullable: false),
                    LiDoKhongThamGia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DaoTrangId = table.Column<int>(type: "int", nullable: false),
                    PhatTuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhatTuDaoTrang", x => x.PhatTuDaoTrangId);
                    table.ForeignKey(
                        name: "FK_PhatTuDaoTrang_DaoTrang_DaoTrangId",
                        column: x => x.DaoTrangId,
                        principalTable: "DaoTrang",
                        principalColumn: "DaoTrangId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhatTuDaoTrang_PhatTu_PhatTuId",
                        column: x => x.PhatTuId,
                        principalTable: "PhatTu",
                        principalColumn: "PhatTuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Token",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Stoken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sxpired = table.Column<int>(type: "int", nullable: false),
                    Revoked = table.Column<int>(type: "int", nullable: false),
                    TokenType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhatTuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Token", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Token_PhatTu_PhatTuId",
                        column: x => x.PhatTuId,
                        principalTable: "PhatTu",
                        principalColumn: "PhatTuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhatTuId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_PhatTu_PhatTuId",
                        column: x => x.PhatTuId,
                        principalTable: "PhatTu",
                        principalColumn: "PhatTuId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "KieuThanhVien",
                columns: new[] { "KieuThanhVienId", "Code", "TenKieu" },
                values: new object[,]
                {
                    { 1, "TRUTRI", "Trụ trì" },
                    { 2, "DETU", "Đệ tử" }
                });

            migrationBuilder.InsertData(
                table: "PhatTu",
                columns: new[] { "PhatTuId", "AnhChup", "ChuaId", "DaHoanTuc", "Email", "GioiTinh", "Ho", "IsActive", "KieuThanhVienId", "NgayCapNhat", "NgayHoanTuc", "NgaySinh", "NgayXuatGia", "PassWord", "PhapDanh", "SoDienThoai", "Ten", "TenDem" },
                values: new object[] { 1, null, null, false, "admin", null, null, true, null, new DateTime(2023, 9, 26, 9, 51, 36, 505, DateTimeKind.Utc).AddTicks(5253), null, null, null, "$2a$11$PSqcm7TcUyKeUYI/GcdhsunAqHlda56tEL9E42m9jRjTsZFXgyOTS", null, null, "Admin", null });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "Code", "NameRole" },
                values: new object[,]
                {
                    { 1, "ADMIN", "Quản trị viên" },
                    { 2, "MEMBER", "Thành viên" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "PhatTuId", "RoleId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_DonDangKy_DaoTrangId",
                table: "DonDangKy",
                column: "DaoTrangId");

            migrationBuilder.CreateIndex(
                name: "IX_DonDangKy_PhatTuId",
                table: "DonDangKy",
                column: "PhatTuId");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordRefresh_PhatTuId",
                table: "PasswordRefresh",
                column: "PhatTuId");

            migrationBuilder.CreateIndex(
                name: "IX_PhatTu_ChuaId",
                table: "PhatTu",
                column: "ChuaId");

            migrationBuilder.CreateIndex(
                name: "IX_PhatTu_KieuThanhVienId",
                table: "PhatTu",
                column: "KieuThanhVienId");

            migrationBuilder.CreateIndex(
                name: "IX_PhatTuDaoTrang_DaoTrangId",
                table: "PhatTuDaoTrang",
                column: "DaoTrangId");

            migrationBuilder.CreateIndex(
                name: "IX_PhatTuDaoTrang_PhatTuId",
                table: "PhatTuDaoTrang",
                column: "PhatTuId");

            migrationBuilder.CreateIndex(
                name: "IX_Token_PhatTuId",
                table: "Token",
                column: "PhatTuId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_PhatTuId",
                table: "UserRoles",
                column: "PhatTuId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonDangKy");

            migrationBuilder.DropTable(
                name: "PasswordRefresh");

            migrationBuilder.DropTable(
                name: "PhatTuDaoTrang");

            migrationBuilder.DropTable(
                name: "Token");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "DaoTrang");

            migrationBuilder.DropTable(
                name: "PhatTu");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Chua");

            migrationBuilder.DropTable(
                name: "KieuThanhVien");
        }
    }
}
