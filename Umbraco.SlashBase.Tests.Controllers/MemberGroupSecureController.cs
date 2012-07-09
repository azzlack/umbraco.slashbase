namespace Umbraco.SlashBase.Tests.Controllers
{
    using System.Net.Http;

    using Umbraco.SlashBase.Attributes;
    using Umbraco.SlashBase.Controllers;

    public class MemberGroupSecureController : SlashBaseController
    {
        [SlashBaseMethod(AllowedMemberGroups = new[] { "administrators" })]
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage() { Content = new StringContent("Hello administrator group member!") };
        }
    }
}