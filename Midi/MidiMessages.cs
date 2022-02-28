using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RtMidi.Core.Devices;
using RtMidi.Core.Enums;
using RtMidi.Core.Messages;

namespace KeystrokeToMidi.Midi
{
    public class ProgramChange : IMidiMessage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private sbyte byte1;
        private sbyte byte2;
        public sbyte Byte1 
        {
            get => byte1;
            set
            {
                byte1 = value;
                NotifyPropertyChanged();
            }
        }
        public sbyte Byte2
        {
            get => byte2;
            set
            {
                byte2 = value;
                NotifyPropertyChanged();
            }
        }

        public MessageTypes MessageType { get { return MessageTypes.ProgramChange; } }

        public ProgramChange()
        {

        }
        public ProgramChange(sbyte byte1)
        {
            Byte1 = byte1;
            Byte2 = 0;
        }

        public override string ToString()
        {
            return MessageType.ToString();
        }
        public object GetMessage(Channel channel)
        {
            return new ProgramChangeMessage(channel, Byte1);
        }
        public bool Send(IMidiOutputDevice device, Channel channel)
        {
            ProgramChangeMessage message = new ProgramChangeMessage(channel, Byte1);
            return device.Send(message);
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class ControlChange : IMidiMessage
    {
        public sbyte Byte1 { get; set; }
        public sbyte Byte2 { get; set; }
        public MessageTypes MessageType { get { return MessageTypes.ControlChange; } }
        
        public ControlChange()
        {

        }
        public ControlChange(sbyte byte1)
        {
            Byte1 = byte1;
            Byte2 = 0;
        }

        public override string ToString()
        {
            return MessageType.ToString();
        }

        public bool Send(IMidiOutputDevice device, Channel channel)
        {
            ControlChangeMessage message = new ControlChangeMessage(channel, Byte1, Byte2);
            return device.Send(message);
        }
    }
}
