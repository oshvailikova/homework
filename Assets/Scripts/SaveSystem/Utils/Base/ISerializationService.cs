namespace SaveSystem.Utils.Base
{
    public interface ISerializationService
    {
        string Serialize<T>(T obj);
        T Deserialize<T>(string data);
    }
}