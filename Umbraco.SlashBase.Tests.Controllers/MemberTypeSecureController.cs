namespace Umbraco.SlashBase.Tests.Controllers
{
    using System.Net.Http;

    using Umbraco.SlashBase.Attributes;
    using Umbraco.SlashBase.Controllers;

    public class MemberTypeSecureController : SlashBaseController
    {
        [SlashBaseMethod(AllowedMemberTypes = new[] { "administrator" })]
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage() { Content = new StringContent("Hello administrator type!") };
        }
    }
}