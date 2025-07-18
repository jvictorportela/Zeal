using FluentValidation;
using Zeal.Communication.Requests.Event;

namespace Zeal.Application.UseCases.Event;

public class EventValidator : AbstractValidator<RequestEventJson>
{
    // Criar ResourceMessages para manter o padrao de mensagens de erro
    public EventValidator()
    {
        // Nome do evento - obrigatório e com limite de caracteres
        RuleFor(e => e.Name)
            .NotEmpty().WithMessage("O nome do evento é obrigatório.")
            .MaximumLength(100).WithMessage("O nome do evento deve ter no máximo 100 caracteres.");

        // Descrição - opcional mas com limite de caracteres
        RuleFor(e => e.Description)
            .MaximumLength(3000).WithMessage("A descrição deve ter no máximo 3000 caracteres.");

        // Data de início do evento - deve ser no futuro
        RuleFor(e => e.StartDate)
            .NotEmpty().WithMessage("A data de início do evento é obrigatória.")
            .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
            .WithMessage("A data de início do evento deve ser hoje ou no futuro.");

        // Distância - deve ser positiva
        RuleFor(e => e.Distance)
            .GreaterThan(0).WithMessage("A distância da corrida deve ser maior que zero.");

        // Máximo de participantes - se informado, deve ser positivo
        RuleFor(e => e.MaxParticipants)
            .GreaterThan(0).When(e => e.MaxParticipants.HasValue)
            .WithMessage("O número máximo de participantes deve ser maior que zero.");

        // Idade mínima - se informada, não pode ser negativa
        RuleFor(e => e.MinimumAge)
            .GreaterThanOrEqualTo(0).When(e => e.MinimumAge.HasValue)
            .WithMessage("A idade mínima não pode ser negativa.");

        // Preço de inscrição - não pode ser negativo
        RuleFor(e => e.RegistrationPrice)
            .GreaterThanOrEqualTo(0).WithMessage("O preço de inscrição não pode ser negativo.");

        // Data de início das inscrições - deve ser antes do evento
        RuleFor(e => e.RegistrationStartDate)
            .NotEmpty().WithMessage("A data de início das inscrições é obrigatória.")
            .LessThan(e => e.StartDate)
            .WithMessage("As inscrições devem começar antes da data do evento.");

        // Data de término das inscrições - se informada, deve ser válida
        RuleFor(e => e.RegistrationEndDate)
            .GreaterThan(e => e.RegistrationStartDate).When(e => e.RegistrationEndDate.HasValue)
            .WithMessage("O término das inscrições deve ser após o início.")
            .LessThan(e => e.StartDate).When(e => e.RegistrationEndDate.HasValue)
            .WithMessage("As inscrições devem terminar antes do evento.");
    }
}
