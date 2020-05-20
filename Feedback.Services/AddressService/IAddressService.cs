using Feedback.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Feedback.Services.AddressService
{
    public interface IAddressService
    {
        Task<List<PostalAddress>> GetAddresses(
            string addressProviderUrl,
            string headerKey,
            string postcode);
    }
}
