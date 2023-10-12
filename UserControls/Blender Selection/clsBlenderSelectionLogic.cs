/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder class clsBlenderSelectionLogic
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 0.5
 *  ----------------------------------------------------------------------------------------------------------
 * This file contains the logistic for the UserControl BlenderSelection so that the logistics is not behind the UI.
 * -----------------------------------------------------------------------------------------------------------
 */

using System;
using System.Reflection;

namespace Blender_Script_Rendering_Builder.UserControls.Blender_Selection
{
    class clsBlenderSelectionLogic
    {
        #region Class Variables
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public clsBlenderSelectionLogic()
        {

        }
        #endregion

        #region Functions
        /// <summary>
        /// Grabs the name of the file from the full path to the file.
        /// Source https://forum.uipath.com/t/regex-getting-filename-out-from-filepath/190312/3
        /// </summary>
        /// <param name="FullPath">The full path to the file.</param>
        /// <exception cref="Exception">Catches any exceptions that this method might come across.</exception>
        public string ExtractFileName(string FullPath)
        {
            try
            {
                return System.IO.Path.GetFileName(FullPath);
            }
            catch (Exception ex)
            {
                throw new System.Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion
    }

}
