using Microsoft.EntityFrameworkCore;
using Core.Api.ServiceConfigurations;
using Core.Shared;
using Microsoft.Extensions.Caching.Memory;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers(options =>
{
    // options.Filters.Add(new AuthorizeFilter());
}).AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapperConfiguration();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddCorsConfiguration();
// builder.Services.AddSwaggerGenConfiguration(builder.Configuration);
// builder.Services.AddAuthenticationConfiguration(builder.Configuration);
builder.Services.AddDbContextConfiguration(builder.Configuration);
// builder.Services.AddIdentityConfiguration();
// builder.Services.AddRepositories();
// builder.Services.AddUnitsOfWork();
// builder.Services.AddFluentValidation();
// builder.Services.AddLocalizationConfiguration();


var app = builder.Build();
using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope())
{
    var context = serviceScope?.ServiceProvider.GetRequiredService<ApplicationDbContext>()!;

    context.Database.Migrate();

}
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
