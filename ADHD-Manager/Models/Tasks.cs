using System.Collections.Generic;

namespace ADHD_Manager.Models
{
    public class Tasks
    {
        public int TaskID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public int StatusId { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<Status> Statuses { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
