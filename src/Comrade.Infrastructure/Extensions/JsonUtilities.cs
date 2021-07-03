#region

using System.Collections.Generic;
using System.IO;
using Comrade.Domain.Bases;
using Newtonsoft.Json;

#endregion

namespace Comrade.Infrastructure.Extensions
{
    public static class JsonUtilities
    {
        public static List<TTargetModel> GetListFromJson<TTargetModel>(Stream jsonStream)
            where TTargetModel : Entity
        {
            var reader = new StreamReader(jsonStream);
            var jsonString = reader.ReadToEnd();

            var list = JsonConvert.DeserializeObject<List<TTargetModel>>(jsonString);

            return list;
        }
    }
}