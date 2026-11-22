using NoteKeeper.Models;
namespace NoteKeeper.Repositories.Interfaces;
public interface INotebookRepository
{
    Task<IEnumerable<Notebook>> GetAllAsync(string userId);

    Task<Notebook?> GetByIdAsync(int id, string userId);

    Task<int> CreateAsync(Notebook notebook);

    Task<bool> UpdateAsync(Notebook notebook);

    Task<bool> DeleteAsync(int id, string userId);
}
