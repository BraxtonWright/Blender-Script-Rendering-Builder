/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder class BlenderInfo
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 0.5
 * -----------------------------------------------------------------------------------------------------------
 * This file contains the variables and functions that are required make an Blender data for the UI when
 * outputting to a script file.
 * -----------------------------------------------------------------------------------------------------------
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Blender_Script_Rendering_Builder.Classes.Modules
{
    public class clsBlender : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged members
        /// <summary>
        /// This is the contract we have to make with the compiler because we are implementing the interface "INotifyPropertyChanged".  So we must have this event defined.  We will raise this event anytime one of our properties changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// A reusable set of code so that we can attach the PropertyChangedEventHandler to the below properties, without having to type out this code multiple times
        /// </summary>
        /// <param name="propertyName">The name of the property</param>
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Private class variables
        private string _fullPath;
        //private List<clsScene> _scenes;
        #endregion

        #region Getters/Setters for private class variables
        /// <summary>
        /// Full Path to the Blender file
        /// </summary>
        public string FullPath
        {
            get { return _fullPath; }
            set
            {
                _fullPath = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("FullPath"));

                //OnPropertyChanged("FullPath");
            }
        }

        /// <summary>
        /// A list of scenes we want to render from in the Blender file
        /// </summary>
        /*public List<clsScene> Scenes
        {
            get { return _scenes; }
            set
            {
                _scenes = value;
                OnPropertyChanged("Scenes");
            }
        }*/
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public clsBlender()
        {
            //_scenes = new List<clsScene>();
        }

        /// <summary>
        /// Overloaded constructor for Blender files with a list of instances of the model SceneModel
        /// </summary>
        /// <param name="fullPath">The full path to the blender file</param>
        /// <param name="scenes">A list of scenes for the blender file</param>
        public clsBlender(string fullPath, List<clsScene> scenes)
        {
            _fullPath = fullPath;
            //_scenes = scenes;
        }
        #endregion
    }
}
