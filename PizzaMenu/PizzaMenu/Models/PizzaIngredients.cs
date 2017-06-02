using System;
using System.Collections.Generic;

namespace PizzaMenu.Models
{
    public partial class PizzaIngredients
    {
        public int PizzId { get; set; }
        public int IngredienId { get; set; }

        public virtual Ingredients Ingredien { get; set; }
        public virtual Pizza Pizz { get; set; }
    }
}
