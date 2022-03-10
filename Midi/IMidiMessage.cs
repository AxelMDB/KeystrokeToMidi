using System;
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
