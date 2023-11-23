/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder class ThemeManager
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 1.0
 *  ----------------------------------------------------------------------------------------------------------
 * This file contains fuctions to enable the user to change the theme of the app.
 * Original source for the code is found here
 * https://github.com/mesta1/WPF.Themes/blob/master/WPF.Themes/ThemeManager.cs (also found inside my "Files For
 * Students" zip folder from CS 3280)
 * -----------------------------------------------------------------------------------------------------------
 */

using Blender_Script_Rendering_Builder.Classes.Modules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Blender_Script_Rendering_Builder.Classes.Helpers
{
    public static class ThemeManager
    {
        public static ResourceDictionary GetThemeResourceDictionary(string theme)
        {
            if (theme != null)
            {
                Assembly assembly = Assembly.LoadFrom("Blender Script Rendering Builder.dll");  // This file is located inside of the folder "\bin\Debug\net7.0-windows\"
                string packUri = String.Format(@"/Blender Script Rendering Builder;component/Themes/{0}.xaml", theme);  // "component" references the root of the project.  I.E. "Blender Script Rendering Builder"
                return Application.LoadComponent(new Uri(packUri, UriKind.Relative)) as ResourceDictionary;
            }
            return null;
        }

        // This is modified from the orignal so instead of using a combobox, we use a menuitem.  This idea for this modification was found here https://stackoverflow.com/questions/32791619/wpf-mvvm-checking-a-menuitem-based-on-string-match/32793843#32793843
        public static ObservableCollection<Theme> GetThemes()
        {
            // The names defined inside here have to match the name of the file inside the "Themes" folder.
            ObservableCollection<Theme> themes = new ObservableCollection<Theme>
            {
                new("Light", false),
                new("Dark", false)
            };
            return themes;
        }

        public static void ApplyTheme(this Application app, string theme)
        {
            ResourceDictionary dictionary = ThemeManager.GetThemeResourceDictionary(theme);

            if (dictionary != null)
            {
                app.Resources.MergedDictionaries.Clear();
                app.Resources.MergedDictionaries.Add(dictionary);
            }
        }

        public static void ApplyTheme(this ContentControl control, string theme)
        {
            ResourceDictionary dictionary = ThemeManager.GetThemeResourceDictionary(theme);

            if (dictionary != null)
            {
                control.Resources.MergedDictionaries.Clear();
                control.Resources.MergedDictionaries.Add(dictionary);
            }
        }

        #region Theme
        /// <summary>
        /// Theme Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty ThemeProperty =
            DependencyProperty.RegisterAttached("Theme", typeof(string), typeof(ThemeManager),
                new FrameworkPropertyMetadata((string)string.Empty,
                    new PropertyChangedCallback(OnThemeChanged)));

        /// <summary>
        /// Gets the Theme property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static string GetTheme(DependencyObject d)
        {
            return (string)d.GetValue(ThemeProperty);
        }

        /// <summary>
        /// Sets the Theme property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static void SetTheme(DependencyObject d, string value)
        {
            d.SetValue(ThemeProperty, value);
        }

        /// <summary>
        /// Handles changes to the Theme property.
        /// </summary>
        private static void OnThemeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            string theme = e.NewValue as string;
            if (theme == string.Empty)
                return;

            ContentControl control = d as ContentControl;
            if (control != null)
            {
                control.ApplyTheme(theme);
            }
        }
        #endregion

    }
}
