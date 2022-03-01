using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commen.EntityFramworkCore
{
    public interface IBaseEntity<T> where T : new()
    {
        [Key]
        T Id { get; set; }
    }
}
