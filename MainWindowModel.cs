using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using Commons.Music.Midi;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KeystrokeToMidi
{
    public class MainWindowModel : INotifyPropertyChanged
    {
        private string labelText;
        private ICommand buttonCommand;
        private IMidiAccess access;
        private IMidiOutput output;
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
            ButtonCommand = new Command(onButton_Click, CanExecute);
            access = MidiAccessManager.Default;
            output = access.OpenOutputAsync(access.Outputs.Last().Id).Result;

            //output.CloseAsync();
        }
        private bool CanExecute(object value)
        {
            return true;
        }
        private void onButton_Click(object obj)
        {
            LabelText = ":>";
            output.Send(new byte[] {MidiEvent.Program, 1}, 0, 0, 0);
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
