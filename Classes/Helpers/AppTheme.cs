/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder class AppTheme
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 0.5
 *  ----------------------------------------------------------------------------------------------------------
 * This file contains only one fuction to enable the user to change the theme of the app
 * -----------------------------------------------------------------------------------------------------------
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Blender_Script_Rendering_Builder.Classes.Helpers
{
    public class AppTheme
    {
        /// <summary>
        /// Changes the theme of the application
        /// </summary>
        /// <param name="themeURI">The URI to the theme xmal file</param>
        public static void ChangeTheme(Uri themeURI)
        {
            try
            {
                ResourceDictionary Theme = new ResourceDictionary() { Source = themeURI };

                // Clear all the application resources from the App.xaml file
                App.Current.Resources.Clear();
                // Add the resource dictionary as defined in the above varaible to the App.xaml file
                App.Current.Resources.MergedDictionaries.Add(Theme);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
