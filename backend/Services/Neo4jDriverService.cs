using Neo4j.Driver;

namespace backend.Services
{
    public class Neo4jDriverService:IDisposable
    {
        public IDriver Driver { get; }
        public Neo4jDriverService(string uri, string user,string password)
        {
            Driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
        }

        public void Dispose() { Driver.Dispose(); }
    }
}
