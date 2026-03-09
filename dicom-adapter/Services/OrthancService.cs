

using System.Text;
using System.Text.Json;

namespace DicomAdapter.Services;

public class OrthancService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public OrthancService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<string> GetStudies()
    {
        var baseUrl = _config["Orthanc:BaseUrl"];
        var response = await _httpClient.GetAsync($"{baseUrl}/studies");

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> GetStudy(string id)
    {
        var baseUrl = _config["Orthanc:BaseUrl"];
        var response = await _httpClient.GetAsync($"{baseUrl}/studies/{id}");

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
    public async Task<string?> FindStudyIdByUID(string studyInstanceUid)
    {
        var body = new
        {
            Level = "Study",
            Query = new
            {
                StudyInstanceUID = studyInstanceUid
            }
        };

        var json = JsonSerializer.Serialize(body);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var baseUrl = _config["Orthanc:BaseUrl"];

        var response = await _httpClient.PostAsync($"{baseUrl}/tools/find", content);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsStringAsync();

        var ids = JsonSerializer.Deserialize<List<string>>(result);

        return ids?.FirstOrDefault();
    }
}