using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System;
using System.Security.Cryptography.X509Certificates;

namespace WebApi_Scrapping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmartPhonesDataController : ControllerBase
    {
        [HttpGet]
        public IActionResult PhoneDetails()
        {

            var response = "https://www.smartprix.com/mobiles";
            //HtmlWeb web = new HtmlWeb();
            //HtmlDocument doc = web.Load(response);
            //List<Mobile> mobiles = GetBookDetails(doc);
            ////var moname= doc.DocumentNode.SelectNodes("//a[@class='name clamp-2']/h2");
            ////var price = doc.DocumentNode.SelectNodes("//span[@class='price']");
            List<Mobile> mobiles = GetBookDetails(response);
            if (mobiles != null)
            {
                return Ok(mobiles);
            }
            else
            {
                return NotFound("No matching nodes found.");
            }

        }
        static HtmlDocument GetDocument(string url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            return doc;
        }

        static List<Mobile> GetBookDetails(string urls)
        {
            var mobiles = new List<Mobile>();

                HtmlDocument document = GetDocument(urls);
                var titleXPath = "//a[@class='name clamp-2']/h2";
                var priceXPath = "//span[@class='price']";
                var mobile = new Mobile();

                var a = document.DocumentNode.SelectNodes(titleXPath);
                var s = document.DocumentNode.SelectNodes(priceXPath);
            var s1 = new List<Mobile>()
            {

            };
            mobiles.Add(mobile);
            return mobiles;
        }
    }

}
