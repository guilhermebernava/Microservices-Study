using System.Net;

namespace RestaurantService.Http;

public class CheckItemHttp : ICheckItemHttp
{
    public CheckItemHttp(HttpClient httpClient, IConfiguration configuration)
    {
        HttpClient = httpClient;
        Configuration = configuration;
    }

    public HttpClient HttpClient { get; set; }
    public IConfiguration Configuration { get; set; }

    public async Task<bool> Send(int itemId)
    {
        var response = await HttpClient.GetAsync(Configuration["CheckItemUrl"] + $"?id={itemId}");
        return response.StatusCode == HttpStatusCode.OK;
    }
}
