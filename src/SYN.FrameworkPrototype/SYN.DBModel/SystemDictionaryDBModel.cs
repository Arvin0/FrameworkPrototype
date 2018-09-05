using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SYN.DBModel
{
    [Table("system_dictionary", Schema = "mydemo")]
    public class SystemDictionaryDBModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("type")]
        public string Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("code")]
        public string Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("value")]
        public string Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("status")]
        public bool Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("order")]
        public int Order { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("value_1")]
        public string Value1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("value_2")]
        public string Value2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("description")]
        public string Description { get; set; }

    }
}
