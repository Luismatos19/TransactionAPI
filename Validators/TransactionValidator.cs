using FluentValidation;
using TransactionAPI.Models;

public class TransactionValidator : AbstractValidator<Transaction>
{
    public TransactionValidator()
    {
        RuleFor(t => t.TransactionAmount)
            .GreaterThan(0).WithMessage("O valor da transação deve ser positivo.");

        RuleFor(t => t.TransactionCurrencyCode)
            .NotEmpty().WithMessage("Código da moeda é obrigatório.")
            .Length(3).WithMessage("Código da moeda deve ter 3 caracteres (ex: USD, BRL).");

        RuleFor(t => t.TransactionScenario)
            .NotEmpty().WithMessage("O cenário da transação não pode estar vazio.");
    }
}