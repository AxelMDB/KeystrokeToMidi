using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

using RtMidi.Core;
using RtMidi.Core.Devices;
using RtMidi.Core.Enums;


using KeystrokeToMidi.Midi;
using System.Windows.Controls;

namespace KeystrokeToMidi
{
    public class MainWindowModel : INotifyPropertyChanged
    {

        private ObservableCollection<IMidiOutputDevice> outputDevices;
        private IMidiOutputDevice currentOutputDevice;
        private ObservableCollection<MidiMessageConfig> messageConfigs;
        private Channel channel;
        private int columnCount;
        public event PropertyChangedEventHandler PropertyChanged;
        private ICommand buttonCommand;
        private ICommand addOrRemoveCommand;
        private ICommand bankSelectCommand;
        public int ColumnCount
        {
            get => columnCount;
            set
            {
                columnCount = value;
                NotifyPropertyChanged();
            }
        }
        public Channel Channel
        {
            get => channel;
            set
            {
                channel = value;
                NotifyPropertyChanged();
            }
        }
        public Channel[] EnumValues
        {
            get => Enum.GetValues<Channel>();
        }
        public ObservableCollection<MidiMessageConfig> MessageConfigs
        {
            get => messageConfigs;
            set
            {
                messageConfigs = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<IMidiOutputDevice> OutputDevices
        {
            get { return outputDevices; }
            set
            {
                outputDevices = value;
                NotifyPropertyChanged();
            }
        }
        public IMidiOutputDevice CurrentOutputDevice
        {
            get => currentOutputDevice;
            set
            {
                currentOutputDevice = value;
                NotifyPropertyChanged();
            }
        }
        public ICommand ButtonCommand
        {
            get => buttonCommand;
            set
            {
                buttonCommand = value;
                NotifyPropertyChanged();
            }
        }
        public ICommand AddOrRemoveCommand
        {
            get => addOrRemoveCommand;
            private set
            {
                addOrRemoveCommand = value;
                NotifyPropertyChanged();
            }
        }
        public ICommand BankSelectCommand
        {
            get => bankSelectCommand;
            set
            {
                bankSelectCommand = value;
                NotifyPropertyChanged();
            }
        }
        public MainWindowModel()
        {


            MessageConfigs = new ObservableCollection<MidiMessageConfig>();
            ColumnCount = 8;

            ButtonCommand = new Command(onButton_Click, CanExecute);
            AddOrRemoveCommand = new Command(addOrRemoveConfig, CanExecute);
            BankSelectCommand = new Command(BankSelect, CanExecute);

            Channel = EnumValues.First();

            OutputDevices = new ObservableCollection<IMidiOutputDevice>();
            foreach (var device in MidiDeviceManager.Default.OutputDevices)
            {
                var newDevice = device.CreateDevice();
                newDevice.Open();
                outputDevices.Add(newDevice);
            }

            CurrentOutputDevice = OutputDevices.FirstOrDefault();

        }

        private void onButton_Click(object sender)
        {
            var config = (MidiMessageConfig) sender;
            var message = config.CurrentMessage;
            bool success = message.Send(CurrentOutputDevice, Channel);
        }
        private void addOrRemoveConfig(object sender)
        {
            if (sender.ToString().Equals("Add"))
            {
                MessageConfigs.Add(new MidiMessageConfig());
            }
            else
            {
                if (MessageConfigs.Count != 0) MessageConfigs.RemoveAt(MessageConfigs.Count - 1);
            }
        }
        private void BankSelect(object sender)
        {
            var buttonName = (string) sender;

            var programConfigs = MessageConfigs.Where(c => c.CurrentMessage.MessageType == MessageTypes.ProgramChange);

            int? minimum = null;
            int maximum = 0;

            foreach (var config in programConfigs)
            {
                var current = config.CurrentMessage;
                if (current.Byte1 > maximum) maximum = current.Byte1;
                if (current.Byte1 < minimum || minimum == null) minimum = current.Byte1;
            }

            int change = maximum - (int) minimum + 1;

            if (buttonName.Equals("Down")) change *= -1;

            if (minimum + change < 0 || maximum + change > 127) return;

            foreach (var config in programConfigs)
            {
                config.CurrentMessage.Byte1 += (sbyte) change;
            }

        }
        private bool CanExecute(object obj)
        {
            return true;
            // ...obj is the optional command parameter.
            // ...return whether your command can execute.
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
