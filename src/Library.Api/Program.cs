using Library.Api.Applications.Authors;
using Library.Api.Applications.Books;
using Library.Api.Infrastructure.MongoDb;
using Library.Api.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
builder.Services.AddMongo(configuration);
builder.Services.AddServices();
builder.Services.AddScoped<CreateAuthor>();
builder.Services.AddScoped<GetAuthor>();
builder.Services.AddScoped<UpdateAuthor>();
builder.Services.AddScoped<CreateBook>();
builder.Services.AddScoped<GetBook>();
builder.Services.AddScoped<GetBookAll>();
builder.Services.AddScoped<GetBookDetails>();
builder.Services.AddScoped<UpdateBook>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
       
    c.CustomSchemaIds(type => type.FullName.Replace("+", "."));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

AuthorEndpoints.Map(app);
BookEndpoints.Map(app);

app.Run();
