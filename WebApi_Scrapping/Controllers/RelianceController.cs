using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace WebApi_Scrapping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelianceController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetReliance(string url)
        {
            HttpClient client = new HttpClient();
            //string url = "https://www.reliancedigital.in/search?q=:relevance:productTags:affordable-mobiles";
            var res = client.GetStringAsync(url).Result;
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(res);
            HtmlNodeCollection linkNodes = htmlDocument.DocumentNode.SelectNodes("//div[@class='sp grid']/a");
            var address = new List<string>();

            foreach (var link in linkNodes)
            {
                string href = link.Attributes["href"].Value;
                address.Add("https://www.reliancedigital.in" + href);
            }

            var mobile = new List<Mobile>();

            foreach (var link in address)
            {
                var res2 = client.GetStringAsync(link).Result;
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(res2);
                var mob = new Mobile();
                mob.MobileName = htmlDoc.DocumentNode.SelectSingleNode("//h1[@class='pdp__title']").InnerText;
                mob.Price = htmlDoc.DocumentNode.SelectSingleNode("//ul/li[1]/ span/span[2]").InnerText;
                mobile.Add(mob);
            }
            return Ok(mobile);
        }
    }
}
