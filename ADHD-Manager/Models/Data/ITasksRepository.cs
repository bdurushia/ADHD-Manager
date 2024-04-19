using Dapper;

namespace ADHD_Manager.Models.Data
{
    public interface ITasksRepository
    {
        public IEnumerable<Tasks> GetAllTasks();
        public Tasks GetTask(int taskId);
        public void InsertTasks(Tasks taskToInsert);
        public void UpdateTasks(Tasks task);
        public void DeleteTasks(Tasks task);

        public IEnumerable<Category> GetCategories();
        public Tasks AssignCategoryAndStatus();

        public IEnumerable<Status> GetStatus();
        public void UpdateStatusTasks(Tasks taskToUpdate);
        public void CompleteTasks(int id);
    }
}
