using Microsoft.EntityFrameworkCore;
using CompanyWork.Data;
using CompanyWork.Services.AssetServices;
using CompanyWork.Services.AssetTypeServices;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
// Add services to the container.


//For DI
//Asset
builder.Services.AddScoped<AssetPostUpdate>();
builder.Services.AddTransient<AssetSearch>();
builder.Services.AddScoped<AssetDelete>();
//builder.Services.AddScoped<AssetGetById>();



//AssetType
builder.Services.AddScoped<AssetTypePostUpdate>();
builder.Services.AddScoped<AssetTypeSearch>();
builder.Services.AddScoped<AssetTypeDelete>();
//builder.Services.AddScoped<AssetTypeGetById>();
//builder.Services.AddScoped<Test>();





builder.Services.AddDbContext<MyDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
builder.Services.AddMvcCore(); //maybe add .AddControllersAsServices();
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.WithOrigins("http://localhost:4200")
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowAnyOrigin();
}));

 
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
