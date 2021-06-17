﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace SalootProject.Api.Configuration
{
    public static class SwaggerRegistration
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(p =>
            {
                p.SwaggerDoc("v1", new OpenApiInfo { Title = "SalootProject", Version = "v1" });
                p.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });
                p.AddSecurityRequirement(new OpenApiSecurityRequirement {
                     {
                       new OpenApiSecurityScheme
                       {
                         Reference = new OpenApiReference
                         {
                           Type = ReferenceType.SecurityScheme,
                           Id = "Bearer"
                         }
                        },
                        new string[] { }
                      }
                    });
            });
        }

        public static IApplicationBuilder RegisterSwaggerMidlleware(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(p => p.SwaggerEndpoint("/swagger/v1/swagger.json", "SalootProject"));

            return app;
        }
    }
}