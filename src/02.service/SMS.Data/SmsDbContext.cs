using Common.VNextFramework.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SMS.Data.Entitys;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace SMS.Data
{
    public class SmsDbContext : DbContext
    {
        private readonly IServiceProvider _serviceProvider;
        public SmsDbContext(DbContextOptions<SmsDbContext> options, IServiceProvider serviceProvider) : base(options)
        {
            _serviceProvider = serviceProvider;
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //自定义表名前缀
            modelBuilder.HasDefaultSchema("Sms");
            // 根据实体类名来生成数据库名称、默认是根据DbSet<User> Users 生成表明的。
            foreach (var mutableEntityType in modelBuilder.Model.GetEntityTypes())
                if (mutableEntityType.ClrType.IsSubclassOf(typeof(BaseAuditEntity)))
                    mutableEntityType.SetTableName(mutableEntityType.ClrType.Name);

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()))
            {
                // 枚举生成字符串
                if (property.ClrType.IsEnum || Nullable.GetUnderlyingType(property.ClrType)?.IsEnum == true)
                {
                    property.SetColumnType("nvarchar(256)");
                }
            }

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            AddTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddTimestamps()
        {
            var userId = _serviceProvider.GetService<IHttpContextAccessor>()?.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            var trackingEntities = ChangeTracker.Entries().Where(x => x.Entity is BaseAuditEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var trackingEntity in trackingEntities)
            {
                var entity = (BaseAuditEntity)trackingEntity.Entity;

                if (trackingEntity.State == EntityState.Added)
                {
                    entity.CreatedOn = DateTime.Now;
                    entity.CreatedBy = userId;
                    entity.IsDeleted = false;
                }
                else
                {
                    entity.LastModifiedOn = DateTime.Now;
                    entity.LastModifiedBy = userId;
                }
            }
        }
    }
}
