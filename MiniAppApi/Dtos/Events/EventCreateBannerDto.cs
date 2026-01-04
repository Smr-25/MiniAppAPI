using FluentValidation;

namespace MiniAppApi.Dtos.Events;

public class EventCreateBannerDto
{
    public IFormFile BannerImage { get; set; }
}

public class EventCreateBannerDtoValidator : AbstractValidator<EventCreateBannerDto>
{
    public EventCreateBannerDtoValidator()
    {
        RuleFor(e => e.BannerImage)
            .NotNull().WithMessage("Banner image is required.")
            .Must(file => file.Length > 0).WithMessage("Banner image cannot be empty.");
    }
}