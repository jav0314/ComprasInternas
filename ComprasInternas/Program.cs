using ApiComprasInternas.DAO;
using ComprasInternas.DAO;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<SolicitudesUsuarioDAO>();
builder.Services.AddScoped<SolicitudSupervisorDAO>();
builder.Services.AddScoped<RegisterDAO>();
builder.Services.AddScoped<LoginDAO>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy
            .WithOrigins("http://localhost:63603") // frontend Angular
            .WithOrigins("http://localhost:4200") // frontend Angular
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("AllowAngularApp");
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
