using Microsoft.AspNetCore.Mvc;
using SalootProject.Api.FilterActions;

namespace SalootProject.Api.Controllers
{
    [Route("api/[controller]"), ApiResponseFilter, TypeFilter(typeof(ActionsLoggerFilterAttribute)), ApiController]
    public class BaseController : ControllerBase { }
}