namespace DevCore.Remember {
    using Microsoft.AspNetCore.Mvc;
    using System;

    public class ManageController : Controller {
        public IActionResult Index() {
            //Console.WriteLine($"Accounting/Input Controller");
            return View();
        }
    }
}