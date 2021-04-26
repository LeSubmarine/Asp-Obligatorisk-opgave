using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using KanbanBoard.Data;
using KanbanBoard.Models;
using KanbanBoard.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KanbanBoard.Controllers
{
    [Authorize(Roles = "Organizer,Team Player,Contributor,Observer,UltraAdmin")]
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

        [Authorize(Roles = "Team Player,Contributor,Organizer,UltraAdmin")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Users = _context.Users.ToArray();
            return View(new KanbanTask());
        }

        [Authorize(Roles = "Team Player,Contributor,Organizer,UltraAdmin")]
        [HttpPost]
        public IActionResult Create([FromForm] KanbanTask task)
        {
            task.OwnerRefId =
                _signInContext.UserManager.GetUserId(User);
            task.Owner = _context.Users.Find(task.OwnerRefId);
            TaskManager.CreateTaskAsync(task);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Team Player,Contributor,Organizer,UltraAdmin")]
        [HttpGet]
        public IActionResult Edit(int taskId)
        {
            ViewBag.Users = _context.Users.ToArray();

            if (!(User.IsInRole("Organizer") || (User.IsInRole("Team Player"))))
            {
                if (_signInContext.UserManager.GetUserId(User) == TaskManager.Tasks.Where(a => a.Id == taskId)?.First()?.OwnerRefId)
                {
                    return View(TaskManager.Tasks.Find(t => t.Id == taskId));
                }
                return RedirectToAction("Index");
            }


            return View(TaskManager.Tasks.Find(t => t.Id == taskId));
        }

        [Authorize(Roles = "Team Player,Contributor,Organizer,UltraAdmin")]
        [HttpPost]
        public IActionResult Edit([FromForm] KanbanTask task)
        {
            task.ResponsibleUser = _context.Users.Find(task.ResponsibleUser?.Id) ?? task.ResponsibleUser;
            if (!(User.IsInRole("Organizer") || (User.IsInRole("Team Player"))))
            {
                if (_signInContext.UserManager.GetUserId(User) != TaskManager.Tasks.Where(a => a.Id == task.Id)?.First()?.OwnerRefId)
                {
                    return RedirectToAction("Index");
                }
            }

            KanbanTask oldTask = TaskManager.Tasks.Find(t => t.Id == task.Id);
            foreach (var prop in oldTask.GetType().GetProperties())
            {
                prop.SetValue(oldTask, prop.GetValue(task));
            }

            TaskManager.UpdateTask(oldTask);

            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Team Player,Contributor,Organizer,UltraAdmin")]
        public IActionResult Delete(int taskId)
        {
            if (!(User.IsInRole("Organizer") || (User.IsInRole("Team Player"))))
            {
                if (_signInContext.UserManager.GetUserId(User) == TaskManager.Tasks.Where(a => a.Id == taskId)?.First()?.OwnerRefId)
                {
                    TaskManager.DeleteTask(TaskManager.Tasks.Find(a => a.Id == taskId));
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
            TaskManager.DeleteTask(TaskManager.Tasks.Find(a=>a.Id == taskId));
            return RedirectToAction("Index");
        }
    }
}
