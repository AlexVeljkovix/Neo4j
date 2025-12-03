using backend.Models;
using backend.Services;
using Neo4j.Driver;

namespace backend.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly IDriver _driver;
        public UserRepo(Neo4jDriverService driverService)
        {
            _driver = driverService.Driver;
        }

        public async Task<User> GetUserById(string id)
        {
            await using var session = _driver.AsyncSession();

            var cursor = await session.RunAsync(
                "MATCH (u:User {Id: $id}) RETURN u",
                new { id }
            );

            var record = await cursor.SingleAsync(); // uzmi jedini rezultat
            if (record == null) return null;

            var node = record["u"].As<INode>();

            return new User
            {
                Id = node.Properties["Id"].As<string>(),
                Username = node.Properties["Username"].As<string>(),
                Email = node.Properties["Email"].As<string>(),
                Password = node.Properties["Password"].As<string>()
            };
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var users = new List<User>();
            await using var session = _driver.AsyncSession();

            var cursor = await session.RunAsync("MATCH (u:User) RETURN u");

            await cursor.ForEachAsync(record =>
            {
                var node = record["u"].As<INode>();
                users.Add(new User
                {
                    Id = node.Properties["Id"].As<string>(),
                    Username = node.Properties["Username"].As<string>(),
                    Email = node.Properties["Email"].As<string>(),
                    Password = node.Properties["Password"].As<string>()
                });
            });

            return users;
        }
        public async Task<User> CreateUser(User user)
        {
            await using var session = _driver.AsyncSession();

            await session.RunAsync(
                @"CREATE (u:User {
                    Id: $Id,
                    Username: $Username,
                    Email: $Email,
                    Password: $Password
                })",
                new
                {
                    user.Id,
                    user.Username,
                    user.Email,
                    user.Password
                }
            );

            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            await using var session = _driver.AsyncSession();

            await session.RunAsync(
                @"MATCH (u:User {Id: $Id})
                  SET u.Username = $Username,
                      u.Email = $Email,
                      u.Password = $Password",
                new
                {
                    user.Id,
                    user.Username,
                    user.Email,
                    user.Password
                }
            );

            return user;
        }

        public async Task<User> DeleteUser(string id)
        {
            var user = await GetUserById(id);
            if (user == null) return null;

            await using var session = _driver.AsyncSession();

            await session.RunAsync(
                "MATCH (u:User {Id: $id}) DETACH DELETE u",
                new { id }
            );

            return user;
        }

       
    }
}
