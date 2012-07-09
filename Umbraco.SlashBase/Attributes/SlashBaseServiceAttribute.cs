namespace Umbraco.SlashBase.Attributes
{
    using System;

    /// <summary>
    /// Attribute for setting service security
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SlashBaseServiceAttribute : SlashBaseSecurityAttribute
    {
    }
}