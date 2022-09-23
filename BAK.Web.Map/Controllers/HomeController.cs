
using BAK.Web.Map.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BAK.Web.Map.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, Route("DataWriter")]
        public async Task<bool> DataWriter([FromBody] object postData)
        {
            StreamWriter sw;
            string path = "points.json";
            var mapModel = new List<MapModel>();
            var map = JsonConvert.DeserializeObject<MapModel>(postData.ToString());
            

            mapModel.Add(map);
            string prJs = "";
            string json = JsonConvert.SerializeObject(mapModel, Formatting.Indented);
            using(StreamReader sr = new StreamReader(path))
            {
                prJs = sr.ReadToEnd().ToString();
            }


            if (System.IO.File.Exists(path))
            {
                var previousJson = JsonConvert.DeserializeObject<MapModel>(prJs);
                mapModel.Add(previousJson);
                string updatedjson = JsonConvert.SerializeObject(mapModel, Formatting.Indented);
                System.IO.File.WriteAllText(path, updatedjson);
            }
            else
            {
                System.IO.File.WriteAllText(path, json);
            }
            return true;
        }

        [HttpGet, Route("DataReader")]
        public async Task<MapModel> DataReader()
        {
            var mapModel = new MapModel();
            using (StreamReader r = new StreamReader("points.json"))
            {
                string json = r.ReadToEnd();
                mapModel = JsonConvert.DeserializeObject<MapModel>(json);
            }
            return await Task.FromResult(mapModel);
        }
    }
}