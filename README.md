# Fewest Pizza's Challenge
For this challenge, your function will take an integer representing the maximum number of toppings that can go on a pizza, and an array of topping preferences, representing each guest, defined as:
```
    public class PizzaPreferences
    {
        public List<PizzaTopping> likes;
        public List<PizzaTopping> hates;
    }
```
The challenge is to determine the fewest number of pizzas that you need to order such that each person (represented by their preferences in the array) get some pizza with at least 1 topping they like, and 0 toppings they hate. 

For example, if the preferences are:
```
[
  {
    "likes": ["Bacon"],
    "hates": ["Pineapple"]
  },
  {
    "likes": ["Pineapple"],
    "hates": ["Bacon"]
  }
]
```
Your function should return 2 (pizzas), since your two guests don’t like any of the same toppings.  However with:
```
[
  {
    "likes": ["Bacon", "GreenPepper"],
    "hates": ["Pineapple"]
  },
  {
    "likes": ["Pineapple", "GreenPepper"],
    "hates": ["Bacon"]
  }
]
```
you can now order 1 pizza with GreenPepper to satisfy everyone... in theory.

Replace TopFavoritesShare with your own implementation and give it a unique name.  You can test it from the command like with a specific set of preferences in JSON format, or have the framework randomly generate one.  For example:
```
> FewestPizzas/bin/Debug> .\FewestPizzas.exe pizza 3 random 5

Using random seed 381319968

Testing FewestPizzas algorithms with max toppings 3 and preferences 
[
  {
    "likes": [
      "Pineapple",
      "Jalapeno"
    ],
    "hates": []
  }
]
TopFavoritesShare (in 9 ms) << 4
```
The 3 in the command line arguments above says pizzas can have a maximum of 3 toppings, and it will generate 5 guests worth of preferences.  You can provide the random seed that is printed as the last argument to repeat a specific set of preferences; i.e.:

> FewestPizzas/bin/Debug> .\FewestPizzas.exe pizza 3 random 5 381319968

will run the algorithm(s) with the same input as previously.  To run with a specific set of preferences, in place of ‘random’ and the number of guests, you can put the preferences array as JSON, e.g. with cmd:

> .\FewestPizzas.exe 3 [{"likes":["Beef","GreenOlive"],"hates":["GreenPepper"]},{"likes":["Jalapeno","Anchovies"],"hates":["Chicken"]}]

Powershell is more of a pain with the quotes:

> .\FewestPizzas.exe pizza 3 '[{\"likes\":[\"Beef\",\"GreenOlive\"],\"hates\":[\"GreenPepper\"]},{\"likes\":[\"Jalapeno\",\"Anchovies\"],\"hates\":[\"Chicken\"]}]'

 


