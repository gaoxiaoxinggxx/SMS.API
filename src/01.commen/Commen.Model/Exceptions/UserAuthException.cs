using System;
using System.Collections.Generic;
using System.Text;

namespace Commen.Model.Exceptions
{
    public class UserAuthException : Exception
    {
        public UserAuthException(string message) : base(message)
        {
        }

        public static UserAuthException UserNameOrPasswordNotMatch = new UserAuthException("username or password is not match");
        
    }
}
