using Fatihdgn.Todo.Context;
using Fatihdgn.Todo.DTOs.Validators;
using Fatihdgn.Todo.Entities;
using Fatihdgn.Todo.Handlers;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
    config.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                    new HeaderApiVersionReader("x-api-version"),
                                                    new MediaTypeApiVersionReader("x-api-version"));
});

builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});

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
        Scheme = JwtBearerDefaults.AuthenticationScheme,
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

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3(options =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions.Reverse())
        {
            options.SwaggerRoutes.Add(new NSwag.AspNetCore.SwaggerUi3Route(description.GroupName.ToUpperInvariant(), $"/swagger/{description.GroupName}/swagger.json"));
        }
    });
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

