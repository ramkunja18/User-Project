using ServiceLibrary.Models;
using ServiceLibrary.Exceptions; // Ensure this namespace exists for ApiRequestException
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ServiceLibrary.Inteface;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace ServiceLibrary.Services
{
    public class ExtUserService : IExternalUserService
    {
        private readonly IReqResClient _client;
       // private readonly IMemoryCache _cache;
       // private readonly CacheSettings _cacheSettings;
      //  private readonly HttpClient _httpClient;
        public ExtUserService(IReqResClient client)
        {
            _client = client;
           // _cache = cache;
           // _cacheSettings = cacheSettings.Value;
           // _httpClient = httpClientFactory.CreateClient("ReqResClient");
        }

        public async Task<UserData?> GetUserByIdAsync(int userId)
        {
            

            try
            {
                var user = await _client.GetUserByIdAsync(userId);

                
                return user;
            }
            catch (HttpRequestException ex)
            {
                throw new ApiRequestException($"Network/server error while fetching user ID {userId}.", ex);
            }
            catch (JsonException ex)
            {
                throw new ApiRequestException($"Invalid JSON received for user ID {userId}.", ex);
            }
            catch (Exception ex)
            {
                throw new ApiRequestException($"Unexpected error while fetching user ID {userId}.", ex);
            }
        }


        public async Task<IEnumerable<UserData>> GetAllUsersAsync(int startPage)
        {

            var allUsers = new List<UserData>();
            int currentPage = startPage;
            UserPageResponse? response;

            try
            {
                do
                {
                    response = await _client.GetUsersAsync(currentPage);
                    if (response?.Data == null)
                    {
                        throw new ApiRequestException($"Null response received on page {currentPage}.");
                    }

                    allUsers.AddRange(response.Data);
                    currentPage++;
                }
                while (currentPage <= response.Total_Pages);
            }
            catch (UserNotFoundException)
            {
                throw; // allow known error to bubble up
            }
            catch (HttpRequestException ex)
            {
                throw new ApiRequestException("Failed to fetch users due to network/server error.", ex);
            }
            catch (JsonException ex)
            {
                throw new ApiRequestException("Invalid JSON structure returned by API.", ex);
            }
            catch (Exception ex)
            {
                throw new ApiRequestException("Unexpected error while fetching users.", ex);
            }

            return allUsers;
        }
    }
}
