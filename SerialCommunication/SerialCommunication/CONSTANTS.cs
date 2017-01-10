using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialCommunication
{
    public static class CONSTANTS
    {
        public const int BaudRate = 19200;
        public const string DefaultCOM = "COM4";
        public const byte StartByte = 0x07;
        public const byte EndByte = 0x03;

        public const string Disconnected = "Disconnected";
        public const string Connected = "Connected";
        public static Color Color_Connected = Color.Green;
        public static Color Color_Disconnected = Color.Red;

        /// <summary>
        /// Communication interval - 1000 ms 
        /// </summary>
        public static int CommInterval = 1000;
        /// <summary>
        /// How often are data inserted into DB (60 s)
        /// </summary>
        public static int DbInsertPeriod = 60;
        /// <summary>
        /// How many seconds elapsed since last DB Insert
        /// </summary>
        public static int ElapsedSeconds = 0;


        public enum Command
        {
            Sonda1Data,
            Sonda2Data,
            Timestamp
        }

        /// <summary>
        /// Contains commands paired with command type keys for easy command sending.
        /// Commands are random (but correct) byte arrays that were, for lack of previous documentation,
        /// succesfully aquired through serial port listening and testing.
        /// </summary>
        public static Dictionary<Command, byte[]> DataCommandType = new Dictionary<Command, byte[]>
        {
            { Command.Sonda1Data, new byte[] { 0x07, 0x30, 0x31, 0x52, 0x4D, 0x31, 0x33, 0x38, 0x03 } }, //01RM138 register (from AdvancedSerialPortMonitor)
            { Command.Sonda2Data, new byte[] { 0x07, 0x30, 0x31, 0x52, 0x4D, 0x32, 0x33, 0x39, 0x03 } }, //01RM239 register (from AdvancedSerialPortMonitor)
            { Command.Timestamp, new byte[] { 0x07, 0x30, 0x31, 0x5A, 0x52, 0x31, 0x34, 0x03 } } //01ZR14 register (from AdvancedSerialPortMonitor)
        };
    }
}
