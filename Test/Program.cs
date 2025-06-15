using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using ServiceLibrary.Inteface;
using ServiceLibrary.Models;
using ServiceLibrary.Services;
using System.Net.Http.Headers;



var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        string baseUrl = context.Configuration["ReqResApi:BaseUrl"]!;


        services.AddHttpClient<IReqResClient, ReqResClient>(client =>
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; MyApp/1.0)");
        });
        //services.AddHttpClient("ReqResClient", client =>
        //{
        //    client.BaseAddress = new Uri(baseUrl!);
        //    client.DefaultRequestHeaders.Add("Accept", "application/json");
        //})
        //.AddPolicyHandler(GetRetryPolicy());
        services.AddScoped<IExternalUserService, ExtUserService>();
       // services.Configure<CacheSettings>(context.Configuration.GetSection("CacheSettings"));
       // services.AddMemoryCache();
    })
    .Build();

using var scope = host.Services.CreateScope();
var userService = scope.ServiceProvider.GetRequiredService<IExternalUserService>();

try
{
    Console.WriteLine("Fetching all users...");
    var allUsers = await userService.GetAllUsersAsync(1);

    foreach (var user in allUsers)
    {
        var fullName = $"{user.First_Name} {user.Last_Name}";
        Console.WriteLine($"{user.Id} name: {fullName}, email:{user.Email}, avatar: {user.Avatar}");
    }

    Console.WriteLine("\nFetching user with ID 2...");
    var singleUser = await userService.GetUserByIdAsync(2);
    if (singleUser != null)
    {
        var fullName = $"{singleUser.First_Name} {singleUser.Last_Name}";
        Console.WriteLine($"{singleUser.Id}:  name: {fullName}, email: {singleUser.Email}, avatar: {singleUser.Avatar}");
    }

    else
    {
        Console.WriteLine("User not found.");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

//static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
//{
//    return HttpPolicyExtensions
//        .HandleTransientHttpError()
//        .WaitAndRetryAsync(
//            3,
//            retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), // 2s, 4s, 8s
//            onRetry: (outcome, delay, retryAttempt, context) =>
//            {
//                Console.WriteLine($"Retrying... Attempt {retryAttempt}");
//            });
//}
