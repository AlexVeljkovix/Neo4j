using backend.DTOs;
using backend.Models;
using backend.Repos;

namespace backend.Services
{
    public class AuthorService
    {
        private readonly IAuthorRepo _authorRepo;
        


        public AuthorService(IAuthorRepo authorRepo)
        {
            _authorRepo = authorRepo;
        }

        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            return await _authorRepo.GetAllAuthors();
        }

        public async Task<Author> GetAuthorById(string authorId)
        {
            return await _authorRepo.GetAuthorById(authorId);
        }

        public async Task<IEnumerable<Author>>GetAuthorByName(string firstName, string lastName)
        {
            return await _authorRepo.GetAuthorByName(firstName, lastName);
        }

        public async Task<Author>GetAuthorByGameId(string gameId)
        {
            return await _authorRepo.GetAuthorByGameId(gameId);
        }

        public async Task<Author> CreateAuthor(AuthorDTO author)
        {
            return await _authorRepo.CreateAuthor(new Author
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                Country = author.Country
            });
        }

        public async Task<Author> UpdateAuthor(string id, AuthorDTO author)
        {
            return await _authorRepo.UpdateAuthor(new Author
            {
                Id = id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Country = author.Country
            });
        }

        public async Task<Author> DeleteAuthor(string authorId)
        {
            return await _authorRepo.DeleteAuthor(authorId);
        }
    }
}
