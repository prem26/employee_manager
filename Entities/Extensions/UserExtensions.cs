using System;
using Entities.Models;

namespace Entities.Extensions
{
    public static class UserExtensions
    {
        public static void Map(this User dbUser, User user){
            dbUser.Id = user.Id;
            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            dbUser.Email = user.Email;
            dbUser.Password = user.Password;
            dbUser.PasswordSalt = user.PasswordSalt;
            dbUser.Created = user.Created;
            dbUser.Updated = user.Updated;
            dbUser.SoftDeleted = user.SoftDeleted;
        }
    }
}
