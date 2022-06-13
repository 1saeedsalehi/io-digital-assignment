using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace IO.TedTalk.Api.Framrwork.AspNetCore;

/// <summary>
/// Helper and Utility methods
/// </summary>
public static class AspNetCoreMvcExtensions
{
    public static ControllerActionDescriptor AsControllerActionDescriptor(this ActionDescriptor actionDescriptor)
    {
        if (actionDescriptor is not ControllerActionDescriptor controllerActionDescriptor)
        {
            throw new Core.Exceptions.IOException(
                $"{nameof(actionDescriptor)} should be type of {typeof(ControllerActionDescriptor).AssemblyQualifiedName}");
        }

        return controllerActionDescriptor;
    }

    public static MethodInfo GetMethodInfo(this ActionDescriptor actionDescriptor)
    {
        return actionDescriptor.AsControllerActionDescriptor().MethodInfo;
    }

}
