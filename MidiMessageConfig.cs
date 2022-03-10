using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using KeystrokeToMidi.Midi;


namespace KeystrokeToMidi
{
    public class MidiMessageConfig : INotifyPropertyChanged, IComparable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IMidiMessage currentMessage;
        private Key currentKey;
        private MessageTypes currentMessageType;
        public static Key[] Keys
        {
            get => (Key[]) Enum.GetValues(typeof(Key));
        }
        public static MessageTypes[] MessageTypes
        {
            get => (MessageTypes[])Enum.GetValues(typeof(MessageTypes));
        }
        public IMidiMessage CurrentMessage
        {
            get => currentMessage;
            set
            {
                currentMessage = value;
                NotifyPropertyChanged();
            }
        }
        public MessageTypes CurrentMessageType
        {
            get => currentMessageType;
            set
            {
                currentMessageType = value;
                switch (value)
                {
                    case Midi.MessageTypes.ProgramChange:
                        CurrentMessage = new ProgramChange();
                        break;
                    case Midi.MessageTypes.ControlChange:
                        CurrentMessage = new ControlChange();
                        break;
                }
                NotifyPropertyChanged();
            }
        }
        public Key CurrentKey
        {
            get => currentKey;
            set
            {
                currentKey = value;
                NotifyPropertyChanged();
            }
        }
        public MidiMessageConfig()
        {
            CurrentKey = Keys.First();
            CurrentMessageType = MessageTypes.First();
        }
        public int CompareTo(object other)
        {
            if (other == null) return 1;
            MidiMessageConfig _other = other as MidiMessageConfig;
            if (_other != null)
                return this.CurrentMessage.CompareTo(_other.CurrentMessage);
            else
                throw new ArgumentException("Object is not MidiMessageConfig");
        }
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
