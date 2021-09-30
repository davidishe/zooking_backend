using Core.Dtos;
using FluentValidation;

namespace Zooking.Core.Validators
{
  public class MemberValidator : AbstractValidator<AssistantDto>
  {
    public MemberValidator()
    {
      RuleFor(item => item.Name)
        .Cascade(CascadeMode.Stop)
        .NotEmpty().WithMessage("Значение не должно быть пустым")
        .NotNull().WithMessage("Значение не должно быть пустым")
        .Length(3, 30).WithMessage("Длина значения должна быть от 3 до 30 символов");
    }

  }
}