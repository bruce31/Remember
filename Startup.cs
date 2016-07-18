namespace Accounting
{
    using System;
	using System.Diagnostics;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
	
	using Newtonsoft.Json;
	
	using Microsoft.Extensions.Caching.Memory;

	using System.Net;
	using System.Net.Http;
    using System.IO;
    
	using HtmlUtil;
	
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddMemoryCache()
					.AddMvc();
        }
		
		private IMemoryCache mCache;

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IMemoryCache cache, ILoggerFactory log)
		{
       Debug.Write($"Startup debug output:  {env.EnvironmentName}");
			mCache = cache;
			//app.AddConsole(Configuration.GetSection("Logging"));
    //log.AddDebug();
    log.CreateLogger("Startup")                     // add
        .LogInformation($"Startup: {env.EnvironmentName}");   // this
			// log.AddConsole();
			// log.AddDebug();
			Debug.Write($"Startup debug output:  {env.EnvironmentName}");
			//app.UseMiddleware<DeveloperExceptionPageMiddleware>();
			
			app.UseMvc( routes => {
				routes.MapRoute("default", "{controller=Accounting}/{action=Index}/{id?}");
			});
            //app.Run(ProcessResponse);
        }
		
		public async Task ProcessResponse(HttpContext context) 
		{
			var exInfo = "";
			try {
                var path = context.Request.Path.Value;
                if (path.ToLower().Contains("/input"))
                {
					var inputForm = GetInputForm();
                    await context.Response.WriteAsync(inputForm);
                }
                else
                {
					var requestHtml = DisplayRequest(context.Request);
                    await context.Response.WriteAsync( requestHtml);
                }
			}
			catch(Exception ex) {
				
				var tmp = $"<html><head></head><body><p>{exInfo}</p><p>{ex}</p></body></html>";
				await context.Response.WriteAsync(tmp);
			}
        }
		
		public string DisplayRequest(HttpRequest req) {
			
			var json = "error";
			try {
			json = JsonConvert.SerializeObject(req.Form);
			} catch (Exception ex){
				json = ex.ToString();
			}
			var html = new [] {"<p><code>", json, "</code></p>"};
			return string.Join(Environment.NewLine, html);
		}

		public string GetInputForm ()
		{
			return "input form"; //string.Join(Environment.NewLine, HtmlDoc.GenerateTopAndTail (FormElements()));
		}
	}
}