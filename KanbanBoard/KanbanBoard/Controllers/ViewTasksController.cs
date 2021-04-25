using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KanbanBoard.Data;
using KanbanBoard.Models;
using KanbanBoard.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace KanbanBoard.Controllers
{
    [Authorize(Roles = "Organizer,TeamPlayer,Contributer,Observer")]
    public class ViewTasksController : Controller
    {
        private ApplicationDbContext _context;
        private SignInManager<IdentityUser> _signInContext;
        public ViewTasksController(ApplicationDbContext context, SignInManager<IdentityUser> signInContext)
        {
            _context = context;
            _signInContext = signInContext;
        }

        public KanbanTaskManager TaskManager => new KanbanTaskManager(_context);

        public IActionResult Index()
        {
            return View(TaskManager);
        }

        public IActionResult Edit(KanbanTask task)
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "Team Player,Contributor,Organizer,UltraAdmin")]
        public IActionResult Delete(KanbanTask task)
        {
            if(task.OwnerRefId ==)
            throw new NotImplementedException();
        }
    }
}
