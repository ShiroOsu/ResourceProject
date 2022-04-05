using System.Runtime.Serialization;
using Code.HelperClasses;
using UnityEngine;

namespace Code.SaveSystem.Surrogates
{
    public class QuaternionSerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var q = (Quaternion)obj;
            info.AddValue("x", q.x);
            info.AddValue("y", q.y);
            info.AddValue("z", q.z);
            info.AddValue("w", q.w);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var q = (Quaternion)obj;
            q.x = info.GetValue<float>("x");
            q.y = info.GetValue<float>("y");
            q.z = info.GetValue<float>("z");
            q.w = info.GetValue<float>("w");
            return obj = q;
        }
    }
}