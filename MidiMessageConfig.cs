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
        private IMidiMessage midiMessage;
        private Key currentKey;
        private List<IMidiMessage> midiMessages = new()
        {
            new ProgramChange(),
            new ControlChange(),
        };
        public IMidiMessage CurrentMessage
        {
            get => midiMessage;
            set
            {
                midiMessage = value;
                NotifyPropertyChanged();
            }
        }
        public Key[] Keys
        {
            get => (Key[]) Enum.GetValues(typeof(Key));
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
        public List<IMidiMessage> MidiMessages
        {
            get => midiMessages;
        }
        public MidiMessageConfig()
        {
            CurrentMessage = MidiMessages.FirstOrDefault();
            CurrentKey = Keys.First();
        }
        public int CompareTo(object other)
        {
            if (other == null) return 1;
            MidiMessageConfig _other = other as MidiMessageConfig;
            if (_other != null)
                return this.CurrentMessage.CompareTo(_other.CurrentMessage);
            else
                throw new ArgumentException("Object is not Program Change");
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
