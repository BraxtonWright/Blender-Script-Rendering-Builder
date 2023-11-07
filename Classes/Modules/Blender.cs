/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder class Blender
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 0.5
 * -----------------------------------------------------------------------------------------------------------
 * This file contains the variables and functions that are required make an Blender data for the UI when
 * outputting to a script file.
 * -----------------------------------------------------------------------------------------------------------
 */

using Blender_Script_Rendering_Builder.Classes.Shared;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Blender_Script_Rendering_Builder.Classes.Modules
{
    internal class Blender : INotifyPropertyChangedImplmented
    {
        #region Class variables
        string _fullPath;
        /// <summary>
        /// The full path to the blender file
        /// </summary>
        public string FullPath
        {
            get { return _fullPath; }
            set
            {
                _fullPath = value;
                OnPropertyChanged(nameof(FullPath));
                FileName = GetFileName(value);
            }
        }

        string _fileName;
        /// <summary>
        /// The name of the blender file
        /// </summary>
        public string FileName
        {
            get { return _fileName; }
            private set  // We have this set as private because we don't want to have it be set from outside this file.  This is because when the FullPath property changes, this property is automatically updated.
            {
                _fileName = value;
                OnPropertyChanged(nameof(FileName));
            }
        }

        /// <summary>
        /// A list of scenes for the blender file
        /// </summary>
        public List<Scene> scenes = new List<Scene>();
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public Blender()
        {
            // do nothing
        }

        /// <summary>
        /// Overloaded constructor to define both the blender full path and the scenes for that blender file
        /// </summary>
        /// <param name="fullPath">The full path to the blender file</param>
        /// <param name="scenes">A list of scenes for the blender file</param>
        public Blender(string fullPath, List<Scene> scenes)
        {
            _fullPath = fullPath;
            this.scenes = scenes;
        }
        #endregion

        #region Functions
        /// <summary>
        /// Grabs the name of the file from the full path to the file.
        /// Source https://forum.uipath.com/t/regex-getting-filename-out-from-filepath/190312/3
        /// </summary>
        /// <param name="FullPath">The full path to the file</param>
        /// <returns>The name of the file including it's extention</returns>
        private string GetFileName(string FullPath)
        {
            try
            {
                return System.IO.Path.GetFileName(FullPath);
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                return "";
            }
        }
        #endregion
    }
}
