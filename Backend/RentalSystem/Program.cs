var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.ConfigureServices();

builder.Services.AddScoped<IVoziloRepository, VoziloRepository>();
builder.Services.AddScoped<IKorisnikRepository, KorisnikRepository>();
builder.Services.AddScoped<IIznajmljivanjeService, IznajmljivanjeService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


var app = builder.Build();
// Configure the HTTP request pipeline.
app.ConfigurePipeline();
app.Run();







