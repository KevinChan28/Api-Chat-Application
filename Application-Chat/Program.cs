using Application_Chat.Context;
using Application_Chat.Repository;
using Application_Chat.Repository.Imp;
using Application_Chat.Service;
using Application_Chat.Service.Imp;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add services to the containes
builder.Services.Configure<ChatStoreDatabaseSettings>(
	builder.Configuration.GetSection("ChatStoreDatabase"));

builder.Services.AddSingleton(sp =>
{
	var databaseSettings = sp.GetRequiredService<IOptions<ChatStoreDatabaseSettings>>().Value;
	var mongoDbClient = new MongoClient(databaseSettings.ConnectionString);
	var mongoDb = mongoDbClient.GetDatabase(databaseSettings.DatabaseName);

	return mongoDb;
});

builder.Services.AddScoped<IUserRepository, ImpUserRepository>();
builder.Services.AddScoped<IUser, ImpUser>();

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
