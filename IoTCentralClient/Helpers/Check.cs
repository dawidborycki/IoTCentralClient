#region Using

using System;

#endregion

namespace IoTCentralClient.Helpers
{
    public static class Check
    {
        #region Methods

        public static void IsNull(object obj)
        {
            if(obj == null)
            {
                throw new ArgumentNullException();
            }
        }

        #endregion
    }
}
