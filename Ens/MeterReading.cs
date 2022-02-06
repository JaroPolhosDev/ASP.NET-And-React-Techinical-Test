using System;

namespace Ens
{
    /// <summary>
    /// Meter Reading model class
    /// TODO: Needs to connect to Entity Framework for DB access
    /// </summary>
    public class MeterReading
    {
        /// <summary>
        /// Account id of the customer
        /// </summary>
        public int AccountID { get; set; }

        /// <summary>
        /// Date time representing when the reading was taken
        /// </summary>
        public DateTime MeterReadingTaken { get; set; }

        /// <summary>
        /// The actual meter reading value
        /// </summary>
        public int MeterReadingValue { get; set; }
    }

}