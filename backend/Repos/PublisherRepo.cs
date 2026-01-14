using backend.Models;
using backend.Services;
using Neo4j.Driver;
using System.Xml.Linq;

namespace backend.Repos
{
    public class PublisherRepo:IPublisherRepo
    {
        private readonly IDriver _driver;

        public PublisherRepo(Neo4jDriverService driverService)
        {
            _driver = driverService.Driver;
        }

        public async Task<IEnumerable<Publisher>> GetAllPublishers()
        {
            List<Publisher> publishers = new List<Publisher>();
            await using var session = _driver.AsyncSession();

            var cursor = await session.RunAsync("MATCH (p:Publisher) RETURN p");
            await cursor.ForEachAsync(record =>
            {
                var node = record["p"].As<INode>();

                publishers.Add(new Publisher
                {
                    Id = node.Properties["Id"].As<string>(),
                    Name = node.Properties["Name"].As<string>(),
                    Country = node.Properties["Country"].As<string>()
                }) ;
            });
            return publishers;
        }

        public async Task<Publisher> GetPublisherById(string publisherId)
        {
            Publisher publisher = null;

            await using var session = _driver.AsyncSession();

            var cursor = await session.RunAsync(@"
                                                MATCH (p:Publisher {Id:$publisherId})
                                                OPTIONAL MATCH (p)-[:PUBLISHED]->(g:Game)
                                                OPTIONAL MATCH (g)<-[:CREATED]-(a:Author)
                                                OPTIONAL MATCH (g)-[:HAS_MECHANIC]->(m:Mechanic)
                                                RETURN p, g, a, collect(m) AS mechanics",
                                                new { publisherId });

            var gamesDict = new Dictionary<string, Game>();

            await cursor.ForEachAsync(record =>
            {
                if (publisher == null)
                {
                    var pNode = record["p"].As<INode>();
                    publisher = new Publisher
                    {
                        Id = pNode.Properties["Id"].As<string>(),
                        Name = pNode.Properties["Name"].As<string>(),
                        Country = pNode.Properties["Country"].As<string>(),
                        Games = new List<Game>()
                    };
                }

                
                if (!(record["g"] is INode gNode))
                    return;

                string gameId = gNode.Properties["Id"].As<string>();
                if (!gamesDict.TryGetValue(gameId, out var game))
                {
                    game = new Game
                    {
                        Id = gameId,
                        Title = gNode.Properties["Title"].As<string>(),
                        Description = gNode.Properties["Description"].As<string>(),
                        Mechanics = new List<Mechanic>(),
                        Difficulty = gNode.Properties["Difficulty"].As<float>(),
                        AvailableUnits = gNode.Properties["AvailableUnits"].As<float>()

                    };

                    gamesDict.Add(gameId, game);
                }

                if (record["a"] is INode aNode)
                {
                    game.Author = new Author
                    {
                        Id = aNode.Properties["Id"].As<string>(),
                        FirstName = aNode.Properties["FirstName"].As<string>(),
                        LastName = aNode.Properties["LastName"].As<string>(),
                        Country = aNode.Properties["Country"].As<string>()
                    };
                }


                foreach (var mNode in record["mechanics"].As<List<INode>>())
                {
                    game.Mechanics.Add(new Mechanic
                    {
                        Id = mNode.Properties["Id"].As<string>(),
                        Name = mNode.Properties["Name"].As<string>()
                    });
                }
            });

            if (publisher != null)
                publisher.Games = gamesDict.Values.ToList();

            return publisher;
        }

        public async Task<IEnumerable<Publisher>> GetPublisherByName(string publisherName)
        {
            var publishersDict = new Dictionary<string, Publisher>();
            await using var session = _driver.AsyncSession();

            var cursor = await session.RunAsync(@"MATCH(p: Publisher { Name:$publisherName})
                                                OPTIONAL MATCH(p)- [:PUBLISHED]->(g: Game)
                                                OPTIONAL MATCH(g)< - [:CREATED] - (a: Author)
                                                OPTIONAL MATCH(g)- [:HAS_MECHANIC]->(m: Mechanic)
                                                RETURN p, g, a, collect(m) AS mechanics",
                                                new { publisherName });


            await cursor.ForEachAsync(record =>
            {
                var pNode = record["p"].As<INode>();
                string publisherId = pNode.Properties["Id"].As<string>();


                if (!publishersDict.TryGetValue(publisherId, out var publisher))
                {
                    publisher = new Publisher
                    {
                        Id=publisherId,
                        Name = pNode.Properties["Name"].As<string>(),
                        Country= pNode.Properties["Country"].As<string>(),
                        Games = new List<Game>()
                    };

                    publishersDict.Add(publisherId, publisher);
                }


                if (!(record["g"] is INode gNode))
                    return;


                string gameId = gNode.Properties["Id"].As<string>();
                var game = publisher.Games.FirstOrDefault(g => g.Id == gameId);
                if (game == null)
                {
                    game = new Game
                    {
                        Id = gameId,
                        Title = gNode.Properties["Title"].As<string>(),
                        Description = gNode.Properties["Description"].As<string>(),
                        Difficulty = gNode.Properties["Difficulty"].As<float>(),
                        AvailableUnits= gNode.Properties["AvailableUnits"].As<float>(),
                        Mechanics = new List<Mechanic>()
                    };

                    publisher.Games.Add(game);
                }


                if (record["a"] is INode aNode)
                {
                    game.Author = new Author
                    {
                        Id = aNode.Properties["Id"].As<string>(),
                        FirstName = aNode.Properties["FirstName"].As<string>(),
                        LastName = aNode.Properties["LastName"].As<string>(),
                        Country = aNode.Properties["Country"].As<string>()
                    };
                }


                foreach (var mNode in record["mechanics"].As<List<INode>>())
                {
                    game.Mechanics.Add(new Mechanic
                    {
                        Id = mNode.Properties["Id"].As<string>(),
                        Name = mNode.Properties["Name"].As<string>()
                    });
                }
            });

            return publishersDict.Values.ToList();
        }

