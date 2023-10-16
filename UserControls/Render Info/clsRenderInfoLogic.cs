/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder class clsRenderInfoLogic
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 0.5
 *  ----------------------------------------------------------------------------------------------------------
 * This file contains the logistic for the UserControl RenderInfo so that the logistics is not behind the UI.
 * -----------------------------------------------------------------------------------------------------------
 */

using Blender_Script_Rendering_Builder.Modules;
using Blender_Script_Rendering_Builder.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Blender_Script_Rendering_Builder.UserControls.Render_Info
{
    class clsRenderInfoLogic
    {
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public clsRenderInfoLogic()
        {

        }
        #endregion

        #region Functions
        /// <summary>
        /// Returns a list of valid options to define what type of render the user wants to do.
        /// </summary>
        /// <returns>A list of valid options for the user to chose what render they want to do.</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across.</exception>
        public List<string> AnimationOrFrameList()
        {
            try
            {
                return new List<string>()
                {
                    "Use Blender configs",
                    "Animation",
                    "Frame Range",
                    "Frames Custom"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns a list of valid options to define what output file type they want to output.
        /// </summary>
        /// <returns>A list of valid options for the user to chose what output file type they want to output.</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across.</exception>
        public List<string> OutputFileTypeList()
        {
            try
            {
                return new List<string>()
                {
                    "Use Blender configs",
                    "avijpeg",
                    "aviraw",
                    "bmp",
                    "iris",
                    "iriz",
                    "jpeg",
                    "png",
                    "rawtga",
                    "tga"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns a list of valid options to define what rendering engine they want to use.
        /// </summary>
        /// <returns>A list of valid options for the user to chose what rendering engine they want to use.</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across.</exception>
        public List<string> RenderingEngineList()
        {
            try
            {
                return new List<string>
                {
                    "Use Blender configs",
                    "Cycles",
                    "Eevee",
                    "Workbench"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> OutputFolderList()
        {
            try
            {
                return new List<string>
                {
                    "Use Blender configs",
                    "Browse for folder"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Will grab lowest child folder's name from the path supplied
        /// Source https://stackoverflow.com/a/29901348
        /// </summary>
        /// <param name="folderPath">The folder path to the folder</param>
        /// <returns>The name of the folder the farthest from the root. I.E. the folder that you selected</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across.</exception>
        public string ExtractFolderName(string folderPath)
        {
            try
            {
                char pathSeparator = System.IO.Path.DirectorySeparatorChar;  // Grabs the character the system uses to separate directories
                string regexPattern = @".*\" + pathSeparator + @"([^\" + pathSeparator + "]+)";  // Make a Regex pattern to look for the folder
                string folderName = Regex.Match(folderPath, regexPattern).Groups[1].ToString();  // Find the folder's name
                return folderName;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion
    }
}
