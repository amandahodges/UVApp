using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace UVApp
{
    class OpenUVInfo
    {
        public async static Task<Root> GetUVData(double lat, double lon)
        {
            var http = new HttpClient();
            http.DefaultRequestHeaders.Add("x-access-token", "APP_KEY_HERE");
            var url = String.Format("https://api.openuv.io/api/v1/uv?lat={0}&lng={1}", lat, lon);
            var response = await http.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();

            var settings = new DataContractJsonSerializerSettings
            {
                DateTimeFormat = new System.Runtime.Serialization.DateTimeFormat("yyyy-MM-ddTHH:mm:ss.fffK")
            };
            var serializer = new DataContractJsonSerializer(typeof(Root), settings);

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (Root)serializer.ReadObject(ms);

            return data;
        }
    }

    [DataContract]
    public class SafeExposureTime
    {
        [DataMember]
        public int st1 { get; set; }
        [DataMember]
        public int st2 { get; set; }
        [DataMember]
        public int st3 { get; set; }
        [DataMember]
        public int st4 { get; set; }
        [DataMember]
        public int st5 { get; set; }
        [DataMember]
        public int st6 { get; set; }
    }

    [DataContract]
    public class SunTimes
    {
        [DataMember]
        public DateTime solarNoon { get; set; }
        [DataMember]
        public DateTime nadir { get; set; }
        [DataMember]
        public DateTime sunrise { get; set; }
        [DataMember]
        public DateTime sunset { get; set; }
        [DataMember]
        public DateTime sunriseEnd { get; set; }
        [DataMember]
        public DateTime sunsetStart { get; set; }
        [DataMember]
        public DateTime dawn { get; set; }
        [DataMember]
        public DateTime dusk { get; set; }
        [DataMember]
        public DateTime nauticalDawn { get; set; }
        [DataMember]
        public DateTime nauticalDusk { get; set; }
        [DataMember]
        public DateTime nightEnd { get; set; }
        [DataMember]
        public DateTime night { get; set; }
        [DataMember]
        public DateTime goldenHourEnd { get; set; }
        [DataMember]
        public DateTime goldenHour { get; set; }
    }

    [DataContract]
    public class SunPosition
    {
        [DataMember]
        public double azimuth { get; set; }
        [DataMember]
        public double altitude { get; set; }
    }

    [DataContract]
    public class SunInfo
    {
        [DataMember]
        public SunTimes sun_times { get; set; }
        [DataMember]
        public SunPosition sun_position { get; set; }
    }

    [DataContract]
    public class Result
    {
        [DataMember]
        public double uv { get; set; }
        [DataMember]
        public DateTime uv_time { get; set; }
        [DataMember]
        public double uv_max { get; set; }
        [DataMember]
        public DateTime uv_max_time { get; set; }
        [DataMember]
        public double ozone { get; set; }
        [DataMember]
        public DateTime ozone_time { get; set; }
        [DataMember]
        public SafeExposureTime safe_exposure_time { get; set; }
        [DataMember]
        public SunInfo sun_info { get; set; }
    }

    [DataContract]
    public class Root
    {
        [DataMember]
        public Result result { get; set; }
    }

}
