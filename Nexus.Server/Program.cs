using Microsoft.AspNetCore.Authentication.Cookies;
using Nexus.Server.NexusContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using Nexus.Server.NexusListener;
using Nexus.Server.ThemeController;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Google.Api;
using Microsoft.AspNetCore.Authorization;
[assembly: ApiController]

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
    options.LoginPath = "/login";
});
builder.Services.AddSingleton<ListenerX>();
builder.Services.AddSingleton<ThemeControllerX>();
builder.Services.AddControllers();
//builder.Services.AddDbContext<NexusAppContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NexusConnection")));
builder.Services.AddGrpc();

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthorization();
app.UseWebSockets();

app.UseHttpsRedirection();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

await app.RunAsync();