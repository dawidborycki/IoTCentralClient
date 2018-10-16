#region Using

using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Text;

#endregion

namespace IoTCentralClient.Telemetry
{
    public class Data
    {
        #region Properties

        public double Temperature { get; set; }

        public double Humidity { get; set; }

        #endregion

        #region Methods

        public Message ToMessage()
        {
            var dataJson = JsonConvert.SerializeObject(this);

            return new Message(Encoding.ASCII.GetBytes(dataJson));
        }

        public override string ToString()
        {
            return $"Temperature: {Temperature,6:F2}, Humidity: {Humidity,6:F2}";
        }

        #endregion
    }
}
