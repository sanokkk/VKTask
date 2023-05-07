using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using VKTask.Auth;
using VKTask.DAL;
using VKTask.DAL.Interfaces;
using VKTask.DAL.Repos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("BasicAuth").AddScheme<AuthenticationSchemeOptions, BasicAuthHandler>("BasicAuth", null);

builder.Services.AddRouting(r =>
{
    r.LowercaseQueryStrings = true;
});

builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("Pg")));

builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepo, UserRepo>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
