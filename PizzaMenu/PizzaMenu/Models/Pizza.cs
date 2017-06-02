using System;
using System.Collections.Generic;

namespace PizzaMenu.Models
{
    public partial class Pizza
    {
        public Pizza()
        {
            PizzaIngredients = new HashSet<PizzaIngredients>();
        }

        public int PizzaId { get; set; }
        public string Title { get; set; }
        public int? Pris { get; set; }

        public virtual ICollection<PizzaIngredients> PizzaIngredients { get; set; }


    }
}
