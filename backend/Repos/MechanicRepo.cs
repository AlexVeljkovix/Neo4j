using backend.Models;
using backend.Services;
using Neo4j.Driver;
using System.Security.Cryptography.X509Certificates;

namespace backend.Repos
{
    public class MechanicRepo : IMechanicRepo
    {
        private readonly IDriver _driver;

        public MechanicRepo(Neo4jDriverService driverService)
        {
            _driver = driverService.Driver;
        }

        public async Task<IEnumerable<Mechanic>> GetAllMechanics()
        {
            List<Mechanic> mechanics = new List<Mechanic>();
            await using var session = _driver.AsyncSession();
            var cursor = await session.RunAsync("MATCH (m:Mechanic) RETURN m");

            await cursor.ForEachAsync(record =>
            {
                var node = record["m"].As<INode>();
                mechanics.Add(new Mechanic
                {
                    Id = node.Properties["Id"].As<string>(),
                    Name = node.Properties["Name"].As<string>()
                });

            });

            return mechanics;
        }

        public async Task<Mechanic> GetMechanicById(string id)
        {
            List<Mechanic> mechanics = new List<Mechanic>();
            await using var session = _driver.AsyncSession();
            var cursor = await session.RunAsync("MATCH (m:Mechanic {Id:$Id}) RETURN m", 
                new {id});

            await cursor.ForEachAsync(record =>
            {
                var node = record["m"].As<INode>();
                mechanics.Add(new Mechanic
                {
                    Id = node.Properties["Id"].As<string>(),
                    Name = node.Properties["Name"].As<string>()
                });

            });

            return mechanics.FirstOrDefault();
        }

        public async Task<Mechanic> GetMechanicByName(string name)
        {
            List<Mechanic> mechanics = new List<Mechanic>();
            await using var session = _driver.AsyncSession();
            var cursor = await session.RunAsync("MATCH (m:Mechanic {Name:$name}) RETURN m",
                new {name});

            await cursor.ForEachAsync(record =>
            {
                var node = record["m"].As<INode>();
                mechanics.Add(new Mechanic
                {
                    Id = node.Properties["Id"].As<string>(),
                    Name = node.Properties["Name"].As<string>()
                });

            });

            return mechanics.FirstOrDefault();
        }

        public async Task<Mechanic> CreateMechanic(Mechanic mechanic)
        {
            await using var session = _driver.AsyncSession();
            await session.RunAsync("CREATE (m:Mechanic {Id:$Id, Name:$Name})",
                new
                {
                    mechanic.Id,
                    mechanic.Name
                }) ;
            return mechanic;
        }

        public async Task<Mechanic> UpdateMechanic(Mechanic mechanic)
        {
            await using var session = _driver.AsyncSession();
            await session.RunAsync(@"MATCH (m:Mechanic {Id:$Id}) SET m.Name=$Name",
                new
                {
                    mechanic.Id,
                    mechanic.Name
                });
            return mechanic;
        }

        public async Task<Mechanic> DeleteMechanic(string id)
        {
            Mechanic mechanic = await GetMechanicById(id);
            await using var session = _driver.AsyncSession();
            await session.RunAsync("MATCH (m:Mechanic {Id:$id}) DETACH DELETE m",
                new {id});
           
            return mechanic;
        }
        public async Task<IEnumerable<Mechanic>> GetMechanicsByGameId(string gameId)
        {
            List<Mechanic> mechanics = new List<Mechanic>();
            await using var session = _driver.AsyncSession();
            var cursor = await session.RunAsync("MATCH (g:Game {Id:$gameId})-[:HAS_MECHANIC]->(m:Mechanic) return m",
                new {gameId});
            await cursor.ForEachAsync(record =>
            {
                var node = record["m"].As<INode>();
                mechanics.Add(new Mechanic
                {
                    Id = node.Properties["Id"].As<string>(),
                    Name = node.Properties["Name"].As<string>()
                }) ;
            });

            return mechanics;
        }

    }
}
