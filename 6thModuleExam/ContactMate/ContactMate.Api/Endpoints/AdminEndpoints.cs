using ContactMate.Bll.Dtos;
using ContactMate.Bll.Services;
using ContactMate.Core.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ContactMate.Api.Endpoints;

public static class AdminEndpoints
{
    public static void MapAdminEndpoints(this WebApplication app)
    {
        var userGroup = app.MapGroup("/api/user")
            .RequireAuthorization()
            .WithTags("User endpoints");

        userGroup.MapGet("/getUsersByRole",
            [Authorize(Roles = "Admin, SuperAdmin")]
        [ResponseCache(Duration = 5, Location = ResponseCacheLocation.Any, NoStore = false)]
        async (string role, IUserRoleService _userRoleService) =>
            {
                var users = await _userRoleService.GetAllUsersByRoleNameAsync(role);
                return Results.Ok(users);
            })
        .WithName("GetUsersByRole");

        userGroup.MapDelete("/delete", [Authorize(Roles = "Admin, SuperAdmin")]
        async (long userId, HttpContext httpContext, IUserService userService) =>
        {
            var role = httpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            await userService.DeleteUserByRoleAsync(userId, role);
            return Results.Ok();
        })
        .WithName("DeleteUserByRole");

        userGroup.MapPatch("/changeRole", [Authorize(Roles = "SuperAdmin")]
        async (long userId, string userRole, IUserService userService) =>
        {
            await userService.UpdateUserRoleAsync(userId, userRole);
            return Results.Ok();
        })
        .WithName("UpdateUserRole");
    }
}
