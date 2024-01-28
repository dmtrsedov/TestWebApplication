namespace TestWebApplication.Models
{
    /// <summary>
    /// Модель задачи.
    /// </summary>
    public class Task
    {
        public int TaskID { get; set; }
        public int SprintID { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public string Files { get; set; }
        public int AssignedUserID { get; set; }
        public Sprint Sprint { get; set; }
        public User AssignedUser { get; set; }
    }
}
