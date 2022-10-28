using System.ComponentModel.DataAnnotations;

namespace RestaurantRaterMVC.Models.Rating
{
    public class RatingCreate
    {
        [Required]
        public int RestaurantId { get; set; }
        [Required]
        [Range(0, 5)]
        public double FoodScore { get; set; }
        [Required]
        [Range(0, 5)]
        public double CleanlinessScore { get; set; }
        [Required]
        [Range(0, 5)]
        public double AtmosphereScore { get; set; }
    }
}
