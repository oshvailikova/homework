using Newtonsoft.Json;
using SaveSystem.Utils.Base;

namespace SaveSystem.Utils
{
    public class JsonSerializationService : ISerializationService
    {
        public string Serialize<T>(T obj) => JsonConvert.SerializeObject(obj);
        public T Deserialize<T>(string data) => JsonConvert.DeserializeObject<T>(data);
    }
}
