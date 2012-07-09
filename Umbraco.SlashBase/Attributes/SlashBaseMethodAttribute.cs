namespace Umbraco.SlashBase.Attributes
{
    using System;

    /// <summary>
    /// Attribute for setting method security
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class SlashBaseMethodAttribute : SlashBaseSecurityAttribute
    {
    }
}