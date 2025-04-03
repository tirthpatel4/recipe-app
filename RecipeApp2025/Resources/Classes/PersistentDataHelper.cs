using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;

namespace RecipeApp2025.Resources.Classes
{
    public class PersistentDataHelper
    {
        private static string mainDir = FileSystem.Current.AppDataDirectory;
        private static string login_filepath = mainDir+"/login.txt";
        private static string theme_filepath = mainDir+"/theme.txt";

        public static string GetLogin()
        {
            string s = string.Empty;
            if (!File.Exists(login_filepath))
            {
                using (var fs = File.Create(login_filepath))
                {
                    // Close the stream immediately after creating the file
                }
            }

            using (StreamReader sr = File.OpenText(login_filepath))
            {
               
                s = sr.ReadLine();
                if (s == null)
                {
                    s = string.Empty;
                }
                Debug.WriteLine(s.Length);
            }
            return s; 
        }
         

        public static void SetLogin(string user)
        {
            System.IO.File.WriteAllText(login_filepath, user);
        }


        public static int GetTheme()
        {
            int i = -1; 
            string s = string.Empty;
            if (!File.Exists(theme_filepath))
            {
                using (var fs = File.Create(theme_filepath))
                {
                    // Close the stream immediately after creating the file
                }
            }

            using (StreamReader sr = File.OpenText(theme_filepath))
            {
                s = sr.ReadLine();
                if (s != null)
                {
                    i = Convert.ToInt32(s);
                }
            }
            
            return i;
        }

        public static void SetTheme(int theme)
        {
            System.IO.File.WriteAllText(theme_filepath, theme.ToString());
        }

        
    }
}
