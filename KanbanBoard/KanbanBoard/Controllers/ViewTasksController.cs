using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KanbanBoard.Data;
using KanbanBoard.Utility;
using Microsoft.AspNetCore.Authorization;

namespace KanbanBoard.Controllers
{
    [Authorize(Roles = "Organizer,TeamPlayer,Contributer,Observer")]
    public class ViewTasksController : Controller
    {
        private ApplicationDbContext _context;
        public ViewTasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        public KanbanTaskManager TaskManager => new KanbanTaskManager(_context);

        public IActionResult Index()
        {
            return View(TaskManager);
        }
    }
}
