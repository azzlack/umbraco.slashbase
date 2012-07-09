namespace Umbraco.SlashBase.Tests.Web.Tests
{
    using System.Collections.Specialized;
    using System.Net;
    using System.Web.Security;

    using NUnit.Framework;

    using umbraco.providers;

    public class UserTypeSecureControllerTests : BaseTestFixture
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
        public void Get_WhenNotLoggedIn_ShouldReturnException()
        {
            var result = this.Client.GetAsync("UserTypeSecure").Result;

            Assert.That(result.StatusCode == HttpStatusCode.Forbidden);
        }
    }
}
