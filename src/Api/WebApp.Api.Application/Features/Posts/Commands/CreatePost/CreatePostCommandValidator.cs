using FluentValidation;

namespace WebApp.Api.Application.Features.Posts.Commands.CreatePost
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
            RuleFor(p => p.Author)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Body)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            
            RuleFor(p => p.PostCategoryId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0);
        }
    }
}

