using CAPSTONEPROJECT.Models;
using CAPSTONEPROJECT.Services;
using CAPSTONEPROJECT.Settings;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using System;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CAPSTONEPROJECT
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

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.Configure<NotificationSettings>(Configuration.GetSection("NotificationSettings"));
            services.AddDbContext<LugContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("lug_Context")));

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                                  builder =>
                                  {
                                  builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                                    });
            });

            services.AddControllers();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                

            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                    ClockSkew = TimeSpan.FromMinutes(5)
                };

                x.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Lug API",
                        Description = "A simple example ASP.NET Core Web API",
                    });

                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "Enter ‘Bearer’ [space] and then your valid token in the text input below.",
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type= ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },

                            new string[]
                            {
                            }
                        }
                    });    
                });
            

            services.AddRouting(opt => opt.LowercaseUrls = true);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<EmployeeService>();
            services.AddScoped<PositionService>();
            services.AddScoped<WorkplaceService>();
            services.AddScoped<ContractSalaryService>();
            services.AddScoped<RoleService>();
            services.AddScoped<AccountService>();
       
            services.AddScoped<SalaryService>();
            services.AddScoped<WorkScheduleService>();
            services.AddScoped<ShiftService>();
            services.AddScoped<SkillService>();
        
            services.AddScoped<EmployeeSkillService>();
            services.AddScoped<ApplicationService>();
            services.AddScoped<LoginService>();
            services.AddScoped<ApplicationTypeService>();
            services.AddScoped<CheckService>();
            services.AddScoped<DashboardService>();
            services.AddScoped<MailService>();
            services.AddScoped<CountService>();
            services.AddScoped<EmployeeTypeService>();
            services.AddScoped<FeedbackService>();
            services.AddScoped<TotalDayService>();
            services.AddScoped<BackupSalaryService>();
            services.AddScoped<SystemSettingService>();
            services.AddScoped<WorkScheduleStatusService>();
            services.AddScoped<NotificationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lug System API");
                c.RoutePrefix = string.Empty;
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
