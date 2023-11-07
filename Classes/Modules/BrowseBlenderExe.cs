/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder class BrowseBlenderExe
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 0.5
 * -----------------------------------------------------------------------------------------------------------
 * This file contains the variables that will allow us to return an object to the calling function for us to
 * use the browse for Blender executible.
 * -----------------------------------------------------------------------------------------------------------
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blender_Script_Rendering_Builder.Classes.Modules
{
    class BrowseBlenderExe
    {
        #region Class variables
        /// <summary>
        /// Need to open the browse for Blender executible
        /// </summary>
        public bool needToOpenWindow { get; set; }

        /// <summary>
        /// The tile of the window to be opened
        /// </summary>
        public string windowTitle { get; set; }

        /// <summary>
        /// The message to be displayed on the window
        /// </summary>
        public string windowMessage { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Default contstructor
        /// </summary>
        public BrowseBlenderExe()
        {

        }

        /// <summary>
        /// Overloaded constructor for when you don't need to open the window
        /// </summary>
        /// <param name="needToOpenWindow">A bool repersenting if we need to open the window</param>
        public BrowseBlenderExe(bool needToOpenWindow)
        {
            this.needToOpenWindow = needToOpenWindow;
        }

        /// <summary>
        /// Overloaded constructor for when you need to open the window
        /// </summary>
        /// <param name="needToOpenWindow">A bool repersenting if we need to open the window</param>
        /// <param name="windowTitle">The tile of the window</param>
        /// <param name="windowMessage">The message to be displayed</param>
        public BrowseBlenderExe(bool needToOpenWindow, string windowTitle, string windowMessage) : this(needToOpenWindow)  // This constructor inherits from the above constructor so we don't need include the same code inside this constructor
        {
            this.windowTitle = windowTitle;
            this.windowMessage = windowMessage;
        }
        #endregion
    }
}
