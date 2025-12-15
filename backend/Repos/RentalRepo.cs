using backend.DTOs;
using backend.Models;
using backend.Services;
using Neo4j.Driver;
using System.Xml.Linq;

namespace backend.Repos
{
    public class RentalRepo : IRentalRepo
    {
        private readonly IDriver _driver;

        public RentalRepo(Neo4jDriverService neo4jDriverService)
        {
            _driver = neo4jDriverService.Driver;
        }
        
        public async Task<IEnumerable<RentalWithGameDTO>> GetAllRentals()
        {
            List<RentalWithGameDTO> rentals = new List<RentalWithGameDTO>();
            await using var session= _driver.AsyncSession();
            var cursor = await session.RunAsync("MATCH (r:Rental)-[:RENTED_GAME]->(g:Game) RETURN r, g.Id AS GameId, g.Title AS GameTitle");
            await cursor.ForEachAsync(record =>
            {
                var rNode = record["r"].As<INode>();
                rentals.Add(new RentalWithGameDTO
                {
                    Id = rNode.Properties["Id"].As<string>(),
                    Active = rNode.Properties["Active"].As<bool>(),
                    RentalDate = rNode.Properties["RentalDate"].As<ZonedDateTime>().ToDateTimeOffset().DateTime,
                    ReturnDate = rNode.Properties.ContainsKey("ReturnDate") ? (DateTime?)rNode.Properties["ReturnDate"].As<ZonedDateTime>().ToDateTimeOffset().DateTime : null,
                    PersonName = rNode.Properties["PersonName"].As<string>(),
                    PersonPhoneNumber = rNode.Properties["PersonPhoneNumber"].As<string>(),
                    PersonJMBG = rNode.Properties["PersonJMBG"].As<string>(),
                    GameId = record["GameId"].As<string>(),
                    GameName = record["GameTitle"].As<string>()
                });
            });
            return rentals;
        }
        public async Task<IEnumerable<RentalWithGameDTO>>GetAllActiveRentals()
        {
            List<RentalWithGameDTO> rentals = new List<RentalWithGameDTO>();
            await using var session = _driver.AsyncSession();
            var cursor = await session.RunAsync(@"MATCH 
                (r:Rental {Active: true})-[:RENTED_GAME]->(g:Game) 
                RETURN r, g.Id AS GameId, g.Title AS GameName");

            await cursor.ForEachAsync(record =>
            {
                var rNode = record["r"].As<INode>();
                rentals.Add(new RentalWithGameDTO
                {

                    Id = rNode.Properties["Id"].As<string>(),
                    Active = rNode.Properties["Active"].As<bool>(),
                    RentalDate = rNode.Properties["RentalDate"].As<ZonedDateTime>().ToDateTimeOffset().DateTime,
                    ReturnDate = rNode.Properties.ContainsKey("ReturnDate") ? (DateTime?)rNode.Properties["ReturnDate"].As<ZonedDateTime>().ToDateTimeOffset().DateTime : null,
                    PersonName = rNode.Properties["PersonName"].As<string>(),
                    PersonPhoneNumber = rNode.Properties["PersonPhoneNumber"].As<string>(),
                    PersonJMBG = rNode.Properties["PersonJMBG"].As<string>(),
                    GameId = record["GameId"].As<string>(),
                    GameName = record["GameName"].As<string>()
                });
            });
            return rentals;
        }
        public async Task<RentalWithGameDTO> GetRentalById(string id)
        {
            RentalWithGameDTO rental = null;
            await using var session= _driver.AsyncSession();
            var cursor = await session.RunAsync(@"MATCH 
                                (r:Rental {Id: $id})-[:RENTED_GAME]->(g:Game) 
                                RETURN r, g.Id AS GameId, g.Title AS GameName", new { id });

            if (!await cursor.FetchAsync())
                return null;

            var record = cursor.Current;

            var rNode = record["r"].As<INode>();
            rental = new RentalWithGameDTO
            {

                Id = rNode.Properties["Id"].As<string>(),
                Active = rNode.Properties["Active"].As<bool>(),
                RentalDate = rNode.Properties["RentalDate"].As<ZonedDateTime>().ToDateTimeOffset().DateTime,
                ReturnDate = rNode.Properties.ContainsKey("ReturnDate") ? (DateTime?)rNode.Properties["ReturnDate"].As<ZonedDateTime>().ToDateTimeOffset().DateTime : null,
                PersonName = rNode.Properties["PersonName"].As<string>(),
                PersonPhoneNumber = rNode.Properties["PersonPhoneNumber"].As<string>(),
                PersonJMBG = rNode.Properties["PersonJMBG"].As<string>(),
                GameId = record["GameId"].As<string>(),
                GameName = record["GameName"].As<string>()
            };
           
            return rental;
        }

        public async Task<IEnumerable<RentalWithGameDTO>> GetActiveRentals(string JMBG)
        {
            List<RentalWithGameDTO> rentals = new List<RentalWithGameDTO>();
            await using var session = _driver.AsyncSession();
            var cursor = await session.RunAsync(@"MATCH 
                (r:Rental {PersonJMBG: $JMBG})-[:RENTED_GAME]->(g:Game) 
                RETURN r, g.Id AS GameId, g.Title AS GameName", new { JMBG });

            await cursor.ForEachAsync(record =>
            {
                var rNode = record["r"].As<INode>();
                rentals.Add(new RentalWithGameDTO
                {

                    Id = rNode.Properties["Id"].As<string>(),
                    Active = rNode.Properties["Active"].As<bool>(),
                    RentalDate = rNode.Properties["RentalDate"].As<ZonedDateTime>().ToDateTimeOffset().DateTime,
                    ReturnDate = rNode.Properties.ContainsKey("ReturnDate") ? (DateTime?)rNode.Properties["ReturnDate"].As<ZonedDateTime>().ToDateTimeOffset().DateTime : null,
                    PersonName = rNode.Properties["PersonName"].As<string>(),
                    PersonPhoneNumber = rNode.Properties["PersonPhoneNumber"].As<string>(),
                    PersonJMBG = rNode.Properties["PersonJMBG"].As<string>(),
                    GameId = record["GameId"].As<string>(),
                    GameName = record["GameName"].As<string>()
                });
            });

            

            return rentals;
        }

        public async Task<IEnumerable<RentalWithGameDTO>> GetRentalsByGameId(string gameId)
        {
            List<RentalWithGameDTO> rentals = new List<RentalWithGameDTO>();
            await using var session=_driver.AsyncSession();
            var cursor= await session.RunAsync(@"MATCH 
            (g:Game {Id: $gameId})<-[:RENTED_GAME]-(r:Rental) 
            RETURN r,  g.Title AS GameName", new { gameId });
            await cursor.ForEachAsync(record =>
            {
                var rNode = record["r"].As<INode>();
                rentals.Add(new RentalWithGameDTO
                {

                    Id = rNode.Properties["Id"].As<string>(),
                    Active = rNode.Properties["Active"].As<bool>(),
                    RentalDate = rNode.Properties["RentalDate"].As<ZonedDateTime>().ToDateTimeOffset().DateTime,
                    ReturnDate = rNode.Properties.ContainsKey("ReturnDate") ? (DateTime?)rNode.Properties["ReturnDate"].As<ZonedDateTime>().ToDateTimeOffset().DateTime : null,
                    PersonName = rNode.Properties["PersonName"].As<string>(),
                    PersonPhoneNumber = rNode.Properties["PersonPhoneNumber"].As<string>(),
                    PersonJMBG = rNode.Properties["PersonJMBG"].As<string>(),
                    GameId = gameId,
                    GameName = record["GameName"].As<string>()
                });
            });

            return rentals;
        }
        public async Task<Rental> CreateRentalRecord(Rental rental, string gameId)
        {
            await using var session=_driver.AsyncSession();
            var cursor=await session.RunAsync(@" MATCH (g:Game {Id:$gameId})
                                                            WHERE g.AvailableUnits>0
                                                            CREATE (r:Rental {Id: $id,
                                                            Active: true,   
                                                            RentalDate: datetime($rentalDate), 
                                                            PersonName: $personName,
                                                            PersonPhoneNumber: $personPhoneNumber, 
                                                            PersonJMBG: $personJMBG,
                                                            GameId: $gameId})
                                                            CREATE (r)-[:RENTED_GAME]->(g)
                                                            SET g.AvailableUnits = g.AvailableUnits-1",
                                                           
                                                           new
                                                           {
                                                                gameId,
                                                                id = rental.Id,
                                                                rentalDate = DateTime.Now.ToString("o"),
                                                                personName = rental.PersonName,
                                                                personPhoneNumber = rental.PersonPhoneNumber,
                                                                personJMBG = rental.PersonJMBG
                                                            });
            await cursor.ConsumeAsync();
            return rental;
        }

        public async Task<RentalWithGameDTO> FinishRentalRecord(string id)
        {
            RentalWithGameDTO rental = await GetRentalById(id);
            if (rental == null)
                return null;
            await using var session = _driver.AsyncSession();
            await session.RunAsync(@"MATCH (r:Rental {Id: $id}) SET r.Active=false,
                                                                    r.ReturnDate=datetime($date)
                                WITH r
                                MATCH (g)<-[:RENTED_GAME]-(r) SET g.AvailableUnits=g.AvailableUnits+1", new {id, date=DateTime.Now});
            return await GetRentalById(id);
        }
        public async Task<RentalWithGameDTO> DeleteRentalRecord(string id)
        {
            RentalWithGameDTO rental = await GetRentalById(id);
            if (rental == null)
                return null;
            await using var session = _driver.AsyncSession();
            await session.RunAsync("MATCH (r:Rental {Id: $id}) DETACH DELETE r", new { id });
            return rental;
        }

       
    }
}
