using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Product:BaseClass
    {
       
        public required string Name { get; set; }
        public int Status { get; set; }
        public required int Stock { get; set; }
        public string Description { get; set; } = null!;
        public required double Price { get; set; }
       

    }
}
