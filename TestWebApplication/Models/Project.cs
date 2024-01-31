using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestWebApplication.Models
{
    /// <summary>
    /// Модель проекта.
    /// </summary>
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectID { get; set; }

        [Required(ErrorMessage = "Имя проекта обязательно")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Имя проекта должно содержать от 3 до 100 символов")]
        public string ProjectName { get; set; }

        [StringLength(500, ErrorMessage = "Описание проекта не должно превышать 500 символов")]
        public string Description { get; set; }

        public List<Sprint>? Sprints { get; set; }
    }
}
