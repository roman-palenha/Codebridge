using Codebridge.Configs.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Codebridge.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        private readonly IInformationConfig _info;

        public PingController(IInformationConfig info)
        {
            _info = info;
        }

        [HttpGet]
        public string Ping()
        {
            return $"{_info.DogsService}. {_info.Version}";
        } 
    }
}
