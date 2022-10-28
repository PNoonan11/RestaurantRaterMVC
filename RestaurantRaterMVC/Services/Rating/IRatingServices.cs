using RestaurantRaterMVC.Models.Rating;

public interface IRatingServices
{
    Task<bool> RateRestaurant(RatingCreate model);
    Task<List<RatingListItem>> GetAllRatings();
    Task<List<RatingListItem>> GetRatingsForRestaurant(int id);
    Task<bool> DeleteRating(int id);
}