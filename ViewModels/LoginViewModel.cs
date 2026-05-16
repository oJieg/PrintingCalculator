using System.ComponentModel.DataAnnotations;

namespace printing_calculator.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите имя пользователя")]
        [Display(Name = "Имя пользователя")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        //[Display(Name = "Запомнить меня")]
        //public bool RememberMe { get; set; }
    }
}
