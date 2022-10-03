using BookStore.BL.Interfaces;
using BookStore.BL.Services;
using BookStore.DL.Interfaces;
using BookStore.DL.Repositories.InMemoryRepos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IPersonRepo,PersonInMemoryRepo>();
builder.Services.AddSingleton<IAuthorRepo, AuthorInMemoryRepo>();
builder.Services.AddSingleton<IBookRepo, BookInMemoryRepo>();
builder.Services.AddSingleton<IPersonService, PersoneService>();
builder.Services.AddSingleton<IAuthorService, AuthorService>();
builder.Services.AddSingleton<IBookService, BookService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
