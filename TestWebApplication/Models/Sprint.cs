namespace TestWebApplication.Models
{
    /// <summary>
    /// Модель спринта.
    /// </summary>
    public class Sprint
    {
        public int SprintID { get; set; }
        public int ProjectID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string SprintName { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public string Files { get; set; }

        // Навигационные свойства для связи с таблицами проектов и задач
        public Project Project { get; set; }
        public List<Task> Tasks { get; set; }
    }
}
