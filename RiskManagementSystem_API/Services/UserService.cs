using System;
using System.Collections.Generic;
using System.Linq;
using RiskManagementSystem_API.Entities;
using RiskManagementSystem_API.Helpers;

namespace RiskManagementSystem_API.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(Guid id);
        User GetByEmail(string email);
        bool CheckRole(Guid id, Role role);
        User Create(User user, string password);
        void Update(User user, string password = null);
        void Delete(Guid id);
    }

    public enum Role
    {
        Admin,
        RiskManager
    }

    public class UserService : IUserService
    {
        private DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Authenticats user by email and password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.SingleOrDefault(x => x.Email.Equals(email));

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetAll()
        {
            var users = _context.Users;
            return users;
        }

        /// <summary>
        /// Gets user by userid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetById(Guid id)
        {
            return _context.Users.Find(id);
        }

        /// <summary>
        /// Gets user by email (NOT USED AT PRESENT)
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User GetByEmail(string email)
        {
            return _context.Users.SingleOrDefault(x => x.Email.Equals(email));
        }

        /// <summary>
        /// Confirms user has role
        /// </summary>
        /// <param name="id"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool CheckRole(Guid id, Role role)
        {
            bool hasRole = false;
            User user = null;
            switch (role)
            {
                case Role.Admin:
                    user = _context.Users.Find(id);
                    hasRole = (user != null) ? user.Admin : false;
                    return hasRole;
                    // if admin check, return here.
                case Role.RiskManager:
                    user = _context.Users.Find(id);
                    hasRole = (user != null) ? user.RiskManager : false;
                    break;
                default:
                    break;
            }
            // Check if Admin as has all roles.
            if (!hasRole) { hasRole = (user != null) ? user.Admin : false; }
            return hasRole;
        }

        /// <summary>
        /// Creates new User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User Create(User user, string password)
        {
            // validation

            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_context.Users.Any() == false)
            {
                user.Admin = true;
            }
            else
            {
                if (_context.Users.Any(x => x.Email == user.Email))
                    throw new AppException("Email \"" + user.Email + "\" is already in use");
                if (_context.Users.Any(x => x.Username == user.Username))
                    throw new AppException("Username \"" + user.Username + "\" is already in use");
            }
            // Generate Hash and Salt
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        /// <summary>
        /// Updates user by parameter, if no password will remain old
        /// </summary>
        /// <param name="userParam"></param>
        /// <param name="password"></param>
        public void Update(User userParam, string password = null)
        {
            var user = _context.Users.Find(userParam.Id);

            if (user == null)
                throw new AppException("User not found");

            // update email if it has changed
            if (!string.IsNullOrWhiteSpace(userParam.Email) && userParam.Email != user.Email)
            {
                // throw error if the new email is already taken
                if (_context.Users.Any(x => x.Email == userParam.Email))
                    throw new AppException("Email " + userParam.Email + " is already in use");

                user.Email = userParam.Email;
            }

            // update username if it has changed
            if (!string.IsNullOrWhiteSpace(userParam.Username) && userParam.Username != user.Username)
            {
                // throw error if the new username is already taken
                if (_context.Users.Any(x => x.Username == userParam.Username))
                    throw new AppException("Username " + userParam.Username + " is already in use");

                user.Username = userParam.Username;
            }

            // update user properties if provided
            user.RiskManager = userParam.RiskManager;
            user.Admin = userParam.Admin;

            // update password if provided
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        /// <summary>
        /// Deletes user of id
        /// </summary>
        /// <param name="id"></param>
        public void Delete(Guid id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        // private helper methods

        // Generates password hash
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

        // verifies password matches stored password
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

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
    }
}