using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestApiDatabase.Model
{
    public class CurrencyModel
    {
        [JsonProperty("Cur_ID")]
        [Column("Cur_ID")]
        public int Id { get; set; }

        [JsonProperty("Cur_ParentID")]
        [Column("Cur_ParentID")]
        public int? ParentId { get; set; }

        [JsonProperty("Cur_Code")]
        [Column("Cur_Code")]
        public string Code { get; set; }
        
        [JsonProperty("Cur_Abbreviation")]
        [Column("Cur_Abbreviation")]
        public string Abbreviation { get; set; }
        
        [JsonProperty("Cur_Name")]
        [Column("Cur_Name")]
        public string Name { get; set; }
        
        [JsonProperty("Cur_Name_Bel")]
        [Column("Cur_Name_Bel")]
        public string NameBel { get; set; }
        
        [JsonProperty("Cur_Name_Eng")]
        [Column("Cur_Name_Eng")]
        public string NameEng { get; set; }
        
        [JsonProperty("Cur_QuotName")]
        [Column("Cur_QuotName")]
        public string QuotName { get; set; }
        
        [JsonProperty("Cur_QuotName_Bel")]
        [Column("Cur_QuotName_Bel")]
        public string QuotNameBel { get; set; }
        
        [JsonProperty("Cur_QuotName_Eng")]
        [Column("Cur_QuotName_Eng")]
        public string QuotNameEng { get; set; }
        
        [JsonProperty("Cur_NameMulti")]
        [Column("Cur_NameMulti")]
        public string NameMulti { get; set; }
        
        [JsonProperty("Cur_Name_BelMulti")]
        [Column("Cur_Name_BelMulti")]
        public string NameBelMulti { get; set; }
        
        [JsonProperty("Cur_Name_EngMulti")]
        [Column("Cur_Name_EngMulti")]
        public string NameEngMulti { get; set; }
        
        [JsonProperty("Cur_Scale")]
        [Column("Cur_Scale")]
        public int Scale { get; set; }
        
        [JsonProperty("Cur_Periodicity")]
        [Column("Cur_Periodicity")]
        public int Periodicity { get; set; }
        
        [JsonProperty("Cur_DateStart")]
        [Column("Cur_DateStart")]
        public DateTime DateStart { get; set; }
        
        [JsonProperty("Cur_DateEnd")]
        [Column("Cur_DateEnd")]
        public DateTime DateEnd { get; set; }
    }
}
