using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commen.EntityFramworkCore
{
    /// <summary>
    /// 创建上下文工厂
    /// </summary>
   public interface IRepositoryFactory
    {
        IRepositorys<T> CreateRepository<T>() where T : class;
    }
}
