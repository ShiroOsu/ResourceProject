using System.Runtime.Serialization;

namespace Code.Tools.CreateSerializationSurrogate
{
    public class name : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            return obj;
        }
    }
}