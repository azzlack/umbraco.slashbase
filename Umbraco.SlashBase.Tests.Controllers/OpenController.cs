namespace Umbraco.SlashBase.Tests.Controllers
{
    using System;
    using System.Net.Http;

    using Umbraco.SlashBase.Controllers;

    public class OpenController : SlashBaseController
    {
        public HttpResponseMessage Get()
         {
             throw new Exception("TEST");
         }

        public HttpResponseMessage Get(string id)
        {
            return new HttpResponseMessage() { Content = new StringContent(id) };
        }
    }
}