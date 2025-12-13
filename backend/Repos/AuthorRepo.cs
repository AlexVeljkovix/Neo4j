using backend.Models;
using backend.Services;
using Neo4j.Driver;

namespace backend.Repos
{
    public class AuthorRepo : IAuthorRepo
    {
        private readonly IDriver _driver;

        public AuthorRepo(Neo4jDriverService driverService)
        {
            _driver = driverService.Driver;
        }

        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            List<Author> authors = new List<Author>();

            await using var session = _driver.AsyncSession();
            var cursor = await session.RunAsync("MATCH (a:Author) RETURN a");

            await cursor.ForEachAsync(record =>
            {
                var node = record["a"].As<INode>();
                authors.Add(new Author
                {
                    Id = node.Properties["Id"].As<string>(),
                    FirstName = node.Properties["FirstName"].As<string>(),
                    LastName = node.Properties["LastName"].As<string>(),
                    Country = node.Properties["Country"].As<string>(),
                });

            });
            return authors;
        }


        public async Task<Author> GetAuthorById(string id)
        {
            Author author = null;
            var gamesDict = new Dictionary<string, Game>();

            await using var session = _driver.AsyncSession();
            var cursor = await session.RunAsync(@"
                                MATCH (a:Author {Id:$id})
                                OPTIONAL MATCH (a)-[:CREATED]->(g:Game)
                                OPTIONAL MATCH (g)<-[:PUBLISHED]-(p:Publisher)
                                OPTIONAL MATCH (g)-[:HAS_MECHANIC]->(m:Mechanic)
                                RETURN a, g, p, collect(m) AS mechanics", 
                                new { id });

           

            await cursor.ForEachAsync(record =>
            {
                if (author == null)
                {
                    var aNode = record["a"].As<INode>();
                    author = new Author
                    {
                        Id = aNode.Properties["Id"].As<string>(),
                        FirstName = aNode.Properties["FirstName"].As<string>(),
                        LastName = aNode.Properties["LastName"].As<string>(),
                        Country = aNode.Properties["Country"].As<string>(),
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

                
                if (record["p"] is INode pNode)
                {
                    game.Publisher = new Publisher
                    {
                        Id = pNode.Properties["Id"].As<string>(),
                        Name = pNode.Properties["Name"].As<string>(),
                        Country = pNode.Properties["Country"].As<string>()
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

            if (author != null)
                author.Games = gamesDict.Values.ToList();

            return author;
        }

        public async Task<IEnumerable<Author>> GetAuthorByName(string firstName, string lastName)
        {

            await using var session = _driver.AsyncSession();

            var cursor = await session.RunAsync(@"
                                                MATCH (a:Author {FirstName:$firstName, LastName:$lastName})
                                                OPTIONAL MATCH (a)-[:CREATED]->(g:Game)
                                                OPTIONAL MATCH (g)<-[:PUBLISHED]-(p:Publisher)
                                                OPTIONAL MATCH (g)-[:HAS_MECHANIC]->(m:Mechanic)
                                                RETURN a, g, p, collect(m) AS mechanics",
                                                new { firstName, lastName });

            
            var authorsDict = new Dictionary<string, Author>();

            await cursor.ForEachAsync(record =>
            {
               
                var aNode = record["a"].As<INode>();
                string authorId = aNode.Properties["Id"].As<string>();

                
                if (!authorsDict.TryGetValue(authorId, out var author))
                {
                    author = new Author
                    {
                        Id = authorId,
                        FirstName = aNode.Properties["FirstName"].As<string>(),
                        LastName = aNode.Properties["LastName"].As<string>(),
                        Country = aNode.Properties["Country"].As<string>(),
                        Games = new List<Game>()
                    };

                    authorsDict.Add(authorId, author);
                }


                if (!(record["g"] is INode gNode))
                    return;

                
                string gameId = gNode.Properties["Id"].As<string>();
                var game = author.Games.FirstOrDefault(g => g.Id == gameId);
                if (game == null)
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

                    author.Games.Add(game);
                }

                
                if (record["p"] is INode pNode)
                {
                    game.Publisher = new Publisher
                    {
                        Id = pNode.Properties["Id"].As<string>(),
                        Name = pNode.Properties["Name"].As<string>(),
                        Country = pNode.Properties["Country"].As<string>()
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

            return authorsDict.Values.ToList();
        }


        public async Task<Author> CreateAuthor(Author author)
        {
            await using var session= _driver.AsyncSession();
            await session.RunAsync("CREATE (a:Author {Id:$Id, FirstName:$FirstName, LastName:$LastName, Country:$Country})",
                new
                {
                    author.Id,
                    author.FirstName,
                    author.LastName,
                    author.Country
                });

            return author;
        }

        public async Task<Author> UpdateAuthor(Author author)
        {
            await using var session = _driver.AsyncSession();
            await session.RunAsync(@"MATCH (a:Author {Id:$Id}) SET
                                            a.FirstName=$FirstName,
                                            a.LastName=$LastName,
                                            a.Country=$Country",
                                            new
                                            {
                                                author.Id,
                                                author.FirstName,
                                                author.LastName,
                                                author.Country
                                            });

            return author;
        }

        public async Task<Author> DeleteAuthor(string authorId)
        {
            Author author = await GetAuthorById(authorId);
            await using var session = _driver.AsyncSession();
            await session.RunAsync("MATCH (a:Author {Id:$authorId}) DETACH DELETE a",
                new
                {
                    authorId
                });
            return author;
        }

        public async Task<Author> GetAuthorByGameId(string gameId)
        {
            Author author = null;
            var gamesDict = new Dictionary<string, Game>();

            await using var session = _driver.AsyncSession();
            var cursor = await session.RunAsync(@"
                                MATCH (a:Author)-[:CREATED]->(g:Game {Id:$gameId})
                                OPTIONAL MATCH (g)<-[:PUBLISHED]-(p:Publisher)
                                OPTIONAL MATCH (g)-[:HAS_MECHANIC]->(m:Mechanic)
                                RETURN a, g, p, collect(m) AS mechanics",
                                new { gameId });



            await cursor.ForEachAsync(record =>
            {
                if (author == null)
                {
                    var aNode = record["a"].As<INode>();
                    author = new Author
                    {
                        Id = aNode.Properties["Id"].As<string>(),
                        FirstName = aNode.Properties["FirstName"].As<string>(),
                        LastName = aNode.Properties["LastName"].As<string>(),
                        Country = aNode.Properties["Country"].As<string>(),
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


                if (record["p"] is INode pNode)
                {
                    game.Publisher = new Publisher
                    {
                        Id = pNode.Properties["Id"].As<string>(),
                        Name = pNode.Properties["Name"].As<string>(),
                        Country = pNode.Properties["Country"].As<string>()
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

            if (author != null)
                author.Games = gamesDict.Values.ToList();

            return author;
        }
    }
}
