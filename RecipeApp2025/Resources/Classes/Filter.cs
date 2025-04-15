using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp2025.Resources.Classes
{
    public class Filter
    {
        public int MinServing { get; set; }
        public int MaxServing { get; set; }
        public int MaxPrep { get; set; }
        public string Sort { get; set; }
        public List<string> Ingredients { get; set; }
        public Filter ()
        {
            MinServing = -1;
            MaxServing = -1;
            MaxPrep = -1;
            Ingredients = new List<string>();
            Sort = "random";

            if (Preferences.Get("cb1", false))
            {
                MinServing = Preferences.Get("e1", -1);
            }
            if (Preferences.Get("cb2", false))
            {
                MaxServing = Preferences.Get("e2", -1);
            }
            if (Preferences.Get("cb3", false))
            {
                MaxPrep = Preferences.Get("e3", -1);
            }
        }
        public string GetSearchInquiry()
        {
            string inquiry = "";
            if (MinServing >= 0)
            {
                inquiry += "&minServings=" + MinServing.ToString();
            }
            if (MaxServing >= 0 && MaxServing >= MinServing)
            {
                inquiry += "&maxServings=" + MaxServing.ToString();
            }
            if (MaxPrep >= 0)
            {
                inquiry += "&maxReadyTime=" + MaxPrep.ToString();
            }
            if (Ingredients.Count > 0)
            {
                inquiry += "&includeIngredients=";
                for (int i = 0; i < Ingredients.Count; i++)
                {
                    if (!Ingredients[i].Equals("none"))
                    {
                        if (i == Ingredients.Count - 1)
                        {
                            inquiry += Ingredients[i];
                        }
                        else
                        {
                            inquiry += Ingredients[i] + ",";
                        }
                    }
                }
            }
            return inquiry;
        }
    }

}
