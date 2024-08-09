using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using secondYear.context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers();
//Cross-Origin Resource Sharing (CORS)
builder.Services.AddCors(Options => {
    Options.AddPolicy("AllowAllOrigins", 
    builder => {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// .AddJsonOptions(options =>
//     {
//         options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
//     });

// void AddJsonOptions(Action<object> value)
// {
//     throw new NotImplementedException();
// }

// builder.Services.AddSingleton<TokenService>();



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

app.Run();

