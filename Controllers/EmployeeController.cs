using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using testing1.Models;



namespace testing1.Controllers;

public class EmployeeController : Controller
{
    private readonly ILogger<EmployeeController> _logger;

    public EmployeeController(ILogger<EmployeeController> logger)
    {
        _logger = logger;
    }


     public IActionResult Index()   
        {  
            FormClass emp = new();  
            return View(emp);  
        } 

        [HttpPost]  
        [ValidateAntiForgeryToken]
        public IActionResult SubmitEmp(FormClass emp)  
        {
            return RedirectToAction("EmpDetails", emp);
           // return View();
        } 


     public IActionResult EmpDetails(FormClass emp)  
    {
        return View(emp);
     }  

     [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}





