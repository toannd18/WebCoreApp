using DataContext.WebCoreApp.Pipe;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataContext.WebCoreApp
{
    public partial class EFContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public EFContext()
        {
        }

        public EFContext(DbContextOptions<EFContext> options)
            : base(options)
        {
            Database.SetCommandTimeout(900);
        }

        public virtual DbSet<AppRole> AppRole { get; set; }
        public virtual DbSet<AppUser> AppUser { get; set; }

        public virtual DbSet<Functions> Functions { get; set; }
        public virtual DbSet<LogIn> LogIn { get; set; }

        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<TblBp> TblBp { get; set; }
        public virtual DbSet<TblCv> TblCv { get; set; }
        public virtual DbSet<TblDaily> TblDaily { get; set; }
        public virtual DbSet<TblDailyDetail> TblDailyDetail { get; set; }
        public virtual DbSet<TblDeXuat> TblDeXuat { get; set; }
        public virtual DbSet<TblDeXuatHd> TblDeXuatHd { get; set; }
        public virtual DbSet<TblDeXuatKt> TblDeXuatKt { get; set; }
        public virtual DbSet<TblDeXuatTm> TblDeXuatTm { get; set; }
        public virtual DbSet<TblDgKt> TblDgKt { get; set; }
        public virtual DbSet<TblDgNcc> TblDgNcc { get; set; }
        public virtual DbSet<TblDgTm> TblDgTm { get; set; }
        public virtual DbSet<TblJob> TblJob { get; set; }
        public virtual DbSet<TblListCheck> TblListCheck { get; set; }
        public virtual DbSet<TblListRequest> TblListRequest { get; set; }
        public virtual DbSet<TblNcc> TblNcc { get; set; }
        public virtual DbSet<TblNotifications> TblNotifications { get; set; }
        public virtual DbSet<TblProvider> TblProvider { get; set; }
        public virtual DbSet<TblTo> TblTo { get; set; }

        //PipeClass

        #region PipeClass

        public virtual DbSet<IsoJoint> IsoJoints { get; set; }
        public virtual DbSet<Isometric> Isometrics { get; set; }

        public virtual DbSet<MaterialPipe> MaterialPipes { get; set; }
        public virtual DbSet<TypeJoint> TypeJoints { get; set; }

        public virtual DbSet<Welder> Welders { get; set; }
        public virtual DbSet<WelderCertification> WelderCertifications { get; set; }

        #endregion PipeClass

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        string path = Directory.GetCurrentDirectory();
        //        IConfiguration configuration = new ConfigurationBuilder()
        //            .SetBasePath(Directory.GetDirectoryRoot(path))
        //            .AddJsonFile("appsettings.json").Build();

        //        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Identity config

            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims").HasKey(x => x.Id);
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims")
               .HasKey(x => x.Id);

            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens")
               .HasKey(x => new { x.UserId });

            #endregion Identity config

            modelBuilder.Entity<AppRole>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(50);
            });

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.Avatar)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BrithDay).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MaBp)
                    .HasColumnName("Ma_BP")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MaCv)
                    .HasColumnName("Ma_CV")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MaTo)
                    .HasColumnName("Ma_TO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash)

                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.HasOne(e => e.TblBp)
                .WithMany(e => e.AppUser)
                .HasForeignKey(e => e.MaBp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AppUser_TblBp");
                entity.HasOne(e => e.TblCv)
                .WithMany(e => e.AppUser)
                .HasForeignKey(e => e.MaCv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AppUser_TblCv");
                entity.HasOne(e => e.TblTo)
                .WithMany(e => e.AppUser)
                .HasForeignKey(e => e.MaTo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AppUser_TblTo");
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.UserId });
                entity.ToTable("AppUserRoles");
            });

            modelBuilder.Entity<Functions>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.IconCss)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ParentId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Functions_Functions")
                    ;
            });

            modelBuilder.Entity<LogIn>(entity =>
            {
                entity.ToTable("Log_in");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Event)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TblName)
                    .IsRequired()
                    .HasColumnName("tbl_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Permissions>(entity =>
   {
       entity.Property(e => e.FunctionId)
           .IsRequired()
           .HasMaxLength(50)
           .IsUnicode(false);

       entity.HasOne(d => d.Function)
           .WithMany(p => p.Permissions)
           .HasForeignKey(d => d.FunctionId)
           .OnDelete(DeleteBehavior.ClientSetNull)
           .HasConstraintName("FK_Permissions_Functions");
   });

            modelBuilder.Entity<TblBp>(entity =>
            {
                entity.HasKey(e => e.MaBp);

                entity.ToTable("tbl_BP");

                entity.Property(e => e.MaBp)
                    .HasColumnName("Ma_BP")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.TenBp)
                    .IsRequired()
                    .HasColumnName("Ten_BP")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblCv>(entity =>
            {
                entity.HasKey(e => e.MaCv);

                entity.ToTable("tbl_CV");

                entity.Property(e => e.MaCv)
                    .HasColumnName("Ma_CV")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.TenCv)
                    .HasColumnName("Ten_CV")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblDaily>(entity =>
            {
                entity.ToTable("tbl_Daily");

                entity.Property(e => e.Comment1).HasColumnType("ntext");

                entity.Property(e => e.Comment2).HasColumnType("ntext");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.StatusAutho1).HasColumnName("Status_Autho1");

                entity.Property(e => e.StatusAutho2).HasColumnName("Status_Autho2");

                entity.Property(e => e.StatusAutho3).HasColumnName("Status_Autho3");

                entity.Property(e => e.TotalJob)
                    .HasColumnName("Total_Job")
                    .HasColumnType("ntext");

                entity.Property(e => e.UserAutho1)
                    .HasColumnName("User_Autho1")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserAutho2)
                    .HasColumnName("User_Autho2")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserAutho3)
                    .HasColumnName("User_Autho3")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblDailyDetail>(entity =>
            {
                entity.ToTable("tbl_DailyDetail");

                entity.Property(e => e.Comment1).HasColumnType("ntext");

                entity.Property(e => e.Comment2).HasColumnType("ntext");

                entity.Property(e => e.Comment3).HasColumnType("ntext");

                entity.Property(e => e.ContentJob)
                    .IsRequired()
                    .HasColumnName("Content_Job")
                    .HasColumnType("ntext");

                entity.Property(e => e.Level1).HasColumnName("Level_1");

                entity.Property(e => e.Level2).HasColumnName("Level_2");

                entity.Property(e => e.Level3).HasColumnName("Level_3");

                entity.Property(e => e.Method).HasColumnType("ntext");

                entity.Property(e => e.Result).HasMaxLength(250);

                entity.HasOne(d => d.Daily)
                    .WithMany(p => p.TblDailyDetail)
                    .HasForeignKey(d => d.DailyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_DailyDetail_tbl_Daily");
            });

            modelBuilder.Entity<TblDeXuat>(entity =>
            {
                entity.ToTable("tbl_DeXuat");

                entity.Property(e => e.GhiChu)
                    .HasColumnName("Ghi_Chu")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ma)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.NgayEval)
                    .HasColumnName("Ngay_Eval")
                    .HasColumnType("datetime");

                entity.Property(e => e.NgayExp)
                    .HasColumnName("Ngay_Exp")
                    .HasColumnType("datetime");

                entity.Property(e => e.NgayGui)
                    .HasColumnName("Ngay_Gui")
                    .HasColumnType("datetime");

                entity.Property(e => e.NgayHd)
                    .HasColumnName("Ngay_HD")
                    .HasColumnType("datetime");

                entity.Property(e => e.NgayKy)
                    .HasColumnName("Ngay_Ky")
                    .HasColumnType("datetime");

                entity.Property(e => e.NgayNt)
                    .HasColumnName("Ngay_NT")
                    .HasColumnType("datetime");

                entity.Property(e => e.NgayNtQc)
                    .HasColumnName("Ngay_NT_QC")
                    .HasColumnType("datetime");

                entity.Property(e => e.NgayPd)
                    .HasColumnName("Ngay_PD")
                    .HasColumnType("datetime");

                entity.Property(e => e.NgayPhd)
                    .HasColumnName("Ngay_PHD")
                    .HasColumnType("datetime");

                entity.Property(e => e.NgayTao)
                    .HasColumnName("Ngay_Tao")
                    .HasColumnType("datetime");

                entity.Property(e => e.NgayTh)
                    .HasColumnName("Ngay_TH")
                    .HasColumnType("datetime");

                entity.Property(e => e.NgayThtt)
                    .HasColumnName("Ngay_THTT")
                    .HasColumnType("datetime");

                entity.Property(e => e.TenDg)
                    .HasColumnName("Ten_Dg")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenDg1)
                    .HasColumnName("Ten_Dg1")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenDg2)
                    .HasColumnName("Ten_Dg2")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenDg3)
                    .HasColumnName("Ten_Dg3")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenDg4)
                    .HasColumnName("Ten_Dg4")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenDx)
                    .HasColumnName("Ten_Dx")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenDx1)
                    .HasColumnName("Ten_Dx1")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenDx2)
                    .HasColumnName("Ten_Dx2")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenDx3)
                    .HasColumnName("Ten_Dx3")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenDx4)
                    .HasColumnName("Ten_Dx4")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenDx5)
                    .HasColumnName("Ten_Dx5")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TieuDe)
                    .HasColumnName("Tieu_De")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblDeXuatHd>(entity =>
            {
                entity.ToTable("tbl_DeXuat_HD");

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChatLuong).HasColumnName("Chat_luong");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Diem).IsUnicode(false);

                entity.Property(e => e.GiaCa).HasColumnName("Gia_Ca");

                entity.Property(e => e.MaNcc)
                    .IsRequired()
                    .HasColumnName("Ma_NCC")
                    .IsUnicode(false);

                entity.Property(e => e.SoHd).HasColumnName("So_HD");

                entity.Property(e => e.ThaiDo).HasColumnName("Thai_Do");

                entity.Property(e => e.TienDo).HasColumnName("Tien_Do");

                entity.HasOne(d => d.DeXuat)
                    .WithMany(p => p.TblDeXuatHd)
                    .HasForeignKey(d => d.DeXuatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_DeXuat_HD_tbl_DeXuat");
            });

            modelBuilder.Entity<TblDeXuatKt>(entity =>
            {
                entity.ToTable("tbl_DeXuat_KT");

                entity.Property(e => e.Dvt).HasColumnName("DVT");

                entity.Property(e => e.GhiChu).HasColumnName("Ghi_Chu");

                entity.Property(e => e.MoTa).HasColumnName("Mo_Ta");

                entity.HasOne(d => d.DeXuat)
                    .WithMany(p => p.TblDeXuatKt)
                    .HasForeignKey(d => d.DeXuatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_DeXuat_KT_tbl_DeXuat");
            });

            modelBuilder.Entity<TblDeXuatTm>(entity =>
            {
                entity.ToTable("tbl_DeXuat_TM");

                entity.Property(e => e.Bh).HasColumnName("BH");

                entity.Property(e => e.CheDo).HasColumnName("Che_Do");

                entity.Property(e => e.DiaDiem).HasColumnName("Dia_Diem");

                entity.Property(e => e.DieuKien).HasColumnName("Dieu_Kien");

                entity.Property(e => e.GhiChu).HasColumnName("Ghi_Chu");

                entity.Property(e => e.HieuLuc).HasColumnName("Hieu_Luc");

                entity.Property(e => e.LoaiTien)
                    .HasColumnName("Loai_Tien")
                    .IsUnicode(false);

                entity.Property(e => e.ThoiGian).HasColumnName("Thoi_Gian");

                entity.HasOne(d => d.DeXuat)
                    .WithMany(p => p.TblDeXuatTm)
                    .HasForeignKey(d => d.DeXuatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_DeXuat_TM_tbl_DeXuat");
            });

            modelBuilder.Entity<TblDgKt>(entity =>
            {
                entity.ToTable("tbl_DG_KT");

                entity.Property(e => e.Dg).HasColumnName("DG");

                entity.Property(e => e.DgKtId).HasColumnName("DG_KT_Id");

                entity.Property(e => e.DgNccId).HasColumnName("DG_NCC_Id");

                entity.Property(e => e.DonGia).HasColumnName("Don_Gia");

                entity.Property(e => e.GhiChu).HasColumnName("Ghi_Chu");

                entity.Property(e => e.MoTa)
                    .IsRequired()
                    .HasColumnName("Mo_Ta");

                entity.Property(e => e.Ten).IsRequired();

                entity.HasOne(d => d.DeXuat)
                    .WithMany(p => p.TblDgKt)
                    .HasForeignKey(d => d.DeXuatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_DG_KT_tbl_DeXuat");
            });

            modelBuilder.Entity<TblDgNcc>(entity =>
            {
                entity.ToTable("tbl_DG_NCC");

                entity.Property(e => e.Dg).HasColumnName("DG");

                entity.Property(e => e.DgKt).HasColumnName("DG_KT");

                entity.Property(e => e.DgTm).HasColumnName("DG_TM");

                entity.Property(e => e.MaNcc)
                    .HasColumnName("Ma_NCC")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.DeXuat)
                    .WithMany(p => p.TblDgNcc)
                    .HasForeignKey(d => d.DeXuatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_DG_NCC_tbl_DeXuat");

                entity.HasOne(d => d.MaNccNavigation)
                    .WithMany(p => p.TblDgNcc)
                    .HasForeignKey(d => d.MaNcc)
                    .HasConstraintName("FK_tbl_DG_NCC_tbl_NCC");
            });

            modelBuilder.Entity<TblDgTm>(entity =>
            {
                entity.ToTable("tbl_DG_TM");

                entity.Property(e => e.Bh).HasColumnName("BH");

                entity.Property(e => e.CheDo).HasColumnName("Che_Do");

                entity.Property(e => e.DgNccId).HasColumnName("DG_NCC_Id");

                entity.Property(e => e.DiaDiem).HasColumnName("Dia_Diem");

                entity.Property(e => e.DieuKien).HasColumnName("Dieu_Kien");

                entity.Property(e => e.GhiChu).HasColumnName("Ghi_Chu");

                entity.Property(e => e.HieuLuc).HasColumnName("Hieu_Luc");

                entity.Property(e => e.ThoiGian).HasColumnName("Thoi_Gian");

                entity.Property(e => e.VanChuyen)
                    .IsRequired()
                    .HasColumnName("Van_Chuyen")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.DeXuat)
                    .WithMany(p => p.TblDgTm)
                    .HasForeignKey(d => d.DeXuatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_DG_TM_tbl_DeXuat");
            });

            modelBuilder.Entity<TblJob>(entity =>
            {
                entity.ToTable("tbl_Job");

                entity.Property(e => e.MaBp)
                    .IsRequired()
                    .HasColumnName("Ma_BP")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MaTo)
                    .HasColumnName("Ma_TO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TenJob)
                    .IsRequired()
                    .HasColumnName("Ten_Job")
                    .HasMaxLength(250);

                entity.HasOne(d => d.MaBpNavigation)
                    .WithMany(p => p.TblJob)
                    .HasForeignKey(d => d.MaBp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Job_tbl_BP");
            });

            modelBuilder.Entity<TblListCheck>(entity =>
            {
                entity.ToTable("tbl_List_Check");

                entity.Property(e => e.Co).HasColumnName("CO");

                entity.Property(e => e.Cq).HasColumnName("CQ");

                entity.Property(e => e.DateEdit)
                    .HasColumnName("Date_Edit")
                    .HasColumnType("date");

                entity.Property(e => e.DateNhap)
                    .HasColumnName("Date_Nhap")
                    .HasColumnType("date");

                entity.Property(e => e.DonVi)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.MaNcc)
                    .IsRequired()
                    .HasColumnName("Ma_NCC")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MaTb)
                    .IsRequired()
                    .HasColumnName("Ma_TB")
                    .HasMaxLength(50);

                entity.Property(e => e.Mtr).HasColumnName("MTR");

                entity.Property(e => e.NoteOther)
                    .HasColumnName("Note_Other")
                    .HasMaxLength(200);

                entity.Property(e => e.Pn).HasColumnName("PN");

                entity.Property(e => e.Sn).HasColumnName("SN");

                entity.Property(e => e.TtKt).HasColumnName("TT_KT");

                entity.Property(e => e.TtSl).HasColumnName("TT_SL");

                entity.Property(e => e.UserEdit)
                    .HasColumnName("User_Edit")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserNhap)
                    .HasColumnName("User_Nhap")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.YcKt).HasColumnName("YC_KT");

                entity.Property(e => e.YcSl).HasColumnName("YC_SL");
            });

            modelBuilder.Entity<TblListRequest>(entity =>
            {
                entity.ToTable("tbl_List_Request");

                entity.Property(e => e.Co).HasColumnName("CO");

                entity.Property(e => e.Cq).HasColumnName("CQ");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.DateAutho)
                    .HasColumnName("Date_Autho")
                    .HasColumnType("date");

                entity.Property(e => e.DateEdit)
                    .HasColumnName("Date_Edit")
                    .HasColumnType("date");

                entity.Property(e => e.DateNhap)
                    .HasColumnName("Date_Nhap")
                    .HasColumnType("date");

                entity.Property(e => e.DeXuat)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DiaDiem)
                    .HasColumnName("Dia_Diem")
                    .HasMaxLength(70);

                entity.Property(e => e.HangMuc)
                    .HasColumnName("Hang_Muc")
                    .HasMaxLength(50);

                entity.Property(e => e.HopDong).HasMaxLength(50);

                entity.Property(e => e.LateId)
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.MaBp)
                    .IsRequired()
                    .HasColumnName("Ma_BP")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Note).HasMaxLength(50);

                entity.Property(e => e.NoteAutho)
                    .HasColumnName("Note_Autho")
                    .HasMaxLength(50);

                entity.Property(e => e.StatusAutho)
                    .HasColumnName("Status_Autho")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.UserAutho)
                    .HasColumnName("User_Autho")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserEdit)
                    .HasColumnName("User_Edit")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserNhap)
                    .IsRequired()
                    .HasColumnName("User_Nhap")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblNcc>(entity =>
            {
                entity.HasKey(e => e.MaNcc);

                entity.ToTable("tbl_NCC");

                entity.Property(e => e.MaNcc)
                    .HasColumnName("Ma_NCC")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Attn).HasMaxLength(50);

                entity.Property(e => e.DiaChi)
                    .HasColumnName("Dia_Chi")
                    .HasMaxLength(100);

                entity.Property(e => e.DichVu).HasColumnName("Dich_Vu");

                entity.Property(e => e.Diem)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.HangHoa).HasColumnName("Hang_Hoa");

                entity.Property(e => e.Tel)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TenNcc)
                    .IsRequired()
                    .HasColumnName("Ten_NCC")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TblNotifications>(entity =>
            {
                entity.ToTable("tbl_Notifications");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Notification).HasMaxLength(100);

                entity.Property(e => e.ReceiveId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SendId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblProvider>(entity =>
            {
                entity.ToTable("tbl_Provider");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Attn).HasMaxLength(20);

                entity.Property(e => e.Email)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Tel)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblTo>(entity =>
            {
                entity.HasKey(e => e.MaTo);

                entity.ToTable("tbl_TO");

                entity.Property(e => e.MaTo)
                    .HasColumnName("Ma_TO")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.MaBp)
                    .IsRequired()
                    .HasColumnName("Ma_BP")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TenTo)
                    .HasColumnName("Ten_TO")
                    .HasMaxLength(50);

                entity.HasOne(d => d.MaBpNavigation)
                    .WithMany(p => p.TblTo)
                    .HasForeignKey(d => d.MaBp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_TO_tbl_BP");
            });

            modelBuilder.Entity<IsoJoint>(entity =>
            {
                entity.Property(e => e.Status)
                        .HasColumnType("tinyint");

                entity.Property(e => e.SF)
                        .HasColumnType("tinyint");

                entity.Property(e => e.WeldingDate)
                       .HasColumnType("Date");

                entity.HasOne(d => d.Isometric)
                       .WithMany(e => e.IsoJoints)
                        .HasForeignKey(e => e.DrawName);
                entity.Property(e => e.Size).HasColumnType("decimal(5,2)");
            });

            modelBuilder.Entity<Isometric>(entity =>
            {
                entity.Property(e => e.DrawName).IsUnicode(false);
                entity.Property(e => e.Line).IsUnicode(false);
                entity.Property(e => e.PipeClass).IsUnicode(false);
                entity.Property(e => e.Unit).IsUnicode(false);
                entity.Property(e => e.Size).HasColumnType("decimal(5,2)");
            });

            modelBuilder.Entity<MaterialPipe>(entity =>
            {
                entity.Property(e => e.Name)
                .IsUnicode(false);
            });

            modelBuilder.Entity<TypeJoint>(entity =>
            {
                entity.Property(e => e.Type)
                .IsUnicode(false);
            });

            modelBuilder.Entity<Welder>(entity =>
            {
                entity.Property(e => e.Id)
                .IsUnicode(false);
                entity.Property(e => e.BrithDay)
               .HasColumnType("Date");
            });

            modelBuilder.Entity<WelderCertification>(entity =>
            {
                entity.Property(e => e.Certification)
                      .IsUnicode(false);
                entity.Property(e => e.Description)
                    .IsUnicode(true);
                entity.Property(e => e.CerDate)
                    .HasColumnType("Date");
                entity.Property(e => e.Remark)
                  .IsUnicode(true);
                entity.HasOne(d => d.Welder)
                .WithMany(e => e.WelderCertifications)
                .HasForeignKey(e => e.IdWelder)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}