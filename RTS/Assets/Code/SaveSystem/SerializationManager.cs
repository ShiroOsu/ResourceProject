using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Code.Debugging;
using Code.SaveSystem.Surrogates;
using Newtonsoft.Json;
using UnityEngine;
using Directory = UnityEngine.Windows.Directory;

namespace Code.SaveSystem
{
    public static class SerializationManager
    {
        public static bool Save(string saveName, object data, int saveIndex)
        {
            SaveAsJson(saveName, data, saveIndex);
            var binaryFormatter = GetBinaryFormatter();

            if (!Directory.Exists(Application.persistentDataPath + "/saves"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/saves");
            }

            var path = Application.persistentDataPath + "/saves/" + saveName + $"{saveIndex}.save";
            var file = File.Create(path);
            binaryFormatter.Serialize(file, data);
            file.Close();
            Log.Print("SerializationManager.cs", "data saved to file: " + file.Name);
            return true;
        }

        // Debugging save files
        private static bool SaveAsJson(string saveName, object data, int saveIndex)
        {
            if (!Directory.Exists(Application.dataPath + "/saves"))
            {
                Directory.CreateDirectory(Application.dataPath + "/saves");
            }

            var path = Application.dataPath + "/saves/" + saveName + $"{saveIndex}.json";

            JsonSerializer serializer = new()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };

            using (var sw = new StreamWriter(File.Create(path)))
            using (var writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, data);
            }
        
            return true;
        }

        public static object Load(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }

            var formatter = GetBinaryFormatter();
            var file = File.Open(path, FileMode.Open);
        
            try
            {
                var save = formatter.Deserialize(file);
                file.Close();
                Log.Print("SerializationManager.cs", "data loaded from file: " + file.Name);
                return save;
            }
            catch (Exception e)
            {
                Debug.Log(e);
                file.Close();
                return null;
            }
        }
    
        private static BinaryFormatter GetBinaryFormatter()
        {
            var formatter = new BinaryFormatter();
            var selector = new SurrogateSelector();
    
            /*
             * Serialization Surrogates : Tells the binary formatter how to serialize a class.
             */
            var vector3Surrogate = new Vector3SerializationSurrogate();
            var quaternionSurrogate = new QuaternionSerializationSurrogate();
            var transformSurrogate = new TransformSerializationSurrogate();
    
            /*
             * You could potentially extend this further and create surrogates for all of the properties of
             * a MonoBehaviour, to make it completely serializable.
             * Maybe idk
             */
            selector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All),
                vector3Surrogate);
            selector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All),
                quaternionSurrogate);
            selector.AddSurrogate(typeof(Transform), new StreamingContext(StreamingContextStates.All),
                transformSurrogate);
    
            formatter.SurrogateSelector = selector;
    
            return formatter;
        }
    }
}
