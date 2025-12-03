using backend.Models;

namespace backend.Repos
{
    public interface IUserRepo
    {
        Task<User> GetUserById(string id);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task<User> DeleteUser (string id);

    }
}
