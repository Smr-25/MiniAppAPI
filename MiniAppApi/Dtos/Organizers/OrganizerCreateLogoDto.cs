using FluentValidation;

namespace MiniAppApi.Dtos.Organizers;

public class OrganizerCreateLogoDto
{
    public IFormFile LogoImage { get; set; }
}

public class OrganizerCreateLogoDtoValidator : AbstractValidator<OrganizerCreateLogoDto>
{
    public OrganizerCreateLogoDtoValidator()
    {
        RuleFor(o => o.LogoImage)
            .NotNull().WithMessage("Logo image is required.")
            .Must(file => file.Length > 0).WithMessage("Logo image cannot be empty.");
    }
}