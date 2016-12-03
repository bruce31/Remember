namespace DevCore.Remember {
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Models;
    public class ManageController : Controller {
        public IActionResult Index() {
            Console.WriteLine($"Accounting/Input Controller");
            var x = new SystemLoginDetails() {SystemName = "Netscape"};
            return View(x);
        }

        [HttpGet("/Service/{area}", Name="Login")]
        public IActionResult Login(string area) {

            var x = new SystemLoginDetails() {SystemName = area};
            return View(x);
        }
    }
}