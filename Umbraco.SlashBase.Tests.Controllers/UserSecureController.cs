namespace Umbraco.SlashBase.Tests.Controllers
{
    using System.Net.Http;

    using Umbraco.SlashBase.Attributes;
    using Umbraco.SlashBase.Controllers;

    public class UserSecureController : SlashBaseController
    {
        [SlashBaseSecurity(AllowedUsers = new[] { "admin" })]
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage() { Content = new StringContent("Hello admin user!") };
        }
    }
}