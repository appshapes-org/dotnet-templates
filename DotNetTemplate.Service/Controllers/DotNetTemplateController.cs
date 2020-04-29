using AppShapes.Core.Service;
using Microsoft.Extensions.Logging;

namespace DotNetTemplate.Service.Controllers
{
    public class DotNetTemplateController : ApiControllerBase
    {
        public DotNetTemplateController(ILogger<DotNetTemplateController> logger)
        {
            Logger = logger;
        }

        protected ILogger<DotNetTemplateController> Logger { get; }
    }
}