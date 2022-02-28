using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using KeystrokeToMidi.Midi;


namespace KeystrokeToMidi
{
    public class MidiMessageConfig : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IMidiMessage midiMessage;
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
        public Key Key
        {
            get;
            set;
        }
        public List<IMidiMessage> MidiMessages
        {
            get => midiMessages;
        }
        public MidiMessageConfig()
        {
            CurrentMessage = MidiMessages.FirstOrDefault();
            Key = Key.Enter;
        }
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
