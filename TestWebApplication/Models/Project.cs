namespace TestWebApplication.Models
{
    /// <summary>
    /// Модель проекта.
    /// </summary>
    public class Project
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }

        // Навигационное свойство для связи с таблицей спринтов
        public List<Sprint> Sprints { get; set; }
    }
}
