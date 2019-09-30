using System.Collections.Generic;
using System.Linq;

namespace FewestPizzas
{
    public class TopFavoritesShare : IFewestPizzas
    {
        private Dictionary<int, List<PizzaPreferences>> likes;
        private Dictionary<Pizza, List<PizzaTopping>> pizzas;
        private List<PizzaPreferences> satisfied;

        public int Run(int maxToppings, PizzaPreferences[] prefs)
        {
            if (maxToppings <= 0)
            {
                throw new System.Exception("Invalid number of toppings specified. Must be greater than 0.");
            }
           
            likes = new Dictionary<int, List<PizzaPreferences>>();
            foreach(var pref in prefs)
            {
                foreach(int like in pref.likes)
                {
                    if (likes.ContainsKey(like))
                    {
                        likes[like].Add(pref);
                    }
                    else
                    {
                        likes.Add(like, new List<PizzaPreferences> { pref });
                    }
                }
            }

            pizzas = new Dictionary<Pizza, List<PizzaTopping>>();
            satisfied = new List<PizzaPreferences>();
            AddNewPizza(likes.First().Key);
            foreach (var topping in likes.Keys)
            {
                if (likes[topping].Count == prefs.Length) // This topping will satisfy everyone
                {
                    return 1;
                }
                else if (!likes[topping].All(x => satisfied.Contains(x))) // Someone who wants this topping has yet to be satisfied
                {
                    if (pizzas.Last().Key.toppings.Count() == maxToppings || pizzas.Last().Key.toppings.Count() == 0 || pizzas.Last().Value.Contains((PizzaTopping)topping))
                    {
                        AddNewPizza(topping);
                    }
                    else
                    {
                        pizzas.Last().Key.toppings.Add((PizzaTopping)topping);
                        foreach (var pref in likes[topping])
                        {
                            if (!satisfied.Contains(pref))
                            {
                                satisfied.Add(pref);
                            }
                            pizzas.Last().Value.AddRange(pref.hates.Where(x => !pizzas.Last().Value.Contains(x)));
                        }
                    }
                }
            }
            
            return pizzas.Count;
        }

        private void AddNewPizza(int topping)
        {
            var pizza = new Pizza();
            pizza.toppings = new List<PizzaTopping>
            {
                (PizzaTopping)topping
            };
            var dislikes = new List<PizzaTopping>();
            foreach (var pref in likes[topping])
            {
                if (!satisfied.Contains(pref))
                {
                    satisfied.Add(pref);
                }
                dislikes.AddRange(pref.hates.Where(x => !dislikes.Contains(x)));
            }
            pizzas.Add(pizza, dislikes);
        }
    }
}
