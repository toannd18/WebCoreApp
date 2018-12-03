using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class MyFirt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Role = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: true),
                    ProviderKey = table.Column<string>(nullable: true),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogins", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRoles", x => new { x.RoleId, x.UserId });
                });

            migrationBuilder.CreateTable(
                name: "AppUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTokens", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Functions",
                columns: table => new
                {
                    Id = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Url = table.Column<string>(unicode: false, maxLength: 250, nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    ParentId = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    IconCss = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Functions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Functions_Functions",
                        column: x => x.ParentId,
                        principalTable: "Functions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Log_in",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Event = table.Column<string>(maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    IdName = table.Column<long>(nullable: false),
                    tbl_Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_in", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BP",
                columns: table => new
                {
                    Ma_BP = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    Ten_BP = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BP", x => x.Ma_BP);
                });

            migrationBuilder.CreateTable(
                name: "tbl_CV",
                columns: table => new
                {
                    Ma_CV = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    Ten_CV = table.Column<string>(maxLength: 50, nullable: true),
                    Display = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_CV", x => x.Ma_CV);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Daily",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Total_Job = table.Column<string>(type: "ntext", nullable: true),
                    Comment1 = table.Column<string>(type: "ntext", nullable: true),
                    User_Autho1 = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    User_Autho2 = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Status_Autho1 = table.Column<bool>(nullable: false),
                    Status_Autho2 = table.Column<bool>(nullable: false),
                    User_Autho3 = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Status_Autho3 = table.Column<bool>(nullable: false),
                    Comment2 = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Daily", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_DeXuat",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ma = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    Tieu_De = table.Column<string>(maxLength: 50, nullable: true),
                    Kieu = table.Column<bool>(nullable: false),
                    Ten_Dx = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Ten_Dx1 = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Ten_Dx2 = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Ten_Dx3 = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Ten_Dx4 = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Ten_Dx5 = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Ten_Dg = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Ten_Dg1 = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Ten_Dg2 = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Ten_Dg3 = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Ten_Dg4 = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Ngay_Tao = table.Column<DateTime>(type: "datetime", nullable: true),
                    Ngay_Exp = table.Column<DateTime>(type: "datetime", nullable: true),
                    Ngay_Eval = table.Column<DateTime>(type: "datetime", nullable: true),
                    Ngay_Gui = table.Column<DateTime>(type: "datetime", nullable: true),
                    Ngay_PD = table.Column<DateTime>(type: "datetime", nullable: true),
                    Ngay_HD = table.Column<DateTime>(type: "datetime", nullable: true),
                    Ngay_PHD = table.Column<DateTime>(type: "datetime", nullable: true),
                    Ngay_Ky = table.Column<DateTime>(type: "datetime", nullable: true),
                    Ngay_TH = table.Column<DateTime>(type: "datetime", nullable: true),
                    Ngay_THTT = table.Column<DateTime>(type: "datetime", nullable: true),
                    Ngay_NT = table.Column<DateTime>(type: "datetime", nullable: true),
                    Ngay_NT_QC = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    Ghi_Chu = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_DeXuat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_List_Check",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RequestId = table.Column<int>(nullable: false),
                    Ma_NCC = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    Ma_TB = table.Column<string>(maxLength: 50, nullable: false),
                    YC_KT = table.Column<string>(nullable: true),
                    TT_KT = table.Column<string>(nullable: true),
                    YC_SL = table.Column<int>(nullable: false),
                    TT_SL = table.Column<int>(nullable: false),
                    DonVi = table.Column<string>(maxLength: 20, nullable: false),
                    CO = table.Column<bool>(nullable: true),
                    CQ = table.Column<bool>(nullable: true),
                    MTR = table.Column<bool>(nullable: true),
                    SN = table.Column<bool>(nullable: true),
                    PN = table.Column<bool>(nullable: true),
                    Other = table.Column<bool>(nullable: true),
                    Note_Other = table.Column<string>(maxLength: 200, nullable: true),
                    Result = table.Column<bool>(nullable: true),
                    Reason = table.Column<string>(nullable: true),
                    User_Nhap = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Date_Nhap = table.Column<DateTime>(type: "date", nullable: true),
                    User_Edit = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Date_Edit = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_List_Check", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_List_Request",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstId = table.Column<int>(nullable: false),
                    LateId = table.Column<string>(unicode: false, maxLength: 70, nullable: true),
                    Ma_BP = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    Dia_Diem = table.Column<string>(maxLength: 70, nullable: true),
                    Hang_Muc = table.Column<string>(maxLength: 50, nullable: true),
                    HopDong = table.Column<string>(maxLength: 50, nullable: true),
                    DeXuat = table.Column<string>(maxLength: 50, nullable: false),
                    CO = table.Column<bool>(nullable: false),
                    CQ = table.Column<bool>(nullable: false),
                    Other = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(maxLength: 50, nullable: true),
                    User_Nhap = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Date_Nhap = table.Column<DateTime>(type: "date", nullable: false),
                    User_Edit = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Date_Edit = table.Column<DateTime>(type: "date", nullable: true),
                    User_Autho = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Date_Autho = table.Column<DateTime>(type: "date", nullable: true),
                    Status_Autho = table.Column<string>(unicode: false, maxLength: 1, nullable: true),
                    Note_Autho = table.Column<string>(maxLength: 50, nullable: true),
                    Date = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_List_Request", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_NCC",
                columns: table => new
                {
                    Ma_NCC = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    Ten_NCC = table.Column<string>(maxLength: 100, nullable: false),
                    Dia_Chi = table.Column<string>(maxLength: 100, nullable: true),
                    Tel = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Fax = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Attn = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Hang_Hoa = table.Column<string>(nullable: true),
                    Dich_Vu = table.Column<string>(nullable: true),
                    Diem = table.Column<string>(unicode: false, maxLength: 1, nullable: true),
                    Time = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_NCC", x => x.Ma_NCC);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Notifications",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SendId = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    ReceiveId = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Url = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Notification = table.Column<string>(maxLength: 100, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Provider",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: false),
                    Tel = table.Column<string>(unicode: false, maxLength: 15, nullable: true),
                    Fax = table.Column<string>(unicode: false, maxLength: 15, nullable: true),
                    Attn = table.Column<string>(maxLength: 20, nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Provider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<Guid>(nullable: false),
                    FunctionId = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    CanRead = table.Column<bool>(nullable: false),
                    CanUpdate = table.Column<bool>(nullable: false),
                    CanCreate = table.Column<bool>(nullable: false),
                    CanDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_Functions",
                        column: x => x.FunctionId,
                        principalTable: "Functions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Permissions_AppRole",
                        column: x => x.RoleId,
                        principalTable: "AppRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Job",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ten_Job = table.Column<string>(maxLength: 250, nullable: false),
                    Ma_BP = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    Ma_TO = table.Column<string>(unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Job", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Job_tbl_BP",
                        column: x => x.Ma_BP,
                        principalTable: "tbl_BP",
                        principalColumn: "Ma_BP",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_TO",
                columns: table => new
                {
                    Ma_TO = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    Ten_TO = table.Column<string>(maxLength: 50, nullable: true),
                    Ma_BP = table.Column<string>(unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_TO", x => x.Ma_TO);
                    table.ForeignKey(
                        name: "FK_tbl_TO_tbl_BP",
                        column: x => x.Ma_BP,
                        principalTable: "tbl_BP",
                        principalColumn: "Ma_BP",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_DailyDetail",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FormTime = table.Column<TimeSpan>(nullable: false),
                    ToTime = table.Column<TimeSpan>(nullable: false),
                    Comment1 = table.Column<string>(type: "ntext", nullable: true),
                    Content_Job = table.Column<string>(type: "ntext", nullable: false),
                    Method = table.Column<string>(type: "ntext", nullable: true),
                    Result = table.Column<string>(maxLength: 250, nullable: true),
                    DailyId = table.Column<int>(nullable: false),
                    JobId = table.Column<int>(nullable: false),
                    Level_1 = table.Column<int>(nullable: true),
                    Level_2 = table.Column<int>(nullable: true),
                    Comment2 = table.Column<string>(type: "ntext", nullable: true),
                    Level_3 = table.Column<int>(nullable: true),
                    Comment3 = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_DailyDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_DailyDetail_tbl_Daily",
                        column: x => x.DailyId,
                        principalTable: "tbl_Daily",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_DeXuat_HD",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeXuatId = table.Column<int>(nullable: false),
                    So_HD = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Ma_NCC = table.Column<string>(unicode: false, nullable: false),
                    Chat_luong = table.Column<int>(nullable: false),
                    Tien_Do = table.Column<int>(nullable: false),
                    Gia_Ca = table.Column<int>(nullable: false),
                    Thai_Do = table.Column<int>(nullable: false),
                    Diem = table.Column<string>(unicode: false, nullable: true),
                    Author = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_DeXuat_HD", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_DeXuat_HD_tbl_DeXuat",
                        column: x => x.DeXuatId,
                        principalTable: "tbl_DeXuat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_DeXuat_KT",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeXuatId = table.Column<int>(nullable: false),
                    Ten = table.Column<string>(nullable: true),
                    Mo_Ta = table.Column<string>(nullable: true),
                    SoLuong = table.Column<int>(nullable: false),
                    DVT = table.Column<string>(nullable: true),
                    Ghi_Chu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_DeXuat_KT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_DeXuat_KT_tbl_DeXuat",
                        column: x => x.DeXuatId,
                        principalTable: "tbl_DeXuat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_DeXuat_TM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeXuatId = table.Column<int>(nullable: false),
                    Loai_Tien = table.Column<string>(unicode: false, nullable: true),
                    Hieu_Luc = table.Column<string>(nullable: true),
                    Thoi_Gian = table.Column<string>(nullable: true),
                    Dia_Diem = table.Column<string>(nullable: true),
                    Dieu_Kien = table.Column<string>(nullable: true),
                    BH = table.Column<string>(nullable: true),
                    Che_Do = table.Column<string>(nullable: true),
                    Ghi_Chu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_DeXuat_TM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_DeXuat_TM_tbl_DeXuat",
                        column: x => x.DeXuatId,
                        principalTable: "tbl_DeXuat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_DG_KT",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DG_NCC_Id = table.Column<int>(nullable: false),
                    DG_KT_Id = table.Column<int>(nullable: false),
                    DeXuatId = table.Column<int>(nullable: false),
                    Ten = table.Column<string>(nullable: false),
                    Mo_Ta = table.Column<string>(nullable: false),
                    Ghi_Chu = table.Column<string>(nullable: true),
                    DG = table.Column<bool>(nullable: false),
                    Don_Gia = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_DG_KT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_DG_KT_tbl_DeXuat",
                        column: x => x.DeXuatId,
                        principalTable: "tbl_DeXuat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_DG_TM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DG_NCC_Id = table.Column<int>(nullable: false),
                    DeXuatId = table.Column<int>(nullable: false),
                    Hieu_Luc = table.Column<string>(nullable: true),
                    Thoi_Gian = table.Column<string>(nullable: true),
                    Dia_Diem = table.Column<string>(nullable: true),
                    Dieu_Kien = table.Column<string>(nullable: true),
                    BH = table.Column<string>(nullable: true),
                    Che_Do = table.Column<string>(nullable: true),
                    Van_Chuyen = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    Ghi_Chu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_DG_TM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_DG_TM_tbl_DeXuat",
                        column: x => x.DeXuatId,
                        principalTable: "tbl_DeXuat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_DG_NCC",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeXuatId = table.Column<int>(nullable: false),
                    Ma_NCC = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    DG_KT = table.Column<bool>(nullable: true),
                    DG_TM = table.Column<int>(nullable: true),
                    DG = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_DG_NCC", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_DG_NCC_tbl_DeXuat",
                        column: x => x.DeXuatId,
                        principalTable: "tbl_DeXuat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_DG_NCC_tbl_NCC",
                        column: x => x.Ma_NCC,
                        principalTable: "tbl_NCC",
                        principalColumn: "Ma_NCC",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    Avatar = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    BrithDay = table.Column<DateTime>(type: "date", nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    Gender = table.Column<bool>(nullable: false),
                    Ma_BP = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    Ma_TO = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    Ma_CV = table.Column<string>(unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUser_tbl_BP",
                        column: x => x.Ma_BP,
                        principalTable: "tbl_BP",
                        principalColumn: "Ma_BP",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppUser_tbl_CV",
                        column: x => x.Ma_CV,
                        principalTable: "tbl_CV",
                        principalColumn: "Ma_CV",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppUser_tbl_TO",
                        column: x => x.Ma_TO,
                        principalTable: "tbl_TO",
                        principalColumn: "Ma_TO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_Ma_BP",
                table: "AppUser",
                column: "Ma_BP");

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_Ma_CV",
                table: "AppUser",
                column: "Ma_CV");

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_Ma_TO",
                table: "AppUser",
                column: "Ma_TO");

            migrationBuilder.CreateIndex(
                name: "IX_Functions_ParentId",
                table: "Functions",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_FunctionId",
                table: "Permissions",
                column: "FunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_RoleId",
                table: "Permissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_DailyDetail_DailyId",
                table: "tbl_DailyDetail",
                column: "DailyId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_DeXuat_HD_DeXuatId",
                table: "tbl_DeXuat_HD",
                column: "DeXuatId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_DeXuat_KT_DeXuatId",
                table: "tbl_DeXuat_KT",
                column: "DeXuatId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_DeXuat_TM_DeXuatId",
                table: "tbl_DeXuat_TM",
                column: "DeXuatId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_DG_KT_DeXuatId",
                table: "tbl_DG_KT",
                column: "DeXuatId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_DG_NCC_DeXuatId",
                table: "tbl_DG_NCC",
                column: "DeXuatId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_DG_NCC_Ma_NCC",
                table: "tbl_DG_NCC",
                column: "Ma_NCC");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_DG_TM_DeXuatId",
                table: "tbl_DG_TM",
                column: "DeXuatId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Job_Ma_BP",
                table: "tbl_Job",
                column: "Ma_BP");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_TO_Ma_BP",
                table: "tbl_TO",
                column: "Ma_BP");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppRoleClaims");

            migrationBuilder.DropTable(
                name: "AppUser");

            migrationBuilder.DropTable(
                name: "AppUserClaims");

            migrationBuilder.DropTable(
                name: "AppUserLogins");

            migrationBuilder.DropTable(
                name: "AppUserRoles");

            migrationBuilder.DropTable(
                name: "AppUserTokens");

            migrationBuilder.DropTable(
                name: "Log_in");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "tbl_DailyDetail");

            migrationBuilder.DropTable(
                name: "tbl_DeXuat_HD");

            migrationBuilder.DropTable(
                name: "tbl_DeXuat_KT");

            migrationBuilder.DropTable(
                name: "tbl_DeXuat_TM");

            migrationBuilder.DropTable(
                name: "tbl_DG_KT");

            migrationBuilder.DropTable(
                name: "tbl_DG_NCC");

            migrationBuilder.DropTable(
                name: "tbl_DG_TM");

            migrationBuilder.DropTable(
                name: "tbl_Job");

            migrationBuilder.DropTable(
                name: "tbl_List_Check");

            migrationBuilder.DropTable(
                name: "tbl_List_Request");

            migrationBuilder.DropTable(
                name: "tbl_Notifications");

            migrationBuilder.DropTable(
                name: "tbl_Provider");

            migrationBuilder.DropTable(
                name: "tbl_CV");

            migrationBuilder.DropTable(
                name: "tbl_TO");

            migrationBuilder.DropTable(
                name: "Functions");

            migrationBuilder.DropTable(
                name: "AppRole");

            migrationBuilder.DropTable(
                name: "tbl_Daily");

            migrationBuilder.DropTable(
                name: "tbl_NCC");

            migrationBuilder.DropTable(
                name: "tbl_DeXuat");

            migrationBuilder.DropTable(
                name: "tbl_BP");
        }
    }
}
