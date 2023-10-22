using Newtonsoft.Json;

public class Utils
{
    public static T JsonToObject<T>(string json)
    {
        T rs;

        if (json != null && json != "")
        {
            rs = JsonConvert.DeserializeObject<T>(json);
            return rs;
        }

        return default;
    }
}