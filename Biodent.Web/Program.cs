using Biodent.Models;
using Biodent.Web.Controllers;
using Biodent.Web.Hubs;
using Biodent.Web.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(o =>
//{
//    o.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidIssuer = builder.Configuration["Jwt:Issuer"],
//        ValidAudience = builder.Configuration["Jwt:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),

//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true
//    };
//});

builder.Services.AddAuthorization();
builder.Services.AddSignalR();

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BiodentDbContext>(
    //options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"))
    options => options.UseMySql(builder.Configuration.GetConnectionString("DBConnection"),
        new MySqlServerVersion(new Version(8, 0, 26)))
    );

//Fixed for Linux error
builder.Services.AddDataProtection().UseCryptographicAlgorithms(
    new AuthenticatedEncryptorConfiguration
    {
        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
        ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
    });

//builder.Services.AddSession();
builder.Services.AddSession(options =>
{
    // Configure session options as needed
});
builder.Services.AddSignalR();

var app = builder.Build();

//For Linux
if (!app.Environment.IsDevelopment())
{
    app.UseHttpLogging();
    //app.UseHttpsRedirection();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseAuthentication();
app.UseSession();
app.UseRouting();


//app.MapPost("/security/createToken",
//[AllowAnonymous] (UsersModel user) =>
//{
//    if (user.UsersName == "joydip" && user.Password == "joydip123")
//    {
//        var issuer = builder.Configuration["Jwt:Issuer"];
//        var audience = builder.Configuration["Jwt:Audience"];
//        var key = Encoding.ASCII.GetBytes
//        (builder.Configuration["Jwt:Key"]);
//        var tokenDescriptor = new SecurityTokenDescriptor
//        {
//            Subject = new ClaimsIdentity(new[]
//            {
//                new Claim("Id", Guid.NewGuid().ToString()),
//                new Claim(JwtRegisteredClaimNames.Sub, user.UsersName),
//                new Claim(JwtRegisteredClaimNames.Email, user.Email),
//                new Claim(JwtRegisteredClaimNames.Jti,
//                Guid.NewGuid().ToString())
//             }),
//            Expires = DateTime.UtcNow.AddMinutes(5),
//            Issuer = issuer,
//            Audience = audience,
//            SigningCredentials = new SigningCredentials
//            (new SymmetricSecurityKey(key),
//            SecurityAlgorithms.HmacSha512Signature)
//        };
//        var tokenHandler = new JwtSecurityTokenHandler();
//        var token = tokenHandler.CreateToken(tokenDescriptor);
//        var jwtToken = tokenHandler.WriteToken(token);
//        var stringToken = tokenHandler.WriteToken(token);
//        return Results.Ok(stringToken);
//    }
//    return Results.Unauthorized();
//});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.UseEndpoints(routes =>
{
    routes.MapHub<ChatHub>("/chatHub");
});

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//       name: "default",
//    pattern: "{controller=Chats}/{action=Index}/{id?}");
//});

//app.MapControllerRoute<ClaimsIdentity>(
    //name: "default",
    //pattern: "{controller=Login}/{action=Index}/{id?}"
    //);
//app.MapHub<ChatHub>("/chatHub");

app.Run();
