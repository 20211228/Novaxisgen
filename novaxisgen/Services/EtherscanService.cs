using System.Net.Http.Json;
using System.Text.Json;

namespace Novaxisgen.Services
{
    public class EtherscanService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _networkUrl;

        public EtherscanService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _apiKey = config["Blockchain:ApiKey"];
            _networkUrl = config["Blockchain:Network"]; // ví dụ: https://api.bscscan.com/v2/api
        }

        /// <summary>
        /// Lấy danh sách giao dịch của ví (chuẩn Etherscan V2)
        /// </summary>
        public async Task<JsonDocument?> GetTransactionHistoryAsync(string walletAddress)
        {
            var requestBody = new
            {
                id = 1,
                jsonrpc = "2.0",
                method = "account.txlist",
                @params = new
                {
                    address = walletAddress,
                    startblock = 0,
                    endblock = 99999999,
                    sort = "desc",
                    apikey = _apiKey
                }
            };

            var response = await _httpClient.PostAsJsonAsync(_networkUrl, requestBody);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"❌ Lỗi kết nối Etherscan: {response.StatusCode}");
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            return JsonDocument.Parse(json);
        }
    }
}
