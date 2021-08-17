using FluentValidation;
using System;
using System.Text.RegularExpressions;
using App.Docker.Domain.Commands.Products;

namespace App.Docker.Domain.Validations.Products
{
    public class UpdateProductCommandValidations : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidations()
        {
            When(p => !string.IsNullOrEmpty(p.Description), () =>
            {

                RuleFor(p => p.Description)
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo")
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio")
                .MinimumLength(10).WithMessage("O campo {PropertyName} deve ter pelo menos 10 caracteres")
                .MaximumLength(100).WithMessage("O campo {PropertyName} deve ter no máximo 100 caracteres");

            });
            

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("O campo {PropertyName} deve ser maior que zero");

            RuleFor(p => true)
                .Equal(p => IsGuid(p.Id.ToString())).WithMessage("Id do produto inválido");
        }

        private bool IsGuid(string guid)
        {
            if (guid == Guid.Empty.ToString()) return false;

            return Regex.IsMatch(guid, @"(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}");

        }
    }
}
