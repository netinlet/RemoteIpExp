using System.Net;
using System.Net.Sockets;
using Microsoft.AspNetCore.Mvc;

namespace RemoteIpExp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IpDecoder : ControllerBase
    {
        // GET api/<IpDecoder>/5
        [HttpGet("{decodeType}")]
        public string Get(string decodeType)
        {
            return decodeType switch
            {
                "ByConnectionRemoteIp" => ByConnectionRemoteIp(),
                "ByIpLookupToIpV4" => ByIpLookupToIpV4(),
                _ => "Unknown decodeType"
            };
        }
        private string ByConnectionRemoteIp()
        {
            var remoteIpString = HttpContext.Connection.RemoteIpAddress?.ToString();
            if (string.IsNullOrEmpty(remoteIpString))
                return "ByIpLookupToIpV4: IP address is null or empty";

            return remoteIpString;
        }

        private string ByIpLookupToIpV4()
        {
            var remoteIpString = HttpContext.Connection.RemoteIpAddress?.ToString();
            if (string.IsNullOrEmpty(remoteIpString))
                return "ByIpLookupToIpV4: IP address is null or empty";

            var addresses = Dns.GetHostAddresses(remoteIpString);
            var ipv4 = addresses.FirstOrDefault(ip =>
                ip.AddressFamily == AddressFamily.InterNetwork);

            return ipv4?.ToString() ?? "ByIpLookupToIpV4: IPv4 address not found";
        }
    }
}