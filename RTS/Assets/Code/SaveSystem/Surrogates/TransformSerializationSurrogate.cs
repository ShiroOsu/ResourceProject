using System.Runtime.Serialization;
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
            info.AddValue("posY", position.x);
            info.AddValue("posZ", position.x);

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
                (float) info.GetValue("posX", typeof(float)),
                (float) info.GetValue("posY", typeof(float)),
                (float) info.GetValue("posZ", typeof(float)));
            
            t.rotation = new Quaternion(
                (float) info.GetValue("rotX", typeof(float)),
                (float) info.GetValue("rotY", typeof(float)),
                (float) info.GetValue("rotZ", typeof(float)),
                (float) info.GetValue("rotW", typeof(float)));

            t.localScale = new Vector3(
                (float) info.GetValue("scaleX", typeof(float)),
                (float) info.GetValue("scaleY", typeof(float)),
                (float) info.GetValue("scaleZ", typeof(float)));

            return obj = t;
        }
    }
}