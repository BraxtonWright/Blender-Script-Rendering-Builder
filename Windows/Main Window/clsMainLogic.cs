/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder class clsMainWindowLogic
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 0.5
 *  ----------------------------------------------------------------------------------------------------------
 * This file contains the logistic for the main window so that the logistics is not behind the UI.
 * -----------------------------------------------------------------------------------------------------------
 */

using Blender_Script_Rendering_Builder.Classes.Modules;
using Blender_Script_Rendering_Builder.UserControls.Blender_Selection;
using Blender_Script_Rendering_Builder.Windows.Browse_Blender_Executible;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Controls;

namespace Blender_Script_Rendering_Builder.Main
{
    class clsMainLogic
    {
        #region Variables
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public clsMainLogic()
        {
        
        }
        #endregion

        #region Functions
        /// <summary>
        /// Determines if the window used to browse for the Blender application should be opened, and if so what should it display
        /// </summary>
        /// <returns>An instance of the class BrowseBlenderExe with properties containing information how to preced</returns>
        public BrowseBlenderExe ShouldOpenBlendApplictionWindow()
        {
            BrowseBlenderExe returnObject = new BrowseBlenderExe();

            string blenderApplicationPath = Properties.Settings.Default.BlenderApplicationPath;

            //blenderApplicationPath = "C:\\bad";  // For simply testing each condition the window can be in

            // It is the first time opening the application
            if (String.IsNullOrEmpty(blenderApplicationPath))
            {
                Trace.WriteLine("Open the \"Please read\" window because this is the first time launching the applcation");

                returnObject.needToOpenWindow = true;
                returnObject.windowTitle = "Please read";
                returnObject.windowMessage = "Before you can use this program, it has to know where your Blender executible is located.  Press the browse button for you to locate it.  This can be changed in the future by pressing the Config tab in the main application window.";
            }
            // The application path is no longer there
            else if(!File.Exists(blenderApplicationPath))
            {
                Trace.WriteLine("Open the \"Executible location changed\" window because the application path has been moves/removed");

                returnObject.needToOpenWindow = true;
                returnObject.windowTitle = "Executible location changed";
                returnObject.windowMessage = "This program has discovered that the exeuctible defined previously has either been moved or removed.  Please redefine where the executible is to continue using this program.";
            }
            // Everything is valid
            else
            {
                Trace.WriteLine("Do nothing");

                returnObject.needToOpenWindow = false;
            }

            return returnObject;
        }

        public void GenerateScriptFile(StackPanel spBlenderFiles)
        {
            try
            {
                
                List<BlenderData> renderInfo = new List<BlenderData>();

                foreach (BlenderSelection blenderUserControl in spBlenderFiles.Children)
                {
                    // WIP
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion
    }
}
