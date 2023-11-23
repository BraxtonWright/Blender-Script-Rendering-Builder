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
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }

        public Theme(string name, bool isChecked)
        {
            Name = name;
            IsChecked = isChecked;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
