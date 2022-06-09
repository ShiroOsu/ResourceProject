using System.Runtime.Serialization;
using Code.Tools.HelperClasses;
using UnityEngine;

namespace Code.SaveSystem.Surrogates
{
    public class TransformSerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var t = (Transform)obj;
            var position = t.position;
            info.AddValue("posX", position.x);
            info.AddValue("posY", position.y);
            info.AddValue("posZ", position.z);

            var rotation = t.rotation;
            info.AddValue("rotX", rotation.x);
            info.AddValue("rotY", rotation.y);
            info.AddValue("rotZ", rotation.z);
            info.AddValue("rotW", rotation.w);

            var localScale = t.localScale;
            info.AddValue("scaleX", localScale.x);
            info.AddValue("scaleY", localScale.y);
            info.AddValue("scaleZ", localScale.z);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var t = (Transform)obj;
            t.position = new Vector3(
                info.GetValue<float>("posX"),
                info.GetValue<float>("posY"),
                info.GetValue<float>("posZ"));
            
            t.rotation = new Quaternion(
                info.GetValue<float>("rotX"),
                info.GetValue<float>("rotY"),
                info.GetValue<float>("rotZ"),
                info.GetValue<float>("rotW"));

            t.localScale = new Vector3(
                info.GetValue<float>("scaleX"),
                info.GetValue<float>("scaleY"),
                info.GetValue<float>("scaleZ"));

            return obj = t;
        }
    }
}