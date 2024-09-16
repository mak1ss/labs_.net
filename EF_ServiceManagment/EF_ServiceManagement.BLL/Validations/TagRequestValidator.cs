using EF_ServiceManagement.BLL.DTO.Tag;
using FluentValidation;

namespace EF_ServiceManagement.BLL.Validations
{
    public class TagRequestValidator : AbstractValidator<TagRequest>
    {
        public TagRequestValidator()
        {
            RuleFor(request => request.Name)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.Name)} can't be empty")
                .MaximumLength(50)
                .WithMessage(request => $"{nameof(request.Name)} length should be less than 50 symbols");
        }
    }
}
