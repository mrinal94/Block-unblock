using testing1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace testing1.Controllers;
public class ClientTryController : Controller
{
    public IActionResult Index(string? response)
    {
        ClientVM clientVM = new()
        {
            ResponseJson = response
        };
        return View(clientVM);

        
    }

    [HttpPost]
    public IActionResult Block_UnBlock(ClientVM model)
    {
        string response = UpdateClientStatus(model, model._Type);
        return RedirectToAction("Index", new { response });
    }

    private string UpdateClientStatus(ClientVM model, string action)
    {
        string result_Response = string.Empty;
        var obj = new
        {
            model.who,
            model._as
        };
        var jsondata = JsonConvert.SerializeObject(obj);
        jsondata = jsondata.Replace("_", "");

        var uri = "http://44.215.98.9:18083/api/v5/banned";
        using (HttpClient client = new HttpClient())
        {
            //This is the key section you were missing    
            var plainTextBytes = Encoding.UTF8.GetBytes("632bbca9852546f0:KXp6athuUHvlDG7u8kbcJmd2NyYXibtpPqy5krfUYVC");
            string val = Convert.ToBase64String(plainTextBytes);
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + val);

            var httpContent = new StringContent(jsondata, Encoding.UTF8, "application/json");

            if (action.Contains("1"))
            {
                var postTask = client.PostAsync(uri, httpContent);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    result_Response = result.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    result_Response = "Eror";
                }
            }
            else
            {
                uri = uri + "/" + model._as + "/" + model.who;
                var postTask = client.DeleteAsync(uri);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    result_Response = "Unblock Done";
                }
                else
                {
                    result_Response = "Eror";
                }
            }
        }
        return result_Response;
    }
}