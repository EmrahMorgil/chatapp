using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Server.Application;
using Server.Application.Behaviors;
using Server.Domain.Entities;
using Server.Persistence;
using Server.Persistence.Extensions;
using Server.WebApi.Hubs;
using Server.WebApi.Middlewares;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
var settings = builder.Configuration.GetSection("TokenInfo").Get<Settings>();

//Http isteginde gonderilen header'a ulasmak ıcın eklendi.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//Registiration
//builder.Services.AddFluentValidationAutoValidation();   bu fluentvalidation un default ayarları

//fluent validation error ayarları
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
//---
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
        ValidIssuer = settings.ValidIssuer,
        ValidAudience = settings.ValidAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret)),
    };
});
//--------------


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}




//app.UseHttpsRedirection(); //kaldýrýlacak

//JWT
app.UseAuthentication();
app.UseAuthorization();
//---

app.MapHub<ChatHub>("/api/chat-hub");

//static klasör wwwroot için
app.UseStaticFiles();

//CORS
app.UseCors();
//------

//exception handler
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.MigrateDatabase();

app.Run();
