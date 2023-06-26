using Microsoft.EntityFrameworkCore;
using CoolApi.Models;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options=>{
    options.TokenValidationParameters=new TokenValidationParameters{
        ValidateIssuer=false,
        ValidateAudience=false,
        ValidateLifetime=true,
       IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) 
    };
});
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s=>
    s.AddSecurityDefinition("Bearer",new Microsoft.OpenApi.Models.OpenApiSecurityScheme{
        In=Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description="Please enter JWT with Bearer into field",
        Name ="Authorization",
        Type =  Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        BearerFormat="JWT",
        Scheme="bearer"
    })
);
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddDbContext<UserContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<ProductContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("ProductConnection")));

builder.Services.AddDbContext<AdsContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("AdsConnection")));

builder.Services.AddDbContext<CategorieContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("CategorieConnection")));

builder.Services.AddDbContext<CountriesContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("CountriesConnection")));
builder.Services.AddDbContext<CitiesContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("CitiesConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthorization();
app.MapControllers();

app.Run();
