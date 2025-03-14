

using Microsoft.OpenApi.Models;

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


        builder.Services.AddAutoMapper(typeof(MappingProfile));

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new VoziloDodavanjeDTOConverter());
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Rental System API", Version = "v1" });

            // Dodavanje JWT autentifikacije u Swagger
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Unesi JWT token u formatu: Bearer <tvoj-token>",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });


    }
}
