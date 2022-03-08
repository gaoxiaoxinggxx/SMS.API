using Common.VNextFramework.EntityFramework;
using Microsoft.EntityFrameworkCore;
using SMS.Data.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SMS.Data
{
    public class SmsDbContext : DbContext
    {
        public SmsDbContext(DbContextOptions<SmsDbContext> options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
            var trackingEntities = ChangeTracker.Entries().Where(x => x.Entity is BaseAuditEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var trackingEntity in trackingEntities)
            {
                var entity = (BaseAuditEntity)trackingEntity.Entity;

                if (trackingEntity.State == EntityState.Added)
                {
                    entity.CreatedOn = DateTime.Now;
                    entity.CreatedBy = "";
                    entity.IsDeleted = false;
                }
                else
                {
                    entity.LastModifiedOn = DateTime.Now;
                    entity.LastModifiedBy = "";
                }
            }
        }
    }
}
