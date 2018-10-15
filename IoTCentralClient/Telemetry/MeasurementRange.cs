namespace IoTCentralClient.Telemetry
{
    public class MeasurementRange
    {
        public double Min { get; set; }
        public double Max { get; set; }

        public double ValueRange()
        {
            return Max - Min;
        }
    }
}
