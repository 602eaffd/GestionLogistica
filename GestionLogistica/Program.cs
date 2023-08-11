using GestionLogistica.Models;
using AutoMapper;
using GestionLogistica;
using GestionLogistica.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using GestionLogistica.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//1 - Inyección ContextoDB
builder.Services.AddDbContext<GestionLogisticaContext>(ServiceLifetime.Scoped);

//2 - CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy",
        builder =>
        {
            builder.AllowAnyHeader()
                   .WithMethods("*") // Métodos permitidos
                   .AllowAnyOrigin();
        });
});

//3 - AUTOMAPPER
var mapperConfig = new MapperConfiguration(m =>
{
    m.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper); //Mapeo mismo para todos los que soliciten durante la ejecucion de la app.
builder.Services.AddMvc();

builder.Services.AddScoped<IUserService, UserService>();

//4 - JWT 
// Configurar sección de configuración "AppSettings" del archivo de configuración
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);
// Obtener las configuraciones de "AppSettings" a partir de la sección
var appSettings = appSettingsSection.Get<AppSettings>();

// Convertir la clave secreta en un arreglo de bytes
var key = Encoding.ASCII.GetBytes(appSettings.Secreto);

// Configurar el esquema de autenticación por defecto (JWT)
builder.Services.AddAuthentication(d =>
{
    d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(d =>
    {
        // Configurar si se requiere HTTPS para las solicitudes de metadatos del token (false en este caso)
        d.RequireHttpsMetadata = false;

        // Indicar si se deben guardar los tokens en el contexto del token
        d.SaveToken = true;

        // Configurar los parámetros de validación del token
        d.TokenValidationParameters = new TokenValidationParameters
        {
            // Indicar si se debe validar la clave secreta del emisor del token
            ValidateIssuerSigningKey = true,

            // Establecer la clave secreta usada para firmar y validar el token
            IssuerSigningKey = new SymmetricSecurityKey(key),

            // Indicar si se debe validar el emisor del token (en este caso, no se valida)
            ValidateIssuer = false,

            // Indicar si se debe validar la audiencia del token (en este caso, no se valida)
            ValidateAudience = false
        };
    });


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

app.UseHttpsRedirection();

//2.1 - CORS
app.UseCors("MyPolicy");

//4.1 - JWT
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
