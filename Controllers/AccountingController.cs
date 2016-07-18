namespace Accounting {
    using Microsoft.AspNetCore.Mvc;
    //using System.Text.Encodings.Web;
    
    public class AccountingController : Controller {
        public IActionResult Index() {
            return View();
        }
        public IActionResult Input() {
            System.Diagnostics.Debug.WriteLine($"Accounting/Input Controller");
            return View();
        }
    }
}