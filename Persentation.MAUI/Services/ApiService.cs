using System.Net.Http.Json;
namespace Persentation.MAUI;
public class ApiService
{
    private readonly IHttpClientFactory _httpClientFactory;
    const string baseUrl = "http://localhost:5198";

    public ApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> OpenAccountAsync(OpenAccountCommand openAccountCommand)
    {

        var httpClient = _httpClientFactory.CreateClient();
        var response = await httpClient.PostAsJsonAsync($"{baseUrl}/accounts", openAccountCommand);
        return response.IsSuccessStatusCode;

    }

    public async Task<List<GetAllAccountsViewModel>> GetAllAccountsAsync()
    {
        var httpClient = _httpClientFactory.CreateClient();

        var response = await httpClient.GetFromJsonAsync<List<GetAllAccountsViewModel>>($"{baseUrl}/accounts");
        return response ?? new List<GetAllAccountsViewModel>();
    }

    public async Task<List<GetAllTransactionsViewModel>> GetAllTransactionAsync()
    {
        var httpClient = _httpClientFactory.CreateClient();

        var response = await httpClient.GetFromJsonAsync<List<GetAllTransactionsViewModel>>($"{baseUrl}/transactions");
        return response ?? new List<GetAllTransactionsViewModel>();
    }

    public async Task<bool> DraftTransfer(DraftTransferCommand draftTransferCommand)
    {

        var httpClient = _httpClientFactory.CreateClient();
        var response = await httpClient.PostAsJsonAsync($"{baseUrl}/transactions/draft", draftTransferCommand);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CommitTransfer(CommitTransferCommand commitTransferCommand)
    {

        var httpClient = _httpClientFactory.CreateClient();
        var response = await httpClient.PostAsJsonAsync($"{baseUrl}/transactions/commit", commitTransferCommand);
        return response.IsSuccessStatusCode;
    }
}
