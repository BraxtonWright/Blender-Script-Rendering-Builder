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

namespace Blender_Script_Rendering_Builder.Modules
{
    class BlenderModel
    {
        /// <summary>
        /// Full Path to the Blender file
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// A list of scenes we want to render from in the Blender file
        /// </summary>
        public List<SceneModel> Scenes { get; set; }
    }
}
