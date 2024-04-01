using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NZWalksDbContext>(options
    => options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString")));

//su dung cho repository
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
//moi lan tao repository cho model moi phai su dung dong nay moi co tac dung
builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();

//su dung auto mapper
builder.Services.AddAutoMapper(typeof(NZWalksDbContext));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
