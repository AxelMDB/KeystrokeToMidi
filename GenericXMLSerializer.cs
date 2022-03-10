using ExtendedXmlSerializer;
using System.IO;
using ExtendedXmlSerializer.Configuration;
using System.Xml;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Collections.Generic;
using KeystrokeToMidi.Midi;
using System.Windows.Input;

namespace KeystrokeToMidi
{
    public class GenericXMLSerializer
    {

        public static string Write<T>(T objectToSerialize, string filename) where T : new()
        {
            IExtendedXmlSerializer serializer = GetSerializer(typeof(T));
            Stream stream = new FileStream(filename, FileMode.Create);
            string result = serializer.Serialize(new XmlWriterSettings { Indent = true }, stream, objectToSerialize);
            stream.Close();
            return result;
        }

        public static T Read<T>(string filename) where T : new()
        {
            IExtendedXmlSerializer serializer = GetSerializer(typeof(T));
            Stream stream = new FileStream(filename, FileMode.Open);
            T @object = serializer.Deserialize<T>(stream);
            stream.Close();
            return @object;
        }

        public static string WriteConfigs(Preset configs, string filename)
        {
            IExtendedXmlSerializer serializer = GetConfigsSerializer();
            Stream stream = new FileStream(filename, FileMode.Create);
            string result = serializer.Serialize(new XmlWriterSettings { Indent = true, }, stream, configs);
            stream.Close();
            return result;
        }

        private static IExtendedXmlSerializer GetSerializer(System.Type type)
        {
            return new ConfigurationContainer().UseAutoFormatting()
                                                    .EnableImplicitTyping(type)
                                                    .EnableReferences()
                                                    // Additional configurations...
                                                    .Create();
        }

        private static IExtendedXmlSerializer GetConfigsSerializer()
        {
            var type = typeof(Preset);
            var ignore1 = typeof(MessageTypes[]).GetTypeInfo();
            var ignore2 = typeof(Key[]).GetTypeInfo();

            return new ConfigurationContainer().UseAutoFormatting()
                                                    .UseOptimizedNamespaces()
                                                    .EnableImplicitTyping(type)
                                                    .EnableReferences()
                                                    .Ignore(ignore1)
                                                    .Ignore(ignore2)
                                                    // Additional configurations...
                                                    .Create();
        }
    }
}
