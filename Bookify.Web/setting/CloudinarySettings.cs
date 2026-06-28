using Microsoft.AspNetCore.SignalR.Protocol;

namespace Bookify.Web.setting
{
    public class CloudinarySettings
    {
        public string CloudName { get; set; } = null!;
        public string APIkey { get; set; } = null!;
        public string APISecret { get; set; } = null!;
    }
}
