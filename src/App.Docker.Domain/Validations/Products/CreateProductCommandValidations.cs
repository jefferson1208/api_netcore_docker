using FluentValidation;
using App.Docker.Domain.Commands.Products;

namespace App.Docker.Domain.Validations.Products
{
    public class CreateProductCommandValidations: AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidations()
        {
            RuleFor(p => p.Name)
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo")
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio")
                .MinimumLength(5).WithMessage("O campo {PropertyName} deve ter pelo menos 5 caracteres")
                .MaximumLength(30).WithMessage("O campo {PropertyName} deve ter no máximo 30 caracteres");

            RuleFor(p => p.Description)
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo")
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio")
                .MinimumLength(10).WithMessage("O campo {PropertyName} deve ter pelo menos 10 caracteres")
                .MaximumLength(100).WithMessage("O campo {PropertyName} deve ter no máximo 100 caracteres");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("O campo {PropertyName} deve ser maior que zero");
        }
    }
}
