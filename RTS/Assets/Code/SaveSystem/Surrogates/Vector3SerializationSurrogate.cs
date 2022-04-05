using System.Runtime.Serialization;
using Code.HelperClasses;
using UnityEngine;

namespace Code.SaveSystem.Surrogates
{
    public class Vector3SerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var v3 = (Vector3)obj;
            info.AddValue("x", v3.x);
            info.AddValue("y", v3.y);
            info.AddValue("z", v3.z);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var v3 = (Vector3)obj;
            v3.x = info.GetValue<float>("x");
            v3.y = info.GetValue<float>("y");
            v3.z = info.GetValue<float>("z");
            return obj = v3;
        }
    }
}
