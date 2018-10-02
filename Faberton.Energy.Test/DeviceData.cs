using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Faberton.Energy.Test
{
    public class DeviceData
    {
        [JsonProperty("id_device")]
        public string DeviceId { get; set; }

        [JsonProperty("id_identifier")]
        public string Identifier { get; set; }

        [JsonProperty("dateDevice")]
        public String Date { get; set; }

        [JsonProperty("watts")]
        public double Watts { get; set; }

        [JsonProperty("irms")]
        public double IRMS { get; set; }
    }
}
