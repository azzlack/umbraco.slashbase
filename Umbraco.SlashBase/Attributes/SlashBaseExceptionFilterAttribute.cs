namespace Umbraco.SlashBase.Attributes
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Filters;

    using umbraco.BusinessLogic;

    /// <summary>
    /// Exception handler for SlashBase services.
    /// </summary>
    public class SlashBaseExceptionFilterAttribute : ExceptionFilterAttribute 
    {
        /// <summary>
        /// Called when [exception].
        /// </summary>
        /// <param name="context">The context.</param>
        public override void OnException(HttpActionExecutedContext context)
        {
            // Log error to umbraco log
            Log.Add(LogTypes.Error, -1, string.Format("[SlashBase] Message: {0}, StackTrace: {1}", context.Exception.Message, context.Exception.StackTrace));

            // Throw exception to user
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("A critical error occurred. Look in the Umbraco log for exception details."),
                ReasonPhrase = "Internal Server Error"
            });
        }
    }
}