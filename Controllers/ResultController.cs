namespace Accounting {
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    
    public class ResultController : Controller {
        public string Index(object model) {
            Debug.WriteLine($"Result/Index controller");
            return JsonConvert.SerializeObject(model);
        }
        public IActionResult Input() {
            return View();
        }
    }
}