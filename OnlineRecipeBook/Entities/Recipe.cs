using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Recipe : CommonEntity
    {
        string Text { get; }

        string Ingridients { get; set; }

        List<string> ImageNames { get; set; }

        double RecipeRating { get; set; }


        public Recipe(int id, string text, string ingridients, List<string> images):base(id)
        {
            Text = text;

            Ingridients = ingridients;

            ImageNames = images;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
