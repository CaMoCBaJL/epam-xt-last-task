using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Recipe : CommonEntity
    {
        string Title { get; }

        string Ingridients { get; set; }

        string CookingProcess { get; set; }

        double RecipeRating { get; set; }


        public Recipe(int id, string title, 
            string ingridients, string cookingProcess, int recipeAward):base(id)
        {
            Title = title;

            CookingProcess = cookingProcess;

            Ingridients = ingridients;

            RecipeRating = recipeAward;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
