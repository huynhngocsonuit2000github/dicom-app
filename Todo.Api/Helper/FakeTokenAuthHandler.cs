using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

public class FakeTokenAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public FakeTokenAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock
    ) : base(options, logger, encoder, clock) { }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
            return Task.FromResult(AuthenticateResult.Fail("No token"));

        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        ClaimsIdentity identity = token switch
        {
            "admin" => new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "admin"),
                new Claim(ClaimTypes.Role, "admin"),
                new Claim(ClaimTypes.Role, "member")
            }, Scheme.Name),

            "member" => new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "member"),
                new Claim(ClaimTypes.Role, "member")
            }, Scheme.Name),

            _ => null
        };

        if (identity == null)
            return Task.FromResult(AuthenticateResult.Fail("Invalid token"));

        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
