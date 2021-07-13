using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Recipe : CommonEntity
    {
        public string Title { get; }

        public string Ingridients { get; set; }

        public string CookingProcess { get; set; }

        public double RecipeRating { get; set; }


        public Recipe(int id, string title, 
            string ingridients, string cookingProcess, double recipeAward):base(id)
        {
            Title = title;

            CookingProcess = cookingProcess;

            Ingridients = ingridients;

            RecipeRating = recipeAward;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append(Title + Environment.NewLine);

            result.Append(RecipeRating.ToString() + Environment.NewLine);

            result.Append(Ingridients + Environment.NewLine);

            result.Append(CookingProcess + Environment.NewLine);

            return result.ToString();
        }
    }
}
