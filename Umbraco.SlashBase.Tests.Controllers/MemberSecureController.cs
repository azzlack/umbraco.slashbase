namespace Umbraco.SlashBase.Tests.Controllers
{
    using System.Net.Http;

    using Umbraco.SlashBase.Attributes;
    using Umbraco.SlashBase.Controllers;

    public class MemberSecureController : SlashBaseController
    {
        [SlashBaseSecurity(AllowedMembers = new[] { "admin" })]
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage() { Content = new StringContent("Hello admin member!") };
        }
    }
}