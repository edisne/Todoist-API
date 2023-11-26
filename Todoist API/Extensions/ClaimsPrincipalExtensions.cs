﻿using System.Security.Claims;

namespace Todoist_API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUsername (this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value;
        }

        public static string GetUserId (this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
