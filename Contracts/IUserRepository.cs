using System;
using System.Collections.Generic;
using Entities.Models;

namespace Contracts
{
    public interface IUserRepository: IRepositoryBase<User> 
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(Guid userId);
        void CreateUser(User user);
        User Authenticate(string email, string password);
        void UpdateUser(User dbUser, User user);
        void DeleteUser(User user);
    }
}
