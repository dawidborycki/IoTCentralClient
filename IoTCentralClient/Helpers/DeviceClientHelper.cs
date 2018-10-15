#region Using

using Microsoft.Azure.Devices.Client;

#endregion

namespace IoTCentralClient.Helpers
{
    public static class DeviceClientHelper
    {
        #region Fields

        private static readonly string connectionString
            = "<your connection string>";

        private static DeviceClient deviceClient;

        #endregion

        #region Methods

        public static DeviceClient Init()
        {
            if (deviceClient == null)
            {
                deviceClient = DeviceClient.CreateFromConnectionString(connectionString);
            }

            return deviceClient;
        }

        #endregion
    }
}
