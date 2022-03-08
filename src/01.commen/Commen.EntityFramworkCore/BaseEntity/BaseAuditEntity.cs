using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.VNextFramework.EntityFramework
{
   public class BaseAuditEntity
    {
        public DateTimeOffset? CreatedOn
        {
            get;
            set;
        }

        public string CreatedBy
        {
            get;
            set;
        }

        public DateTimeOffset? LastModifiedOn
        {
            get;
            set;
        }

        public string LastModifiedBy
        {
            get;
            set;
        }
        
        public bool IsDeleted
        {
            get;
            set;
        } = false;


        [Timestamp]
        public byte[] Timestamp
        {
            get;
            set;
        }
    }
}
