using backend.Repos;
using backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// ==== CORS ====
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // tvoj React frontend
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// ==== Neo4j Driver ====
builder.Services.AddSingleton<Neo4jDriverService>(sp =>
    new Neo4jDriverService(
        "bolt://localhost:7687",
        "neo4j",
        "newpassword"
    )
);

// ==== Repozitorijumi i servisi ====

builder.Services.AddScoped<IAuthorRepo, AuthorRepo>();
builder.Services.AddScoped<AuthorService>();

builder.Services.AddScoped<IPublisherRepo, PublisherRepo>();
builder.Services.AddScoped<PublisherService>();

builder.Services.AddScoped<IMechanicRepo, MechanicRepo>();
builder.Services.AddScoped<MechanicService>();

builder.Services.AddScoped<IGameRepo, GameRepo>();
builder.Services.AddScoped<GameService>();

builder.Services.AddScoped<IRentalRepo, RentalRepo>();
builder.Services.AddScoped<RentalService>();

// ==== Swagger ====
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

// ==== Enable CORS ====
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
