namespace Accounting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
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
			services.AddMemoryCache();
        }
		
		private IMemoryCache mCache;

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IMemoryCache cache)
        {
			mCache = cache;
			
            app.Run(ProcessResponse);
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
			return string.Join(Environment.NewLine, HtmlDoc.GenerateTopAndTail (FormElements()));
		}
		
		public IEnumerable<string> FormElements() {
			yield return "<Form name='CCEntry' action='/return' method='POST'>";
			yield return "<table>";
			yield return "        <tr><td>Description</td><td><input type='text' name='Desc'></td></tr>";
			yield return "        <tr><td>Date</td><td><input type='date' name='Date'></td></tr>";
			yield return "        <tr><td>Amount</td><td><input type='text' name='Amount'></td></tr>";
			yield return "        <tr><td></td><td><input type='submit'></td></tr>";
			yield return "    </table>";
			yield return "</Form>";
		}
	}
}