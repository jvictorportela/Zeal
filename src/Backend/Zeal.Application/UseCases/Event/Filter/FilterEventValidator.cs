using FluentValidation;
using Zeal.Communication.Requests.Event;

namespace Zeal.Application.UseCases.Event.Filter;

public class FilterEventValidator : AbstractValidator<RequestFilterEventJson>
{
    // Implementar o ResourceMessagesExceptions para mensagens de erro mais amigáveis
    public FilterEventValidator()
    {
        // Se Name for informado, validar tamanho
        When(x => !string.IsNullOrWhiteSpace(x.Name), () =>
        {
            RuleFor(x => x.Name)
                .MaximumLength(100).WithMessage("O nome pode ter no máximo 100 caracteres.");
        });

        // Se houver datas informadas, validar cada uma
        When(x => x.StartDate.Any(), () =>
        {
            RuleForEach(x => x.StartDate)
                .Must(date => date != default)
                .WithMessage("Data de início inválida.");
        });

        // Se houver distâncias informadas, validar cada uma
        When(x => x.Distance.Any(), () =>
        {
            RuleForEach(x => x.Distance)
                .GreaterThan(0).WithMessage("A distância deve ser maior que zero.");
        });
    }
}
