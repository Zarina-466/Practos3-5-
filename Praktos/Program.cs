using Microsoft.EntityFrameworkCore;
using Praktos.DatabaseContext;
using Praktos.Interfaces;
using Praktos.Services;
using ProxyKit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TestApiDB>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TestDbString")), ServiceLifetime.Scoped);

builder.Services.AddScoped<IPhotoService, PhotosService>();
builder.Services.AddScoped<IBooksService, BooksService>();
builder.Services.AddScoped<IAuthorsService, AuthorsService>();
builder.Services.AddScoped<IGenresService, GenresService>();
builder.Services.AddScoped<IReadersService, ReadersService>();
builder.Services.AddScoped<IRentalService, RentalService>();

builder.Services.AddProxy();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseWhen(context => context.Request.Path.Value.Contains("/api/Books"),
    applicationBuilder => applicationBuilder.RunProxy(context =>
        context.ForwardTo("https://localhost:7192/").AddXForwardedHeaders().Send()));
app.UseWhen(context => context.Request.Path.Value.Contains("/api/Reader"),
applicationBuilder => applicationBuilder.RunProxy(context =>
context.ForwardTo("https://localhost:7118/").AddXForwardedHeaders().Send()));
app.UseWhen(context => context.Request.Path.Value.Contains("/api/Photo"),
applicationBuilder => applicationBuilder.RunProxy(context =>
context.ForwardTo("https://localhost:7021/").AddXForwardedHeaders().Send()));


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
