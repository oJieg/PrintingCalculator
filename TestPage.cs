using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
namespace printing_calculator
{

    public class TestPage
    {
        public async Task Test(HttpContext context)
        {
            context.Response.ContentType = "text/html; charset=utf-8";
            if (context.Request.Path == "/1")
            {
                IFormCollection form = context.Request.Form;
                string one = form["one"];
                string two = form["two"];



                await context.Response.WriteAsync(HTML());
            }
           // HomeController controller = new HomeController();
            
          //  await context.Response.SendFileAsync("html/TestHTML.html");
            await context.Response.WriteAsync(HTML());
            IndexModel indexModel = new IndexModel();
            //IActionResult
            // await context.Response.SendFileAsync(controller.v);
            // HttpRequest request = context.Request;

        }
        static string HTML()
        {
            string fullHtmlCode = "<!DOCTYPE html><html><head>";
            fullHtmlCode += "<title>Главная страница</title>";
            fullHtmlCode += "<meta charset=utf-8 />";
            fullHtmlCode += "</head> <body>";
            fullHtmlCode += "<h2>test2_2</h2>";
            fullHtmlCode += "</body></html>";
            return fullHtmlCode;
        }
    }

}
