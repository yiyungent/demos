using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetcore2._1UseMySqlEFCore
{
    public class UserService
    {
        public bool Add(UserContext userContext, User user)
        {
            userContext.User.Add(user);
            return userContext.SaveChanges() > 0 ? true : false;
        }
    }
}
