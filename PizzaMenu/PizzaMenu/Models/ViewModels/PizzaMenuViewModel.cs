using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaMenu.Models.ViewModels
{
    public class PizzaMenuViewModel
    {
        public List<Pizza> pizzas { get; set; }

        public List<Ingredients> ingredients { get; set; }

        public List<PizzaIngredients> pizzaIngredients { get; set; }

        public Dictionary<Pizza, List<Ingredients>> PizzaMenu { get; set; }
    }
}
