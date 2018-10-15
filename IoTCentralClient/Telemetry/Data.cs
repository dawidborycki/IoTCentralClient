using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Text;

namespace IoTCentralClient.Telemetry
{
    public class Data
    {
        public double Temperature { get; set; }

        public double Humidity { get; set; }

        public Message ToMessage()
        {
            var dataJson = JsonConvert.SerializeObject(this);

            return new Message(Encoding.ASCII.GetBytes(dataJson));
        }

        public override string ToString()
        {
            return $"Temperature: {Temperature:F2}, Humidity: {Humidity:F2}";
        }
    }
}
