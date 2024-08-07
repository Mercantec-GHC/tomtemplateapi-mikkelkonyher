var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();

// Define a route that responds to HTTP GET requests
app.MapGet("/GasPrice",
    async (IHttpClientFactory httpClientFactory) =>
    {
        var client = httpClientFactory.CreateClient();
        var response = await client.GetFromJsonAsync<Miles95Prices[]>("https://magsapi.onrender.com/api/miles95");

        if (response is null)
        {
            return Results.NotFound();
        }

        // Manually set the date for testing
        var currentTime = new DateTime(2023, 8, 6, 10, 0, 0); 
        // var currentTime = DateTime.Now; // Use this line for the actual current time

        string message;

        if (currentTime.Hour < 12)
        {
            message = "Du er tidligt oppe og tjekke priser";
        }
        else
        {
            message = "Test test jeg er en hest.";
        }

        var customResponse = new
        {
            Message = message,
            Timestamp = currentTime,
            Data = response
        };

        return  Results.Ok(customResponse);
    });

app.Run();

record Miles95Prices(DateOnly Date, decimal Price);