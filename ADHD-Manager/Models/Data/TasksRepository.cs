using System.Collections.Generic;
using Dapper;
using System.Data;

namespace ADHD_Manager.Models.Data
{
    public class TasksRepository : ITasksRepository
    {
        private readonly IDbConnection _conn;
        public TasksRepository(IDbConnection conn)
        {
            _conn = conn;
        }


        public IEnumerable<Tasks> GetAllTasks()
        {
            // return _conn.Query<Tasks>("SELECT * FROM tasks JOIN status ON tasks.StatusID = status.StatusID;");
            return _conn.Query<Tasks>("SELECT * FROM tasks;");
        }

        public Tasks GetTask(int taskId)
        {
            return _conn.QuerySingle<Tasks>("SELECT * FROM tasks WHERE TaskID = @taskId;", new { taskId = taskId });
        }

        public void UpdateTasks(Tasks task)
        {
            _conn.Execute("UPDATE tasks SET Title = @title, Description = @description, " +
                          "DueDate = @dueDate, CategoryID = @categoryId, StatusID = @statusId WHERE TaskID = @taskId", 
                          new { title = task.Title, description = task.Description, dueDate = task.DueDate, 
                              categoryId = task.CategoryId, statusId = task.StatusId, taskId = task.TaskID });
        }
        public void InsertTasks(Tasks taskToInsert)
        {
            _conn.Execute("INSERT INTO tasks (Title, Description, DueDate, CategoryID, StatusID) " +
                          "VALUES (@title, @description, @dueDate, @categoryId, @statusId);", 
                          new { title = taskToInsert.Title, description = taskToInsert.Description,
                          dueDate = taskToInsert.DueDate, categoryId = taskToInsert.CategoryId, statusId = taskToInsert.StatusId });
        }


        public IEnumerable<Category> GetCategories()
        {
            return _conn.Query<Category>("SELECT * FROM category;");
        }
        public IEnumerable<Status> GetStatus()
        {
            return _conn.Query<Status>("SELECT * FROM status");
        }
        public Tasks AssignCategoryAndStatus()
        {
            var categoryList = GetCategories();
            var statusList = GetStatus();
            var task = new Tasks();

            task.Categories = categoryList;
            task.Statuses = statusList;

            return task;
        }

        public void DeleteTasks(Tasks task)
        {
            _conn.Execute("DELETE FROM tasks WHERE TaskID = @taskId", new { taskId = task.TaskID });
        }
    }
}
