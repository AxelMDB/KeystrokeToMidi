using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using RtMidi.Core;
using RtMidi.Core.Devices;
using RtMidi.Core.Messages;

namespace KeystrokeToMidi
{
    public class MainWindowModel : INotifyPropertyChanged
    {
        private string labelText;
        private ICommand buttonCommand;
        private MidiDeviceManager deviceManager;
        private List<IMidiOutputDevice> outputDevices;

        public event PropertyChangedEventHandler PropertyChanged;
        public string LabelText
        {
            get { return labelText; }
            set 
            { 
                labelText = value;
                NotifyPropertyChanged();
            }
        }
        public ICommand ButtonCommand
        {
            get { return buttonCommand; }
            private set
            {
                buttonCommand = value;
                NotifyPropertyChanged();
            }
        }
        public MainWindowModel()
        {
            LabelText = "Hello";
            outputDevices = new List<IMidiOutputDevice>();
            ButtonCommand = new Command(onButton_Click, CanExecute);

            foreach (var device in MidiDeviceManager.Default.OutputDevices)
            {
                var newDevice = device.CreateDevice();
                newDevice.Open();
                outputDevices.Add(newDevice);
            }
            
        }
        private bool CanExecute(object value)
        {
            return true;
        }
        private void onButton_Click(object obj)
        {
            LabelText = ":>";
            var ProgramChange = new ProgramChangeMessage(RtMidi.Core.Enums.Channel.Channel1, 2);
            outputDevices[1].Send(ProgramChange);
        }

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
