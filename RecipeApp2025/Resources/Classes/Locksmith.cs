using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp2025.Resources.Classes
{
    public class Locksmith
    {
        private static List<string> Fresh = new List<string> { "ec6ea12f18d7fccae1dfc66f23f09153c3fd1a54ec6ea12f18d7fccae1dfc66f23f09153c3fd1a54", "7cb65daeeb2aea85fd00dddc1332ec4edca70ecb", "553f966fcec73ca32839a280c00f6c1c5c904817", "60bd29f7cfc54795868a9a053cb447a3", "e9086f66a8184e88a43bac38c112dba0" };
        //private static List<string> Fresh = new List<string> { "ec6ea12f18d7fccae1dfc66f23f09153c3fd1a54ec6ea12f18d7fccae1dfc66f23f09153c3fd1a54", "7cb65daeeb2aea85fd00dddc1332ec4edca70ecb", "553f966fcec73ca32839a280c00f6c1c5c904817", "60bd29f7cfc54795868a9a053cb447a3", "e9086f66a8184e88a43bac38c112dba0"};

        private static List<string> Used = new List<string>();
        private static Random rnd = new Random();

        private static void RefreshList()
        {
            if (Fresh.Count == 0)
            {
                Fresh.AddRange(Used);
                Used.Clear();
            }
        }

        public static string GetKey()
        {
            RefreshList();
            int i = rnd.Next(0, Fresh.Count);
            String result = Fresh[i];
            Fresh.RemoveAt(i);
            Used.Add(result);
            return result;
        }
    }
}