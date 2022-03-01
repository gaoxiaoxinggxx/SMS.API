using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Commen.EntityFramworkCore
{
    public class MultiDbContext : DbContext
    {
        public MultiDbContext() : base()
        { }

        public MultiDbContext(DbContextOptions options) : base(options)
        { }

        public override int SaveChanges()
        {
            var entities = from e in ChangeTracker.Entries()
                           where e.State == EntityState.Added
                               || e.State == EntityState.Modified
                               || e.State == EntityState.Deleted
                           select (e.Entity, e.State);

            foreach (var entity in entities)
            {
                switch (entity.State)
                {
                    case EntityState.Deleted:
                        break;
                    case EntityState.Modified:
                    case EntityState.Added:
                        var validationContext = new ValidationContext(entity.Entity);
                        // 定義進行Validation回傳的訊息
                        var validationResults = new List<ValidationResult>();
                        
                        bool isValid = Validator.TryValidateObject(entity.Entity, validationContext, validationResults, true);
                        if (validationResults.Count > 0)
                        {
                            //throw new Exception("实体校验失败");
                        }
                        break;
                    default:
                        break;
                }
                
            }
            return base.SaveChanges();
        }
    }
}
