using FinanceFlow.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IGastoService, GastoService>();
builder.Services.AddCors(options =>
options.AddPolicy("AllowAngular",
    policy => policy.WithOrigins("http://localhost:4200")
    .AllowAnyHeader()
    .AllowAnyMethod())
);

var app = builder.Build();

app.UseCors("AllowAngular");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
