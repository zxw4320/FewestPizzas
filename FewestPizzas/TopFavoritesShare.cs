using System;
using System.Collections.Generic;
using System.Linq;

namespace FewestPizzas
{
    public class TopFavoritesShare : IFewestPizzas
    {
        public int Run(int maxToppings, PizzaPreferences[] prefs)
        {
            if (maxToppings <= 0)
            {
                throw new Exception("Invalid number of toppings specified. Must be 0 < x <= 16.");
            }
           
            // create a dictionary of toppings to all the people who like it
            var likes = new Dictionary<int, List<PizzaPreferences>>();
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

            // if a pizza with one topping with satisfy everyone
            if(likes.Any(x => x.Value.Count == prefs.Length))
            {
                return 1;
            }

            // find the minimum number of pizzas that will satisfy everyone
            var likedToppings = likes.Keys.ToList();
            var allPizzas = AllPizzas(likedToppings);
            var possiblePizzaCombinations = PossiblePizzaCombinations(allPizzas);
            var minPizzas = prefs.Length <= likes.Keys.Count ? prefs.Length : likes.Keys.Count;
            foreach (var combo in possiblePizzaCombinations)
            {
                if(combo.Count < minPizzas)
                {
                    var satisfied = new HashSet<PizzaPreferences>();
                    foreach(var pizza in combo)
                    {
                        foreach(var pref in prefs)
                        {
                            if(pref.likes.Intersect(pizza.toppings).Count() > 0 && pref.hates.Intersect(pizza.toppings).Count() == 0)
                            {
                                satisfied.Add(pref);
                            }
                        }
                    }
                    if(satisfied.Count() == prefs.Length)
                    {
                        minPizzas = combo.Count;
                    }
                }
            }

            // worst case is either each person gets their own or one pizza for every liked topping
            return minPizzas;
        }

        // find all possible pizzas
        private List<Pizza> AllPizzas(List<int> pizzaToppings)
        {
            List<Pizza> result = new List<Pizza>();

            var pizza = new Pizza
            {
                toppings = new List<PizzaTopping>
            {
                (PizzaTopping)pizzaToppings.First()
            }
            };
            result.Add(pizza);
            if (pizzaToppings.Count == 1)
                return result;

            List<Pizza> tailCombos = AllPizzas(pizzaToppings.Skip(1).ToList());
            tailCombos.ForEach(combo =>
            {
                result.Add(combo);
                var p = new Pizza
                {
                    toppings = new List<PizzaTopping>
                {
                    (PizzaTopping)pizzaToppings.First()
                }
                };
                p.toppings.AddRange(combo.toppings);
                result.Add(p);
            });

            return result;
        }

        // find all possible pizza combinations
        private List<List<Pizza>> PossiblePizzaCombinations(List<Pizza> pizzas)
        {
            List<List<Pizza>> result = new List<List<Pizza>>();

            result.Add(new List<Pizza>());
            result.Last().Add(pizzas.First());
            if (pizzas.Count == 1)
                return result;

            List<List<Pizza>> tailCombos = PossiblePizzaCombinations(pizzas.Skip(1).ToList());
            foreach(var combo in tailCombos)
            {
                result.Add(new List<Pizza>(combo));
                if(!combo.Any(x => x.toppings.Intersect(pizzas.First().toppings).Count() != 0))
                {
                    combo.Add(pizzas.First());
                    result.Add(combo);
                }                
            }

            return result;
        }
    }
}
