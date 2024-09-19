using FluentValidation;
using System.ComponentModel.DataAnnotations;
using TechPotentialChallenge.Domain.Model;

namespace TechPotentialChallenge.Domain.Validate
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(x => x.NameSaler)
                .NotEmpty()
                .WithMessage("Name of the seller is required");

            RuleFor(x => x.CPFSaler)
                .Must(BeValidCPF)
                .WithMessage("Invalid CPF");

            RuleFor(x => x.EmailSaler)
                .Must(BeValidEmail)
                .WithMessage("Invalid Email");

            RuleFor(x => x.TelephoneSeler)
                .NotEmpty()
                .WithMessage("Invalid Telephone");

            RuleFor(x => x.OrderItem)
                .NotEmpty()
                .WithMessage("Order item is required");

            RuleForEach(x => x.OrderItem)
                .Must(BeValidOrderItem)
                .WithMessage("Invalid Order Item");
        }

        private static bool BeValidCPF(string cpf)
        {
            int[] multiplicador1 = [10, 9, 8, 7, 6, 5, 4, 3, 2];
            int[] multiplicador2 = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += (tempCpf[i] - '0') * multiplicador1[i];

            int resto = soma % 11;
            resto = (resto < 2) ? 0 : 11 - resto;
            string digito = resto.ToString();

            tempCpf += digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += (tempCpf[i] - '0') * multiplicador2[i];

            resto = soma % 11;
            resto = (resto < 2) ? 0 : 11 - resto;
            digito += resto.ToString();

            return cpf.EndsWith(digito);
        }

        private static bool BeValidEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }

        private static bool BeValidOrderItem(OrderItem item)
        {
            return item.Quantity > 0 && item.Price > 0 && !string.IsNullOrEmpty(item.Product);
        }
    }
}
