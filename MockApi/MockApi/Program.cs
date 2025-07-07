using MockApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient<PriceService>();


// CORS policy ekle
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // React uygulamanýn adresi
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Altýn fiyatý servisini ekle
builder.Services.AddHttpClient<PriceService>();

var app = builder.Build();
// CORS middleware'i kullan
app.UseCors("AllowReactApp");

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
