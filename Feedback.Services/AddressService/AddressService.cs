using Feedback.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Feedback.Services.AddressService
{
    public class AddressService: IAddressService
    {
        private HttpClient _client;
        private readonly ILogger<AddressService> _logger;
        private const string ApiHeaderKey = "x-api-key";

        public AddressService(HttpClient client, ILogger<AddressService> logger)
        {
            _client = client;
            _logger = logger;
        }
        public async Task<List<PostalAddress>> GetAddresses(string addressProviderUrl, 
                                                            string headerKey,
                                                            string postcode)
        {
            var addressList = new List<PostalAddress>();
            if (string.IsNullOrWhiteSpace(addressProviderUrl))
            {
                throw new ArgumentNullException(nameof(addressProviderUrl));
            }

            if (string.IsNullOrWhiteSpace(headerKey))
            {
                throw new ArgumentNullException(nameof(headerKey));
            }

            if (string.IsNullOrWhiteSpace(postcode))
            {
                throw new ArgumentNullException(nameof(postcode));
            }

            try
            {

                if (!string.IsNullOrWhiteSpace(postcode))
                {
                    _client.DefaultRequestHeaders.Add(ApiHeaderKey, headerKey);
                    var response = await _client.GetStringAsync($"{addressProviderUrl}{postcode}");
                    addressList = JsonConvert.DeserializeObject<List<PostalAddress>>(response);
                }

            }
            catch (Exception ex)
            {
                 _logger.LogError($"Please disable the Network  Proxy and Internet Options -->Connections-->LAN Setting (Automatic detect settings & Use automatic configuration script) setting -- {ex}");
            }

            return addressList;
        }
    }
}
