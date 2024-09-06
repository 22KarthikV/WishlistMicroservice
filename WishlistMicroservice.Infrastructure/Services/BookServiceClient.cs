using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WishlistMicroservice.Infrastructure.Services
{
    public interface IBookServiceClient
    {
        Task<BookInfo> GetBookInfoAsync(Guid bookId);
    }

    public class BookServiceClient : IBookServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _bookServiceBaseUrl;

        public BookServiceClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _bookServiceBaseUrl = configuration["BookService:BaseUrl"];
        }

        public async Task<BookInfo> GetBookInfoAsync(Guid bookId)
        {
            var response = await _httpClient.GetAsync($"{_bookServiceBaseUrl}/api/books/{bookId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to retrieve book info for book ID: {bookId}");
            }

            return await response.Content.ReadFromJsonAsync<BookInfo>();
        }
    }

    public class BookInfo
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public double AverageRating { get; set; }
        public int StockCount { get; set; }
    }
}