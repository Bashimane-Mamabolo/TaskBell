using TaskBell.Models;

namespace TaskBell.Repositories
{
    public interface IToDoItemRepository
    {
        Task<IEnumerable<ToDoItem>> GetAllAsync();
        Task<ToDoItem?> GetByIdAsync(int id);
        Task<ToDoItem> AddAsync(ToDoItem item);
        Task<ToDoItem> UpdateAsync(ToDoItem item);
        Task<bool> DeleteAsync(int id);
    }
}
