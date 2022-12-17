namespace Utils
{
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using UnityEngine;

    public class BinarySerializer
    {
        private static readonly SurrogateSelector surrogateSelector = GetSurrogateSelector();

        /// <summary>
        ///     Save data to disk.
        /// </summary>
        /// <param name="data">your dataClass instance.</param>
        /// <param name="filename">the file where you want to save data.</param>
        /// <returns></returns>
        public static void Save<T>(T data, string filename)
        {
            if(IsSerializable<T>())
            {
                var formatter = new BinaryFormatter();

                formatter.SurrogateSelector = surrogateSelector;

                var fs = new FileStream(filename, FileMode.Create);

                formatter.Serialize(fs, data);

                fs.Close();
            }
        }

        /// <summary>
        ///     Save data to disk.
        /// </summary>
        /// <param name="filePath">the file where you saved data.</param>
        /// <returns></returns>
        public static T Load<T>(string filePath)
        {
            var formatter = new BinaryFormatter();
            formatter.SurrogateSelector = surrogateSelector;
            var file = File.Open(filePath, FileMode.Open);

            try
            {
                return (T) formatter.Deserialize(file);
            }
            catch(SerializationException e)
            {
                Debug.Log("Failed to serialize. Reason: " + e.Message);
            }
            finally
            {
                file.Close();
            }

            return default;
        }

        private static bool IsSerializable<T>()
        {
            bool isSerializable = typeof(T).IsSerializable;
            if(!isSerializable)
            {
                string type = typeof(T).ToString();
                Debug.LogError(
                               "Class <b><color=white>" + type +
                               "</color></b> is not marked as Serializable, "
                             + "make sure to add <b><color=white>[System.Serializable]</color></b> at the top of your " +
                               type + " class."
                              );
            }

            return isSerializable;
        }

        //Other non-serialized types /// SS: Serialization Surrogate
        //Vector2 , Vector3 , Vector4 , Color , Quaternion.

        private static SurrogateSelector GetSurrogateSelector()
        {
            var surrogateSelector = new SurrogateSelector();

            var v2_ss = new Vector2_SS();
            var v3_ss = new Vector3_SS();
            var v4_ss = new Vector4_SS();
            var co_ss = new Color_SS();
            var qu_ss = new Quaternion_SS();

            surrogateSelector.AddSurrogate(typeof(Vector2),
                                           new StreamingContext(StreamingContextStates.All), v2_ss);
            surrogateSelector.AddSurrogate(typeof(Vector3),
                                           new StreamingContext(StreamingContextStates.All), v3_ss);
            surrogateSelector.AddSurrogate(typeof(Vector4),
                                           new StreamingContext(StreamingContextStates.All), v4_ss);
            surrogateSelector.AddSurrogate(typeof(Color),
                                           new StreamingContext(StreamingContextStates.All), co_ss);
            surrogateSelector.AddSurrogate(typeof(Quaternion),
                                           new StreamingContext(StreamingContextStates.All), qu_ss);

            return surrogateSelector;
        }

        private class Vector2_SS : ISerializationSurrogate
        {
            //Serialize Vector2
            public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
            {
                var v2 = (Vector2) obj;
                info.AddValue("x", v2.x);
                info.AddValue("y", v2.y);
            }

            //Deserialize Vector2
            public object SetObjectData(object obj, SerializationInfo info,
                                        StreamingContext context, ISurrogateSelector selector)
            {
                var v2 = (Vector2) obj;

                v2.x = (float) info.GetValue("x", typeof(float));
                v2.y = (float) info.GetValue("y", typeof(float));

                obj = v2;
                return obj;
            }
        }

        private class Vector3_SS : ISerializationSurrogate
        {
            //Serialize Vector3
            public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
            {
                var v3 = (Vector3) obj;
                info.AddValue("x", v3.x);
                info.AddValue("y", v3.y);
                info.AddValue("z", v3.z);
            }

            //Deserialize Vector3
            public object SetObjectData(object obj, SerializationInfo info,
                                        StreamingContext context, ISurrogateSelector selector)
            {
                var v3 = (Vector3) obj;

                v3.x = (float) info.GetValue("x", typeof(float));
                v3.y = (float) info.GetValue("y", typeof(float));
                v3.z = (float) info.GetValue("z", typeof(float));

                obj = v3;
                return obj;
            }
        }

        private class Vector4_SS : ISerializationSurrogate
        {
            //Serialize Vector4
            public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
            {
                var v4 = (Vector4) obj;
                info.AddValue("x", v4.x);
                info.AddValue("y", v4.y);
                info.AddValue("z", v4.z);
                info.AddValue("w", v4.w);
            }

            //Deserialize Vector4
            public object SetObjectData(object obj, SerializationInfo info,
                                        StreamingContext context, ISurrogateSelector selector)
            {
                var v4 = (Vector4) obj;

                v4.x = (float) info.GetValue("x", typeof(float));
                v4.y = (float) info.GetValue("y", typeof(float));
                v4.z = (float) info.GetValue("z", typeof(float));
                v4.w = (float) info.GetValue("w", typeof(float));

                obj = v4;
                return obj;
            }
        }

        private class Color_SS : ISerializationSurrogate
        {
            //Serialize Color
            public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
            {
                var color = (Color) obj;
                info.AddValue("r", color.r);
                info.AddValue("g", color.g);
                info.AddValue("b", color.b);
                info.AddValue("a", color.a);
            }

            //Deserialize Color
            public object SetObjectData(object obj, SerializationInfo info,
                                        StreamingContext context, ISurrogateSelector selector)
            {
                var color = (Color) obj;

                color.r = (float) info.GetValue("r", typeof(float));
                color.g = (float) info.GetValue("g", typeof(float));
                color.b = (float) info.GetValue("b", typeof(float));
                color.a = (float) info.GetValue("a", typeof(float));

                obj = color;
                return obj;
            }
        }

        private class Quaternion_SS : ISerializationSurrogate
        {
            //Serialize Quaternion
            public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
            {
                var qua = (Quaternion) obj;
                info.AddValue("x", qua.x);
                info.AddValue("y", qua.y);
                info.AddValue("z", qua.z);
                info.AddValue("w", qua.w);
            }

            //Deserialize Quaternion
            public object SetObjectData(object obj, SerializationInfo info,
                                        StreamingContext context, ISurrogateSelector selector)
            {
                var qua = (Quaternion) obj;

                qua.x = (float) info.GetValue("x", typeof(float));
                qua.y = (float) info.GetValue("y", typeof(float));
                qua.z = (float) info.GetValue("z", typeof(float));
                qua.w = (float) info.GetValue("w", typeof(float));

                obj = qua;
                return obj;
            }
        }
    }
}