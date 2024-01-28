using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TestWebApplication.Models
{
    /// <summary>
    /// Модель пользователя
    /// </summary>
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Имя пользователя обязательно")]
        [Display(Name = "Введите имя")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Пароль обязателен")]
        [DataType(DataType.Password)]
        [Display(Name = "Введите пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите адрес электронной почты")]
        [EmailAddress(ErrorMessage = "Некорректный адрес электронной почты")]
        [Display(Name = "Введите почту")]
        public string Email { get; set; }

        // Role устанавливается по умолчанию в "User"
        public string Role { get; set; } = "User";

        // Навигационное свойство для связи с таблицей задач
        public List<Task>? AssignedTasks { get; set; }
    }
}
