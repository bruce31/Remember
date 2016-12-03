namespace Accounting
{
    using System;
    using System.Diagnostics;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using Microsoft.Extensions.Caching.Memory;
    using System.IO;

    public class Startup {

		public static void Main(string[] args) {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
			services.AddMemoryCache()
					.AddMvc(options =>
{
    options.Filters.Add(new UnhandledExceptionFilter());
});
        }
		
		private IMemoryCache mCache;

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IMemoryCache cache, ILoggerFactory log) {
			Debug.Write($"Startup debug output:  {env.EnvironmentName}");
			mCache = cache;
			//app.AddConsole(Configuration.GetSection("Logging"));
			//log.AddDebug();
			log.CreateLogger("Startup")                     // add
        	   .LogInformation($"Startup: {env.EnvironmentName}");   // this

			// log.AddConsole();
			// log.AddDebug();
			Debug.Write($"Startup debug output:  {env.EnvironmentName}");
			Console.WriteLine($"Startup debug output:  {env.EnvironmentName}");
			//app.UseMiddleware<DeveloperExceptionPageMiddleware>();

			app.UseDeveloperExceptionPage();
			app.UseMvc( routes => {
				routes.MapRoute("default", "{controller=Manage}/{action=Index}/{id?}");
			});
            //app.Run(ProcessResponse);
        }
	}

	public class UnhandledExceptionFilter : ExceptionFilterAttribute {
    	public override void OnException(ExceptionContext context) {

            // Unhandled errors
            // #if !DEBUG
            //     var msg = "An unhandled error occurred.";                
            //     string stack = null;
            // #else
                var msg = context.Exception.GetBaseException().Message;
                string stack = context.Exception.StackTrace;
            //#endif

			Console.WriteLine(msg);
			Console.WriteLine(stack);

            //context.HttpContext.Response.StatusCode = 500;

            // handle logging here
        

        	base.OnException(context);
    	}
	}
}