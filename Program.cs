using System.Text;
using GraphQL_Test2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AddAuthorization()
    .AddQueryType<Query>();

var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("15129322381076306817"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = "http://localhost:4000/",
            ValidAudience = "http://localhost:4000/",
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey
        };
    });

var app = builder.Build();

app.UseAuthentication();
app.MapGet("/", () => "Hello Docker!");
app.MapGraphQL();

app.Run();
