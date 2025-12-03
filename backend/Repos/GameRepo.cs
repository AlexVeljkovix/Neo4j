using backend.Models;
using backend.Services;
using Neo4j.Driver;

namespace backend.Repos
{
    public class GameRepo : IGameRepo
    {
        private readonly IDriver _driver;

        public GameRepo(Neo4jDriverService driverService)
        {
            _driver = driverService.Driver;
        }

        public async Task<IEnumerable<Game>> GetAllGames()
        {
            List<Game> games = new List<Game>();
            await using var session = _driver.AsyncSession();
            var cursor = await session.RunAsync(@"
                MATCH (g:Game)
                OPTIONAL MATCH (g)<-[:CREATED]-(a:Author)
                OPTIONAL MATCH (g)<-[:PUBLISHED]-(p:Publisher)
                RETURN g, a, p
            ");

            await cursor.ForEachAsync(record =>
            {
                var gNode = record["g"].As<INode>();
                var aNode = record["a"]?.As<INode>();
                var pNode = record["p"]?.As<INode>();

                games.Add(new Game
                {
                    Id = gNode.Properties["Id"].As<string>(),
                    Title = gNode.Properties["Title"].As<string>(),
                    Description = gNode.Properties["Description"].As<string>(),
                    Author = new Author
                    {
                        Id = aNode.Properties["Id"].As<string>(),
                        FirstName = aNode.Properties["FirstName"].As<string>(),
                        LastName = aNode.Properties["LastName"].As<string>(),
                        Country = aNode.Properties["Country"].As<string>()
                    },
                    Publisher = new Publisher
                    {
                        Id = pNode.Properties["Id"].As<string>(),
                        Name = pNode.Properties["Name"].As<string>()
                    }
                });
            });

            return games;
        }

        public async Task<Game> GetGameById(string id)
        {
            List<Game> games = new List<Game>();
            await using var session = _driver.AsyncSession();

            var cursor = await session.RunAsync(@"
                MATCH (g:Game {Id:$id})
                OPTIONAL MATCH (g)<-[:CREATED]-(a:Author)
                OPTIONAL MATCH (g)<-[:PUBLISHED]-(p:Publisher)
                RETURN g, a, p
            ", new { id });

            await cursor.ForEachAsync(record =>
            {
                var gNode = record["g"].As<INode>();
                var aNode = record["a"]?.As<INode>();
                var pNode = record["p"]?.As<INode>();

                games.Add(new Game
                {
                    Id = gNode.Properties["Id"].As<string>(),
                    Title = gNode.Properties["Title"].As<string>(),
                    Description = gNode.Properties["Description"].As<string>(),
                    Author = new Author
                    {
                        Id = aNode.Properties["Id"].As<string>(),
                        FirstName = aNode.Properties["FirstName"].As<string>(),
                        LastName = aNode.Properties["LastName"].As<string>(),
                        Country = aNode.Properties["Country"].As<string>()
                    },
                    Publisher = new Publisher
                    {
                        Id = pNode.Properties["Id"].As<string>(),
                        Name = pNode.Properties["Name"].As<string>()
                    }
                });
            });

            return games.FirstOrDefault();
        }

        public async Task<IEnumerable<Game>> GetGameByTitle(string title)
        {
            List<Game> games = new List<Game>();
            await using var session = _driver.AsyncSession();

            var cursor = await session.RunAsync(@"
                MATCH (g:Game {Title:$title})
                OPTIONAL MATCH (g)<-[:CREATED]-(a:Author)
                OPTIONAL MATCH (g)<-[:PUBLISHED]-(p:Publisher)
                RETURN g, a, p
            ", new { title });

            await cursor.ForEachAsync(record =>
            {
                var gNode = record["g"].As<INode>();
                var aNode = record["a"]?.As<INode>();
                var pNode = record["p"]?.As<INode>();

                games.Add(new Game
                {
                    Id = gNode.Properties["Id"].As<string>(),
                    Title = gNode.Properties["Title"].As<string>(),
                    Description = gNode.Properties["Description"].As<string>(),
                    Author = new Author
                    {
                        Id = aNode.Properties["Id"].As<string>(),
                        FirstName = aNode.Properties["FirstName"].As<string>(),
                        LastName = aNode.Properties["LastName"].As<string>(),
                        Country = aNode.Properties["Country"].As<string>()
                    },
                    Publisher = new Publisher
                    {
                        Id = pNode.Properties["Id"].As<string>(),
                        Name = pNode.Properties["Name"].As<string>()
                    }
                });
            });

            return games;
        }

        public async Task<Game> CreateGame(Game game, string authorId, string publisherId)
        {
            await using var session = _driver.AsyncSession();
            await session.RunAsync(@"
                CREATE (g:Game {Id:$Id})
                SET g.Title = $Title,
                    g.Description = $Description

                WITH g
                MATCH (a:Author {Id:$authorId})
                MERGE (a)-[:CREATED]->(g)

                WITH g
                MATCH (p:Publisher {Id:$publisherId})
                MERGE (p)-[:PUBLISHED]->(g)
            ",
            new
            {
                game.Id,
                game.Title,
                game.Description,
                authorId,
                publisherId
            });

            return game;
        }

       
        public async Task<Game> UpdateGame(Game game)
        {
            await using var session = _driver.AsyncSession();

            await session.RunAsync(@"
                MATCH (g:Game {Id:$Id})
                SET g.Title = $Title,
                    g.Description = $Description

                WITH g
                OPTIONAL MATCH (g)<-[r1:CREATED]-()
                DELETE r1

                WITH g
                OPTIONAL MATCH (g)<-[r2:PUBLISHED]-()
                DELETE r2

                WITH g
                MATCH (a:Author {Id:$AuthorId})
                MERGE (a)-[:CREATED]->(g)

                WITH g
                MATCH (p:Publisher {Id:$PublisherId})
                MERGE (p)-[:PUBLISHED]->(g)
            ",
            new
            {
                game.Id,
                game.Title,
                game.Description,
                AuthorId = game.Author.Id,
                PublisherId = game.Publisher.Id
            });

            return game;
        }

        public async Task<Game> DeleteGame(string id)
        {
            Game game = await GetGameById(id);
            await using var session = _driver.AsyncSession();
            await session.RunAsync("MATCH (g:Game {Id:$id}) DETACH DELETE g", new { id });
            return game;
        }

        public async Task<IEnumerable<Game>> GetGamesByAuthorId(string authorId)
        {
            List<Game> games = new List<Game>();
            await using var session = _driver.AsyncSession();

            var cursor = await session.RunAsync(@"
                MATCH (a:Author {Id:$authorId})-[:CREATED]->(g:Game)
                OPTIONAL MATCH (g)<-[:PUBLISHED]-(p:Publisher)
                RETURN g, a, p
            ", new { authorId });

            await cursor.ForEachAsync(record =>
            {
                var gNode = record["g"].As<INode>();
                var aNode = record["a"]?.As<INode>();
                var pNode = record["p"]?.As<INode>();

                games.Add(new Game
                {
                    Id = gNode.Properties["Id"].As<string>(),
                    Title = gNode.Properties["Title"].As<string>(),
                    Description = gNode.Properties["Description"].As<string>(),
                    Author = new Author
                    {
                        Id = aNode.Properties["Id"].As<string>(),
                        FirstName = aNode.Properties["FirstName"].As<string>(),
                        LastName = aNode.Properties["LastName"].As<string>(),
                        Country = aNode.Properties["Country"].As<string>()
                    },
                    Publisher = new Publisher
                    {
                        Id = pNode.Properties["Id"].As<string>(),
                        Name = pNode.Properties["Name"].As<string>()
                    }
                });
            });

            return games;
        }

        public async Task<IEnumerable<Game>> GetGamesByPublisherId(string publisherId)
        {
            List<Game> games = new List<Game>();
            await using var session = _driver.AsyncSession();

            var cursor = await session.RunAsync(@"
                MATCH (p:Publisher {Id:$publisherId})-[:PUBLISHED]->(g:Game)
                OPTIONAL MATCH (g)<-[:CREATED]-(a:Author)
                RETURN g, a, p
            ", new { publisherId });

            await cursor.ForEachAsync(record =>
            {
                var gNode = record["g"].As<INode>();
                var aNode = record["a"]?.As<INode>();
                var pNode = record["p"]?.As<INode>();

                games.Add(new Game
                {
                    Id = gNode.Properties["Id"].As<string>(),
                    Title = gNode.Properties["Title"].As<string>(),
                    Description = gNode.Properties["Description"].As<string>(),
                    Author = new Author
                    {
                        Id = aNode.Properties["Id"].As<string>(),
                        FirstName = aNode.Properties["FirstName"].As<string>(),
                        LastName = aNode.Properties["LastName"].As<string>(),
                        Country = aNode.Properties["Country"].As<string>()
                    },
                    Publisher = new Publisher
                    {
                        Id = pNode.Properties["Id"].As<string>(),
                        Name = pNode.Properties["Name"].As<string>()
                    }
                });
            });

            return games;
        }

        public async Task<IEnumerable<Game>> GetGamesByMechanicsId(string mechanicsId)
        {
            List<Game> games = new List<Game>();
            await using var session = _driver.AsyncSession();

            var cursor = await session.RunAsync(@"
                MATCH (g:Game)-[:HAS_MECHANIC]->(m:Mechanic {Id:$mechanicsId})
                OPTIONAL MATCH (g)<-[:CREATED]-(a:Author)
                OPTIONAL MATCH (g)<-[:PUBLISHED]-(p:Publisher)
                RETURN g, a, p
            ", new { mechanicsId });

            await cursor.ForEachAsync(record =>
            {
                var gNode = record["g"].As<INode>();
                var aNode = record["a"]?.As<INode>();
                var pNode = record["p"]?.As<INode>();

                games.Add(new Game
                {
                    Id = gNode.Properties["Id"].As<string>(),
                    Title = gNode.Properties["Title"].As<string>(),
                    Description = gNode.Properties["Description"].As<string>(),
                    Author = new Author
                    {
                        Id = aNode.Properties["Id"].As<string>(),
                        FirstName = aNode.Properties["FirstName"].As<string>(),
                        LastName = aNode.Properties["LastName"].As<string>(),
                        Country = aNode.Properties["Country"].As<string>()
                    },
                    Publisher = new Publisher
                    {
                        Id = pNode.Properties["Id"].As<string>(),
                        Name = pNode.Properties["Name"].As<string>()
                    }
                });
            });

            return games;
        }

        public async Task<Game> AddMechanicToGame(string gameId, string mechanicId)
        {
            await using var session = _driver.AsyncSession();

            await session.RunAsync(@"
                                    MATCH (g:Game {Id:$gameId})
                                    MATCH (m:Mechanic {Id:$mechanicId})
                                    MERGE (g)-[:HAS_MECHANIC]->(m)",
                                    new { gameId, mechanicId });

            return await GetGameById(gameId);
        }

       
    }
}
