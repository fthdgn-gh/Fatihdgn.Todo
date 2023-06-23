using Fatihdgn.Todo.Context;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using Fatihdgn.Todo.DTOs.Validators;
using Fatihdgn.Todo.Handlers;
using NSwag.Annotations;
using Fatihdgn.Todo.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NSwag;
using NSwag.Generation.Processors.Security;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocument(config =>
{
    config.Title = "Fatihdgn Todo";
    config.Version = "v1";
    config.AddSecurity(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Type = OpenApiSecuritySchemeType.ApiKey,
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = OpenApiSecurityApiKeyLocation.Header,
    });

    config.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor(JwtBearerDefaults.AuthenticationScheme));

    config.DocumentProcessors.Add(new SecurityDefinitionAppender(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Type = OpenApiSecuritySchemeType.ApiKey,
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = OpenApiSecurityApiKeyLocation.Header
    }));

    config.OperationProcessors.Add(new OperationSecurityScopeProcessor(JwtBearerDefaults.AuthenticationScheme));
});

builder.Services.AddDbContext<TodoDB>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseInMemoryDatabase(nameof(TodoDB));
    else
        options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(TodoDB)));
});

builder.Services.AddTodoDBRepositories();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<DTOValidatorsMarker>();

builder.Services.AddMediatR(options => options.RegisterServicesFromAssemblyContaining<RequestHandlersMarker>());

builder.Services.AddIdentity<TodoUserEntity, IdentityRole>()
       .AddEntityFrameworkStores<TodoDB>()
       .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtBearerAuthenticationValidIssuer"]!,
            ValidAudience = builder.Configuration["JwtBearerAuthenticationValidAudience"]!,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtBearerAuthenticationIssuerSigningKey"]!))
        };
    });

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
