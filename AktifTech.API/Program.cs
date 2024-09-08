using AktifTech.API.Authentication.Interfaces;
using AktifTech.API.Authentication.Services;
using AktifTech.Business.Interfaces;
using AktifTech.Business.Services;
using AktifTech.Cache.Extensions;
using AktifTech.Cache.Repositories;
using AktifTech.Database.DataAccessLayer;
using AktifTech.Database.Repositories;
using AktifTech.Database.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerOrderRepository, CustomerOrderRepository>();
builder.Services.AddScoped<IOrderProductRepository, OrderProductRepository>();

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerOrderService, CustomerOrderService>();
builder.Services.AddScoped<IOrderProductService, OrderProductService>();

//redis
builder.Services.AddStackExchangeRedis(builder.Configuration);
builder.Services.AddSingleton<RedisRepository>();
builder.Services.AddSingleton<IRedisService, RedisService>();

//builder.Services.AddTransient<IAuthService, AuthService>();
//builder.Services.AddTransient<ITokenService, TokenService>();

////JWT
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(o =>
//{
//    o.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
//        ValidAudience = builder.Configuration["JWT:ValidAudience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])),
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = false,
//        ValidateIssuerSigningKey = true
//    };
//});

//builder.Services.AddAuthorization();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.Use(async (context, next) =>
//{
//    if (context.Request.RouteValues.Count == 0)
//    {
//        context.Response.StatusCode = 404;
//        return;
//    }
//    else
//    {
//        if (context.Request.Path == "/api/Customers/LoginControl")
//        {
//            await next.Invoke();
//        }
//        else
//        {
//            if (context.Request.Headers.ContainsKey("Authorization"))
//            {
//                string AuthHeader = context.Request.Headers["Authorization"];
//                bool isValid = false;
//                var handler = new JwtSecurityTokenHandler();

//                //invalid token kontrolü
//                var CheckIsJWT = IsValidJWT(AuthHeader, app);
//                if (CheckIsJWT == false)
//                {
//                    context.Response.StatusCode = 403;
//                    return;
//                }
//                var token = handler.ReadJwtToken(AuthHeader);
//                isValid = true;
//                //doðrulama burda
//                if (isValid)
//                {
//                    await next.Invoke();
//                }
//                else
//                {
//                    context.Response.StatusCode = 403;
//                    return;
//                }
//            }
//            else
//            {
//                context.Response.StatusCode = 403;
//                return;
//            }
//        }
//    }
//});

app.Run();

//static bool IsValidJWT(string token, WebApplication app)
//{
//    try
//    {
//        var handler = new JwtSecurityTokenHandler();
//        handler.ValidateToken(token, new TokenValidationParameters
//        {
//            ValidIssuer = app.Configuration["JWT:ValidIssuer"],
//            ValidAudience = app.Configuration["JWT:ValidAudience"],
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(app.Configuration["JWT:Secret"])),
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = false,
//            ValidateIssuerSigningKey = true
//        }, out SecurityToken validatedToken);
//        if (validatedToken != null)
//        {
//            return true;
//        }
//        return false;
//    }
//    catch (Exception ex)
//    {
//        return false;
//    }

//}