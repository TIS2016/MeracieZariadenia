using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialCommunication
{
    public class Data
    {
        public ushort Day { get; set; }
        public ushort Month { get; set; }
        public ushort Year { get; set; }
        public ushort ProbeID { get; set; }
        public double Value { get; set; }

        public Data(ushort sondaID, double sondaValue, string timestamp)
        {
            //Todo rozparsovat datum, teraz sa mi nechce
            ProbeID = sondaID;
            Value = sondaValue;
        }
    }
}
