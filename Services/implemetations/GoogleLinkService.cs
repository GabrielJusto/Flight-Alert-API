using System.Text.RegularExpressions;

using Flight_Alert_API.Services.Interfaces;

namespace Flight_Alert_API.Services.implemetations;

public partial class GoogleLinkService : IGoogleLinkService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<GoogleLinkService> _logger;

    public GoogleLinkService(IHttpClientFactory httpClientFactory, ILogger<GoogleLinkService> logger)
    {
        _httpClient = httpClientFactory.CreateClient();
        _logger = logger;
    }

    public async Task<string> GetRedirectUrlAsync(string fullUrl)
    {
        try
        {
            _logger.LogInformation("Requesting redirect URL from Google: {FullUrl}", fullUrl);


            HttpResponseMessage response = await _httpClient.GetAsync(fullUrl);
            response.EnsureSuccessStatusCode();


            string htmlContent = await response.Content.ReadAsStringAsync();


            Match match = MyRegex().Match(htmlContent);

            if(match.Success && match.Groups.Count > 1)
            {
                string redirectUrl = match.Groups[1].Value;
                _logger.LogInformation("Successfully extracted redirect URL: {RedirectUrl}", redirectUrl);
                return redirectUrl;
            }

            _logger.LogWarning("Could not find redirect URL in HTML response from {FullUrl}", fullUrl);
            throw new InvalidOperationException("Redirect URL not found in Google response HTML");
        }
        catch(HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request failed while getting redirect URL from {FullUrl}", fullUrl);
            throw;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error occurred while processing redirect URL from {FullUrl}", fullUrl);
            throw;
        }
    }

    [GeneratedRegex(@"content=[""']0;url='([^']+)'[""']", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex MyRegex();
}
