using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Optivem.Framework.Core.Domain;
using RestaurantRaterMVC.Data;
using RestaurantRaterMVC.Models.Restaurant;
using RestaurantRaterMVC.Services;

namespace RestaurantRaterMVC.Controllers
{
    public class RestaurantController : Controller
    {
        private IRestaurantServices _restaurantServices;
        public RestaurantController(IRestaurantServices restaurantServices)
        {
            _restaurantServices = restaurantServices;
        }

        public async Task<IActionResult> Index()
        {
            List<RestaurantListItem> restaurants = await _restaurantServices.GetAllRestaurants();
            return View(restaurants);
        }
        [HttpPost]
        public async Task<IActionResult> Create(RestaurantCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);
            await _restaurantServices.CreateRestaurant(model);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            RestaurantDetail restaurant = await _restaurantServices.GetRestaurantById(id);
            if (restaurant == null) return RedirectToAction(nameof(Index));
            return View(restaurant);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            RestaurantDetail restaurant =  await _restaurantServices.GetRestaurantById(id);

            if (restaurant == null)
                return RedirectToAction(nameof(Index));
            RestaurantEdit restaurantEdit = new RestaurantEdit()
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Location = restaurant.Location,
            };

            return View(restaurantEdit);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, RestaurantEdit model)
        {
            if (!ModelState.IsValid) return View(ModelState);
            bool hasUpdated = await _restaurantServices.UpdateRestaurant(model);
            if (!hasUpdated) return View(model);
            return RedirectToAction(nameof(Details), new { id = model.Id });
        }
        public async Task<IActionResult> Delete(int id)
        {
            RestaurantDetail restaurant = await _restaurantServices.GetRestaurantById(id);
            if (restaurant == null) return RedirectToAction(nameof(Index));
            return View(restaurant);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id, RestaurantDetail model)
        {
            bool wasDeleted = await _restaurantServices.DeleteRestaurant(model.Id);
            if (!wasDeleted) return View(model);

            return RedirectToAction(nameof(Index));
        }


    }
}
