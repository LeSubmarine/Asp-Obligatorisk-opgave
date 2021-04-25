using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KanbanBoard.Data;
using KanbanBoard.Models;

namespace KanbanBoard.Utility
{
    public class KanbanTaskManager
    {
        private ApplicationDbContext _context;

        public KanbanTaskManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<KanbanTask> Tasks => new List<KanbanTask>(_context.KanbanTasks);

        public async Task CreateTaskAsync(KanbanTask task)
        {
            await _context.KanbanTasks.AddAsync(task);
        }

        public void DeleteTask(KanbanTask task)
        {
            _context.KanbanTasks.Remove(task);
        }
    }
}
