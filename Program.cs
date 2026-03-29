using CloudinaryDotNet;
using doctors.data;
using doctors.services.impelemtion;
using doctors.services.interfaces;
using dotenv.net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using doctors.Middlewares;

namespace doctors
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<IDoctorService, DoctorService>();

            builder.Services.AddDbContext<AppDbContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("cs")));


            // ????? ?????????
            DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));

            builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
            builder.Services.AddScoped<Icloudinarycs, CloudinaryService>();
<<<<<<< HEAD
            builder.Services.AddScoped<IEmail, Emailservis>();
            builder.Services.AddScoped<IDoctorPatientService, DoctorPatientService>();
            builder.Services.AddScoped<IAuthService, doctors.services.implementation.AuthService>();
            builder.Services.AddScoped<IpatientService, PatientService>();
            builder.Services.AddScoped<IChatService, ChatService>();
            builder.Services.AddScoped<IMeasurementService, MeasurementService>();
            // builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddIdentity<User, IdentityRole>()
       .AddEntityFrameworkStores<AppDbContext>();
=======
            builder.Services.AddScoped<IEmail,Emailservis>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();
>>>>>>> ac09d612b98d7ffab041ed4ae440eff7cb744df1


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", policy =>
                {
                    policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });



            });

            builder.Services.AddControllers();

            var keyStr = builder.Configuration["Jwt:Key"] ?? "a_very_long_super_secret_key_which_is_secure_1234567890";
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? "DoctorsApp",
                    ValidAudience = builder.Configuration["Jwt:Audience"] ?? "DoctorsApp",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyStr))
                };
            });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SupportNonNullableReferenceTypes();


            });


            var app = builder.Build();

            app.UseMiddleware<GlobalExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

            }
            app.UseCors("MyPolicy");


            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            
        }
    }
}
