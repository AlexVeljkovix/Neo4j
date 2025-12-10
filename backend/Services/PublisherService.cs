using backend.DTOs;
using backend.Models;
using backend.Repos;

namespace backend.Services
{
    public class PublisherService
    {
        public readonly IPublisherRepo _publisherRepo;

        public PublisherService(IPublisherRepo publisherRepo)
        {
            _publisherRepo = publisherRepo;
        }

        public async Task<IEnumerable<Publisher>> GetAllPublishers()
        {
            return await _publisherRepo.GetAllPublishers();
        }

        public async Task<Publisher> GetPublisherById(string publisherId)
        {
            return await _publisherRepo.GetPublisherById(publisherId);
        }

        public async Task<IEnumerable<Publisher>> GetPublisherByName(string name)
        {
            return await _publisherRepo.GetPublisherByName(name);
        }

        public async Task<Publisher>GetPublisherByGameId(string gameId)
        {
            return await _publisherRepo.GetPublisherByGameId(gameId);
        }

        public async Task<Publisher> CreatePublisher(PublisherDTO publisher)
        {
            return await _publisherRepo.CreatePublisher(new Publisher
            {
                Name = publisher.Name,
                Country= publisher.Country,
            });
        }

        public async Task<Publisher> UpdatePublisher(string id, PublisherDTO publisherer)
        {
            return await _publisherRepo.UpdatePublisher(new Publisher
            {
                Id= id,
                Name= publisherer.Name,
                Country= publisherer.Country,
            });
        }

        public async Task<Publisher> DeletePublisher(string publisherId)
        {
            return await _publisherRepo.DeletePublisher(publisherId);
        }
    }
}
