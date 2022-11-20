using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using Taller_Kforce_Autos.Models;

namespace Taller_Kforce_Autos.Controllers
{
    public class GameController : Controller
    {
        private List<Car> cars = new()
        {
            new Car { Id = 1, Make = "Audi", Model = "R8", Year = 2018, Doors = 2, Color = "Red", Price = 79995 },
            new Car { Id = 2, Make = "Tesla", Model = "3", Year = 2018, Doors = 4, Color = "Black", Price = 54995 },
            new Car { Id = 3, Make = "Porsche", Model = "911 991", Year = 2020, Doors = 2, Color = "White", Price = 155000 },
            new Car { Id = 4, Make = "Mercedes-Benz", Model = "GLE 63S", Year = 2021, Doors = 5, Color = "Blue", Price = 83995 },
            new Car { Id = 5, Make = "BMW", Model = "X6 M", Year = 2020, Doors = 5, Color = "Silver", Price = 62995 },
        };

        private ClickCounter clicks= new ClickCounter();

        public IActionResult Index()
        {
            try
            {
                Random r = new Random();
                var firstCar = cars.FirstOrDefault();
                var lastCar = cars.LastOrDefault();

                int rInt = r.Next(firstCar.Id, lastCar.Id);
                

                Car car = cars.Select(c => new Car
                    {
                        Id = c.Id,
                        Make = c.Make,
                        Model = c.Model,
                        Year = c.Year,
                        Doors = c.Doors,
                        Color = c.Color,
                        Price = 0
                    }).First(x => x.Id == rInt);

                return View(car);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IActionResult> Play(decimal price, int id)
        {
            try
            {
                clicks.ClickCount = 1; //TODO: Figure out how to persist this data
                for (var i = clicks.ClickCount; i <= 5000; i++)
                {
                    var car = cars.FirstOrDefault(x => x.Id == id);
                    if (price == car.Price)
                    {
                        clicks.ClickCount = 0;
                        return PartialView("_popUp"); //TODO: Make it as a Pop Up
                    }
                    else
                    {
                        return RedirectToAction("Index", "Game");//, new { click = clicks.ClickCount });
                    }
                }
                return RedirectToAction("Index", "Game");//, new { click = clicks.ClickCount });

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IActionResult> Reload()
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
