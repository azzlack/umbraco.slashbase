namespace Umbraco.SlashBase.Tests.Web.Helpers
{
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web.Security;

    using umbraco.BusinessLogic;

    public class LoginHelper
    {
        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="cookieContainer">The cookie container.</param>
        /// <returns>True if login successful. Otherwise false.</returns>
        public static bool DoLogin(User user, CookieContainer cookieContainer)
        {
            var retVal = Guid.NewGuid();
            
            try
            {
                Application.SqlHelper.ExecuteNonQuery(
                                          "insert into umbracoUserLogins (contextID, userID, timeout) values (@contextId,'" + user.Id + "','" + (DateTime.Now.Ticks + (600000000 * 1)) + "') ",
                                          Application.SqlHelper.CreateParameter("@contextId", retVal));

                var logincookie = new Cookie("UMB_UCONTEXT", retVal.ToString(), "/", "localhost");
                cookieContainer.Add(logincookie);

                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex);

                return false;
            }
        }

        /// <summary>
        /// Logs in a member.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <param name="client">The client.</param>
        /// <returns>
        /// True if login successful. Otherwise false.
        /// </returns>
        public static bool DoLogin(MembershipUser member, HttpClient client)
        {
            try
            {
                // Need to get forms authentication cookie from server, otherwise it won't work
                var loginResult = client.GetAsync("uBase/MemberSecure/" + member.UserName).Result;

                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex);

                return false;
            }
        } 
    }
}