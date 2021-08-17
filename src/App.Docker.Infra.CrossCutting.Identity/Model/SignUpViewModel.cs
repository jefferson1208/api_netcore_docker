using System.ComponentModel.DataAnnotations;

namespace App.Docker.Infra.CrossCutting.Identity.Model
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(30, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(255, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string FullName { get; set; }

        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "As senhas não conferem!")]
        public string PasswordConfirmation { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido!")]
        public string Email { get; set; }

    }
}
