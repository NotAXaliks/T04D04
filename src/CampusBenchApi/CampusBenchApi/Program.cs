using CampusBenchApi.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt=>opt.UseNpgsql("host=127.0.0.1:40001;username=xaliks;password=coolPaSsw0rd;database=campusbench"));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendDev", policy =>
    {
        policy.SetIsOriginAllowed(origin => true) 
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); 
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowFrontendDev");
app.UseRouting();


app.MapControllers();
app.Run();