        public async Task<Publisher> CreatePublisher(Publisher publisher)
        {
            await using var session = _driver.AsyncSession();
            await session.RunAsync("CREATE (p:Publisher {Id:$Id, Name:$Name, Country:$Country})",
                new
                {
                    publisher.Id,
                    publisher.Name,
                    publisher.Country
                });
            return publisher;
        }

        public async Task<Publisher> UpdatePublisher(Publisher publisher)
        {
            await using var session = _driver.AsyncSession();
            await session.RunAsync(@"MATCH (p:Publisher {Id:$Id}) SET
                                            p.Name=$Name,
                                            p.Country=$Country",
                                            new
                                            {
                                                publisher.Id,
                                                publisher.Name,
                                                publisher.Country
                                            });
            return publisher;
        }

        public async Task<Publisher> DeletePublisher(string publisherId)
        {
            Publisher publisher = await GetPublisherById(publisherId);
            await using var session = _driver.AsyncSession();
            await session.RunAsync("MATCH (p:Publisher {Id:$publisherId}) DETACH DELETE p",
                new {publisherId});
            return publisher;
        }

        public async Task<Publisher> GetPublisherByGameId(string gameId)
        {
            Publisher publisher = null;

            await using var session = _driver.AsyncSession();

            var cursor = await session.RunAsync(@"
                                                MATCH (p)-[:PUBLISHED]->(g:Game {Id:$gameId})
                                                OPTIONAL MATCH (g)<-[:CREATED]-(a:Author)
                                                OPTIONAL MATCH (g)-[:HAS_MECHANIC]->(m:Mechanic)
                                                RETURN p, g, a, collect(m) AS mechanics",
                                                new { gameId });

            var gamesDict = new Dictionary<string, Game>();

            await cursor.ForEachAsync(record =>
            {
                if (publisher == null)
                {
                    var pNode = record["p"].As<INode>();
                    publisher = new Publisher
                    {
                        Id = pNode.Properties["Id"].As<string>(),
                        Name = pNode.Properties["Name"].As<string>(),
                        Country = pNode.Properties["Country"].As<string>(),
                        Games = new List<Game>()
                    };
                }


                if (!(record["g"] is INode gNode))
                    return;

                string gameId = gNode.Properties["Id"].As<string>();
                if (!gamesDict.TryGetValue(gameId, out var game))
                {
                    game = new Game
                    {
                        Id = gameId,
                        Title = gNode.Properties["Title"].As<string>(),
                        Description = gNode.Properties["Description"].As<string>(),
                        Difficulty = gNode.Properties["Difficulty"].As<float>(),
                        AvailableUnits = gNode.Properties["AvailableUnits"].As<float>(),
                        Mechanics = new List<Mechanic>()
                    };

                    gamesDict.Add(gameId, game);
                }

                if (record["a"] is INode aNode)
                {
                    game.Author = new Author
                    {
                        Id = aNode.Properties["Id"].As<string>(),
                        FirstName = aNode.Properties["FirstName"].As<string>(),
                        LastName = aNode.Properties["LastName"].As<string>(),
                        Country = aNode.Properties["Country"].As<string>()
                    };
                }


                foreach (var mNode in record["mechanics"].As<List<INode>>())
                {
                    game.Mechanics.Add(new Mechanic
                    {
                        Id = mNode.Properties["Id"].As<string>(),
                        Name = mNode.Properties["Name"].As<string>()
                    });
                }
            });

            if (publisher != null)
                publisher.Games = gamesDict.Values.ToList();

            return publisher;
        }

    }
}
