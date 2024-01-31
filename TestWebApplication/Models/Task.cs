using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestWebApplication.Models
{
    /// <summary>
    /// Модель задачи.
    /// </summary>
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskID { get; set; }

        [Required(ErrorMessage = "Идентификатор спринта обязателен")]
        public int SprintID { get; set; }

        [Required(ErrorMessage = "Имя задачи обязательно")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Имя задачи должно содержать от 3 до 100 символов")]
        public string TaskName { get; set; }

        [StringLength(500, ErrorMessage = "Описание задачи не должно превышать 500 символов")]
        public string? Description { get; set; }

        [StringLength(50, ErrorMessage = "Статус задачи не должен превышать 50 символов")]
        public string Status { get; set; }

        [StringLength(500, ErrorMessage = "Комментарий к задаче не должен превышать 500 символов")]
        public string? Comment { get; set; }

        [StringLength(500, ErrorMessage = "Файлы не должны превышать 500 символов")]
        public string? Files { get; set; }

        [Required(ErrorMessage = "Идентификатор назначенного пользователя обязателен")]
        public int AssignedUserID { get; set; }
        // Навигационные свойства
        public Sprint? Sprint { get; set; }
        public User? AssignedUser { get; set; }
    }
}
