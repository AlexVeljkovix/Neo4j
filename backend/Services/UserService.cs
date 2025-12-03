using backend.Models;
using backend.Repos;
using System.Net.Mail;

namespace backend.Services
{
    public class UserService
    {
        private readonly IUserRepo _repo;

        public UserService(IUserRepo repo)
        {
            _repo = repo;
        }

        // GET user by ID
        public async Task<User> GetUserByID(string id)
        {
            var user = await _repo.GetUserById(id);
            if (user == null)
                throw new KeyNotFoundException("User not found");
            return user;
        }

        // GET all users
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _repo.GetAllUsers();
        }

        // CREATE user
        public async Task<User> CreateUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Username))
                throw new ArgumentException("Username cannot be empty");

            if (string.IsNullOrWhiteSpace(user.Email) || !IsValidEmail(user.Email))
                throw new ArgumentException("Invalid email format");

            if (string.IsNullOrWhiteSpace(user.Password) || user.Password.Length < 6)
                throw new ArgumentException("Password must be at least 6 characters");

            var existingUsers = await _repo.GetAllUsers();
            if (existingUsers.Any(u => u.Email == user.Email))
                throw new InvalidOperationException("Email already exists");
            if (existingUsers.Any(u => u.Username == user.Username))
                throw new InvalidOperationException("Username already exists");

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            return await _repo.CreateUser(user);
        }

        // UPDATE user
        public async Task<User> UpdateUser(User user)
        {
            var existingUser = await _repo.GetUserById(user.Id);
            if (existingUser == null)
                throw new KeyNotFoundException("User not found");

            if (!string.IsNullOrWhiteSpace(user.Email) && !IsValidEmail(user.Email))
                throw new ArgumentException("Invalid email format");

            if (!string.IsNullOrWhiteSpace(user.Password))
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            else
                user.Password = existingUser.Password; 

            return await _repo.UpdateUser(user);
        }

        // DELETE user
        public async Task<User> DeleteUser(string userId)
        {
            var userToDelete = await _repo.GetUserById(userId);
            if (userToDelete == null)
                throw new KeyNotFoundException("User not found");

            return await _repo.DeleteUser(userId);
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
