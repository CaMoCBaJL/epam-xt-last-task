using System.Text;
using CommonConstants;
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

            result.Append(Title + DelimiterConstant.myDelimiter);

            result.Append(RecipeRating.ToString() + DelimiterConstant.myDelimiter);

            result.Append(Ingridients + DelimiterConstant.myDelimiter);

            result.Append(CookingProcess + DelimiterConstant.myDelimiter);

            return result.ToString();
        }
    }
}
