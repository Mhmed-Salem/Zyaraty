using System;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Zyarat.Data;
using Zyarat.Handlers;
using Zyarat.Mapping;
using Zyarat.Models.Repositories.EvaluationRepos;
using Zyarat.Models.Repositories.MedicalRepRepo;
using Zyarat.Models.Repositories.ReportRepo;
using Zyarat.Models.Repositories.VisitsRepo;
using Zyarat.Models.Services;
using Zyarat.Models.Services.EvaluationsServices;
using Zyarat.Models.Services.IdentityServices;
using Zyarat.Models.Services.InterActing;
using Zyarat.Models.Services.IVisitService.VisitsServices;
using Zyarat.Models.Services.MedicalRepService;
using Zyarat.Models.Services.ReportServices;
using Zyarat.Options;

namespace Zyarat
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
            /**Database services and identity**/
            var ConsoleLoggetFactory = LoggerFactory.Create(builder => 
            {
                builder.AddFilter((s, level) =>
                    s == DbLoggerCategory.Database.Command.Name
                    && level == LogLevel.Information).AddConsole(); 
            });
            services.AddDbContextPool<ApplicationContext>(
                builder => builder.UseSqlServer(Configuration.GetConnectionString("ZyaratConnection"))
                    .UseLoggerFactory(ConsoleLoggetFactory)
            ); 
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 5;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<ApplicationContext>(); 
                /**End of database services injection**/
                
                /** start Inject Services */
                services.AddScoped<IUnitWork, UnitWork>();
                services.AddScoped<IMedicalRepRepo, MedicalRepRepo>();
                services.AddScoped<IVisitsRepo, VisitRepo>();
                services.AddScoped(typeof(FileService));
                services.AddScoped<IMedicalRepService, MedicalRepService>();
                services.AddScoped(typeof(MedicalRepVisitsHandlers));
                services.AddScoped<IVisitService,VisitService>();
                services.AddScoped<IEvaluationService,EvaluationService>();
                
                services.AddScoped<IEvaluationRepo,EvaluationRepo>();
                services.AddScoped<IIdentityUser, IdentityService>();
                services.AddScoped<IVisitInteracting, VisitInteracting>();

                services.AddScoped(typeof(MedicalRepEvaluationsHandlers));
                services.AddScoped(typeof(VisitAssertion));

                services.AddScoped<IReportService, ReportService>();
                services.AddScoped<IReportRepo, ReportRepo>();

                services.AddScoped(typeof(MedicalRepReportHandlers));


                /**end of Inject services */
                /**Add AutoMapper*/
                services.AddAutoMapper(typeof(AutoMapping));
                /**end of adding Mapper*/
                /**start of JWT Settings*/
                var jwtSettings = new JwtSettings();
                Configuration.Bind(nameof(JwtSettings),jwtSettings);
                services.AddSingleton(jwtSettings);
                var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

                services.AddAuthentication(x =>
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
                            ValidateAudience = false,
                            ValidateIssuer = false,
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero
                        };
                    });
                /**end of Jwt Settings*/
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
