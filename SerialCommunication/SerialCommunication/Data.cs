using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            Date = parseTimestamp(timestamp);
        }

        /// <summary>
        /// Tries to parse the timestamp from device. If successfull, return the DateTime formatted string. 
        /// If parsing fails, return the default unix time date time
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        private string parseTimestamp(string datetime)
        {
            string year; string month; string day;
            string hour; string min; string sec;
            try
            {
                /*  01ZR 17011202155673  */
                datetime = datetime.Remove(0, 4); //removes the registry name from start of string (01ZR)
                year = "20" + datetime[0] + "" + datetime[1] + "";
                month = datetime[2] + "" + datetime[3] + "";
                day = datetime[4] + "" + datetime[5] + "";
                hour = datetime[6] + "" + datetime[7] + "";
                min = datetime[8] + "" + datetime[9] + "";
                sec = datetime[10] + "" + datetime[11] + "";
                return year + "-" + month + "-" + day + " " + hour + ":" + min + ":" + sec;
            }
            catch (Exception e)
            {
                return "1970-01-01 00:00:00"; //returns a default unixtime datetime
            }
        }
    }
}
