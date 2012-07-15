namespace Umbraco.SlashBase.Tests.Web.Tests
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Net.NetworkInformation;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;

    using NUnit.Framework;

    using umbraco.presentation.install.utills;

    [TestFixture]
    public abstract class BaseTestFixture
    {
        /// <summary>
        /// The base address.
        /// </summary>
        private const string BaseAddress = "http://localhost";

        /// <summary>
        /// The base port.
        /// </summary>
        private int basePort = 59322;

        /// <summary>
        /// Thee IIS Express process
        /// </summary>
        private Process iisProcess;

        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        public HttpClient Client { get; private set; }

        /// <summary>
        /// Gets the cookie container.
        /// </summary>
        /// <value>
        /// The cookie container.
        /// </value>
        public CookieContainer CookieContainer { get; private set; }

        /// <summary>
        /// Gets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        public int BasePort
        {
            get
            {
                return this.basePort;
            }
        }

        /// <summary>
        /// Posts the message to the specified window.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [TestFixtureSetUp]
        public virtual void TestFixtureSetUp()
        {
            // Start standalone iis express instance if port has not been defined
            if (this.basePort == 0)
            {
                // Get port
                this.basePort = this.GetAvailableTcpPort();         

                // Start site
                var thread = new Thread(this.StartIisExpress) { IsBackground = true };

                thread.Start();
            } 
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            this.CloseIisExpress();
        }

        [SetUp]
        public virtual void SetUp()
        {
            // Setup http client
            this.CookieContainer = new CookieContainer();

            var handler = new HttpClientHandler
                {
                    CookieContainer = this.CookieContainer
                };

            this.Client = new HttpClient(handler)
            {
                BaseAddress = new Uri(BaseAddress + ":" + this.BasePort + "/")
            };

            this.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [TearDown]
        public void TearDown()
        {
            this.Client.Dispose();
        }

        /// <summary>
        /// Starts the IIS express process.
        /// </summary>
        private void StartIisExpress()
        {
            var appLocation = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory);

            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                ErrorDialog = true,
                LoadUserProfile = true,
                CreateNoWindow = false,
                UseShellExecute = false,
                Arguments = string.Format("/path:\"{0}\" /port:{1}", appLocation, this.BasePort)
            };

            var programfiles = string.IsNullOrEmpty(startInfo.EnvironmentVariables["programfiles"])
                                ? startInfo.EnvironmentVariables["programfiles(x86)"]
                                : startInfo.EnvironmentVariables["programfiles"];

            startInfo.FileName = programfiles + "\\IIS Express\\iisexpress.exe";

            try
            {
                this.iisProcess = new Process { StartInfo = startInfo };

                this.iisProcess.Start();
                this.iisProcess.WaitForExit();
            }
            catch (Exception ex)
            {
                Debug.Write(ex);

                this.CloseIisExpress();
            }
        }

        /// <summary>
        /// Closes the IIS express process.
        /// </summary>
        private void CloseIisExpress()
        {
            if (this.iisProcess != null && !this.iisProcess.HasExited)
            {
                // Send the 'Q' key to the process window
                PostMessage(this.iisProcess.MainWindowHandle, 0x100, (IntPtr)Keys.Q, IntPtr.Zero);

                // Give iis express time to shut down
                Thread.Sleep(2000);

                // Force shutdown of process if not closed
                if (!this.iisProcess.HasExited)
                {
                    this.iisProcess.CloseMainWindow();
                    this.iisProcess.Dispose();
                }
            }
        }


        /// <summary>
        /// Gets an available TCP port.
        /// </summary>
        /// <returns>A port number.</returns>
        private int GetAvailableTcpPort()
        {
            var globalIpProperties = IPGlobalProperties.GetIPGlobalProperties();
            var activeTcpConnections = globalIpProperties.GetActiveTcpConnections();

            var availablePorts = Enumerable.Range(1024, 64510).Where(x => activeTcpConnections.All(c => c.LocalEndPoint.Port != x)).ToList();

            var port = availablePorts[new Random().Next(availablePorts.Count())];

            return port;
        }
    }
}