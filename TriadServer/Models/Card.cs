using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TriadServer.Models
{
    public class Card
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Stars { get; set; }
		public string Type { get; set; }
		public string Img { get; set; }
		public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }
        public int Left { get; set; }
        public int SellPrice { get; set; }
    }
}
