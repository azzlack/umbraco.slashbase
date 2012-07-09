namespace Umbraco.SlashBase.Controllers
{
    using System.Web.Http;

    using Umbraco.SlashBase.Attributes;

    /// <summary>
    /// Base class for SlashBase services
    /// </summary>
    [SlashBaseExceptionFilter]
    public abstract class SlashBaseController : ApiController
    {
    }
}