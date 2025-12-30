namespace MiniAppApi.Utils;

public class FileManager(IWebHostEnvironment _env)
{ 
    public async Task<string> SaveEventBannerAsync(IFormFile file)
    {
        return await SaveFileAsync(file, "uploads/events");
    }

    public async Task<string> SaveOrganizerLogoAsync(IFormFile file)
    {
        return await SaveFileAsync(file, "uploads/organizers");
    }

    private async Task<string> SaveFileAsync(IFormFile file, string folder)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File is null or empty");

        var uploadsPath = Path.Combine(_env.WebRootPath, folder);
        Directory.CreateDirectory(uploadsPath);

        var extension = Path.GetExtension(file.FileName);
        var fileName = $"{Guid.NewGuid()}{extension}";
        var fullPath = Path.Combine(uploadsPath, fileName);

        using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);

        return $"{folder}/{fileName}".Replace("\\", "/");
    }
}
