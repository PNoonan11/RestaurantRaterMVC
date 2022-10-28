using Microsoft.EntityFrameworkCore;
using RestaurantRaterMVC.Data;
using RestaurantRaterMVC.Models.Rating;
using System;

public class RatingServices : IRatingServices
{
    private readonly ApplicationDbContext _context;
    public RatingServices(ApplicationDbContext context)
    {
        _context = context;
    }
    public Task<bool> DeleteRating(int id)
	{
		throw new NotImplementedException();
	}

	public async Task<List<RatingListItem>> GetAllRatings()
	{
        var ratings = _context.Ratings
                .Select(r => new RatingListItem()
                {
                    Id = r.Id,
                    RestaurantName = r.Restaurant.Name,
                    FoodScore = r.FoodScore,
                    AtmosphereScore = r.AtmosphereScore,
                    CleanlinessScore = r.CleanlinessScore,
                });
        return await ratings.ToListAsync();
    }

	public async Task<List<RatingListItem>> GetRatingsForRestaurant(int id)
	{
        var ratings = _context.Ratings
               .Where(r => r.RestaurantId == id)
               .Select(r => new RatingListItem()
               {
                   Id = r.Id,
                   RestaurantName = r.Restaurant.Name,
                   FoodScore = r.FoodScore,
                   AtmosphereScore = r.AtmosphereScore,
                   CleanlinessScore = r.CleanlinessScore,
               });
        return await ratings.ToListAsync();
    }

	public async Task<bool> RateRestaurant(RatingCreate model)
	{
        var rating = new RatingEntity()
        {
            FoodScore = model.FoodScore,
            AtmosphereScore = model.AtmosphereScore,
            CleanlinessScore = model.CleanlinessScore,
            RestaurantId = model.RestaurantId,

        };
        _context.Ratings.Add(rating);
        return await _context.SaveChangesAsync() == 1;
    }
}
