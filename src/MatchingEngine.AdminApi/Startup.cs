using System.Globalization;
using System.Reflection;
using System.Text;
using Autofac;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MatchingEngine.AdminApi.Configuration;
using MatchingEngine.AdminApi.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Swisschain.Sdk.Server.Common;
using Swisschain.Sdk.Server.Swagger;

namespace MatchingEngine.AdminApi
{
    public sealed class Startup
    {
        public Startup(IConfiguration configRoot)
        {
            ConfigRoot = configRoot;
            Config = ConfigRoot.Get<AppConfig>();
        }

        private IConfiguration ConfigRoot { get; }

        private AppConfig Config { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCors(options => options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                }))
                .AddAutoMapper(typeof(AutoMapperProfile))
                .AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    var namingStrategy = new CamelCaseNamingStrategy();

                    options.SerializerSettings.Converters.Add(new StringEnumConverter(namingStrategy));
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                    options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                    options.SerializerSettings.Culture = CultureInfo.InvariantCulture;
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    options.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Error;
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = namingStrategy
                    };
                })
                .AddFluentValidation(options =>
                {
                    ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;
                    options.RegisterValidatorsFromAssembly(Assembly.GetEntryAssembly());
                });

            var key = Encoding.ASCII.GetBytes(Config.AdminApi.Secret);

            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true
                    };
                });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo {Title = ApplicationInformation.AppName, Version = "v1"});
                options.EnableXmsEnumExtension();
                options.MakeResponseValueTypesRequired();

                options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme.",
                });

                options.OperationFilter<AuthOperationFilter>();
            });

            services.AddSwaggerGenNewtonsoftSupport();

            services.AddSingleton(Config);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwagger(c => c.RouteTemplate = "api/{documentName}/swagger.json");
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../../api/v1/swagger.json", "API V1");
                c.RoutePrefix = "swagger/ui";
            });

            app.ApplicationServices.GetRequiredService<AutoMapper.IConfigurationProvider>()
                .AssertConfigurationIsValid();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule(Config.AdminApi.AssetsServiceUrl,
                Config.AdminApi.BalancesServiceUrl, Config.AdminApi.OrderBooksServiceUrl,
                Config.AdminApi.CashOperationsServiceAddress, Config.AdminApi.TradingServiceAddress));
        }
    }
}
