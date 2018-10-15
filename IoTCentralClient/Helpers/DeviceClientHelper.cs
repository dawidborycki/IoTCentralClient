#region Using

using Microsoft.Azure.Devices.Client;

#endregion

namespace IoTCentralClient.Helpers
{
public static class DeviceClientHelper
{
        #region Fields

        private static readonly string connectionString
        = "HostName=saas-iothub-28681fd2-94c7-4938-bf7e-7ae3e94a407c.azure-devices.net;DeviceId=msdn-device-id1;SharedAccessKey=nQqFzf6TvnQA+zFI4MVaSSBeZgsYSY0P7KXrl6z6oDE=";

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
