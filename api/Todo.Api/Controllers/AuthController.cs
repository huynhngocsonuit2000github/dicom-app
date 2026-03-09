using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Security.Claims;
using System.Text.Json;

namespace Todo.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    // ===================== AZURE LOGIN =====================
    [HttpGet("login-az")]
    [AllowAnonymous]
    public IActionResult LoginAzure()
    {
        var returnUrl = "http://localhost:4200/about";

        var redirectUrl = Url.Action(
            nameof(HandleLoginCallback),
            "Auth",
            new { returnUrl }
        );

        return Challenge(
            new AuthenticationProperties
            {
                RedirectUri = redirectUrl
            },
            OpenIdConnectDefaults.AuthenticationScheme
        );
    }

    // ===================== AZURE CALLBACK =====================
    [Authorize]
    [HttpGet("handle-logincallback")]
    public IActionResult HandleLoginCallback(string returnUrl)
    {
        var claims = User.Claims;

        var objectId = claims.FirstOrDefault(c => c.Type == "oid")?.Value;
        var tenantId = claims.FirstOrDefault(c => c.Type == "tid")?.Value;
        var email =
            claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value
            ?? claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        // ✅ FE-readable cookie
        Response.Cookies.Append(
            "TODO_custom_cookies",
            JsonSerializer.Serialize(new AppContextCookie
            {
                ObjectId = objectId,
                TenantId = tenantId,
                Email = email,
                Tenant = "Azure",
                CustomField = "RoleId123"
            }),
            new CookieOptions
            {
                HttpOnly = false,
                Secure = true,                // 🔥 REQUIRED
                SameSite = SameSiteMode.None, // 🔥 REQUIRED
                Path = "/"
            }
        );

        return Redirect(returnUrl ?? "http://localhost:4200");
    }

    // ===================== AUTHENTICATED USER =====================
    [Authorize]
    [HttpGet("authenticated-user")]
    public IActionResult AuthenticatedUser()
    {
        Request.Cookies.TryGetValue("TODO_custom_cookies", out var rawCookie);

        AppContextCookie? context = null;

        if (!string.IsNullOrWhiteSpace(rawCookie))
        {
            context = JsonSerializer.Deserialize<AppContextCookie>(
                rawCookie,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        var roleId = context?.CustomField;

        var roles = User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);


        return Ok(new
        {
            name = User.Identity?.Name,
            context,
            roles = roleId == "RoleId123" ? new List<string>() { "admin", "member" } : new List<string>() { "member" },
            azureRoles = roles
        });
    }
}

// ===================== COOKIE MODEL =====================
public class AppContextCookie
{
    public string? ObjectId { get; set; }
    public string? TenantId { get; set; }
    public string? Email { get; set; }
    public string? Tenant { get; set; }
    public string? CustomField { get; set; }
}
