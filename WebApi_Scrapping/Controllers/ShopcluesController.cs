using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_Scrapping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopcluesController : ControllerBase
    {
        [HttpGet]
        public IActionResult ShopcluesDetails(string url)
        {

            HttpClient client = new HttpClient();
            //string url = "https://www.shopclues.com/smartphone-sales.html";
            var res = client.GetStringAsync(url).Result;
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(res);
            HtmlNodeCollection linkNodes = htmlDocument.DocumentNode.SelectNodes("//div[@class='column col3']/a[2]");
            var address = new List<string>();

            foreach (var link in linkNodes)
            {
                string href = link.Attributes["href"].Value;
                address.Add(href);
            }
            var mobile = new List<ShopcluesMobile>();
            foreach (var link in address)
            {
                var res2 = client.GetStringAsync("https://www.shopclues.com/" + link).Result;
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(res2);
                var mob = new ShopcluesMobile();
                mob.MobileName = htmlDoc.DocumentNode.SelectSingleNode("//h1").InnerText;
                mob.Price = htmlDoc.DocumentNode.SelectSingleNode("//span[@class='f_price']").InnerText.Replace("₹", String.Empty);
                mobile.Add(mob);
            }
            return Ok(mobile);
        }
    }
}
