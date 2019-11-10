using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities;
using Entities.Extensions;
using Entities.Models;

namespace Repository
{
    public class UserRepository: RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public IEnumerable<User> GetAllUsers()
        {
            return FindAll().OrderBy(u => u.FirstName);
        }

        public User GetUserById(Guid userId)
        {
            return FindByCondition(user => user.Id.Equals(userId))
                    .DefaultIfEmpty(new User())
                    .FirstOrDefault();
        }

        public void CreateUser(User user)
        {
            user.Id = Guid.NewGuid();
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(Convert.ToBase64String(user.Password), out passwordHash, out passwordSalt);
            user.Password = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Created = DateTime.Now;
            user.Updated = DateTime.Now;
            Create(user);
            Save();
        }

        public User Authenticate(string email, string password)
        {
            try
            {
                Console.WriteLine(password);
                Console.WriteLine(email);

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                    return null;

                var user = FindByCondition(u => u.Email.Equals(email)).SingleOrDefault();

                if (user == null)
                    return null;

                if (!VerifyPasswordHash(password, user.Password, user.PasswordSalt))
                    return null;

                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }

        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            //if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            //if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }

        public void UpdateUser(User dbUser, User user)
        {
            dbUser.Map(user);
            Update(dbUser);
        }

        public void DeleteUser(User user)
        {
            Delete(user);
        }
    }
}
