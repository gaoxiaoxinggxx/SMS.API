using Common.VNextFramework.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Data.Entitys
{
    public class User : SequentialGuidEntity
    {
        public enum UserRoleEnum
        {
            Admin = 1,
            Developer = 2,
            Staff = 3
        }

        public enum UserStatusEnum
        {
            Active = 1,
            InActive = 2
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRoleEnum Role { get; set; }
        public UserStatusEnum Status { get; set; }
    }
}
