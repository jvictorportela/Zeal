using Zeal.API.Filters;
using Zeal.API.Middleware;
using Zeal.Application;
using Zeal.Infra;
using Zeal.Infra.Extensions;
using Zeal.Infra.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new Zeal.API.Converters.StringConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Injeção de dependências
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfra(builder.Configuration);
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CultureMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

MigrateDatabase();

app.Run();

void MigrateDatabase()
{
    var conectionString = builder.Configuration.ConnectionString();

    var serviceProvider = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

    DatabaseMigration.Migrate(conectionString, serviceProvider.ServiceProvider);
}