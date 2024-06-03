using DoAn.Domain.Core;
using DoAn.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Domain
{
    public class DoAnContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DoAnContext(DbContextOptions<DoAnContext> options, IHttpContextAccessor httpContextAccessor) : base(options) 
        { 
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>().ToTable("AppUser");
            builder.Entity<AppRole>().ToTable("AppRole");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserToken");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaim");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaim");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogin");

        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is IAuditableEntity entity)
                {
                    var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
                    var _user = this.Users.Where(x => x.UserName == userName).FirstOrDefault();
                    switch (item.State)
                    {
                        case EntityState.Modified:
                            entity.UpdatedDate = DateTime.Now;

                            if (_user != null && _user.Id != Guid.Empty)
                            {
                                entity.CreatedBy = _user.UserName;
                                entity.CreatedID = _user.Id;
                            }
                            break;
                        case EntityState.Added:
                            entity.CreatedDate = DateTime.Now;
                            if (_user != null && _user.Id != Guid.Empty)
                            {
                                entity.CreatedBy = _user.UserName;
                                entity.CreatedID = _user.Id;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        //Table tạo mới
        public DbSet<DanhMuc> DanhMucs { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppUserRole> AppUserRoles { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<NhaXuatBan> NhaXuatBan { get; set; }
        public DbSet<NhomDoTuoi> NhomDoTuois { get; set; }  
        public DbSet<HinhAnh> HinhAnhs { get; set; }
        public DbSet<FileManager> FileManager { get; set; }
        public DbSet<QLSlide> QLSlide { get; set; }
        public DbSet<TTSlide> TTSlide { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail > OrderDetail { get; set; }
        public DbSet<TinTuc> TinTuc { get; set;}
        public DbSet<DanhMucTinTuc> DanhMucTinTuc { get; set; }
    }
}
