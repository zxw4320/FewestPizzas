using System.Linq;

namespace FewestPizzas
{
    public class TopFavoritesShare : IFewestPizzas
    {
        public int Run(int maxToppings, PizzaPreferences[] prefs)
        {
            return prefs.Select(p => p.likes
                                      .OrderBy(x => x).First())
                        .Distinct()
                        .Count();
        }
    }
}
