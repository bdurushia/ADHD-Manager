using ADHD_Manager.Models;
using ADHD_Manager.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace ADHD_Manager.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITasksRepository repo;

        public TasksController(ITasksRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Index(string categoryFilter, string statusFilter)
        {
            var tasks = repo.GetAllTasks();

            if (!string.IsNullOrEmpty(categoryFilter))
            {
                int categoryId = int.Parse(categoryFilter);
                tasks = tasks.Where(t => t.CategoryId == categoryId);
            }

            if (!string.IsNullOrEmpty(statusFilter))
            {
                int statusId = int.Parse(statusFilter);
                tasks = tasks.Where(t => t.StatusId == statusId);
            }

            return View(tasks);
        }

        public IActionResult ViewTasks(int id)
        {
            var task = repo.GetTask(id);
            return View(task);
        }

        public IActionResult UpdateTasks(int id)
        {
            Tasks task = repo.GetTask(id);

            var statusList = repo.GetStatus();
            var categoriesList = repo.GetCategories();

            task.Categories = categoriesList;
            task.Statuses = statusList;


            if (task == null)
            {
                return View("Error: TaskNotFound");
            }
            return View(task);
        }

        public IActionResult UpdateTaskToDatabase(Tasks task)
        {
            repo.UpdateTasks(task);
            return RedirectToAction("ViewTasks", new { id = task.TaskID });
        }

        public IActionResult InsertTasks()
        {
            var newTask = repo.AssignCategoryAndStatus();
            return View(newTask);
        }

        public IActionResult InsertTaskToDatabase(Tasks taskToInsert)
        {
            repo.InsertTasks(taskToInsert);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteTasks(Tasks task)
        {
            repo.DeleteTasks(task);
            return RedirectToAction("Index");
        }
    }
}
