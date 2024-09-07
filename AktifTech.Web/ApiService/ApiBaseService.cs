using AktifTech.Constant;
using System.Text;
using System.Text.Json;

namespace AktifTech.Web.ApiService
{
    public abstract class ApiBaseService
    {
        private readonly HttpClient _httpClient;
        //private readonly string _baseUrl; //di string gibi primitive türleri inject edemiyor, sonra bakılacak
        public ApiBaseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResultSet> DeleteAsync(int id, string _baseUrl)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{_baseUrl}/{id}");
            var response = await _httpClient.SendAsync(request);
            var statusCode = response.StatusCode;
            var message = await response.Content.ReadAsStringAsync();

            //return new ResultSet
            //{
            //    Result = Result.Success,
            //    Message = message
            //};

            return JsonSerializer.Deserialize<ResultSet>(message);
        }

        public async Task<T?> GetAsync<T>(int id, string _baseUrl)
        {
            //using var http = new HttpClient();
            //var request = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}/{id}");
            ////request.Headers.Add("Authorization", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyTmFtZSI6InNha2FuQGdtYWlsLmNvbSIsIm5iZiI6MTcyNTY1NDQwMiwiZXhwIjoxNzI1Njg0NDAyLCJpc3MiOiJJc3N1ZXJJbmZvcm1hdGlvbiIsImF1ZCI6IkF1ZGllbmNlSW5mb3JtYXRpb24ifQ.jhrVjjmUiN9g8BsFYNPciGoyBOaC3Z0zO8kGP6y6cP4");

            //var Response = await http.SendAsync(request);
            //var StatusCode = Response.StatusCode;
            //var Message = await Response.Content.ReadAsStringAsync();


            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/{id}");
            var response = await _httpClient.SendAsync(request);
            var statusCode = response.StatusCode;
            var message = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(message);
        }

        public async Task<List<T>> GetListAsync<T>(string _baseUrl)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _baseUrl);
            var response = await _httpClient.SendAsync(request);
            var statusCode = response.StatusCode;
            var message = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<T>>(message);
        }

        public async Task<ResultSet> SaveAsync<T>(T entity, string _baseUrl)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _baseUrl);
            request.Content = new StringContent(JsonSerializer.Serialize(entity), Encoding.UTF8, "application/json");
            var response = await _httpClient.SendAsync(request);
            var statusCode = response.StatusCode;
            var message = await response.Content.ReadAsStringAsync();

            //return new ResultSet
            //{
            //    Result = Result.Success,
            //    Message = message
            //};
            return JsonSerializer.Deserialize<ResultSet>(message);
        }

        public async Task<ResultSet> UpdateAsync<T>(T entity, string _baseUrl)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, _baseUrl);
            request.Content = new StringContent(JsonSerializer.Serialize(entity), Encoding.UTF8, "application/json");
            var response = await _httpClient.SendAsync(request);
            var statusCode = response.StatusCode;
            var message = await response.Content.ReadAsStringAsync();

            //return new ResultSet
            //{
            //    Result = Result.Success,
            //    Message = message
            //};
            return JsonSerializer.Deserialize<ResultSet>(message);
        }
    }
}
