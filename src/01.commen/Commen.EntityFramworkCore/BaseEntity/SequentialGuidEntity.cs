using Commen.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.VNextFramework.EntityFramework
{
    public abstract class SequentialGuidEntity : BaseAuditEntity, IBaseEntity<Guid>
    {
        public Guid Id { get; set; } = GuidTool.GenerateSequentialGuid();
    }
}
