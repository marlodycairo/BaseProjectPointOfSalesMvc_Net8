using System.ComponentModel.DataAnnotations;

namespace BaseProjectMvc_Net8.Data.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Category_Name { get; set; } = string.Empty;
    }
}
