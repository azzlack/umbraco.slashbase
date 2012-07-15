namespace Umbraco.SlashBase.Tests.Controllers
{
    using System.Net.Http;
    using System.Web.Security;

    using Umbraco.SlashBase.Attributes;
    using Umbraco.SlashBase.Controllers;

    public class MemberSecureFunctionsController : SlashBaseController
    {
        [SlashBaseSecurity(AllowedMembers = new[] { "admin" })]
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage() { Content = new StringContent("Hello admin member!") };
        }

        public HttpResponseMessage Get(string id)
        {
            FormsAuthentication.SetAuthCookie(id, false);

            return new HttpResponseMessage() { Content = new StringContent("User '" + id + "' logged in.") };
        }
    }
}