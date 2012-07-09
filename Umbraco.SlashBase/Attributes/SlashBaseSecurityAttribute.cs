namespace Umbraco.SlashBase.Attributes
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;
    using System.Web.Security;

    using umbraco.BusinessLogic;
    using umbraco.cms.businesslogic.member;

    /// <summary>
    /// Base attribute for SlashBase security.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class SlashBaseSecurityAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Gets or sets the allowed members.
        /// </summary>
        /// <value>
        /// The allowed members.
        /// </value>
        public string[] AllowedMembers { get; set; }

        /// <summary>
        /// Gets or sets the allowed member groups.
        /// </summary>
        /// <value>
        /// The allowed member group aliases.
        /// </value>
        public string[] AllowedMemberGroups { get; set; }

        /// <summary>
        /// Gets or sets the allowed member types .
        /// </summary>
        /// <value>
        /// The allowed member type aliases.
        /// </value>
        public string[] AllowedMemberTypes { get; set; }

        /// <summary>
        /// Gets or sets the allowed users.
        /// </summary>
        /// <value>
        /// The allowed users.
        /// </value>
        public string[] AllowedUsers { get; set; }

        /// <summary>
        /// Gets or sets the allowed user types.
        /// </summary>
        /// <value>
        /// The allowed user type aliases.
        /// </value>
        public string[] AllowedUserTypes { get; set; }

        /// <summary>
        /// Called when [action executing].
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var user = User.GetCurrent();
            var member = Member.GetCurrentMember();

            // Check member first
            if ((this.AllowedMembers != null && this.AllowedMembers.Any()) || (this.AllowedMemberGroups != null && this.AllowedMemberGroups.Any()) || (this.AllowedMemberTypes != null && this.AllowedMemberTypes.Any()))
            {
                this.ValidateMember(member, actionContext);
            }

            // Then check user
            if ((this.AllowedUsers != null && this.AllowedUsers.Any()) || (this.AllowedUserTypes != null && this.AllowedUserTypes.Any()))
            {
                this.ValidateUser(user, actionContext);
            }
        }

        /// <summary>
        /// Validates the member.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <param name="actionContext">The action context.</param>
        private void ValidateMember(Member member, HttpActionContext actionContext)
        {
            if (member == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            else
            {
                if (this.AllowedMembers.Any() && !this.AllowedMembers.Contains(member.LoginName))
                {
                    // If allowedmembers are set, reject the request if the member is not in the list
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
                }
                else if (this.AllowedMemberGroups.Any() && !this.AllowedMemberGroups.Any(x => Roles.GetRolesForUser().Contains(x)))
                {
                    // If allowedmembergroups are set, reject the request if the member is not in the group
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
                }
                else if (this.AllowedMemberTypes.Any() && !this.AllowedMemberTypes.Contains(Member.GetCurrentMember().ContentType.Alias))
                {
                    // If allowedmembertypes are set, reject the request if the member is not of the specified type
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
                }
            }
        }

        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="actionContext">The action context.</param>
        private void ValidateUser(User user, HttpActionContext actionContext)
        {
            if (user == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            else 
            {
                if (this.AllowedUsers.Any() && !this.AllowedUsers.Contains(User.GetCurrent().LoginName))
                {
                    // If allowedusers are set, reject the request if the is not in the list
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
                }                
                else if (this.AllowedUserTypes.Any() && !this.AllowedUserTypes.Contains(User.GetCurrent().UserType.Alias))
                {
                    // If allowedusertypes are set, reject the request if the user is not of the specified type
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
                }
            }
        }
    }
}