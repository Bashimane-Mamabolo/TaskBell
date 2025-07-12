using Microsoft.EntityFrameworkCore;
using TaskBell.Data;
using TaskBell.Models;

namespace TaskBell.Repositories
{
    public class ToDoItemRepository : IToDoItemRepository
    {
        private readonly ToDoContext _context;
        public ToDoItemRepository(ToDoContext context) 
        {
            _context = context;
        }

        public async Task<ToDoItem> AddAsync(ToDoItem item)
        {
            var addedItem = _context.ToDoItems.Add(item);
            await _context.SaveChangesAsync();
            return addedItem.Entity;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.ToDoItems.FindAsync(id);
            if (item == null)
            {
                throw new KeyNotFoundException($"ToDoItem with ID {id} not found.");
            }
            _context.ToDoItems.Remove(item);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<ToDoItem>> GetAllAsync()
        {
            var items = await _context.ToDoItems.ToListAsync();
            //if (items == null || !items.Any())
            //{
            //    throw new KeyNotFoundException("No ToDoItems found.");
            //}
            return items;
        }

        public async Task<ToDoItem?> GetByIdAsync(int id)
        {
            var item = await _context.ToDoItems.FindAsync(id);
            //if (item == null)
            //{
            //    throw new KeyNotFoundException($"ToDoItem with ID {id} not found.");
            //}
            return item;

        }

        public async Task<ToDoItem> UpdateAsync(ToDoItem item)
        {
            var existingItem = await _context.ToDoItems.FindAsync(item.Id);
            if (existingItem == null)
            {
                throw new KeyNotFoundException($"ToDoItem with ID {item.Id} not found.");
            }
            // Update the properties of the existing item
            _context.Entry(existingItem).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();

            //_context.Update(item);
            //await _context.SaveChangesAsync();


            return existingItem;

        }
    }
}
