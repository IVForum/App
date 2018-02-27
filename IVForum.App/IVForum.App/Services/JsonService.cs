using Newtonsoft.Json;

namespace IVForum.App.Services
{
	public class JsonService
    {
		public static string Serialize(object value)
		{
			return JsonConvert.SerializeObject(value);
		}

		public static T Deserialize<T>(string value) where T: class
		{
			return JsonConvert.DeserializeObject<T>(value);
		}
    }
}
