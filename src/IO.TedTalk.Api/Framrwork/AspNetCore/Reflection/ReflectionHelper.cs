using System.Reflection;

namespace IO.TedTalk.Api.Framrwork.AspNetCore.Reflection;

/// <summary>
/// Defines helper methods for reflection.
/// </summary>
public static class ReflectionHelper
{
    
    public static TAttribute GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<TAttribute>(MemberInfo memberInfo, TAttribute defaultValue = default, bool inherit = true)
        where TAttribute : class
    {
        return memberInfo.GetCustomAttributes(true).OfType<TAttribute>().FirstOrDefault()
               ?? memberInfo.DeclaringType?.GetTypeInfo().GetCustomAttributes(true).OfType<TAttribute>().FirstOrDefault()
               ?? defaultValue;
    }

    
}
