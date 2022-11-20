using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json.Serialization;
using System.Text.Json;
using Taller_Kforce_Autos.Models;
using System.Net.Http.Json;

namespace Taller_Kforce_Autos.Controllers
{
    public class CarController : Controller
    {
        private static List<Car> cars = new()
        {
            new Car { Id = 1, Make = "Audi", Model = "R8", Year = 2018, Doors = 2, Color = "Red", Price = 79995 },
            new Car { Id = 2, Make = "Tesla", Model = "3", Year = 2018, Doors = 4, Color = "Black", Price = 54995 },
            new Car { Id = 3, Make = "Porsche", Model = "911 991", Year = 2020, Doors = 2, Color = "White", Price = 155000 },
            new Car { Id = 4, Make = "Mercedes-Benz", Model = "GLE 63S", Year = 2021, Doors = 5, Color = "Blue", Price = 83995 },
            new Car { Id = 5, Make = "BMW", Model = "X6 M", Year = 2020, Doors = 5, Color = "Silver", Price = 62995 },
        };

        public IActionResult Index()
        {
            return View(cars);
        }

        public IActionResult CreateCar()
        {
            List<int> years = new List<int>();
            int currentYear = DateTime.Now.Year;

            for (int i = currentYear - 50; i <= currentYear; i++)
            {
                years.Add(i);
            }

            ViewBag.Years = new SelectList(years);

            return View();
        }

        public IActionResult EditCar(int id)
        {
            try
            {
                if (cars.Any(x => x.Id == id))
                {
                    Car car = cars.Select(c => new Car
                    {
                        Id = c.Id,
                        Make = c.Make,
                        Model = c.Model,
                        Year = c.Year,
                        Doors = c.Doors,
                        Color = c.Color,
                        Price = c.Price
                    }).First(x => x.Id == id);

                    return View(car);
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IActionResult DeleteCar(int id)
        {
            try
            {
                if (cars.Any(x => x.Id == id))
                {
                    Car car = cars.Select(c => new Car
                    {
                        Id = c.Id,
                        Make = c.Make,
                        Model = c.Model,
                        Year = c.Year,
                        Doors = c.Doors,
                        Color = c.Color,
                        Price = c.Price
                    }).First(x => x.Id == id);

                    return View(car);
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Car car)
        {
            try
            {
                //List<Car> cars =new List<Car> { car };
                var lastCar = cars.LastOrDefault();
                car.Id = lastCar.Id++;
                cars.Add(car);
                return base.RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(Car car)
        {
            try
            {
                var carUpdate = cars.FirstOrDefault(x => x.Id == car.Id);
                if (carUpdate != null)
                {
                    carUpdate.Make = car.Make;
                    carUpdate.Model = car.Model;
                    carUpdate.Year = car.Year;
                    carUpdate.Doors = car.Doors;
                    carUpdate.Color = car.Color;
                    carUpdate.Price = car.Price;

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (cars.Any(x => x.Id == id))
                {
                    Car carDelete = cars.FirstOrDefault(x => x.Id == id);
                    cars.Remove(carDelete);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IActionResult> Cancel()
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
