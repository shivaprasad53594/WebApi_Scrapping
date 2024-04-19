using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Reflection;
using static System.Reflection.Metadata.BlobBuilder;
using System.Reflection.Metadata;

namespace WebApi_Scrapping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmartPhonesDataController : ControllerBase
    {
        [HttpGet]
        public IActionResult PhoneDetails(string url)
        {

            //var url = "https://www.smartprix.com/mobiles";
            var bookLinks = GetBookLinks(url);
            List<Mobile> mobiles = GetBookDetails(bookLinks);
            if (mobiles != null)
            {
                return Ok(mobiles);
            }
            else
            {
                return NotFound("No matching nodes found.");
            }
        }
        static List<string> GetBookLinks(string url)
        {
            var bookLinks = new List<string>();
            HtmlDocument doc = GetDocument(url);
            HtmlNodeCollection linkNodes = doc.DocumentNode.SelectNodes("//a[@class='name clamp-2']");
            var baseUri = new Uri(url);
            foreach (var link in linkNodes)
            {
                string href = link.Attributes["href"].Value;
                bookLinks.Add(new Uri(baseUri, href).AbsoluteUri);
            }
            return bookLinks;
        }
        static HtmlDocument GetDocument(string url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            return doc;
        }

        //Get Each Book Deatails
        static List<Mobile> GetBookDetails(List<string> urls)
        {
            var books = new List<Mobile>();
            foreach (var url in urls)
            {
                HtmlDocument document = GetDocument(url);
                var titleXPath = "//h1";
                var priceXPath = "//strong";
                var book = new Mobile();
                book.MobileName = document.DocumentNode.SelectSingleNode(titleXPath).InnerText;
                book.Price = document.DocumentNode.SelectSingleNode(priceXPath).InnerText;
                books.Add(book);
            }
            return books;
        }
    }

}
