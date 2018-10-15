using System;

namespace IoTCentralClient.Helpers
{
    public static class Check
    {
        public static void IsNull(object obj)
        {
            if(obj == null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}
