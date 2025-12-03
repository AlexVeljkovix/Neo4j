using backend.Models;
namespace backend.Repos
{
    public interface IAuthorRepo
    {
        Task<IEnumerable<Author>> GetAllAuthors();
        Task<Author> GetAuthorById(string id);
        Task<IEnumerable<Author>> GetAuthorByName(string firstName, string lastName);
        Task<Author> CreateAuthor(Author author);
        Task<Author> UpdateAuthor(Author author);
        Task<Author> DeleteAuthor(string authorId);

        Task<Author>GetAuthorByGameId(string gameId);
    }
}
