using backend.Models;
namespace backend.Repos
{
    public interface IPublisherRepo
    {
        Task<IEnumerable<Publisher>> GetAllPublishers();
        Task<Publisher> GetPublisherById(string id);
        Task<IEnumerable<Publisher>> GetPublisherByName(string name);
        Task<Publisher> CreatePublisher(Publisher publisher);
        Task<Publisher> UpdatePublisher(Publisher publisher);
        Task<Publisher> DeletePublisher(string publisherId);
        Task<Publisher> GetPublisherByGameId(string gameId);

    }
}
