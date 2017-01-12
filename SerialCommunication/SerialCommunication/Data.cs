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
            Date = "20" + timestamp[4] + timestamp[5] + "-" + timestamp[6] + timestamp[7] + "-" + timestamp[8] + timestamp[9] + " " + timestamp[10] + timestamp[11] + ":" + timestamp[12] + timestamp[13] + ":" + timestamp[14] + timestamp[15];
        }
    }
}
