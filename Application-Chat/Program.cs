using Application_Chat.Context;
using Application_Chat.Repository;
using Application_Chat.Repository.Imp;
using Application_Chat.Security;
using Application_Chat.Service;
using Application_Chat.Service.Imp;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiChat", Version = "v1" });
	var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

	//define security for authorization
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
	{
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "JWT Authorization header using bearer scheme"
	});
	c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
					   new string[]{}
					}

				 });
});

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

//Configuration of server
builder.Services.AddJwtServices(builder.Configuration);

builder.Services.AddScoped<IUserRepository, ImpUserRepository>();
builder.Services.AddScoped<IUser, ImpUser>();
builder.Services.AddScoped<IGroupRepository, ImpGroupRepository>();
builder.Services.AddScoped<IGroup, ImpGroup>();

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
