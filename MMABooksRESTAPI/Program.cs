/* Author:  Lindy Stewart
 * Editor:  Eric Robinson L00709820
 * Date:    11/25/23
 * Course:  Lane Community College CS234 Advanced Programming: C# (.NET)
 * Lab:     6 
 * Purpose: 
 */

using MMABooksEFClasses.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// add cors policy - in a production app lock this down!
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(
        builder => {
            builder.AllowAnyOrigin()
          .WithMethods("POST", "PUT", "DELETE", "GET", "OPTIONS")
          .AllowAnyHeader();
        });
});
// adding the dbContext to the service
builder.Services.AddDbContext<MMABooksContext>();

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

//Comment this out
//app.UseHttpsRedirection();

//add the CORS

// in a production app you would want to turn this back on!
// app.UseHttpsRedirection();

// enables the cors policy
app.UseCors();


app.UseAuthorization();

app.MapControllers();

app.Run();
