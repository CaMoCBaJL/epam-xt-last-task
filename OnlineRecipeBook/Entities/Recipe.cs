using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    class Recipe : CommonEntity
    {
        string Text { get; }

        Dictionary<string, string> Ingridients { get; set; }

        List<string> ImageNames { get; set; }

        double RecipeRating { get; set; }


        public Recipe(int id, string text, Dictionary<string, string> ingridients, List<string> images):base(id)
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
