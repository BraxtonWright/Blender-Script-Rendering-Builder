/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder class BlenderInfo
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 0.5
 * -----------------------------------------------------------------------------------------------------------
 * This file contains the variables and functions that are required make an Blender File for the UI when outputting
 * to a script file.
 * -----------------------------------------------------------------------------------------------------------
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blender_Script_Rendering_Builder.Shared
{
    class BlenderInstance
    {
        #region Class Variables
        /// <summary>
        /// Full Path to the Blender file
        /// </summary>
        public string fullPath { get; set; }

        /// <summary>
        /// A list of scenes we want to render from in the Blender file
        /// </summary>
        public List<SceneInstance> scenes { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public BlenderInstance()
        {

        }

        /// <summary>
        /// Overloaded constructor that accepts the Blender file's full path and scenes for that Blender file
        /// </summary>
        /// <param name="fullPath">The full path to the blender file</param>
        /// <param name="scenes">A list of scenes we want to render from this blender file</param>
        public BlenderInstance(string fullPath, List<SceneInstance> scenes)
        {
            try
            {
                this.fullPath = fullPath;
                this.scenes = scenes;
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion
    }
}
