using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using VKTask.DAL;

namespace VKTask.Auth;

public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly ApplicationDbContext _context;
    public BasicAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> option, ILoggerFactory logger,
    UrlEncoder encoder, ISystemClock clock, ApplicationDbContext context) : base(option, logger, encoder, clock)
    {
        _context = context;
    }
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
            return AuthenticateResult.Fail("No header found");
        var headerValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
        var bytes = Convert.FromBase64String(headerValue.Parameter);
        string credentials = Encoding.UTF8.GetString(bytes);
        if (!string.IsNullOrEmpty(credentials))
        {
            var array = credentials.Split(':');
            string userName = array[0];
            string password = array[1];
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == userName 
            && u.Password == password);
            if (user is null)
                return AuthenticateResult.Fail("Unauthorized");
            
            var claims = new[] {
            new Claim(ClaimTypes.Name, userName), new Claim("Id", user.Id.ToString())};
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
        else
        {
            return AuthenticateResult.Fail("Unauthorized");
        }
            
    }
}
