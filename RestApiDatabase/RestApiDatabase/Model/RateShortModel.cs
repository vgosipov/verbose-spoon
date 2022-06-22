using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestApiDatabase.Model
{
    public class RateShortModel
    {
        [JsonProperty("Cur_ID")]
        [Column("Cur_ID")]
        public int CurId { get; set; }

        public DateTime Date { get; set; }

        [JsonProperty("Cur_OfficialRate")]
        [Column("Cur_OfficialRate")]
        public decimal? OfficialRate { get; set; }
    }
}
