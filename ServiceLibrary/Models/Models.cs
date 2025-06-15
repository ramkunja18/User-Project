using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Models
{
    


    public class UserData
    {
        public int Id { get; set; }
        public string Email { get; set; } 
        public string First_Name { get; set; } 
        public string Last_Name { get; set; } 
        public string Avatar { get; set; } 
    }
    public class UserPageResponse
    {
        public int Page { get; set; }
        public int Per_Page { get; set; }
        public int Total { get; set; }
        public int Total_Pages { get; set; }
        public List<UserData> Data { get; set; } = new();
        public SupportInfo Support { get; set; } = new();
        public int StatusCode { get; set; }
    }
    public class SupportInfo
    {
        public string Url { get; set; }
        public string Text { get; set; } 
    }
    public class SingleUserResponse
    {
        public UserData Data { get; set; } = new();
        public SupportInfo Support { get; set; } = new();
    }
    public class CacheSettings
    {
        public int UserExpirationMinutes { get; set; }
    }
   public class AppSettings
    {
        public string BaseUrl { get; set; }
        public int PageSize { get; set; }
    }
}
