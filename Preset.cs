using RtMidi.Core.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeystrokeToMidi
{
    public class Preset
    {
        public List<MidiMessageConfig> Configs { get; set; }
        public int ColumnCount { get; set; }
        public Channel Channel { get; set; }
        public System.Windows.Input.Key BankUpKey { get; set; }
        public System.Windows.Input.Key BankDownKey { get; set; }
        public Preset()
        {
            Configs = new();
        }
        public Preset(ObservableCollection<MidiMessageConfig> configs, int columnCount,
            Channel channel, System.Windows.Input.Key bankUpKey, System.Windows.Input.Key bankDownKey)
        {
            Configs = configs.ToList();
            ColumnCount = columnCount;
            Channel = channel;
            BankUpKey = bankUpKey;
            BankDownKey = bankDownKey;
        }
    }
}
