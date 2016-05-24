namespace HtmlUtil
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;

	using System.Net;
	using System.Net.Http;
    using System.IO;
    

    
	public static class HtmlDoc {


		public static IEnumerable<string>  GenerateTopAndTail (IEnumerable<string> elements) {
	
			yield return "<html><head></head><body>";

			foreach (var ele in elements) {
				yield return ele;
			}
			yield return "</body></html>";
		}
	}
}
