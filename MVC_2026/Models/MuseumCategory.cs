namespace MVC2026.Models
{
    public class MuseumCategory
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
