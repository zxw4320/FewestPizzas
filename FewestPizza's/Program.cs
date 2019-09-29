using System.Linq;

namespace FewestPizzas
{
    class Program
    {
        static void Main(string[] args)
        {
            Challenge c = new DefaultChallenge();
            if (args[0].Contains("pizza"))
            {
                c = new FewestPizzasChallenge();
            }
            c.Run(args.Skip(1));
        }
    }
}
