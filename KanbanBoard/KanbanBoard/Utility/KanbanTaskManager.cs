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
            Tasks = new List<KanbanTask>(context.KanbanTasks);
            foreach (var task in Tasks)
            {
                if (task.ResponsibleUser is null && !(task.ResponsibleUserRefId is null))
                {
                    task.ResponsibleUser = context.Users.Find(task.ResponsibleUserRefId);
                }
                if (task.Owner is null && !(task.OwnerRefId is null))
                {
                    task.Owner = context.Users.Find(task.OwnerRefId);
                }
            }
        }

        public List<KanbanTask> Tasks { get; set; }

        public async Task CreateTaskAsync(KanbanTask task)
        {
            await _context.KanbanTasks.AddAsync(task);
            _context.SaveChanges();
        }

        public void DeleteTask(KanbanTask task)
        {
            _context.KanbanTasks.Remove(task);
            _context.SaveChanges();
        }
    }
}
