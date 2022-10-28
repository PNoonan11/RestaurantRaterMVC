using Microsoft.EntityFrameworkCore;
using RestaurantRaterMVC.Data;
using RestaurantRaterMVC.Models.Restaurant;


namespace RestaurantRaterMVC.Services
{
    public class RestaurantServices : IRestaurantServices
    {
        private ApplicationDbContext _context;
        public RestaurantServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RestaurantListItem>> GetAllRestaurants()
        {
            List<RestaurantListItem> restautants = await _context.Restaurants
            .Include(r => r.Ratings)
            .Select(r => new RestaurantListItem()
            {
                Id = r.Id,
                Name = r.Name,
                Score = r.Score
            }).ToListAsync();

            return restautants;
            /*Manual addition of FK relationships
             *  List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();
            foreach (Restaurant restaurant in restaurants)
            {
                restaurant.Ratings = await _context.Ratings.Where(r => r.RestaurantId == restaurant.Id).ToListAsync();
            }
            List<RestaurantListItem> restaurantList = restaurants.Select(...*/
        }
        public async Task<bool> CreateRestaurant(RestaurantCreate model)
        {
            RestaurantEntity restaurant = new RestaurantEntity()
            {
                Name = model.Name,
                Location = model.Location
            };
            _context.Restaurants.Add(restaurant);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<RestaurantDetail> GetRestaurantById(int id)
        {
            RestaurantEntity restaurant = await _context.Restaurants
                .Include(r => r.Ratings)
                .FirstOrDefaultAsync(r => r.Id == id);
            if (restaurant == null) return null;
            RestaurantDetail restaurantDetail = new RestaurantDetail()
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Location = restaurant.Location,
                AverageFoodScore = restaurant.AverageFoodScore,
                AverageCleanlinessScore = restaurant.AverageCleanlinessScore,
                AverageAtmosphereScore = restaurant.AverageAtmosphereScore
            };
            return restaurantDetail;
        }
        public async Task<bool> UpdateRestaurant(RestaurantEdit model)
        {
            RestaurantEntity restaurant = await _context.Restaurants.FindAsync(model.Id);
            if (restaurant == null) return false;
            restaurant.Location = model.Location;
            restaurant.Name = model.Name;
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeleteRestaurant(int id)
        {
            RestaurantEntity restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null) return false;
            _context.Restaurants.Remove(restaurant);

            return await _context.SaveChangesAsync() == 1;
        }
    }
}
