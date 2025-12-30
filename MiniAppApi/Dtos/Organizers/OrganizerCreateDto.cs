using FluentValidation;

namespace MiniAppApi.Dtos.Organizer;

public class OrganizerCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string LogoUrl { get; set; } = string.Empty;
}

public class OrganizerCreateDtoValidator : AbstractValidator<OrganizerCreateDto>
{
    public OrganizerCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.");

        RuleFor(x => x.Phone)
            .MaximumLength(200).WithMessage("Phone number cannot exceed 15 characters.");

        // RuleFor(x => x.LogoUrl)
        //     .NotEmpty().WithMessage("Logo URL is required.")
        //     .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
        //     .WithMessage("A valid Logo URL is required.");
    }
}