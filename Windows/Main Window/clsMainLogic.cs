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

using Blender_Script_Rendering_Builder.Classes.Helpers;
using Blender_Script_Rendering_Builder.Classes.Modules;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

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
            try
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
                else if (!File.Exists(blenderApplicationPath))
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
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public bool ScriptInfoValid(List<BlenderData> renderInformation)
        {
            try
            {
                bool isValid = true;  // This will be modified with the &= bitwise operator, this means that this and what comes after the &= have to be true for it to stay as true.  But if one of them is false, it stays false.

                // Foreach blender file
                foreach (BlenderData blendData in renderInformation)
                {
                    if (Validators.StringEmpty(blendData.FullPath)) isValid &= false;

                    else
                    {
                        // Foreach scene
                        foreach (SceneData sceneData in blendData.scenesInfo)
                        {
                            // The scene name is not valid
                            if (!Validators.SceneNameValid(sceneData.SceneName)) isValid &= false;

                            else
                            {
                                // Foreach rendering information
                                foreach (RenderData renderData in sceneData.rendersInfo)
                                {
                                    // We inverse the return from Validators.StringEmpty() because it returns true if it is empty and we want it to be false if it is empty for this use case
                                    // We also don't check the combobox items to see if they are null or empty because we have them default to an option.
                                    if (renderData.RenderType == "Custom Frames") isValid &= Validators.CustomFramesValid(renderData.CustomFrames);
                                    if (renderData.OutputPathSelection == "Browse") isValid &= !Validators.StringEmpty(renderData.OutputFullPath);
                                }
                            }
                        }
                    }
                }

                return isValid;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Generate a .bat file containing the necessary information for rendering
        /// </summary>
        /// <param name="renderInformation">A list of BlenderData containing everything required for generateing the script</param>
        /// <param name="saveFilePath">The full path to a file where they want save the script to</param>
        /// <param name="shutdownTime">The number of minutes to wait until shuting down the PC</param>
        /// <exception cref="Exception">Catches any exceptions that this method might come across.</exception>
        public void GenerateScriptFile(List<BlenderData> renderInformation, string saveFilePath, bool shutdown, int shutdownTime)
        {
            try
            {
                string blenderApplicationPath = Path.GetDirectoryName(Properties.Settings.Default.BlenderApplicationPath);
                string blenderApplicationName = Path.GetFileName(Properties.Settings.Default.BlenderApplicationPath);

                //Create the stream writer
                //StreamWriter MyWriter = File.CreateText(sFile);
                //or
                using (StreamWriter MyWriter = new StreamWriter(saveFilePath))
                {
                    // Equivalent switches for the below lines of code
                    // --background == -b
                    // --scene == -S
                    // --render-output == -o
                    // --render-format == -F
                    // --engine -E
                    // --render-anim -a
                    // --render-frame -f
                    // --frame-start == -s
                    // --frame-end == -e
                    MyWriter.WriteLine($"cd \"{blenderApplicationPath}\"\n");

                    // Foreach blender file
                    foreach (BlenderData blendData in renderInformation)
                    {
                        // Foreach scene
                        foreach (SceneData sceneData in blendData.scenesInfo)
                        {
                            MyWriter.Write($"{blenderApplicationName} --background \"{blendData.FullPath}\" --scene {sceneData.SceneName}");

                            // Foreach rendering information
                            foreach (RenderData renderData in sceneData.rendersInfo)
                            {
                                // The user has defined a different folder to save the renders to
                                if (renderData.OutputPathSelection == "Browse")
                                {
                                    MyWriter.Write($" --render-output \"{renderData.OutputFullPath}\"");
                                }
                                // The user has defined a different output file type for the renders
                                if (renderData.OutputFileType != "Use Blender configs" && renderData.OutputFileType != "")
                                {
                                    MyWriter.Write($" --render-format {renderData.OutputFileType.ToUpper()}");
                                }
                                // The user has defined a different rendering engine for the renders
                                if (renderData.RenderEngine != "Use Blender configs" && renderData.RenderEngine != "")
                                {
                                    MyWriter.Write(" --engine ");
                                    switch (renderData.RenderEngine)
                                    {
                                        case "Cycles":
                                            MyWriter.Write("CYCLES");
                                            break;
                                        case "Eevee":
                                            MyWriter.Write("BLENDER_EEVEE");
                                            break;
                                        case "Workbench":
                                            MyWriter.Write("BLENDER_WORKBENCH");
                                            break;
                                    }
                                }
                                // Depending on what is selected for the render type, process the results
                                switch (renderData.RenderType)
                                {
                                    case "Use Blender configs":
                                        MyWriter.Write(" --render-anim");
                                        break;
                                    case "Animation":
                                        MyWriter.Write($" --frame-start {renderData.StartFrame} --frame-end {renderData.EndFrame} --render-anim");
                                        break;
                                    case "Frame Range":
                                        MyWriter.Write($" --render-frame {renderData.StartFrame}..{renderData.EndFrame}");
                                        break;
                                    case "Custom Frames":
                                        string processedRenderData = Regex.Replace(renderData.CustomFrames, " ", "");  // Remove any spaces from the data because spaces are not allowed
                                        processedRenderData = Regex.Replace(processedRenderData, "-", "..");  // Replace any "-" with ".." because blender uses ".." to represent a range of frames

                                        MyWriter.Write($" --render-frame {processedRenderData}");
                                        break;
                                }

                                MyWriter.WriteLine("\n");  // Used to move to a new line and add some space between render jobs
                            }
                        }
                    }

                    if (shutdown)
                    {
                        MyWriter.WriteLine("Timeout /T " + 60 * shutdownTime);
                        MyWriter.WriteLine("Shutdown \\s");
                    }

                    //Don't need to call "Close" because we use the "using" statement
                    //The "using" statement is the same as a try/catch block and putting the "FileRead.Dispose" method call in the "finally" statment
                    //FileRead.Close();
                }

                MessageBox.Show("Your script file has been generated and can be located here:\n" + saveFilePath, "File saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion
    }
}
