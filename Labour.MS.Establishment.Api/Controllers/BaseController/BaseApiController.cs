using Labour.MS.Establishment.Utility.Constants;
using Labour.MS.Establishment.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labour.MS.Establishment.Api.Controllers.BaseController
{
    [ApiController]
    //[Authorize]
    [Route($"{ApiInformation.BasePath}/{ApiInfoConstant.SubBasePath}/{ApiInfoConstant.Establishment}")]
    [Produces("application/json")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class BaseApiController : ControllerBase
    {
    }
}
