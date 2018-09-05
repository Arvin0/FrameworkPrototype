using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SYN.ApiCore.Common;
using SYN.Model;
using SYN.Service;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using SYN.ApiCore.Model;

namespace SYN.ApiService.Controllers
{
    [Route("config")]
    [ApiController]
    public class SystemConfigurationController : ApiController
    {
        private readonly ISystemConfigurationService _systemConfigurationService;

        /// <inheritdoc />
        public SystemConfigurationController(ISystemConfigurationService systemConfigurationService)
        {
            _systemConfigurationService = systemConfigurationService;
        }

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(ApiResponse<IList<SystemDictionaryModel>>))]
        public async Task<ApiResponse<IList<SystemDictionaryModel>>> Get()
        {
            var result = await _systemConfigurationService.Get();
            return Success(result);
        }

        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(ApiResponse<SystemDictionaryModel>))]
        public async Task<ApiResponse<SystemDictionaryModel>> Get(int id)
        {
            var result = await _systemConfigurationService.Get(id);
            return Success(result);
        }
    }
}