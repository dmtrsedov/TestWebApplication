using System.ComponentModel.DataAnnotations;

namespace TestWebApplication.Models
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Поле 'Код' обязательно для заполнения")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Поле 'Новый пароль' обязательно для заполнения")]
        [StringLength(100, ErrorMessage = "Пароль должен содержать минимум {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Пароль и подтверждение пароля не совпадают.")]
        public string ConfirmPassword { get; set; }
    }

}
