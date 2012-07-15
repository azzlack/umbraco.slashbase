namespace Umbraco.SlashBase.Tests.Web.Tests
{
    using System.Collections.Specialized;
    using System.Net;
    using System.Web.Security;

    using NUnit.Framework;

    using umbraco.providers.members;
    using Umbraco.SlashBase.Tests.Web.Helpers;

    public class MemberSecure2ControllerTests : BaseTestFixture
    {
        /// <summary>
        /// The membership provider
        /// </summary>
        private UmbracoMembershipProvider provider;

        /// <summary>
        /// Sets up.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            this.provider = new UmbracoMembershipProvider();
           
            var config = new NameValueCollection
                {
                    { "name", "UmbracoMembershipProvider" },
                    { "enablePasswordRetrieval", "false" },
                    { "enablePasswordReset", "false" },
                    { "requiresQuestionAndAnswer", "false" },
                    { "defaultMemberTypeAlias", "Another Type" },
                    { "passwordFormat", "Hashed" }
                };
            this.provider.Initialize(config["name"], config);
        }

        [Test]
        public void Get_WhenLoggedInAsAdmin_ShouldReturnForbidden()
        {
            var member = Membership.GetUser("admin");

            var loggedIn = LoginHelper.DoLogin(member, this.Client);

            var result = this.Client.GetAsync("uBase/MemberSecure2").Result;

            Assert.That(result.StatusCode == HttpStatusCode.Forbidden);
        }

        [Test]
        public void Get_WhenLoggedInAsUser_ShouldReturnOK()
        {
            var member = Membership.GetUser("user");

            var loggedIn = LoginHelper.DoLogin(member, this.Client);

            var result = this.Client.GetAsync("uBase/MemberSecure2").Result;

            Assert.That(result.StatusCode == HttpStatusCode.OK);
        }

        [Test]
        public void Get_WhenNotLoggedIn_ShouldReturnForbidden()
        {
            var result = this.Client.GetAsync("uBase/MemberSecure2").Result;

            Assert.That(result.StatusCode == HttpStatusCode.Forbidden);
        }


        [Test]
        public void GetSpecific_WhenLoggedInAsUser_ShouldReturnForbidden()
        {
            var member = Membership.GetUser("user");

            var loggedIn = LoginHelper.DoLogin(member, this.Client);

            var result = this.Client.GetAsync("uBase/MemberSecure2/1").Result;

            Assert.That(result.StatusCode == HttpStatusCode.Forbidden);
        }

        [Test]
        public void GetSpecific_WhenLoggedInAsAdmin_ShouldReturnOK()
        {
            var member = Membership.GetUser("admin");

            var loggedIn = LoginHelper.DoLogin(member, this.Client);

            var result = this.Client.GetAsync("uBase/MemberSecure2/1").Result;

            Assert.That(result.StatusCode == HttpStatusCode.OK);
        }

        [Test]
        public void GetSpecific_WhenNotLoggedIn_ShouldReturnForbidden()
        {
            var result = this.Client.GetAsync("uBase/MemberSecure2/1").Result;

            Assert.That(result.StatusCode == HttpStatusCode.Forbidden);
        }
    }
}
