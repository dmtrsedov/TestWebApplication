using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestWebApplication.Models
{
    /// <summary>
    /// Модель спринта.
    /// </summary>
    public class Sprint
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SprintID { get; set; }

        [Required(ErrorMessage = "Идентификатор проекта обязателен")]
        public int ProjectID { get; set; }

        [Required(ErrorMessage = "Дата начала спринта обязательна")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Дата окончания спринта обязательна")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Имя спринта обязательно")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Имя спринта должно содержать от 3 до 100 символов")]
        public string SprintName { get; set; }

        [StringLength(500, ErrorMessage = "Описание спринта не должно превышать 500 символов")]
        public string Description { get; set; }

        [StringLength(500, ErrorMessage = "Комментарий к спринту не должен превышать 500 символов")]
        public string? Comment { get; set; }

        [StringLength(500, ErrorMessage = "Файлы не должны превышать 500 символов")]
        public string? Files { get; set; }
        public Project? Project { get; set; }
        public ICollection<Task>? Tasks { get; set; }
    }
}
