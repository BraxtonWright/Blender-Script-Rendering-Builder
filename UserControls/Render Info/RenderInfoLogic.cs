/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder class RenderInfoLogic
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 0.5
 *  ----------------------------------------------------------------------------------------------------------
 * This file contains the logistic for the UserControl RenderInfo so that the logistics is not behind the UI.
 * -----------------------------------------------------------------------------------------------------------
 */

using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Blender_Script_Rendering_Builder.UserControls.Render_Info
{
    class RenderInfoLogic
    {
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public RenderInfoLogic()
        {

        }
        #endregion

        #region Functions
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
