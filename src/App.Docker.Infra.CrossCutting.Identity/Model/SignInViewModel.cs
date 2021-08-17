using System.ComponentModel.DataAnnotations;

namespace App.Docker.Infra.CrossCutting.Identity.Model
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Password { get; set; }
    }
}
