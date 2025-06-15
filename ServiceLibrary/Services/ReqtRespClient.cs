using ServiceLibrary.Inteface;
using ServiceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceLibrary.Services
{
    public class ReqResClient : IReqResClient
    {
        private readonly HttpClient _httpClient;

        public ReqResClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserPageResponse> GetUsersAsync(int page)
        {
         
            var requestUri = $"users?page={page}";
            Console.WriteLine("Requesting: " + _httpClient.BaseAddress + requestUri);

            var response = await _httpClient.GetAsync(requestUri);
          

            Console.WriteLine($"Status: {response.StatusCode}");
          

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                var userResponse = JsonSerializer.Deserialize<UserPageResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return userResponse!;
                
            }
            else
            {
               return  new UserPageResponse() { StatusCode = (int)response.StatusCode };

            }
        }

        public async Task<UserData?> GetUserByIdAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"users/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                var userResponse = JsonSerializer.Deserialize<SingleUserResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return userResponse?.Data;
            }
            else
            {
                Console.WriteLine($"Failed to fetch user. Status code: {(int)response.StatusCode}");
                return null;
            }
           
        }
    }
}
