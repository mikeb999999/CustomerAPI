using CustomerAPI.Services;
using CustomerAPI.Repositories;
using Swashbuckle.AspNetCore.SwaggerUI;
using EFContext;

namespace CustomerAPI
{
    public class Startup
    {
        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            //Initialize this object
            Environment = environment;
            Configuration = configuration;
        }

        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        /// <summary> Configure all services </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            //Register the services
            this.RegisterDependencies(services);

            //Configure controllers and views
            services.AddControllersWithViews()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();

            //Register auto-mapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        /// <summary> Configure the application </summary>
        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();

            //Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                c.DocumentTitle = "Identity Server";
                c.InjectStylesheet("/swagger/lib/swagger.css");
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mike's Customer API");
                c.DefaultModelExpandDepth(2);
                c.DefaultModelRendering(ModelRendering.Example);
                c.DisplayRequestDuration();
                c.DocExpansion(DocExpansion.List);
                c.EnableDeepLinking();
                c.ShowExtensions();
                c.ShowCommonExtensions();
                c.EnableFilter();
                c.OAuthUsePkce();
            });

            // Now start the app
            app.UseRouting();
            app.UseDeveloperExceptionPage();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); //Routes for my API controllers
            });
        }

        /// <summary> Register the business dependencies </summary>
        private void RegisterDependencies(IServiceCollection services)
        {
            //Add the http resources
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Register the services
            services.AddTransient<ICustomerService, CustomerService>();

            //Register the repositories
            services.AddTransient<ICustomerRepository, CustomerRepository>();

            //Configure SQL Server
            services.AddDbContext<CustomerDbContext>();

            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.DescribeAllParametersInCamelCase();
                c.UseInlineDefinitionsForEnums();
            });


        }

    }
}