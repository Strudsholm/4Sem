using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaMenu.Models;
using PizzaMenu.Models.ViewModels;
using PizzaMenu.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace PizzaMenu.Controllers
{
    public class HomeController : ApiHubController<Broadcaster>
    {


        private PizzaDBContext _context;



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BuyPizza(string id)
        {
            var dbcontext = new PizzaDBContext();
            int Pizz = int.Parse(id);
            foreach (var item in dbcontext.Ingredients.Where(x => dbcontext.PizzaIngredients.Any(y => x.IngredientsId == y.IngredienId && Pizz == y.PizzId)).ToList())
            {
                item.Amount--;
            }
            dbcontext.SaveChanges();

            return Json(new { success = true });
        }

        public IActionResult PizzaMenu()
        {
            var dbcontext = new PizzaDBContext();
            var vm = new PizzaMenuViewModel()
            {
                ingredients = dbcontext.Ingredients.ToList(),
                pizzas = dbcontext.Pizza.ToList(),
                pizzaIngredients = dbcontext.PizzaIngredients.ToList(),

            };
            vm.PizzaMenu = new Dictionary<Pizza, List<Ingredients>>();
            foreach (var item in vm.pizzas)
            {
                if (!dbcontext.Ingredients.Where(x => dbcontext.PizzaIngredients.Any(y => x.IngredientsId == y.IngredienId && item.PizzaId == y.PizzId)).ToList().Any(a => a.Amount < 1))
                {
                    vm.PizzaMenu.Add(item, dbcontext.Ingredients.Where(x => dbcontext.PizzaIngredients.Any(y => x.IngredientsId == y.IngredienId && item.PizzaId == y.PizzId)).ToList());
                }

            }
            return View("PizzaMenu", vm.PizzaMenu);
        }

        public IActionResult CheckPizza(string id)
        {

            var dbcontext = new PizzaDBContext();
            int Pizz = int.Parse(id);
            List<int> pizzaNoMore = new List<int>();
            foreach (var item in dbcontext.Ingredients.Where(x => dbcontext.PizzaIngredients.Any(y => x.IngredientsId == y.IngredienId && Pizz == y.PizzId)).ToList())
            {
                item.Amount--;
            }
            dbcontext.SaveChanges();
            var vm = new PizzaMenuViewModel()
            {
                ingredients = dbcontext.Ingredients.ToList(),
                pizzas = dbcontext.Pizza.ToList(),
                pizzaIngredients = dbcontext.PizzaIngredients.ToList(),

            };
            vm.PizzaMenu = new Dictionary<Pizza, List<Ingredients>>();
            foreach (var item in vm.pizzas)
            {
                if (dbcontext.Ingredients.Where(x => dbcontext.PizzaIngredients.Any(y => x.IngredientsId == y.IngredienId && item.PizzaId == y.PizzId)).ToList().Any(a => a.Amount < 1))
                {
                    pizzaNoMore.Add(item.PizzaId);
                }

            }
            var context = Startup.ConnectionManager.GetHubContext<Broadcaster>();
            context.Clients.All.sendNotification(pizzaNoMore);

            return NoContent();
        }

        [HttpGet]
        [Route("[controller]/LoadMenu")]
        public IActionResult LoadMenu()
        {
            var dbcontext = new PizzaDBContext();
            var vm = new PizzaMenuViewModel()
            {
                ingredients = dbcontext.Ingredients.ToList(),
                pizzas = dbcontext.Pizza.ToList(),
                pizzaIngredients = dbcontext.PizzaIngredients.ToList(),

            };
            vm.PizzaMenu = new Dictionary<Pizza, List<Ingredients>>();
            foreach (var item in vm.pizzas)
            {
                if (!dbcontext.Ingredients.Where(x => dbcontext.PizzaIngredients.Any(y => x.IngredientsId == y.IngredienId && item.PizzaId == y.PizzId)).ToList().Any(a => a.Amount < 1))
                {
                    vm.PizzaMenu.Add(item, dbcontext.Ingredients.Where(x => dbcontext.PizzaIngredients.Any(y => x.IngredientsId == y.IngredienId && item.PizzaId == y.PizzId)).ToList());
                }

            }
            return Json(vm.PizzaMenu);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
