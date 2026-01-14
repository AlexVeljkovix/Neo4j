using backend.Repos;
using backend.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") 
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


builder.Services.AddSingleton<Neo4jDriverService>(sp =>
    new Neo4jDriverService(
        "bolt://localhost:7687",
        "neo4j",
        "newpassword"
    )
);


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


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
