using Microsoft.EntityFrameworkCore;
using CompanyWork.Data;
using CompanyWork.Interfaces;
using CompanyWork.Services;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
// Add services to the container.

builder.Services.AddDbContext<MyDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection"))); 
builder.Services.AddMvcCore(); //maybe add .AddControllersAsServices();

//add asset services here




builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.WithOrigins("http://localhost:4200")
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowAnyOrigin();
}));

 



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//DI
builder.Services.AddScoped<IPost, AssetPost>();
builder.Services.AddScoped<IUpdate, AssetUpdate>();



var app = builder.Build();


app.UseCors("MyPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

 
 
app.UseAuthorization();
app.UseRouting();

app.MapControllers();
 


app.Run();
