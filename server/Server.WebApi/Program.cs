using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Server.Application;
using Server.Domain.Entities;
using Server.Persistence;
using Server.Persistence.Extensions;
using Server.WebApi.Business;
using Server.WebApi.Hubs;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
var settings = builder.Configuration.GetSection("TokenInfo").Get<Settings>();

//Registiration
builder.Services.AddApplicationRegistration();
builder.Services.AddPersistenceServices();

//CORS settings
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .SetIsOriginAllowed(origin => true)
    )
);


//SignalR
builder.Services.AddTransient<MyBusiness>();
builder.Services.AddSignalR();
//---------------

//JWT settings
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = settings.validIssuer,
        ValidAudience = settings.validAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.secret)),
    };
});
//--------------


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//CORS
app.UseCors();
//------


app.UseHttpsRedirection();

//JWT
app.UseAuthentication();
app.UseAuthorization();
//---

app.MapHub<ChatHub>("/chat-hub");

app.MapControllers();

app.MigrateDatabase();

app.Run();
