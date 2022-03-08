using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ToDo.Entity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("any", builder =>
    {
        builder.WithMethods("GET", "POST", "HEAD", "PUT", "DELETE", "OPTIONS")
                //.AllowCredentials()//指定处理cookie
                .WithOrigins("http://139.224.221.148:201").AllowAnyHeader(); //允许任何来源的主机访问
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,//是否验证Issuer
        ValidateAudience = true,//是否验证Audience
        ValidateLifetime = true,//是否验证失效时间
        ClockSkew = TimeSpan.FromSeconds(60*24*7),
        ValidateIssuerSigningKey = true,//是否验证SecurityKey
        ValidAudience = "QANstar",//Audience
        ValidIssuer = "QANstar",//Issuer，这两项和前面签发jwt的设置一致
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("QANstarAndSuoMi1931"))//拿到SecurityKey
    };
});

builder.Services.AddDbContext<QAN_TododbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("any");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
