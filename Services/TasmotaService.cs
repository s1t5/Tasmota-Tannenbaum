// Services/TasmotaService.cs
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using WeihnachtsTannenbaum.Models;

namespace WeihnachtsTannenbaum.Services
{
    public class TasmotaService
    {
        private readonly TasmotaSettings _settings;
        private readonly HttpClient _httpClient;

        public TasmotaService(IOptions<TasmotaSettings> settings)
        {
            _settings = settings.Value;
            _httpClient = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes($"{_settings.Username}:{_settings.Password}");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        public async Task TogglePowerAsync()
        {
            var url = $"http://{_settings.IpAddress}/cm?cmnd=Power%20Toggle";
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();
        }
    }
}