namespace Umbraco.SlashBase.Tests.Web.Tests
{
    using System.Collections.Specialized;
    using System.Net;
    using System.Web;
    using System.Web.Security;

    using NUnit.Framework;

    using umbraco.BusinessLogic;
    using umbraco.providers;

    using Umbraco.SlashBase.Tests.Web.Helpers;

    public class UserSecureControllerTests : BaseTestFixture
    {
        /// <summary>
        /// The membership provider
        /// </summary>
        private MembershipProvider provider;

        /// <summary>
        /// Sets up.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            this.provider = new UsersMembershipProvider();
           
            var config = new NameValueCollection
                {
                    { "name", "UsersMembershipProvider" },
                    { "enablePasswordRetrieval", "false" },
                    { "enablePasswordReset", "false" },
                    { "requiresQuestionAndAnswer", "false" },
                    { "passwordFormat", "Hashed" }
                };
            this.provider.Initialize(config["name"], config);
        }

        [Test]
        public void Get_WhenLoggedIn_ShouldReturnOK()
        {
            var user = User.GetUser(0);
            var loggedIn = LoginHelper.DoLogin(user, this.CookieContainer);

            var result = this.Client.GetAsync("uBase/UserSecure").Result;

            Assert.That(result.StatusCode == HttpStatusCode.OK);
        }

        [Test]
        public void Get_WhenNotLoggedIn_ShouldReturnForbidden()
        {
            var result = this.Client.GetAsync("uBase/UserSecure").Result;

            Assert.That(result.StatusCode == HttpStatusCode.Forbidden);
        }
    }
}
