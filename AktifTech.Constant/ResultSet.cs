using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AktifTech.Constant
{
    public class ResultSet
    {
        [JsonPropertyName("Result")]
        public Result Result { get; set; } = Result.Success;

        [JsonPropertyName("Message")]
        public string Message { get; set; } = "İşlem Başarılı";

        /// <summary>
        /// İşlem yapılan nesnenin Id bilgisini tutar.
        /// </summary>
        [JsonPropertyName("Id")]
        public int? Id { get; set; }

        /// <summary>
        /// İşlem yapılan nesneyi tutar.
        /// </summary>
        [JsonPropertyName("Object")]
        public object Object { get; set; }
    }
}
