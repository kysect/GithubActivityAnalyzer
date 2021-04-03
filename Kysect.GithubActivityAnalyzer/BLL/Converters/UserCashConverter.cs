using Kysect.GithubActivityAnalyzer.DLL.Entities;
using Kysect.GithubActivityAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Kysect.GithubActivityAnalyzer.DLL.Converters
{
    public class UserCashConverter
    {
        public UserCash ConvertToUserCash(string username, ActivityInfo info)
        {
            var cash = JsonSerializer.Serialize(info);
            return new UserCash() { Username = username, ActivityInfo = cash };
        }
        public ActivityInfo GetActivityFromUserCash(UserCash userCash)
        {
            var activity = JsonSerializer.Deserialize<ActivityInfo>(userCash.ActivityInfo, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return activity;
        }
    }
}
