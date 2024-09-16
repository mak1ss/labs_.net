
using EF_ServiceManagement.BLL.DTO.Category;
using FluentValidation;

namespace EF_ServiceManagement.BLL.Validations
{
    public class CategoryRequestValidator : AbstractValidator<CategoryRequest>
    {
        public CategoryRequestValidator()
        {
            RuleFor(request => request.Name)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.Name)} can't be empty")
                .MaximumLength(50)
                .WithMessage(request => $"{nameof(request.Name)} length should be less than 50 symbols");

            RuleFor(request => request.Description)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.Description)} can't be empty")
                .MaximumLength(255)
                .WithMessage(request => $"{nameof(request.Description)} length should be less than 255 symbols");
        }
    }
}
