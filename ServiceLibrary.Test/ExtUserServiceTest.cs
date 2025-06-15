using Castle.Core.Logging;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using ServiceLibrary.Inteface;
using ServiceLibrary.Models;
using ServiceLibrary.Services;

namespace ServiceLibrary.Test
{
    public class ExtUserServiceTest
    {
        private readonly Mock<IReqResClient> _clientMock;
      //  private readonly IMemoryCache _cache;
       // private readonly ILogger<ExtUserService> _logger;
        private readonly ExtUserService _service;
        private readonly IOptions<AppSettings> _settings;
        public ExtUserServiceTest()
        {
            _clientMock = new Mock<IReqResClient>();
           // _cache = new MemoryCache(new MemoryCacheOptions());
           // _logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<ExtUserService>();

            _settings = Options.Create(new AppSettings
            {
                PageSize = 6,
                BaseUrl = "https://reqres.in"
            });

            _service = new ExtUserService(_clientMock.Object);
        }
      
            [Fact]
            public async Task GetUserByIdAsync_ReturnsUser_WhenUserExists()
            {
                // Arrange
                var userId = 2;
                var expectedUser = new UserData
                {
                    Id = userId,
                    Email = "janet.weaver@reqres.in",
                    First_Name = "Janet",
                    Last_Name = "Weaver",
                    Avatar = "https://reqres.in/img/faces/2-image.jpg"
                };

                _clientMock.Setup(c => c.GetUserByIdAsync(userId)).ReturnsAsync(expectedUser);

                // Act
                var result = await _service.GetUserByIdAsync(userId);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(expectedUser.Email, result.Email);
            }
        }
    }
    
