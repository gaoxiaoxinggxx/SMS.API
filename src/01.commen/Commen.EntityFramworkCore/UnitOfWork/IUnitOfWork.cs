using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.VNextFramework.EntityFramework
{
   public interface IUnitOfWork
    {
        MultiDbContext Context { get; set; }

        void Commit();

        Task<int> SaveChangesAsync();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
