using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace WebApi_Scrapping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmazonDataController : Controller
    {
        [HttpGet]
        public IActionResult AmazonData()
        {
            string url = "https://www.amazon.in/Samsung-Storage-5000mAh-Snapdragon-Security/dp/B0CYQ3SDSH/ref=sr_1_1_sspa?dib=eyJ2IjoiMSJ9.WQFB0UkhwDycWo1WwthZfpPJW1UM3_fqIeNWj6Ly6tgwjbRoXdEmC62NHwg2Ar8QQH_Hdu7ZkbNmxq2u7cPD0CVaYnTvjMyTImf8zuMU59rGSXGhxUVU43uiyrIkFw0h3l9ifygPaBR-1QG7Mtj6X-LspKWOQ9sdP0jX5NRzIjI2LJVWPdHg-FH31A1qupdHKl7Kj7bd9p_q9pksKD1feMI85ylkbm98ZF0BCe9rzjk.zyIDY1oAkNm2A9kvACp13ZR67ObeisZaNYwhupaF3BA&dib_tag=se&keywords=mobile&qid=1713100066&sr=8-1-spons&sp_csd=d2lkZ2V0TmFtZT1zcF9hdGY&th=1";

            var books = new List<Book>();
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            var book = new Book();

            var titleNode = doc.DocumentNode.SelectSingleNode("//span[contains(@class,\"aok-offscreen\"]");
            book.Title= titleNode.InnerText.Trim();

            var priceNode = doc.DocumentNode.SelectSingleNode("//span[@id='priceblock_ourprice']");
            book.Price = priceNode.InnerText.Trim();
            books.Add(book);
            if (books != null)
            {
                return Ok(books);
            }
            else
            {
                return NotFound("No matching nodes found.");
            }

        }
    }
}
