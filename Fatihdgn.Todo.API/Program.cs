using Fatihdgn.Todo.Context;
using Fatihdgn.Todo.DTOs.Validators;
using Fatihdgn.Todo.Entities;
using Fatihdgn.Todo.Handlers;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using System.Text;

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
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    config.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor(JwtBearerDefaults.AuthenticationScheme));

    config.DocumentProcessors.Add(new SecurityDefinitionAppender(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Type = OpenApiSecuritySchemeType.ApiKey,
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = OpenApiSecurityApiKeyLocation.Header,
        Scheme = JwtBearerDefaults.AuthenticationScheme
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

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddHttpsRedirection(options =>
    {
        options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
        options.HttpsPort = 443;
    });
}

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
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

