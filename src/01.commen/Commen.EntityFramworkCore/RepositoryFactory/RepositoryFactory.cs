using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commen.EntityFramworkCore
{
   public class RepositoryFactory:IRepositoryFactory
    {
        private IUnitOfWork UnitOfWork { get; set; }

        public RepositoryFactory(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IRepositorys<T> CreateRepository<T>()
            where T : class
        {
            return new Repositorys<T>(UnitOfWork);
        }
    }
}
