using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Feedback.Web.ViewModels
{
    public class UserPostalAddress
    {
        [Required(ErrorMessage = "Please either verify the entered postcode or Network Proxy and Internet Options --> Connection -->LAN Settings")]
        public string SelectedAddress { get; set; }
        public List<SelectListItem> ListAddresses { get; set; }
        public BackButton BackButton { get; set; }
        public bool ShowError { get; set; }
    }
}
