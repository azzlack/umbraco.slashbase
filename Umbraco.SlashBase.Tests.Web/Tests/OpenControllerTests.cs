namespace Umbraco.SlashBase.Tests.Web.Tests
{
    using System.Net;

    using NUnit.Framework;

    public class OpenControllerTests : BaseTestFixture
    {
        [Test]
        public void Get_WhenGivenId_ShouldEchoId()
        {
            const string Message = "hello";

            var result = this.Client.GetStringAsync("uBase/Open/" + Message).Result;

            Assert.That(result == Message);
        }

        [Test]
        public void Get_WhenNotGivenId_ShouldThrowException()
        {
            var result = this.Client.GetAsync("uBase/Open/").Result;

            Assert.IsTrue(result.StatusCode == HttpStatusCode.InternalServerError);
        }
    }
}
