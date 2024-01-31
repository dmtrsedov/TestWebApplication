using System.ComponentModel.DataAnnotations;

namespace TestWebApplication.Models
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Поле 'Email' обязательно для заполнения")]
        [EmailAddress(ErrorMessage = "Неправильный формат адреса электронной почты")]
        public string Email { get; set; }
    }
}
