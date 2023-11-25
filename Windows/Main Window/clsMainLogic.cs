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
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using System.Windows.Forms;
using Blender_Script_Rendering_Builder.Windows.Error_List;


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

        /// <summary>
        /// Determine if the rendering information is valid. If it is, then it will then generate a script file using the rendering information.
        /// </summary>
        /// <param name="renderingInfo">A list containing all the blender files, scenes, and rendering info required to generate the script</param>
        /// <param name="shutdown">A boolean representing if the PC should shutdown after the script finishes</param>
        /// <param name="shutdownTime">The time in minuites for the PC to shutdown after finishing rendering</param>
        /// <exception cref="Exception">Catches any exceptions that this method might come across</exception>
        public void GenerateScriptFileIfValid(List<BlenderData> renderingInfo, bool shutdown, int shutdownTime)
        {
            try
            {
                ScriptInfoValid scriptInfoValid = ScriptInfoIsValid(renderingInfo);

                // Info is valid so allow the user to chose where to save the file
                if (scriptInfoValid.Valid)
                {
                    SaveFileDialog saveFileDailog = new SaveFileDialog();
                    saveFileDailog.Filter = "Batch file (*.bat)|*.bat";

                    // If the window has closed correctly, I.E. the user has chosen where to save the file, generate the script file
                    if (saveFileDailog.ShowDialog() == DialogResult.OK)
                    {
                        GenerateScriptFile(renderingInfo, saveFileDailog.FileName, shutdown, shutdownTime);
                    }
                }
                // Info is isvalid so display a new window with the list of errors found
                else
                {
                    wndErrorList wndErrorList = new wndErrorList(scriptInfoValid.Errors);

                    wndErrorList.Show();  //open this new window and allow the user to continue to use the original window
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Determines if the script information is valid
        /// </summary>
        /// <param name="renderingInfo"></param>
        /// <returns>An instance of the class ScriptInfoValid containing two properties "Valid" and optionally "Errors" if any were detected</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across</exception>
        private ScriptInfoValid ScriptInfoIsValid(List<BlenderData> renderingInfo)
        {
            try
            {
                bool isValid = true;  // This will be modified with the &= bitwise operator, this means that this and what comes after the &= have to be true for it to stay as true.  But if one of them is false, it stays false.
                List<ErrorTreeBranch> tree = new List<ErrorTreeBranch>();  // This is a virtual tree of the any errors it discovers while going through the data

                // If no blender files have been defined
                if (renderingInfo.Count <= 0)
                {
                    isValid &= false;
                    // Add a error message
                    ErrorTreeBranch blenderBranch = new ErrorTreeBranch("There is no information for the script, please click the \"Add a new Blender file\" and supply the required infomration and try again.");

                    tree.Add(blenderBranch);
                }

                // Foreach blender file
                foreach (BlenderData blendData in renderingInfo)
                {
                    // The blender file path is not defined
                    if (Validators.StringEmpty(blendData.FullPath))
                    {
                        isValid &= false;
                        // Add a error message
                        ErrorTreeBranch blenderBranch = new ErrorTreeBranch("A Blender file is not supplied.");

                        tree.Add(blenderBranch);
                    }

                    else
                    {
                        // Foreach scene
                        foreach (SceneData sceneData in blendData.scenesInfo)
                        {
                            ValidatorsReturn results = Validators.SceneNameValid(sceneData.SceneName);

                            // The scene name is not valid
                            if (!results.Valid)
                            {
                                isValid &= false;
                                // Add a error message
                                ErrorTreeBranch sceneBranch = new ErrorTreeBranch(results.ErrorMessage);
                                ErrorTreeBranch blenderBranch = new ErrorTreeBranch("File: " + blendData.FileName);
                                blenderBranch.BranchErrors.Add(sceneBranch);

                                tree.Add(blenderBranch);
                            }

                            else
                            {
                                // Create the two instances of the class ErrorTreeBranch and create a local variable to determin if these instances of the class should be added to the tree
                                ErrorTreeBranch sceneBranch = new ErrorTreeBranch("Scene: " + sceneData.SceneName);
                                ErrorTreeBranch blenderBranch = new ErrorTreeBranch("File: " + blendData.FileName);
                                bool errorsDetected = false;  // This will be modified with the |= bitwise operator, this means that this or what comes after the |= has to be true for it to become true.  But once it beomces true, it stays true.

                                // Foreach rendering information
                                foreach (RenderData renderData in sceneData.rendersInfo)
                                {
                                    ValidatorsReturn customFrameResults = Validators.CustomFramesValid(renderData.CustomFrames);


                                    // The custom frames combobox is not valid
                                    if (renderData.RenderType == "Custom Frames" && !customFrameResults.Valid)
                                    {
                                        isValid &= false;

                                        // Add a error message
                                        ErrorTreeBranch renderBranch = new ErrorTreeBranch(customFrameResults.ErrorMessage);
                                        sceneBranch.BranchErrors.Add(renderBranch);
                                        errorsDetected |= true;
                                    }
                                    // The output folder has not been defined (this should no longer be possible because if they close the folder browse window, it sets the control to use the settings inside of blender)
                                    if (renderData.OutputPathSelection == "Browse for folder" && Validators.StringEmpty(renderData.OutputFullPath))
                                    {
                                        isValid &= false;

                                        // Add a error message (WIP)
                                        ErrorTreeBranch renderBranch = new ErrorTreeBranch("An output folder has not been defined");
                                        sceneBranch.BranchErrors.Add(renderBranch);
                                        errorsDetected |= true;
                                    }
                                }

                                // If some errors were detected, add the scene to the blenderBranch and then add the blenderBranch to the tree
                                if(errorsDetected)
                                {
                                    blenderBranch.BranchErrors.Add(sceneBranch);
                                    tree.Add(blenderBranch);
                                }
                            }
                        }
                    }
                }

                if(isValid)
                {
                    return new ScriptInfoValid(isValid);
                }
                else
                {
                    return new ScriptInfoValid(isValid, tree);  // WIP (need to make the class accept a list of errors generated here)
                }
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
        /// <exception cref="Exception">Catches any exceptions that this method might come across</exception>
        private void GenerateScriptFile(List<BlenderData> renderInformation, string saveFilePath, bool shutdown, int shutdownTime)
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
