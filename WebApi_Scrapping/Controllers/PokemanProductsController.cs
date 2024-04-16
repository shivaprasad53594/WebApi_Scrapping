//using HtmlAgilityPack;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Formats.Asn1;
//using System.Globalization;

//namespace WebApi_Scrapping.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PokemanProductsController : ControllerBase
//    {
//        public IActionResult PokemonData()
//        {
//            var web = new HtmlWeb();
//            // loading the target web page 
//            var document = web.Load("https://scrapeme.live/shop/");
//            var pokemonProducts = new List<PokemonProduct>();
//            // selecting all HTML product elements from the current page 
//            var productHTMLElements = document.DocumentNode.QuerySelectorAll("li.product");
//            // iterating over the list of product elements 
//            foreach (var productHTMLElement in productHTMLElements)
//            {
//                // scraping the interesting data from the current HTML element 
//                var url = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("a").Attributes["href"].Value);
//                var image = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("img").Attributes["src"].Value);
//                var name = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("h2").InnerText);
//                var price = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector(".price").InnerText);
//                // instancing a new PokemonProduct object 
//                var pokemonProduct = new PokemonProduct() { Url = url, Image = image, Name = name, Price = price };
//                // adding the object containing the scraped data to the list 
//                pokemonProducts.Add(pokemonProduct);
//            }
//            // initializing the CSV output file 
//            using (var writer = new StreamWriter("pokemon-products.csv"))
//            // initializing the CSV writer 
//            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
//            {
//                // populating the CSV file 
//                csv.WriteRecords(pokemonProducts);
//            }
//            return Ok(pokemonProducts);
//        }
//        public class PokemonProduct
//        {
//            public string? Url { get; set; }
//            public string? Image { get; set; }
//            public string? Name { get; set; }
//            public string? Price { get; set; }
//        }
//    }
//}
