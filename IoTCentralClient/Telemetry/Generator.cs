#region Using

using IoTCentralClient.Helpers;
using Microsoft.Azure.Devices.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace IoTCentralClient.Telemetry
{
    public class Generator
    {
        #region Fields

        // Device client and cancellation token
        private DeviceClient deviceClient;
        private CancellationToken cancellationToken;

        // Random generator and delay time
        private Random randomNumberGenerator = new Random();
        private TimeSpan delayTime = TimeSpan.FromSeconds(1);

        // Measurement range
        private readonly MeasurementRange temperatureRange
            = new MeasurementRange() { Min = -20, Max = 60 };
        private readonly MeasurementRange humidityRange
            = new MeasurementRange() { Min = 0, Max = 100 };

        // Telemetry task
        private Task telemetryTask;

        #endregion

        #region Properties

        public bool IsTelemetryActive { get; set; } = true;

        #endregion

        #region Constructor

        public Generator(DeviceClient deviceClient, 
            CancellationToken cancellationToken)
        {
            Check.IsNull(deviceClient);
            Check.IsNull(cancellationToken);

            this.deviceClient = deviceClient;
            this.cancellationToken = cancellationToken;
        }

        #endregion

        #region Methods        

        public Task Start()
        {
            telemetryTask = new Task(TelemetryAction);

            telemetryTask.Start();

            return telemetryTask;
        }

        private void TelemetryAction()
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var telemetryData = new Data()
                {
                    Temperature = GetRandomValue(temperatureRange),
                    Humidity = GetRandomValue(humidityRange)
                };

                if (IsTelemetryActive)
                {
                    deviceClient.SendEventAsync(telemetryData.ToMessage());

                    Console.WriteLine($"Sending telemetry: {telemetryData}");
                }
                else
                {
                    Console.WriteLine("Idle");
                }

                Task.Delay(delayTime).Wait();
            }
        }
        
        private double GetRandomValue(MeasurementRange measurementRange)
        {
            var randomValueRescaled = randomNumberGenerator.NextDouble()
                * measurementRange.ValueRange();

            return measurementRange.Min + randomValueRescaled;
        }

        #endregion
    }
}
