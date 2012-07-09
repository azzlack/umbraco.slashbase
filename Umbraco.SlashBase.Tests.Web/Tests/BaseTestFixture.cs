namespace Umbraco.SlashBase.Tests.Web.Tests
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;

    using NUnit.Framework;

    [TestFixture]
    public abstract class BaseTestFixture
    {
        /// <summary>
        /// The base address.
        /// </summary>
        private const string BaseAddress = "http://localhost:59322/uBase/";

        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        public HttpClient Client { get; private set; }

        [SetUp]
        public virtual void SetUp()
        {
            this.Client = new HttpClient()
            {
                BaseAddress = new Uri(BaseAddress)
            };

            this.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [TearDown]
        public void TearDown()
        {
            this.Client.Dispose();
        }
    }
}