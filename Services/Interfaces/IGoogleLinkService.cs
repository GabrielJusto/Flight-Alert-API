namespace Flight_Alert_API.Services.Interfaces;

public interface IGoogleLinkService
{
    Task<string> GetRedirectUrlAsync(string fullUrl);
}
