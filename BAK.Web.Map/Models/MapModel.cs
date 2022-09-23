using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BAK.Web.Map.Models
{
    public partial class MapModel
    {
        [JsonProperty("Points")]
        public List<Point> Points { get; set; }
    }

    public partial class Point
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Number")]
        public long Number { get; set; }

        [JsonProperty("Lat")]
        public decimal Lat { get; set; }

        [JsonProperty("Lon")]
        public decimal Lon { get; set; }
    }

}
