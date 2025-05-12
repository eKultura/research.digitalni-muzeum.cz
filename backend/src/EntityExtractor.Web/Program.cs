using eKultura.EntityExtractor.Contracts;
using eKultura.EntityExtractor.Domain;
using System.IO.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IFileSystem, FileSystem>();
builder.Services.AddScoped<IFileStoring>(provider =>
{
    var fileSystem = provider.GetRequiredService<IFileSystem>();
    var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "StoredFiles");

    return new FileStoring(fileSystem, baseFolder);
});
    
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();