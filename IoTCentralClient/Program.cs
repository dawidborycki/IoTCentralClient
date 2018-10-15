#region Using

using IoTCentralClient.Helpers;
using IoTCentralClient.Telemetry;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using System;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace IoTCentralClient
{
    class Program
    {
        #region Fields

        private static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();        

        private static readonly string telemetryActivePropertyName = "IsTelemetryActive";
        private static readonly string propertyValue = "value";        

        #endregion

        static void Main(string[] args)
        {
            // Configure cancel key press handler (to stop the app)
            Console.CancelKeyPress += new ConsoleCancelEventHandler(CancelKeyPressHandler);

            // Connect to the cloud
            var deviceClient = DeviceClientHelper.Init();

            // Telemetry generator produces random temperature 
            // and humidity, and then sends them both to the cloud
            var telemetryGenerator = new Generator(
                deviceClient, cancellationTokenSource.Token);

            // Associate handler to update device properties according to cloud requests
            deviceClient.SetDesiredPropertyUpdateCallbackAsync(
                PropertyUpdateCallback, telemetryGenerator).Wait();

            // Start telemetry
            telemetryGenerator.Start().Wait();
        }

        private static void CancelKeyPressHandler(object sender, ConsoleCancelEventArgs e)
        {
            if (e.SpecialKey == ConsoleSpecialKey.ControlC)
            {
                cancellationTokenSource.Cancel();

                Environment.Exit(0);
            }
        }

        private static Task PropertyUpdateCallback(
            TwinCollection desiredProperties, object userContext)
        {
            if (desiredProperties.Contains(telemetryActivePropertyName))
            {
                var telemetryGenerator = userContext as Generator;

                telemetryGenerator.IsTelemetryActive =
                    desiredProperties[telemetryActivePropertyName][propertyValue];
            }

            return Task.CompletedTask;
        }
    }
}
