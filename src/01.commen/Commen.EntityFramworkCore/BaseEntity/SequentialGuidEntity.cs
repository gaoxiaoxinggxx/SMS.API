using Commen.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commen.EntityFramworkCore
{
    public abstract class SequentialGuidEntity : BaseAuditEntity, IBaseEntity<Guid>
    {
        public Guid Id { get; set; } = GuidTool.GenerateSequentialGuid();
    }
}
