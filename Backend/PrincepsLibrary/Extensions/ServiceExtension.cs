

namespace PrincepsLibrary.Extensions;

public static class ServiceExtension
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {

        builder.Services.AddDbContext<Context>(options =>
        {
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("TIKScs"),
                b => b.MigrationsAssembly("RentalSystem")
            );
        });
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<Context>();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CORS", policy =>
            {
                policy.AllowAnyHeader()
                      .AllowAnyMethod()
                      .WithOrigins("http://localhost:5500",
                                   "https://localhost:5500",
                                   "http://127.0.0.1:5500",
                                   "https://127.0.0.1:5500");
            });
        });


        builder.Services.AddAutoMapper(typeof(KorisnikMapper));
        builder.Services.AddAutoMapper(typeof(VoziloMapper));

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new VoziloDodavanjeDTOConverter());
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();
        });

    }
}
