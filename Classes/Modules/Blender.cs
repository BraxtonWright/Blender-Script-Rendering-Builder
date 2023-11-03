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

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Blender_Script_Rendering_Builder.Classes.Modules
{
    internal class Blender : INotifyPropertyChangedImplmented
    {
        #region Private class variables
        string _fullPath;
        string _fileName;
        #endregion

        #region Getters/Setters
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
                _fileName = ExtractFileName(_fullPath);
                OnPropertyChanged(nameof(FileName));  // This is here instead of the coresponding getter becuase when we set the FullPath, the FileName also changes along side it. 
            }
        }

        /// <summary>
        /// The name of the blender file
        /// </summary>
        public string FileName
        {
            get { return _fileName; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public Blender()
        {
            // do nothing
        }
        #endregion

        #region Functions
        /// <summary>
        /// Grabs the name of the file from the full path to the file.
        /// Source https://forum.uipath.com/t/regex-getting-filename-out-from-filepath/190312/3
        /// </summary>
        /// <param name="FullPath">The full path to the file.</param>
        /// <exception cref="Exception">Catches any exceptions that this method might come across.</exception>
        private string ExtractFileName(string FullPath)
        {
            try
            {
                return System.IO.Path.GetFileName(FullPath);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Extract all the nessary information required for the script
        /// </summary>
        /// <returns>The full path to the blender file</returns>
        public string ExtractData()
        {
            return FullPath;
        }
        #endregion
    }
}
