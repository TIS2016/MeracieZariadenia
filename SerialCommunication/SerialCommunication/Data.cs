using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialCommunication
{
    public class Data
    {
        public string Date { get; set; }
        public ushort ProbeID { get; set; }
        public double Value { get; set; }

        public Data(ushort sondaID, double sondaValue, string timestamp)
        {           
            ProbeID = sondaID;
            Value = sondaValue;
            Date = timestamp;
        }
    }
}
