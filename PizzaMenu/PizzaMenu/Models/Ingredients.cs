using System;
using System.Collections.Generic;

namespace PizzaMenu.Models
{
    public partial class Ingredients
    {
        public Ingredients()
        {
            PizzaIngredients = new HashSet<PizzaIngredients>();
        }

        public int IngredientsId { get; set; }
        public string IngredientName { get; set; }
        public int? Amount { get; set; }

        public virtual ICollection<PizzaIngredients> PizzaIngredients { get; set; }
    }
}
