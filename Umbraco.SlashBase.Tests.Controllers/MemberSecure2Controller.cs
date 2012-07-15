namespace Umbraco.SlashBase.Tests.Controllers
{
    using System.Net.Http;
    using System.Web.Security;

    using Umbraco.SlashBase.Attributes;
    using Umbraco.SlashBase.Controllers;

    [SlashBaseSecurity(AllowedMembers = new[] { "user" })]
    public class MemberSecure2Controller : SlashBaseController
    {
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage() { Content = new StringContent("Hello user/admin member!") };
        }

        [SlashBaseSecurity(AllowedMembers = new[] { "admin" })]
        public HttpResponseMessage Get(string id)
        {
            return new HttpResponseMessage() { Content = new StringContent("Hello admin member!") };
        }
    }
}