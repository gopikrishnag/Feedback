using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Feedback.Helpers.CacheHelper;
using Feedback.Models.Settings;
using Feedback.Repository.DbContextes;
using Feedback.Repository.Repository;
using Feedback.Services.AddressService;
using Feedback.Services.FeedbackService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ZNetCS.AspNetCore.Authentication.Basic;
using ZNetCS.AspNetCore.Authentication.Basic.Events;

namespace Feedback.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        private bool EnableAutoDatabaseMigration { get; set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();

            services.AddDistributedMemoryCache();

            var webSettings = new WebSettings();
            Configuration.GetSection("WebSettings").Bind(webSettings);
            services.AddSingleton(webSettings);

            EnableAutoDatabaseMigration = webSettings.EnableAutoDatabaseMigration;

            services
               .AddAuthentication(BasicAuthenticationDefaults.AuthenticationScheme)
                //TODO: Move to dedicate class
               .AddBasicAuthentication(
                                       options =>
                                       {
                                           options.Realm = "My Application";
                                           options.Events = new BasicAuthenticationEvents
                                           {
                                               OnValidatePrincipal = context =>
                                                  {
                                                      if ((context.UserName == webSettings.Username) && (context.Password == webSettings.Password))
                                                      {
                                                          var claims = new List<Claim>
                                                              {
                                                                new Claim(ClaimTypes.Name, context.UserName, context.Options.ClaimsIssuer)
                                                              };
                                                          var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, context.Scheme.Name));
                                                          context.Principal = principal;
                                                      }
                                                      else
                                                      {
                                                          context.AuthenticationFailMessage = "Authentication failed.";
                                                      }

                                                      return Task.CompletedTask;
                                                  }
                                           };
                                       });


            var feedbackDbConnection = Configuration.GetConnectionString("FeedbackDBConnection");
            services.AddDbContext<FeedbackContext>(options => options.UseSqlServer(feedbackDbConnection));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddHttpClient<IAddressService, AddressService>();

            services.AddScoped<ICacheHelpers, CacheHelpers>();
            services.AddScoped<IFeedbackService, FeedbackService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, FeedbackContext dbFeedbackContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Feedback/Error");
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Feedback}/{action=Index}/{id?}");
            });

            if (EnableAutoDatabaseMigration)
            {
                dbFeedbackContext.Database.Migrate();
            }

        }
    }
}
