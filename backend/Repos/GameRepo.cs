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
                MATCH (g)<-[:CREATED]-(a:Author)
                MATCH (g)<-[:PUBLISHED]-(p:Publisher)
                OPTIONAL MATCH (g)-[:HAS_MECHANIC]->(m:Mechanic)
                RETURN g, a, p, collect(m) AS mechanics

            ");

            await cursor.ForEachAsync(record =>
            {
                var gNode = record["g"].As<INode>();
                var aNode = record["a"]?.As<INode>();
                var pNode = record["p"]?.As<INode>();
                Game game = new Game
                {
                    Id = gNode.Properties["Id"].As<string>(),
                    Title = gNode.Properties["Title"].As<string>(),
                    Description = gNode.Properties["Description"].As<string>(),
                    Difficulty = gNode.Properties["Difficulty"].As<float>(),
                    AvailableUnits = gNode.Properties["AvailableUnits"].As<float>(),
                    Mechanics = new List<Mechanic>(),
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
                        Name = pNode.Properties["Name"].As<string>(),
                        Country = pNode.Properties["Country"].As<string>()
                    }
                };
                foreach (var mNode in record["mechanics"].As<List<INode>>())
                {
                    game.Mechanics.Add(new Mechanic
                    {
                        Id = mNode.Properties["Id"].As<string>(),
                        Name = mNode.Properties["Name"].As<string>()
                    });
                }
                games.Add(game);
            });

            return games;
        }

        public async Task<IEnumerable<Game>> GetAllAvailableGames()
        {
            List<Game> games = new List<Game>();
            await using var session = _driver.AsyncSession();
            var cursor = await session.RunAsync(@"
                MATCH (g:Game)
                WHERE g.AvailableUnits>0
                MATCH (g)<-[:CREATED]-(a:Author)
                MATCH (g)<-[:PUBLISHED]-(p:Publisher)
                OPTIONAL MATCH (g)-[:HAS_MECHANIC]->(m:Mechanic)
                RETURN g, a, p, collect(m) AS mechanics

            ");

            await cursor.ForEachAsync(record =>
            {
                var gNode = record["g"].As<INode>();
                var aNode = record["a"]?.As<INode>();
                var pNode = record["p"]?.As<INode>();
                Game game = new Game
                {
                    Id = gNode.Properties["Id"].As<string>(),
                    Title = gNode.Properties["Title"].As<string>(),
                    Description = gNode.Properties["Description"].As<string>(),
                    Difficulty = gNode.Properties["Difficulty"].As<float>(),
                    AvailableUnits = gNode.Properties["AvailableUnits"].As<float>(),
                    Mechanics = new List<Mechanic>(),
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
                        Name = pNode.Properties["Name"].As<string>(),
                        Country = pNode.Properties["Country"].As<string>()
                    }
                };
                foreach (var mNode in record["mechanics"].As<List<INode>>())
                {
                    game.Mechanics.Add(new Mechanic
                    {
                        Id = mNode.Properties["Id"].As<string>(),
                        Name = mNode.Properties["Name"].As<string>()
                    });
                }
                games.Add(game);
            });

            return games;
        }

        public async Task<Game> GetGameById(string id)
        {
            
            await using var session = _driver.AsyncSession();
            var cursor = await session.RunAsync(@"
                MATCH (g:Game {Id:$id})
                MATCH (g)<-[:CREATED]-(a:Author)
                MATCH (g)<-[:PUBLISHED]-(p:Publisher)
                WITH g, a, p
                MATCH (g)-[:HAS_MECHANIC]->(m:Mechanic)
                RETURN g, a, p, collect(m) AS mechanics
            ", new {id});

            Game game = null;
            await cursor.ForEachAsync(record =>
            {
                var gNode = record["g"].As<INode>();
                var aNode = record["a"].As<INode>();
                var pNode = record["p"].As<INode>();
                game = new Game
                {
                    Id = gNode.Properties["Id"].As<string>(),
                    Title = gNode.Properties["Title"].As<string>(),
                    Description = gNode.Properties["Description"].As<string>(),
                    Difficulty = gNode.Properties["Difficulty"].As<float>(),
                    AvailableUnits = gNode.Properties["AvailableUnits"].As<float>(),
                    Mechanics = new List<Mechanic>(),
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
                        Name = pNode.Properties["Name"].As<string>(),
                        Country = pNode.Properties["Country"].As<string>()
                    }
                };
                foreach (var mNode in record["mechanics"].As<List<INode>>())
                {
                    game.Mechanics.Add(new Mechanic
                    {
                        Id = mNode.Properties["Id"].As<string>(),
                        Name = mNode.Properties["Name"].As<string>()
                    });
                }
 
            });

            return game;
        }

        public async Task<IEnumerable<Game>> GetGameByTitle(string title)
        {
            List<Game> games = new List<Game>();
            await using var session = _driver.AsyncSession();
            var cursor = await session.RunAsync(@"
                MATCH (g:Game {Title:$title})
                MATCH (g)<-[:CREATED]-(a:Author)
                MATCH (g)<-[:PUBLISHED]-(p:Publisher)
                WITH g, a, p
                MATCH (g)-[:HAS_MECHANIC]->(m:Mechanic)
                RETURN g, a, p, collect(m) AS mechanics
            ", new {title});

            await cursor.ForEachAsync(record =>
            {
                var gNode = record["g"].As<INode>();
                var aNode = record["a"]?.As<INode>();
                var pNode = record["p"]?.As<INode>();
                Game game = new Game
                {
                    Id = gNode.Properties["Id"].As<string>(),
                    Title = gNode.Properties["Title"].As<string>(),
                    Description = gNode.Properties["Description"].As<string>(),
                    Difficulty = gNode.Properties["Difficulty"].As<float>(),
                    AvailableUnits = gNode.Properties["AvailableUnits"].As<float>(),
                    Mechanics = new List<Mechanic>(),
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
                        Name = pNode.Properties["Name"].As<string>(),
                        Country = pNode.Properties["Country"].As<string>()
                    }
                };
                foreach (var mNode in record["mechanics"].As<List<INode>>())
                {
                    game.Mechanics.Add(new Mechanic
                    {
                        Id = mNode.Properties["Id"].As<string>(),
                        Name = mNode.Properties["Name"].As<string>()
                    });
                }
                games.Add(game);
            });

            return games;
        }

       public async Task<Game> CreateGame(
    Game game,
    List<string> mechanicIds,
    string authorId,
    string publisherId)
{
    await using var session = _driver.AsyncSession();

    // 1️⃣ CREATE game + relacije
    await session.RunAsync(@"
        CREATE (g:Game {
            Id:$Id,
            Title:$Title,
            Description:$Description,
            Difficulty:$Difficulty,
            AvailableUnits:$AvailableUnits
        })
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
        game.Difficulty,
        game.AvailableUnits,
        authorId,
        publisherId
    });

    foreach (var mechanicId in mechanicIds)
    {
        await session.RunAsync(@"
            MATCH (g:Game {Id:$gameId})
            MATCH (m:Mechanic {Id:$mechanicId})
            MERGE (g)-[:HAS_MECHANIC]->(m)
        ", new { gameId = game.Id, mechanicId });
    }

    // 2️⃣ 🔥 NAJBITNIJE: ponovo pročitaj iz baze
    return await GetGameById(game.Id);
}




        public async Task<Game> UpdateGame(Game game)
        {
            await using var session = _driver.AsyncSession();

            await session.RunAsync(@"
                MATCH (g:Game {Id:$Id})
                SET g.Title = $Title,
                    g.Description = $Description,
                    g.Difficulty=$Difficulty,
                    g.AvailableUnits=$AvailableUnits

                WITH g
                MATCH (g)<-[r1:CREATED]-()
                DELETE r1

                WITH g
                MATCH (g)<-[r2:PUBLISHED]-()
                DELETE r2

                WITH g
                MATCH (a:Author {Id:$AuthorId})
                MERGE (a)-[:CREATED]->(g)

                WITH g
                MATCH (p:Publisher {Id:$PublisherId})
                MERGE (p)-[:PUBLISHED]->(g)

                WITH g
                OPTIONAL MATCH (g)-[r3:HAS_MECHANIC]->()
                DELETE r3
            ",
            new
            {
                game.Id,
                game.Title,
                game.Description,
                AuthorId = game.Author.Id,
                PublisherId = game.Publisher.Id
            });
            foreach(var mechanic in game.Mechanics)
            {
                await session.RunAsync(@"
                MATCH (g:Game {Id:$gameId})
                MATCH (m:Mechanic {Id:$mechanicId})
                MERGE (g)-[:HAS_MECHANIC]->(m)
            ", new { gameId = game.Id, mechanicId =mechanic.Id});
            }
           

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
                MATCH (g)<-[:PUBLISHED]-(p:Publisher)
                WITH g, a, p
                MATCH (g)-[:HAS_MECHANIC]->(m:Mechanic)
                RETURN g, a, p, collect(m) AS mechanics
            ", new { authorId });

            await cursor.ForEachAsync(record =>
            {
                var gNode = record["g"].As<INode>();
                var aNode = record["a"]?.As<INode>();
                var pNode = record["p"]?.As<INode>();
                Game game = new Game
                {
                    Id = gNode.Properties["Id"].As<string>(),
                    Title = gNode.Properties["Title"].As<string>(),
                    Description = gNode.Properties["Description"].As<string>(),
                    Difficulty = gNode.Properties["Difficulty"].As<float>(),
                    AvailableUnits = gNode.Properties["AvailableUnits"].As<float>(),
                    Mechanics = new List<Mechanic>(),
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
                        Name = pNode.Properties["Name"].As<string>(),
                        Country = pNode.Properties["Country"].As<string>()
                    }
                };
                foreach (var mNode in record["mechanics"].As<List<INode>>())
                {
                    game.Mechanics.Add(new Mechanic
                    {
                        Id = mNode.Properties["Id"].As<string>(),
                        Name = mNode.Properties["Name"].As<string>()
                    });
                }
                games.Add(game);
            });

            return games;
        }

        public async Task<IEnumerable<Game>> GetGamesByPublisherId(string publisherId)
        {
            List<Game> games = new List<Game>();
            await using var session = _driver.AsyncSession();
            var cursor = await session.RunAsync(@"
                MATCH (p:Publisher {Id:$publisherId})-[:PUBLISHED]->(g:Game)
                MATCH (g)<-[:CREATED]-(a:Author)
                WITH g, a, p
                MATCH (g)-[:HAS_MECHANIC]->(m:Mechanic)
                RETURN g, a, p, collect(m) AS mechanics
            ", new { publisherId });

            await cursor.ForEachAsync(record =>
            {
                var gNode = record["g"].As<INode>();
                var aNode = record["a"]?.As<INode>();
                var pNode = record["p"]?.As<INode>();
                Game game = new Game
                {
                    Id = gNode.Properties["Id"].As<string>(),
                    Title = gNode.Properties["Title"].As<string>(),
                    Description = gNode.Properties["Description"].As<string>(),
                    Difficulty = gNode.Properties["Difficulty"].As<float>(),
                    AvailableUnits = gNode.Properties["AvailableUnits"].As<float>(),
                    Mechanics = new List<Mechanic>(),
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
                        Name = pNode.Properties["Name"].As<string>(),
                        Country = pNode.Properties["Country"].As<string>()
                    }
                };
                foreach (var mNode in record["mechanics"].As<List<INode>>())
                {
                    game.Mechanics.Add(new Mechanic
                    {
                        Id = mNode.Properties["Id"].As<string>(),
                        Name = mNode.Properties["Name"].As<string>()
                    });
                }
                games.Add(game);
            });

            return games;
        }

        public async Task<IEnumerable<Game>> GetGamesByMechanicsId(string mechanicsId)
        {
            List<Game> games = new List<Game>();
            await using var session = _driver.AsyncSession();

            var cursor = await session.RunAsync(@"
                MATCH (m:Mechanic{Id:$mechanicsId})<-[:HAS_MECHANIC]-(g:Game) 
                OPTIONAL MATCH (g)<-[:CREATED]-(a:Author)
                OPTIONAL MATCH (g)<-[:PUBLISHED]-(p:Publisher)
                OPTIONAL MATCH (g)-[:HAS_MECHANIC]->(mech:Mechanic)
                RETURN g, a, p, collect(mech) AS mechanics
            ", new { mechanicsId });

            await cursor.ForEachAsync(record =>
            {
                var gNode = record["g"].As<INode>();
                var aNode = record["a"]?.As<INode>();
                var pNode = record["p"]?.As<INode>();

                Game game = null;
                game = new Game
                {
                    Id = gNode.Properties["Id"].As<string>(),
                    Title = gNode.Properties["Title"].As<string>(),
                    Description = gNode.Properties["Description"].As<string>(),
                    Difficulty = gNode.Properties["Difficulty"].As<float>(),
                    AvailableUnits = gNode.Properties["AvailableUnits"].As<float>(),
                    Mechanics = new List<Mechanic>(),
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
                        Name = pNode.Properties["Name"].As<string>(),
                        Country = pNode.Properties["Country"].As<string>()
                    }
                };
                foreach(var mechanic in record["mechanics"].As<List<INode>>())
                {
                    game.Mechanics.Add( new Mechanic
                    {
                        Id = mechanic.Properties["Id"].As<string>(),
                        Name = mechanic.Properties["Name"].As<string>()
                    });
                }
                games.Add(game) ;
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
