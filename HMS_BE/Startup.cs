using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using HMS_BE.Repository;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace HMS_BE
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<HMS_BE.Models.HMSContext>(options =>
             //   options.UseSqlServer(Configuration["ConnectionStrings:DB"]));
            services.AddDbContext<HMS_BE.Models.HMSContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DB")));

            services.AddScoped<IGroupUserRepository, GroupUserRepository>();
            services.AddScoped<IAllowedWorkGroupRepository, AllowedWorkGroupRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<ILeaderRepository, LeaderRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWorkRepository, WorkRepository>();
            services.AddScoped<IWorkTicketRepository, WorkTicketRepository>();

            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(type => type.ToString());
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "HMS_BE",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Firebase JWT Authorization header using Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
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
                        new string[]{}
                    }
                });

                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                c.CustomSchemaIds(type => type.ToString());
            });
            services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.Authority = "https://securetoken.google.com/work-management-system-80525";
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = "https://securetoken.google.com/work-management-system-80525",
                ValidateAudience = true,
                ValidAudience = "work-management-system-80525",
                ValidateLifetime = true
            };
        });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers();
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
                c.AddPolicy("AllowHeader", options => options.AllowAnyHeader());
                c.AddPolicy("AllowMethod", options => options.AllowAnyMethod());
            });

            // Admin
            services.AddTransient<HMS_BE.Repository.IUserRepository, HMS_BE.Repository.UserRepository>();

            /*services.AddSingleton(FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(Configuration["Firebase:Admin"]),
            })
           );*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HMS_BE v1"));
            }
            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors(options =>
            {
                options.AllowAnyHeader();
                options.AllowAnyMethod();
                options.AllowAnyOrigin();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
