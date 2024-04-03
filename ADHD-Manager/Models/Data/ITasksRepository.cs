using Dapper;

namespace ADHD_Manager.Models.Data
{
    public interface ITasksRepository
    {
        public IEnumerable<Tasks> GetAllTasks();
        public Tasks GetTask(int taskId);
        public void InsertTasks(Tasks taskToInsert);
        public void UpdateTasks(Tasks task);
        public void DeleteTasks(int taskId);

        public IEnumerable<Category> GetCategories();
        public Tasks AssignCategory();

        public IEnumerable<Status> GetStatus();
        public Tasks UpdateStatus();
        public Tasks AssignStatus();
    }
}
