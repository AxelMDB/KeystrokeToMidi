using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtMidi.Core;
using RtMidi.Core.Messages;
using RtMidi.Core.Enums;
using RtMidi.Core.Devices;

namespace KeystrokeToMidi.Midi
{
    public enum MessageTypes
    {
        ProgramChange,
        ControlChange
    }
    public interface IMidiMessage : IComparable
    {
        public MessageTypes MessageType { get; }
        public sbyte Byte1 { get; set; }
        public sbyte Byte2 { get; set; }
        public bool Send(IMidiOutputDevice device, Channel channel);
        public string ToString();
    }
}
