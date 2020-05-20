 

namespace Feedback.Models.Settings
{
    public class WebSettings
    {
        public string PostalAddressUrl { get; set; } = null;

        public string PostalAddressKey { get; set; } = null;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public bool EnableAutoDatabaseMigration { get; set; } = false;


    }
}
