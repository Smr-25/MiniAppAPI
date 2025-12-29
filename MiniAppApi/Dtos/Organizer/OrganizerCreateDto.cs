namespace MiniAppApi.Dtos.Organizer;

public class OrganizerCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string LogoUrl { get; set; } = string.Empty;
}
