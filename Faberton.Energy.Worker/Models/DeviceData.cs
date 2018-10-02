using System;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace Faberton.Energy.Worker.Models
{
    public class DeviceData : TableEntity
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