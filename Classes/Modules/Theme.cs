/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder class Theme
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 1.0
 *  ----------------------------------------------------------------------------------------------------------
 * This file contains the variables and functions that are required make a theme instance so we can pass
 * around an instance of this class to make multiple instances of MenuItems to allow the user to change the
 * theme of the application.
 * -----------------------------------------------------------------------------------------------------------
 */

using Blender_Script_Rendering_Builder.Classes.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blender_Script_Rendering_Builder.Classes.Modules
{
    public class Theme : INotifyPropertyChangedImplmented
    {
        string _name;
        /// <summary>
        /// The name of the theme
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        bool _isChecked;
        /// <summary>
        /// Is the theme selected/checked
        /// </summary>
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="name">The name of the Scene</param>
        /// <param name="isChecked">Is the theme selected</param>
        public Theme(string name, bool isChecked)
        {
            Name = name;
            IsChecked = isChecked;
        }

        /// <summary>
        /// Overloaded ToString() function for this class
        /// </summary>
        /// <returns>The Name property of this class, I.E. the name of the scene</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
