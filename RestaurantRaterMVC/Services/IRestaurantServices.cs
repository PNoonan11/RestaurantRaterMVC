using RestaurantRaterMVC.Models.Restaurant;

namespace RestaurantRaterMVC.Services
{
    public interface IRestaurantServices
    {
        Task<bool> CreateRestaurant(RestaurantCreate model);
        Task<List<RestaurantListItem>> GetAllRestaurants();
        Task<RestaurantDetail> GetRestaurantById(int id);
        Task<bool> UpdateRestaurant(RestaurantEdit model);
        Task<bool> DeleteRestaurant(int id);

    }
}
